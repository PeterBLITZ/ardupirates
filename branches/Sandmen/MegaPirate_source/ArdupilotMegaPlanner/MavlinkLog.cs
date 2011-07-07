using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
//using KMLib;
//using KMLib.Feature;
//using KMLib.Geometry;
//using Core.Geometry;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Core;

using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Dom.GX;

using System.Reflection;
using System.Xml;


namespace ArdupilotMega
{
    public partial class MavlinkLog : Form
    {
        List<CurrentState> flightdata = new List<CurrentState>();

        public MavlinkLog()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false; // so can update display from another thread
        }

        /*
        private void processLine(string line)
        {
            try
            {
                line = line.Replace(", ", ",");
                line = line.Replace(": ", ":");

                string[] items = line.Split(',',':');

                if (items[0].Contains("CMD")) {
                    if (flightdata.Count == 0)
                    {
                        if (int.Parse(items[2]) <= (int)MAVLink.MAV_CMD.LAST) // wps
                        {
                            PointLatLngAlt temp = new PointLatLngAlt(double.Parse(items[5],new System.Globalization.CultureInfo("en-US")) / 10000000, double.Parse(items[6],new System.Globalization.CultureInfo("en-US")) / 10000000, double.Parse(items[4],new System.Globalization.CultureInfo("en-US")) / 100, items[1].ToString());
                            cmd.Add(temp);
                        }
                    }
                }
                if (items[0].Contains("MOD"))
                {
                    positionindex++;
                    modelist.Add(""); // i cant be bothered doing this properly
                    modelist.Add("");                        
                    modelist[positionindex] = (items[1]);
                }
                if (items[0].Contains("GPS") && items[2] == "1" && items[4] != "0" && items[4] != "-1" && lastline != line) // check gps line and fixed status
                {
                    if (position[positionindex] == null)
                        position[positionindex] = new List<Point3D>();

                    position[positionindex].Add(new Point3D(double.Parse(items[5],new System.Globalization.CultureInfo("en-US")), double.Parse(items[4],new System.Globalization.CultureInfo("en-US")), double.Parse(items[6],new System.Globalization.CultureInfo("en-US"))));
                    oldlastpos = lastpos;
                    lastpos = (position[positionindex][position[positionindex].Count - 1]);
                    lastline = line;
                }
                if (items[0].Contains("CTUN"))
                {
                    ctunlast = items;
                }
                if (items[0].Contains("NTUN"))
                {
                    ntunlast = items;
                    line = "ATT:" + double.Parse(ctunlast[3], new System.Globalization.CultureInfo("en-US")) * 100 + "," + double.Parse(ctunlast[6], new System.Globalization.CultureInfo("en-US")) * 100 + "," + double.Parse(items[1], new System.Globalization.CultureInfo("en-US")) * 100;
                    items = line.Split(',', ':');
                }
                if (items[0].Contains("ATT"))
                {
                    try
                    {
                        if (lastpos.X != 0 && oldlastpos != lastpos)
                        {
                                Data dat = new Data();

                                runmodel = new Model();

                                runmodel.Location.longitude = lastpos.X;
                                runmodel.Location.latitude = lastpos.Y;
                                runmodel.Location.altitude = lastpos.Z;

                                runmodel.Orientation.roll = double.Parse(items[1], new System.Globalization.CultureInfo("en-US")) / -100;
                                runmodel.Orientation.tilt = double.Parse(items[2], new System.Globalization.CultureInfo("en-US")) / -100;
                                runmodel.Orientation.heading = double.Parse(items[3], new System.Globalization.CultureInfo("en-US")) / 100;

                                dat.model = runmodel;
                                dat.ctun = ctunlast;
                                dat.ntun = ntunlast;

                                    flightdata.Add(dat);                          
                        }
                    }
                    catch { }
                }
            }
            catch (Exception)
            {
                // if items is to short or parse fails.. ignore
            }
        }
        */

        private void writeKML(string filename)
        {
            Color[] colours = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet, Color.Pink };

            Document kml = new Document();

            Tour tour = new Tour();
            tour.Name = "First Person View";
            Playlist tourplaylist = new Playlist();

            AddNamespace(kml, "gx", "http://www.google.com/kml/ext/2.2");

            Style style = new Style();
            style.Id = "yellowLineGreenPoly";
            style.Line = new LineStyle(new Color32(HexStringToColor("7f00ffff")), 4);

            PolygonStyle pstyle = new PolygonStyle();
            pstyle.Color = new Color32(HexStringToColor("7f00ff00"));
            style.Polygon = pstyle;

            kml.AddStyle(style);

            // create sub folders
            Folder planes = new Folder();
            planes.Name = "Planes";
            kml.AddFeature(planes);


            // coords for line string
            CoordinateCollection coords = new CoordinateCollection();

