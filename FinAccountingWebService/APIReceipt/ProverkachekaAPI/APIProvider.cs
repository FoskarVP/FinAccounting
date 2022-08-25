using System;
using System.Drawing;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinAccountingWebService.APIReceipt.ProverkachekaAPI
{
    internal static class APIProvider
    {
        private static readonly string URL = "https://proverkacheka.com/";
        private static readonly string POST = "api/v1/check/get";

        public static async Task<Receipt?> GetReceiptByFiscalData(string fn, string fd, string fp, DateTime dateTime, int type, decimal sum)
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

            return ReceiptParse(response);
        }

        public static async Task<Receipt?> GetReceiptByQRRaw(string qrraw)
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

            return ReceiptParse(response);
        }

        public static async Task<Receipt?> GetReceiptByQRUrl(string qrurl)
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

            return ReceiptParse(response);
        }

        private static Receipt? ReceiptParse(JObject response)
        {
            Receipt? receipt = null;

            if (response != null && (ResponseCodeEnum)(short)response["code"] == ResponseCodeEnum.Success)
            {
                receipt = new Receipt()
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
            string orgName = matches.Count > 0 ? retailPlace.ToString() : user.ToString();

            if (orgName.Contains("Пятерочка"))
            {
                orgName = "Пятерочка";
            }

            Regex regMultipleSpaces = new Regex("\\s{2,}");
            orgName = regMultipleSpaces.Replace(orgName, " ");

            return orgName;
        }

        private static string GetProductName(JToken productName)
        {
            string name = productName.ToString().Trim();

            //удаляем:
            //1) из начала названия  - * или #
            //2) из конца названия - вес продукта
            Regex regWeightAndTechnicalSymbols = new Regex("^(\\*|#)|(\\d+(\\.|\\,))?\\d+\\s?(кг|к|мл|л|г|гр|%|шт)", RegexOptions.IgnoreCase);
            name = regWeightAndTechnicalSymbols.Replace(name, "");

            //удаляем указание упаковки (п/уп или пл/уп и пр.)
            Regex regPack = new Regex("\\w{1,3}/\\w{1,3}");
            name = regPack.Replace(name, "");

            Regex regBrackets = new Regex("(\\(\\w*(\\s\\w+)?\\)?.?)$");
            name = regBrackets.Replace(name, "");

            Regex regDigitsEnd = new Regex("(\\d*(\\.|\\,?)\\d+)$");
            name = regDigitsEnd.Replace(name, "");

            Regex regMultipleSpaces = new Regex("\\s{2,}");
            name = regMultipleSpaces.Replace(name, " ");

            Regex regMultipleDots = new Regex("\\.{2,}");
            name = regMultipleDots.Replace(name, ".");

            return name.Trim();
        }
    }
}
