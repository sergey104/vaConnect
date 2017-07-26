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

namespace HelpDeskClient
{
    /// <summary>
    /// Simple class representing a HelpDesk Request.
    /// </summary>
    public class HelpDeskRequest
    {
        private int id;

        /// <summary>
        /// The ID of the request.
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
	
        private string subject;

        /// <summary>
        /// The Subject of the request.
        /// </summary>
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        private bool closed;

        /// <summary>
        /// The status of the request.
        /// </summary>
        public bool Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        private DateTime date;

        /// <summary>
        /// The time stamp of the request.
        /// </summary>
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }	
    }
}
