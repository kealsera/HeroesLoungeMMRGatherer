using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using HeroesLoungeMMRGatherer.Shared.Helpers;
using HeroesLoungeMMRGatherer.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HeroesLoungeMMRGatherer.Shared.Fetcher
{
    public static class ProfileFetcher
    {
        public static async Task<List<PlayerProfile>>  FetchAll()
        {
            var retVal = new List<PlayerProfile>();
            var pathString = ConfigurationManager.AppSettings[ConfigHelper.CSVPathKey];
            if (File.Exists(pathString))
            {

                var players = File.ReadAllLines(pathString)
                    .Select(x => x.Split(','))
                    .FirstOrDefault()
                    .Select(x => new
                    {
                        BattleTag = x.Replace('\t',' ')
                    });

                foreach (var tag in players)             
                {
                    using (var client = new HttpClient())
                    {
                        try
                        {
                            //1== NA,2 == EU
                            var url = string.Format(URLHelper.PlayerProfileURL, 2, tag.BattleTag.Replace('#', '_'));
                            var resp = await client.GetStringAsync(url);
                            //Non Existing Or Private Profile
                            if (resp != "null")
                            {
                                var json = JObject.Parse(resp);
                                var profile = JsonConvert.DeserializeObject<PlayerProfile>(json.ToString());
                                profile.BattleTag = tag.BattleTag;
                                retVal.Add(profile);
                            }
                          
                        }
                        catch (Exception ex) 
                        {
                            
                            throw;
                        }
                    }
                }

            }
            return retVal;
        }

    }
}
