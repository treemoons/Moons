using System.Collections;
using System.Text;
using System.Text.Json;

namespace ModelsLibrary.Languages
{
    /// <summary>
    /// internal language class must inherit。
    /// </summary>
    public interface ILanguage:IEnumerable
    {
        void Initialing(JsonElement _json);
        object this[string index]{ get; set; }
    }
}