            int a = 1;
            int c = -1;
            DateTime lasttime = DateTime.MaxValue;
            DateTime starttime = DateTime.MinValue;
            Color stylecolor = Color.AliceBlue;
            string mode = flightdata[0].mode;
            foreach (CurrentState cs in flightdata)
            {
                progressBar1.Value = 50 + (int)((float)a / (float)flightdata.Count * 100.0f / 2.0f);
                progressBar1.Refresh();

                if (starttime == DateTime.MinValue)
                {
                    starttime = cs.datetime;
                    lasttime = cs.datetime;
                }

                if (mode != cs.mode || flightdata.Count == a)
                {
                    c++;

                    LineString ls = new LineString();
                    ls.AltitudeMode = SharpKml.Dom.AltitudeMode.Absolute;
                    ls.Extrude = true;

                    ls.Coordinates = coords;

                    Placemark pm = new Placemark();

                    pm.Name = c + " Flight Path " + mode;
                    pm.StyleUrl = new Uri("#yellowLineGreenPoly",UriKind.Relative);
                    pm.Geometry = ls;

                    SharpKml.Dom.TimeSpan ts = new SharpKml.Dom.TimeSpan();
                    ts.Begin = starttime;
                    ts.End = cs.datetime;

                    pm.Time = ts;

                    // setup for next line
                    mode = cs.mode;
                    starttime = cs.datetime;

                    stylecolor = colours[c % (colours.Length - 1)];

                    Style style2 = new Style();
                    style2.Line = new LineStyle(new Color32(stylecolor), 4);

                    pm.StyleSelector = style2;

                    kml.AddFeature(pm);

                    coords = new CoordinateCollection();
                }

                coords.Add(new Vector(cs.lat,cs.lng,cs.alt));

                SharpKml.Dom.Timestamp tstamp = new SharpKml.Dom.Timestamp();
                tstamp.When = cs.datetime;

                FlyTo flyto = new FlyTo();

                flyto.Duration = (cs.datetime - lasttime).TotalMilliseconds / 1000.0;

                flyto.Mode = FlyToMode.Smooth;
                Camera cam = new Camera();
                cam.AltitudeMode = SharpKml.Dom.AltitudeMode.Absolute;
                cam.Latitude = cs.lat;
                cam.Longitude = cs.lng;
                cam.Altitude = cs.alt;
                cam.Heading = cs.yaw;
                cam.Roll = -cs.roll;
                cam.Tilt = (90 - (cs.pitch * -1));

                cam.GXTimePrimitive = tstamp;

                flyto.View = cam;
                //if (Math.Abs(flyto.Duration.Value) > 0.1)
                {
                    tourplaylist.AddTourPrimitive(flyto);
                    lasttime = cs.datetime;
                }

                 
                Placemark pmplane = new Placemark();
                pmplane.Name = "Plane " + a;



                pmplane.Time = tstamp;

                pmplane.Visibility = false;

                SharpKml.Dom.Location loc = new SharpKml.Dom.Location();
                loc.Latitude = cs.lat;
                loc.Longitude = cs.lng;
                loc.Altitude = cs.alt;

                SharpKml.Dom.Orientation ori = new SharpKml.Dom.Orientation();
                ori.Heading = cs.yaw;
                ori.Roll = -cs.roll;
                ori.Tilt = -cs.pitch;

                SharpKml.Dom.Scale sca = new SharpKml.Dom.Scale();

                sca.X = 2;
                sca.Y = 2;
                sca.Z = 2;

                Model model = new Model();
                model.Location = loc;
                model.Orientation = ori;
                model.AltitudeMode = SharpKml.Dom.AltitudeMode.Absolute;
                model.Scale = sca;

                try
                {
                    Description desc = new Description();
                    desc.Text = @"<![CDATA[
              <table>
                <tr><td>Roll: " + model.Orientation.Roll + @" </td></tr>
                <tr><td>Pitch: " + model.Orientation.Tilt + @" </td></tr>
                <tr><td>Yaw: " + model.Orientation.Heading + @" </td></tr>

              </table>
            ]]>";

