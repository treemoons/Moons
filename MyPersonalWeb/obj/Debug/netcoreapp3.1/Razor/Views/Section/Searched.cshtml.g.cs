#pragma checksum "E:\Html\MyWork\MyPersonalWeb\Views\Section\Searched.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9f7ef1659dd3b981ea34270cc6ba61a3ff24affb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Section_Searched), @"mvc.1.0.view", @"/Views/Section/Searched.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\Html\MyWork\MyPersonalWeb\Views\_ViewImports.cshtml"
using MyPersonalWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Html\MyWork\MyPersonalWeb\Views\_ViewImports.cshtml"
using MyPersonalWeb.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\Html\MyWork\MyPersonalWeb\Views\_ViewImports.cshtml"
using ModelsLibrary.Languages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\Html\MyWork\MyPersonalWeb\Views\_ViewImports.cshtml"
using ModelsLibrary.Languages.MainViews;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9f7ef1659dd3b981ea34270cc6ba61a3ff24affb", @"/Views/Section/Searched.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9d85b3321ebb7e5cfdadd7ff87d4d7b0dbc4cba5", @"/Views/_ViewImports.cshtml")]
    public class Views_Section_Searched : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ValueTuple<string, string>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "GET", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("searched"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "E:\Html\MyWork\MyPersonalWeb\Views\Section\Searched.cshtml"
  
    var languageSearched = (Language.Searched)Utils.Languages[ViewBag.lang][nameof(Language.Searched)];
    ViewData["Title"] = ViewBag.UserName ?? Model.Item1;

#line default
#line hidden
#nullable disable
            WriteLiteral("<div>\r\n    <div class=\"searched-keywords\">\r\n        <div>搜索：</div>\r\n        <div class=\"search-option\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9f7ef1659dd3b981ea34270cc6ba61a3ff24affb4607", async() => {
                WriteLiteral("\r\n                <input type=\"search\"");
                BeginWriteAttribute("placeholder", "\r\n                       placeholder=\"", 436, "\"", 561, 1);
#nullable restore
#line 12 "E:\Html\MyWork\MyPersonalWeb\Views\Section\Searched.cshtml"
WriteAttributeValue("", 474, ((Language.Master)Utils.Languages[ViewBag.lang][nameof(Language.Master)]).SearchText, 474, 87, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("\r\n                       name=\"searchtext\"");
                BeginWriteAttribute("value", " value=\"", 604, "\"", 624, 1);
#nullable restore
#line 13 "E:\Html\MyWork\MyPersonalWeb\Views\Section\Searched.cshtml"
WriteAttributeValue("", 612, Model.Item1, 612, 12, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 336, "/", 336, 1, true);
#nullable restore
#line 10 "E:\Html\MyWork\MyPersonalWeb\Views\Section\Searched.cshtml"
AddHtmlAttributeValue("", 337, ViewBag.lang, 337, 13, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue("", 350, "/section/searched", 350, 17, true);
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n        <div class=\"search-icon\">\r\n            <i onclick=\"searched.submit();\"></i>\r\n        </div>\r\n    </div>\r\n    <div onclick=\"signout()\"> 退出登录</div>\r\n    <div class=\"searched-results\">\r\n");
#nullable restore
#line 22 "E:\Html\MyWork\MyPersonalWeb\Views\Section\Searched.cshtml"
         if (Model.Item2 == null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div>没找到</div>\r\n");
#nullable restore
#line 25 "E:\Html\MyWork\MyPersonalWeb\Views\Section\Searched.cshtml"
        }
        else
        {

            

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "E:\Html\MyWork\MyPersonalWeb\Views\Section\Searched.cshtml"
             for (int i = 0; i < 8; i++)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <div class=""searched-result"">
                    <h3 class=""result-title"">
                        <a href=""#""><span>关键字1</span>不相关词语bioatssssss<span>关键字1</span>不相关词语bioatssssss<span>关键字1</span>不相关词语bioatssssss<span>关键字1</span>不相关词语bioatssssss<span>关键字1</span>不相关词语bioatssssss<span>关键字1</span>不相关词语bioatssssss</a>
                    </h3>
                    <article>
                        <span>关键字1</span>不相关词语句子<span>关键字1</span>不相关词语句子<span>关键字1</span>不相关词语句子
                        <span>关键字1</span>不相关词语句子<span>关键字1</span>不相关词语句子<span>关键字1</span>不相关词语句子
                        <span>关键字1</span>不相关词语句子<span>关键字1</span>不相关词语句子<span>关键字1</span>不相关词语句子

                    </article>
                    <address class=""result-author"">作者用户<time>2020-06-05</time></address>
                </div>
");
#nullable restore
#line 43 "E:\Html\MyWork\MyPersonalWeb\Views\Section\Searched.cshtml"

            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "E:\Html\MyWork\MyPersonalWeb\Views\Section\Searched.cshtml"
             
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ValueTuple<string, string>> Html { get; private set; }
    }
}
#pragma warning restore 1591
