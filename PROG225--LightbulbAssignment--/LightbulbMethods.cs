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
            foreach (string s in Directory.GetFiles("./LightBulbs"))
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

        
        
    }

    internal class Lightbulb
    {
        internal PictureBox pbLightbulb = new PictureBox();
        internal int currentIndex = 0;
        private int currentBrightness = 0;
        internal System.Timers.Timer timer;

        internal Lightbulb(int x, int y)
        {
            pbLightbulb = LightbulbMethods.CreateLightbulb(x, y, 0);
            Form1.MainForm.Controls.Add(pbLightbulb);

            timer = new System.Timers.Timer(500);
            timer.Enabled = false;

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

            Button btnSlowBrighten = new Button
            {
                Name = "btnSlowBrighten",
                Left = x + 50,
                Top = y + 140,
                Height = 23,
                Width = 50,
                Text = "++"
            };

            Button btnSlowDim = new Button
            {
                Name = "btnSlowDim",
                Left = x + 50,
                Top = y + 140,
                Height = 23,
                Width = 50,
                Text = "--"
            };

            Form1.MainForm.Controls.Add(btnDim);
            Form1.MainForm.Controls.Add(btnBrighten);

            
        }

        private void btnDim_Click(object sender, EventArgs e)
        {
            if (currentIndex != 0)
            {
                LightbulbMethods.Dim(pbLightbulb, ref currentIndex);
            }
        }

        private void btnBrighten_Click(object sender, EventArgs e)
        {
            if (currentIndex != Form1.MainForm.LightbulbList.Count - 1)
            {
                LightbulbMethods.Brighten(pbLightbulb, ref currentIndex);
            }
        }

        private void SlowDim(Lightbulb LB)
        {
            LB.timer.Elapsed += (sender, e) => LightbulbMethods.Dim(LB.pbLightbulb, ref LB.currentIndex);
        }

        private void SlowBrighten(Lightbulb LB)
        {
            LB.timer.Elapsed += (sender, e) => LightbulbMethods.Brighten(LB.pbLightbulb, ref LB.currentIndex);
        }
    }
}
