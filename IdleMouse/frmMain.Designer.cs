namespace IdleMouse
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txtTimeoutValue = new System.Windows.Forms.NumericUpDown();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.cmbAnimation = new System.Windows.Forms.ComboBox();
            this.animationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblTimeoutUnit = new System.Windows.Forms.Label();
            this.movementPreview = new IdleMouse.Controls.MovementPreviewControl();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.idleStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSep1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.idleTimeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSep2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.locationStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblAnimationArea = new System.Windows.Forms.Label();
            this.lblBy1 = new System.Windows.Forms.Label();
            this.txtAnimationWidth = new System.Windows.Forms.NumericUpDown();
            this.txtAnimationHeight = new System.Windows.Forms.NumericUpDown();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.lblAnimationSpeed = new System.Windows.Forms.Label();
            this.trkAnimationSpeed = new System.Windows.Forms.TrackBar();
            this.lblSpeedFaster = new System.Windows.Forms.Label();
            this.lblSpeedSlower = new System.Windows.Forms.Label();
            this.chkDisableScreensaver = new System.Windows.Forms.CheckBox();
            this._IdleTimer = new System.Windows.Forms.Timer(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.tableLayoutPanelSettings = new System.Windows.Forms.TableLayoutPanel();
            this.lblAnimation = new System.Windows.Forms.Label();
            this.flowLayoutPanelAnimSize = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanelAnimSpeed = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelTimeout = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCursor = new System.Windows.Forms.Label();
            this.btnCursor = new IdleMouse.Controls.IconButton();
            this.btnManageAnimations = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimeoutValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.animationBindingSource)).BeginInit();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnimationWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnimationHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkAnimationSpeed)).BeginInit();
            this.tableLayoutPanelSettings.SuspendLayout();
            this.flowLayoutPanelAnimSize.SuspendLayout();
            this.tableLayoutPanelAnimSpeed.SuspendLayout();
            this.flowLayoutPanelTimeout.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTimeoutValue
            // 
            this.txtTimeoutValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimeoutValue.Location = new System.Drawing.Point(3, 3);
            this.txtTimeoutValue.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.txtTimeoutValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtTimeoutValue.Name = "txtTimeoutValue";
            this.txtTimeoutValue.Size = new System.Drawing.Size(50, 20);
            this.txtTimeoutValue.TabIndex = 2;
            this.txtTimeoutValue.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.txtTimeoutValue.ValueChanged += new System.EventHandler(this.txtTimeoutValue_ValueChanged);
            // 
            // lblTimeout
            // 
            this.lblTimeout.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeout.Location = new System.Drawing.Point(13, 6);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(52, 13);
            this.lblTimeout.TabIndex = 3;
            this.lblTimeout.Text = "Timeout";
            // 
            // cmbAnimation
            // 
            this.cmbAnimation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAnimation.DataSource = this.animationBindingSource;
            this.cmbAnimation.DisplayMember = "Name";
            this.cmbAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnimation.FormattingEnabled = true;
            this.cmbAnimation.Location = new System.Drawing.Point(71, 29);
            this.cmbAnimation.Name = "cmbAnimation";
            this.cmbAnimation.Size = new System.Drawing.Size(176, 21);
            this.cmbAnimation.TabIndex = 4;
            // 
            // animationBindingSource
            // 
            this.animationBindingSource.AllowNew = false;
            this.animationBindingSource.DataSource = typeof(IdleMouse.Models.Animation);
            // 
            // lblTimeoutUnit
            // 
            this.lblTimeoutUnit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTimeoutUnit.AutoSize = true;
            this.lblTimeoutUnit.Location = new System.Drawing.Point(59, 6);
            this.lblTimeoutUnit.Name = "lblTimeoutUnit";
            this.lblTimeoutUnit.Size = new System.Drawing.Size(47, 13);
            this.lblTimeoutUnit.TabIndex = 5;
            this.lblTimeoutUnit.Text = "seconds";
            // 
            // movementPreview
            // 
            this.movementPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.movementPreview.Location = new System.Drawing.Point(268, 35);
            this.movementPreview.Name = "movementPreview";
            this.movementPreview.Size = new System.Drawing.Size(104, 210);
            this.movementPreview.TabIndex = 0;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.idleStatusLabel,
            this.statusSep1,
            this.idleTimeStatusLabel,
            this.statusSep2,
            this.locationStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 339);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.Size = new System.Drawing.Size(384, 22);
            this.statusStrip.TabIndex = 7;
            // 
            // idleStatusLabel
            // 
            this.idleStatusLabel.Image = global::IdleMouse.Properties.Resources.user;
            this.idleStatusLabel.Name = "idleStatusLabel";
            this.idleStatusLabel.Size = new System.Drawing.Size(16, 17);
            // 
            // statusSep1
            // 
            this.statusSep1.Name = "statusSep1";
            this.statusSep1.Size = new System.Drawing.Size(10, 17);
            this.statusSep1.Text = "|";
            // 
            // idleTimeStatusLabel
            // 
            this.idleTimeStatusLabel.Name = "idleTimeStatusLabel";
            this.idleTimeStatusLabel.Size = new System.Drawing.Size(43, 17);
            this.idleTimeStatusLabel.Text = "0:00:00";
            // 
            // statusSep2
            // 
            this.statusSep2.Name = "statusSep2";
            this.statusSep2.Size = new System.Drawing.Size(10, 17);
            this.statusSep2.Text = "|";
            // 
            // locationStatusLabel
            // 
            this.locationStatusLabel.Name = "locationStatusLabel";
            this.locationStatusLabel.Size = new System.Drawing.Size(25, 17);
            this.locationStatusLabel.Text = "0, 0";
            // 
            // lblAnimationArea
            // 
            this.lblAnimationArea.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAnimationArea.AutoSize = true;
            this.lblAnimationArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnimationArea.Location = new System.Drawing.Point(16, 59);
            this.lblAnimationArea.Name = "lblAnimationArea";
            this.lblAnimationArea.Size = new System.Drawing.Size(49, 13);
            this.lblAnimationArea.TabIndex = 8;
            this.lblAnimationArea.Text = "Bounds";
            this.lblAnimationArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBy1
            // 
            this.lblBy1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBy1.AutoSize = true;
            this.lblBy1.Location = new System.Drawing.Point(59, 6);
            this.lblBy1.Name = "lblBy1";
            this.lblBy1.Size = new System.Drawing.Size(12, 13);
            this.lblBy1.TabIndex = 9;
            this.lblBy1.Text = "x";
            // 
            // txtAnimationWidth
            // 
            this.txtAnimationWidth.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAnimationWidth.Location = new System.Drawing.Point(3, 3);
            this.txtAnimationWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtAnimationWidth.Name = "txtAnimationWidth";
            this.txtAnimationWidth.Size = new System.Drawing.Size(50, 20);
            this.txtAnimationWidth.TabIndex = 10;
            this.txtAnimationWidth.ValueChanged += new System.EventHandler(this.txtAnimationWidth_ValueChanged);
            // 
            // txtAnimationHeight
            // 
            this.txtAnimationHeight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAnimationHeight.Location = new System.Drawing.Point(77, 3);
            this.txtAnimationHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtAnimationHeight.Name = "txtAnimationHeight";
            this.txtAnimationHeight.Size = new System.Drawing.Size(50, 20);
            this.txtAnimationHeight.TabIndex = 11;
            this.txtAnimationHeight.ValueChanged += new System.EventHandler(this.txtAnimationHeight_ValueChanged);
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = global::IdleMouse.Properties.Settings.Default.IdleEnabled;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::IdleMouse.Properties.Settings.Default, "IdleEnabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkEnabled.Location = new System.Drawing.Point(12, 12);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkEnabled.TabIndex = 1;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // lblAnimationSpeed
            // 
            this.lblAnimationSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAnimationSpeed.AutoSize = true;
            this.lblAnimationSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnimationSpeed.Location = new System.Drawing.Point(22, 85);
            this.lblAnimationSpeed.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblAnimationSpeed.Name = "lblAnimationSpeed";
            this.lblAnimationSpeed.Size = new System.Drawing.Size(43, 13);
            this.lblAnimationSpeed.TabIndex = 12;
            this.lblAnimationSpeed.Text = "Speed";
            // 
            // trkAnimationSpeed
            // 
            this.trkAnimationSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.trkAnimationSpeed.AutoSize = false;
            this.tableLayoutPanelAnimSpeed.SetColumnSpan(this.trkAnimationSpeed, 2);
            this.trkAnimationSpeed.LargeChange = 1000;
            this.trkAnimationSpeed.Location = new System.Drawing.Point(3, 3);
            this.trkAnimationSpeed.Maximum = 10000;
            this.trkAnimationSpeed.Minimum = 10;
            this.trkAnimationSpeed.Name = "trkAnimationSpeed";
            this.trkAnimationSpeed.Size = new System.Drawing.Size(176, 27);
            this.trkAnimationSpeed.TabIndex = 13;
            this.trkAnimationSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkAnimationSpeed.Value = 10;
            this.trkAnimationSpeed.ValueChanged += new System.EventHandler(this.trkAnimationSpeed_ValueChanged);
            // 
            // lblSpeedFaster
            // 
            this.lblSpeedFaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpeedFaster.AutoSize = true;
            this.lblSpeedFaster.Location = new System.Drawing.Point(143, 33);
            this.lblSpeedFaster.Name = "lblSpeedFaster";
            this.lblSpeedFaster.Size = new System.Drawing.Size(36, 13);
            this.lblSpeedFaster.TabIndex = 14;
            this.lblSpeedFaster.Text = "Faster";
            // 
            // lblSpeedSlower
            // 
            this.lblSpeedSlower.AutoSize = true;
            this.lblSpeedSlower.Location = new System.Drawing.Point(3, 33);
            this.lblSpeedSlower.Name = "lblSpeedSlower";
            this.lblSpeedSlower.Size = new System.Drawing.Size(39, 13);
            this.lblSpeedSlower.TabIndex = 15;
            this.lblSpeedSlower.Text = "Slower";
            // 
            // chkDisableScreensaver
            // 
            this.chkDisableScreensaver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDisableScreensaver.AutoSize = true;
            this.chkDisableScreensaver.Checked = global::IdleMouse.Properties.Settings.Default.DisableScreensaver;
            this.chkDisableScreensaver.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::IdleMouse.Properties.Settings.Default, "DisableScreensaver", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkDisableScreensaver.Location = new System.Drawing.Point(12, 285);
            this.chkDisableScreensaver.Name = "chkDisableScreensaver";
            this.chkDisableScreensaver.Size = new System.Drawing.Size(124, 17);
            this.chkDisableScreensaver.TabIndex = 17;
            this.chkDisableScreensaver.Text = "Disable Screensaver";
            this.chkDisableScreensaver.UseVisualStyleBackColor = true;
            // 
            // _IdleTimer
            // 
            this._IdleTimer.Tick += new System.EventHandler(this._IdleTimer_Tick);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Image = global::IdleMouse.Properties.Resources.stop;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(222, 308);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 28);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "Exit Idle Mouse";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tableLayoutPanelSettings
            // 
            this.tableLayoutPanelSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanelSettings.ColumnCount = 2;
            this.tableLayoutPanelSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSettings.Controls.Add(this.cmbAnimation, 1, 1);
            this.tableLayoutPanelSettings.Controls.Add(this.lblAnimation, 0, 1);
            this.tableLayoutPanelSettings.Controls.Add(this.lblAnimationArea, 0, 2);
            this.tableLayoutPanelSettings.Controls.Add(this.lblTimeout, 0, 0);
            this.tableLayoutPanelSettings.Controls.Add(this.flowLayoutPanelAnimSize, 1, 2);
            this.tableLayoutPanelSettings.Controls.Add(this.lblAnimationSpeed, 0, 3);
            this.tableLayoutPanelSettings.Controls.Add(this.tableLayoutPanelAnimSpeed, 1, 3);
            this.tableLayoutPanelSettings.Controls.Add(this.flowLayoutPanelTimeout, 1, 0);
            this.tableLayoutPanelSettings.Controls.Add(this.lblCursor, 0, 4);
            this.tableLayoutPanelSettings.Controls.Add(this.btnCursor, 1, 4);
            this.tableLayoutPanelSettings.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::IdleMouse.Properties.Settings.Default, "IdleEnabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tableLayoutPanelSettings.Enabled = global::IdleMouse.Properties.Settings.Default.IdleEnabled;
            this.tableLayoutPanelSettings.Location = new System.Drawing.Point(12, 35);
            this.tableLayoutPanelSettings.Name = "tableLayoutPanelSettings";
            this.tableLayoutPanelSettings.RowCount = 6;
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSettings.Size = new System.Drawing.Size(250, 244);
            this.tableLayoutPanelSettings.TabIndex = 19;
            // 
            // lblAnimation
            // 
            this.lblAnimation.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAnimation.AutoSize = true;
            this.lblAnimation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnimation.Location = new System.Drawing.Point(3, 33);
            this.lblAnimation.Name = "lblAnimation";
            this.lblAnimation.Size = new System.Drawing.Size(62, 13);
            this.lblAnimation.TabIndex = 9;
            this.lblAnimation.Text = "Animation";
            this.lblAnimation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanelAnimSize
            // 
            this.flowLayoutPanelAnimSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelAnimSize.AutoSize = true;
            this.flowLayoutPanelAnimSize.Controls.Add(this.txtAnimationWidth);
            this.flowLayoutPanelAnimSize.Controls.Add(this.lblBy1);
            this.flowLayoutPanelAnimSize.Controls.Add(this.txtAnimationHeight);
            this.flowLayoutPanelAnimSize.Location = new System.Drawing.Point(68, 53);
            this.flowLayoutPanelAnimSize.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelAnimSize.Name = "flowLayoutPanelAnimSize";
            this.flowLayoutPanelAnimSize.Size = new System.Drawing.Size(182, 26);
            this.flowLayoutPanelAnimSize.TabIndex = 10;
            // 
            // tableLayoutPanelAnimSpeed
            // 
            this.tableLayoutPanelAnimSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelAnimSpeed.ColumnCount = 2;
            this.tableLayoutPanelAnimSpeed.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelAnimSpeed.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelAnimSpeed.Controls.Add(this.lblSpeedFaster, 1, 1);
            this.tableLayoutPanelAnimSpeed.Controls.Add(this.lblSpeedSlower, 0, 1);
            this.tableLayoutPanelAnimSpeed.Controls.Add(this.trkAnimationSpeed, 0, 0);
            this.tableLayoutPanelAnimSpeed.Location = new System.Drawing.Point(68, 79);
            this.tableLayoutPanelAnimSpeed.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelAnimSpeed.Name = "tableLayoutPanelAnimSpeed";
            this.tableLayoutPanelAnimSpeed.RowCount = 2;
            this.tableLayoutPanelAnimSpeed.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAnimSpeed.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAnimSpeed.Size = new System.Drawing.Size(182, 52);
            this.tableLayoutPanelAnimSpeed.TabIndex = 21;
            // 
            // flowLayoutPanelTimeout
            // 
            this.flowLayoutPanelTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelTimeout.AutoSize = true;
            this.flowLayoutPanelTimeout.Controls.Add(this.txtTimeoutValue);
            this.flowLayoutPanelTimeout.Controls.Add(this.lblTimeoutUnit);
            this.flowLayoutPanelTimeout.Location = new System.Drawing.Point(68, 0);
            this.flowLayoutPanelTimeout.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelTimeout.Name = "flowLayoutPanelTimeout";
            this.flowLayoutPanelTimeout.Size = new System.Drawing.Size(182, 26);
            this.flowLayoutPanelTimeout.TabIndex = 22;
            // 
            // lblCursor
            // 
            this.lblCursor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCursor.AutoSize = true;
            this.lblCursor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCursor.Location = new System.Drawing.Point(22, 151);
            this.lblCursor.Name = "lblCursor";
            this.lblCursor.Size = new System.Drawing.Size(43, 13);
            this.lblCursor.TabIndex = 23;
            this.lblCursor.Text = "Cursor";
            // 
            // btnCursor
            // 
            this.btnCursor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCursor.CursorIcon = null;
            this.btnCursor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCursor.Location = new System.Drawing.Point(71, 134);
            this.btnCursor.Name = "btnCursor";
            this.btnCursor.Size = new System.Drawing.Size(48, 48);
            this.btnCursor.TabIndex = 24;
            this.btnCursor.Text = global::IdleMouse.Properties.Resources.None;
            this.btnCursor.UseVisualStyleBackColor = true;
            this.btnCursor.Click += new System.EventHandler(this.btnCursor_Click);
            // 
            // btnManageAnimations
            // 
            this.btnManageAnimations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnManageAnimations.Image = global::IdleMouse.Properties.Resources.cog;
            this.btnManageAnimations.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManageAnimations.Location = new System.Drawing.Point(12, 308);
            this.btnManageAnimations.Name = "btnManageAnimations";
            this.btnManageAnimations.Size = new System.Drawing.Size(150, 28);
            this.btnManageAnimations.TabIndex = 5;
            this.btnManageAnimations.Text = "Manage Animations";
            this.btnManageAnimations.UseVisualStyleBackColor = true;
            this.btnManageAnimations.Click += new System.EventHandler(this.btnManageAnimations_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.btnManageAnimations);
            this.Controls.Add(this.tableLayoutPanelSettings);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.chkDisableScreensaver);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.movementPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "frmMain";
            this.Text = "Idle Mouse";
            ((System.ComponentModel.ISupportInitialize)(this.txtTimeoutValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.animationBindingSource)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnimationWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnimationHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkAnimationSpeed)).EndInit();
            this.tableLayoutPanelSettings.ResumeLayout(false);
            this.tableLayoutPanelSettings.PerformLayout();
            this.flowLayoutPanelAnimSize.ResumeLayout(false);
            this.flowLayoutPanelAnimSize.PerformLayout();
            this.tableLayoutPanelAnimSpeed.ResumeLayout(false);
            this.tableLayoutPanelAnimSpeed.PerformLayout();
            this.flowLayoutPanelTimeout.ResumeLayout(false);
            this.flowLayoutPanelTimeout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IdleMouse.Controls.MovementPreviewControl movementPreview;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.NumericUpDown txtTimeoutValue;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.ComboBox cmbAnimation;
        private System.Windows.Forms.Label lblTimeoutUnit;
        private System.Windows.Forms.BindingSource animationBindingSource;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel idleStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel locationStatusLabel;
        private System.Windows.Forms.Label lblAnimationArea;
        private System.Windows.Forms.Label lblBy1;
        private System.Windows.Forms.NumericUpDown txtAnimationWidth;
        private System.Windows.Forms.NumericUpDown txtAnimationHeight;
        private System.Windows.Forms.Label lblAnimationSpeed;
        private System.Windows.Forms.TrackBar trkAnimationSpeed;
        private System.Windows.Forms.Label lblSpeedFaster;
        private System.Windows.Forms.Label lblSpeedSlower;
        private System.Windows.Forms.CheckBox chkDisableScreensaver;
        private System.Windows.Forms.ToolStripStatusLabel idleTimeStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusSep1;
        private System.Windows.Forms.ToolStripStatusLabel statusSep2;
        private System.Windows.Forms.Timer _IdleTimer;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAnimSpeed;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSettings;
        private System.Windows.Forms.Label lblAnimation;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAnimSize;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTimeout;
        private System.Windows.Forms.Label lblCursor;
        private IdleMouse.Controls.IconButton btnCursor;
        private System.Windows.Forms.Button btnManageAnimations;
    }
}

