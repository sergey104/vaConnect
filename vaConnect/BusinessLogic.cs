// Copyright (c) 2007, Jonas Follesø
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the Jonas Follesø nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY Jonas Follesø ``AS IS'' AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL Jonas Follesø BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

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
