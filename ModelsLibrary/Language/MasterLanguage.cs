using System.Reflection;
using System;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using CommonUtils;
namespace ModelsLibrary
{
    /// <summary>    
    ///  | Author：TreeMoons <br/>
    ///  | Date：May,6th,2020 <br/>
    ///  | work：store the temporary keywords by international language <br/>
    ///  | instructon：store struct by action name,which reflect keywords in the views <br/>
    /// </summary>
    [Serializable]
    public partial class Language : IEnumerable
    {
        private JsonElement LanguageJson { get; set; }
        public Language(JsonElement _json)
        {
            LanguageJson = _json;
            LanguageInfo[nameof(Master)] = new Master(LanguageJson.GetProperty(nameof(Master)));
            LanguageInfo[nameof(Index)] = new Index(LanguageJson.GetProperty(nameof(Index)));
            LanguageInfo[nameof(Searched)] = new Searched(LanguageJson.GetProperty(nameof(Searched)));
            LanguageInfo[nameof(Article)] = new Article(LanguageJson.GetProperty(nameof(Article)));
            LanguageInfo[nameof(Profile)] = new Profile(LanguageJson.GetProperty(nameof(Profile)));
            LanguageInfo[nameof(EditArticle)] = new EditArticle(LanguageJson.GetProperty(nameof(EditArticle)));
            LanguageInfo[nameof(EditProfile)] = new EditProfile(LanguageJson.GetProperty(nameof(EditProfile)));
        }
        private Hashtable LanguageInfo = Hashtable.Synchronized(new Hashtable());
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
            private  JsonElement? LanguageJson { get; set; }
            public Master(JsonElement? _json)
            {
                LanguageJson = _json;
                FooterArray = new List<LinkTable>();
                var footer = LanguageJson.GetProperty(nameof(FooterArray)).Value;
                for (int i = 0; i < footer.GetArrayLength(); i++)
                {
                    FooterArray.Add(new LinkTable(footer[i]));
                }
                MenuArray = new List<LinkTable>();
                var menu = LanguageJson.GetProperty(nameof(MenuArray)).Value;
                for (int i = 0; i < menu.GetArrayLength(); i++)
                {
                    MenuArray.Add(new LinkTable(menu[i]));
                }
            }

            public JsonElement? HeaderConvenienceOptionsArray => LanguageJson.GetProperty(nameof(HeaderConvenienceOptionsArray));
            public string Contact => LanguageJson.GetProperty(nameof(Contact))?.ToString();
            public string Language => LanguageJson.GetProperty(nameof(Language))?.ToString();
            public string FooterCopyright => LanguageJson.GetProperty(nameof(FooterCopyright))?.ToString();
            public string MenuMyProfile => LanguageJson.GetProperty(nameof(MenuMyProfile))?.ToString();
            public string CurrentLanguageCode => LanguageJson.GetProperty(nameof(CurrentLanguageCode))?.ToString();
            public string CurrentLanguage => LanguageJson.GetProperty(nameof(CurrentLanguage))?.ToString();

            public JsonElement? UserOptionsTheme => LanguageJson.GetProperty(nameof(UserOptionsTheme));
            public JsonElement? UserOptionsleftArray => LanguageJson.GetProperty(nameof(UserOptionsleftArray));
            public List<LinkTable> FooterArray { get; }
            public List<LinkTable> MenuArray{ get; }
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
                private  JsonElement? LoginJson { get; set; }
                public LinkTable(JsonElement? _json)
                {
                    LoginJson = _json;
                }
                public string Title => LoginJson.GetProperty(nameof(Title)).ToString();
                public string[] Option1 => new string[] { LoginJson.GetProperty(nameof(Option1))?[0].ToString(), LoginJson.GetProperty(nameof(Option1))?[1].ToString() };
                public string[] Option2 => new string[] { LoginJson.GetProperty(nameof(Option2))?[0].ToString(), LoginJson.GetProperty(nameof(Option2))?[1].ToString() };
                public string[] Option3 => new string[] { LoginJson.GetProperty(nameof(Option3))?[0].ToString(), LoginJson.GetProperty(nameof(Option3))?[1].ToString() };
            }
        }

    }
}