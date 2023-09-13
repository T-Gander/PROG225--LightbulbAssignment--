using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PROG225__LightbulbAssignment__
{
    internal class LightbulbFormMethods
    {
        public static List<Bitmap> MyLightbulbImages = new List<Bitmap>();  

        internal static List<Bitmap> LoadImages()
        {
            foreach (string s in Directory.GetFiles("../../../LightBulbs"))
            {
                if (File.Exists(s))
                {
                    if (s.Contains(".bmp"))
                    {
                        MyLightbulbImages.Add(new Bitmap(s));
                    }
                }
            }

            return MyLightbulbImages;
        }

        internal static PictureBox CreateLightbulbPictureBox(int x, int y, Lightbulb.LightBulbState Brightness)
        {
            PictureBox Lightbulb = new PictureBox 
            { 
                Left = x,
                Top = y, 
                Height = 120,
                Width = 100,
                SizeMode = PictureBoxSizeMode.AutoSize, 
                Image = Form1.MainForm.LightbulbList[(int)Brightness] 
            };

            return Lightbulb;
        }
    }

    internal class Lightbulb
    {
        internal enum LightBulbState { OFF, VDIM, DIM, HALF, BRIGHT, VBRIGHT, ON }

        private LightBulbState Brightness = LightBulbState.OFF;

        private PictureBox pbLightbulb = new PictureBox();
        private int lightbulbIndex = 0;
        private int lumens = 0;
        public string Name { get { return _name; } set { _name = value; } }
        private string _name = "";

        private System.Timers.Timer dimTimer;
        private System.Timers.Timer brightenTimer;

        internal Lightbulb(int x, int y)
        {
            pbLightbulb = LightbulbFormMethods.CreateLightbulbPictureBox(x, y, LightBulbState.OFF);
            Form1.MainForm.Controls.Add(pbLightbulb);

            dimTimer = new System.Timers.Timer(500);
            dimTimer.Elapsed += DimTimer_Elapsed;
            dimTimer.Enabled = false;

            brightenTimer = new System.Timers.Timer(500);
            brightenTimer.Elapsed += BrightenTimer_Elapsed;
            brightenTimer.Enabled = false;

            TextBox txtName = new TextBox
            {
                Name = "txtName",
                Left = x,
                Top = y - 60,
                Height = 23,
                Width = 60,
                Text = Name
            };

            Label lblName = new Label
            {
                Name = "lblName",
                Left = x,
                Top = y - 30,
                Height = 23,
                Width = 100,
                Text = ""
            };

            Button btnAddName = new Button
            {
                Name = "btnAddName",
                Left = x + 65,
                Top = y - 60,
                Height = 23,
                Width = 40,
                Text = "Set Name"
            };
            btnAddName.Click += (sender, e) => { lblName.Text = txtName.Text; txtName.Text = ""; };

            Button btnDim = new Button
            {
                Name = "btnDim",
                Left = x,
                Top = y + 140,
                Height = 23,
                Width = 50,
                Text = "-"
            };
            btnDim.Click += btnDim_Click;

            Button btnBrighten = new Button
            {
                Name = "btnBrighten",
                Left = x + 50,
                Top = y + 140,
                Height = 23,
                Width = 50,
                Text = "+"
            };
            btnBrighten.Click += btnBrighten_Click;

            Button btnSlowDim = new Button
            {
                Name = "btnSlowDim",
                Left = x,
                Top = y + 170,
                Height = 23,
                Width = 50,
                Text = "--"
            };
            btnSlowDim.Click += (sender, e) => { dimTimer.Enabled = true; brightenTimer.Enabled = false; }; //Creates an anonymous function using lambda (could also put into a seperate event like above)

            Button btnSlowBrighten = new Button
            {
                Name = "btnSlowBrighten",
                Left = x + 50,
                Top = y + 170,
                Height = 23,
                Width = 50,
                Text = "++"
            };
            btnSlowBrighten.Click += (sender, e) => { dimTimer.Enabled = false; brightenTimer.Enabled = true; };

            Form1.MainForm.Controls.Add(txtName);
            Form1.MainForm.Controls.Add(lblName);
            Form1.MainForm.Controls.Add(btnAddName);
            Form1.MainForm.Controls.Add(btnDim);
            Form1.MainForm.Controls.Add(btnBrighten);
            Form1.MainForm.Controls.Add(btnSlowDim);
            Form1.MainForm.Controls.Add(btnSlowBrighten);
            
        }

        private void DimTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            UpdateLightbulbBrightness();
            if (lumens == 0)
            {
                dimTimer.Enabled = false;
            }
            else
            {
                lumens -= 5;
                Dim(ref Brightness);
            }
        }

        private void BrightenTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            UpdateLightbulbBrightness();
            if (lumens == 100)
            {
                brightenTimer.Enabled = false;
            }
            else
            {
                lumens += 5;
                Brighten(ref Brightness);
            }
        }

        private void UpdateLightbulbBrightness()
        {
            SetLumens();
        }

        private void btnDim_Click(object sender, EventArgs e)
        {
            Dim(ref Brightness);
        }

        private void btnBrighten_Click(object sender, EventArgs e)
        {
            Brighten(ref Brightness);
        }

        private void Brighten(ref LightBulbState Brightness)
        {
            if(Brightness != LightBulbState.ON)
            {
                Brightness += 1;
                int brightnessIndex = (int)Brightness;
                pbLightbulb.Invoke(new Action(() => {
                    pbLightbulb.Image = Form1.MainForm.LightbulbList[brightnessIndex];       //Some help from chatgpt, was running into a cross threaded exception. Fixed by using Invoke. Invoke tells the UI thread to do stuff instead of the current thread.
                }));
            }
        }

        private void Dim(ref LightBulbState Brightness)
        {
            if(Brightness != LightBulbState.OFF)
            {
                Brightness -= 1;
                int brightnessIndex = (int)Brightness;
                pbLightbulb.Invoke(new Action(() => {
                    pbLightbulb.Image = Form1.MainForm.LightbulbList[brightnessIndex];
                }));
            }
        }

        private void SetLumens()
        {
            switch ((int)Brightness)
            {
                case 1:
                    lumens = 10; break;
                case 2:
                    lumens = 30; break;
                case 3:
                    lumens = 50; break;
                case 4:
                    lumens = 70; break;
                case 5:
                    lumens = 90; break;
                case 6:
                    lumens = 100; break;
            }
        }
    }
}
