
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

        public struct Profile
        {
            private static JsonElement? LanguageJson { get; set; }
            public Profile(JsonElement? _json)
            {
                LanguageJson = _json;
            }

        }
        public struct EditProfile
        {
            private static JsonElement? LanguageJson { get; set; }
            public EditProfile(JsonElement? _json)
            {
                LanguageJson = _json;
            }

        }
        public struct EditArticle
        {
            private static JsonElement? LanguageJson { get; set; }
            public EditArticle(JsonElement? _json)
            {
                LanguageJson = _json;
            }

        }

    }
}