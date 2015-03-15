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
            tabControl = new System.Windows.Forms.TabControl();
            tabSearch = new System.Windows.Forms.TabPage();
            tbSearch = new System.Windows.Forms.TextBox();
            lvAllRecipes = new System.Windows.Forms.ListView();
            tabView = new System.Windows.Forms.TabPage();
            webBrowser1 = new System.Windows.Forms.WebBrowser();
            tabCreate = new System.Windows.Forms.TabPage();
            tbComment = new System.Windows.Forms.TextBox();
            lblComment = new System.Windows.Forms.Label();
            btnSaveRecipe = new System.Windows.Forms.Button();
            gbInstructions = new System.Windows.Forms.GroupBox();
            tbInstructions = new System.Windows.Forms.TextBox();
            gbIngredients = new System.Windows.Forms.GroupBox();
            cbIsOptional = new System.Windows.Forms.CheckBox();
            tbRemarks = new System.Windows.Forms.TextBox();
            tbItem = new System.Windows.Forms.TextBox();
            tbUnits = new System.Windows.Forms.TextBox();
            tbQuantity = new System.Windows.Forms.TextBox();
            lvIngredients = new System.Windows.Forms.ListView();
            tbCookTime = new System.Windows.Forms.TextBox();
            lblCookTime = new System.Windows.Forms.Label();
            tbPrepTime = new System.Windows.Forms.TextBox();
            lblPrepTime = new System.Windows.Forms.Label();
            tbServings = new System.Windows.Forms.TextBox();
            lblServings = new System.Windows.Forms.Label();
            cbCuisine = new System.Windows.Forms.ComboBox();
            lblCuisine = new System.Windows.Forms.Label();
            cbCategory = new System.Windows.Forms.ComboBox();
            lblCategory = new System.Windows.Forms.Label();
            cbCredit = new System.Windows.Forms.ComboBox();
            lblCredit = new System.Windows.Forms.Label();
            tbName = new System.Windows.Forms.TextBox();
            lblName = new System.Windows.Forms.Label();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            statusLabelGeneralPurpose = new System.Windows.Forms.ToolStripStatusLabel();
            statusLabelSpacer = new System.Windows.Forms.ToolStripStatusLabel();
            statusLabelFlambeLink = new System.Windows.Forms.ToolStripStatusLabel();
            tabControl.SuspendLayout();
            tabSearch.SuspendLayout();
            tabView.SuspendLayout();
            tabCreate.SuspendLayout();
            gbInstructions.SuspendLayout();
            gbIngredients.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabSearch);
            this.tabControl.Controls.Add(this.tabView);
            this.tabControl.Controls.Add(this.tabCreate);
            this.tabControl.Location = new System.Drawing.Point(6, 6);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(544, 334);
            this.tabControl.TabIndex = 0;
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this.tbSearch);
            this.tabSearch.Controls.Add(this.lvAllRecipes);
            this.tabSearch.Location = new System.Drawing.Point(4, 22);
            this.tabSearch.Margin = new System.Windows.Forms.Padding(2);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Padding = new System.Windows.Forms.Padding(2);
            this.tabSearch.Size = new System.Drawing.Size(536, 308);
            this.tabSearch.TabIndex = 0;
            this.tabSearch.Text = "Search";
            this.tabSearch.ToolTipText = "Find or browse recipes";
            this.tabSearch.UseVisualStyleBackColor = true;
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Location = new System.Drawing.Point(3, 295);
            this.tbSearch.Margin = new System.Windows.Forms.Padding(2);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(536, 20);
            this.tbSearch.TabIndex = 2;
            this.tbSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearch_KeyPress);
            // 
            // lvAllRecipes
            // 
            this.lvAllRecipes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAllRecipes.FullRowSelect = true;
            this.lvAllRecipes.Location = new System.Drawing.Point(3, 3);
            this.lvAllRecipes.Margin = new System.Windows.Forms.Padding(2);
            this.lvAllRecipes.MultiSelect = false;
            this.lvAllRecipes.Name = "lvAllRecipes";
            this.lvAllRecipes.Size = new System.Drawing.Size(536, 291);
            this.lvAllRecipes.TabIndex = 1;
            this.lvAllRecipes.UseCompatibleStateImageBehavior = false;
            this.lvAllRecipes.SelectedIndexChanged += new System.EventHandler(this.lvAllRecipes_SelectedIndexChanged);
            this.lvAllRecipes.DoubleClick += new System.EventHandler(this.lvAllRecipes_DoubleClick);
            this.lvAllRecipes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvAllRecipes_MouseClick);
            this.lvAllRecipes.Resize += new System.EventHandler(this.lvAllRecipes_Resize);
            // 
            // tabView
            // 
            this.tabView.Controls.Add(this.webBrowser1);
            this.tabView.Location = new System.Drawing.Point(4, 22);
            this.tabView.Margin = new System.Windows.Forms.Padding(2);
            this.tabView.Name = "tabView";
            this.tabView.Size = new System.Drawing.Size(536, 308);
            this.tabView.TabIndex = 2;
            this.tabView.Text = "View";
            this.tabView.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(2);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(10, 10);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(536, 308);
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
            this.tabCreate.Location = new System.Drawing.Point(4, 22);
            this.tabCreate.Margin = new System.Windows.Forms.Padding(2);
            this.tabCreate.Name = "tabCreate";
            this.tabCreate.Padding = new System.Windows.Forms.Padding(2);
            this.tabCreate.Size = new System.Drawing.Size(536, 308);
            this.tabCreate.TabIndex = 1;
            this.tabCreate.Text = "Create";
            this.tabCreate.ToolTipText = "Create a new recipe";
            this.tabCreate.UseVisualStyleBackColor = true;
            // 
            // tbComment
            // 
            this.tbComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbComment.Location = new System.Drawing.Point(280, 81);
            this.tbComment.Margin = new System.Windows.Forms.Padding(2);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(259, 20);
            this.tbComment.TabIndex = 15;
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(219, 82);
            this.lblComment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(54, 13);
            this.lblComment.TabIndex = 14;
            this.lblComment.Text = "Comment:";
            // 
            // btnSaveRecipe
            // 
            this.btnSaveRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRecipe.Location = new System.Drawing.Point(465, 290);
            this.btnSaveRecipe.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveRecipe.Name = "btnSaveRecipe";
            this.btnSaveRecipe.Size = new System.Drawing.Size(72, 21);
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
            this.gbInstructions.Location = new System.Drawing.Point(8, 231);
            this.gbInstructions.Margin = new System.Windows.Forms.Padding(2);
            this.gbInstructions.Name = "gbInstructions";
            this.gbInstructions.Padding = new System.Windows.Forms.Padding(2);
            this.gbInstructions.Size = new System.Drawing.Size(528, 56);
            this.gbInstructions.TabIndex = 0;
            this.gbInstructions.TabStop = false;
            this.gbInstructions.Text = "Instructions";
            // 
            // tbInstructions
            // 
            this.tbInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInstructions.Location = new System.Drawing.Point(4, 16);
            this.tbInstructions.Margin = new System.Windows.Forms.Padding(2);
            this.tbInstructions.Multiline = true;
            this.tbInstructions.Name = "tbInstructions";
            this.tbInstructions.Size = new System.Drawing.Size(524, 38);
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
            this.gbIngredients.Location = new System.Drawing.Point(8, 113);
            this.gbIngredients.Margin = new System.Windows.Forms.Padding(2);
            this.gbIngredients.Name = "gbIngredients";
            this.gbIngredients.Padding = new System.Windows.Forms.Padding(2);
            this.gbIngredients.Size = new System.Drawing.Size(528, 115);
            this.gbIngredients.TabIndex = 16;
            this.gbIngredients.TabStop = false;
            this.gbIngredients.Text = "Ingredients";
            // 
            // cbIsOptional
            // 
            this.cbIsOptional.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIsOptional.AutoSize = true;
            this.cbIsOptional.Location = new System.Drawing.Point(459, 92);
            this.cbIsOptional.Margin = new System.Windows.Forms.Padding(2);
            this.cbIsOptional.Name = "cbIsOptional";
            this.cbIsOptional.Size = new System.Drawing.Size(65, 17);
            this.cbIsOptional.TabIndex = 5;
            this.cbIsOptional.Text = "Optional";
            this.cbIsOptional.UseVisualStyleBackColor = true;
            this.cbIsOptional.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.allIngredients_KeyPress);
            // 
            // tbRemarks
            // 
            this.tbRemarks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRemarks.Location = new System.Drawing.Point(357, 93);
            this.tbRemarks.Margin = new System.Windows.Forms.Padding(2);
            this.tbRemarks.Name = "tbRemarks";
            this.tbRemarks.Size = new System.Drawing.Size(98, 20);
            this.tbRemarks.TabIndex = 4;
            this.tbRemarks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.allIngredients_KeyPress);
            // 
            // tbItem
            // 
            this.tbItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbItem.Location = new System.Drawing.Point(136, 93);
            this.tbItem.Margin = new System.Windows.Forms.Padding(2);
            this.tbItem.Name = "tbItem";
            this.tbItem.Size = new System.Drawing.Size(220, 20);
            this.tbItem.TabIndex = 3;
            this.tbItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.allIngredients_KeyPress);
            // 
            // tbUnits
            // 
            this.tbUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbUnits.Location = new System.Drawing.Point(41, 93);
            this.tbUnits.Margin = new System.Windows.Forms.Padding(2);
            this.tbUnits.Name = "tbUnits";
            this.tbUnits.Size = new System.Drawing.Size(94, 20);
            this.tbUnits.TabIndex = 2;
            this.tbUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.allIngredients_KeyPress);
            // 
            // tbQuantity
            // 
            this.tbQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbQuantity.Location = new System.Drawing.Point(4, 93);
            this.tbQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.tbQuantity.Name = "tbQuantity";
            this.tbQuantity.Size = new System.Drawing.Size(36, 20);
            this.tbQuantity.TabIndex = 1;
            this.tbQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.allIngredients_KeyPress);
            // 
            // lvIngredients
            // 
            this.lvIngredients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvIngredients.FullRowSelect = true;
            this.lvIngredients.HideSelection = false;
            this.lvIngredients.Location = new System.Drawing.Point(4, 16);
            this.lvIngredients.Margin = new System.Windows.Forms.Padding(2);
            this.lvIngredients.MultiSelect = false;
            this.lvIngredients.Name = "lvIngredients";
            this.lvIngredients.Size = new System.Drawing.Size(524, 75);
            this.lvIngredients.TabIndex = 0;
            this.lvIngredients.UseCompatibleStateImageBehavior = false;
            this.lvIngredients.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvIngredients_ItemSelectionChanged);
            // 
            // tbCookTime
            // 
            this.tbCookTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCookTime.Location = new System.Drawing.Point(280, 58);
            this.tbCookTime.Margin = new System.Windows.Forms.Padding(2);
            this.tbCookTime.Name = "tbCookTime";
            this.tbCookTime.Size = new System.Drawing.Size(259, 20);
            this.tbCookTime.TabIndex = 13;
            // 
            // lblCookTime
            // 
            this.lblCookTime.AutoSize = true;
            this.lblCookTime.Location = new System.Drawing.Point(219, 60);
            this.lblCookTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCookTime.Name = "lblCookTime";
            this.lblCookTime.Size = new System.Drawing.Size(61, 13);
            this.lblCookTime.TabIndex = 12;
            this.lblCookTime.Text = "Cook Time:";
            // 
            // tbPrepTime
            // 
            this.tbPrepTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPrepTime.Location = new System.Drawing.Point(280, 33);
            this.tbPrepTime.Margin = new System.Windows.Forms.Padding(2);
            this.tbPrepTime.Name = "tbPrepTime";
            this.tbPrepTime.Size = new System.Drawing.Size(259, 20);
            this.tbPrepTime.TabIndex = 11;
            // 
            // lblPrepTime
            // 
            this.lblPrepTime.AutoSize = true;
            this.lblPrepTime.Location = new System.Drawing.Point(219, 35);
            this.lblPrepTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrepTime.Name = "lblPrepTime";
            this.lblPrepTime.Size = new System.Drawing.Size(58, 13);
            this.lblPrepTime.TabIndex = 10;
            this.lblPrepTime.Text = "Prep Time:";
            // 
            // tbServings
            // 
            this.tbServings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServings.Location = new System.Drawing.Point(280, 7);
            this.tbServings.Margin = new System.Windows.Forms.Padding(2);
            this.tbServings.Name = "tbServings";
            this.tbServings.Size = new System.Drawing.Size(259, 20);
            this.tbServings.TabIndex = 9;
            // 
            // lblServings
            // 
            this.lblServings.AutoSize = true;
            this.lblServings.Location = new System.Drawing.Point(219, 9);
            this.lblServings.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblServings.Name = "lblServings";
            this.lblServings.Size = new System.Drawing.Size(51, 13);
            this.lblServings.TabIndex = 8;
            this.lblServings.Text = "Servings:";
            // 
            // cbCuisine
            // 
            this.cbCuisine.FormattingEnabled = true;
            this.cbCuisine.Location = new System.Drawing.Point(66, 81);
            this.cbCuisine.Margin = new System.Windows.Forms.Padding(2);
            this.cbCuisine.Name = "cbCuisine";
            this.cbCuisine.Size = new System.Drawing.Size(146, 21);
            this.cbCuisine.TabIndex = 7;
            // 
            // lblCuisine
            // 
            this.lblCuisine.AutoSize = true;
            this.lblCuisine.Location = new System.Drawing.Point(6, 81);
            this.lblCuisine.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCuisine.Name = "lblCuisine";
            this.lblCuisine.Size = new System.Drawing.Size(44, 13);
            this.lblCuisine.TabIndex = 6;
            this.lblCuisine.Text = "Cuisine:";
            // 
            // cbCategory
            // 
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(66, 56);
            this.cbCategory.Margin = new System.Windows.Forms.Padding(2);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(146, 21);
            this.cbCategory.TabIndex = 5;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(6, 56);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(52, 13);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "Category:";
            // 
            // cbCredit
            // 
            this.cbCredit.FormattingEnabled = true;
            this.cbCredit.Location = new System.Drawing.Point(66, 32);
            this.cbCredit.Margin = new System.Windows.Forms.Padding(2);
            this.cbCredit.Name = "cbCredit";
            this.cbCredit.Size = new System.Drawing.Size(146, 21);
            this.cbCredit.TabIndex = 3;
            // 
            // lblCredit
            // 
            this.lblCredit.AutoSize = true;
            this.lblCredit.Location = new System.Drawing.Point(6, 32);
            this.lblCredit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(37, 13);
            this.lblCredit.TabIndex = 2;
            this.lblCredit.Text = "Credit:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(66, 9);
            this.tbName.Margin = new System.Windows.Forms.Padding(2);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(146, 20);
            this.tbName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 10);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 359);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.statusStrip1.Size = new System.Drawing.Size(556, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabelGeneralPurpose
            // 
            this.statusLabelGeneralPurpose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabelGeneralPurpose.Name = "statusLabelGeneralPurpose";
            this.statusLabelGeneralPurpose.Size = new System.Drawing.Size(10, 17);
            this.statusLabelGeneralPurpose.Text = " ";
            // 
            // statusLabelSpacer
            // 
            this.statusLabelSpacer.Name = "statusLabelSpacer";
            this.statusLabelSpacer.Size = new System.Drawing.Size(10, 17);
            this.statusLabelSpacer.Text = " ";
            // 
            // statusLabelFlambeLink
            // 
            this.statusLabelFlambeLink.IsLink = true;
            this.statusLabelFlambeLink.Name = "statusLabelFlambeLink";
            this.statusLabelFlambeLink.Size = new System.Drawing.Size(64, 17);
            this.statusLabelFlambeLink.Text = "Flambe 3.0";
            this.statusLabelFlambeLink.Click += new System.EventHandler(this.toolStripStatusLabel2_Click);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 381);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(408, 401);
            this.Name = "formMain";
            this.Text = "Flambe";
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