using System;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using CommonUtils;
namespace ModelsLibrary.Languages
{

    static public class Utils
    {
        public static JsonElement? GetProperty(this JsonElement? language, string element)
        {
            if (language.Value.TryGetProperty(element, out JsonElement value))
                return value;
            else
                return null;
        }
        public static StringBuilder AreaAdminLanguagesParttern { get; set; } = new StringBuilder();
        public static StringBuilder LanguagesParterrn { get; set; } = new StringBuilder();
        public static Hashtable Languages { get; set; } = Hashtable.Synchronized(new Hashtable());
        public static Hashtable LanguageJsonElementDictionary { get; set; } = Hashtable.Synchronized(new Hashtable());
        public static Hashtable LanguageByteArrayDictionary { get; set; } = Hashtable.Synchronized(new Hashtable());
        public static void ReadAllLanguageJson()
        {
            GetAreaLanguageJson<MainViews.Language>(
                path: "./wwwroot/src/languages",
                routeParttern: LanguagesParterrn,
                languagesBtyes: LanguageByteArrayDictionary,
                languagesTable: LanguageJsonElementDictionary,
                languages: Languages
            );
        }

        public static void GetAreaLanguageJson<TAreaLanguage>(string path, StringBuilder routeParttern, Hashtable languages, Hashtable languagesTable, Hashtable languagesBtyes)
        where TAreaLanguage : class, ILanguage, new()
        {
            var langDictionary = new DirectoryInfo(path);
            var fileInfos = langDictionary.GetFileSystemInfos();
            foreach (var file in fileInfos)
            {
                if (file.Extension == ".json")
                {
                    using var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
                    var fileName = file.Name.Replace(file.Extension, "");
                    routeParttern.Append($@"({fileName})?");
                    var element = JsonDocument.Parse(stream).RootElement;
                    languagesTable[fileName] = element;
                    languagesBtyes[fileName] = stream;
                    var language = new TAreaLanguage();
                    language.Initialing(element);
                    languages[fileName] = language;
                }
            }
        }

    }
}