using System;
using System.Threading.Tasks;
using JSON_URL.Modules;

namespace JSON_URL
{
    public static class Program
    {
        public static async Task Main(string[] args) => await RunJsonUrl();
        
        private static async Task RunJsonUrl()
        {
            Console.WriteLine("Enter url addresses");
            var inputString = Console.ReadLine();
            
            Console.WriteLine("Enter destination folder");
            var destFolderString = Console.ReadLine();
            var internetData = new InternetData(destFolderString);

            if (await internetData.GetDataFromUrls(inputString) != 0)
            {
                if (await internetData.SaveData())
                    Console.WriteLine("Downloading succeed");
                else
                    Console.WriteLine("Downloading failed");
            }
            else
                Console.WriteLine("Invalid input values");
        }
    }
}
