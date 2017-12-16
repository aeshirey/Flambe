namespace Flambe
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
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
            currentRecipe.LoadChildren(this.flambeConnection);

            // load the recipe for easy editing, too
            EditRecipe(currentRecipe, switchTabs: false);

            webBrowser1.DocumentText = currentRecipe.ToHtml();
            tabControl.SelectedIndex = 1;
        }

        private void lvAllRecipes_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                bool isShift = Control.ModifierKeys.HasFlag(Keys.Shift);

                var cm = new ContextMenu();

                Recipe selectedRecipe = null;
                foreach (ListViewItem item in lvAllRecipes.Items)
                {
                    if (item.Bounds.Contains(new Point(e.X, e.Y)))
                    {
                        selectedRecipe = item.Tag as Recipe;
                        break;
                    }
                }

                MenuItem mi;
                if (selectedRecipe != null)
                {
                    selectedRecipe.LoadChildren(this.flambeConnection);

                    mi = new MenuItem("Open re&cipe", lvAllRecipes_DoubleClick) { Tag = selectedRecipe };
                    cm.MenuItems.Add(mi);
                    
                    mi = new MenuItem("&Edit recipe") { Tag = selectedRecipe };
                    mi.Click += new EventHandler((obj, args) =>
                    {
                        EditRecipe(selectedRecipe);
                        tabControl.TabPages[2].Text = "Edit";
                    });
                    cm.MenuItems.Add(mi);

                    mi = new MenuItem("Uplo&ad recipe") { Tag = selectedRecipe };
                    mi.Click += new EventHandler((obj, args) =>
                    {
                        Guid? onlineId = selectedRecipe.Upload();
                        if (onlineId.HasValue)
                        {
                            Process.Start(string.Format(Recipe.CardUrl, onlineId.Value));
                        }
                    });
                    cm.MenuItems.Add(mi);

                    mi = new MenuItem("&Delete recipe") { Tag = selectedRecipe };
                    mi.Click += new EventHandler((obj, args) =>
                    {
                        var taggedRecipe = ((MenuItem)obj).Tag as Recipe;
                        if (isShift || DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this recipe?", "Delete recipe?", MessageBoxButtons.YesNo))
                        {
                            for (int i = 0; i < lvAllRecipes.Items.Count; i++)
                            {
                                if (lvAllRecipes.Items[i].Tag == taggedRecipe)
                                {
                                    lvAllRecipes.Items.RemoveAt(i);
                                    break;
                                }
                            }

                            taggedRecipe.Delete(this.flambeConnection);
                        }
                    });
                    cm.MenuItems.Add(mi);
                }

                mi = new MenuItem("&New recipe");
                mi.Click += new EventHandler((obj, args) =>
                {
                    EditRecipe(null);
                    tabControl.TabPages[2].Text = "Create";
                    tbName.Focus();
                });
                cm.MenuItems.Add(mi);
                ContextMenu = cm;

                mi = new MenuItem("Download Recipe");
                mi.Click += new EventHandler(async (obj, args) =>
                {
                    var dd = new DownloadDialog();
                    if (dd.ShowDialog(this) == DialogResult.Cancel)
                    {
                        return;
                    }

                    Guid recipeId;
                    if (!Guid.TryParse(dd.tbRecipeId.Text, out recipeId))
                    {
                        var msg = string.Format("The given recipe ID was not valid. Please confirm the ID and try again.");
                        MessageBox.Show(msg, "Invalid Recipe ID");
                    }

                    var downloadSuccess = await Recipe.DownloadRecipe(recipeId, this.flambeConnection);

                    if (downloadSuccess)
                    {
                        displayRecipes();

                        var msg = string.Format("Download succeeded");
                        MessageBox.Show(msg, "Download successful");
                    }
                    else
                    {
                        var msg = string.Format("Download failed. Please check the id of the recipe you want to download.");
                        MessageBox.Show(msg, "Download failed");
                    }
                });
                cm.MenuItems.Add(mi);

                if (Control.ModifierKeys == Keys.Control)
                {
                    if (selectedRecipe != null)
                    {
                        mi = new MenuItem("To &JSON") { Tag = selectedRecipe };
                        mi.Click += new EventHandler((obj, args) =>
                        {
                            var json = selectedRecipe.ToJson();
                            MessageBox.Show(json);
                        });
                        cm.MenuItems.Add(mi);
                    }

                    mi = new MenuItem("Vac&uum Database");
                    mi.Click += new EventHandler((obj, args) =>
                    {
                        this.flambeConnection.Execute("VACUUM");
                    });
                    cm.MenuItems.Add(mi);
                }
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
                    recipe.Delete(this.flambeConnection);
                    displayRecipes();
                }
            }
        }

        private void lvAllRecipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAllRecipes.SelectedItems.Count == 0)
            {
                tabControl.TabPages[2].Text = "Create";
                EditRecipe(null, false);
                return;
            }

            EditRecipe(lvAllRecipes.SelectedItems[0].Tag as Recipe, false);
            tabControl.TabPages[2].Text = "Edit";
            var recipe = lvAllRecipes.SelectedItems[0].Tag as Recipe;
            statusStrip1.Items[0].Text = recipe.Name + (string.IsNullOrEmpty(recipe.Credit) ? string.Empty : " by " + recipe.Credit);
        }
    }
}
