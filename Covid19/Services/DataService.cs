using Covid19.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace Covid19.Services
{
    internal class DataService
    {
        private const string dataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        // Получаем поток байт
        private static async Task<Stream> GetDataStreamAsync()
        {
            HttpClient client = new HttpClient();

            // Конфигурируем так, чтобы получить только техническую информацию (заголовок)
            var response = await client.GetAsync(dataSourceAddress, HttpCompletionOption.ResponseHeadersRead);

            // Возвращаем контент в виде потока (т.е. не сохраняем данные в память, не засоряем ее)
            return await response.Content.ReadAsStreamAsync();
        }

        // Разбиваем поток на последовательность строк
        // Каждая строка извлекается отдельно. В любой момент мы можем прервать чтение и остаток не застрянет в памяти
        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStreamAsync().Result; // получаем заголовки
            using var data_reader = new StreamReader(data_stream);

            // Читаем построчно
            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line.Replace("Korea,", "Korea -").Replace("Bonaire,", "Bonaire -");
            }
        }

        // Получаем все даты
        private static DateTime[] GetDates() => GetDataLines()
            .First() // берем только первую строку (т.е. где наименования столбцов и даты)
            .Split(',') // разделяем по запятым
            .Skip(4) // отбрасываем первые 4 колонки, т.к. там название провинции и т.д
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture)) // преобразовываем в DateTime все даты
            .ToArray();

        // Получаем количество зараженных для каждой провинции и страны
        // Применяем кортежи
        private static IEnumerable<(string Country, string Province, (double Lat, double Lon) Place, int[] Count)> GetCountriesData()
        {
            var lines = GetDataLines()
                .Skip(1) // Первую строку отбрасываем (наименования столбцов и даты)
                .Select(line => line.Split(',')); // каждую строку разбиваем по разделителю запятой

            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"'); // указываем, что хотим обрезать кавычки и пробелы
                var latitude = double.Parse(row[2]);
                var longitude = double.Parse(row[3]);
                var count = row.Skip(4).Select(int.Parse).ToArray(); // пропускаем широту, долготу и лишнее. Превращаем в число кол-во зараженных

                yield return (country_name, province, (latitude, longitude), count); // возвращаем данные в виде кортежа
            }
        }

        // Получаем информацию по всем странам
        public IEnumerable<CountryInfo> GetData()
        {
            var dates = GetDates();
            var data = GetCountriesData().GroupBy(d => d.Country);

            foreach (var country_info in data)
            {
                var country = new CountryInfo
                {
                    Name = country_info.Key,
                    ProvinceCounts = country_info.Select(c => new PlaceInfo
                    {
                        Name = c.Province,
                        Location = new Point(c.Place.Lat, c.Place.Lon),
                        Counts = dates.Zip(c.Count, (date, count) => new ConfirmedCount { Date = date, Count = count })
                    })
                };
                yield return country;
            }
        }
    }
}
