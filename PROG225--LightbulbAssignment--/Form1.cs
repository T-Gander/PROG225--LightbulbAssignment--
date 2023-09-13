namespace PROG225__LightbulbAssignment__
{
    public partial class Form1 : Form
    {
        public static Form1 MainForm;

        internal PictureBox MainLightbulb = new PictureBox { Left = 100, Top = 100, SizeMode = PictureBoxSizeMode.AutoSize };

        internal List<Bitmap> LightbulbBitmapList;

        private List<Lightbulb> MyLightbulbs = new List<Lightbulb>();

        private int currentX = 100;

        private int currentY = 100;

        private int numberOfLightbulbs = 0;

        public Form1()
        {
            InitializeComponent();
            MainForm = this;
            LightbulbBitmapList = LightbulbFormMethods.LoadImages();
        }

        private void btnCreateLightbulb_Click(object sender, EventArgs e)
        {
            numberOfLightbulbs++;
            if (numberOfLightbulbs < 6)
            {
                Lightbulb newLightbulb = new Lightbulb(currentX, currentY);
                MyLightbulbs.Add(newLightbulb);
                currentX += 120;
            }
        }

        private void btnAllOn_Click(object sender, EventArgs e)
        {
            MyLightbulbs.ForEach(Lightbulb => Lightbulb.DimTimer.Enabled = false);
            MyLightbulbs.ForEach(Lightbulb => Lightbulb.BrightenTimer.Enabled = true);
        }

        private void btnAllOff_Click(object sender, EventArgs e)
        {
            MyLightbulbs.ForEach(Lightbulb => Lightbulb.BrightenTimer.Enabled = false);
            MyLightbulbs.ForEach(Lightbulb => Lightbulb.DimTimer.Enabled = true);
        }
    }
}