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
using System.IO;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.Drawing.Drawing2D;

namespace Mini_GCS_beta
{
    partial class Form1
    {
        public PointLatLng lastPosition;
        PointLatLng latLng;
        GMapOverlay GCSmarkersOverlay = new GMapOverlay("GCS_marker");
        GMapOverlay mommarkersOverlay = new GMapOverlay("momFW_marker");
        GMapOverlay sonmarkersOverlay = new GMapOverlay("son_marker");
        GMapOverlay markerOverlay = new GMapOverlay("marker"); // Mouse click to mark
        List<PointLatLng> points = new List<PointLatLng>();
        List<PointLatLng> GCSpoints = new List<PointLatLng>();
        List<PointLatLng> mompoints = new List<PointLatLng>();
        List<PointLatLng> sonpoints = new List<PointLatLng>();
        GMapOverlay GCSrouteOverlay = new GMapOverlay("GCSroute");
        GMapOverlay momrouteOverlay = new GMapOverlay("momroute");
        GMapOverlay sonrouteOverlay = new GMapOverlay("sonroute");
       // GMapOverlay routeOverlay = new GMapOverlay("route");  //draw route


        // Gmap Load


        private void timer_GPS_Tick(object sender, EventArgs e)
        {   
            
            /*mom location update*/
            mommarkersOverlay.Clear();
            GMarkerGoogle mommarker = new GMarkerGoogle(new PointLatLng(uav_list[1].lat, uav_list[1].lon),
            RotateImage(Image.FromFile("Drawing.png"), uav_list[1].heading));
            mommarker.ToolTipText = "mom";
            mommarker.ToolTipMode = MarkerTooltipMode.Always; //Text always on;
            mommarkersOverlay.Markers.Add(mommarker);
            gmap.Overlays.Add(mommarkersOverlay);
            //addmomRoute();

            /*son location update*/
            sonmarkersOverlay.Clear();
            GMarkerGoogle sonmarker = new GMarkerGoogle(new PointLatLng(uav_list[2].lat, uav_list[2].lon),
            RotateImage(Image.FromFile("Drawing1.png"), uav_list[2].heading));
            sonmarker.ToolTipText = "son";
            sonmarker.ToolTipMode = MarkerTooltipMode.Always; //Text always on;
            sonmarkersOverlay.Markers.Add(sonmarker);
            gmap.Overlays.Add(sonmarkersOverlay);
            //addsonRoute();
        }

        void addmomRoute()
        {
            momrouteOverlay.Clear();
            mompoints.Add(new PointLatLng(uav_list[1].lat, uav_list[1].lon));//add points
            GMapRoute momroute = new GMapRoute(mompoints, "momroute"); //set route
            momroute.Stroke = new Pen(Color.Green, 1);
            momrouteOverlay.Routes.Add(momroute);
            gmap.Overlays.Add(momrouteOverlay);

        }

        void addsonRoute()
        {
            sonrouteOverlay.Clear();
            sonpoints.Add(new PointLatLng(uav_list[2].lat, uav_list[2].lon));//add points
            GMapRoute sonroute = new GMapRoute(sonpoints, "momroute"); //set route
            sonroute.Stroke = new Pen(Color.Blue, 1);
            sonrouteOverlay.Routes.Add(sonroute);
            gmap.Overlays.Add(sonrouteOverlay);

        }

        public static Bitmap RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        private void gmap_MouseMove(object sender, MouseEventArgs e)
        {
            PointLatLng latLng = this.gmap.FromLocalToLatLng(e.X, e.Y);
            this.label6.Text = latLng.Lng.ToString() + " " + latLng.Lat.ToString();
            lastPosition = latLng;
        }

        private void gmap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                markerOverlay.Clear();
                latLng = this.gmap.FromLocalToLatLng(e.X, e.Y);
                this.label5.Text = latLng.Lng.ToString() + " " + latLng.Lat.ToString();
                lastPosition = latLng;
                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(latLng.Lat, latLng.Lng),
                GMarkerGoogleType.green);      
                marker.ToolTipMode = MarkerTooltipMode.Always; //Text always on;
                markerOverlay.Markers.Add(marker);
                gmap.Overlays.Add(markerOverlay);
            }
        }

        private void Readmap_Click(object sender, EventArgs e)
        {
            this.gmap.Manager.Mode = AccessMode.CacheOnly;
            if (this.gmap.ShowImportDialog() == true)
            {
                // GMap.NET.CacheProviders.SQLitePureImageCache ms = new SQLitePureImageCache();  
                // ms.CacheLocation = "E:\\123\\";  
                // MessageBox.Show(ms.db);  
                //MessageBox.Show(ms.dir);  
                this.gmap.Position = new PointLatLng(23.10611, 120.21083);
                this.gmap.ReloadMap();
            }
        }

        public void PointCal(double Distance, string direction, double lat, double lon, double heading)
        {
            double newlat, newlon;
            const double lat_udis = 0.11113333;
            double lon_udis = 0.11131955 * Math.Cos((lat / 180) * 3.1415926);

            if (direction == "forward")
            {
                newlat = lat + Distance * Math.Cos((heading / 180) * 3.1415926) / lat_udis;
                newlon = lon + Distance * Math.Sin((heading / 180) * 3.1415926) / lon_udis;
            }
            else if (direction == "backward")
            {
                newlat = lat - Distance * Math.Cos((heading / 180) * 3.1415926) / lat_udis;
                newlon = lon - Distance * Math.Sin((heading / 180) * 3.1415926) / lon_udis;
            }
            else if (direction == "left")
            {
                newlat = lat + Distance * Math.Sin((heading / 180) * 3.1415926) / lat_udis;
                newlon = lon - Distance * Math.Cos((heading / 180) * 3.1415926) / lon_udis;
            }
            else if (direction == "right")
            { 
                newlat = lat - Distance * Math.Sin((heading / 180) * 3.1415926) / lat_udis;
                newlon = lon + Distance * Math.Cos((heading / 180) * 3.1415926) / lon_udis;
            }

        
        }

    }
            
    
}
