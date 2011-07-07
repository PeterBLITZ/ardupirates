using System;
using System.Collections.Generic; // Lists
using System.Text; // stringbuilder
using System.Drawing; // pens etc
using System.IO; // file io
using System.IO.Ports; // serial
using System.Windows.Forms; // Forms
using System.Collections; // hashs
using System.Text.RegularExpressions; // regex
using System.Xml; // GE xml alt reader
using System.Net; // dns, ip address
using System.Net.Sockets; // tcplistner
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Globalization; // language
using GMap.NET.WindowsForms.Markers;
using ZedGraph; // Graphs
using System.Drawing.Drawing2D;

// written by michael oborne
namespace ArdupilotMega.GCSViews
{
    partial class FlightData : UserControl
    {
        ArdupilotMega.MAVLink comPort = MainV2.comPort;
        public static int threadrun = 0;
        StreamWriter swlog;
        int tickStart = 0;
        RollingPointPairList list1 = new RollingPointPairList(1200);
        RollingPointPairList list2 = new RollingPointPairList(1200);
        RollingPointPairList list3 = new RollingPointPairList(1200);
        RollingPointPairList list4 = new RollingPointPairList(1200);

        List<PointLatLng> trackPoints = new List<PointLatLng>();

        const float rad2deg = (float)(180 / Math.PI);

        const float deg2rad = (float)(1.0 / rad2deg);

        public static hud.HUD myhud = null;

        public SplitContainer MainHcopy = null;

        protected override void Dispose(bool disposing)
        {
            MainV2.config["FlightSplitter"] = MainH.SplitterDistance.ToString();
            base.Dispose(disposing);
        }

        public FlightData()
        {
            InitializeComponent();

            myhud = hud1;
            MainHcopy = MainH;

            Control.CheckForIllegalCrossThreadCalls = false; // so can update display from another thread

            List<string> list = new List<string>();

            //foreach (object obj in Enum.GetValues(typeof(MAVLink.MAV_ACTION)))
            {
                list.Add("RETURN");
                list.Add("HALT");
                list.Add("CONTINUE");
                list.Add("SET_MANUAL");
                list.Add("SET_AUTO");
                list.Add("STORAGE_READ");
                list.Add("STORAGE_WRITE");
                list.Add("CALIBRATE_RC");
                list.Add("NAVIGATE");
                list.Add("LOITER");
            }

            CMB_action.DataSource = list;

            CreateChart(zg1);

            // config map             
            gMapControl1.MapType = MapType.GoogleSatellite;
            gMapControl1.MinZoom = 1;
            gMapControl1.CacheLocation = Path.GetDirectoryName(Application.ExecutablePath) + "/gmapcache/";

            gMapControl1.OnMapZoomChanged += new MapZoomChanged(gMapControl1_OnMapZoomChanged);

            gMapControl1.Zoom = 3;           

            polygons = new GMapOverlay(gMapControl1, "polygons");
            gMapControl1.Overlays.Add(polygons);

            routes = new GMapOverlay(gMapControl1, "routes");
            gMapControl1.Overlays.Add(routes);
        }

        void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            //e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;

            LinearGradientBrush linear = new LinearGradientBrush(e.Bounds, Color.FromArgb(0x94, 0xc1, 0x1f), Color.FromArgb(0xcd, 0xe2, 0x96), LinearGradientMode.Vertical);

            e.Graphics.FillRectangle(linear, e.Bounds);

            // Draw the current item text based on the current Font 
            // and the custom brush settings.
            e.Graphics.DrawString(((TabControl)sender).TabPages[e.Index].Text.ToString(),
                e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();

        }

        void gMapControl1_OnMapZoomChanged()
        {
            Zoomlevel.Value = Convert.ToDecimal(gMapControl1.Zoom);
        }

        private void GCS_Load(object sender, EventArgs e)
        {

            System.Threading.Thread t11 = new System.Threading.Thread(delegate() { mainloop(); });
            t11.IsBackground = true;
            t11.Name = "GCS Serial listener";
            t11.Start();
            //MainH.threads.Add(t11);

            Zoomlevel.Minimum = gMapControl1.MinZoom;
            Zoomlevel.Maximum = gMapControl1.MaxZoom + 1;
            Zoomlevel.Value = Convert.ToDecimal(gMapControl1.Zoom);
        }

