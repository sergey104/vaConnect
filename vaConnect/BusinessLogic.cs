
using System;
using System.Collections.Generic;
using System.Text;

namespace vaConnect
{
    /// <summary>
    /// Simple class used to provide some basic sample data to the application.
    /// </summary>
    public class BusinessLogic
    {
        private List<VaConnectRequest> requests;

        /// <summary>
        /// A collection of VaConnectRequests.
        /// </summary>
        public List<VaConnectRequest> RequestDatabase
        {
            get { return requests; }
        }

        /// <summary>
        /// Default constructor. Creates a list of 10 VaConnect Requests.
        /// </summary>
        public BusinessLogic()
        {
            requests = new List<VaConnectRequest>();
            for (int i = 0; i < 10; ++i)
            {
                VaConnectRequest request = new VaConnectRequest();
                request.ID = (10 - i);
                request.Subject = "VaConnect Request #" + (10 - i).ToString();
                request.Date = DateTime.Now.Subtract(new TimeSpan(i, 0, 0));
                request.Closed = false;
                requests.Add(request);
            }
        }
    }
}
