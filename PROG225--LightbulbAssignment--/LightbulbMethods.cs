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

        internal static PictureBox CreateLightbulbPictureBox(int x, int y, int imageIndex)
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

        internal static void OffLightbulb(ref PictureBox LightBulb)
        {
            List<Bitmap> lightbulbList = Form1.MainForm.LightbulbList;
            LightBulb.Image = lightbulbList[0];
        }

        internal static void SetLightbulb(ref PictureBox LightBulb, int index)
        {
            List<Bitmap> lightbulbList = Form1.MainForm.LightbulbList;
            LightBulb.Image = lightbulbList[index];
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

        internal static void SetLightbulbIndex(Lightbulb LB)
        {
            switch (LB.currentBrightness)
            {
                case 0:
                    LB.lightbulbIndex = 0; break;
                case <10:
                    LB.lightbulbIndex = 1; break;
                case <30:
                    LB.lightbulbIndex = 2; break;
                case <50:
                    LB.lightbulbIndex = 3; break;
                case <70:
                    LB.lightbulbIndex = 4; break;
                case <90:
                    LB.lightbulbIndex = 5; break;
                case <100:
                    LB.lightbulbIndex = 6; break;
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
            pbLightbulb = LightbulbMethods.CreateLightbulbPictureBox(x, y, 0);
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
            btnSlowDim.Click += (sender, e) => { dimTimer.Enabled = true; brightenTimer.Enabled = false; };

            Button btnSlowBrighten = new Button
            {
                Name = "btnSlowBrighten",
                Left = x + 50,
                Top = y + 140,
                Height = 23,
                Width = 50,
                Text = "++"
            };
            btnSlowBrighten.Click += (sender, e) => { dimTimer.Enabled = false; brightenTimer.Enabled = true; };

            Form1.MainForm.Controls.Add(btnDim);
            Form1.MainForm.Controls.Add(btnBrighten);
            Form1.MainForm.Controls.Add(btnSlowDim);
            Form1.MainForm.Controls.Add(btnSlowBrighten);
        }

        private void DimTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            UpdateLightbulbBrightness();
            if (currentBrightness == 0)
            {
                //LightbulbMethods.OffLightbulb(ref pbLightbulb);
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
            UpdateLightbulbBrightness();
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

        private void UpdateLightbulbBrightness()
        {
            LightbulbMethods.SetCurrentBrightness(this);
        }

        private void btnDim_Click(object sender, EventArgs e)
        {
            Dim(ref lightbulbIndex);
        }

        private void btnBrighten_Click(object sender, EventArgs e)
        {
            Brighten(ref lightbulbIndex);
        }

        private void Brighten(ref int currentIndex)
        {
            if(currentIndex != Form1.MainForm.LightbulbList.Count - 1)
            {
                currentIndex += 1;
                pbLightbulb.Invoke(new Action(() => {
                    pbLightbulb.Image = Form1.MainForm.LightbulbList[lightbulbIndex];       //Some help from chatgpt, was running into a cross threaded exception. Invoke tells the UI thread to do stuff instead of the current thread.
                }));
            }
        }

        private void Dim(ref int currentIndex)
        {
            if(currentIndex != 0)
            {
                currentIndex -= 1;
                pbLightbulb.Invoke(new Action(() => {
                    pbLightbulb.Image = Form1.MainForm.LightbulbList[lightbulbIndex];
                }));
            }
        }
    }
}
