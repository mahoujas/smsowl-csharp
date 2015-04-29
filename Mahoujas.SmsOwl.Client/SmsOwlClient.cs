using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mahoujas.SmsOwl.Client.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Mahoujas.SmsOwl.Client
{
    public class SmsOwlClient
    {
        private readonly string _accountId;
        private readonly string _apiKey;

        private const string Url = "https://smsowl.in/api/v1/sms";

        private static readonly JsonSerializerSettings JsonSerializerSettings;

        static SmsOwlClient()
        {
            JsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public SmsOwlClient(string accountId, string apiKey)
        {
            _accountId = accountId;
            _apiKey = apiKey;
        }

        public async Task<string> SendPromotionalSmsAsync(string senderId ,string to,string message,SmsType smsType = SmsType.Normal)
        {
            var data = new SingleDirectSms
            {
                AccountId = _accountId,
                ApiKey = _apiKey,
                DndType = DndType.Promotional.ToString().ToLower(),
                SmsType = smsType.ToString().ToLower(),
                Message = message,
                SenderId = senderId,
                To = to
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(data,JsonSerializerSettings);
            var result = await httpClient.PostAsync(Url, new StringContent(json, Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var ack = JsonConvert.DeserializeObject<SingleSmsSuccessResult>(content, JsonSerializerSettings);
                return ack.SmsId;
            }
            else
            {
                var content = await result.Content.ReadAsStringAsync();
                var ack = JsonConvert.DeserializeObject<SmsFailureResult>(content, JsonSerializerSettings);
                throw new SmsOwlException(ack.Message);
            }
        }

        public async Task<IList<string>> SendPromotionalSmsAsync(string senderId, IList<string> to, string message, SmsType smsType = SmsType.Normal)
        {
            var data = new BulkDirectSms
            {
                AccountId = _accountId,
                ApiKey = _apiKey,
                DndType = DndType.Promotional.ToString().ToLower(),
                SmsType = smsType.ToString().ToLower(),
                Message = message,
                SenderId = senderId,
                To = to
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(data, JsonSerializerSettings);
            var result = await httpClient.PostAsync(Url, new StringContent(json, Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var ack = JsonConvert.DeserializeObject<BulkSmsSuccessResult>(content, JsonSerializerSettings);
                return ack.SmsIds;
            }
            else
            {
                var content = await result.Content.ReadAsStringAsync();
                var ack = JsonConvert.DeserializeObject<SmsFailureResult>(content, JsonSerializerSettings);
                throw new SmsOwlException(ack.Message);
            }
        }

        public async Task<string> SendTransactionalSmsAsync<T>(string senderId, string to, string templateId,T placeholder)
        {
            var data = new SingleTemplatedSms<T>
            {
                AccountId = _accountId,
                ApiKey = _apiKey,
                DndType = DndType.Transactional.ToString().ToLower(),
                SmsType = SmsType.Normal.ToString().ToLower(),
                SenderId = senderId,
                To = to,
                TemplateId = templateId,
                Placeholders = placeholder
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(data, JsonSerializerSettings);
            var result = await httpClient.PostAsync(Url, new StringContent(json, Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var ack = JsonConvert.DeserializeObject<SingleSmsSuccessResult>(content, JsonSerializerSettings);
                return ack.SmsId;
            }
            else
            {
                var content = await result.Content.ReadAsStringAsync();
                var ack = JsonConvert.DeserializeObject<SmsFailureResult>(content, JsonSerializerSettings);
                throw new SmsOwlException(ack.Message);
            }
        }
    }
}
