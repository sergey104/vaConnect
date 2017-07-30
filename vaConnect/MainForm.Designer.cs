namespace vaConnect
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.installProtocolHandler = new System.Windows.Forms.ToolStripButton();
            this.removeProtocolHandler = new System.Windows.Forms.ToolStripButton();
            this.showWebButton = new System.Windows.Forms.ToolStripButton();
            this.helpButton = new System.Windows.Forms.ToolStripButton();
            this.requestList = new System.Windows.Forms.ListView();
            this.chID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSubject = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 367);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(556, 22);
            this.statusStrip.TabIndex = 0;
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.installProtocolHandler,
            this.removeProtocolHandler,
            this.showWebButton,
            this.helpButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(556, 39);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // installProtocolHandler
            // 
            this.installProtocolHandler.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.installProtocolHandler.Image = ((System.Drawing.Image)(resources.GetObject("installProtocolHandler.Image")));
            this.installProtocolHandler.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.installProtocolHandler.Name = "installProtocolHandler";
            this.installProtocolHandler.Size = new System.Drawing.Size(36, 36);
            this.installProtocolHandler.Text = "Register Protocol Handler";
            this.installProtocolHandler.Click += new System.EventHandler(this.installProtocolHandler_Click);
            // 
            // removeProtocolHandler
            // 
            this.removeProtocolHandler.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeProtocolHandler.Image = ((System.Drawing.Image)(resources.GetObject("removeProtocolHandler.Image")));
            this.removeProtocolHandler.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.removeProtocolHandler.Name = "removeProtocolHandler";
            this.removeProtocolHandler.Size = new System.Drawing.Size(36, 36);
            this.removeProtocolHandler.Text = "Remove Protocol Handler";
            this.removeProtocolHandler.Click += new System.EventHandler(this.removeProtocolHandler_Click);
            // 
            // showWebButton
            // 
            this.showWebButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showWebButton.Image = ((System.Drawing.Image)(resources.GetObject("showWebButton.Image")));
            this.showWebButton.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.showWebButton.Name = "showWebButton";
            this.showWebButton.Size = new System.Drawing.Size(36, 36);
            this.showWebButton.Text = "Scan WiFi networks";
            this.showWebButton.Click += new System.EventHandler(this.showWebButton_Click);
            // 
            // helpButton
            // 
            this.helpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpButton.Image = ((System.Drawing.Image)(resources.GetObject("helpButton.Image")));
            this.helpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(36, 36);
            this.helpButton.Text = "Help";
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // requestList
            // 
            this.requestList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chSubject});
            this.requestList.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requestList.FullRowSelect = true;
            this.requestList.Location = new System.Drawing.Point(12, 88);
            this.requestList.MultiSelect = false;
            this.requestList.Name = "requestList";
            this.requestList.Size = new System.Drawing.Size(356, 205);
            this.requestList.TabIndex = 2;
            this.requestList.UseCompatibleStateImageBehavior = false;
            this.requestList.View = System.Windows.Forms.View.Details;
            this.requestList.DoubleClick += new System.EventHandler(this.requestList_DoubleClick);
            // 
            // chID
            // 
            this.chID.Text = "SID";
            this.chID.Width = 170;
            // 
            // chSubject
            // 
            this.chSubject.Text = "Net Security";
            this.chSubject.Width = 179;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::vaConnect.Properties.Resources.fon_logo;
            this.pictureBox1.Location = new System.Drawing.Point(444, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 90);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "WiFi networks found";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gray;
            this.button1.Location = new System.Drawing.Point(209, 327);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 389);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.requestList);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "VaConnect Application v1.0";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ListView requestList;
        private System.Windows.Forms.ColumnHeader chSubject;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ToolStripButton installProtocolHandler;
        private System.Windows.Forms.ToolStripButton removeProtocolHandler;
        private System.Windows.Forms.ToolStripButton showWebButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton helpButton;
        private System.Windows.Forms.Button button1;
    }
}

