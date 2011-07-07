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
using System.Resources;
using System.Reflection;
using System.ComponentModel;

namespace ArdupilotMega.GCSViews
{
    partial class FlightPlanner : UserControl
    {
        int selectedrow = 0;
        int t7 = 10000000;
        bool quickadd = false;
        bool isonline = true;
        bool sethome = false;
        Hashtable param = new Hashtable();
        public static Hashtable hashdefines = new Hashtable();
        public static List<PointLatLngAlt> pointlist = new List<PointLatLngAlt>(); // used to calc distance
        static public Object thisLock = new Object();
        private TextBox textBox1;
        private ComponentResourceManager rm = new ComponentResourceManager(typeof(FlightPlanner));
        /// <summary>
        /// Reads defines.h for all valid commands and eeprom positions
        /// </summary>
        /// <param name="file">File Path</param>
        /// <returns></returns>
        public bool readdefines(string file)
        {
            if (!File.Exists(file))
            {
                return false;
            }
            try
            {
                StreamReader sr = new StreamReader(file); //"defines.h"
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Regex regex2 = new Regex(@"define\s+([^\s]+)\s+([^\s]+)", RegexOptions.IgnoreCase);
                    if (regex2.IsMatch(line))
                    {
                        MatchCollection matchs = regex2.Matches(line);
                        for (int i = 0; i < matchs.Count; i++)
                        {
                            int num = 0;
                            if (matchs[i].Groups[2].Value.ToString().ToLower().Contains("0x"))
                            {
                                try
                                {
                                    num = Convert.ToInt32(matchs[i].Groups[2].Value.ToString(), 16);
                                }
                                catch (Exception) { System.Diagnostics.Debug.WriteLine("BAD hex " + matchs[i].Groups[1].Value.ToString()); }
                            }
                            else
                            {
                                try
                                {
                                    num = Convert.ToInt32(matchs[i].Groups[2].Value.ToString(), 10);
                                }
                                catch (Exception) { System.Diagnostics.Debug.WriteLine("BAD dec " + matchs[i].Groups[1].Value.ToString()); }
                            }
                            System.Diagnostics.Debug.WriteLine(matchs[i].Groups[1].Value.ToString() + " = " + matchs[i].Groups[2].Value.ToString() + " = " + num.ToString());
                            try
                            {
                                hashdefines.Add(matchs[i].Groups[1].Value.ToString(), num);
                            }
                            catch (Exception) { }
                        }
                    }
                }

                sr.Close();


                if (!hashdefines.ContainsKey("WP_START_BYTE"))
                {
                    MessageBox.Show("Your Ardupilot Mega project defines.h is Invalid");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't open file!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Read from waypoint writter *.h file
        /// </summary>
        /// <param name="file">File Path</param>
        /// <returns></returns>
        bool readwaypointwritterfile(string file)
        {

            byte wp_rad = 30;
            byte loit_rad = 45;
            int alt_hold = 100;
            byte wp_count = 0;
            bool error = false;
            List<Locationwp> cmds = new List<Locationwp>();

            cmds.Add(new Locationwp());

            try
            {
                StreamReader sr = new StreamReader(file); //"defines.h"
                while (!error && !sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    // defines
                    Regex regex2 = new Regex(@"define\s+([^\s]+)\s+([^\s]+)", RegexOptions.IgnoreCase);
                    if (regex2.IsMatch(line))
                    {
                        MatchCollection matchs = regex2.Matches(line);
                        for (int i = 0; i < matchs.Count; i++)
                        {
                            if (matchs[i].Groups[1].Value.ToString().Equals("WP_RADIUS"))
                                wp_rad = Convert.ToByte(matchs[i].Groups[2].Value.ToString(), 10);
                            if (matchs[i].Groups[1].Value.ToString().Equals("LOITER_RADIUS"))
                                loit_rad = Convert.ToByte(matchs[i].Groups[2].Value.ToString(), 10);
                            if (matchs[i].Groups[1].Value.ToString().Equals("ALT_TO_HOLD"))
                                alt_hold = Convert.ToInt32(matchs[i].Groups[2].Value.ToString(), 10);
                        }
                    }
                    // waypoints
                    regex2 = new Regex(@"([^,{]+),([^,]+),([^,]+),([^,]+),([^,}]+)", RegexOptions.IgnoreCase);
                    if (regex2.IsMatch(line))
                    {
                        MatchCollection matchs = regex2.Matches(line);
                        for (int i = 0; i < matchs.Count; i++)
                        {
                            Locationwp temp = new Locationwp();
                            temp.id = (byte)(int)Enum.Parse(typeof(MAVLink.MAV_CMD), matchs[i].Groups[1].Value.ToString().Replace("NAV_", ""), false);
                            temp.p1 = byte.Parse(matchs[i].Groups[2].Value.ToString());

                            if (temp.id < (byte)MAVLink.MAV_CMD.LAST)
                            {
                                temp.alt = (int)(double.Parse(matchs[i].Groups[3].Value.ToString(), new System.Globalization.CultureInfo("en-US"))* 100);
                                temp.lat = (int)(double.Parse(matchs[i].Groups[4].Value.ToString(), new System.Globalization.CultureInfo("en-US"))* 10000000);
                                temp.lng = (int)(double.Parse(matchs[i].Groups[5].Value.ToString(), new System.Globalization.CultureInfo("en-US"))* 10000000);
                            }
                            else
                            {
                                temp.alt = (int)(double.Parse(matchs[i].Groups[3].Value.ToString(), new System.Globalization.CultureInfo("en-US")));
                                temp.lat = (int)(double.Parse(matchs[i].Groups[4].Value.ToString(), new System.Globalization.CultureInfo("en-US")));
                                temp.lng = (int)(double.Parse(matchs[i].Groups[5].Value.ToString(), new System.Globalization.CultureInfo("en-US")));
                            }
                            cmds.Add(temp);

                            wp_count++;
                            if (wp_count == byte.MaxValue)
                                break;
                        }
                        if (wp_count == byte.MaxValue)
                        {
                            MessageBox.Show("To many Waypoints!!!");
                            break;
                        }
                    }
                }

                sr.Close();

                TXT_DefaultAlt.Text = (alt_hold).ToString();
                TXT_WPRad.Text = (wp_rad).ToString();
                TXT_loiterrad.Text = (loit_rad).ToString();

                processToScreen(cmds);

                writeKML();

                MainMap.ZoomAndCenterMarkers("objects");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't open file! " + ex.ToString());
                return false;
            }
            return true;

        }

        /// <summary>
        /// used to adjust existing point in the datagrid including "Home"
        /// </summary>
        /// <param name="pointno"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="alt"></param>
        public void callMeDrag(string pointno, double lat, double lng, int alt)
        {
            if (pointno == "")
            {
                return;
            }

            // dragging a WP
            if (pointno == "Home")
            {
                if (isonline && CHK_geheight.Checked)
                {
                    TXT_homealt.Text = getGEAlt(lat, lng).ToString();
                }
                else
                {
                    // no change
                    //TXT_homealt.Text = alt.ToString();
                }
                TXT_homelat.Text = lat.ToString();
                TXT_homelng.Text = lng.ToString();
                return;
            }

            try
            {
                selectedrow = int.Parse(pointno) - 1;
                Commands.CurrentCell = Commands[1, selectedrow];
            }
            catch
            {
                return;
            }

            setfromGE(lat, lng, alt);
        }
        /// <summary>
        /// Actualy Sets the values into the datagrid and verifys height if turned on
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="alt"></param>
        void setfromGE(double lat, double lng, int alt)
        {
            if (selectedrow > Commands.RowCount)
            {
                MessageBox.Show("Invalid coord, How did you do this?");
                return;
            }
            DataGridViewTextBoxCell cell;
            if (Commands.Columns[Param3.Index].HeaderText.Equals("Lat"))
            {
                cell = Commands.Rows[selectedrow].Cells[3] as DataGridViewTextBoxCell;
                cell.Value = lat.ToString("0.0000000");
                cell.DataGridView.EndEdit();
            }
            if (Commands.Columns[Param4.Index].HeaderText.Equals("Long"))
            {
                cell = Commands.Rows[selectedrow].Cells[4] as DataGridViewTextBoxCell;
                cell.Value = lng.ToString("0.0000000");
                cell.DataGridView.EndEdit();
            }
            if (Commands.Columns[Param1.Index].HeaderText.Equals("Delay"))
            {
                cell = Commands.Rows[selectedrow].Cells[1] as DataGridViewTextBoxCell;
                cell.Value = 0;
            }
            if (Commands.Columns[Param2.Index].HeaderText.Equals("Alt"))
            {
                cell = Commands.Rows[selectedrow].Cells[2] as DataGridViewTextBoxCell;

                cell.Value = TXT_DefaultAlt.Text;

                float result;
                float.TryParse(TXT_homealt.Text, out result);

                if (result == 0)
                {
                    MessageBox.Show("You must have a home altitude");
                }

                int ans;
                if (float.TryParse(TXT_homealt.Text, out result) && int.TryParse(cell.Value.ToString(), out ans))
                {
                    // is  absolute             online          verify height
                    if (CHK_altmode.Checked && isonline && CHK_geheight.Checked)
                    {
                        cell.Value = ((int)getGEAlt(lat, lng) + int.Parse(TXT_DefaultAlt.Text)).ToString();
                    }
                    else
                    {
                        // is absolute but no verify
                        if (CHK_altmode.Checked)
                        {
                            cell.Value = (float.Parse(TXT_homealt.Text) + int.Parse(TXT_DefaultAlt.Text)).ToString();
                        } // is relative and check height
                        else if (float.TryParse(TXT_homealt.Text, out result) && isonline && CHK_geheight.Checked)
                        {
                            alt = (int)getGEAlt(lat, lng);

                            if (float.Parse(TXT_homealt.Text) + int.Parse(TXT_DefaultAlt.Text) < alt) // calced height is less that GE ground height
                            {
                                MessageBox.Show("Altitude appears to be low!! (you will fly into a hill)\nGoogle Ground height: " + alt + " Meters\nYour height: " + ((float.Parse(TXT_homealt.Text) + int.Parse(TXT_DefaultAlt.Text))) + " Meters");
                                cell.Style.BackColor = Color.Red;
                            }
                            else
                            {
                                cell.Style.BackColor = Color.LightGreen;
                            }
                        }

                    }
                    cell.DataGridView.EndEdit();
                }
                else
                {
                    MessageBox.Show("Invalid Home or wp Alt");
                    cell.Style.BackColor = Color.Red;
                }

            }
            writeKML();
            Commands.EndEdit();
        }
        /// <summary>
        /// Used for current mouse position
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="alt"></param>
        public void callMeDisplay(double lat, double lng, int alt)
        {
            TXT_mouselat.Text = lat.ToString();
            TXT_mouselong.Text = lng.ToString();
            TXT_mousealt.Text = alt.ToString();

            try
            {
                double lastdist = MainMap.Manager.GetDistance(polygon.Points[polygon.Points.Count - 1], currentMarker.Position);

                lbl_prevdist.Text = (lastdist * 1000 * MainV2.cs.multiplierdist).ToString(rm.GetString("lbl_prevdist.Text") + ": 0.00");

                double homedist = MainMap.Manager.GetDistance(currentMarker.Position, polygon.Points[0]);

                lbl_homedist.Text = (homedist * 1000 * MainV2.cs.multiplierdist).ToString(rm.GetString("lbl_homedist.Text") + ": 0.00");
            }
            catch { }
        }
        
        /// <summary>
        /// Used to create a new WP
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="alt"></param>
        public void callMe(double lat, double lng, int alt)
        {

            if (sethome)
            {
                sethome = false;
                callMeDrag("Home", lat, lng, alt);
                try
                {
                    config(true);
                }
                catch { }
                return;
            }
            try
            {
                config(true);
            }
            catch { }
            // creating a WP

                Commands.Rows.Add();
                selectedrow = Commands.RowCount - 1;
                Commands.CurrentCell = Commands.Rows[selectedrow].Cells[Param1.Index];

            setfromGE(lat, lng, alt);
        }

        public FlightPlanner()
        {
            //Form frm = Main.LoadingBox("Loading", "Loading ... Cache");

            InitializeComponent();

            //frm.Close();



            this.Text = "Planner MAVLINK";

            // config map             
            MainMap.MapType = MapType.GoogleSatellite;
            MainMap.MinZoom = 1;
            MainMap.CacheLocation = Path.GetDirectoryName(Application.ExecutablePath) + "/gmapcache/";

            //MainMap.Manager.ImageCacheLocal.PutImageToCache(,MapType.None,new GPoint(),17);

            // map events
            MainMap.OnCurrentPositionChanged += new CurrentPositionChanged(MainMap_OnCurrentPositionChanged);
            MainMap.OnTileLoadStart += new TileLoadStart(MainMap_OnTileLoadStart);
            MainMap.OnTileLoadComplete += new TileLoadComplete(MainMap_OnTileLoadComplete);
            MainMap.OnMarkerClick += new MarkerClick(MainMap_OnMarkerClick);
            MainMap.OnMapZoomChanged += new MapZoomChanged(MainMap_OnMapZoomChanged);
            MainMap.OnMapTypeChanged += new MapTypeChanged(MainMap_OnMapTypeChanged);
            MainMap.MouseMove += new MouseEventHandler(MainMap_MouseMove);
            MainMap.MouseDown += new MouseEventHandler(MainMap_MouseDown);
            MainMap.MouseUp += new MouseEventHandler(MainMap_MouseUp);
            MainMap.OnMarkerEnter += new MarkerEnter(MainMap_OnMarkerEnter);
            MainMap.OnMarkerLeave += new MarkerLeave(MainMap_OnMarkerLeave);

            MainMap.MapScaleInfoEnabled = false;
            MainMap.ScalePen = new Pen(Color.Red);

            MainMap.ForceDoubleBuffer = false;

            WebRequest.DefaultWebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            // get map type
            comboBoxMapType.DataSource = Enum.GetValues(typeof(MapType));
            comboBoxMapType.SelectedItem = MainMap.MapType;

            comboBoxMapType.SelectedValueChanged += new System.EventHandler(this.comboBoxMapType_SelectedValueChanged);
            /*
            // acccess mode
            comboBoxMode.DataSource = Enum.GetValues(typeof(AccessMode));
            comboBoxMode.SelectedItem = GMaps.Instance.Mode;

            // get position
            textBoxLat.Text = MainMap.Position.Lat.ToString(CultureInfo.InvariantCulture);
            textBoxLng.Text = MainMap.Position.Lng.ToString(CultureInfo.InvariantCulture);
             */

            MainMap.RoutesEnabled = true;

            MainMap.MaxZoom = 18;

            // get zoom  
            trackBar1.Minimum = MainMap.MinZoom;
            trackBar1.Maximum = MainMap.MaxZoom + 0.99;

            routes = new GMapOverlay(MainMap, "routes");
            MainMap.Overlays.Add(routes);

            polygons = new GMapOverlay(MainMap, "polygons");
            MainMap.Overlays.Add(polygons);

            objects = new GMapOverlay(MainMap, "objects");
            MainMap.Overlays.Add(objects);

            top = new GMapOverlay(MainMap, "top");
            //MainMap.Overlays.Add(top);

            objects.Markers.Clear();

            // set current marker
            currentMarker = new GMapMarkerGoogleRed(MainMap.Position);
            //top.Markers.Add(currentMarker);

            // map center
            center = new GMapMarkerCross(MainMap.Position);
            //top.Markers.Add(center);

            MainMap.Zoom = 3;

            //set home
            try
            {
                MainMap.Position = new PointLatLng(double.Parse(TXT_homelat.Text), double.Parse(TXT_homelng.Text));
                MainMap.Zoom = 13;

            }
            catch (Exception) { }

            RegeneratePolygon();

            //Commands.DataError += new DataGridViewDataErrorEventHandler(Commands_DataError);

            List<string> list = new List<string>();

            foreach (object obj in Enum.GetValues(typeof(MAVLink.MAV_CMD)))
            {
                if (obj.ToString().EndsWith("LAST") || obj.ToString().EndsWith("END"))
                    continue;
                list.Add(obj.ToString());
            }

            Command.DataSource = list;

            Up.Image = global::ArdupilotMega.Properties.Resources.up;
            Down.Image = global::ArdupilotMega.Properties.Resources.down;

            hashdefines.Clear();
            if (File.Exists(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + "defines.h"))
            {
                readdefines(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + "defines.h");
            }
        }

        void Commands_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine(e.Exception.ToString() + " " + e.Context + " col " + e.ColumnIndex);
            e.Cancel = false;
            e.ThrowException = false;
            //throw new NotImplementedException();
        }
        /// <summary>
        /// Adds a new row to the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BUT_Add_Click(object sender, EventArgs e)
        {
            if (Commands.CurrentRow == null)
            {
                selectedrow = 0;
            }
            else
            {
                selectedrow = Commands.CurrentRow.Index;
            }

            if (Commands.RowCount <= 1)
            {
                Commands.Rows.Add();
            }
            else
            {
                if (Commands.RowCount == selectedrow + 1)
                {
                    DataGridViewRow temp = Commands.Rows[selectedrow];
                    Commands.Rows.Add();
                }
                else
                {
                    Commands.Rows.Insert(selectedrow + 1, 1);
                }
            }
            writeKML();
        }

        private void Planner_Load(object sender, EventArgs e)
        {
            config(false);

            trackBar1.Value = (int)MainMap.Zoom;

            // check for net and set offline if needed
            try
            {
                IPAddress[] addresslist = Dns.GetHostAddresses("www.google.com");
            }
            catch (Exception)
            { // here if dns failed
                isonline = false;
            }

            writeKML();
        }
        /// <summary>
        /// Used to update column headers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Commands_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (quickadd)
                return;
            try
            {
                selectedrow = e.RowIndex;
                string option = Commands[Command.Index, selectedrow].EditedFormattedValue.ToString();
                switch (option)
                {
                    case "WAYPOINT":
                        if (MainV2.APMFirmware == MainV2.Firmwares.ArduPilotMega)
                        {
                            Commands.Columns[1].HeaderText = "N/A";
                        }
                        else
                        {
                            Commands.Columns[1].HeaderText = "Delay";
                        }
                        Commands.Columns[2].HeaderText = "Alt";
                        Commands.Columns[3].HeaderText = "Lat";
                        Commands.Columns[4].HeaderText = "Long";
                        break;
                    case "LOITER_UNLIM":
                        Commands.Columns[1].HeaderText = "N/A";
                        Commands.Columns[2].HeaderText = "Alt";
                        Commands.Columns[3].HeaderText = "Lat";
                        Commands.Columns[4].HeaderText = "Long";
                        break;
                    case "LOITER_TURNS":
                        Commands.Columns[1].HeaderText = "Turns";
                        Commands.Columns[2].HeaderText = "Alt";
                        Commands.Columns[3].HeaderText = "Lat";
                        Commands.Columns[4].HeaderText = "Long";
                        break;
                    case "LOITER_TIME":
                        Commands.Columns[1].HeaderText = "Time s*10";
                        Commands.Columns[2].HeaderText = "Alt";
                        Commands.Columns[3].HeaderText = "Lat";
                        Commands.Columns[4].HeaderText = "Long";
                        break;
                    case "RETURN_TO_LAUNCH":
                        Commands.Columns[1].HeaderText = "N/A";
                        Commands.Columns[2].HeaderText = "Alt";
                        Commands.Columns[3].HeaderText = "Lat";
                        Commands.Columns[4].HeaderText = "Long";
                        break;
                    case "LAND":
                        Commands.Columns[1].HeaderText = "N/A";
                        if (MainV2.APMFirmware == MainV2.Firmwares.ArduPilotMega)
                        {
                            Commands.Columns[2].HeaderText = "Alt";
                        }
                        else
                        {
                            Commands.Columns[2].HeaderText = "N/A";
                        }
                        Commands.Columns[3].HeaderText = "Lat";
                        Commands.Columns[4].HeaderText = "Long";
                        break;
                    case "TAKEOFF":
                        if (MainV2.APMFirmware == MainV2.Firmwares.ArduPilotMega)
                        {
                            Commands.Columns[1].HeaderText = "Angle";
                        }
                        else
                        {
                            Commands.Columns[1].HeaderText = "N/A";
                        }
                        Commands.Columns[2].HeaderText = "Alt";
                        Commands.Columns[3].HeaderText = "N/A";
                        Commands.Columns[4].HeaderText = "N/A";
                        break;
                    case "CONDITION_DELAY":
                        Commands.Columns[1].HeaderText = "N/A";
                        Commands.Columns[2].HeaderText = "N/A";
                        Commands.Columns[3].HeaderText = "Time (sec)";
                        Commands.Columns[4].HeaderText = "N/A";
                        break;
                    case "CONDITION_CHANGE_ALT":
                        Commands.Columns[1].HeaderText = "Rate (cm/sec)";
                        Commands.Columns[2].HeaderText = "Alt";
                        Commands.Columns[3].HeaderText = "N/A";
                        Commands.Columns[4].HeaderText = "N/A";
                        break;
                    case "CONDITION_DISTANCE":
                        Commands.Columns[1].HeaderText = "N/A";
                        Commands.Columns[2].HeaderText = "N/A";
                        Commands.Columns[3].HeaderText = "Dist (m)";
                        Commands.Columns[4].HeaderText = "N/A";
                        break;
                    case "CONDITION_YAW":
                        Commands.Columns[1].HeaderText = "Angle";
                        Commands.Columns[2].HeaderText = "Speed (deg/sec)";
                        Commands.Columns[3].HeaderText = "Direction (1,-1)";
                        Commands.Columns[4].HeaderText = "Relative(1)/Absolute(0)";
                        break;
                    case "DO_JUMP":
                        Commands.Columns[1].HeaderText = "WP #";
                        Commands.Columns[2].HeaderText = "N/A";
                        Commands.Columns[3].HeaderText = "Repeat Count";
                        Commands.Columns[4].HeaderText = "N/A";
                        break;
                    case "DO_CHANGE_SPEED":
                        Commands.Columns[1].HeaderText = "Type (0=as 1=gs)";
                        Commands.Columns[2].HeaderText = "Speed (m/s)";
                        Commands.Columns[3].HeaderText = "Throttle (%)";
                        Commands.Columns[4].HeaderText = "N/A";
                        break;
                    case "DO_SET_HOME":
                        Commands.Columns[1].HeaderText = "Current (1) Spec (0)";
                        Commands.Columns[2].HeaderText = "Alt (m)";
                        Commands.Columns[3].HeaderText = "Lat";
                        Commands.Columns[4].HeaderText = "Long";
                        break;
                    case "DO_SET_PARAMETER":
                        Commands.Columns[1].HeaderText = "Param Number";
                        Commands.Columns[2].HeaderText = "Param Value";
                        Commands.Columns[3].HeaderText = "N/A";
                        Commands.Columns[4].HeaderText = "N/A";
                        break;
                    case "DO_REPEAT_RELAY":
                        Commands.Columns[1].HeaderText = "N/A";
                        Commands.Columns[2].HeaderText = "Repeat#";
                        Commands.Columns[3].HeaderText = "Delay (sec)";
                        Commands.Columns[4].HeaderText = "N/A";
                        break;
                    case "DO_SET_RELAY":
                        Commands.Columns[1].HeaderText = "Off(0)/On(1)";
                        Commands.Columns[2].HeaderText = "N/A";
                        Commands.Columns[3].HeaderText = "N/A";
                        Commands.Columns[4].HeaderText = "N/A";
                        break;
                    case "DO_SET_SERVO":
                        Commands.Columns[1].HeaderText = "Servo No";
                        Commands.Columns[2].HeaderText = "PWM";
                        Commands.Columns[3].HeaderText = "N/A";
                        Commands.Columns[4].HeaderText = "N/A";
                        break;
                    case "DO_REPEAT_SERVO":
                        Commands.Columns[1].HeaderText = "Servo No";
                        Commands.Columns[2].HeaderText = "PWM";
                        Commands.Columns[3].HeaderText = "Repeat#";
                        Commands.Columns[4].HeaderText = "Delay (sec)";
                        break;
                    default:
                        Commands.Columns[1].HeaderText = "Setme";
                        Commands.Columns[2].HeaderText = "Setme";
                        Commands.Columns[3].HeaderText = "Setme";
                        Commands.Columns[4].HeaderText = "Setme";
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void Commands_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridViewComboBoxCell cell = Commands.Rows[e.RowIndex].Cells[Command.Index] as DataGridViewComboBoxCell;
            if (cell.Value == null)
            {
                cell.Value = "WAYPOINT";
                Commands.Rows[e.RowIndex].Cells[Delete.Index].Value = "X";
                if (!quickadd)
                {
                    Commands_RowEnter(sender, new DataGridViewCellEventArgs(0, e.RowIndex - 0)); // do header labels
                    Commands_RowValidating(sender, new DataGridViewCellCancelEventArgs(0, e.RowIndex)); // do default values
                }
            }

            if (quickadd)
                return;

            Commands.CurrentCell = Commands.Rows[e.RowIndex].Cells[0];
            try
            {
                if (Commands.Rows[e.RowIndex - 1].Cells[Command.Index].Value.ToString() == "WAYPOINT")
                {
                    Commands.Rows[e.RowIndex].Selected = true; // highlight row
                }
                else
                {
                    Commands.CurrentCell = Commands[1, e.RowIndex - 1];
                    //Commands_RowEnter(sender, new DataGridViewCellEventArgs(0, e.RowIndex-1));
                }
            }
            catch (Exception) { }
            // Commands.EndEdit();
        }
        private void Commands_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            selectedrow = e.RowIndex;
            int cols = Commands.Columns.Count;
            for (int a = 1; a < cols; a++)
            {
                DataGridViewTextBoxCell cell;
                cell = Commands.Rows[selectedrow].Cells[a] as DataGridViewTextBoxCell;
                if (Commands.Columns[a].HeaderText.Equals("N/A"))
                {
                    cell.Value = "0";
                }
                else
                {
                    if (cell != null && (cell.Value == null || cell.Value.ToString() == ""))
                    {
                        cell.Value = "I need Data";
                    }
                    else
                    {
                    }
                }
            }

            DataGridViewTextBoxCell cell1;
            cell1 = Commands.Rows[selectedrow].Cells[1] as DataGridViewTextBoxCell;

            byte res;
            if (byte.TryParse(cell1.Value.ToString(), out res))
            {

            }
            else
            {
                try
                {
                    cell1.Value = (byte)(int)hashdefines[cell1.Value.ToString().ToUpper().Trim()];
                }
                catch { }
            }

        }
        /// <summary>
        /// copy of ardupilot code for getting distance between WP's
        /// </summary>
        /// <param name="loc1"></param>
        /// <param name="loc2"></param>
        /// <returns></returns>
        double getDistance(Locationwp loc1, Locationwp loc2)
        {
            if (loc1.lat == 0 || loc1.lng == 0)
                return -1;
            if (loc2.lat == 0 || loc2.lng == 0)
                return -1;

            // this is used to offset the shrinking longitude as we go towards the poles	
            double rads = (double)((Math.Abs(loc2.lat) / t7) * 0.0174532925);
            //377,173,810 / 10,000,000 = 37.717381 * 0.0174532925 = 0.658292482926943		
            double scaleLongDown = Math.Cos(rads);
            double scaleLongUp = 1.0f / Math.Cos(rads);


            float dlat = (float)(loc2.lat - loc1.lat);
            float dlong = (float)(((float)(loc2.lng - loc1.lng)) * scaleLongDown);
            return Math.Sqrt(Math.Pow(dlat, 2) + Math.Pow(dlong, 2)) * .01113195;
        }

        /// <summary>
        /// used to add a marker to the map display
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <param name="alt"></param>
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

                objects.Markers.Add(m);
                objects.Markers.Add(mBorders);
            }
            catch (Exception) { }
        }
        /// <summary>
        /// used to write a KML, update the Map view polygon, and update the row headers
        /// </summary>
        private void writeKML()
        {
            if (quickadd)
                return;

            pointlist = new List<PointLatLngAlt>();

            System.Diagnostics.Debug.WriteLine(DateTime.Now);
            try
            {
                if (objects != null) // hasnt been created yet
                {
                    objects.Markers.Clear();
                }

                string home;
                if (TXT_homealt.Text != "" && TXT_homelat.Text != "" && TXT_homelng.Text != "")
                {
                    home = string.Format("{0},{1},{2}\r\n", TXT_homelng.Text, TXT_homelat.Text, TXT_DefaultAlt.Text);
                    if (objects != null) // during startup
                    {
                        pointlist.Add(new PointLatLngAlt(double.Parse(TXT_homelat.Text), double.Parse(TXT_homelng.Text), (int)double.Parse(TXT_homealt.Text), "Home"));
                        addpolygonmarker("Home", double.Parse(TXT_homelng.Text), double.Parse(TXT_homelat.Text), 0);
                    }
                }
                else
                {
                    home = "";
                }

                double avglat = 0;
                double avglong = 0;
                double maxlat = -180;
                double maxlong = -180;
                double minlat = 180;
                double minlong = 180;
                double homealt = 0;
                try
                {
                    homealt = (int)double.Parse(TXT_homealt.Text);
                }
                catch { }
                if (CHK_altmode.Checked)
                {
                    homealt = 0; // for absolute we dont need to add homealt
                }

                int usable = 0;

                System.Threading.Thread t1 = new System.Threading.Thread(delegate()
                {
                    // thread for updateing row numbers
                    for (int a = 0; a < Commands.Rows.Count - 0; a++)
                    {
                        try
                        {
                            if (Commands.Rows[a].HeaderCell.Value == null)
                            {
                                if (ArdupilotMega.MainV2.MAC)
                                {
                                    Commands.Rows[a].HeaderCell.Value = "    " + (a + 1).ToString(); // mac doesnt auto center header text
                                }
                                else
                                {
                                    Commands.Rows[a].HeaderCell.Value = (a + 1).ToString();
                                }
                            }
                            // skip rows with the correct number
                            string rowno = Commands.Rows[a].HeaderCell.Value.ToString();
                            if (!rowno.Equals((a + 1).ToString()))
                            {
                                // this code is where the delay is when deleting.
                                Commands.Rows[a].HeaderCell.Value = (a + 1).ToString();
                            }
                        }
                        catch (Exception) { }
                    }
                });
                t1.Name = "Row number updater";
                t1.IsBackground = true;
                t1.Start();
                MainV2.threads.Add(t1);

                long temp = System.Diagnostics.Stopwatch.GetTimestamp();

                string lookat = "";
                for (int a = 0; a < Commands.Rows.Count - 0; a++)
                {
                    try
                    {
                        int command = (byte)(int)Enum.Parse(typeof(MAVLink.MAV_CMD), Commands.Rows[a].Cells[Command.Index].Value.ToString(), false);
                        if (command < (byte)MAVLink.MAV_CMD.LAST && command != (byte)MAVLink.MAV_CMD.TAKEOFF)
                        {
                            string cell2 = Commands.Rows[a].Cells[Param2.Index].Value.ToString(); // alt
                            string cell3 = Commands.Rows[a].Cells[Param3.Index].Value.ToString(); // lat
                            string cell4 = Commands.Rows[a].Cells[Param4.Index].Value.ToString(); // lng

                            if (cell4 == "0" || cell3 == "0")
                                continue;
                            if (cell4 == "I need Data" || cell3 == "I need Data")
                                continue;

                            pointlist.Add(new PointLatLngAlt(double.Parse(cell3), double.Parse(cell4), (int)double.Parse(cell2) + homealt, (a + 1).ToString()));
                            addpolygonmarker((a + 1).ToString(), double.Parse(cell4), double.Parse(cell3), (int)double.Parse(cell2));

                            avglong += double.Parse(Commands.Rows[a].Cells[Param4.Index].Value.ToString());
                            avglat += double.Parse(Commands.Rows[a].Cells[Param3.Index].Value.ToString());
                            usable++;

                            maxlong = Math.Max(double.Parse(Commands.Rows[a].Cells[Param4.Index].Value.ToString()), maxlong);
                            maxlat = Math.Max(double.Parse(Commands.Rows[a].Cells[Param3.Index].Value.ToString()), maxlat);
                            minlong = Math.Min(double.Parse(Commands.Rows[a].Cells[Param4.Index].Value.ToString()), minlong);
                            minlat = Math.Min(double.Parse(Commands.Rows[a].Cells[Param3.Index].Value.ToString()), minlat);

                            System.Diagnostics.Debug.WriteLine(temp - System.Diagnostics.Stopwatch.GetTimestamp());
                        }
                    }
                    catch (Exception e) { Console.WriteLine("writekml - bad wp data " + e.ToString()); }
                }

                if (usable > 0)
                {
                    avglat = avglat / usable;
                    avglong = avglong / usable;
                    double latdiff = maxlat - minlat;
                    double longdiff = maxlong - minlong;
                    float range = 4000;

                    Locationwp loc1 = new Locationwp();
                    loc1.lat = (int)(minlat * t7);
                    loc1.lng = (int)(minlong * t7);
                    Locationwp loc2 = new Locationwp();
                    loc2.lat = (int)(maxlat * t7);
                    loc2.lng = (int)(maxlong * t7);

                    //double distance = getDistance(loc1, loc2);  // same code as ardupilot
                    double distance = 2000;

                    if (usable > 1)
                    {
                        range = (float)(distance * 2);
                    }
                    else
                    {
                        range = 4000;
                    }

                    if (avglong != 0 && usable < 4)
                    {
                        // no autozoom
                        lookat = "<LookAt>     <longitude>" + (minlong + longdiff / 2).ToString(new System.Globalization.CultureInfo("en-US")) + "</longitude>     <latitude>" + (minlat + latdiff / 2).ToString(new System.Globalization.CultureInfo("en-US")) + "</latitude> <range>" + range + "</range> </LookAt>";
                        MainMap.ZoomAndCenterMarkers("objects");
                        MainMap_OnMapZoomChanged();
                    }
                }
                else if (home.Length > 5 && usable == 0)
                {
                    lookat = "<LookAt>     <longitude>" + TXT_homelng.Text.ToString(new System.Globalization.CultureInfo("en-US")) + "</longitude>     <latitude>" + TXT_homelat.Text.ToString(new System.Globalization.CultureInfo("en-US")) + "</latitude> <range>4000</range> </LookAt>";
                    MainMap.ZoomAndCenterMarkers("objects");
                    MainMap_OnMapZoomChanged();
                }

                RegeneratePolygon();

                if (polygon != null && polygon.Points.Count > 0)
                {
                    double homedist = 0;

                    if (home.Length > 5)
                    {
                        pointlist.Add(new PointLatLngAlt(double.Parse(TXT_homelat.Text), double.Parse(TXT_homelng.Text), (int)double.Parse(TXT_homealt.Text), "Home"));

                        homedist = MainMap.Manager.GetDistance(polygon.Points[polygon.Points.Count - 1], polygon.Points[0]);

                        lbl_homedist.Text = (homedist * MainV2.cs.multiplierdist).ToString(rm.GetString("lbl_homedist.Text") + ": 0.00");
                    }
                    lbl_distance.Text = ((polygon.Distance + homedist)).ToString(rm.GetString("lbl_distance.Text") + ": 0.0000 km");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            System.Diagnostics.Debug.WriteLine(DateTime.Now);
        }
        /// <summary>
        /// Saves a waypoint writer file
        /// </summary>
        private void savewaypoints()
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "Ardupilot Mission (*.h)|*.*";
            fd.DefaultExt = ".h";
            DialogResult result = fd.ShowDialog();
            string file = fd.FileName;
            if (file != "")
            {
                try
                {
                    StreamWriter sw = new StreamWriter(file);
                    sw.WriteLine("#define WP_RADIUS " + TXT_WPRad.Text.ToString() + " // What is the minimum distance to reach a waypoint?");
                    sw.WriteLine("#define LOITER_RADIUS " + TXT_loiterrad.Text.ToString() + " 	// How close to Loiter?");
                    sw.WriteLine("#define HOLD_CURRENT_ALT 0	// 1 = hold the current altitude, 0 = use the defined altitude to for RTL");
                    sw.WriteLine("#define ALT_TO_HOLD " + TXT_DefaultAlt.Text);
                    sw.WriteLine("");
                    sw.WriteLine("float mission[][5] = {");
                    for (int a = 0; a < Commands.Rows.Count - 0; a++)
                    {
                        sw.Write("{" + Commands.Rows[a].Cells[0].Value.ToString() + ",");
                        sw.Write(Commands.Rows[a].Cells[1].Value.ToString() + ",");
                        sw.Write(double.Parse(Commands.Rows[a].Cells[2].Value.ToString()).ToString(new System.Globalization.CultureInfo("en-US")) + ",");
                        sw.Write(double.Parse(Commands.Rows[a].Cells[3].Value.ToString()).ToString(new System.Globalization.CultureInfo("en-US")) + ",");
                        sw.Write(double.Parse(Commands.Rows[a].Cells[4].Value.ToString()).ToString(new System.Globalization.CultureInfo("en-US")) + "}");
                        //if (a < Commands.Rows.Count - 2)
                        //{
                        sw.Write(",");
                        //}
                        sw.WriteLine("");
                    }
                    sw.WriteLine("};");
                    sw.Close();
                }
                catch (Exception) { MessageBox.Show("Error writing file"); }
            }
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            savewaypoints();
            writeKML();
        }

        /// <summary>
        /// Reads the EEPROM from a com port
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BUT_read_Click(object sender, EventArgs e)
        {

            List<Locationwp> cmds = new List<Locationwp>();
            int error = 0;

            System.Threading.Thread t12 = new System.Threading.Thread(delegate()
            {
                try
                {
                    MAVLink port = MainV2.comPort;

                    if (!port.IsOpen)
                    {
                        throw new Exception("Please Connect First!");
                    }

                    MainV2.givecomport = true;

                    param = port.param;

                    Console.WriteLine("Getting WP #");
                    int cmdcount = port.getWPCount();

                    for (ushort a = 0; a < cmdcount; a++)
                    {
                        Console.WriteLine("Getting WP" + a);
                        cmds.Add(port.getWP(a));
                    }

                    Console.WriteLine("Done");
                }
                catch (Exception ex) { error = 1; MessageBox.Show("Error : " + ex.ToString()); }
                try
                {
                    this.BeginInvoke((System.Threading.ThreadStart)delegate()
                {
                    if (error == 0)
                    {
                        try
                        {
                            processToScreen(cmds);
                        }
                        catch (Exception exx) { Console.WriteLine(exx.ToString()); }
                    }

                    MainV2.givecomport = false;

                    BUT_read.Enabled = true;

                    writeKML();

                });
                }
                catch { }
            });
            t12.IsBackground = true;
            t12.Name = "Read wps";
            t12.Start();
            MainV2.threads.Add(t12);

            BUT_read.Enabled = false;
        }

        /// <summary>
        /// Writes the mission from the datagrid and values to the EEPROM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BUT_write_Click(object sender, EventArgs e)
        {
            config(true);

            if (CHK_altmode.Checked)
            {
                if (DialogResult.No == MessageBox.Show("Absolute Alt is ticked are you sure?", "Alt Mode", MessageBoxButtons.YesNo))
                {
                    CHK_altmode.Checked = false;
                }
            }

            // check for invalid grid data
            for (int a = 0; a < Commands.Rows.Count - 0; a++)
            {
                for (int b = 0; b < Commands.ColumnCount - 0; b++)
                {
                    double answer;
                    if (b >= 1 && b <= 4)
                    {
                        if (!double.TryParse(Commands[b, a].Value.ToString(), out answer))
                        {
                            MessageBox.Show("There are errors in your mission");
                            return;
                        }
                    }
                }
            }

            System.Threading.Thread t12 = new System.Threading.Thread(delegate()
            {
                try
                {
                    MAVLink port = MainV2.comPort;

                    if (!port.IsOpen)
                    {
                        throw new Exception("Please Connect First!");
                    }

                    MainV2.givecomport = true;

                    Locationwp home = new Locationwp();

                    try
                    {
                        home.lat = (int)(float.Parse(TXT_homelat.Text) * 10000000);
                        home.lng = (int)(float.Parse(TXT_homelng.Text) * 10000000);
                        home.alt = (int)(float.Parse(TXT_homealt.Text) / MainV2.cs.multiplierdist * 100); // use saved home
                    }
                    catch { throw new Exception("Your home location is invalid"); }

                    port.setWPTotal((ushort)(Commands.Rows.Count + 1)); // + home

                    port.setWP(home, (ushort)0, MAVLink.MAV_FRAME.MAV_FRAME_GLOBAL, 0);

                    MAVLink.MAV_FRAME frame = MAVLink.MAV_FRAME.MAV_FRAME_GLOBAL_RELATIVE_ALT;

                    // process grid to memory eeprom
                    for (int a = 0; a < Commands.Rows.Count - 0; a++)
                    {
                        Locationwp temp = new Locationwp();
                        temp.id = (byte)(int)Enum.Parse(typeof(MAVLink.MAV_CMD), Commands.Rows[a].Cells[0].Value.ToString(), false);
                        temp.p1 = byte.Parse(Commands.Rows[a].Cells[1].Value.ToString());
                        if (temp.id < (byte)MAVLink.MAV_CMD.LAST)
                        {
                            if (CHK_altmode.Checked)
                            {
                                frame = MAVLink.MAV_FRAME.MAV_FRAME_GLOBAL;
                            }
                            else
                            {
                                frame = MAVLink.MAV_FRAME.MAV_FRAME_GLOBAL_RELATIVE_ALT;
                            }

                            temp.alt = (int)(double.Parse(Commands.Rows[a].Cells[2].Value.ToString()) / MainV2.cs.multiplierdist * 100);
                            temp.lat = (int)(double.Parse(Commands.Rows[a].Cells[3].Value.ToString()) * 10000000);
                            temp.lng = (int)(double.Parse(Commands.Rows[a].Cells[4].Value.ToString()) * 10000000);
                        }
                        else
                        {
                            frame = MAVLink.MAV_FRAME.MAV_FRAME_MISSION;

                            temp.alt = (int)(double.Parse(Commands.Rows[a].Cells[2].Value.ToString()));
                            temp.lat = (int)(double.Parse(Commands.Rows[a].Cells[3].Value.ToString()));
                            temp.lng = (int)(double.Parse(Commands.Rows[a].Cells[4].Value.ToString()));
                        }

                        port.setWP(temp, (ushort)(a + 1), frame, 0);
                    }

                    port.setWPACK();

                    if (CHK_holdalt.Checked)
                    {
                        port.setParam("ALT_HOLD_RTL", int.Parse(TXT_DefaultAlt.Text) / MainV2.cs.multiplierdist * 100);
                    }
                    else
                    {
                        port.setParam("ALT_HOLD_RTL", -1);
                    }

                    port.setParam("WP_RADIUS", (byte)int.Parse(TXT_WPRad.Text) / MainV2.cs.multiplierdist);

                    try
                    {
                        port.setParam("LOITER_RADIUS", (byte)int.Parse(TXT_loiterrad.Text) / MainV2.cs.multiplierdist);
                    }
                    catch
                    {
                        port.setParam("WP_LOITER_RAD", (byte)int.Parse(TXT_loiterrad.Text) / MainV2.cs.multiplierdist);
                    }

                }
                catch (Exception ex) { MessageBox.Show("Error : " + ex.ToString()); }
                try
                {
                    this.BeginInvoke((System.Threading.ThreadStart)delegate()
                    {
                        MainV2.givecomport = false;
                        BUT_write.Enabled = true;
                    });
                }
                catch { }

            });
            t12.IsBackground = true;
            t12.Name = "Write wps";
            t12.Start();
            MainV2.threads.Add(t12);

            MainMap.Focus();

            BUT_write.Enabled = false;
        }

        /// <summary>
        /// Processes a loaded EEPROM to the map and datagrid
        /// </summary>
        void processToScreen(List<Locationwp> cmds)
        {
            quickadd = true;
            Commands.Rows.Clear();

            int i = -1;
            foreach (Locationwp temp in cmds)
            {
                i++;
                /*if (temp.id == 0 && i != 0) // 0 and not home
                    break;
                if (temp.id == 255 && i != 0) // bad record - never loaded any WP's - but have started the board up.
                    break;*/
                if (i + 1 >= Commands.Rows.Count)
                {
                    Commands.Rows.Add();
                }
                if (temp.id == 0 && i == 0 && temp.alt == 0) // skip 0 home
                    continue;
                DataGridViewTextBoxCell cell;
                DataGridViewComboBoxCell cellcmd;
                cellcmd = Commands.Rows[i].Cells[Command.Index] as DataGridViewComboBoxCell;
                foreach (object value in Enum.GetValues(typeof(MAVLink.MAV_CMD)))
                {
                    if ((int)value == temp.id)
                    {
                        cellcmd.Value = value.ToString();
                        break;
                    }
                }
                if (temp.id < (byte)MAVLink.MAV_CMD.LAST)
                {
                    int alt = temp.alt;

                    if ((temp.options & 0x1) == 0 && i != 0) // home is always abs
                    {
                        CHK_altmode.Checked = true;
                    }
                    else
                    {
                        CHK_altmode.Checked = false;
                    }

                    cell = Commands.Rows[i].Cells[Param1.Index] as DataGridViewTextBoxCell;
                    cell.Value = temp.p1;
                    cell = Commands.Rows[i].Cells[Param2.Index] as DataGridViewTextBoxCell;
                    cell.Value = (int)((double)alt * MainV2.cs.multiplierdist / 100);
                    cell = Commands.Rows[i].Cells[Param3.Index] as DataGridViewTextBoxCell;
                    cell.Value = (double)temp.lat / 10000000;
                    cell = Commands.Rows[i].Cells[Param4.Index] as DataGridViewTextBoxCell;
                    cell.Value = (double)temp.lng / 10000000;
                }
                else
                {
                    cell = Commands.Rows[i].Cells[Param1.Index] as DataGridViewTextBoxCell;
                    cell.Value = temp.p1;
                    cell = Commands.Rows[i].Cells[Param2.Index] as DataGridViewTextBoxCell;
                    cell.Value = temp.alt;
                    cell = Commands.Rows[i].Cells[Param3.Index] as DataGridViewTextBoxCell;
                    cell.Value = temp.lat;
                    cell = Commands.Rows[i].Cells[Param4.Index] as DataGridViewTextBoxCell;
                    cell.Value = temp.lng;
                }
            }
            try
            {
                DataGridViewTextBoxCell cellhome;
                cellhome = Commands.Rows[0].Cells[Param3.Index] as DataGridViewTextBoxCell;
                if (cellhome.Value != null)
                {
                    if (cellhome.Value.ToString() != TXT_homelat.Text)
                    {
                        DialogResult dr = MessageBox.Show("Reset Home to loaded coords", "Reset Home Coords", MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            TXT_homelat.Text = (double.Parse(cellhome.Value.ToString())).ToString();
                            cellhome = Commands.Rows[0].Cells[Param4.Index] as DataGridViewTextBoxCell;
                            TXT_homelng.Text = (double.Parse(cellhome.Value.ToString())).ToString();
                            cellhome = Commands.Rows[0].Cells[Param2.Index] as DataGridViewTextBoxCell;
                            TXT_homealt.Text = (double.Parse(cellhome.Value.ToString()) * MainV2.cs.multiplierdist).ToString();
                        }
                    }
                }

                TXT_DefaultAlt.Text = ((float)param["ALT_HOLD_RTL"] * MainV2.cs.multiplierdist / 100).ToString();
                TXT_WPRad.Text = ((float)param["WP_RADIUS"] * MainV2.cs.multiplierdist).ToString();
                try
                {
                    TXT_loiterrad.Text = ((float)param["LOITER_RADIUS"] * MainV2.cs.multiplierdist).ToString();
                }
                catch
                {
                    TXT_loiterrad.Text = ((float)param["WP_LOITER_RAD"] * MainV2.cs.multiplierdist).ToString();
                }
                CHK_holdalt.Checked = Convert.ToBoolean((float)param["ALT_HOLD_RTL"] > 0);

            }
            catch (Exception) { } // if there is no valid home

            if (Commands.RowCount > 0)
            {
                Commands.Rows.Remove(Commands.Rows[0]); // remove home row
            }

            quickadd = false;

            writeKML();

            MainMap.ZoomAndCenterMarkers("objects");

            MainMap_OnMapZoomChanged();
        }



        /// <summary>
        /// Saves this forms config to MAIN, where it is written in a global config
        /// </summary>
        /// <param name="write">true/false</param>
        private void config(bool write)
        {
            if (write)
            {
                ArdupilotMega.MainV2.config["TXT_homelat"] = TXT_homelat.Text;
                ArdupilotMega.MainV2.config["TXT_homelng"] = TXT_homelng.Text;
                ArdupilotMega.MainV2.config["TXT_homealt"] = TXT_homealt.Text;


                ArdupilotMega.MainV2.config["TXT_WPRad"] = TXT_WPRad.Text;

                ArdupilotMega.MainV2.config["TXT_loiterrad"] = TXT_loiterrad.Text;

                ArdupilotMega.MainV2.config["TXT_DefaultAlt"] = TXT_DefaultAlt.Text;

                ArdupilotMega.MainV2.config["CHK_altmode"] = CHK_altmode.Checked;

            }
            else
            {
                foreach (string key in ArdupilotMega.MainV2.config.Keys)
                {
                    switch (key)
                    {
                        case "TXT_homelat":
                            TXT_homelat.Text = ArdupilotMega.MainV2.config[key].ToString();
                            break;
                        case "TXT_homelng":
                            TXT_homelng.Text = ArdupilotMega.MainV2.config[key].ToString();
                            break;
                        case "TXT_homealt":
                            TXT_homealt.Text = ArdupilotMega.MainV2.config[key].ToString();
                            break;
                        case "TXT_WPRad":
                            TXT_WPRad.Text = ArdupilotMega.MainV2.config[key].ToString();
                            break;
                        case "TXT_loiterrad":
                            TXT_loiterrad.Text = ArdupilotMega.MainV2.config[key].ToString();
                            break;
                        case "TXT_DefaultAlt":
                            TXT_DefaultAlt.Text = ArdupilotMega.MainV2.config[key].ToString();
                            break;
                        case "CHK_altmode":
                            CHK_altmode.Checked = false;//bool.Parse(ArdupilotMega.MainV2.config[key].ToString());
                            break;
                        default:
                            break;
                    }
                }

            }
        }

        private void TXT_WPRad_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            if (e.KeyChar.ToString() == "\b")
                return;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }

