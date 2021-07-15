using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon.DataModel
{
    public class TranslationDto
    {
        public Success success { get; set; }
        public Contents contents { get; set; }

        public static TranslationDto DeserializeStream(System.IO.Stream stream)
        {
            using var sr = new System.IO.StreamReader(stream);
            using JsonReader reader = new JsonTextReader(sr);
            var serializer = JsonSerializer.Create();
            return serializer.Deserialize<TranslationDto>(reader);
        }
    }

    public class Success
    {
        public int total { get; set; }
    }

    public class Contents
    {
        public string translated { get; set; }
        public string text { get; set; }
        public string translation { get; set; }
    }

}
