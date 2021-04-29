using System;
using System.IO;
using TinyBrowser.Scripts.Data;

namespace TinyBrowser.Scripts{
    public struct Storage{
        static string _workingDirectory = Environment.CurrentDirectory;
        const int MaxLastWriteTimeInMinutes = 30; 

        public static void SetUpFolders(){
            CreateMissingDirectory("/Cache");
            CreateMissingDirectory("Cache/Websites");
            CreateMissingDirectory("Cache/History");
        }

        public static void CacheWebPage(WebPage webPage){
            CreateMissingDirectory($"Cache/Websites/{webPage.Title}");
            CacheTextFile(webPage);
        }

        public static bool IsCacheOutDated(WebPage webPage){
            var path = GetFilePath(webPage.Title);
            CreateMissingDirectory($"Cache/Websites/{webPage.Title}");
            return !File.Exists(path) && File.GetLastWriteTime(path).Minute >= MaxLastWriteTimeInMinutes;
        }

        static string GetFilePath(string fileName){
            return $"{_workingDirectory}/Cache/Websites/{fileName}/{fileName}.txt";
        }

        static void CreateMissingDirectory(string directoryName){
            var path = $"{_workingDirectory}/{directoryName}";
            if (Directory.Exists(path))
                return;
            Directory.CreateDirectory(path);
        }

        static void CacheTextFile(WebPage webPage){
            var path = GetFilePath(webPage.Title);
            if (IsCacheOutDated(webPage))
                File.WriteAllText(path, webPage.RawHtml);
        }
    }
}