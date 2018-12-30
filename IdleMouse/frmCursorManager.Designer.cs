namespace IdleMouse
{
    partial class frmCursorManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCursorManager));
            this.btnOK = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBoxPreview = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblFrameCount = new System.Windows.Forms.Label();
            this.lblHotspot = new System.Windows.Forms.Label();
            this.lblFrameCountValue = new System.Windows.Forms.Label();
            this.lblHotspotValue = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblSizeValue = new System.Windows.Forms.Label();
            this.lblTypeValue = new System.Windows.Forms.Label();
            this.animatedCursorPreview = new IdleMouse.Controls.AnimatedCursorPreview();
            this.lblPreviewName = new System.Windows.Forms.Label();
            this.groupBoxListView = new System.Windows.Forms.GroupBox();
            this.cursorsList = new IdleMouse.Controls.DoubleBufferedListBox();
            this.cursorModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnRemove = new System.Windows.Forms.Button();
            this.groupBoxPreview.SuspendLayout();
            this.tableLayoutPanelDetails.SuspendLayout();
            this.groupBoxListView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cursorModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(713, 415);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Cursor files (*.cur;*.ani)|*.cur;*.ani|Animated cursor files (*.ani)|*.ani|All fi" +
    "les|*.*";
            this.openFileDialog.Title = "Choose custom cursor...";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Image = global::IdleMouse.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(12, 415);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add Custom...";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPreview.Controls.Add(this.tableLayoutPanelDetails);
            this.groupBoxPreview.Controls.Add(this.animatedCursorPreview);
            this.groupBoxPreview.Controls.Add(this.lblPreviewName);
            this.groupBoxPreview.Location = new System.Drawing.Point(632, 12);
            this.groupBoxPreview.Name = "groupBoxPreview";
            this.groupBoxPreview.Size = new System.Drawing.Size(156, 397);
            this.groupBoxPreview.TabIndex = 5;
            this.groupBoxPreview.TabStop = false;
            this.groupBoxPreview.Text = "Preview";
            // 
            // tableLayoutPanelDetails
            // 
            this.tableLayoutPanelDetails.ColumnCount = 2;
            this.tableLayoutPanelDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDetails.Controls.Add(this.lblFrameCount, 0, 3);
            this.tableLayoutPanelDetails.Controls.Add(this.lblHotspot, 0, 2);
            this.tableLayoutPanelDetails.Controls.Add(this.lblFrameCountValue, 1, 3);
            this.tableLayoutPanelDetails.Controls.Add(this.lblHotspotValue, 1, 2);
            this.tableLayoutPanelDetails.Controls.Add(this.lblType, 0, 0);
            this.tableLayoutPanelDetails.Controls.Add(this.lblSize, 0, 1);
            this.tableLayoutPanelDetails.Controls.Add(this.lblSizeValue, 1, 1);
            this.tableLayoutPanelDetails.Controls.Add(this.lblTypeValue, 1, 0);
            this.tableLayoutPanelDetails.Location = new System.Drawing.Point(6, 179);
            this.tableLayoutPanelDetails.Name = "tableLayoutPanelDetails";
            this.tableLayoutPanelDetails.RowCount = 5;
            this.tableLayoutPanelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDetails.Size = new System.Drawing.Size(144, 212);
            this.tableLayoutPanelDetails.TabIndex = 2;
            // 
            // lblFrameCount
            // 
            this.lblFrameCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFrameCount.AutoSize = true;
            this.lblFrameCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrameCount.Location = new System.Drawing.Point(7, 60);
            this.lblFrameCount.Margin = new System.Windows.Forms.Padding(3);
            this.lblFrameCount.Name = "lblFrameCount";
            this.lblFrameCount.Size = new System.Drawing.Size(47, 13);
            this.lblFrameCount.TabIndex = 1;
            this.lblFrameCount.Text = "Frames";
            // 
            // lblHotspot
            // 
            this.lblHotspot.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblHotspot.AutoSize = true;
            this.lblHotspot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHotspot.Location = new System.Drawing.Point(3, 41);
            this.lblHotspot.Margin = new System.Windows.Forms.Padding(3);
            this.lblHotspot.Name = "lblHotspot";
            this.lblHotspot.Size = new System.Drawing.Size(51, 13);
            this.lblHotspot.TabIndex = 0;
            this.lblHotspot.Text = "Hotspot";
            // 
            // lblFrameCountValue
            // 
            this.lblFrameCountValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFrameCountValue.AutoSize = true;
            this.lblFrameCountValue.Location = new System.Drawing.Point(60, 60);
            this.lblFrameCountValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblFrameCountValue.Name = "lblFrameCountValue";
            this.lblFrameCountValue.Size = new System.Drawing.Size(13, 13);
            this.lblFrameCountValue.TabIndex = 2;
            this.lblFrameCountValue.Text = "0";
            // 
            // lblHotspotValue
            // 
            this.lblHotspotValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblHotspotValue.AutoSize = true;
            this.lblHotspotValue.Location = new System.Drawing.Point(60, 41);
            this.lblHotspotValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblHotspotValue.Name = "lblHotspotValue";
            this.lblHotspotValue.Size = new System.Drawing.Size(25, 13);
            this.lblHotspotValue.TabIndex = 3;
            this.lblHotspotValue.Text = "0, 0";
            // 
            // lblType
            // 
            this.lblType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(19, 3);
            this.lblType.Margin = new System.Windows.Forms.Padding(3);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(35, 13);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Type";
            // 
            // lblSize
            // 
            this.lblSize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.Location = new System.Drawing.Point(23, 22);
            this.lblSize.Margin = new System.Windows.Forms.Padding(3);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(31, 13);
            this.lblSize.TabIndex = 5;
            this.lblSize.Text = "Size";
            // 
            // lblSizeValue
            // 
            this.lblSizeValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSizeValue.AutoSize = true;
            this.lblSizeValue.Location = new System.Drawing.Point(60, 22);
            this.lblSizeValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblSizeValue.Name = "lblSizeValue";
            this.lblSizeValue.Size = new System.Drawing.Size(13, 13);
            this.lblSizeValue.TabIndex = 6;
            this.lblSizeValue.Text = "0";
            // 
            // lblTypeValue
            // 
            this.lblTypeValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTypeValue.AutoSize = true;
            this.lblTypeValue.Location = new System.Drawing.Point(60, 3);
            this.lblTypeValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblTypeValue.Name = "lblTypeValue";
            this.lblTypeValue.Size = new System.Drawing.Size(27, 13);
            this.lblTypeValue.TabIndex = 7;
            this.lblTypeValue.Text = "N/A";
            // 
            // animatedCursorPreview
            // 
            this.animatedCursorPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.animatedCursorPreview.CursorIcon = null;
            this.animatedCursorPreview.Location = new System.Drawing.Point(6, 19);
            this.animatedCursorPreview.Name = "animatedCursorPreview";
            this.animatedCursorPreview.Size = new System.Drawing.Size(144, 138);
            this.animatedCursorPreview.TabIndex = 1;
            // 
            // lblPreviewName
            // 
            this.lblPreviewName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviewName.Location = new System.Drawing.Point(6, 160);
            this.lblPreviewName.Name = "lblPreviewName";
            this.lblPreviewName.Size = new System.Drawing.Size(144, 16);
            this.lblPreviewName.TabIndex = 0;
            this.lblPreviewName.Text = IdleMouse.Properties.Resources.None;
            this.lblPreviewName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxListView
            // 
            this.groupBoxListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxListView.Controls.Add(this.cursorsList);
            this.groupBoxListView.Location = new System.Drawing.Point(12, 12);
            this.groupBoxListView.Name = "groupBoxListView";
            this.groupBoxListView.Size = new System.Drawing.Size(614, 397);
            this.groupBoxListView.TabIndex = 6;
            this.groupBoxListView.TabStop = false;
            this.groupBoxListView.Text = "Cursors";
            // 
            // cursorsList
            // 
            this.cursorsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cursorsList.DataSource = this.cursorModelBindingSource;
            this.cursorsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cursorsList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cursorsList.FormattingEnabled = true;
            this.cursorsList.IntegralHeight = false;
            this.cursorsList.ItemHeight = 40;
            this.cursorsList.Location = new System.Drawing.Point(3, 16);
            this.cursorsList.Name = "cursorsList";
            this.cursorsList.Size = new System.Drawing.Size(608, 378);
            this.cursorsList.TabIndex = 0;
            this.cursorsList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cursorsList_DrawItem);
            // 
            // cursorModelBindingSource
            // 
            this.cursorModelBindingSource.DataSource = typeof(IdleMouse.Interop.IcoCurAni.CursorModel);
            this.cursorModelBindingSource.PositionChanged += new System.EventHandler(this.cursorModelBindingSource_PositionChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Image = global::IdleMouse.Properties.Resources.delete;
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.Location = new System.Drawing.Point(118, 415);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(100, 23);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frmCursorManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.groupBoxListView);
            this.Controls.Add(this.groupBoxPreview);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "frmCursorManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cursor Manager";
            this.groupBoxPreview.ResumeLayout(false);
            this.tableLayoutPanelDetails.ResumeLayout(false);
            this.tableLayoutPanelDetails.PerformLayout();
            this.groupBoxListView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cursorModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBoxPreview;
        private IdleMouse.Controls.AnimatedCursorPreview animatedCursorPreview;
        private System.Windows.Forms.Label lblPreviewName;
        private System.Windows.Forms.GroupBox groupBoxListView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDetails;
        private System.Windows.Forms.Label lblHotspot;
        private System.Windows.Forms.Label lblFrameCount;
        private System.Windows.Forms.Label lblFrameCountValue;
        private System.Windows.Forms.Label lblHotspotValue;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblSizeValue;
        private System.Windows.Forms.Label lblTypeValue;
        private IdleMouse.Controls.DoubleBufferedListBox cursorsList;
        private System.Windows.Forms.BindingSource cursorModelBindingSource;
        private System.Windows.Forms.Button btnRemove;
    }
}