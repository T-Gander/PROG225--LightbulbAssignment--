using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PROG225__LightbulbAssignment__
{
    internal class LightbulbMethods
    {
        public int Lumens;

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

        internal static PictureBox CreateLightbulb(int x, int y, int imageIndex)
        {
            PictureBox Lightbulb = new PictureBox 
            { 
                Left = x,
                Top = y, 
                Height = 120,
                Width = 100,
                SizeMode = PictureBoxSizeMode.AutoSize, 
                Image = Form1.MainForm.LightbulbList[imageIndex] 
            };

            return Lightbulb;
        }

        internal static void Brighten(PictureBox LightBulb, ref int currentIndex)
        {
            currentIndex += 1;
            LightBulb.Image = Form1.MainForm.LightbulbList[currentIndex];
        }

        internal static void Dim(PictureBox LightBulb, ref int currentIndex)
        {
            currentIndex -= 1;
            LightBulb.Image = Form1.MainForm.LightbulbList[currentIndex];
        }

        internal static void DefaultLightbulb(List<Bitmap> pictureBoxes, ref PictureBox LightBulb)
        {
            LightBulb.Image = pictureBoxes[0];
        }

        internal static void SetLightbulb(ref PictureBox LightBulb, int state)
        {
            List<Bitmap> lightbulbList = Form1.MainForm.LightbulbList;
            LightBulb.Image = lightbulbList[state];
        }

        internal static void SetCurrentBrightness(Lightbulb LB)
        {
            switch (LB.lightbulbIndex)
            {
                case 1:
                    LB.currentBrightness = 10; break;
                case 2:
                    LB.currentBrightness = 30; break;
                case 3:
                    LB.currentBrightness = 50; break;
                case 4:
                    LB.currentBrightness = 70; break;
                case 5:
                    LB.currentBrightness = 90; break;
                case 6:
                    LB.currentBrightness = 100; break;
            }
        }
    }

    internal class Lightbulb
    {
        internal PictureBox pbLightbulb = new PictureBox();
        internal int lightbulbIndex = 0;
        internal int currentBrightness = 0;
        internal System.Timers.Timer dimTimer;
        internal System.Timers.Timer brightenTimer;

        internal Lightbulb(int x, int y)
        {
            pbLightbulb = LightbulbMethods.CreateLightbulb(x, y, 0);
            Form1.MainForm.Controls.Add(pbLightbulb);

            dimTimer = new System.Timers.Timer(500);
            dimTimer.Elapsed += DimTimer_Elapsed;
            dimTimer.Enabled = false;

            brightenTimer = new System.Timers.Timer(500);
            brightenTimer.Elapsed += BrightenTimer_Elapsed;
            brightenTimer.Enabled = false;

            Button btnDim = new Button
            {
                Name = "btnDim",
                Left = x,
                Top = y + 120,
                Height = 23,
                Width = 50,
                Text = "-"
            };
            btnDim.Click += btnDim_Click;

            Button btnBrighten = new Button
            {
                Name = "btnBrighten",
                Left = x + 50,
                Top = y + 120,
                Height = 23,
                Width = 50,
                Text = "+"
            };
            btnBrighten.Click += btnBrighten_Click;

            Button btnSlowDim = new Button
            {
                Name = "btnSlowDim",
                Left = x,
                Top = y + 140,
                Height = 23,
                Width = 50,
                Text = "--"
            };
            btnSlowDim.Click += (sender, e) => dimTimer.Enabled = true;

            Button btnSlowBrighten = new Button
            {
                Name = "btnSlowBrighten",
                Left = x + 50,
                Top = y + 140,
                Height = 23,
                Width = 50,
                Text = "++"
            };
            btnSlowBrighten.Click += (sender, e) => brightenTimer.Enabled = true;

            Form1.MainForm.Controls.Add(btnDim);
            Form1.MainForm.Controls.Add(btnBrighten);
            Form1.MainForm.Controls.Add(btnSlowDim);
            Form1.MainForm.Controls.Add(btnSlowBrighten);
        }

        private void DimTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            UpdateLightbulb();
            if (currentBrightness == 0)
            {
                dimTimer.Enabled = false;
            }
            else
            {
                currentBrightness -= 5;
                Dim(ref lightbulbIndex);
            }
        }

        private void BrightenTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            UpdateLightbulb();
            if (currentBrightness == 100)
            {
                brightenTimer.Enabled = false;
            }
            else
            {
                currentBrightness += 5;
                Brighten(ref lightbulbIndex);
            }
        }

        private void UpdateLightbulb()
        {
            LightbulbMethods.SetCurrentBrightness(this);
        }

        private void btnDim_Click(object sender, EventArgs e)
        {
            dimTimer.Enabled = true;
        }

        private void btnBrighten_Click(object sender, EventArgs e)
        {
            brightenTimer.Enabled = true;
        }

        private void Brighten(ref int currentIndex)
        {
            currentIndex += 1;
            pbLightbulb.Invoke(new Action(() => {
                pbLightbulb.Image = Form1.MainForm.LightbulbList[lightbulbIndex]; 
            }));
        }

        private void Dim(ref int currentIndex)
        {
            currentIndex -= 1;
            pbLightbulb.Invoke(new Action(() => {
                pbLightbulb.Image = Form1.MainForm.LightbulbList[lightbulbIndex];
            }));
        }
    }
}
