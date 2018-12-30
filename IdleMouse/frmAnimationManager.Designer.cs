namespace IdleMouse
{
    partial class frmAnimationManager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnimationManager));
            this.listBoxAnimations = new System.Windows.Forms.ListBox();
            this.animationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.animationEditorControl = new IdleMouse.Controls.AnimationEditorControl();
            this.grpEditAnimation = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblPreviewSpeed = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblInterpolation = new System.Windows.Forms.Label();
            this.cmbAnimFunction = new System.Windows.Forms.ComboBox();
            this.cmbAnimType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.layoutEditorToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonPointer = new IdleMouse.Controls.ToolStripRadioButton();
            this.toolStripButtonPreview = new IdleMouse.Controls.ToolStripRadioButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonReverse = new IdleMouse.Controls.BindableToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAddPoint = new IdleMouse.Controls.ToolStripRadioButton();
            this.toolStripButtonRemovePoint = new IdleMouse.Controls.ToolStripRadioButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonInterpolationModeNormal = new IdleMouse.Controls.ToolStripRadioButton();
            this.toolStripButtonInterpolationModeAlternate = new IdleMouse.Controls.ToolStripRadioButton();
            this.toolStripButtonInterpolationModeRepeat = new IdleMouse.Controls.ToolStripRadioButton();
            this.trkPreviewSpeed = new System.Windows.Forms.TrackBar();
            this.grpAnimations = new System.Windows.Forms.GroupBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.animationBindingSource)).BeginInit();
            this.grpEditAnimation.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.layoutEditorToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkPreviewSpeed)).BeginInit();
            this.grpAnimations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxAnimations
            // 
            this.listBoxAnimations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxAnimations.DataSource = this.animationBindingSource;
            this.listBoxAnimations.DisplayMember = "Name";
            this.listBoxAnimations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAnimations.FormattingEnabled = true;
            this.listBoxAnimations.IntegralHeight = false;
            this.listBoxAnimations.Location = new System.Drawing.Point(3, 16);
            this.listBoxAnimations.Name = "listBoxAnimations";
            this.listBoxAnimations.Size = new System.Drawing.Size(194, 369);
            this.listBoxAnimations.TabIndex = 0;
            // 
            // animationBindingSource
            // 
            this.animationBindingSource.DataSource = typeof(IdleMouse.Models.Animation);
            this.animationBindingSource.CurrentItemChanged += new System.EventHandler(this.animationBindingSource_CurrentItemChanged);
            this.animationBindingSource.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.animationBindingSource_ListChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(713, 411);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "OK";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Image = global::IdleMouse.Properties.Resources.toggle;
            this.btnRemove.Location = new System.Drawing.Point(12, 406);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(32, 32);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Image = global::IdleMouse.Properties.Resources.toggle_expand;
            this.btnAdd.Location = new System.Drawing.Point(180, 406);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(32, 32);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // animationEditorControl
            // 
            this.animationEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.animationEditorControl.Animation = null;
            this.tableLayoutPanel.SetColumnSpan(this.animationEditorControl, 2);
            this.animationEditorControl.Location = new System.Drawing.Point(3, 108);
            this.animationEditorControl.Mode = IdleMouse.Controls.AnimationEditorMode.Select;
            this.animationEditorControl.Name = "animationEditorControl";
            this.animationEditorControl.Size = new System.Drawing.Size(558, 231);
            this.animationEditorControl.TabIndex = 4;
            // 
            // grpEditAnimation
            // 
            this.grpEditAnimation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEditAnimation.Controls.Add(this.tableLayoutPanel);
            this.grpEditAnimation.Location = new System.Drawing.Point(218, 12);
            this.grpEditAnimation.Name = "grpEditAnimation";
            this.grpEditAnimation.Size = new System.Drawing.Size(570, 388);
            this.grpEditAnimation.TabIndex = 5;
            this.grpEditAnimation.TabStop = false;
            this.grpEditAnimation.Text = "Edit Animation";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.lblPreviewSpeed, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.lblInterpolation, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.cmbAnimFunction, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.cmbAnimType, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.lblType, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.txtName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.animationEditorControl, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.layoutEditorToolStrip, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.trkPreviewSpeed, 1, 5);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 6;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(564, 369);
            this.tableLayoutPanel.TabIndex = 9;
            // 
            // lblPreviewSpeed
            // 
            this.lblPreviewSpeed.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPreviewSpeed.AutoSize = true;
            this.lblPreviewSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviewSpeed.Location = new System.Drawing.Point(3, 349);
            this.lblPreviewSpeed.Name = "lblPreviewSpeed";
            this.lblPreviewSpeed.Size = new System.Drawing.Size(92, 13);
            this.lblPreviewSpeed.TabIndex = 7;
            this.lblPreviewSpeed.Text = "Preview Speed";
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(56, 6);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 13);
            this.lblName.TabIndex = 10;
            this.lblName.Text = "Name";
            // 
            // lblInterpolation
            // 
            this.lblInterpolation.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblInterpolation.AutoSize = true;
            this.lblInterpolation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInterpolation.Location = new System.Drawing.Point(17, 60);
            this.lblInterpolation.Name = "lblInterpolation";
            this.lblInterpolation.Size = new System.Drawing.Size(78, 13);
            this.lblInterpolation.TabIndex = 7;
            this.lblInterpolation.Text = "Interpolation";
            // 
            // cmbAnimFunction
            // 
            this.cmbAnimFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAnimFunction.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.animationBindingSource, "EasingFunction", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbAnimFunction.DisplayMember = "Value";
            this.cmbAnimFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnimFunction.FormattingEnabled = true;
            this.cmbAnimFunction.Location = new System.Drawing.Point(101, 56);
            this.cmbAnimFunction.Name = "cmbAnimFunction";
            this.cmbAnimFunction.Size = new System.Drawing.Size(460, 21);
            this.cmbAnimFunction.TabIndex = 8;
            this.cmbAnimFunction.ValueMember = "Key";
            // 
            // cmbAnimType
            // 
            this.cmbAnimType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAnimType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.animationBindingSource, "ItemType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbAnimType.DisplayMember = "Value";
            this.cmbAnimType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnimType.FormattingEnabled = true;
            this.cmbAnimType.Location = new System.Drawing.Point(101, 29);
            this.cmbAnimType.Name = "cmbAnimType";
            this.cmbAnimType.Size = new System.Drawing.Size(460, 21);
            this.cmbAnimType.TabIndex = 6;
            this.cmbAnimType.ValueMember = "Key";
            // 
            // lblType
            // 
            this.lblType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(60, 33);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(35, 13);
            this.lblType.TabIndex = 5;
            this.lblType.Text = "Type";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.animationBindingSource, "Name", true));
            this.errorProvider.SetIconPadding(this.txtName, -20);
            this.txtName.Location = new System.Drawing.Point(101, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(460, 20);
            this.txtName.TabIndex = 9;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // layoutEditorToolStrip
            // 
            this.layoutEditorToolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.layoutEditorToolStrip, 2);
            this.layoutEditorToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.layoutEditorToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.layoutEditorToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPointer,
            this.toolStripButtonPreview,
            this.toolStripSeparator1,
            this.toolStripButtonReverse,
            this.toolStripSeparator3,
            this.toolStripButtonAddPoint,
            this.toolStripButtonRemovePoint,
            this.toolStripSeparator2,
            this.toolStripButtonInterpolationModeNormal,
            this.toolStripButtonInterpolationModeAlternate,
            this.toolStripButtonInterpolationModeRepeat});
            this.layoutEditorToolStrip.Location = new System.Drawing.Point(0, 80);
            this.layoutEditorToolStrip.Name = "layoutEditorToolStrip";
            this.layoutEditorToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.layoutEditorToolStrip.Size = new System.Drawing.Size(564, 25);
            this.layoutEditorToolStrip.TabIndex = 14;
            // 
            // toolStripButtonPointer
            // 
            this.toolStripButtonPointer.Checked = true;
            this.toolStripButtonPointer.CheckOnClick = true;
            this.toolStripButtonPointer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonPointer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPointer.Image = global::IdleMouse.Properties.Resources.cursor;
            this.toolStripButtonPointer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPointer.Name = "toolStripButtonPointer";
            this.toolStripButtonPointer.RadioButtonGroupId = 1;
            this.toolStripButtonPointer.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPointer.Tag = IdleMouse.Controls.AnimationEditorMode.Select;
            this.toolStripButtonPointer.ToolTipText = "Select";
            this.toolStripButtonPointer.CheckedChanged += new System.EventHandler(this.toolStripRadioButton_CheckedChanged);
            // 
            // toolStripButtonPreview
            // 
            this.toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPreview.Image = global::IdleMouse.Properties.Resources.magnifier;
            this.toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreview.Name = "toolStripButtonPreview";
            this.toolStripButtonPreview.RadioButtonGroupId = 1;
            this.toolStripButtonPreview.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPreview.Tag = IdleMouse.Controls.AnimationEditorMode.Preview;
            this.toolStripButtonPreview.ToolTipText = "Preview";
            this.toolStripButtonPreview.CheckedChanged += new System.EventHandler(this.toolStripRadioButton_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonReverse
            // 
            this.toolStripButtonReverse.CheckOnClick = true;
            this.toolStripButtonReverse.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.animationBindingSource, "Reverse", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.toolStripButtonReverse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReverse.Image = global::IdleMouse.Properties.Resources.arrow_rotate_anticlockwise;
            this.toolStripButtonReverse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReverse.Name = "toolStripButtonReverse";
            this.toolStripButtonReverse.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonReverse.ToolTipText = "Reverse animation direction";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAddPoint
            // 
            this.toolStripButtonAddPoint.CheckOnClick = true;
            this.toolStripButtonAddPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddPoint.Image = global::IdleMouse.Properties.Resources.vector_add;
            this.toolStripButtonAddPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddPoint.Name = "toolStripButtonAddPoint";
            this.toolStripButtonAddPoint.RadioButtonGroupId = 1;
            this.toolStripButtonAddPoint.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddPoint.Tag = IdleMouse.Controls.AnimationEditorMode.AddPathPoint;
            this.toolStripButtonAddPoint.ToolTipText = "Add node to path";
            this.toolStripButtonAddPoint.CheckedChanged += new System.EventHandler(this.toolStripRadioButton_CheckedChanged);
            // 
            // toolStripButtonRemovePoint
            // 
            this.toolStripButtonRemovePoint.CheckOnClick = true;
            this.toolStripButtonRemovePoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemovePoint.Image = global::IdleMouse.Properties.Resources.vector_delete;
            this.toolStripButtonRemovePoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemovePoint.Name = "toolStripButtonRemovePoint";
            this.toolStripButtonRemovePoint.RadioButtonGroupId = 1;
            this.toolStripButtonRemovePoint.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRemovePoint.Tag = IdleMouse.Controls.AnimationEditorMode.RemovePathPoint;
            this.toolStripButtonRemovePoint.ToolTipText = "Remove node from path";
            this.toolStripButtonRemovePoint.CheckedChanged += new System.EventHandler(this.toolStripRadioButton_CheckedChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonInterpolationModeNormal
            // 
            this.toolStripButtonInterpolationModeNormal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInterpolationModeNormal.Image = global::IdleMouse.Properties.Resources.arrow_right;
            this.toolStripButtonInterpolationModeNormal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInterpolationModeNormal.Name = "toolStripButtonInterpolationModeNormal";
            this.toolStripButtonInterpolationModeNormal.RadioButtonGroupId = 2;
            this.toolStripButtonInterpolationModeNormal.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonInterpolationModeNormal.ToolTipText = "Normal, ignore path segments";
            // 
            // toolStripButtonInterpolationModeAlternate
            // 
            this.toolStripButtonInterpolationModeAlternate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInterpolationModeAlternate.Image = global::IdleMouse.Properties.Resources.arrow_undo;
            this.toolStripButtonInterpolationModeAlternate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInterpolationModeAlternate.Name = "toolStripButtonInterpolationModeAlternate";
            this.toolStripButtonInterpolationModeAlternate.RadioButtonGroupId = 2;
            this.toolStripButtonInterpolationModeAlternate.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonInterpolationModeAlternate.ToolTipText = "Alternate for each path segment";
            // 
            // toolStripButtonInterpolationModeRepeat
            // 
            this.toolStripButtonInterpolationModeRepeat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInterpolationModeRepeat.Image = global::IdleMouse.Properties.Resources.arrow_repeat;
            this.toolStripButtonInterpolationModeRepeat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInterpolationModeRepeat.Name = "toolStripButtonInterpolationModeRepeat";
            this.toolStripButtonInterpolationModeRepeat.RadioButtonGroupId = 2;
            this.toolStripButtonInterpolationModeRepeat.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonInterpolationModeRepeat.ToolTipText = "Repeat for each path segment";
            // 
            // trkPreviewSpeed
            // 
            this.trkPreviewSpeed.AutoSize = false;
            this.trkPreviewSpeed.LargeChange = 1000;
            this.trkPreviewSpeed.Location = new System.Drawing.Point(101, 345);
            this.trkPreviewSpeed.Maximum = 10000;
            this.trkPreviewSpeed.Minimum = 10;
            this.trkPreviewSpeed.Name = "trkPreviewSpeed";
            this.trkPreviewSpeed.Size = new System.Drawing.Size(251, 21);
            this.trkPreviewSpeed.TabIndex = 15;
            this.trkPreviewSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkPreviewSpeed.Value = 10;
            this.trkPreviewSpeed.ValueChanged += new System.EventHandler(this.trkPreviewSpeed_ValueChanged);
            // 
            // grpAnimations
            // 
            this.grpAnimations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpAnimations.Controls.Add(this.listBoxAnimations);
            this.grpAnimations.Location = new System.Drawing.Point(12, 12);
            this.grpAnimations.Name = "grpAnimations";
            this.grpAnimations.Size = new System.Drawing.Size(200, 388);
            this.grpAnimations.TabIndex = 6;
            this.grpAnimations.TabStop = false;
            this.grpAnimations.Text = "Animations";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // frmAnimationManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grpAnimations);
            this.Controls.Add(this.grpEditAnimation);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAnimationManager";
            this.Text = "Animation Manager";
            ((System.ComponentModel.ISupportInitialize)(this.animationBindingSource)).EndInit();
            this.grpEditAnimation.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.layoutEditorToolStrip.ResumeLayout(false);
            this.layoutEditorToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkPreviewSpeed)).EndInit();
            this.grpAnimations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxAnimations;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private IdleMouse.Controls.AnimationEditorControl animationEditorControl;
        private System.Windows.Forms.BindingSource animationBindingSource;
        private System.Windows.Forms.GroupBox grpEditAnimation;
        private System.Windows.Forms.Label lblInterpolation;
        private System.Windows.Forms.ComboBox cmbAnimType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbAnimFunction;
        private System.Windows.Forms.GroupBox grpAnimations;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ToolStrip layoutEditorToolStrip;
        private IdleMouse.Controls.ToolStripRadioButton toolStripButtonPointer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private IdleMouse.Controls.ToolStripRadioButton toolStripButtonAddPoint;
        private IdleMouse.Controls.ToolStripRadioButton toolStripButtonRemovePoint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Controls.ToolStripRadioButton toolStripButtonPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private Controls.BindableToolStripButton toolStripButtonReverse;
        private Controls.ToolStripRadioButton toolStripButtonInterpolationModeNormal;
        private Controls.ToolStripRadioButton toolStripButtonInterpolationModeAlternate;
        private Controls.ToolStripRadioButton toolStripButtonInterpolationModeRepeat;
        private System.Windows.Forms.Label lblPreviewSpeed;
        private System.Windows.Forms.TrackBar trkPreviewSpeed;
    }
}