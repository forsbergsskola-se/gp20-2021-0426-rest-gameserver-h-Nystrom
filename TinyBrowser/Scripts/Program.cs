using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace TinyBrowser.Scripts{
    class Program{
        const string HostName = "acme.com";
        const int Port = 80;
        
        static void Main(string[] args){
            //var websiteInfo = GetStringFromWebRequest(HostName, Port);
            var websiteInfo = GetStringFromTextFile("Website");
            var title = GetWebsiteTitle(websiteInfo);
            var hyperLinks = GetSubPages(websiteInfo);
            Console.WriteLine(title);
            for (var i = 0; i < hyperLinks.Count; i++){
                Console.WriteLine($"{i}: {HostName}/{hyperLinks[i]}");
            }
        }
        static string GetStringFromWebRequest(string hostName, int port){
            var tcpClient = new TcpClient(hostName, port);
            var stream = tcpClient.GetStream();
            var streamWriter = new StreamWriter(stream){
                AutoFlush = true
            };
            streamWriter.Write($"GET / HTTP/1.1\r\nHost: {hostName}\r\n\r\n");
            
            var streamReader = new StreamReader(stream);
            var result = streamReader.ReadToEnd();
            tcpClient.Close();
            return result;
        }
        static string GetStringFromTextFile(string fileName){
            return File.ReadAllText($@"D:\UnityProjects_2020\gp20-2021-0426-rest-gameserver-h-Nystrom\GameServerTests\GameServerTests\Resources\{fileName}.txt");
        }
        static string GetWebsiteTitle(string websiteInfo){
            return Regex.Match(websiteInfo, @"(<title>)(.*?)(\</title\>)", RegexOptions.IgnoreCase).Groups[2].Value;
        }
        static List<string> GetSubPages(string websiteInfo){
            return Regex.Matches(websiteInfo, @"(href="")([^http | // | mailto][a-zA-Z0-9].*?)("")", RegexOptions.IgnoreCase)
                .Select(m => m.Groups[2].Value)
                .ToList();
        }
    }
}
