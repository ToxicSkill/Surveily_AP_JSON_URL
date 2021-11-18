using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using JSON_URL.Interfaces;

namespace JSON_URL.Modules
{
    public class Saver : ISaver
    {
        private string _folderName = "JsonDownload/";
        private const string Extension = ".json";
        private bool _status;

        public Saver(string folderName)
        {
            SetFolderName(folderName);
            CreateDirectory();
        }

        public Saver() 
        {
            CreateDirectory();
        }

        private void SetFolderName(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                return;
            StringBuilder fnameBuilder = new();
            fnameBuilder.Append(name).Append('/');
            _folderName = fnameBuilder.ToString();
        }

        private bool CreateDirectory()
        {
            var exists = Directory.Exists(_folderName);
            try
            {
                if (!exists)
                    Directory.CreateDirectory(_folderName);
                return _status = true;
            }
            catch (Exception)
            {
                Console.WriteLine($"Can not create directory {_folderName}");
                return _status = false;
            }
        }

        public async Task<bool> SaveFiles(List<(string data, string fname)> files)
        {
            if (files == null)
                return false;
            if (!_status || !files.Any())
                return false;
            foreach (var (data, fname) in files)
            {
                if (data.Length == 0)
                    continue;
                StringBuilder fileNameBuilder = new(_folderName);
                fileNameBuilder.Append(fname).Append(Extension);
                if (File.Exists(fileNameBuilder.ToString()))
                    fileNameBuilder.Insert(fileNameBuilder.Length - Extension.Length, "_c");
                await File.WriteAllTextAsync(fileNameBuilder.ToString(), data);
            }
            return true;
        }
    }
}
