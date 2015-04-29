using System.Collections.Generic;

namespace Mahoujas.SmsOwl.Client.Dto
{
    internal class BulkSmsSuccessResult
    {
        public string Status { get; set; }
        public IList<string> SmsIds { get; set; }
    }
}
