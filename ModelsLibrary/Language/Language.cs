using System;
using System.Collections.Generic;
using System.Collections;
namespace ModelsLibrary
{
    /// <summary>    
    ///  | Author：TreeMoons <br/>
    ///  | Date：May,6th,2020 <br/>
    ///  | work：store the temporary keywords by international language <br/>
    ///  | instructon：store struct by action name,which reflect keywords in the views <br/>
    /// </summary>
    [Serializable]
    public class Language:IEnumerable
    {
        private static Dictionary<string, object> LanguageInfo = new Dictionary<string, object>();
        public object this[string index]
        {
            get => LanguageInfo[index];
            set => LanguageInfo.Add(index, value);
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
            public HrefAndIntroduction HeaderConvenienceOptionOne { get; set; }
            public HrefAndIntroduction HeaderConvenienceOptionTwo { get; set; }
            public HrefAndIntroduction HeaderConvenienceOptionTHree { get; set; }
            public HrefAndIntroduction UserOptionsMyProfile { get; set; }
            public HrefAndIntroduction UserOptionsSettings { get; set; }
            public HrefAndIntroduction UserOptionsArticle { get; set; }
            public HrefAndIntroduction UserOptionsContent { get; set; }
            public HrefAndIntroduction UserOptionsTheme { get; set; }
            public HrefAndIntroduction UserOptionsleft1 { get; set; }
            public HrefAndIntroduction UserOptionsleft2 { get; set; }
            public HrefAndIntroduction UserOptionsleft3 { get; set; }
            string FooterOption { get; set; }


        }
        [Serializable]
        public struct HrefAndIntroduction
        {
            HrefAndIntroduction(string _h, string _i)
            {
                Href = _h;
                Introduction = _i;
            }
            public readonly string Href { get; }
            public readonly string Introduction { get; }
        }
        /// <summary>
        ///  the name of one of the Views: Index
        /// </summary>
        public struct Index
        {
        }

    }
}