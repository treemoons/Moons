
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
    public partial class Language
    {

        public struct Searched
        {
            private static JsonElement? LanguageJson { get; set; }
            public Searched(JsonElement? _json)
            {
                LanguageJson = _json;
            }
            
        }
        public struct Article
        {
            private static JsonElement? LanguageJson { get; set; }
            public Article(JsonElement? _json)
            {
                LanguageJson = _json;
            }
        }
    }
}