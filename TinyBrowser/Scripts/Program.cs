using System;
using System.Collections.Generic;
using TinyBrowser.Scripts.Data;

namespace TinyBrowser.Scripts{
    class Program{
        const string HostName = "acme.com";
        const int Port = 80;
        static WebPage _currentWebPage;

        static void Main(string[] args){
            //TODO: checkout xmlReader
            _currentWebPage = new WebPage(null, HostName, null);
            Storage.SetUpFolders();
            
            if (Storage.IsCacheOutDated(_currentWebPage)){
                _currentWebPage = WebRequester.SendAndReceiveRequest(HostName, Port);
                Storage.CacheWebPage(_currentWebPage);
            }
            
            Console.ReadKey();


        }
        static void DisplayCurrentWebPage(string title, List<string> hyperLinks){
            Console.WriteLine(title);
            for (var i = 0; i < hyperLinks.Count; i++){
                Console.WriteLine($"{i}: {HostName}/{hyperLinks[i]}");
            }
        }
    }
}
