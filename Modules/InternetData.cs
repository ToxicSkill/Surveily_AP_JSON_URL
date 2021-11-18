using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JSON_URL.Interfaces;

namespace JSON_URL.Modules
{
    public class InternetData : IInternetData
    {
        #region variables

        private const char Separator  = ';';
        private List<(string data, string fname)> _downloadedData;
        private readonly ISaver _saver;

        #endregion

        public InternetData(string folderName) => _saver = new Saver(folderName);
        
        public InternetData() => _saver = new Saver();
       
        public async Task<bool> SaveData() => await _saver.SaveFiles(_downloadedData);
        
        public string GetDownloadedData()
        {
            StringBuilder returnString = new();
            foreach (var singleData in _downloadedData)
            {
                returnString.Append(singleData.fname).Append(":\n").Append(singleData.data);
            }

            return returnString.ToString();
        }
        
        private bool CheckUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private Uri CreateUri(string url) => new(url);
        
        private async Task<(string data, string fname)> DownloadDataFromUri(Uri uri)
        {
            try
            {
                using WebClient webClient = new();
                return new(await Task.Run(() => webClient.DownloadString(uri)), uri.Authority.ToString());
            }
            catch (Exception)
            {
                return new(string.Empty, string.Empty);
            }
        }
        public async Task<int> GetDataFromUrls(string inputUrlsString)
        {
            if (string.IsNullOrEmpty(inputUrlsString))
                return 0;
            List<Task<(string data, string fname)>> tasks = new();
            var urls = inputUrlsString.Split(Separator);
            foreach (var url in urls)
            {
                if (CheckUrl(url))
                    tasks.Add(DownloadDataFromUri(CreateUri(url)));
            }
            var results = await Task.WhenAll(tasks);
            
            _downloadedData = new List<(string data, string fname)>();
           
            foreach (var (data, fname) in results)
            {
                if (!data.Equals(string.Empty))
                    _downloadedData.Add(new(data, fname));
                else
                {
                    Console.WriteLine($"Can not download {fname} data");
                }
            }

            return _downloadedData.Count;
        }
    }
}
