using System.Collections;
using System.Text;
using System.Text.Json;

namespace ModelsLibrary.Languages
{
/// <summary>
/// 页面语言
/// </summary>
    public interface ILanguagePage
    {
        /// <summary>
        /// Meta 标签中的关键词
        /// </summary>
        /// <value></value>
        string Keywords{ get; }
        /// <summary>
        /// 
        ///Meta 中网站的描述
        /// </summary>
        /// <value></value>
        string Description{ get; }
    }
}