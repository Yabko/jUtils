using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using jUtils.Core.Configuration;

namespace jUtils.Core
{
    public class JsonParser
    {
        Config _config;

        public JsonParser(Config config)
        {
            _config = config;
        }

        public void ParseToJsonConfig()
        {

            string json = File.ReadAllText(_config.JsonConfigPath);

            dynamic array = JsonConvert.DeserializeObject(json);

            JsonConfig.okTime = array.methods.okTime;
            JsonConfig.coolTime = array.methods.coolTime;
            JsonConfig.users = array.methods.users;
            JsonConfig.loop = array.methods.loop;
            JsonConfig.rampup = array.methods.rampup;
        }

    }
}