        private void mainloop()
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            threadrun = 1;
            EndPoint Remote = (EndPoint)(new IPEndPoint(IPAddress.Any, 0));

            DateTime lastdata = DateTime.Now.AddSeconds(-20);

            DateTime tracklast = DateTime.Now.AddSeconds(0);

            DateTime tunning = DateTime.Now.AddSeconds(0);

            //comPort.stopall(true);

            while (threadrun == 1)
            {
                if (threadrun == 0) { return; }

                if (MainV2.givecomport == true)
                {
                    System.Threading.Thread.Sleep(20);
                    continue;
                }
                // re-request servo data
                if (!(lastdata.AddMilliseconds(8000) > DateTime.Now) && comPort.IsOpen) // 8 seconds
                {
                    Console.WriteLine("REQ streams - flightdata");
                    try
                    {
                        //comPort.requestDatastream((byte)ArdupilotMega.MAVLink.MAV_DATA_STREAM.MAV_DATA_STREAM_RAW_CONTROLLER, 3); // request servoout
                        comPort.requestDatastream((byte)ArdupilotMega.MAVLink.MAV_DATA_STREAM.MAV_DATA_STREAM_EXTENDED_STATUS, 1); // mode
                        comPort.requestDatastream((byte)ArdupilotMega.MAVLink.MAV_DATA_STREAM.MAV_DATA_STREAM_POSITION, 3); // request gps
                        comPort.requestDatastream((byte)ArdupilotMega.MAVLink.MAV_DATA_STREAM.MAV_DATA_STREAM_EXTRA1, 10); // request attitude
                        comPort.requestDatastream((byte)ArdupilotMega.MAVLink.MAV_DATA_STREAM.MAV_DATA_STREAM_EXTRA2, 10); // request vfr
                        //comPort.requestDatastream((byte)ArdupilotMega.MAVLink.MAV_DATA_STREAM.MAV_DATA_STREAM_RAW_SENSORS, 0); // request raw sensor
                        comPort.requestDatastream((byte)ArdupilotMega.MAVLink.MAV_DATA_STREAM.MAV_DATA_STREAM_RC_CHANNELS, 3); // request rc info
                    }
                    catch { }
                    lastdata = DateTime.Now; // prevent flooding
                }
                if (!MainV2.comPort.logreadmode)
                    System.Threading.Thread.Sleep(100); // max is only ever 10 hz

                if (MainV2.comPort.logreadmode && MainV2.comPort.logplaybackfile != null)
                {
                    DateTime logplayback = MainV2.comPort.lastlogread;

                    MainV2.comPort.readPacket();
                    tracklog.Value = (int)(MainV2.comPort.logplaybackfile.BaseStream.Position / (double)MainV2.comPort.logplaybackfile.BaseStream.Length * 100);

                    int ts = (int)(MainV2.comPort.lastlogread - logplayback).TotalMilliseconds;
                    if (ts > 0)
                        System.Threading.Thread.Sleep(ts);

                    if (MainV2.comPort.logplaybackfile.BaseStream.Position == MainV2.comPort.logplaybackfile.BaseStream.Length)
                    {
                        MainV2.comPort.logreadmode = false;
                    }
                }

                try
                {
                    //Console.WriteLine(DateTime.Now.Millisecond);
                    MainV2.cs.UpdateCurrentSettings(bindingSource1);
                    //Console.WriteLine(DateTime.Now.Millisecond + " done ");

                    if (tunning.AddMilliseconds(50) < DateTime.Now && CB_tuning.Checked == true)
                    {

                        double time = (Environment.TickCount - tickStart) / 1000.0;

                        list1.Add(time, MainV2.cs.roll);

                        list2.Add(time, MainV2.cs.pitch);

                        list3.Add(time, MainV2.cs.nav_roll);

                        list4.Add(time, MainV2.cs.nav_pitch);
                    }

                    if (tracklast.AddSeconds(1) < DateTime.Now)
                    {
                        if (trackPoints.Count > 200)
                        {
                            trackPoints.RemoveRange(0, trackPoints.Count - 200);
                        }
                        if (MainV2.cs.lat != 0)
                            trackPoints.Add(new PointLatLng(MainV2.cs.lat, MainV2.cs.lng));



                        if (CB_tuning.Checked == false) // draw if in view
                        {
                            Application.DoEvents();

                            routes.Markers.Clear();
                            routes.Routes.Clear();

                            route = new GMapRoute(trackPoints, "track");
                            //track.Stroke = Pens.Red;
                            //route.Stroke = new Pen(Color.FromArgb(144, Color.Red));
                            //route.Stroke.Width = 5;
                            //route.Tag = "track";
                            routes.Routes.Add(route);

                            if (DateTime.Now.Second % 10 == 0)
                            {
                                polygons.Markers.Clear();

                                PointLatLng[] temp = new PointLatLng[FlightPlanner.pointlist.Count];

                                foreach (PointLatLngAlt plla in FlightPlanner.pointlist)
                                {
                                    addpolygonmarker(plla.Tag, plla.Lng, plla.Lat, (int)plla.Alt);
                                }

                                RegeneratePolygon();


                            }

                            //routes.Polygons.Add(poly);    

                            if (trackPoints.Count > 0)
                            {
                                PointLatLng currentloc = new PointLatLng(trackPoints[trackPoints.Count - 1].Lat, trackPoints[trackPoints.Count - 1].Lng);


                                routes.Markers.Add(new GMapMarkerGoogleRed(currentloc));

                                if (trackPoints[trackPoints.Count - 1].Lat != 0 && (DateTime.Now.Second % 4 == 0) && CHK_autopan.Checked)
                                {
                                    gMapControl1.Position = currentloc;
                                }

                                if (trackPoints.Count == 1)
                                {
                                    gMapControl1.ZoomAndCenterMarkers("routes");// ZoomAndCenterRoutes("routes");
                                }
                            }


                        }

                        tracklast = DateTime.Now;
                    }
                }
                catch { }
            }

        }

