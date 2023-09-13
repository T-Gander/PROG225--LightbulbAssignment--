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
        private Label lblLightbulbState;
        private PictureBox pbLightbulb = new PictureBox();
        
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

            Label lblLightbulbState = new Label
            {
                Name = "lblLightbulbState",
                Left = x,
                Top = y + 115,
                Height = 23,
                Width = 100,
                Text = Brightness.ToString()
            };
            this.lblLightbulbState = lblLightbulbState;

            Button btnDim = new Button
            {
                Name = "btnDim",
                Left = x,
                Top = y + 140,
                Height = 23,
                Width = 50,
                Text = "-"
            };
            btnDim.Click += (sender, e) => Dim(ref Brightness);

            Button btnBrighten = new Button
            {
                Name = "btnBrighten",
                Left = x + 50,
                Top = y + 140,
                Height = 23,
                Width = 50,
                Text = "+"
            };
            btnBrighten.Click += (sender, e) => Brighten(ref Brightness); ;

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
            Form1.MainForm.Controls.Add(lblLightbulbState);
            Form1.MainForm.Controls.Add(btnDim);
            Form1.MainForm.Controls.Add(btnBrighten);
            Form1.MainForm.Controls.Add(btnSlowDim);
            Form1.MainForm.Controls.Add(btnSlowBrighten);
            
        }

        private void DimTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            SetLumens();
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

        private string UpdateLightbulbStateText()
        {
            string result = "OFF";
            switch(Brightness)
            {
                case LightBulbState.VDIM:
                    return "VDIM";
                case LightBulbState.DIM:
                    return "DIM";
                case LightBulbState.HALF:
                    return "HALF";
                case LightBulbState.BRIGHT:
                    return "BRIGHT";
                case LightBulbState.VBRIGHT:
                    return "VBRIGHT";
                case LightBulbState.ON:
                    return "ON";
            }
            return result;
        }

        private void BrightenTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            SetLumens();
            if (lumens == 100)
            {
                lblLightbulbState.Text = UpdateLightbulbStateText();
                brightenTimer.Enabled = false;
            }
            else
            {
                lumens += 5;
                Brighten(ref Brightness);
                lblLightbulbState.Text = UpdateLightbulbStateText();
            }
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
                    lblLightbulbState.Text = UpdateLightbulbStateText();
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
                    lblLightbulbState.Text = UpdateLightbulbStateText();
                }));
                
            }
        }

        private void SetLumens()
        {
            switch ((int)Brightness)
            {
                case 0:
                    lumens = 0; Brightness = LightBulbState.OFF; break;
                case 1:
                    lumens = 10; Brightness = LightBulbState.VDIM;  break;
                case 2:
                    lumens = 30; Brightness = LightBulbState.DIM; break;
                case 3:
                    lumens = 50; Brightness = LightBulbState.HALF; break;
                case 4:
                    lumens = 70; Brightness = LightBulbState.BRIGHT; break;
                case 5:
                    lumens = 90; Brightness = LightBulbState.VBRIGHT; break;
                case 6:
                    lumens = 100; Brightness = LightBulbState.ON; break;
            }
        }
    }
}
