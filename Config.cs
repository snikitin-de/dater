﻿using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dater
{
    class Config
    {
        object json;

        public void SaveConfig(string path, DatasetMetaInfo metadata, object columns)
        {
            var metadataJSON = JObject.Parse(JsonConvert.SerializeObject(metadata));
            var columnsJSON = JObject.Parse(JsonConvert.SerializeObject(columns));

            metadataJSON.Merge(columnsJSON);

            File.WriteAllText(path, metadataJSON.ToString(Formatting.Indented));
        }

        public JObject LoadConfig(string path)
        {
            return JObject.Parse(File.ReadAllText(path));
        }
    }
}
