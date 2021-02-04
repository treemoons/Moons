using System.Reflection;
using System;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using CommonUtils;

namespace ModelsLibrary.Languages.MainViews
{

    /// <summary>
    ///  | Author:treemoons <br/>
    ///  | Date:May6th,2020 <br/>
    ///  | work:store the temporary keywords by international language <br/>
    ///  | instructon:store struct by action name,which reflect keywords in the views <br/>
    /// </summary>
    [Serializable]
    public partial class Language : ILanguage
    {
        /// <summary>
        ///
        /// </summary>
        public Language() { }
        /// <summary>
        /// initialing language of json
        /// </summary>
        /// <param name="_json">main of json</param>
        /// <returns> class of laguage </returns>
        public Language(JsonElement _json)=> Initialing(_json);
        /// <summary>
        /// Initialing all international languages.
        /// <br/>throw exception ,if languageInfo has initialed.
        /// </summary>
        /// <param name="_json">json info</param>
        public void Initialing(JsonElement _json)
        {
            if(LanguageInfo.Count>0) throw new Exception ("LanguageInfo has initialed.");
            LanguageInfo[nameof(Master)] = new Master(_json.GetProperty(nameof(Master)));
            LanguageInfo[nameof(Index)] = new Index(_json.GetProperty(nameof(Index)));
            LanguageInfo[nameof(Searched)] = new Searched(_json.GetProperty(nameof(Searched)));
            LanguageInfo[nameof(Article)] = new Article(_json.GetProperty(nameof(Article)));
            LanguageInfo[nameof(Profile)] = new Profile(_json.GetProperty(nameof(Profile)));
            LanguageInfo[nameof(EditArticle)] = new EditArticle(_json.GetProperty(nameof(EditArticle)));
            LanguageInfo[nameof(EditProfile)] = new EditProfile(_json.GetProperty(nameof(EditProfile)));
        }
        /// <summary>
        /// store the language with hash ,which is security , synchronized
        /// </summary>
        /// <returns></returns>
        private Hashtable LanguageInfo{ get; set; } = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// building index of language
        /// </summary>
        /// <value></value>
        public object this[string index]
        {
            get => LanguageInfo[index];
            set => LanguageInfo[index] = value;
        }
        /// <summary>
        /// get IEnumerator about language
        /// </summary>
        /// <returns></returns>
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
            private readonly JsonElement? LanguageJson;
            /// <summary>
            /// initialing Master
            /// </summary>
            /// <param name="_json"></param>
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


            public string ErrorBrowser =>LanguageJson.GetProperty(nameof(ErrorBrowser))?.ToString();
            public JsonElement? HeaderConvenienceOptionsArray => LanguageJson.GetProperty(nameof(HeaderConvenienceOptionsArray));
            public string Contact => LanguageJson.GetProperty(nameof(Contact))?.ToString();
            public string Logout=>LanguageJson.GetProperty(nameof(Logout))?.ToString();
            public string Language => LanguageJson.GetProperty(nameof(Language))?.ToString();
            public string SearchText => LanguageJson.GetProperty(nameof(SearchText))?.ToString();
            public string FooterCopyright => LanguageJson.GetProperty(nameof(FooterCopyright))?.ToString();
            public string MenuMyProfile => LanguageJson.GetProperty(nameof(MenuMyProfile))?.ToString();
            public string CurrentLanguageCode => LanguageJson.GetProperty(nameof(CurrentLanguageCode))?.ToString();
            public string CurrentLanguage => LanguageJson.GetProperty(nameof(CurrentLanguage))?.ToString();
            public JsonElement? SearchType =>LanguageJson.GetProperty(nameof(SearchType));
            public JsonElement? UserOptionsTheme => LanguageJson.GetProperty(nameof(UserOptionsTheme));
            public JsonElement? UserOptionsleftArray => LanguageJson.GetProperty(nameof(UserOptionsleftArray));
            public List<LinkTable> FooterArray { get; }
            public List<LinkTable> MenuArray{ get; }
            public Login? LoginArray => new Login(LanguageJson.GetProperty(nameof(LoginArray)));
            public struct Login
            {
                private readonly JsonElement? LoginJson;
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
                public string ErrorNull => LoginJson.GetProperty(nameof(ErrorNull))?.ToString();
                public string ErrorLogin => LoginJson.GetProperty(nameof(ErrorLogin))?.ToString();
            }
            /// <summary>
            ///  Array contains two options,put using into menu or footer
            /// </summary>
            public struct LinkTable
            {
                private readonly JsonElement? threeOptionsJson;
                public LinkTable(JsonElement? _json)
                {
                    threeOptionsJson = _json;
                }
                public string Title => threeOptionsJson.GetProperty(nameof(Title)).ToString();
                public string[] Option1 => new string[] { threeOptionsJson.GetProperty(nameof(Option1))?[0].ToString(), threeOptionsJson.GetProperty(nameof(Option1))?[1].ToString() };
                public string[] Option2 => new string[] { threeOptionsJson.GetProperty(nameof(Option2))?[0].ToString(), threeOptionsJson.GetProperty(nameof(Option2))?[1].ToString() };
                public string[] Option3 => new string[] { threeOptionsJson.GetProperty(nameof(Option3))?[0].ToString(), threeOptionsJson.GetProperty(nameof(Option3))?[1].ToString() };
            }
        }

    }
}