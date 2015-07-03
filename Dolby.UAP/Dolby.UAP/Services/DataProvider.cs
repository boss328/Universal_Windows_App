namespace Dolby.UAP.Services
{
    using Dolby.UAP.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using Dolby.UAP.Models;
    using Newtonsoft.Json;
    using Windows.Storage;
    using System.IO;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Net.Http;

    public class DataProvider : IDataProvider
    {
        private IConfigService _configService;

        public DataProvider(IConfigService configService)
        {
            _configService = configService;
        }

        public async Task<List<Movie>> GetDolbyMoviesInfo()
        {
            List<Movie> moviesList = new List<Movie>();
            string jsonData = "";

            switch (_configService.DataSource())
            {
                case DataSourceMode.FromAppFile:
                    jsonData = await JsonTextFromFile(_configService.DataSourceFilePath());
                    break;
                case DataSourceMode.FromUrl:
                    jsonData = await JsonTextFromUrl(_configService.DataSourceFilePath());
                    break;
                default:
                    jsonData = await JsonTextFromFile(_configService.DataSourceFilePath());
                    break;
            }

            try
            {
                moviesList = JsonConvert.DeserializeObject<List<Movie>>(jsonData);
                for (int i = 0; i < moviesList.Count; i++)
                {
                    moviesList[i].Index = i;
                }
            }
            catch (Exception)
            {
                // Handle parsing errors
            }
            return moviesList;
        }

        private async Task<string> JsonTextFromFile(string fileName)
        {
            string text = "";

            var folders = (await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFoldersAsync()).To‌​List();

            StorageFile file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(fileName);
            Stream stream = await file.OpenStreamForReadAsync();
            using (StreamReader reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }

        private async Task<string> JsonTextFromUrl(string url)
        {
            string text = "";

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);

            text = await response.Content.ReadAsStringAsync();
            return text;
        }
    }
}