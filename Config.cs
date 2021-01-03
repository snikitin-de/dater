using System.IO;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dater
{
    class Config
    {
        public void SaveConfig(string path, DatasetMetaInfo metadata, object columns)
        {
            var metadataJSON = JObject.Parse(JsonConvert.SerializeObject(metadata));
            var columnsJSON = JObject.Parse(JsonConvert.SerializeObject(columns));

            metadataJSON.Merge(columnsJSON);

            File.WriteAllText(path, metadataJSON.ToString(Formatting.Indented));
        }

        public JObject LoadConfig(string path)
        {
            JObject json = null;

            try
            {
                json = JObject.Parse(File.ReadAllText(path));
            } catch (Newtonsoft.Json.JsonReaderException)
            {
                string applicationName = Application.ResourceAssembly.GetName().Name;

                MessageBox.Show("Invalid JSON file!", applicationName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            return json;
        }
    }
}
