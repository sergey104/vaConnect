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
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.installProtocolHandler = new System.Windows.Forms.ToolStripButton();
            this.removeProtocolHandler = new System.Windows.Forms.ToolStripButton();
            this.requestList = new System.Windows.Forms.ListView();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chSubject = new System.Windows.Forms.ColumnHeader();
            this.chDate = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.showWebButton = new System.Windows.Forms.ToolStripButton();
            this.newRequest = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
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
            this.newRequest,
            this.installProtocolHandler,
            this.removeProtocolHandler,
            this.showWebButton,
            this.helpToolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(556, 39);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.helpToolStripButton.Text = "About VaConnect";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
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
            // requestList
            // 
            this.requestList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chSubject,
            this.chDate,
            this.chStatus});
            this.requestList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.requestList.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requestList.FullRowSelect = true;
            this.requestList.Location = new System.Drawing.Point(0, 39);
            this.requestList.MultiSelect = false;
            this.requestList.Name = "requestList";
            this.requestList.Size = new System.Drawing.Size(556, 328);
            this.requestList.TabIndex = 2;
            this.requestList.UseCompatibleStateImageBehavior = false;
            this.requestList.View = System.Windows.Forms.View.Details;
            this.requestList.DoubleClick += new System.EventHandler(this.requestList_DoubleClick);
            // 
            // chID
            // 
            this.chID.Text = "ID";
            // 
            // chSubject
            // 
            this.chSubject.Text = "Subject";
            this.chSubject.Width = 234;
            // 
            // chDate
            // 
            this.chDate.Text = "Date";
            this.chDate.Width = 173;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Closed";
            this.chStatus.Width = 74;
            // 
            // showWebButton
            // 
            this.showWebButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showWebButton.Image = ((System.Drawing.Image)(resources.GetObject("showWebButton.Image")));
            this.showWebButton.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.showWebButton.Name = "showWebButton";
            this.showWebButton.Size = new System.Drawing.Size(36, 36);
            this.showWebButton.Text = "Open VaConnect Web Application";
            this.showWebButton.Click += new System.EventHandler(this.showWebButton_Click);
            // 
            // newRequest
            // 
            this.newRequest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newRequest.Image = ((System.Drawing.Image)(resources.GetObject("newRequest.Image")));
            this.newRequest.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.newRequest.Name = "newRequest";
            this.newRequest.Size = new System.Drawing.Size(36, 36);
            this.newRequest.Text = "New Request";
            this.newRequest.Click += new System.EventHandler(this.newRequest_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 389);
            this.Controls.Add(this.requestList);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "VaConnect Application v1.0";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ListView requestList;
        private System.Windows.Forms.ColumnHeader chSubject;
        private System.Windows.Forms.ColumnHeader chDate;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ToolStripButton installProtocolHandler;
        private System.Windows.Forms.ToolStripButton removeProtocolHandler;
        private System.Windows.Forms.ToolStripButton showWebButton;
        private System.Windows.Forms.ToolStripButton newRequest;

    }
}

