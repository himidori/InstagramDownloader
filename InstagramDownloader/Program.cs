﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace InstagramDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.DefaultConnectionLimit = 10000;
            Console.Write(">Instagram Username: ");
            string input = Console.ReadLine();
            string user = string.Format("https://www.instagram.com/{0}/media/", input);
            Directory.CreateDirectory(input);
            Console.Clear();
            Console.WriteLine(">Starting download...");
            var data = FetchImages(user);
            while (data.items.Length > 0)
            {
                List<string> imagesList = new List<string>();
                string id = data.items[data.items.Length - 1].id;

                foreach(var image in data.items)
                {
                    string url = image.images.standard_resolution.url;
                    imagesList.Add(url);
                }

                Parallel.ForEach(imagesList, new ParallelOptions { MaxDegreeOfParallelism = 10 }, item =>
                    {
                        string name = item.Substring(item.LastIndexOf('/') + 1).Split('?')[0];
                        WebClient client = new WebClient();
                        Console.WriteLine(">Downloading: " + name);
                        client.DownloadFile(item, Path.Combine(Directory.GetCurrentDirectory(), input, name));
                    });

                Console.Clear();
                data = FetchImages(string.Format("https://www.instagram.com/{0}/media/?max_id={1}", input, id));
            }
        }

        private static InstagramImage FetchImages(string url)
        {
            string source = GetPageSource(url);
            return JsonConvert.DeserializeObject<InstagramImage>(source);
        }

        private static string GetPageSource(string url)
        {
            WebClient wc = new WebClient();
            return wc.DownloadString(url);
        }
    }
}
