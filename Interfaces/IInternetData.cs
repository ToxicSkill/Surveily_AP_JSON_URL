using System;
using System.Threading.Tasks;

namespace JSON_URL.Interfaces
{
    public interface IInternetData
    {
        Task<int> GetDataFromUrls(string uris);

        Task<bool> SaveData();

        string GetDownloadedData();
    }
}
