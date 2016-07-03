using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using HeroesLoungeMMRGatherer.Shared.Fetcher;
using HeroesLoungeMMRGatherer.Shared.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HeroesLoungeMMRGatherer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gathering Data");
            ProfileFetcher.FetchAll().ContinueWith(x =>
            {
                if (x.IsCompleted)
                {
                    Console.WriteLine("Task Completed - Gathered " + x.Result.Count + " Profiles");
                    var path = ConfigurationManager.AppSettings[ConfigHelper.ResultPathKey];

                    Console.WriteLine("Writing JSON to " + path);
                    JsonConvert.DefaultSettings = (() =>
                    {
                        var settings = new JsonSerializerSettings();
                        settings.Converters.Add(new StringEnumConverter());
                        return settings;
                    });
                     var write = JsonConvert.SerializeObject(x.Result);
                    
                       using (var sw = new StreamWriter(path, true))
                       {
                           sw.Write(write);
                       }
                }

                Console.WriteLine("Done");
                Console.Read();
            }).Wait();
        }
    }
}
