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
        

        private static async Task<Stream> GetDataStreamAsync()
        {
            HttpClient client = new HttpClient();
            // Конфигурируем так, чтобы получать только заголовки (не весь текст)
            var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);

            // Возвращаем поток, который обеспечит процесс чтения с сетевой карты
            return await response.Content.ReadAsStreamAsync();
        }

        // Разбиваем поток на последовательность строк
        // Каждая строка извлекается отдельно. В любой момент мы можем прервать чтение и остаток не застрянет в памяти
        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStreamAsync().Result; // получаем заголовки
            using var data_reader = new StreamReader(data_stream);

            while(!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line.Replace("Korea,", "Korea -").Replace("Bonaire,", "Bonaire -");
            }
        }

        // Получаем все даты
        private static DateTime[] GetDates() => GetDataLines()
            .First() // берем только первую строку (т.е. где заголовок)
            .Split(',') // разделяем по запятым
            .Skip(4) // отбрасываем первые 4 колонки, т.к. там название провинции и т.д
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture)) // преобразовываем в DateTime все даты
            .ToArray();


        // Получаем количество зараженных для каждой провинции и страны
        // Применяем кортеж
        private static IEnumerable<(string Country, string Province, int[] Count)> GetData()
        {
            var lines = GetDataLines()
                .Skip(1) // Первую строку отбрасываем (это заголовки)
                .Select(line => line.Split(',')); // каждую строку разбиваем по разделителю запятой

            foreach(var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"'); // указываем, что хотим обрезать кавычки и пробелы
                var count = row.Skip(4).Select(int.Parse).ToArray(); // пропускаем широту, долготу и лишнее. Превращаем в число

                yield return (country_name, province, count); // возвращаем данные в виде кортежа
            }    
        }

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

            //var dates = GetDates();
            //Console.WriteLine(string.Join("\r\n", dates));

            var russia_data = GetData()
                .First(v => v.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia_data.Count, (date, count) => $"{date:d} - {count}")));

            Console.ReadLine();
        }
    }
}
