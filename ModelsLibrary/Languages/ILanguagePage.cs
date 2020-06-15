using System.Collections;
using System.Text;
using System.Text.Json;

namespace ModelsLibrary.Languages
{
    public interface ILanguagePage
    {
        string Keywords{ get; }
        string Description{ get; }
    }
}