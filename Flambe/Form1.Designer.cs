namespace Flambe
{
    /// <summary>
    /// Flambe's GUI
    /// </summary>
    public partial class formMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.lvAllRecipes = new System.Windows.Forms.ListView();
            this.tabView = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabCreate = new System.Windows.Forms.TabPage();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.btnSaveRecipe = new System.Windows.Forms.Button();
            this.gbInstructions = new System.Windows.Forms.GroupBox();
            this.tbInstructions = new System.Windows.Forms.TextBox();
            this.gbIngredients = new System.Windows.Forms.GroupBox();
            this.cbIsOptional = new System.Windows.Forms.CheckBox();
            this.tbRemarks = new System.Windows.Forms.TextBox();
            this.tbItem = new System.Windows.Forms.TextBox();
            this.tbUnits = new System.Windows.Forms.TextBox();
            this.tbQuantity = new System.Windows.Forms.TextBox();
            this.lvIngredients = new System.Windows.Forms.ListView();
            this.tbCookTime = new System.Windows.Forms.TextBox();
            this.lblCookTime = new System.Windows.Forms.Label();
            this.tbPrepTime = new System.Windows.Forms.TextBox();
            this.lblPrepTime = new System.Windows.Forms.Label();
            this.tbServings = new System.Windows.Forms.TextBox();
            this.lblServings = new System.Windows.Forms.Label();
            this.cbCuisine = new System.Windows.Forms.ComboBox();
            this.lblCuisine = new System.Windows.Forms.Label();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cbCredit = new System.Windows.Forms.ComboBox();
            this.lblCredit = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabelGeneralPurpose = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelSpacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelFlambeLink = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabView.SuspendLayout();
            this.tabCreate.SuspendLayout();
            this.gbInstructions.SuspendLayout();
            this.gbIngredients.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabSearch);
            this.tabControl.Controls.Add(this.tabView);
            this.tabControl.Controls.Add(this.tabCreate);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1088, 675);
            this.tabControl.TabIndex = 0;
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this.tbSearch);
            this.tabSearch.Controls.Add(this.lvAllRecipes);
            this.tabSearch.Location = new System.Drawing.Point(8, 39);
            this.tabSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabSearch.Size = new System.Drawing.Size(1072, 628);
            this.tabSearch.TabIndex = 0;
            this.tabSearch.Text = "Search";
            this.tabSearch.ToolTipText = "Find or browse recipes";
            this.tabSearch.UseVisualStyleBackColor = true;
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Location = new System.Drawing.Point(6, 583);
            this.tbSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(1054, 31);
            this.tbSearch.TabIndex = 2;
            this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
            // 
            // lvAllRecipes
            // 
            this.lvAllRecipes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAllRecipes.FullRowSelect = true;
            this.lvAllRecipes.Location = new System.Drawing.Point(6, 6);
            this.lvAllRecipes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvAllRecipes.MultiSelect = false;
            this.lvAllRecipes.Name = "lvAllRecipes";
            this.lvAllRecipes.Size = new System.Drawing.Size(1054, 566);
            this.lvAllRecipes.TabIndex = 1;
            this.lvAllRecipes.UseCompatibleStateImageBehavior = false;
            this.lvAllRecipes.SelectedIndexChanged += new System.EventHandler(this.lvAllRecipes_SelectedIndexChanged);
            this.lvAllRecipes.DoubleClick += new System.EventHandler(this.lvAllRecipes_DoubleClick);
            this.lvAllRecipes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvAllRecipes_KeyDown);
            this.lvAllRecipes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvAllRecipes_MouseClick);
            this.lvAllRecipes.Resize += new System.EventHandler(this.lvAllRecipes_Resize);
            // 
            // tabView
            // 
            this.tabView.Controls.Add(this.webBrowser1);
            this.tabView.Location = new System.Drawing.Point(8, 39);
            this.tabView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabView.Name = "tabView";
            this.tabView.Size = new System.Drawing.Size(1072, 628);
            this.tabView.TabIndex = 2;
            this.tabView.Text = "View";
            this.tabView.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 19);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1072, 628);
            this.webBrowser1.TabIndex = 0;
            // 
            // tabCreate
            // 
            this.tabCreate.Controls.Add(this.tbComment);
            this.tabCreate.Controls.Add(this.lblComment);
            this.tabCreate.Controls.Add(this.btnSaveRecipe);
            this.tabCreate.Controls.Add(this.gbInstructions);
            this.tabCreate.Controls.Add(this.gbIngredients);
            this.tabCreate.Controls.Add(this.tbCookTime);
            this.tabCreate.Controls.Add(this.lblCookTime);
            this.tabCreate.Controls.Add(this.tbPrepTime);
            this.tabCreate.Controls.Add(this.lblPrepTime);
            this.tabCreate.Controls.Add(this.tbServings);
            this.tabCreate.Controls.Add(this.lblServings);
            this.tabCreate.Controls.Add(this.cbCuisine);
            this.tabCreate.Controls.Add(this.lblCuisine);
            this.tabCreate.Controls.Add(this.cbCategory);
            this.tabCreate.Controls.Add(this.lblCategory);
            this.tabCreate.Controls.Add(this.cbCredit);
            this.tabCreate.Controls.Add(this.lblCredit);
            this.tabCreate.Controls.Add(this.tbName);
            this.tabCreate.Controls.Add(this.lblName);
            this.tabCreate.Location = new System.Drawing.Point(8, 39);
            this.tabCreate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabCreate.Name = "tabCreate";
            this.tabCreate.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabCreate.Size = new System.Drawing.Size(1072, 628);
            this.tabCreate.TabIndex = 1;
            this.tabCreate.Text = "Create";
            this.tabCreate.ToolTipText = "Create a new recipe";
            this.tabCreate.UseVisualStyleBackColor = true;
            // 
            // tbComment
            // 
            this.tbComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbComment.Location = new System.Drawing.Point(560, 156);
            this.tbComment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(514, 31);
            this.tbComment.TabIndex = 15;
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(438, 158);
            this.lblComment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(109, 25);
            this.lblComment.TabIndex = 14;
            this.lblComment.Text = "Comment:";
            // 
            // btnSaveRecipe
            // 
            this.btnSaveRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRecipe.Location = new System.Drawing.Point(930, 590);
            this.btnSaveRecipe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSaveRecipe.Name = "btnSaveRecipe";
            this.btnSaveRecipe.Size = new System.Drawing.Size(144, 40);
            this.btnSaveRecipe.TabIndex = 17;
            this.btnSaveRecipe.Text = "S&ave Recipe";
            this.btnSaveRecipe.UseVisualStyleBackColor = true;
            this.btnSaveRecipe.Click += new System.EventHandler(this.btnSaveRecipe_Click);
            // 
            // gbInstructions
            // 
            this.gbInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInstructions.Controls.Add(this.tbInstructions);
            this.gbInstructions.Location = new System.Drawing.Point(16, 444);
            this.gbInstructions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbInstructions.Name = "gbInstructions";
            this.gbInstructions.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbInstructions.Size = new System.Drawing.Size(1056, 140);
            this.gbInstructions.TabIndex = 0;
            this.gbInstructions.TabStop = false;
            this.gbInstructions.Text = "Instructions";
            // 
            // tbInstructions
            // 
            this.tbInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInstructions.Location = new System.Drawing.Point(8, 31);
            this.tbInstructions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbInstructions.Multiline = true;
            this.tbInstructions.Name = "tbInstructions";
            this.tbInstructions.Size = new System.Drawing.Size(1044, 102);
            this.tbInstructions.TabIndex = 0;
            // 
            // gbIngredients
            // 
            this.gbIngredients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbIngredients.Controls.Add(this.cbIsOptional);
            this.gbIngredients.Controls.Add(this.tbRemarks);
            this.gbIngredients.Controls.Add(this.tbItem);
            this.gbIngredients.Controls.Add(this.tbUnits);
            this.gbIngredients.Controls.Add(this.tbQuantity);
            this.gbIngredients.Controls.Add(this.lvIngredients);
            this.gbIngredients.Location = new System.Drawing.Point(16, 217);
            this.gbIngredients.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbIngredients.Name = "gbIngredients";
            this.gbIngredients.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbIngredients.Size = new System.Drawing.Size(1056, 221);
            this.gbIngredients.TabIndex = 16;
            this.gbIngredients.TabStop = false;
            this.gbIngredients.Text = "Ingredients";
            // 
            // cbIsOptional
            // 
            this.cbIsOptional.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIsOptional.AutoSize = true;
            this.cbIsOptional.Location = new System.Drawing.Point(924, 181);
            this.cbIsOptional.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbIsOptional.Name = "cbIsOptional";
            this.cbIsOptional.Size = new System.Drawing.Size(124, 29);
            this.cbIsOptional.TabIndex = 5;
            this.cbIsOptional.Text = "Optional";
            this.cbIsOptional.UseVisualStyleBackColor = true;
            this.cbIsOptional.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.allIngredients_KeyPress);
            // 
            // tbRemarks
            // 
            this.tbRemarks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRemarks.Location = new System.Drawing.Point(714, 179);
            this.tbRemarks.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbRemarks.Name = "tbRemarks";
            this.tbRemarks.Size = new System.Drawing.Size(192, 31);
            this.tbRemarks.TabIndex = 4;
            this.tbRemarks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.allIngredients_KeyPress);
            // 
            // tbItem
            // 
            this.tbItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbItem.Location = new System.Drawing.Point(272, 179);
            this.tbItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbItem.Name = "tbItem";
            this.tbItem.Size = new System.Drawing.Size(436, 31);
            this.tbItem.TabIndex = 3;
            this.tbItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.allIngredients_KeyPress);
            // 
            // tbUnits
            // 
            this.tbUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbUnits.Location = new System.Drawing.Point(82, 179);
            this.tbUnits.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbUnits.Name = "tbUnits";
            this.tbUnits.Size = new System.Drawing.Size(184, 31);
            this.tbUnits.TabIndex = 2;
            this.tbUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.allIngredients_KeyPress);
            // 
            // tbQuantity
            // 
            this.tbQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbQuantity.Location = new System.Drawing.Point(8, 179);
            this.tbQuantity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbQuantity.Name = "tbQuantity";
            this.tbQuantity.Size = new System.Drawing.Size(68, 31);
            this.tbQuantity.TabIndex = 1;
            this.tbQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbQuantity_KeyPress);
            // 
            // lvIngredients
            // 
            this.lvIngredients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvIngredients.FullRowSelect = true;
            this.lvIngredients.HideSelection = false;
            this.lvIngredients.Location = new System.Drawing.Point(8, 31);
            this.lvIngredients.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvIngredients.MultiSelect = false;
            this.lvIngredients.Name = "lvIngredients";
            this.lvIngredients.Size = new System.Drawing.Size(1044, 141);
            this.lvIngredients.TabIndex = 0;
            this.lvIngredients.UseCompatibleStateImageBehavior = false;
            this.lvIngredients.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvIngredients_ItemSelectionChanged);
            this.lvIngredients.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvIngredients_MouseDown);
            // 
            // tbCookTime
            // 
            this.tbCookTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCookTime.Location = new System.Drawing.Point(560, 112);
            this.tbCookTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbCookTime.Name = "tbCookTime";
            this.tbCookTime.Size = new System.Drawing.Size(514, 31);
            this.tbCookTime.TabIndex = 13;
            // 
            // lblCookTime
            // 
            this.lblCookTime.AutoSize = true;
            this.lblCookTime.Location = new System.Drawing.Point(438, 115);
            this.lblCookTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCookTime.Name = "lblCookTime";
            this.lblCookTime.Size = new System.Drawing.Size(121, 25);
            this.lblCookTime.TabIndex = 12;
            this.lblCookTime.Text = "Cook Time:";
            // 
            // tbPrepTime
            // 
            this.tbPrepTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPrepTime.Location = new System.Drawing.Point(560, 63);
            this.tbPrepTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbPrepTime.Name = "tbPrepTime";
            this.tbPrepTime.Size = new System.Drawing.Size(514, 31);
            this.tbPrepTime.TabIndex = 11;
            // 
            // lblPrepTime
            // 
            this.lblPrepTime.AutoSize = true;
            this.lblPrepTime.Location = new System.Drawing.Point(438, 67);
            this.lblPrepTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrepTime.Name = "lblPrepTime";
            this.lblPrepTime.Size = new System.Drawing.Size(116, 25);
            this.lblPrepTime.TabIndex = 10;
            this.lblPrepTime.Text = "Prep Time:";
            // 
            // tbServings
            // 
            this.tbServings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServings.Location = new System.Drawing.Point(560, 13);
            this.tbServings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbServings.Name = "tbServings";
            this.tbServings.Size = new System.Drawing.Size(514, 31);
            this.tbServings.TabIndex = 9;
            // 
            // lblServings
            // 
            this.lblServings.AutoSize = true;
            this.lblServings.Location = new System.Drawing.Point(438, 17);
            this.lblServings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServings.Name = "lblServings";
            this.lblServings.Size = new System.Drawing.Size(102, 25);
            this.lblServings.TabIndex = 8;
            this.lblServings.Text = "Servings:";
            // 
            // cbCuisine
            // 
            this.cbCuisine.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCuisine.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCuisine.FormattingEnabled = true;
            this.cbCuisine.Location = new System.Drawing.Point(132, 156);
            this.cbCuisine.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbCuisine.Name = "cbCuisine";
            this.cbCuisine.Size = new System.Drawing.Size(288, 33);
            this.cbCuisine.TabIndex = 7;
            // 
            // lblCuisine
            // 
            this.lblCuisine.AutoSize = true;
            this.lblCuisine.Location = new System.Drawing.Point(12, 156);
            this.lblCuisine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCuisine.Name = "lblCuisine";
            this.lblCuisine.Size = new System.Drawing.Size(90, 25);
            this.lblCuisine.TabIndex = 6;
            this.lblCuisine.Text = "Cuisine:";
            // 
            // cbCategory
            // 
            this.cbCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(132, 108);
            this.cbCategory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(288, 33);
            this.cbCategory.TabIndex = 5;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(12, 108);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(105, 25);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "Category:";
            // 
            // cbCredit
            // 
            this.cbCredit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCredit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCredit.FormattingEnabled = true;
            this.cbCredit.Location = new System.Drawing.Point(132, 62);
            this.cbCredit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbCredit.Name = "cbCredit";
            this.cbCredit.Size = new System.Drawing.Size(288, 33);
            this.cbCredit.TabIndex = 3;
            // 
            // lblCredit
            // 
            this.lblCredit.AutoSize = true;
            this.lblCredit.Location = new System.Drawing.Point(12, 62);
            this.lblCredit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(75, 25);
            this.lblCredit.TabIndex = 2;
            this.lblCredit.Text = "Credit:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(132, 17);
            this.tbName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(288, 31);
            this.tbName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 19);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(74, 25);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelGeneralPurpose,
            this.statusLabelSpacer,
            this.statusLabelFlambeLink});
            this.statusStrip1.Location = new System.Drawing.Point(0, 696);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1112, 37);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabelGeneralPurpose
            // 
            this.statusLabelGeneralPurpose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabelGeneralPurpose.Name = "statusLabelGeneralPurpose";
            this.statusLabelGeneralPurpose.Size = new System.Drawing.Size(22, 32);
            this.statusLabelGeneralPurpose.Text = " ";
            // 
            // statusLabelSpacer
            // 
            this.statusLabelSpacer.Name = "statusLabelSpacer";
            this.statusLabelSpacer.Size = new System.Drawing.Size(22, 32);
            this.statusLabelSpacer.Text = " ";
            // 
            // statusLabelFlambeLink
            // 
            this.statusLabelFlambeLink.IsLink = true;
            this.statusLabelFlambeLink.Name = "statusLabelFlambeLink";
            this.statusLabelFlambeLink.Size = new System.Drawing.Size(131, 32);
            this.statusLabelFlambeLink.Text = "Flambe 3.0";
            this.statusLabelFlambeLink.Click += new System.EventHandler(this.toolStripStatusLabel2_Click);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 733);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(790, 706);
            this.Name = "formMain";
            this.Text = "Flambe";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.formMain_KeyDown);
            this.tabControl.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabSearch.PerformLayout();
            this.tabView.ResumeLayout(false);
            this.tabCreate.ResumeLayout(false);
            this.tabCreate.PerformLayout();
            this.gbInstructions.ResumeLayout(false);
            this.gbInstructions.PerformLayout();
            this.gbIngredients.ResumeLayout(false);
            this.gbIngredients.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSearch;
        private System.Windows.Forms.TabPage tabView;
        private System.Windows.Forms.TabPage tabCreate;
        private System.Windows.Forms.ListView lvAllRecipes;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.ComboBox cbCredit;
        private System.Windows.Forms.Label lblCredit;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ComboBox cbCuisine;
        private System.Windows.Forms.Label lblCuisine;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox tbPrepTime;
        private System.Windows.Forms.Label lblPrepTime;
        private System.Windows.Forms.TextBox tbServings;
        private System.Windows.Forms.Label lblServings;
        private System.Windows.Forms.TextBox tbCookTime;
        private System.Windows.Forms.Label lblCookTime;
        private System.Windows.Forms.GroupBox gbInstructions;
        private System.Windows.Forms.GroupBox gbIngredients;
        private System.Windows.Forms.CheckBox cbIsOptional;
        private System.Windows.Forms.TextBox tbRemarks;
        private System.Windows.Forms.TextBox tbItem;
        private System.Windows.Forms.TextBox tbUnits;
        private System.Windows.Forms.TextBox tbQuantity;
        private System.Windows.Forms.ListView lvIngredients;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox tbInstructions;
        private System.Windows.Forms.Button btnSaveRecipe;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelGeneralPurpose;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelFlambeLink;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelSpacer;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Label lblComment;
    }
}