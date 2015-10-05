namespace Flambe
{
    using System.Diagnostics;
    using System.Windows.Forms;

    /// <summary>
    /// A dialog used for downloading recipes from flambe.dingostick.com/recipes
    /// </summary>
    public partial class DownloadDialog : Form
    {
        public DownloadDialog()
        {
            InitializeComponent();
        }

        private void tbRecipeId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                btnDownload.PerformClick();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://flambe.dingostick.com/recipes");
        }
    }
}
