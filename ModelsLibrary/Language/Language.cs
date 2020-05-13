using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Collections;
namespace ModelsLibrary
{

    static class GetJsonProperties
    {

        public static JsonElement? GetProperty(this JsonElement? language, string element)
        {
            if (language.Value.TryGetProperty(element, out JsonElement value))
                return value;
            else
                return null;
        }
    }
    /// <summary>    
    ///  | Author：TreeMoons <br/>
    ///  | Date：May,6th,2020 <br/>
    ///  | work：store the temporary keywords by international language <br/>
    ///  | instructon：store struct by action name,which reflect keywords in the views <br/>
    /// </summary>
    [Serializable]
    public partial class Language : IEnumerable
    {
        private  JsonElement LanguageJson { get; set; }
        public Language(JsonElement _json)
        {
            LanguageJson = _json;
            if (!LanguageInfo.TryAdd(nameof(Master), new Master(LanguageJson.GetProperty(nameof(Master)))))
                LanguageInfo[nameof(Master)] = new Master(LanguageJson.GetProperty(nameof(Master)));
        }
        private  Dictionary<string, object> LanguageInfo = new Dictionary<string, object>();
        public object this[string index]
        {
            get => LanguageInfo[index];
            set => LanguageInfo[index] = value;
        }
        public IEnumerator GetEnumerator()
        {
            foreach (var i in LanguageInfo)
            {
                yield return i;
            }
        }
        /// <summary>
        /// keywords of Master views
        /// </summary>
        [Serializable]
        public struct Master
        {
            private static JsonElement? LanguageJson { get; set; }
            public Master(JsonElement? _json)
            {
                LanguageJson = _json;
            }

            public JsonElement? HeaderConvenienceOptionsArray => LanguageJson.GetProperty(nameof(HeaderConvenienceOptionsArray));
            public string Contact => LanguageJson.GetProperty(nameof(Contact))?.ToString();
            public string language => LanguageJson.GetProperty(nameof(language))?.ToString();
            public string en => LanguageJson.GetProperty(nameof(en))?.ToString();
            public string zh => LanguageJson.GetProperty(nameof(zh))?.ToString();
            public string CurrentLanguageCode => LanguageJson.GetProperty(nameof(CurrentLanguageCode))?.ToString();
            public string CurrentLanguage => LanguageJson.GetProperty(nameof(CurrentLanguage))?.ToString();

            public JsonElement? UserOptionsTheme => LanguageJson.GetProperty(nameof(UserOptionsTheme));
            public JsonElement? UserOptionsleftArray => LanguageJson.GetProperty(nameof(UserOptionsleftArray));
            public LinkTable[] FooterArray => new LinkTable[2] { new LinkTable(LanguageJson.GetProperty(nameof(FooterArray)).Value[0]), new LinkTable(LanguageJson.GetProperty(nameof(FooterArray)).Value[1]) };
            public LinkTable[] MenuArray => new LinkTable[2] { new LinkTable(LanguageJson.GetProperty(nameof(MenuArray)).Value[0]), new LinkTable(LanguageJson.GetProperty(nameof(MenuArray)).Value[1]) };
            public Login? LoginArray => new Login(LanguageJson.GetProperty(nameof(LoginArray)));
            public struct Login
            {
                private static JsonElement? LoginJson { get; set; }
                public Login(JsonElement? _json)
                {
                    LoginJson = _json;
                }
                public string Title => LoginJson.GetProperty(nameof(Title))?.ToString();
                public string UserName => LoginJson.GetProperty(nameof(UserName))?.ToString();
                public string Password => LoginJson.GetProperty(nameof(Password))?.ToString();
                public string Signin => LoginJson.GetProperty(nameof(Signin))?.ToString();
                public string Forget => LoginJson.GetProperty(nameof(Forget))?.ToString();
                public string IsRemember => LoginJson.GetProperty(nameof(IsRemember))?.ToString();
                public string NewUser => LoginJson.GetProperty(nameof(NewUser))?.ToString();
                public string Create => LoginJson.GetProperty(nameof(Create))?.ToString();
            }
            /// <summary>
            ///  Array contains two options,put using into menu or footer
            /// </summary>
            public struct LinkTable
            {
                private static JsonElement? LoginJson { get; set; }
                public LinkTable(JsonElement? _json)
                {
                    LoginJson = _json;
                }
                public string Title => LoginJson.GetProperty(nameof(Title)).ToString();
                public string[] Option1 => new string[] { LoginJson.GetProperty(nameof(Option1))?[0].ToString(),LoginJson.GetProperty(nameof(Option1))?[1].ToString() };
                public string[] Option2 => new string[] { LoginJson.GetProperty(nameof(Option2))?[0].ToString(),LoginJson.GetProperty(nameof(Option2))?[1].ToString() };
                public string[] Option3 => new string[] { LoginJson.GetProperty(nameof(Option3))?[0].ToString(),LoginJson.GetProperty(nameof(Option3))?[1].ToString() };
            }
        }

        /// <summary>
        ///  the name of one of the Views: Index
        /// </summary>
        public struct Index
        {

        }

    }
}