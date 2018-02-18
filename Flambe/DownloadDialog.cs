namespace Flambe
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    /// <summary>
    /// A dialog used for downloading recipes from the official website
    /// </summary>
    public partial class DownloadDialog : Form
    {
        internal static readonly Regex RecipeUrl = new Regex(@"^https?://flambe.azurewebsites.net/recipe/([0-9a-f\-]+)$", RegexOptions.IgnoreCase);

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
            Process.Start("http://flambe.azurewebsites.net/search");
        }

        private void tbRecipeId_TextChanged(object sender, System.EventArgs e)
        {
            TextBox input = (TextBox)sender;

            // change http://flambe.azurewebsites.net/recipe/7d8722b2-9c94-4892-922a-01ff63292a5e to just the GUID
            if (RecipeUrl.IsMatch(input.Text))
            {
                input.Text = RecipeUrl.Match(input.Text).Groups[1].Value;
            }

            Guid recipeId;
            if (Guid.TryParse(input.Text, out recipeId))
            {
                input.BackColor = Color.LightGreen;
                btnDownload.Enabled = true;
            }
            else
            {
                input.BackColor = Color.LightPink;
                btnDownload.Enabled = false;
            }
        }
    }
}
