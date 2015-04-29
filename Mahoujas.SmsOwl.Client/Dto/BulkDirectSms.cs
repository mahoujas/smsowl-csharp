using System.Collections.Generic;

namespace Mahoujas.SmsOwl.Client.Dto
{
    internal class BulkDirectSms : DirectSms
    {
        public IList<string> To { get; set; }
    }
}
