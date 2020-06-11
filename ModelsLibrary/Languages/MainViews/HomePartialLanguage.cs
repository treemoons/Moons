
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
    public partial class Language
    {

        /// <summary>
        ///  the name of one of the Views: Index
        /// </summary>
        public struct Index
        {
            private static JsonElement? LanguageJson { get; set; }
            public Index(JsonElement? _json)
            {
                LanguageJson = _json;
            }
        }
    }
}