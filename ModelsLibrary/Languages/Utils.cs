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
        /// <summary>
        /// 初始化所有语言
        /// </summary>
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
        /// <summary>
        /// 分域加载国家语言到hashtable，将参数中的hashtable以文件名（即国家语言对应代码）为主键
        /// 进行初始化加载。
        /// </summary>
        /// <param name="path">要加载的包含所有语言的资源文件夹目录</param>
        /// <param name="routeParttern">域下要传输的多国显示的过滤正则</param>
        /// <param name="languages">所有国家语言对应LANGUAGE MODEL对象的HASHTABLE表</param>
        /// <param name="languagesTable">所有国家语言对应json对象的HASHTABLE表</param>
        /// <param name="languagesBtyes">所有国家语言对应文件流对象的HASHTABLE表</param>
        /// <typeparam name="TAreaLanguage">对应的LANGUAGE MODEL的类型</typeparam>
        public static void GetAreaLanguageJson<TAreaLanguage>(string path, StringBuilder routeParttern, Hashtable languages, Hashtable languagesTable, Hashtable languagesBtyes)
        where TAreaLanguage : class, ILanguage, new()
        {
            var langDictionary = new DirectoryInfo(path);
            var fileInfos = langDictionary.GetFileSystemInfos();
            foreach (var file in fileInfos)
            {
                if (file.Extension == ".json")
                {
                    try
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
                    catch (System.Exception ex)
                    {
                        CommonUtils.LogText.WriteLogs("", ex.ToString());
                        continue;
                    }
                }
            }
        }

    }
}