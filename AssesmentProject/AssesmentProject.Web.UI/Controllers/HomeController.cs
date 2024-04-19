using AssesmentProject.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace AssesmentProject.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string apiUrl = "https://seffaflik.epias.com.tr/transparency/service/market/intra-day-trade-history";

            DateTime today = DateTime.Now.Date;
            string startDate = today.ToString("yyyy-MM-dd");
            string endDate = today.ToString("yyyy-MM-dd");

            string fullUrl = $"{apiUrl}?startDate={startDate}&endDate={endDate}";
            WebRequest request = WebRequest.Create(fullUrl);
            request.Method = "GET";
            request.ContentType = "application/json";

            string jsonResponse = string.Empty;
            List<IntraDayTrade> dataList = new List<IntraDayTrade>();
            List<Result> resultList = new List<Result>();

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            jsonResponse = reader.ReadToEnd();
                        }
                    }
                }

                ApiResponse responseList = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                var contractGroups = responseList.Body.IntraDayTradeHistoryList
                    .Where(d => d.Conract.StartsWith("PH"))
                    .GroupBy(d => d.Conract);

                foreach (var group in contractGroups)
                {
                    double totalTransactionAmount = Math.Round(group.Sum(d => (d.Price * d.Quantity) / 10), 2);
                    double totalTransactionQuantity = group.Sum(d => d.Quantity / 10);
                    double weightedAveragePrice = Math.Round(totalTransactionAmount / totalTransactionQuantity, 2);
                    DateTime contractDate = DateTime.ParseExact(group.Key.Substring(2), "yyMMddHH", null);

                    resultList.Add(new Result()
                    {
                        DateTime = contractDate,
                        TotalCount = totalTransactionQuantity,
                        TotalPrice = totalTransactionAmount,
                        AveragePrice = weightedAveragePrice
                    });
                }

                resultList = resultList.OrderBy(r => r.DateTime).ToList();
            }
            catch (WebException ex)
            {
                //ex.Message içerisindeki hatayı loglayabiliriz.
            }

            return View(resultList);
        }
    }
}
