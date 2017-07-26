namespace HelpDeskClient
{
    partial class RequestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestForm));
            this.lblID = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkDone = new System.Windows.Forms.CheckBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(21, 13);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(26, 19);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "ID";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(21, 36);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(41, 19);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Date";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(21, 117);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 30);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkDone
            // 
            this.chkDone.AutoSize = true;
            this.chkDone.Location = new System.Drawing.Point(21, 90);
            this.chkDone.Name = "chkDone";
            this.chkDone.Size = new System.Drawing.Size(91, 23);
            this.chkDone.TabIndex = 4;
            this.chkDone.Text = "I\'m done";
            this.chkDone.UseVisualStyleBackColor = true;
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(21, 59);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(392, 27);
            this.txtSubject.TabIndex = 5;
            // 
            // RequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 177);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.chkDone);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblID);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RequestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RequestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkDone;
        private System.Windows.Forms.TextBox txtSubject;
    }
}