using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CV19ConsoleApp
{
    class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        //private const string data_url = @"https://raw.githubusercontent.com/agconti/kaggle-titanic/master/data/test.csv";
        
        static void Main(string[] args)
        {
            //var client = new HttpClient();
            //var response = client.GetAsync(data_url).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    var csv_str = await response.Content.ReadAsStringAsync();
            //}


            HttpClient client = new HttpClient();
            using (HttpResponseMessage response = client.GetAsync(data_url).Result)
            {
                using (HttpContent content = response.Content)
                {
                    var result = content.ReadAsStringAsync().Result;
                    //Console.WriteLine(result);
                }
            }

            Console.ReadLine();
        }
    }
}
