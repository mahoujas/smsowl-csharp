namespace Mahoujas.SmsOwl.Client.Dto
{
    internal class SingleTemplatedSms<T> : TemplatedSms<T>
    {
        public string To { get; set; }
    }
}
