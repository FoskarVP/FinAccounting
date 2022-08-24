using System;
using System.Drawing;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinAccountingWebService.APIReciept.ProverkachekaAPI
{
    internal static class APIProvider
    {
        private static readonly string URL = "https://proverkacheka.com/";
        private static readonly string POST = "api/v1/check/get";

        public static async Task<Reciept?> GetReceiptByFiscalData(string fn, string fd, string fp, DateTime dateTime, int type, decimal sum)
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            FormUrlEncodedContent dataParams = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "token", AppSettings.APISettings.Proverkacheka },
                { "fn", fn },
                { "fd", fd },
                { "fp", fp },
                { "t", dateTime.ToString("yyyyMMddTHHmm") },
                { "n", ((OperationTypeEnum)type).ToString() },
                { "s", sum.ToString() },
                { "qr", "0" }
            });

            var result = await client.PostAsync(POST, dataParams);
            JObject response = JObject.Parse(await result.Content.ReadAsStringAsync());

            return RecieptParse(response);
        }

        public static async Task<Reciept?> GetReceiptByQRRaw(string qrraw)
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            FormUrlEncodedContent dataParams = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "token", AppSettings.APISettings.Proverkacheka },
                { "qrraw", qrraw }
            });

            var result = await client.PostAsync(POST, dataParams);
            JObject response = JObject.Parse(await result.Content.ReadAsStringAsync());

            return RecieptParse(response);
        }

        public static async Task<Reciept?> GetReceiptByQRUrl(string qrurl)
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            FormUrlEncodedContent dataParams = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "token", AppSettings.APISettings.Proverkacheka },
                { "qrurl", qrurl }
            });

            var result = await client.PostAsync(POST, dataParams);
            JObject response = JObject.Parse(await result.Content.ReadAsStringAsync());

            return RecieptParse(response);
        }

        private static Reciept? RecieptParse(JObject response)
        {
            Reciept? receipt = null;

            if (response != null && (ResponseCodeEnum)(short)response["code"] == ResponseCodeEnum.Success)
            {
                receipt = new Reciept()
                {
                    Organization = GetOrganizationName(response["data"]["json"]["retailPlace"], response["data"]["json"]["user"]),
                    Total = Convert.ToDecimal(response["data"]["json"]["totalSum"]) / 100,
                    DateTime = Convert.ToDateTime(response["data"]["json"]["dateTime"])
                };

                foreach (JObject item in response["data"]["json"]["items"])
                {
                    receipt.Items.Add(new Item()
                    {
                        Name = GetProductName(item["name"]),
                        Price = Convert.ToDecimal(item["price"]) / 100,
                        Quantity = Convert.ToDecimal(item["quantity"]),
                    });
                }
            }

            return receipt;
        }

        private static string GetOrganizationName(JToken retailPlace, JToken user)
        {
            Regex regex = new Regex("[a-zA-Zа-яА-ЯеЁ&]+");
            MatchCollection matches = regex.Matches(retailPlace.ToString());
            return matches.Count > 0 ? retailPlace.ToString() : user.ToString();
        }

        private static string GetProductName(JToken productName)
        {
            string name = productName.ToString().Trim();
            if (name[0] == '#')
            {
                name = name.Substring(1);
            }

            return name;
        }
    }
}
