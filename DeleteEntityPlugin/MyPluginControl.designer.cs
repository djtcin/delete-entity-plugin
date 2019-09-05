namespace DeleteEntityPlugin
{
    partial class MyPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            //Service.Execute()
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmbEntities = new System.Windows.Forms.ComboBox();
            this.labelEntity = new System.Windows.Forms.Label();
            this.labelAction = new System.Windows.Forms.Label();
            this.cmbActions = new System.Windows.Forms.ComboBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.boxDependencies = new System.Windows.Forms.GroupBox();
            this.panelDependencies = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelAttributes = new System.Windows.Forms.Label();
            this.cmbAttributes = new System.Windows.Forms.ComboBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.toolStripMenu.SuspendLayout();
            this.boxDependencies.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(746, 27);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(107, 24);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // cmbEntities
            // 
            this.cmbEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEntities.Enabled = false;
            this.cmbEntities.FormattingEnabled = true;
            this.cmbEntities.Location = new System.Drawing.Point(80, 32);
            this.cmbEntities.Name = "cmbEntities";
            this.cmbEntities.Size = new System.Drawing.Size(641, 24);
            this.cmbEntities.TabIndex = 1;
            this.cmbEntities.SelectedIndexChanged += new System.EventHandler(this.cmbEntities_SelectedIndexChanged);
            this.cmbEntities.TextUpdate += new System.EventHandler(this.cmbEntities_TextUpdate);
            // 
            // labelEntity
            // 
            this.labelEntity.AutoSize = true;
            this.labelEntity.Location = new System.Drawing.Point(13, 35);
            this.labelEntity.Name = "labelEntity";
            this.labelEntity.Size = new System.Drawing.Size(47, 17);
            this.labelEntity.TabIndex = 0;
            this.labelEntity.Text = "Entity:";
            // 
            // labelAction
            // 
            this.labelAction.AutoSize = true;
            this.labelAction.Location = new System.Drawing.Point(13, 65);
            this.labelAction.Name = "labelAction";
            this.labelAction.Size = new System.Drawing.Size(51, 17);
            this.labelAction.TabIndex = 5;
            this.labelAction.Text = "Action:";
            // 
            // cmbActions
            // 
            this.cmbActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbActions.Enabled = false;
            this.cmbActions.FormattingEnabled = true;
            this.cmbActions.Location = new System.Drawing.Point(80, 62);
            this.cmbActions.Name = "cmbActions";
            this.cmbActions.Size = new System.Drawing.Size(641, 24);
            this.cmbActions.TabIndex = 6;
            this.cmbActions.SelectedIndexChanged += new System.EventHandler(this.cmbActions_SelectedIndexChanged);
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(16, 122);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(184, 24);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "Search for dependencies";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // boxDependencies
            // 
            this.boxDependencies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxDependencies.Controls.Add(this.panelDependencies);
            this.boxDependencies.Location = new System.Drawing.Point(16, 152);
            this.boxDependencies.Name = "boxDependencies";
            this.boxDependencies.Size = new System.Drawing.Size(705, 70);
            this.boxDependencies.TabIndex = 9;
            this.boxDependencies.TabStop = false;
            this.boxDependencies.Text = "Dependencies";
            // 
            // panelDependencies
            // 
            this.panelDependencies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDependencies.AutoScroll = true;
            this.panelDependencies.Location = new System.Drawing.Point(6, 21);
            this.panelDependencies.Name = "panelDependencies";
            this.panelDependencies.Size = new System.Drawing.Size(693, 43);
            this.panelDependencies.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(16, 228);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(705, 129);
            this.textBox1.TabIndex = 10;
            // 
            // labelAttributes
            // 
            this.labelAttributes.AutoSize = true;
            this.labelAttributes.Enabled = false;
            this.labelAttributes.Location = new System.Drawing.Point(13, 95);
            this.labelAttributes.Name = "labelAttributes";
            this.labelAttributes.Size = new System.Drawing.Size(61, 17);
            this.labelAttributes.TabIndex = 11;
            this.labelAttributes.Text = "Atribute:";
            // 
            // cmbAttributes
            // 
            this.cmbAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAttributes.Enabled = false;
            this.cmbAttributes.FormattingEnabled = true;
            this.cmbAttributes.Location = new System.Drawing.Point(80, 92);
            this.cmbAttributes.Name = "cmbAttributes";
            this.cmbAttributes.Size = new System.Drawing.Size(641, 24);
            this.cmbAttributes.TabIndex = 12;
            // 
            // btnExecute
            // 
            this.btnExecute.Enabled = false;
            this.btnExecute.Location = new System.Drawing.Point(206, 122);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(164, 24);
            this.btnExecute.TabIndex = 13;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.BtnExecute_Click);
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.cmbAttributes);
            this.Controls.Add(this.labelAttributes);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.boxDependencies);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.cmbActions);
            this.Controls.Add(this.labelAction);
            this.Controls.Add(this.labelEntity);
            this.Controls.Add(this.cmbEntities);
            this.Controls.Add(this.toolStripMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(746, 370);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.boxDependencies.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.ComboBox cmbEntities;
        private System.Windows.Forms.Label labelEntity;
        private System.Windows.Forms.Label labelAction;
        private System.Windows.Forms.ComboBox cmbActions;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.GroupBox boxDependencies;
        private System.Windows.Forms.Panel panelDependencies;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelAttributes;
        private System.Windows.Forms.ComboBox cmbAttributes;
        private System.Windows.Forms.Button btnExecute;
    }
}
