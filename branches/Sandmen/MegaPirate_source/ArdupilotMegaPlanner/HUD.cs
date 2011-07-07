using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

 
using System.Drawing.Drawing2D;

// Control written by Michael Oborne 2011

namespace hud
{
    public partial class HUD : UserControl
    {
        public HUD()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HUD));
            this.SuspendLayout();
            // 
            // HUD
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.DoubleBuffered = true;
            this.Name = "HUD";
            resources.ApplyResources(this, "$this");
            this.ResumeLayout(false);

        }

        float _roll;
        float _navroll;
        float _pitch;
        float _navpitch;
        float _heading;
        float _targetheading;
        float _alt;
        float _targetalt;
        float _groundspeed;
        float _airspeed;
        float _targetspeed;
        float _batterylevel;
        float _batteryremaining;
        float _gpsfix;
        float _gpshdop;
        float _disttowp;
        string _mode = "Manual";
        int _wpno;
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float roll { get { return _roll; } set { if (_roll != value) { _roll = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float navroll { get { return _navroll; } set { if (_navroll != value) { _navroll = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float pitch { get { return _pitch; } set { if (_pitch != value) { _pitch = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float navpitch { get { return _navpitch; } set { if (_navpitch != value) { _navpitch = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float heading { get { return _heading; } set { if (_heading != value) { _heading = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float targetheading { get { return _targetheading; } set { if (_targetheading != value) { _targetheading = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float alt { get { return _alt; } set { if (_alt != value) { _alt = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float targetalt { get { return _targetalt; } set { if (_targetalt != value) { _targetalt = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float groundspeed { get { return _groundspeed; } set { if (_groundspeed != value) { _groundspeed = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float airspeed { get { return _airspeed; } set { if (_airspeed != value) { _airspeed = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float targetspeed { get { return _targetspeed; } set { if (_targetspeed != value) { _targetspeed = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float batterylevel { get { return _batterylevel; } set { if (_batterylevel != value) { _batterylevel = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float batteryremaining { get { return _batteryremaining; } set { if (_batteryremaining != value) { _batteryremaining = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float gpsfix { get { return _gpsfix; } set { if (_gpsfix != value) { _gpsfix = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float gpshdop { get { return _gpshdop; } set { if (_gpshdop != value) { _gpshdop = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public float disttowp { get { return _disttowp; } set { if (_disttowp != value) { _disttowp = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public string mode { get { return _mode; } set { if (_mode != value) { _mode = value; this.Invalidate(); } } }
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Values")]
        public int wpno { get { return _wpno; } set { if (_wpno != value) { _wpno = value; this.Invalidate(); } } }

        public bool bgon = true;
        public bool hudon = true;

        [System.ComponentModel.Browsable(true),
System.ComponentModel.Category("Values")]
        public Color hudcolor { get { return whitePen.Color; } set { whitePen = new Pen(value, 2); } }

        Pen whitePen = new Pen(Color.White, 2);

        public Image bgimage { set { _bgimage = value; this.Invalidate(); } }
        Image _bgimage;

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HUD));

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DateTime starttime = DateTime.Now;

            try
            {

                Graphics graphicsObject = e.Graphics;

                if (_bgimage != null)
                {
                    bgon = false;
                    graphicsObject.DrawImage(_bgimage, 0, 0, this.Width, this.Height);

                    if (hudon == false)
                    {
                        return;
                    }
                }
                else
                {
                    hudon = true;
                    bgon = true;
                }

                graphicsObject.TranslateTransform(this.Width / 2, this.Height / 2);

                graphicsObject.RotateTransform(-roll);

                int fontsize = this.Height / 30; // = 10
                int fontoffset = fontsize - 10;

                int every5deg = -this.Height / 60;

                int pitchoffset = (int)-pitch * every5deg;

                int halfwidth = this.Width / 2;
                int halfheight = this.Height / 2;

                SolidBrush whiteBrush = new SolidBrush(whitePen.Color);
                
                Pen blackPen = new Pen(Color.Black, 2);
                Pen greenPen = new Pen(Color.Green, 2);
                Pen redPen = new Pen(Color.Red, 2);

                // draw sky
                if (bgon == true)
                {
                    Rectangle bg = new Rectangle(-halfwidth * 2, -halfheight * 2, this.Width * 2, halfheight * 2 + pitchoffset);

                    if (bg.Height != 0)
                    {
                        LinearGradientBrush linearBrush = new LinearGradientBrush(bg, Color.Blue,
                            Color.LightBlue, LinearGradientMode.Vertical);

                        Pen transPen = new Pen(Color.Transparent, 0);

                        graphicsObject.DrawRectangle(transPen, bg);

                        graphicsObject.FillRectangle(linearBrush, bg);
                    }
                    // draw ground

                    bg = new Rectangle(-halfwidth * 2, pitchoffset, this.Width * 2, halfheight * 2 - pitchoffset);

                    if (bg.Height != 0)
                    {
                        LinearGradientBrush linearBrush = new LinearGradientBrush(bg, Color.FromArgb(0x9b, 0xb8, 0x24),
                            Color.FromArgb(0x41, 0x4f, 0x07), LinearGradientMode.Vertical);

                        Pen transPen = new Pen(Color.Transparent, 0);

                        graphicsObject.DrawRectangle(transPen, bg);

                        graphicsObject.FillRectangle(linearBrush, bg);
                    }

                    //draw centerline

                    graphicsObject.DrawLine(whitePen, -halfwidth * 2, pitchoffset + 0, halfwidth * 2, pitchoffset + 0);
                }

                graphicsObject.ResetTransform();

                graphicsObject.SetClip(new Rectangle(0, this.Height / 14, this.Width, this.Height - this.Height / 14));

                graphicsObject.TranslateTransform(this.Width / 2, this.Height / 2);
                graphicsObject.RotateTransform(-roll);

                //draw pitch           

                int lengthshort = this.Width / 12;
                int lengthlong = this.Width / 8;

                for (int a = -90; a <= 90; a += 5)
                {
                    // limit to 40 degrees
                    //if (a >= pitch - 20 && a <= pitch + 20)
                    {
                        if (a % 10 == 0)
                        {
                            if (a == 0)
                            {
                                graphicsObject.DrawLine(greenPen, this.Width / 2 - lengthlong - halfwidth, pitchoffset + a * every5deg, this.Width / 2 + lengthlong - halfwidth, pitchoffset + a * every5deg);
                            }
                            else
                            {
                                graphicsObject.DrawLine(whitePen, this.Width / 2 - lengthlong - halfwidth, pitchoffset + a * every5deg, this.Width / 2 + lengthlong - halfwidth, pitchoffset + a * every5deg);
                            }
                            drawstring(e,a.ToString(), new Font("Arial", fontsize + 2), whiteBrush, this.Width / 2 - lengthlong - 30 - halfwidth - (int)(fontoffset * 1.7), pitchoffset + a * every5deg - 8 - fontoffset);
                        }
                        else
                        {
                            graphicsObject.DrawLine(whitePen, this.Width / 2 - lengthshort - halfwidth, pitchoffset + a * every5deg, this.Width / 2 + lengthshort - halfwidth, pitchoffset + a * every5deg);
                            //drawstring(e,a.ToString(), new Font("Arial", 10), whiteBrush, this.Width / 2 - lengthshort - 20 - halfwidth, this.Height / 2 + pitchoffset + a * every5deg - 8);
                        }
                    }
                }

                graphicsObject.ResetTransform();

                // draw roll ind needle

                graphicsObject.TranslateTransform(this.Width / 2, this.Height / 2 + this.Height / 14);

                graphicsObject.RotateTransform(-roll);

                Point[] pointlist = new Point[3];

                lengthlong = this.Height / 66;

                int extra = this.Height / 15 * 7;

                pointlist[0] = new Point(0, -lengthlong * 2 - extra);
                pointlist[1] = new Point(-lengthlong, -lengthlong - extra);
                pointlist[2] = new Point(lengthlong, -lengthlong - extra);

                if (Math.Abs(roll) > 45)
                {
                    redPen.Width = 10;
                }

                graphicsObject.DrawPolygon(redPen, pointlist);

                redPen.Width = 2;

                for (int a = -45; a <= 45; a += 15)
                {
                    graphicsObject.ResetTransform();
                    graphicsObject.TranslateTransform(this.Width / 2, this.Height / 2 + this.Height / 14);
                    graphicsObject.RotateTransform(a);
                    drawstring(e,Math.Abs(a).ToString("##"), new Font("Arial", fontsize), whiteBrush, 0 - 6 - fontoffset, -lengthlong * 2 - extra);
                    graphicsObject.DrawLine(whitePen, 0, -halfheight, 0, -halfheight - 10);
                }

                graphicsObject.ResetTransform();

                //draw centre / current att

                Rectangle centercircle = new Rectangle(halfwidth - 10, halfheight - 10, 20, 20);

                graphicsObject.DrawEllipse(redPen, centercircle);
                graphicsObject.DrawLine(redPen, centercircle.Left - 10, halfheight, centercircle.Left, halfheight);
                graphicsObject.DrawLine(redPen, centercircle.Right, halfheight, centercircle.Right + 10, halfheight);
                graphicsObject.DrawLine(redPen, centercircle.Left + centercircle.Width / 2, centercircle.Top, centercircle.Left + centercircle.Width / 2, centercircle.Top - 10);

                // draw roll ind

                Rectangle arcrect = new Rectangle(this.Width / 2 - this.Height / 2, this.Height / 14, this.Height, this.Height);

                graphicsObject.DrawArc(whitePen, arcrect, 180 + 45, 90);

                //draw heading ind

                graphicsObject.ResetClip();

                Rectangle headbg = new Rectangle(0, 0, this.Width - 0, this.Height / 14);

                graphicsObject.DrawRectangle(blackPen, headbg);

                SolidBrush solidBrush = new SolidBrush(Color.FromArgb(0x55, 0xff, 0xff, 0xff));

                graphicsObject.FillRectangle(solidBrush, headbg);

                // center
                graphicsObject.DrawLine(redPen, headbg.Width / 2, headbg.Bottom, headbg.Width / 2, headbg.Top);

                //bottom line
                graphicsObject.DrawLine(whitePen, headbg.Left + 5, headbg.Bottom - 5, headbg.Width - 5, headbg.Bottom - 5);

                float space = (headbg.Width - 10) / 60.0f;
                int start = ((int)heading - 30);

                // draw for outside the 60 deg
                if (targetheading < start)
                {
                    greenPen.Width = 6;
                    graphicsObject.DrawLine(greenPen, headbg.Left + 5 + space * 0, headbg.Bottom, headbg.Left + 5 + space * (0), headbg.Top);
                }
                if (targetheading > heading + 30)
                {
                    greenPen.Width = 6;
                    graphicsObject.DrawLine(greenPen, headbg.Left + 5 + space * 60, headbg.Bottom, headbg.Left + 5 + space * (60), headbg.Top);
                }

                for (int a = start; a <= heading + 30; a += 1)
                {
                    if ((a % 360) == targetheading)
                    {
                        greenPen.Width = 6;
                        graphicsObject.DrawLine(greenPen, headbg.Left + 5 + space * (a - start), headbg.Bottom, headbg.Left + 5 + space * (a - start), headbg.Top);
                    }
                    if (a % 5 == 0)
                    {
                        //Console.WriteLine(space +" " + a +" "+ (headbg.Left + 5 + space * (a - start)));
                        graphicsObject.DrawLine(whitePen, headbg.Left + 5 + space * (a - start), headbg.Bottom - 5, headbg.Left + 5 + space * (a - start), headbg.Bottom - 10);
                        int disp = a;
                        if (disp < 0)
                            disp += 360;
                        disp = disp % 360;
                        if (disp == 0)
                        {
                            drawstring(e,"N".PadLeft(2), new Font("Arial", fontsize + 4), whiteBrush, headbg.Left - 5 + space * (a - start) - fontoffset, headbg.Bottom - 24 - (int)(fontoffset * 1.7));
                        }
                        else if (disp == 90)
                        {
                            drawstring(e,"E".PadLeft(2), new Font("Arial", fontsize + 4), whiteBrush, headbg.Left - 5 + space * (a - start) - fontoffset, headbg.Bottom - 24 - (int)(fontoffset * 1.7));
                        }
                        else if (disp == 180)
                        {
                            drawstring(e,"S".PadLeft(2), new Font("Arial", fontsize + 4), whiteBrush, headbg.Left - 5 + space * (a - start) - fontoffset, headbg.Bottom - 24 - (int)(fontoffset * 1.7));
                        }
                        else if (disp == 270)
                        {
                            drawstring(e,"W".PadLeft(2), new Font("Arial", fontsize + 4), whiteBrush, headbg.Left - 5 + space * (a - start) - fontoffset, headbg.Bottom - 24 - (int)(fontoffset * 1.7));
                        }
                        else
                        {
                            drawstring(e,(disp % 360).ToString().PadLeft(3), new Font("Arial", fontsize), whiteBrush, headbg.Left - 5 + space * (a - start) - fontoffset, headbg.Bottom - 24 - (int)(fontoffset * 1.7));
                        }
                    }
                }

                // left scroller

                Rectangle scrollbg = new Rectangle(0, halfheight - halfheight / 2, this.Width / 10, this.Height / 2);

                graphicsObject.DrawRectangle(whitePen, scrollbg);

                graphicsObject.FillRectangle(solidBrush, scrollbg);

                Point[] arrow = new Point[5];

                arrow[0] = new Point(0, -10);
                arrow[1] = new Point(scrollbg.Width - 10, -10);
                arrow[2] = new Point(scrollbg.Width - 5, 0);
                arrow[3] = new Point(scrollbg.Width - 10, 10);
                arrow[4] = new Point(0, 10);

                graphicsObject.TranslateTransform(0, this.Height / 2);

                int viewrange = 26;

                float speed = airspeed;
                if (speed == 0)
                    speed = groundspeed;

                space = (scrollbg.Height) / (float)viewrange;
                start = ((int)speed - viewrange / 2);

                for (int a = start; a <= (speed + viewrange / 2); a += 1)
                {
                    if (a == (int)targetspeed && targetspeed != 0)
                    {
                        greenPen.Width = 6;
                        graphicsObject.DrawLine(greenPen, scrollbg.Left, scrollbg.Top - space * (a - start), scrollbg.Left + scrollbg.Width, scrollbg.Top - space * (a - start));
                    }
                    if (a % 5 == 0)
                    {
                        //Console.WriteLine(a + " " + scrollbg.Right + " " + (scrollbg.Top - space * (a - start)) + " " + (scrollbg.Right - 20) + " " + (scrollbg.Top - space * (a - start)));
                        graphicsObject.DrawLine(whitePen, scrollbg.Right, scrollbg.Top - space * (a - start), scrollbg.Right - 10, scrollbg.Top - space * (a - start));
                        drawstring(e,a.ToString().PadLeft(5), new Font("Arial", fontsize), whiteBrush, scrollbg.Right - 50 - 4 * fontoffset, scrollbg.Top - space * (a - start) - 6 - fontoffset);
                    }
                }

                graphicsObject.DrawPolygon(blackPen, arrow);
                graphicsObject.FillPolygon(Brushes.Black, arrow);
                drawstring(e,((int)speed).ToString("0 m/s"), new Font("Arial", 10), Brushes.AliceBlue, 0, -9);

                graphicsObject.ResetTransform();

                // extra text data

                drawstring(e,"AS " + airspeed.ToString("0.0"), new Font("Arial", fontsize), whiteBrush, 1, scrollbg.Bottom + 5);
                drawstring(e,"GS " + groundspeed.ToString("0.0"), new Font("Arial", fontsize), whiteBrush, 1, scrollbg.Bottom + fontsize + 2 + 10);

                //drawstring(e,, new Font("Arial", fontsize + 2), whiteBrush, 1, scrollbg.Bottom + fontsize + 2 + 10);

                // right scroller

                scrollbg = new Rectangle(this.Width - this.Width / 10, halfheight - halfheight / 2, this.Width / 10, this.Height / 2);

                graphicsObject.DrawRectangle(whitePen, scrollbg);

                graphicsObject.FillRectangle(solidBrush, scrollbg);

                arrow = new Point[5];

                arrow[0] = new Point(0, -10);
                arrow[1] = new Point(scrollbg.Width - 10, -10);
                arrow[2] = new Point(scrollbg.Width - 5, 0);
                arrow[3] = new Point(scrollbg.Width - 10, 10);
                arrow[4] = new Point(0, 10);






                graphicsObject.TranslateTransform(0, this.Height / 2);




                viewrange = 26;

                space = (scrollbg.Height) / (float)viewrange;
                start = ((int)alt - viewrange / 2);
                for (int a = start; a <= (alt + viewrange / 2); a += 1)
                {
                    if (a == Math.Round(targetalt) && targetalt != 0)
                    {
                        greenPen.Width = 6;
                        graphicsObject.DrawLine(greenPen, scrollbg.Left, scrollbg.Top - space * (a - start), scrollbg.Left + scrollbg.Width, scrollbg.Top - space * (a - start));
                    }
                    if (a % 5 == 0)
                    {
                        //Console.WriteLine(a + " " + scrollbg.Left + " " + (scrollbg.Top - space * (a - start)) + " " + (scrollbg.Left + 20) + " " + (scrollbg.Top - space * (a - start)));
                        graphicsObject.DrawLine(whitePen, scrollbg.Left, scrollbg.Top - space * (a - start), scrollbg.Left + 10, scrollbg.Top - space * (a - start));
                        drawstring(e,a.ToString().PadLeft(5), new Font("Arial", fontsize), whiteBrush, scrollbg.Left + 7 + (int)(0 * fontoffset), scrollbg.Top - space * (a - start) - 6 - fontoffset);
                    }
                }

                graphicsObject.ResetTransform();
                graphicsObject.TranslateTransform(this.Width, this.Height / 2);
                graphicsObject.RotateTransform(180);

                graphicsObject.DrawPolygon(blackPen, arrow);
                graphicsObject.FillPolygon(Brushes.Black, arrow);
                graphicsObject.ResetTransform();
                graphicsObject.TranslateTransform(0, this.Height / 2);

                drawstring(e,((int)alt).ToString("0 m"), new Font("Arial", 10), Brushes.AliceBlue, scrollbg.Left + 10, -9);
                graphicsObject.ResetTransform();

                // mode and wp dist and wp

                drawstring(e,mode, new Font("Arial", fontsize), whiteBrush, scrollbg.Left - 30, scrollbg.Bottom + 5);
                drawstring(e,(int)disttowp + ">" + wpno, new Font("Arial", fontsize), whiteBrush, scrollbg.Left - 30, scrollbg.Bottom + fontsize + 2 + 10);

                // battery

                graphicsObject.ResetTransform();

                drawstring(e,resources.GetString("Bat"), new Font("Arial", fontsize + 2), whiteBrush, fontsize, this.Height - 30 - fontoffset);
                drawstring(e,batterylevel.ToString("0.00v"), new Font("Arial", fontsize + 2), whiteBrush, fontsize * 4, this.Height - 30 - fontoffset);
                drawstring(e,batteryremaining.ToString("0%"), new Font("Arial", fontsize + 2), whiteBrush, fontsize * 9, this.Height - 30 - fontoffset);

                // gps

                string gps = "";

                if (gpsfix == 0) {
                    gps = resources.GetString("GPS: No Fix.Text");
                } else if (gpsfix == 1) {
                    gps = resources.GetString("GPS: No Fix.Text");
                } else if (gpsfix == 2) {
                    gps = resources.GetString("GPS: 2D Fix.Text");
                } else if (gpsfix == 3) {
                    gps = resources.GetString("GPS: 3D Fix.Text");
                }

                drawstring(e,gps, new Font("Arial", fontsize + 2), whiteBrush, this.Width - 10 * fontsize, this.Height - 30 - fontoffset);

            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());            
            }

            //            Console.WriteLine("HUD " + (DateTime.Now - starttime).TotalMilliseconds);
        }

        void drawstring(PaintEventArgs e, string text, Font font, Brush brush, float x, float y)
        {
            GraphicsPath pth = new GraphicsPath();

            /*
            SolidBrush brush1 = new SolidBrush(Color.FromArgb(0x26, 0x27, 0x28));

            float fontscale = font.Size / 12;

            // shadow
            e.Graphics.DrawString(text, font, brush1, x + fontscale, y + fontscale);

            // text
            e.Graphics.DrawString(text,font,brush,x,y);

            */
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            pth.AddString(text, font.FontFamily, 0, font.Size + 5, new Point((int)x, (int)y), StringFormat.GenericTypographic);

            //pen size is the size of the edge

            Pen P = new Pen(Color.FromArgb(0x26, 0x27, 0x28), 2f);

            // drop shadow brush

            

            // drop shadow
            //e.Graphics.FillPath(brush1, pth1);

            //Draw the edge

            e.Graphics.DrawPath(P, pth);

            //Draw the face

            e.Graphics.FillPath(brush, pth);

            pth.Dispose();
            
        }

        protected override void OnResize(EventArgs e)
        {
            this.Height = (int)(this.Width / 1.333f);
            base.OnResize(e);
        }
    }
}
