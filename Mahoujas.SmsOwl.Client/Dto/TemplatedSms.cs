namespace Mahoujas.SmsOwl.Client.Dto
{
    internal class TemplatedSms<T> : SmsBase
    {
        public string TemplateId { get; set; }
        public T Placeholders { get; set; }
    }
}
