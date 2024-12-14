namespace AoCForms
{
    public partial class RobotsDisplayForm : Form
    {
        public RobotsDisplayForm()
        {
            InitializeComponent();
        }

        public void Draw(IEnumerable<(int x, int y, int dx, int dy)> robots)
        {
            using Graphics canvas = pictureBox.CreateGraphics();
            Pen pen = new(Brushes.Black);
            foreach (var (x, y, _, _) in robots)
            {
                canvas.DrawRectangle(pen, x * 8, y * 8, 8, 8);
            }
        }
    }
}
