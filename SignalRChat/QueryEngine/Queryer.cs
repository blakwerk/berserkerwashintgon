using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalRChat.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SignalRChat.QueryEngine
{


    public class Queryer
    {

        private WebClient _client = new WebClient()
        {
            BaseAddress = ("http://www.berserkerwashington.com")
        };

        private Uri _commentsOData;
        private Uri _confessionsOData;

        public Queryer()
        {
            _client.Headers.Add(HttpRequestHeader.Accept, "application/json");
            _confessionsOData = new Uri("http://berserkerwashington.com/odata/ConfessionsOData?");
            //_confessionsOData = new Uri("/odata/ConfessionsOData?");
        }

        public async Task<IEnumerable<Confession>> FirstXConfessions(int x)
        {
            var queryStr = "$top=";
            return await PerformConfessionsODataQuery(_confessionsOData + queryStr + x);
        }

        public async Task<IEnumerable<Confession>> LastXConfessions(int x)
        {
            // Take the top X from the descending order of Id (so Ids going highest to lowest)
            var queryStr = "$orderby=Id%20desc&$top=";
            return await PerformConfessionsODataQuery(_confessionsOData + queryStr + x);
        }

        public async Task<IEnumerable<Confession>> NextXConfessions(int top, int skip)
        {
            // Take the top X from the descending order of Id (so Ids going highest to lowest)
            var queryStr = "$orderby=Id%20desc&$top="+top+"&$skip="+skip;
            return await PerformConfessionsODataQuery(_confessionsOData + queryStr);
        }

        public async Task<IEnumerable<Confession>> GetAllConfessions()
        {
            var queryStr = "$orderby=Id%20desc";
            return await PerformConfessionsODataQuery(_confessionsOData + queryStr);
        }

        private async Task<IEnumerable<Confession>> PerformConfessionsODataQuery(string queryString)
        {
            var confessions = new List<Confession>();

            try
            {
                // Concatenate our operation and value to the odata uri
                var confessionsJson = _client.DownloadString(queryString);

                JObject confessionsJObj = JObject.Parse(confessionsJson);
                IList<JToken> results = confessionsJObj["value"].Children().ToList();

                foreach (var result in results)
                {
                    var conf = JsonConvert.DeserializeObject<Confession>(result.ToString());
                    confessions.Add(conf);
                }
            }
            catch (Exception ex)
            {
                confessions = new List<Confession>
                {
                    new Confession
                    {
                        Submitter = "SYSTEM",
                        TheConfession = "An error occurred.  If this persists, please contact the administrator. <br/>" +
                                        ex.HResult + "<br/>" + ex.Message
                    }
                };
            }

            return confessions;
        }
    }

}