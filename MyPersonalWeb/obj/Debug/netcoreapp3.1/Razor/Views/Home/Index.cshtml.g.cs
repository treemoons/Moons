#pragma checksum "E:\Html\MyWork\MyPersonalWeb\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "33f0f18d18ff8a775f286c025d0062092198b0d6"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"33f0f18d18ff8a775f286c025d0062092198b0d6", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ddeaa2972ab197f2412167ef66f2b4ed1c414263", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "E:\Html\MyWork\MyPersonalWeb\Views\Home\Index.cshtml"
  
ViewData["Title"] = ViewBag.UserName??"Home Page";
(ModelsLibrary.Language language,object Models)=(ValueTuple<ModelsLibrary.Language,object>)Model;
    try{ 
        var languageIndex=(ModelsLibrary.Language.Index)language[nameof(ModelsLibrary.Language.Index)];
    }catch(Exception ex){

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        console.log(\'");
#nullable restore
#line 8 "E:\Html\MyWork\MyPersonalWeb\Views\Home\Index.cshtml"
                Write(ex.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("\')\r\n    </script>\r\n");
#nullable restore
#line 10 "E:\Html\MyWork\MyPersonalWeb\Views\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"text-center\">\r\n\r\n\r\n\r\n    </div>");
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
