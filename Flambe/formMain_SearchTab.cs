namespace Flambe
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    /// <summary>
    /// Events relating to the Search tab of the main window
    /// </summary>
    public partial class formMain
    {
        private void lvAllRecipes_DoubleClick(object sender, EventArgs e)
        {
            var item = lvAllRecipes.SelectedItems[0];
            currentRecipe = item.Tag as Recipe;
            currentRecipe.LoadChildren();

            // load the recipe for easy editing, too
            EditRecipe(currentRecipe, switchTabs: false);

            webBrowser1.DocumentText = currentRecipe.ToHtml();
            tabControl.SelectedIndex = 1;
        }

        private void lvAllRecipes_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var cm = new ContextMenu();

                var recipe = lvAllRecipes.SelectedItems[0].Tag as Recipe;
                recipe.LoadChildren();

                var mi = new MenuItem("Open re&cipe", lvAllRecipes_DoubleClick) { Tag = recipe };
                cm.MenuItems.Add(mi);

                mi = new MenuItem("P&rint recipe") { Tag = recipe };
                cm.MenuItems.Add(mi);

                mi = new MenuItem("&Edit recipe") { Tag = recipe };
                mi.Click += new EventHandler((obj, args) => EditRecipe(recipe));
                cm.MenuItems.Add(mi);

                mi = new MenuItem("Uplo&ad recipe") { Tag = recipe };
                cm.MenuItems.Add(mi);

                mi = new MenuItem("&Delete recipe") { Tag = recipe };
                mi.Click += new EventHandler((obj, args) =>
                {
                    var taggedRecipe = ((MenuItem)obj).Tag as Recipe;
                    var choice = MessageBox.Show("Are you sure you want to delete this recipe?", "Delete recipe?", MessageBoxButtons.YesNo);
                    if (choice == System.Windows.Forms.DialogResult.Yes)
                    {
                        for (int i = 0; i < lvAllRecipes.Items.Count; i++)
                        {
                            if (lvAllRecipes.Items[i].Tag == taggedRecipe)
                            {
                                lvAllRecipes.Items.RemoveAt(i);
                                break;
                            }
                        }

                        taggedRecipe.Delete();
                    }
                });
                cm.MenuItems.Add(mi);

                mi = new MenuItem("&New recipe");
                mi.Click += new EventHandler((obj, args) =>
                {
                    ClearRecipe();
                    tabControl.SelectedIndex = 2;
                    tbName.Focus();
                });
                cm.MenuItems.Add(mi);
                ContextMenu = cm;

                mi = new MenuItem("Download Recipe");
                mi.Click += new EventHandler(async (obj, args) =>
                {
                    var dd = new DownloadDialog();
                    if (dd.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }

                    var downloadStats = await Recipe.DownloadRecipe(dd.tbRecipeId.Text);

                    if (downloadStats.Item1 > 0)
                    {
                        displayRecipes();

                        if (downloadStats.Item1 != downloadStats.Item2)
                        {
                            var msg = string.Format("{0} out of {1} downloads succeeded", downloadStats.Item1, downloadStats.Item2);
                            MessageBox.Show(msg, "Download partially successful");
                        }
                        else
                        {
                            var msg = string.Format("{0} download{1} succeeded", downloadStats.Item1, downloadStats.Item1 == 1 ? string.Empty : "s");
                            MessageBox.Show(msg, "Download successful");
                        }
                    }
                    else
                    {
                        var msg = string.Format("Download failed. Please check the id(s) of the recipe(s) you want to download.");
                        MessageBox.Show(msg, "Download failed");
                    }
                });
                cm.MenuItems.Add(mi);

                ////mi = new MenuItem("To &JSON") { Tag = recipe };
                ////mi.Click += new EventHandler((obj, args) =>
                ////{
                ////    var json = recipe.ToJson();
                ////    MessageBox.Show(json);
                ////});
                ////cm.MenuItems.Add(mi);
            }
        }

        private void lvAllRecipes_Resize(object sender, EventArgs e)
        {
            ////var totalWidth = (lvAllRecipes.Width - 30);

            ////lvAllRecipes.Columns["name"].Width = totalWidth / 3;
            ////lvAllRecipes.Columns["category"].Width = totalWidth / 3;
            ////lvAllRecipes.Columns["cuisine"].Width = totalWidth / 3;
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                // enter -- do the search
                displayRecipes(tbSearch.Text.Trim());
            }
            else if (e.KeyCode == Keys.Escape)
            {
                // esc -- clear the text
                tbSearch.Text = string.Empty;
                displayRecipes();
            }
        }

        private void lvAllRecipes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lvAllRecipes.SelectedItems.Count > 0)
            {
                var recipe = lvAllRecipes.SelectedItems[0].Tag as Recipe;
                if (Control.ModifierKeys == Keys.Shift
                    || MessageBox.Show("Are you sure you want to delete '" + recipe.Name + "' from your database?", "Delete recipe?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    recipe.Delete();
                    displayRecipes();
                }
            }
        }

        private void lvAllRecipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAllRecipes.SelectedItems.Count == 0)
            {
                return;
            }

            var recipe = lvAllRecipes.SelectedItems[0].Tag as Recipe;
            statusStrip1.Items[0].Text = recipe.Name + (string.IsNullOrEmpty(recipe.Credit) ? string.Empty : " by " + recipe.Credit);
        }
    }
}
