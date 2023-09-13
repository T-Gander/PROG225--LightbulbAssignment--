namespace PROG225__LightbulbAssignment__
{
    public partial class Form1 : Form
    {
        public static Form1 MainForm;

        internal PictureBox MainLightbulb = new PictureBox { Left = 100, Top = 100, SizeMode = PictureBoxSizeMode.AutoSize };

        internal List<Bitmap> LightbulbList;

        internal int LightbulbIndex = 0;

        private int currentX = 100;

        private int currentY = 100;

        private int numberOfLightbulbs = 0;

        public Form1()
        {
            InitializeComponent();
            MainForm = this;
            LightbulbList = LightbulbFormMethods.LoadImages();
        }

        private void btnCreateLightbulb_Click(object sender, EventArgs e)
        {
            numberOfLightbulbs++;
            if (numberOfLightbulbs < 6)
            {
                Lightbulb newLightbulb = new Lightbulb(currentX, currentY);
                currentX += 120;
            }
        }
    }
}