        private void addpolygonmarker(string tag, double lng, double lat, int alt)
        {
            try
            {
                PointLatLng point = new PointLatLng(lat, lng);
                GMapMarkerGoogleGreen m = new GMapMarkerGoogleGreen(point);
                m.ToolTipMode = MarkerTooltipMode.Always;
                m.ToolTipText = tag;
                m.Tag = tag;

                GMapMarkerRect mBorders = new GMapMarkerRect(point);
                {
                    mBorders.InnerMarker = m;
                }

                polygons.Markers.Add(m);
                polygons.Markers.Add(mBorders);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// used to redraw the polygon
        /// </summary>
        void RegeneratePolygon()
        {
            List<PointLatLng> polygonPoints = new List<PointLatLng>();

            if (routes == null)
                return;

            foreach (GMapMarker m in polygons.Markers)
            {
                if (m is GMapMarkerRect)
                {
                    m.Tag = polygonPoints.Count;
                    polygonPoints.Add(m.Position);
                }
            }

            if (polygon == null)
            {
                polygon = new GMapPolygon(polygonPoints, "polygon test");
                polygons.Polygons.Add(polygon);
            }
            else
            {
                polygon.Points.Clear();
                polygon.Points.AddRange(polygonPoints);

                polygon.Stroke = new Pen(Color.Yellow, 4);

                if (polygons.Polygons.Count == 0)
                {
                    polygons.Polygons.Add(polygon);
                }
                else
                {
                    //lock (thisLock)
                    {
                        gMapControl1.UpdatePolygonLocalPosition(polygon);
                    }
                }
            }
        }

        GMapPolygon polygon;
        GMapOverlay polygons;
        GMapOverlay routes;
        GMapRoute route;

        public void CreateChart(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = "Servo Tuning";
            myPane.XAxis.Title.Text = "Time";
            myPane.YAxis.Title.Text = "Angle";

            LineItem myCurve;

            myCurve = myPane.AddCurve("Roll", list1, Color.Red, SymbolType.None);

            myCurve = myPane.AddCurve("Pitch", list2, Color.Blue, SymbolType.None);

            myCurve = myPane.AddCurve("Nav_Roll", list3, Color.Green, SymbolType.None);

            myCurve = myPane.AddCurve("Nav_Pitch", list4, Color.Orange, SymbolType.None);

            // Show the x axis grid
            myPane.XAxis.MajorGrid.IsVisible = true;

            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 5;

            // Make the Y axis scale red
            myPane.YAxis.Scale.FontSpec.FontColor = Color.White;
            myPane.YAxis.Title.FontSpec.FontColor = Color.White;
            // turn off the opposite tics so the Y tics don't show up on the Y2 axis
            myPane.YAxis.MajorTic.IsOpposite = false;
            myPane.YAxis.MinorTic.IsOpposite = false;
            // Don't display the Y zero line
            myPane.YAxis.MajorGrid.IsZeroLine = true;
            // Align the Y axis labels so they are flush to the axis
            myPane.YAxis.Scale.Align = AlignP.Inside;
            // Manually set the axis range
            //myPane.YAxis.Scale.Min = -1;
            //myPane.YAxis.Scale.Max = 1;

            // Fill the axis background with a gradient
            //myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

            // Sample at 50ms intervals
            ZedGraphTimer.Interval = 100;
            //timer1.Enabled = true;
            //timer1.Start();


            // Calculate the Axis Scale Ranges
            zgc.AxisChange();

            tickStart = Environment.TickCount;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Make sure that the curvelist has at least one curve
            if (zg1.GraphPane.CurveList.Count <= 0)
                return;

            // Get the first CurveItem in the graph
            LineItem curve = zg1.GraphPane.CurveList[0] as LineItem;
            if (curve == null)
                return;

            // Get the PointPairList
            IPointListEdit list = curve.Points as IPointListEdit;
            // If this is null, it means the reference at curve.Points does not
            // support IPointListEdit, so we won't be able to modify it
            if (list == null)
                return;

            // Time is measured in seconds
            double time = (Environment.TickCount - tickStart) / 1000.0;

            // Keep the X scale at a rolling 30 second interval, with one
            // major step between the max X value and the end of the axis
            Scale xScale = zg1.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - 10.0;
            }

            // Make sure the Y axis is rescaled to accommodate actual data
            try
            {
                zg1.AxisChange();
            }
            catch { }
            // Force a redraw
            zg1.Invalidate();

        }

        private void GCS_FormClosing(object sender, FormClosingEventArgs e)
        {
            ZedGraphTimer.Stop();
            threadrun = 0;
            if (comPort.IsOpen)
            {
                comPort.Close();
            }
        }

        private void BUT_clear_track_Click(object sender, EventArgs e)
        {
            trackPoints.Clear();
        }

        private void BUT_save_log_Click(object sender, EventArgs e)
        {
            // close existing log first
            if (swlog != null)
                swlog.Close();

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + @"logs");
                swlog = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + @"logs" + Path.DirectorySeparatorChar + DateTime.Now.ToString("dd-MM-yy hh-mm") + " telem.log");
            }
            catch { MessageBox.Show("Log creation error"); }
        }

        private void BUTactiondo_Click(object sender, EventArgs e)
        {
            try
            {
                comPort.doAction((MAVLink.MAV_ACTION)Enum.Parse(typeof(MAVLink.MAV_ACTION), "MAV_ACTION_" + CMB_action.Text));
            }
            catch { MessageBox.Show("The Command failed to execute"); }
        }

        private void BUTrestartmission_Click(object sender, EventArgs e)
        {
            try
            {
                comPort.doAction(MAVLink.MAV_ACTION.MAV_ACTION_RETURN); // set nav from
                System.Threading.Thread.Sleep(100);
                comPort.setWPCurrent(1); // set nav to
                System.Threading.Thread.Sleep(100);
                comPort.doAction(MAVLink.MAV_ACTION.MAV_ACTION_SET_AUTO); // set auto
            }
            catch { MessageBox.Show("The command failed to execute"); }

        }

        private void GCS_Resize(object sender, EventArgs e)
        {
            //Gspeed;
            //Galt;
            //Gheading;
            //attitudeIndicatorInstrumentControl1;
        }

        private void CB_tuning_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_tuning.Checked)
            {
                gMapControl1.Visible = false;
                ZedGraphTimer.Enabled = true;
                ZedGraphTimer.Start();
                zg1.Visible = true;
            }
            else
            {
                gMapControl1.Visible = true;
                ZedGraphTimer.Enabled = false;
                ZedGraphTimer.Stop();
                zg1.Visible = false;
            }
        }

        private void SubMainHT_Panel1_Resize(object sender, EventArgs e)
        {
            hud1.Width = MainH.SplitterDistance;

            SubMainLeft.SplitterDistance = hud1.Height + 2;
        }

        private void BUT_RAWSensor_Click(object sender, EventArgs e)
        {
            Form temp = new AC2_Sensor();
            MainV2.fixtheme(temp);
            temp.ShowDialog();
        }

        private void gMapControl1_Click(object sender, EventArgs e)
        {

        }

        PointLatLng gotolocation = new PointLatLng();

        private void gMapControl1_MouseDown(object sender, MouseEventArgs e)
        {
            gotolocation = gMapControl1.FromLocalToLatLng(e.X, e.Y);
        }

        private void goHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!MainV2.comPort.IsOpen)
            {
                MessageBox.Show("Please Connect First");
                return;
            }

            string alt = (100).ToString();
            Common.InputBox("Enter Alt", "Enter Guided Mode Alt", ref alt);

            int intalt = 100;
            if (!int.TryParse(alt, out intalt))
            {
                MessageBox.Show("Bad Alt");
                return;
            }

            if (gotolocation.Lat == 0 || gotolocation.Lng == 0)
            {
                MessageBox.Show("Bad Lat/Long");
                return;
            }

            Locationwp gotohere = new Locationwp();

            gotohere.alt = intalt * 100;
            gotohere.lat = (int)(gotolocation.Lat * 10000000);
            gotohere.lng = (int)(gotolocation.Lng * 10000000);

            try
            {
                MainV2.givecomport = true;

                MainV2.comPort.setWP(gotohere, 0, MAVLink.MAV_FRAME.MAV_FRAME_GLOBAL_RELATIVE_ALT, (byte)2);

                MainV2.givecomport = false;
            }
            catch (Exception ex) { MainV2.givecomport = false; MessageBox.Show("Error sending command : " + ex.Message); }

        }

        private void Zoomlevel_ValueChanged(object sender, EventArgs e)
        {
            if (gMapControl1.MaxZoom + 1 == (double)Zoomlevel.Value)
            {
                gMapControl1.Zoom = (double)Zoomlevel.Value-.1;
            }
            else
            {
                gMapControl1.Zoom = (double)Zoomlevel.Value;
            }
        }

        private void gMapControl1_MouseMove(object sender, MouseEventArgs e)
        {
            PointLatLng point = gMapControl1.FromLocalToLatLng(e.X, e.Y);

            if (e.Button == MouseButtons.Left)
            {
                double latdif = gotolocation.Lat - point.Lat;
                double lngdif = gotolocation.Lng - point.Lng;

                try
                {
                    gMapControl1.Position = new PointLatLng(gMapControl1.Position.Lat + latdif, gMapControl1.Position.Lng + lngdif);
                }
                catch { }
            }
        }

        private void FlightData_ParentChanged(object sender, EventArgs e)
        {
            if (MainV2.cam != null)
            {
                MainV2.cam.camimage += new WebCamService.CamImage(cam_camimage);
            }
        }

        void cam_camimage(Image camimage, bool showhud)
        {
            hud1.bgimage = camimage;
            hud1.hudon = showhud;
        }

        private void TXT_WP_TextChanged(object sender, EventArgs e)
        {
            if (TXT_control_mode.Text == "RTL")
                TXT_WP.Text = "Home";
            if (TXT_control_mode.Text == "Loiter")
                TXT_WP.Text = "Here";
        }

        private void BUT_Homealt_Click(object sender, EventArgs e)
        {
            MainV2.cs.altoffsethome = MainV2.cs.alt;
        }

        private void gMapControl1_Resize(object sender, EventArgs e)
        {
            gMapControl1.Zoom = gMapControl1.Zoom + 0.01;
        }

        private void BUT_savetelem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.AddExtension = true;
            fd.Filter = "Ardupilot Telemtry log (*.tlog)|*.tlog";
            fd.DefaultExt = ".tlog";
            DialogResult result = fd.ShowDialog();
            string file = fd.FileName;
            if (file != "")
            {
                try
                {
                    BinaryWriter bw = new BinaryWriter(File.Open(file,FileMode.Create));
                    MainV2.comPort.logfile = bw;

                    BUT_savetelem.Enabled = false;
                }
                catch { MessageBox.Show("Error: Failed to write log file"); }
            }

        }

        private void BUT_stoptelemlog_Click(object sender, EventArgs e)
        {
            try
            {
                BUT_savetelem.Enabled = true;

                MainV2.comPort.logfile.Close();
            }
            catch { }
            MainV2.comPort.logfile = null;
        }

        private void BUT_loadtelem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.AddExtension = true;
            fd.Filter = "Ardupilot Telemtry log (*.tlog)|*.tlog";
            fd.DefaultExt = ".tlog";
            DialogResult result = fd.ShowDialog();
            string file = fd.FileName;
            if (file != "")
            {
                try
                {
                    BUT_clear_track_Click(sender, e);

                    MainV2.comPort.logreadmode = false;
                    MainV2.comPort.logplaybackfile = new BinaryReader(File.OpenRead(file));
                    MainV2.comPort.lastlogread = DateTime.MinValue;

                    tracklog.Value = 0;
                    tracklog.Minimum = 0;
                    tracklog.Maximum = 100;
                }
                catch { MessageBox.Show("Error: Failed to write log file"); }
            }
        }

        private void BUT_playlog_Click(object sender, EventArgs e)
        {
            if (MainV2.comPort.logreadmode)
            {
                MainV2.comPort.logreadmode = false;
            }
            else
            {
                BUT_clear_track_Click(sender, e);
                MainV2.comPort.logreadmode = true;
            }
        }

        private void tracklog_Scroll(object sender, EventArgs e)
        {
            BUT_clear_track_Click(sender, e);

            MainV2.comPort.lastlogread = DateTime.MinValue;

            if (MainV2.comPort.logplaybackfile != null)
                MainV2.comPort.logplaybackfile.BaseStream.Position = (long)(MainV2.comPort.logplaybackfile.BaseStream.Length * (tracklog.Value / 100.0));
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void hud1_Resize(object sender, EventArgs e)
        {

        }

        private void MainH_SplitterMoved(object sender, SplitterEventArgs e)
        {
            hud1.Width = MainH.Panel1.Width;
        }

        private void SubMainLeft_Panel2_Resize(object sender, EventArgs e)
        {

        }

        private void tabPage1_Resize(object sender, EventArgs e)
        {
            int mywidth;

            if (tabPage1.Width < 500)
            {
                Gvspeed.Visible = false;
                mywidth = tabPage1.Width / 3;

                Gspeed.Height = mywidth;
                Galt.Height = mywidth;
                Gheading.Height = mywidth;

                Gspeed.Location = new Point(0,0);
            }
            else
            {
                Gvspeed.Visible = true;
                mywidth = tabPage1.Width / 4;

                Gvspeed.Height = mywidth;
                Gspeed.Height = mywidth;
                Galt.Height = mywidth;
                Gheading.Height = mywidth;

                Gvspeed.Location = new Point(0, 0);
                Gspeed.Location = new Point(Gvspeed.Right,0);
            }

            Galt.Location = new Point(Gspeed.Right, 0);
            Gheading.Location = new Point(Galt.Right, 0);

        }
    }
}