                    pmplane.Description = desc;
                }
                catch { }

                Link link = new Link();
                link.Href = new Uri("block_plane_0.dae",UriKind.Relative);

                model.Link = link;

                pmplane.Geometry = model;

                planes.AddFeature(pmplane);

                a++;
            }

            tour.Playlist = tourplaylist;

            kml.AddFeature(tour);

            Serializer serializer = new Serializer();
            serializer.Serialize(kml);


            //Console.WriteLine(serializer.Xml);

            StreamWriter sw = new StreamWriter(filename);
            sw.Write(serializer.Xml);
            sw.Close();

            // create kmz - aka zip file

            FileStream fs = File.Open(filename.Replace(".tlog.kml", ".kmz"), FileMode.Create);
            ZipOutputStream zipStream = new ZipOutputStream(fs);
            zipStream.SetLevel(9); //0-9, 9 being the highest level of compression
            zipStream.UseZip64 = UseZip64.Off; // older zipfile

            // entry 1
            string entryName = ZipEntry.CleanName(Path.GetFileName(filename)); // Removes drive from name and fixes slash direction
            ZipEntry newEntry = new ZipEntry(entryName);
            newEntry.DateTime = DateTime.Now;

            zipStream.PutNextEntry(newEntry);

            // Zip the file in buffered chunks
            // the "using" will close the stream even if an exception occurs
            byte[] buffer = new byte[4096];
            using (FileStream streamReader = File.OpenRead(filename))
            {
                StreamUtils.Copy(streamReader, zipStream, buffer);
            }
            zipStream.CloseEntry();

            File.Delete(filename);

            filename = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + "block_plane_0.dae";

            // entry 2
            entryName = ZipEntry.CleanName(Path.GetFileName(filename)); // Removes drive from name and fixes slash direction
            newEntry = new ZipEntry(entryName);
            newEntry.DateTime = DateTime.Now;

            zipStream.PutNextEntry(newEntry);

            // Zip the file in buffered chunks
            // the "using" will close the stream even if an exception occurs
            buffer = new byte[4096];
            using (FileStream streamReader = File.OpenRead(filename))
            {
                StreamUtils.Copy(streamReader, zipStream, buffer);
            }
            zipStream.CloseEntry();


            zipStream.IsStreamOwner = true;	// Makes the Close also Close the underlying stream
            zipStream.Close();
            flightdata.Clear();
        }

        static void AddNamespace(Element element, string prefix, string uri)
        {
            // The Namespaces property is marked as internal.
            PropertyInfo property = typeof(Element).GetProperty(
                "Namespaces",
                BindingFlags.Instance | BindingFlags.NonPublic);

            var namespaces = (XmlNamespaceManager)property.GetValue(element, null);
            namespaces.AddNamespace(prefix, uri);
        }

        private void Log_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void BUT_redokml_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "*.tlog|*.tlog";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;
            try
            {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + @"logs" + Path.DirectorySeparatorChar;
            }
            catch { } // incase dir doesnt exist

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string logfile in openFileDialog1.FileNames)
                {

                    MAVLink mine = new MAVLink();
                    mine.logplaybackfile = new BinaryReader(File.Open(logfile, FileMode.Open,FileAccess.Read,FileShare.Read));
                    mine.logreadmode = true;

                    MAVLink.packets.Initialize(); // clear

                    CurrentState cs = new CurrentState();

                    float oldlatlngalt = 0;

                    while (mine.logplaybackfile.BaseStream.Position < mine.logplaybackfile.BaseStream.Length)
                    {
                        // bar moves to 50 % in this step
                        progressBar1.Value = (int)((float)mine.logplaybackfile.BaseStream.Position / (float)mine.logplaybackfile.BaseStream.Length * 100.0f / 2.0f);
                        progressBar1.Refresh();

                        byte[] packet = mine.readPacket();

                        cs.datetime = mine.lastlogread;

                        cs.UpdateCurrentSettings(null, true);

                        if (MainV2.talk != null)
                            MainV2.talk.SpeakAsyncCancelAll();

                        if ((float)(cs.lat + cs.lng) != oldlatlngalt 
                            && cs.alt != 0 && cs.lat != 0 && cs.lng != 0)
                        {
                            Console.WriteLine(cs.lat + " " + cs.lng + " " + cs.alt + "   lah " + (float)(cs.lat + cs.lng) + "!=" + oldlatlngalt);
                            CurrentState cs2 = (CurrentState)cs.Clone();

                            flightdata.Add(cs2);

                            oldlatlngalt = (cs.lat + cs.lng);
                        }
                    }

                    mine.logreadmode = false;
                    mine.logplaybackfile.Close();
                    mine.logplaybackfile = null;


                    writeKML(logfile + ".kml");


                }
            }
        }

        public static Color HexStringToColor(string hexColor)
        {
            string hc = (hexColor);
            if (hc.Length != 8)
            {
                // you can choose whether to throw an exception
                //throw new ArgumentException("hexColor is not exactly 6 digits.");
                return Color.Empty;
            }
            string a = hc.Substring(0, 2);
            string r = hc.Substring(6, 2);
            string g = hc.Substring(4, 2);
            string b = hc.Substring(2, 2);
            Color color = Color.Empty;
            try
            {
                int ai
                   = Int32.Parse(a, System.Globalization.NumberStyles.HexNumber);
                int ri
                   = Int32.Parse(r, System.Globalization.NumberStyles.HexNumber);
                int gi
                   = Int32.Parse(g, System.Globalization.NumberStyles.HexNumber);
                int bi
                   = Int32.Parse(b, System.Globalization.NumberStyles.HexNumber);
                color = Color.FromArgb(ai, ri, gi, bi);
            }
            catch
            {
                // you can choose whether to throw an exception
                //throw new ArgumentException("Conversion failed.");
                return Color.Empty;
            }
            return color;
        }

    }
}
