#pragma checksum "E:\Html\MyWork\MyPersonalWeb\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "07062a6c1c7fbbab5bb0ef89f846217062869a2d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"07062a6c1c7fbbab5bb0ef89f846217062869a2d", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9d85b3321ebb7e5cfdadd7ff87d4d7b0dbc4cba5", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "E:\Html\MyWork\MyPersonalWeb\Views\Home\Index.cshtml"
  
    var test = ViewBag.lang;
    var languageIndex = (Language.Index)Utils.Languages[ViewBag.lang][nameof(Language.Index)];
    ViewData["Title"] = ViewBag.UserName ?? "Home Page";
    List<SelectListItem> li = new List<SelectListItem>();
    for (int i = 0; i < 4; i++)
    {
        SelectListItem listItem = new SelectListItem();
        listItem.Text = "key:" + i;
        listItem.Value = "item.Code" + i;
        li.Add(listItem);
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<style>\r\n    body {\r\n    transition: all 0.s ease-in-out;\r\n    }\r\n</style>\r\n<div class=\"text-center\">\r\n    ");
#nullable restore
#line 21 "E:\Html\MyWork\MyPersonalWeb\Views\Home\Index.cshtml"
Write(Html.DropDownList("title",li));

#line default
#line hidden
#nullable disable
            WriteLiteral(@";
    <div style=""height: 1000px;background-color:plum;width:20px;""></div>
    <div style=""height: 1000px;background-color:red;width:20px;""></div>
    <div style=""height: 1000px;background-color:blue;width:20px;""></div>
    <div style=""height: 1000px;background-color:green;width:20px;""></div>
    <div style=""height: 1000px;background-color:orange;width:20px;""></div>
    <div style=""height: 1000px;background-color:plum;width:20px;""></div>
    <div style=""height: 1000px;background-color:red;width:20px;""></div>
    <div style=""height: 1000px;background-color:blue;width:20px;""></div>
    <div style=""height: 1000px;background-color:green;width:20px;""></div>
    <div style=""height: 1000px;background-color:orange;width:20px;""></div>
    <button onclick=""totop()"">top</button>
");
            WriteLiteral(@"    <script>

        function totop() {
        let a = document.scrollingElement.scrollTop;
        let b = 2;
        b += 0.02 * b
        for (let i = a - 1; i >= 0; i--) {
        setTimeout(() => {
        this.scrollTo(0, i);
        }, 0.04);
        }
        }
    </script>
</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
