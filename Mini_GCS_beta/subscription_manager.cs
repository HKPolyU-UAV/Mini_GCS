using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace Mini_GCS_beta
{
    class subscription_manager
    {
        /**
         *  Private variables
         */
        private int queue_cnt = 0;
        private List<ConcurrentQueue<DFrame>> queue_list = new List<ConcurrentQueue<DFrame>>();
        private List<int> limit_list = new List<int>();

        /**
         *  Public member functions
         */

        /**
         *  @brief Register a new subscriber to DFrame msgs
         *         subscriber cannot be deleted
         *  @param limit: buffer size. when number of pending msgs exceeds this value, 
         *                old msgs will be thrown away
         *  @retval int: returns the registration id. use this id to
         *               retrive subscribed msgs later
         */
        public int register_new_subscriber(int limit)
        {
            lock (this)
            {
                queue_list.Add(new ConcurrentQueue<DFrame>());
                limit_list.Add(limit);
                queue_cnt += 1;
            }
            return queue_cnt - 1;
        }

        /**
         *  @brief Push new DFrame msgs to all subscribers
         *  @param msg: DFrame msg to be published
         *  @retval: none
         */
        public void publish(DFrame msg)
        {
            DFrame tmp;
            int i = 0;
            for (i = 0; i < queue_cnt; i++)
            {
                queue_list[i].Enqueue(msg);
                lock (this)
                {
                    if (queue_list[i].Count >= limit_list[i])
                        queue_list[i].TryDequeue(out tmp);
                }
            }
        }

        /**
         *  @bried Retrive the latest DFrame msg published
         *  @param id: registration is obtained when registering new subscriber
         *  @param msg: stores retrived msg
         *  @retval bool: whether the operation is successful or not
         */
        public bool subscribe(int id, out DFrame msg)
        {
            return queue_list[id].TryDequeue(out msg);
        }


    }
}
