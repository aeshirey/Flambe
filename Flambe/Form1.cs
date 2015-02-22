using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Flambe
{
    public partial class formMain : Form
    {
        private Recipe currentRecipe;
        private Ingredient currentIngredient;

        #region Initialization
        public formMain()
        {
            InitializeComponent();

            lvAllRecipes.View = View.Details;
            var totalWidth = (lvAllRecipes.Width - 30);
            lvAllRecipes.Columns.Add("name", "Name", totalWidth / 3);
            lvAllRecipes.Columns.Add("category", "Category", totalWidth / 3);
            lvAllRecipes.Columns.Add("cuisine", "Cuisine", totalWidth / 3);


            lvIngredients.View = View.Details;
            lvIngredients.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvIngredients.ItemSelectionChanged += lvIngredients_SelectedIndexChanged;
            lvIngredients.Columns.Add("quantity", "Qty");
            lvIngredients.Columns.Add("units", "Units");
            lvIngredients.Columns.Add("item", "Item");
            lvIngredients.Columns.Add("remarks", "Remarks");
            lvIngredients.Columns.Add("optional", "Optional");

            lvIngredients.Resize += new EventHandler((obj, args) =>
            {
                var availableWidth = lvIngredients.Width - 10;
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


            lvIngredients.LostFocus += new EventHandler((obj, args) =>
            {
                lvIngredients.SelectedIndices.Clear();
            });

            displayRecipes();
            initComboBoxes();
            initStatusStrip();
        }

        private void initComboBoxes()
        {
            var credits = new HashSet<string>();
            var cuisines = new HashSet<string>();
            var categories = new HashSet<string>();
            foreach (var recipe in FlambeDB.connection.Table<Recipe>())
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
            statusStrip1.Items.Add(new ToolStripStatusLabel(""));
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            FlambeDB.CloseDB();
            base.OnClosing(e);
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
                            conditions.Add("rating >= " + rating.ToString());
                    }
                    else
                    {
                        conditions.Add(string.Format("{0} LIKE '%{1}%'", term.Item1, term.Item2.Replace("'", "''")));
                    }
                }

                if (conditions.Count > 0)
                    query = "SELECT * FROM Recipe WHERE " + string.Join(" AND ", conditions);
            }


            lvAllRecipes.Items.Clear();
            foreach (var recipe in FlambeDB.connection.Query<Recipe>(query))
            {
                var item = new ListViewItem(new[]
                {
                    recipe.Name,
                    recipe.Category,
                    recipe.Cuisine
                });
                item.Tag = recipe;
                item.BackColor = lvAllRecipes.Items.Count % 2 == 0 ? Color.WhiteSmoke : Color.White;

                lvAllRecipes.Items.Add(item);
            }

            var numRecipes = lvAllRecipes.Items.Count;
            statusLabelGeneralPurpose.Text = numRecipes == 1 ? "Displaying 1 recipe" : "Displaying " + numRecipes.ToString() + " recipes";
        }

        /// <summary>
        /// Populates the edit/create ComboBoxes with known values from the database
        /// </summary>
        private IEnumerable<Tuple<string, string>> GetSearchTerms(string query)
        {
            const string pattern = @"([a-z]+):(""?)([^""]+?)\b\2";

            query = query.Trim().ToLowerInvariant();
            var matches = Regex.Matches(query, pattern);
            foreach (Match match in matches)
            {
                var k = match.Groups[1].Value;
                var v = match.Groups[3].Value;

                yield return new Tuple<string, string>(match.Groups[1].ToString(), match.Groups[3].ToString());

                query = query.Replace(match.Groups[0].ToString(), "").Trim();
            }

            if (!string.IsNullOrEmpty(query))
            {
                // for the remainder of terms, assign to name individually.
                // TODO: keep enquoted values together
                foreach (var term in query.Split(new [] {' '},  StringSplitOptions.RemoveEmptyEntries))
                    yield return new Tuple<string, string>("name", term);
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

                tbInstructions.Text = "";
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

                foreach (var ingredient in recipe.Ingredients)
                {
                    lvIngredients.Items.Add(new ListViewItem(new[] {
                        ingredient.Quantity,
                        ingredient.Units,
                        ingredient.Item,
                        ingredient.Remarks,
                        ingredient.IsOptional ? "Y" : string.Empty
                    })
                    {
                        Tag = ingredient
                    }
                    );
                }


                tbInstructions.Text = string.Join("\r\n\r\n", recipe.Instructions.OrderBy(i => i.InstructionId).Select(i => i.Text));
            }

            if (switchTabs)
                tabControl.SelectedIndex = 2;
        }
        #endregion


        #region Events
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

                var mi = new MenuItem("Open re&cipe", lvAllRecipes_DoubleClick) { Tag = recipe };
                cm.MenuItems.Add(mi);

                mi = new MenuItem("P&rint recipe") { Tag = recipe };
                cm.MenuItems.Add(mi);

                mi = new MenuItem("&Edit recipe") { Tag = recipe };
                mi.Click += new EventHandler((obj, args) => EditRecipe(mi.Tag as Recipe));
                cm.MenuItems.Add(mi);

                mi = new MenuItem("Uplo&ad recipe") { Tag = recipe };
                cm.MenuItems.Add(mi);

                mi = new MenuItem("&Delete recipe") { Tag = recipe };
                mi.Click += new EventHandler((obj, args) =>
                {
                    var choice = MessageBox.Show("Are you sure you want to delete this recipe?", "Delete recipe?", MessageBoxButtons.YesNo);
                    if (choice == System.Windows.Forms.DialogResult.Yes)
                    {
                        for (int i = 0; i < lvAllRecipes.Items.Count; i++)
                        {
                            if (lvAllRecipes.Items[i].Tag == mi.Tag)
                            {
                                lvAllRecipes.Items.RemoveAt(i);
                                break;
                            }
                        }
                        recipe.Delete();
                        lvAllRecipes.SelectedItems.Clear();
                    }
                });
                cm.MenuItems.Add(mi);

                this.ContextMenu = cm;
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
                        if (ingredient.Delete())
                            lvIngredients.Items.RemoveAt(((ListView)sender).SelectedIndices[0]);
                    }
                });
                cm.MenuItems.Add(mi);


                mi = new MenuItem("Add ingredient");
                mi.Click += new EventHandler((obj, args) =>
                {
                    var i = new Ingredient(null /* TODO */);
                });


                this.ContextMenu = cm;
            }
        }

        private void lvAllRecipes_Resize(object sender, EventArgs e)
        {
            //var totalWidth = (lvAllRecipes.Width - 30);

            //lvAllRecipes.Columns["name"].Width = totalWidth / 3;
            //lvAllRecipes.Columns["category"].Width = totalWidth / 3;
            //lvAllRecipes.Columns["cuisine"].Width = totalWidth / 3;
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

        private void lvIngredients_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = lvIngredients.FocusedItem;
            if (item != null)
            {

                currentIngredient = item.Tag as Ingredient;

                tbQuantity.Text = currentIngredient.Quantity;
                tbUnits.Text = currentIngredient.Units;
                tbItem.Text = currentIngredient.Item;
                tbRemarks.Text = currentIngredient.Remarks;
                cbIsOptional.Checked = currentIngredient.IsOptional;
            }
            else
            {
                currentIngredient = null;
                tbQuantity.Text =
                    tbUnits.Text =
                    tbItem.Text =
                    tbRemarks.Text = string.Empty;
                cbIsOptional.Checked = false;
            }
        }

        private void lvIngredients_SelectedIndexChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            tbQuantity.Text =
                tbUnits.Text =
                tbItem.Text =
                tbRemarks.Text = string.Empty;
            cbIsOptional.Checked = false;

            foreach (ListViewItem item in lvIngredients.SelectedItems)
            {

                var ingredient = item.Tag as Ingredient;

                tbQuantity.Text = ingredient.Quantity;
                tbUnits.Text = ingredient.Units;
                tbItem.Text = ingredient.Item;
                tbRemarks.Text = ingredient.Remarks;
                cbIsOptional.Checked = ingredient.IsOptional;
            }
        }

        private void tbQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (currentIngredient == null)
                currentIngredient = new Ingredient(currentRecipe)
                {
                    Quantity = tbQuantity.Text,
                    Units = tbUnits.Text,
                    Item = tbItem.Text,
                    Remarks = tbRemarks.Text,
                    IsOptional = cbIsOptional.Checked,
                    IngredientOrder = currentRecipe.Ingredients.Count + 1,
                    RecipeId = currentRecipe.RecipeId
                };

            if (e.KeyChar == 13)
            {
                tbQuantity.Text = tbUnits.Text = tbItem.Text = tbRemarks.Text = string.Empty;
                lvIngredients.Items.Add(new ListViewItem(new[] {
                    currentIngredient.Quantity,
                    currentIngredient.Units,
                    currentIngredient.Item,
                    currentIngredient.Remarks,
                    currentIngredient.IsOptional ? "Y" : ""
                })
                {
                    Tag = currentIngredient
                });

                currentIngredient = null;
            }
        }

        private void lvAllRecipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ListView)sender).SelectedItems.Count == 0)
                return;
            var recipe = ((ListView)sender).SelectedItems[0].Tag as Recipe;
            statusStrip1.Items[0].Text = recipe.Name + (string.IsNullOrEmpty(recipe.Credit) ? "" : " by " + recipe.Credit);
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://flambe.dingostick.com");
        }

        private void btnSaveRecipe_Click(object sender, EventArgs e)
        {
            if (currentRecipe == null)
                currentRecipe = new Recipe();

            currentRecipe.Name = tbName.Text;
            currentRecipe.Servings = tbServings.Text;
            currentRecipe.Cuisine = cbCuisine.Text;
            currentRecipe.Category = cbCategory.Text;
            currentRecipe.Credit = cbCredit.Text;
            currentRecipe.CookTime = tbCookTime.Text;
            currentRecipe.PrepTime = tbPrepTime.Text;
            currentRecipe.Comment = tbComment.Text;

            currentRecipe.Instructions = tbInstructions.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select((t, i) => new Instruction() { Text = t, InstructionId = i }).ToList();

            currentRecipe.Commit();
        }
        #endregion
    }
}
