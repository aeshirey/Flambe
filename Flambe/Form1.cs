namespace Flambe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Net.Http;
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
            CheckVersion();
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
            var servings = new HashSet<string>();

            foreach (var recipe in FlambeDB.DbConnection.Table<Recipe>())
            {
                credits.Add(recipe.Credit);
                cuisines.Add(recipe.Cuisine);
                categories.Add(recipe.Category);

                servings.Add(recipe.Servings);
            }

            cbCredit.Items.AddRange(credits.OrderBy(c => c).ToArray());
            cbCuisine.Items.AddRange(cuisines.OrderBy(c => c).ToArray());
            cbCategory.Items.AddRange(categories.OrderBy(c => c).ToArray());

            var servAC = new AutoCompleteStringCollection();
            servAC.AddRange(servings.OrderBy(s => s).ToArray());
            tbServings.AutoCompleteCustomSource = servAC;
            tbServings.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbServings.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            // ingredients auto-complete
            var itemAC = new AutoCompleteStringCollection();
            var ingredients = FlambeDB.DbConnection.Table<Ingredient>().ToList();
            itemAC.AddRange(ingredients
                .Select(ing => ing.Item)
                .Where(item => !string.IsNullOrWhiteSpace(item))
                .Distinct()
                .ToArray());
            tbItem.AutoCompleteCustomSource = itemAC;
            tbItem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        private void initStatusStrip()
        {
            // for storing the recipe name or whatever
            statusStrip1.Items.Add(new ToolStripStatusLabel(string.Empty));

            var currentVersion = typeof(formMain).Assembly.GetName().Version;
            var currentStr = string.Format("{0}.{1}.{2}", currentVersion.Major, currentVersion.MajorRevision, currentVersion.Minor);

            statusLabelFlambeLink.Text = "Flambe " + currentStr;
        }
        #endregion

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://flambe.dingostick.com");
        }

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

        /// <summary>
        /// Clears input data for creating a new recipe
        /// </summary>
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

        /// <summary>
        /// Checks to see whether the user has the newest version. If not, prompts to download.
        /// </summary>
        private async void CheckVersion()
        {
            const string VersionUrl = @"http://flambe.dingostick.com/current.version";
            const string ExeUrl = @"http://flambe.dingostick.com/Flambe.zip";
            var currentVersion = typeof(formMain).Assembly.GetName().Version;
            var currentStr = string.Format("{0}.{1}.{2}", currentVersion.Major, currentVersion.MajorRevision, currentVersion.Minor);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(new Uri(VersionUrl));

                if (response.IsSuccessStatusCode)
                {
                    var newestVersion = response.Content.ReadAsStringAsync().Result.TrimEnd();

                    if (newestVersion != currentStr)
                    {
                        var result = MessageBox.Show("There is a newer version of Flambe available. Download now?", "Newer version available", MessageBoxButtons.YesNo);

                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            Process.Start(ExeUrl);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handle KeyDown events, specifically to account for F11 fullscren functionality
        /// </summary>
        /// <param name="sender">The source of the keypress</param>
        /// <param name="e">Which key was pressed</param>
        private void formMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                if (FormBorderStyle == FormBorderStyle.None)
                {
                    FormBorderStyle = FormBorderStyle.Sizable;
                    WindowState = FormWindowState.Normal;
                }
                else
                {
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Maximized;
                }
            }
            else if ((e.KeyValue == 'n' || e.KeyValue == 'N') && Control.ModifierKeys.HasFlag(Keys.Control))
            {
                // new recipe
                ClearRecipe();
                tabControl.SelectedIndex = 2;
                tbName.Focus();
            }
        }
    }
}
