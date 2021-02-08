using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CV19ConsoleApp
{
    class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        //private const string data_url = @"https://raw.githubusercontent.com/agconti/kaggle-titanic/master/data/test.csv";
        

        private static async Task<Stream> GetDataStream()
        {
            HttpClient client = new HttpClient();
            // Конфигурируем так, чтобы получать только заголовки (не весь текст)
            var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);

            // Возвращаем поток, который обеспечит процесс чтения с сетевой карты
            return await response.Content.ReadAsStreamAsync();
        }

        // Каждая строка извлекается отдельно. В любой момент мы можем прервать чтение и остаток не застрянет в памяти
        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result; // получаем заголовки
            using var data_reader = new StreamReader(data_stream);

            while(!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line;
            }
        }

        // Получаем все даты (из первой строки файла csv)
        private static DateTime[] GetDates() => GetDataLines()
            .First() // берем только первую строку
            .Split(',') // разделяем по запятым
            .Skip(4) // отбрасываем первые 4 колонки, т.к. там название провинции и т.д
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture)) // преобразовываем в DateTime
            .ToArray();

        static void Main(string[] args)
        {
            //var client = new HttpClient();
            //var response = client.GetAsync(data_url).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    var csv_str = await response.Content.ReadAsStringAsync();
            //}


            //HttpClient client = new HttpClient();
            //using (HttpResponseMessage response = client.GetAsync(data_url).Result)
            //{
            //    using (HttpContent content = response.Content)
            //    {
            //        var result = content.ReadAsStringAsync().Result;
            //        //Console.WriteLine(result);
            //    }
            //}

            //foreach (var data_line in GetDataLines())
            //    Console.WriteLine(data_line);

            var dates = GetDates();
            Console.WriteLine(string.Join("\r\n", dates));

            Console.ReadLine();
        }
    }
}
