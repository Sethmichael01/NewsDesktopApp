using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewsApp_Desktop_
{
    public class Config
    {
        public static async Task<News.Rootobject> Deserialize(string Url)
        {
            var json = await Http.GetAsync(Url);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<News.Rootobject>(json);
        }
    }
}
