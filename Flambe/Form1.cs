namespace Flambe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    /// <summary>
    /// Flambe's core GUI logic
    /// </summary>
    public partial class formMain : Form
    {
        private Recipe currentRecipe;
        private Ingredient currentIngredient;

        #region Initialization
        public formMain()
        {
            InitializeComponent();

            lvAllRecipes.View = View.Details;
            var totalWidth = lvAllRecipes.Width - 30;
            lvAllRecipes.Columns.Add("name", "Name", totalWidth / 3);
            lvAllRecipes.Columns.Add("category", "Category", totalWidth / 3);
            lvAllRecipes.Columns.Add("cuisine", "Cuisine", totalWidth / 3);

            lvIngredients.View = View.Details;
            lvIngredients.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvIngredients.Columns.Add("quantity", "Qty");
            lvIngredients.Columns.Add("units", "Units");
            lvIngredients.Columns.Add("item", "Item");
            lvIngredients.Columns.Add("remarks", "Remarks");
            lvIngredients.Columns.Add("optional", "Optional");

            lvIngredients.Resize += new EventHandler((obj, args) =>
            {
                var availableWidth = lvIngredients.Width - 20;
                lvIngredients.Columns[4].Width = cbIsOptional.Width;
                availableWidth -= cbIsOptional.Width;

                lvIngredients.Columns[0].Width = tbQuantity.Width = availableWidth / 10;
                lvIngredients.Columns[1].Width = tbUnits.Width = availableWidth / 10;
                lvIngredients.Columns[2].Width = tbItem.Width = availableWidth * 2 / 5;
                lvIngredients.Columns[3].Width = tbRemarks.Width = availableWidth * 2 / 5;

                tbUnits.Left = tbQuantity.Left + tbQuantity.Width + 3;
                tbItem.Left = tbUnits.Left + tbUnits.Width + 3;
                tbRemarks.Left = tbItem.Left + tbItem.Width + 3;
            });

            FlambeDB.LoadDB();

            displayRecipes();
            initComboBoxes();
            initStatusStrip();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            FlambeDB.CloseDB();
            base.OnClosing(e);
        }

        /// <summary>
        /// Initializes all ComboBoxes to include a set of distinct values known from the database
        /// </summary>
        private void initComboBoxes()
        {
            var credits = new HashSet<string>();
            var cuisines = new HashSet<string>();
            var categories = new HashSet<string>();

            foreach (var recipe in FlambeDB.DbConnection.Table<Recipe>())
            {
                credits.Add(recipe.Credit);
                cuisines.Add(recipe.Cuisine);
                categories.Add(recipe.Category);
            }

            cbCredit.Items.AddRange(credits.OrderBy(c => c).ToArray());
            cbCuisine.Items.AddRange(cuisines.OrderBy(c => c).ToArray());
            cbCategory.Items.AddRange(categories.OrderBy(c => c).ToArray());
        }

        private void initStatusStrip()
        {
            // for storing the recipe name or whatever
            statusStrip1.Items.Add(new ToolStripStatusLabel(string.Empty));
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Query the DB for recipes based on user input
        /// </summary>
        /// <param name="query">The query to use. If blank, grab all recipes; else, the human-readable query such as "name:chicken cuisine:thai rating:>3"</param>
        private void displayRecipes(string query = null)
        {
            if (string.IsNullOrEmpty(query))
            {
                query = "SELECT * FROM Recipe";
            }
            else
            {
                // try to build a query from input such as "cuisine:indian rating:4"
                var conditions = new List<string>();
                foreach (var term in GetSearchTerms(query))
                {
                    if (term.Item1 == "rating")
                    {
                        float rating;
                        if (float.TryParse(term.Item2, out rating))
                        {
                            conditions.Add("rating >= " + rating.ToString());
                        }
                    }
                    else
                    {
                        conditions.Add(string.Format("{0} LIKE '%{1}%'", term.Item1, term.Item2.Replace("'", "''")));
                    }
                }

                if (conditions.Count > 0)
                {
                    query = "SELECT * FROM Recipe WHERE " + string.Join(" AND ", conditions);
                }
            }

            lvAllRecipes.Items.Clear();
            foreach (var recipe in FlambeDB.DbConnection.Query<Recipe>(query))
            {
                var item = new ListViewItem(new[]
                {
                    recipe.Name,
                    recipe.Category,
                    recipe.Cuisine
                })
                {
                    Tag = recipe
                };
                item.BackColor = lvAllRecipes.Items.Count % 2 == 0 ? Color.WhiteSmoke : Color.White;

                lvAllRecipes.Items.Add(item);
            }

            var numRecipes = this.lvAllRecipes.Items.Count;
            statusLabelGeneralPurpose.Text = numRecipes == 1 ? "Displaying 1 recipe" : "Displaying " + numRecipes.ToString() + " recipes";
        }

        /// <summary>
        /// Parses a raw "query string" into key-value pairs
        /// </summary>
        /// <param name="query">A human-readable query; e.g., cuisine:indian credit:"jane smith"</param>
        /// <returns>The parsed components of the query; e.g., (cuisine,indian), (credit,jane smith)</returns>
        private IEnumerable<Tuple<string, string>> GetSearchTerms(string query)
        {
            const string Pattern = @"([a-z]+):(""?)([^""]+?)\b\2";

            query = query.Trim().ToLowerInvariant();
            var matches = Regex.Matches(query, Pattern);
            foreach (Match match in matches)
            {
                var k = match.Groups[1].Value;
                var v = match.Groups[3].Value;

                yield return new Tuple<string, string>(match.Groups[1].ToString(), match.Groups[3].ToString());

                query = query.Replace(match.Groups[0].ToString(), string.Empty).Trim();
            }

            if (!string.IsNullOrEmpty(query))
            {
                // for the remainder of terms, assign to name individually.
                foreach (var term in query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    yield return new Tuple<string, string>("name", term);
                }
            }
        }

        private void EditRecipe(Recipe recipe, bool switchTabs = true)
        {
            if (recipe == null)
            {
                tbName.Text =
                cbCategory.Text =
                tbComment.Text =
                cbCredit.Text =
                cbCuisine.Text =
                tbServings.Text =
                tbCookTime.Text =
                tbPrepTime.Text = string.Empty;

                lvIngredients.Items.Clear();

                tbInstructions.Text = string.Empty;
            }
            else
            {
                tbName.Text = recipe.Name;
                cbCategory.Text = recipe.Category;
                cbCredit.Text = recipe.Credit;
                cbCuisine.Text = recipe.Cuisine;
                tbServings.Text = recipe.Servings;
                tbCookTime.Text = recipe.CookTime;
                tbPrepTime.Text = recipe.PrepTime;
                tbComment.Text = recipe.Comment;

                recipe.LoadChildren();

                lvIngredients.Items.Clear();
                foreach (var ingredient in recipe.Ingredients)
                {
                    lvIngredients.Items.Add(new ListViewItem(new[] 
                    {
                            ingredient.Quantity,
                            ingredient.Units,
                            ingredient.Item,
                            ingredient.Remarks,
                            ingredient.IsOptional ? "Y" : string.Empty
                        })
                        {
                            Tag = ingredient
                        });
                }

                tbInstructions.Text = string.Join("\r\n\r\n", recipe.Instructions.OrderBy(i => i.InstructionId).Select(i => i.Text));
            }

            if (switchTabs)
            {
                tabControl.SelectedIndex = 2;
            }
        }

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

        private void ClearRecipe()
        {
            currentRecipe = null;
            tbName.Text =
                tbServings.Text =
                cbCuisine.Text =
                cbCategory.Text =
                cbCredit.Text =
                tbCookTime.Text =
                tbPrepTime.Text =
                tbComment.Text = string.Empty;
            lvIngredients.Items.Clear();
            tbInstructions.Text = string.Empty;
        }
        #endregion

        #region Search Events
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
            }
        }

        private void lvAllRecipes_Resize(object sender, EventArgs e)
        {
            ////var totalWidth = (lvAllRecipes.Width - 30);

            ////lvAllRecipes.Columns["name"].Width = totalWidth / 3;
            ////lvAllRecipes.Columns["category"].Width = totalWidth / 3;
            ////lvAllRecipes.Columns["cuisine"].Width = totalWidth / 3;
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                // enter -- do the search
                displayRecipes(tbSearch.Text.Trim());
            }
            else if (e.KeyChar == 27)
            {
                // esc -- clear the text
                tbSearch.Text = string.Empty;
                displayRecipes();
            }
        }

        private void lvAllRecipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ListView)sender).SelectedItems.Count == 0)
            {
                return;
            }

            var recipe = ((ListView)sender).SelectedItems[0].Tag as Recipe;
            statusStrip1.Items[0].Text = recipe.Name + (string.IsNullOrEmpty(recipe.Credit) ? string.Empty : " by " + recipe.Credit);
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://flambe.dingostick.com");
        }
        #endregion

        #region Create Events
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
                        if (ingredient.Delete())
                        {
                            lvIngredients.Items.RemoveAt(((ListView)sender).SelectedIndices[0]);
                        }
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
                .Select((t, i) => new Instruction(currentRecipe) { Text = t, InstructionId = i, Parent = currentRecipe }).ToList();

            currentRecipe.Commit();

            // move back to the search page and refresh the list
            displayRecipes();
            tabControl.SelectedIndex = 0;
            ClearRecipe();
        }

        private void allIngredients_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(tbItem.Text))
            {
                // TODO: notify the user that the item must be present
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
            }
            else
            {
                var ingredient = lvIngredients.SelectedItems[0].Tag as Ingredient;

                tbQuantity.Text = ingredient.Quantity;
                tbUnits.Text = ingredient.Units;
                tbItem.Text = ingredient.Item;
                tbRemarks.Text = ingredient.Remarks;
                cbIsOptional.Checked = ingredient.IsOptional;
            }
        }
        #endregion
    }
}