        private void TXT_WPRad_Leave(object sender, EventArgs e)
        {
            int isNumber = 0;
            if (!int.TryParse(TXT_WPRad.Text, out isNumber))
            {
                TXT_WPRad.Text = "30";
            }
        }

        private void TXT_loiterrad_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            if (e.KeyChar.ToString() == "\b")
                return;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }

        private void TXT_loiterrad_Leave(object sender, EventArgs e)
        {
            int isNumber = 0;
            if (!int.TryParse(TXT_loiterrad.Text, out isNumber))
            {
                TXT_loiterrad.Text = "45";
            }
        }

        private void TXT_DefaultAlt_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            if (e.KeyChar.ToString() == "\b")
                return;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }

        private void TXT_DefaultAlt_Leave(object sender, EventArgs e)
        {
            int isNumber = 0;
            if (!int.TryParse(TXT_DefaultAlt.Text, out isNumber))
            {
                TXT_DefaultAlt.Text = "100";
            }
        }


        /// <summary>
        /// used to control buttons in the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Commands_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                if (e.ColumnIndex == Delete.Index && (e.RowIndex + 0) < Commands.RowCount) // delete
                {
                    quickadd = true;
                    Commands.Rows.RemoveAt(e.RowIndex);
                    quickadd = false;
                    writeKML();
                }
                if (e.ColumnIndex == Up.Index && e.RowIndex != 0) // up
                {
                    DataGridViewRow myrow = Commands.CurrentRow;
                    Commands.Rows.Remove(myrow);
                    Commands.Rows.Insert(e.RowIndex - 1, myrow);
                    writeKML();
                }
                if (e.ColumnIndex == Down.Index && e.RowIndex < Commands.RowCount - 1) // down
                {
                    DataGridViewRow myrow = Commands.CurrentRow;
                    Commands.Rows.Remove(myrow);
                    Commands.Rows.Insert(e.RowIndex + 1, myrow);
                    writeKML();
                }
            }
            catch (Exception) { MessageBox.Show("Row error"); }
        }

        private void Commands_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[Delete.Index].Value = "X";
            e.Row.Cells[Up.Index].Value = global::ArdupilotMega.Properties.Resources.up;
            e.Row.Cells[Down.Index].Value = global::ArdupilotMega.Properties.Resources.down;
        }

        private void TXT_homelat_TextChanged(object sender, EventArgs e)
        {
            sethome = false;
            writeKML();
        }

        private void TXT_homelng_TextChanged(object sender, EventArgs e)
        {
            sethome = false;
            writeKML();
        }

        private void TXT_homealt_TextChanged(object sender, EventArgs e)
        {
            sethome = false;
            writeKML();
        }

        private void Planner_FormClosing(object sender, FormClosingEventArgs e)
        {
            config(true);
        }

        private void BUT_loadwpfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Ardupilot Mission (*.h)|*.*";
            fd.DefaultExt = ".h";
            DialogResult result = fd.ShowDialog();
            string file = fd.FileName;
            if (file != "")
            {
                readwaypointwritterfile(file);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                lock (thisLock)
                {
                    MainMap.Zoom = trackBar1.Value;
                }
            }
            catch { }
        }

        // marker
        GMapMarker currentMarker;
        GMapMarker center = new GMapMarkerCross(new PointLatLng(0.0, 0.0));

        // polygons
        GMapPolygon polygon;
        //static GMapRoute route;

        // layers
        GMapOverlay top;
        internal GMapOverlay objects;
        static GMapOverlay routes;// static so can update from gcs
        static GMapOverlay polygons;

        // etc
        readonly Random rnd = new Random();
        string mobileGpsLog = string.Empty;
        GMapMarkerRect CurentRectMarker = null;
        bool isMouseDown = false;
        bool isMouseDraging = false;
        PointLatLng start;
        PointLatLng end;

        //public long ElapsedMilliseconds;

        #region -- map events --
        void MainMap_OnMarkerLeave(GMapMarker item)
        {
            if (!isMouseDown)
            {
                if (item is GMapMarkerRect)
                {
                    CurentRectMarker = null;

                    GMapMarkerRect rc = item as GMapMarkerRect;
                    rc.Pen.Color = Color.Blue;
                    MainMap.Invalidate(false);
                }
            }
        }

        void MainMap_OnMarkerEnter(GMapMarker item)
        {
            if (!isMouseDown)
            {
                if (item is GMapMarkerRect)
                {
                    GMapMarkerRect rc = item as GMapMarkerRect;
                    rc.Pen.Color = Color.Red;
                    MainMap.Invalidate(false);

                    CurentRectMarker = rc;
                }
            }
        }

        void MainMap_OnMapTypeChanged(MapType type)
        {
            comboBoxMapType.SelectedItem = MainMap.MapType;

            MainMap.MaxZoom = MainMap.MaxZoom + 1;

            trackBar1.Minimum = MainMap.MinZoom;
            trackBar1.Maximum = MainMap.MaxZoom + 0.99;

            MainMap.ZoomAndCenterMarkers("objects");

        }

        void MainMap_MouseUp(object sender, MouseEventArgs e)
        {
            end = MainMap.FromLocalToLatLng(e.X, e.Y);

            if (e.Button == MouseButtons.Right) // ignore right clicks
            {
                return;
            }

            if (isMouseDown) // mouse down on some other object and dragged to here.
            {
                if (e.Button == MouseButtons.Left)
                {
                    isMouseDown = false;
                }
                if (!isMouseDraging)
                {
                    if (CurentRectMarker != null)
                    {
                        // cant add WP in existing rect
                    }
                    else
                    {
                        callMe(currentMarker.Position.Lat, currentMarker.Position.Lng, 0);
                    }
                }
                else
                {
                    if (CurentRectMarker != null)
                    {
                        callMeDrag(CurentRectMarker.InnerMarker.Tag.ToString(), currentMarker.Position.Lat, currentMarker.Position.Lng, 0);
                        CurentRectMarker = null;
                    }
                }
            }

            isMouseDraging = false;
        }

        void MainMap_MouseDown(object sender, MouseEventArgs e)
        {
            start = MainMap.FromLocalToLatLng(e.X, e.Y);

            if (e.Button == MouseButtons.Left && Control.ModifierKeys != Keys.Alt)
            {
                isMouseDown = true;
                isMouseDraging = false;

                if (currentMarker.IsVisible)
                {
                    currentMarker.Position = MainMap.FromLocalToLatLng(e.X, e.Y);
                }
            }
        }

        // move current marker with left holding
        void MainMap_MouseMove(object sender, MouseEventArgs e)
        {
            PointLatLng point = MainMap.FromLocalToLatLng(e.X, e.Y);

            currentMarker.Position = point;

            if (!isMouseDown)
            {
                callMeDisplay(point.Lat, point.Lng, 0);
            }

            //draging
            if (e.Button == MouseButtons.Left && isMouseDown)
            {
                isMouseDraging = true;
                if (CurentRectMarker == null) // left click pan
                {
                    double latdif = start.Lat - point.Lat;
                    double lngdif = start.Lng - point.Lng;

                    try
                    {
                        lock (thisLock)
                        {
                            MainMap.Position = new PointLatLng(center.Position.Lat + latdif, center.Position.Lng + lngdif);
                        }
                    }
                    catch { }
                }
                else // move rect marker
                {
                    PointLatLng pnew = MainMap.FromLocalToLatLng(e.X, e.Y);

                    int? pIndex = (int?)CurentRectMarker.Tag;
                    if (pIndex.HasValue)
                    {
                        if (pIndex < polygon.Points.Count)
                        {
                            polygon.Points[pIndex.Value] = pnew;
                            lock (thisLock)
                            {
                                MainMap.UpdatePolygonLocalPosition(polygon);
                            }
                        }
                    }

                    if (currentMarker.IsVisible)
                    {
                        currentMarker.Position = pnew;
                    }
                    CurentRectMarker.Position = pnew;

                    if (CurentRectMarker.InnerMarker != null)
                    {
                        CurentRectMarker.InnerMarker.Position = pnew;
                    }
                }
            }

        }

        // MapZoomChanged
        void MainMap_OnMapZoomChanged()
        {
            if (MainMap.Zoom > 0)
            {
                trackBar1.Value = (int)(MainMap.Zoom);
                //textBoxZoomCurrent.Text = MainMap.Zoom.ToString();
                center.Position = MainMap.Position;
            }
        }

        // click on some marker
        void MainMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            int answer;
            try // when dragging item can sometimes be null
            {
                if (int.TryParse(item.Tag.ToString(), out answer))
                {

                    Commands.CurrentCell = Commands[0, answer - 1];

                }
            }
            catch { }
            return;
        }

        // loader start loading tiles
        void MainMap_OnTileLoadStart()
        {
            MethodInvoker m = delegate()
            {
                lbl_status.Text = "Status: loading tiles...";
            };
            try
            {
                BeginInvoke(m);
            }
            catch
            {
            }
        }

        // loader end loading tiles
        void MainMap_OnTileLoadComplete(long ElapsedMilliseconds)
        {

            //MainMap.ElapsedMilliseconds = ElapsedMilliseconds;

            MethodInvoker m = delegate()
            {
                lbl_status.Text = "Status: loaded tiles";

                //panelMenu.Text = "Menu, last load in " + MainMap.ElapsedMilliseconds + "ms";

                //textBoxMemory.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00}MB of {1:0.00}MB", MainMap.Manager.MemoryCacheSize, MainMap.Manager.MemoryCacheCapacity);
            };
            try
            {
                BeginInvoke(m);
            }
            catch
            {
            }

        }

        // current point changed
        void MainMap_OnCurrentPositionChanged(PointLatLng point)
        {
            if (point.Lat > 90) { point.Lat = 90; }
            if (point.Lat < -90) { point.Lat = -90; }
            if (point.Lng > 180) { point.Lng = 180; }
            if (point.Lng < -180) { point.Lng = -180; }
            center.Position = point;
            TXT_mouselat.Text = point.Lat.ToString(CultureInfo.InvariantCulture);
            TXT_mouselong.Text = point.Lng.ToString(CultureInfo.InvariantCulture);
        }

        // center markers on start
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (objects.Markers.Count > 0)
            {
                MainMap.ZoomAndCenterMarkers(null);
            }
            trackBar1.Value = (int)MainMap.Zoom;
        }

        // ensure focus on map, trackbar can have it too
        private void MainMap_MouseEnter(object sender, EventArgs e)
        {
            // MainMap.Focus();
        }
        #endregion

        /// <summary>
        /// used to redraw the polygon
        /// </summary>
        void RegeneratePolygon()
        {
            List<PointLatLng> polygonPoints = new List<PointLatLng>();

            if (objects == null)
                return;

            foreach (GMapMarker m in objects.Markers)
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
                    lock (thisLock)
                    {
                        MainMap.UpdatePolygonLocalPosition(polygon);
                    }
                }
            }
        }

        private void comboBoxMapType_SelectedValueChanged(object sender, EventArgs e)
        {
            MainMap.MapType = (MapType)comboBoxMapType.SelectedItem;
        }

        private void Commands_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
            {
                var temp = ((ComboBox)e.Control);
                //((ComboBox)e.Control).SelectedIndexChanged += new EventHandler(Commands_SelectedIndexChanged);
                ((ComboBox)e.Control).ForeColor = Color.White;
                ((ComboBox)e.Control).BackColor = Color.FromArgb(0x43, 0x44, 0x45);
                System.Diagnostics.Debug.WriteLine("Setting event handle");
            }
        }

        void Commands_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update row headers
            ((ComboBox)sender).ForeColor = Color.White;
            Commands_RowEnter(null, new DataGridViewCellEventArgs(Commands.CurrentCell.ColumnIndex, Commands.CurrentCell.RowIndex));
        }
        /// <summary>
        /// Get the Google earth ALT for a given coord
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns>Altitude</returns>
        double getGEAlt(double lat, double lng)
        {
            double alt = 0;
            //http://maps.google.com/maps/api/elevation/xml

            try
            {
                using (XmlTextReader xmlreader = new XmlTextReader("http://maps.google.com/maps/api/elevation/xml?locations=" + lat.ToString(new System.Globalization.CultureInfo("en-US")) + "," + lng.ToString(new System.Globalization.CultureInfo("en-US")) + "&sensor=true"))
                {
                    while (xmlreader.Read())
                    {
                        xmlreader.MoveToElement();
                        switch (xmlreader.Name)
                        {
                            case "elevation":
                                alt = double.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch { }

            return alt * MainV2.cs.multiplierdist;
        }

        private void TXT_homelat_Enter(object sender, EventArgs e)
        {
            sethome = true;
            MessageBox.Show("Click on the Map to set Home ");
        }

        private void Planner_Resize(object sender, EventArgs e)
        {
            MainMap.Zoom = trackBar1.Value;
        }

        private void BUT_calc_Click(object sender, EventArgs e)
        {
            Form calc = new Calc();
            MainV2.fixtheme(calc);
            calc.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            writeKML();
            double homealt;
            double.TryParse(TXT_homealt.Text, out homealt);
            Form temp = new ElevationProfile(pointlist, homealt);
            MainV2.fixtheme(temp);
            temp.ShowDialog();
        }

        private void CHK_altmode_CheckedChanged(object sender, EventArgs e)
        {
            if (Commands.RowCount > 0 && !quickadd)
                MessageBox.Show("You will need to change your altitudes");
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            try
            {
                base.OnPaint(pe);
            }
            catch (Exception)
            {
            }
        }

        private void Commands_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Commands_RowEnter(null, new DataGridViewCellEventArgs(Commands.CurrentCell.ColumnIndex, Commands.CurrentCell.RowIndex));
        }

        private void MainMap_Resize(object sender, EventArgs e)
        {
            MainMap.Zoom = MainMap.Zoom + 0.01;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                lock (thisLock)
                {
                    MainMap.Zoom = trackBar1.Value;
                }
            }
            catch { }
        }

        private void panel4_PanelCollapsing(object sender, BSE.Windows.Forms.XPanderStateChangeEventArgs e)
        {

        }

        private void BUT_Prefetch_Click(object sender, EventArgs e)
        {
            RectLatLng area = MainMap.SelectedArea;
            if (!area.IsEmpty)
            {
                for (int i = (int)MainMap.Zoom; i <= MainMap.MaxZoom; i++)
                {
                    DialogResult res = MessageBox.Show("Ready ripp at Zoom = " + i + " ?", "GMap.NET", MessageBoxButtons.YesNoCancel);

                    if (res == DialogResult.Yes)
                    {
                        TilePrefetcher obj = new TilePrefetcher();
                        obj.ShowCompleteMessage = true;
                        obj.Start(area, MainMap.Projection, i, MainMap.MapType, 100);
                    }
                    else if (res == DialogResult.No)
                    {
                        continue;
                    }
                    else if (res == DialogResult.Cancel)
                    {
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Select map area holding ALT", "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BUT_grid_Click(object sender, EventArgs e)
        {
            RectLatLng area = MainMap.SelectedArea;
            if (!area.IsEmpty)
            {
                PointLatLng topright = new PointLatLng(area.LocationTopLeft.Lat, area.LocationRightBottom.Lng);
                PointLatLng bottomleft = new PointLatLng(area.LocationRightBottom.Lat, area.LocationTopLeft.Lng);

                double diagdist = MainMap.Manager.GetDistance(area.LocationTopLeft,area.LocationRightBottom) * 1000;
                double heightdist = MainMap.Manager.GetDistance(area.LocationTopLeft, bottomleft) * 1000;
                double widthdist = MainMap.Manager.GetDistance(area.LocationTopLeft, topright) * 1000;



                string alt = (100 * MainV2.cs.multiplierdist).ToString("0");
                Common.InputBox("Altitude", "Relative Altitude", ref alt);


                string distance = (50 * MainV2.cs.multiplierdist).ToString("0");
                Common.InputBox("Distance","Distance between lines",ref distance);


                string overshoot = (30 * MainV2.cs.multiplierdist).ToString("0");
                Common.InputBox("Overshot", "Enter of line overshoot amount", ref overshoot);

                // lat - up down
                // lng - left right

                int overshootdist = (int)(double.Parse(overshoot) / MainV2.cs.multiplierdist);

                int altitude = (int)(double.Parse(alt) / MainV2.cs.multiplierdist);

                double latdiff = area.HeightLat / ((heightdist / (double.Parse(distance) / MainV2.cs.multiplierdist)));

                double overshootdistlng = area.WidthLng / widthdist * overshootdist;

                bool dir = false;

                int count = 0;

                for (double x = bottomleft.Lat; x < topright.Lat + latdiff; x += latdiff) 
                {
                    if (dir)
                    {
                        callMe(x, topright.Lng, altitude);
                        callMe(x, bottomleft.Lng - overshootdistlng, altitude);
                    }
                    else
                    {
                        callMe(x, bottomleft.Lng, altitude);
                        callMe(x, topright.Lng + overshootdistlng, altitude);
                    }

                    dir = !dir;

                    count++;

                    if (Commands.RowCount > 150)
                    {
                        MessageBox.Show("Stopping at 150 WP's");
                        break;
                    }
                }
                              


            }
            else
            {
                MessageBox.Show("Select map area holding ALT", "Area", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void label4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MainV2.cs.lat != 0)
            {
                TXT_homealt.Text = (MainV2.cs.alt * MainV2.cs.multiplierdist).ToString("0");
                TXT_homelat.Text = MainV2.cs.lat.ToString();
                TXT_homelng.Text = MainV2.cs.lng.ToString();
            }
            else
            {
                MessageBox.Show("Please Connect/wait for lock, and click here to set your home to your current location");
            }
        }
    }
}
