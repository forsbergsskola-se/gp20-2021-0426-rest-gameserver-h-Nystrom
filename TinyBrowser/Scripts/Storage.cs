using System;
using System.IO;
using Newtonsoft.Json;
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

        public static WebPage GetCachedWebPage(string host){
            if(IsCacheOutDated(host))
                throw new Exception("Exception: WebPage cache doesn't exist!");
            var path = GetFilePath(host);
            var rawJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<WebPage>(rawJson);
        }
        public static void CacheWebPage(WebPage webPage){
            CreateMissingDirectory($"Cache/Websites/{webPage.Title}");
            CacheTextFile(webPage);
        }

        public static bool IsCacheOutDated(string host){
            if (!Directory.Exists($"{_workingDirectory}/{host}"))
                return true;
            var path = GetFilePath(host);
            return !File.Exists(path) && File.GetLastWriteTime(path).Minute >= MaxLastWriteTimeInMinutes;
        }
        static string GetFilePath(string fileName){
            return $"{_workingDirectory}/Cache/Websites/{fileName}/{fileName}.Json";
        }

        static void CreateMissingDirectory(string directoryName){
            var path = $"{_workingDirectory}/{directoryName}";
            if (Directory.Exists(path))
                return;
            Directory.CreateDirectory(path);
        }
        static void CacheTextFile(WebPage webPage){
            var path = GetFilePath(webPage.Title);
            if (!IsCacheOutDated(webPage.Title)) 
                return;
            var webPageJson = JsonConvert.SerializeObject(webPage);
            File.WriteAllText(path, $"{webPageJson}.Json");

        }

        public static int Clear(){
            var path = $"{_workingDirectory}/Cache/Websites/";
            var directoryInfo = new DirectoryInfo(path);
            var deleted = 0;
            foreach (var dir in directoryInfo.EnumerateDirectories()){
                dir.Delete(true);
                deleted++;
            }
            return deleted;
        }
    }
}