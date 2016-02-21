namespace Flambe
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Events related to the Create tab of the main window
    /// </summary>
    public partial class formMain
    {
        private void RefreshIngredientList()
        {
            if (currentRecipe == null)
            {
                return;
            }

            lvIngredients.Items.Clear();
            foreach (var ingredient in currentRecipe.Ingredients)
            {
                var item = new ListViewItem(new[]
                {
                            ingredient.Quantity,
                            ingredient.Units,
                            ingredient.Item,
                            ingredient.Remarks,
                            ingredient.IsOptional ? "Y" : string.Empty
                })
                {
                    Tag = ingredient
                };
                lvIngredients.Items.Add(item);
            }
        }

        private void lvIngredients_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var cm = new ContextMenu();
                var ingredient = ((ListView)sender).SelectedItems[0].Tag as Ingredient;

                var mi = new MenuItem("Delete ingredient")
                {
                    Tag = ingredient
                };
                mi.Click += new EventHandler((obj, args) =>
                {
                    var response = MessageBox.Show("Are you sure you want to delete this ingredient?", "Delete ingredient?", MessageBoxButtons.YesNo);
                    if (response == System.Windows.Forms.DialogResult.Yes)
                    {
                        FlambeDB.DbConnection.Delete(ingredient);
                        lvIngredients.Items.RemoveAt(((ListView)sender).SelectedIndices[0]);
                    }
                });
                cm.MenuItems.Add(mi);

                mi = new MenuItem("Add ingredient");
                mi.Click += new EventHandler((obj, args) =>
                {
                    var i = new Ingredient(currentRecipe);
                });

                ContextMenu = cm;
            }
        }

        private void btnSaveRecipe_Click(object sender, EventArgs e)
        {
            if (currentRecipe == null)
            {
                currentRecipe = new Recipe();
            }

            currentRecipe.Name = tbName.Text;
            currentRecipe.Servings = tbServings.Text;
            currentRecipe.Cuisine = cbCuisine.Text;
            currentRecipe.Category = cbCategory.Text;
            currentRecipe.Credit = cbCredit.Text;
            currentRecipe.CookTime = tbCookTime.Text;
            currentRecipe.PrepTime = tbPrepTime.Text;
            currentRecipe.Comment = tbComment.Text;

            // TODO: ensure we don't bloat the Instructions table with orphaned rows
            currentRecipe.Instructions = tbInstructions.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select((t, i) => new Instruction(currentRecipe) { Text = t })
                .ToList();

            currentRecipe.Commit();

            // move back to the search page and refresh the list
            displayRecipes();
            tabControl.SelectedIndex = 0;
            ClearRecipe();
        }

        private void tbQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                tbUnits.Focus();
                tbQuantity.Text = tbQuantity.Text.Trim();
            }
        }

        private void allIngredients_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(tbItem.Text))
            {
                var t = new ToolTip();
                t.Show("'Item' is required", tbItem);

                return;
            }

            if (currentRecipe == null)
            {
                currentRecipe = new Recipe();
            }

            if (currentIngredient == null)
            {
                currentIngredient = new Ingredient(currentRecipe)
                {
                    Quantity = tbQuantity.Text.Trim(),
                    Units = tbUnits.Text.Trim(),
                    Item = tbItem.Text.Trim(),
                    Remarks = tbRemarks.Text.Trim(),
                    IsOptional = cbIsOptional.Checked,
                    IngredientOrder = currentRecipe.Ingredients.Count + 1,
                    RecipeId = currentRecipe.RecipeId,
                };

                currentRecipe.Ingredients.Add(currentIngredient);
            }
            else
            {
                currentIngredient.Quantity = tbQuantity.Text.Trim();
                currentIngredient.Units = tbUnits.Text.Trim();
                currentIngredient.Item = tbItem.Text.Trim();
                currentIngredient.Remarks = tbRemarks.Text.Trim();
                currentIngredient.IsOptional = cbIsOptional.Checked;
            }

            tbQuantity.Text = tbUnits.Text = tbItem.Text = tbRemarks.Text = string.Empty;
            currentIngredient = null;

            RefreshIngredientList();
        }

        private void lvIngredients_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvIngredients.SelectedItems.Count == 0)
            {
                tbQuantity.Text =
                    tbUnits.Text =
                    tbItem.Text =
                    tbRemarks.Text = string.Empty;
                cbIsOptional.Checked = false;

                currentIngredient = null;
            }
            else
            {
                var ingredient = lvIngredients.SelectedItems[0].Tag as Ingredient;

                tbQuantity.Text = ingredient.Quantity;
                tbUnits.Text = ingredient.Units;
                tbItem.Text = ingredient.Item;
                tbRemarks.Text = ingredient.Remarks;
                cbIsOptional.Checked = ingredient.IsOptional;

                currentIngredient = ingredient;
            }
        }

        private void lvIngredients_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Ingredient selectedIngredient = null;
                foreach (ListViewItem item in lvIngredients.Items)
                {
                    if (item.Bounds.Contains(new Point(e.X, e.Y)))
                    {
                        selectedIngredient = item.Tag as Ingredient;
                        break;
                    }
                }

                if (selectedIngredient == null)
                {
                    return;
                }

                var cm = new ContextMenu();
                var mi = new MenuItem("&Delete");
                mi.Click += new EventHandler((obj, args) =>
                {
                    currentRecipe.Ingredients.Remove(selectedIngredient);

                    RefreshIngredientList();
                });
                cm.MenuItems.Add(mi);


                ContextMenu = cm;
            }
        }
    }
}
