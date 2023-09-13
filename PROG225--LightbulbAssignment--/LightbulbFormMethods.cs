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
        internal static List<Bitmap> LoadImages()
        {
            List<Bitmap> MyLightbulbImages = new List<Bitmap>();

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
                Image = LightbulbForms.MainForm.LightbulbBitmapList[(int)Brightness] 
            };

            return Lightbulb;
        }
    }

    internal class Lightbulb
    {
        internal enum LightBulbState { OFF, VDIM, DIM, HALF, BRIGHT, VBRIGHT, ON }

        private LightBulbState Brightness = LightBulbState.OFF;
        private Label lblLumensValue;
        private Label lblLightbulbState;
        private PictureBox pbLightbulb = new PictureBox();

        public int Lumens { get { return _lumens; } set { _lumens = value; }}
        private int _lumens = 0;    //Is this needed or should I just use the get; set; shortcut? (needs researching)

        public string Name { get { return _name; } set { _name = value; } }
        private string _name = "";

        internal System.Timers.Timer DimTimer;
        internal System.Timers.Timer BrightenTimer;

        internal Lightbulb(int x, int y)
        {
            pbLightbulb = LightbulbFormMethods.CreateLightbulbPictureBox(x, y, LightBulbState.OFF);
            LightbulbForms.MainForm.Controls.Add(pbLightbulb);

            DimTimer = new System.Timers.Timer(100);
            DimTimer.Elapsed += DimTimer_Elapsed;
            DimTimer.Enabled = false;

            BrightenTimer = new System.Timers.Timer(100);
            BrightenTimer.Elapsed += BrightenTimer_Elapsed;
            BrightenTimer.Enabled = false;

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

            Label lblLumensValue = new Label
            {
                Name = "lblLumensValue",
                Left = x,
                Top = y + 135,
                Height = 23,
                Width = 100,
                Text = Lumens.ToString()
            };
            this.lblLumensValue = lblLumensValue;

            Button btnDim = new Button
            {
                Name = "btnDim",
                Left = x,
                Top = y + 160,
                Height = 23,
                Width = 50,
                Text = "-"
            };
            btnDim.Click += (sender, e) => Dim(ref Brightness);

            Button btnBrighten = new Button
            {
                Name = "btnBrighten",
                Left = x + 50,
                Top = y + 160,
                Height = 23,
                Width = 50,
                Text = "+"
            };
            btnBrighten.Click += (sender, e) => Brighten(ref Brightness); ;

            Button btnSlowDim = new Button
            {
                Name = "btnSlowDim",
                Left = x,
                Top = y + 190,
                Height = 23,
                Width = 50,
                Text = "--"
            };
            btnSlowDim.Click += (sender, e) => { DimTimer.Enabled = true; BrightenTimer.Enabled = false; }; //Creates an anonymous function using lambda (could also put into a seperate event like above)

            Button btnSlowBrighten = new Button
            {
                Name = "btnSlowBrighten",
                Left = x + 50,
                Top = y + 190,
                Height = 23,
                Width = 50,
                Text = "++"
            };
            btnSlowBrighten.Click += (sender, e) => { DimTimer.Enabled = false; BrightenTimer.Enabled = true; };

            LightbulbForms.MainForm.Controls.Add(txtName);
            LightbulbForms.MainForm.Controls.Add(lblName);
            LightbulbForms.MainForm.Controls.Add(btnAddName);
            LightbulbForms.MainForm.Controls.Add(lblLightbulbState);
            LightbulbForms.MainForm.Controls.Add(lblLumensValue);
            LightbulbForms.MainForm.Controls.Add(btnDim);
            LightbulbForms.MainForm.Controls.Add(btnBrighten);
            LightbulbForms.MainForm.Controls.Add(btnSlowDim);
            LightbulbForms.MainForm.Controls.Add(btnSlowBrighten);
            
        }
        private string UpdateLightbulbStateText()
        {
            string result = "OFF";
            switch (Brightness)
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

        private void DimTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (Lumens == 0)
            {
                UpdateUI();
                DimTimer.Enabled = false;
            }
            else
            {
                Dim(ref Brightness);
                UpdateUI();
            }
        }

        private void BrightenTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (Lumens == 100)
            {
                UpdateUI();
                BrightenTimer.Enabled = false;
            }
            else
            {
                Brighten(ref Brightness);
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            int brightnessIndex = (int)Brightness;
            pbLightbulb.Invoke(new Action(() => {
                pbLightbulb.Image = LightbulbForms.MainForm.LightbulbBitmapList[brightnessIndex];       //Some help from chatgpt, was running into a cross threaded exception. Fixed by using Invoke. Invoke tells the UI thread to do stuff instead of the current thread.
                lblLightbulbState.Text = UpdateLightbulbStateText();
                lblLumensValue.Text = Lumens.ToString();
            }));
        }

        private void Brighten(ref LightBulbState Brightness)
        {
            if(Brightness != LightBulbState.ON)
            {
                Lumens += 5;
                CheckBrightness();
                UpdateUI();
            }
        }

        private void Dim(ref LightBulbState Brightness)
        {
            if(Brightness != LightBulbState.OFF)
            {
                Lumens -= 5;
                CheckBrightness();
                UpdateUI();
            }
        }

        private void CheckBrightness()
        {
            switch (Lumens)
            {
                case 0:
                    Brightness = LightBulbState.OFF; break;
                case <= 15:
                    Brightness = LightBulbState.VDIM; break;
                case <= 40:
                    Brightness = LightBulbState.DIM; break;
                case <= 55:
                    Brightness = LightBulbState.HALF; break;
                case <= 70:
                    Brightness = LightBulbState.BRIGHT; break;
                case <= 95:
                    Brightness = LightBulbState.VBRIGHT; break;
                case 100:
                    Brightness = LightBulbState.ON; break;
            }
        }
    }
}
