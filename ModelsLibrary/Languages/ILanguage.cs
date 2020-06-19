using System.Collections;
using System.Text;
using System.Text.Json;

namespace ModelsLibrary.Languages
{
    /// <summary>
    /// international language class must inherit it
    /// </summary>
    public interface ILanguage:IEnumerable
    {
        /// <summary>
        /// initialing all  international languages of every each of webpages,which adds to index of theirselves' class
        /// </summary>
        /// <param name="_json">hasttable that saves all international languages' jsonElement</param>
        void Initialing(JsonElement _json);
        object this[string index]{ get; set; }
    }
}