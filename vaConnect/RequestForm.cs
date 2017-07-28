

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vaConnect
{
    /// <summary>
    /// Simple form used to update a VaConnect Request.
    /// </summary>
    public partial class RequestForm : Form
    {
        private VaConnectRequest request;

        /// <summary>
        /// Default constructor. Creates the RequestForm and loads it with
        /// a VaConnectRequest object.
        /// </summary>
        /// <param name="request">The request you wan't to load.</param>
        public RequestForm(VaConnectRequest request)
        {
            InitializeComponent();
            this.request = request;
            this.txtSubject.Text = request.Subject;
            this.lblDate.Text = request.Date.ToString();
            this.chkDone.Checked = request.Closed;
        }

        /// <summary>
        /// When the user clicks the OK button to close the dialog.
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            request.Closed = chkDone.Checked;
            request.Subject = this.txtSubject.Text;
            Close();
        }
    }
}