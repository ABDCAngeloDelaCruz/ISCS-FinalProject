namespace FinalProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool menuExpand = false;
        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                postContainer.Height += 10;
                if (postContainer.Height >= 166)
                {

                    menuTransition.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                postContainer.Height -= 10;
                if (postContainer.Height <= 48)
                {
                    menuTransition.Stop();
                    menuExpand = false;
                }
            }
        }

        private void posts_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
        }
        bool sidebarExpand = true;
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= 81)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= 223)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }
    }
}
