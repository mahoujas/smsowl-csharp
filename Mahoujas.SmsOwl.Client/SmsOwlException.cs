using System;

namespace Mahoujas.SmsOwl.Client
{
    public class SmsOwlException : ApplicationException
    {
        public SmsOwlException(string message) : base(message)
        {
        }
    }
}
