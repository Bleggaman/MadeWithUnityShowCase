#pragma checksum "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e61e9282c4ec83abc7c93181a87e706d64ac391d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(MadeWithUnityShowCase.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Index.cshtml", typeof(MadeWithUnityShowCase.Pages.Pages_Index), null)]
namespace MadeWithUnityShowCase.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/_ViewImports.cshtml"
using MadeWithUnityShowCase;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e61e9282c4ec83abc7c93181a87e706d64ac391d", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1072354d9d95d1f324b686a72b280512c67a84e1", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
  
    ViewData["Title"] = "Home page";

#line default
#line hidden
            BeginContext(71, 48, true);
            WriteLiteral("\r\n\r\n\r\n<div class=\"row\">\r\n    <div>\r\n        <h1>");
            EndContext();
            BeginContext(120, 11, false);
#line 11 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
       Write(Model.Title);

#line default
#line hidden
            EndContext();
            BeginContext(131, 19, true);
            WriteLiteral("</h1>\r\n        <h2>");
            EndContext();
            BeginContext(151, 12, false);
#line 12 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
       Write(Model.Studio);

#line default
#line hidden
            EndContext();
            BeginContext(163, 26, true);
            WriteLiteral("</h2>\r\n        <img src = ");
            EndContext();
            BeginContext(190, 16, false);
#line 13 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
              Write(Model.TitleImage);

#line default
#line hidden
            EndContext();
            BeginContext(206, 70, true);
            WriteLiteral(">\r\n    </div>\r\n</div>\r\n\r\n<div>\r\n    <h1>Text</h1>\r\n    <pre>\r\n        ");
            EndContext();
            BeginContext(277, 10, false);
#line 20 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
   Write(Model.Text);

#line default
#line hidden
            EndContext();
            BeginContext(287, 53, true);
            WriteLiteral("\r\n    </pre>\r\n</div>\r\n\r\n<div>\r\n    <h1>Visuals</h1>\r\n");
            EndContext();
#line 26 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
     for (int i = 0; i < @Model.Images.GetLength(0); i++)
    {
        

#line default
#line hidden
#line 28 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
         if (@Model.Images[i,3] == "yt-thumb embed-responsive-item")
        {
            continue;
        }

#line default
#line hidden
            BeginContext(521, 12, true);
            WriteLiteral("        <img");
            EndContext();
            BeginWriteAttribute("src", " src=", 533, "", 556, 1);
#line 32 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
WriteAttributeValue("", 538, Model.Images[i,0], 538, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 556, "\"", 580, 1);
#line 32 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
WriteAttributeValue("", 562, Model.Images[i,1], 562, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("title", " title=\"", 581, "\"", 607, 1);
#line 32 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
WriteAttributeValue("", 589, Model.Images[i,2], 589, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\"", 608, "\"", 634, 1);
#line 32 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
WriteAttributeValue("", 616, Model.Images[i,3], 616, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(635, 3, true);
            WriteLiteral(">\r\n");
            EndContext();
#line 33 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
    }

#line default
#line hidden
            BeginContext(645, 41, true);
            WriteLiteral("</div>\r\n\r\n<div>   \r\n    <h1>Videos</h1>\r\n");
            EndContext();
#line 38 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
     for (int i = 0; i < @Model.Videos.GetLength(0); i++)
    {
        

#line default
#line hidden
#line 40 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
         if(@Model.Videos[i,1] == "yt-video")
        {

#line default
#line hidden
            BeginContext(810, 19, true);
            WriteLiteral("            <iframe");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 829, "\"", 885, 2);
            WriteAttributeValue("", 835, "https://www.youtube.com/watch?v=", 835, 32, true);
#line 42 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
WriteAttributeValue("", 867, Model.Videos[i,2], 867, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(886, 16, true);
            WriteLiteral("type=\"video/mp4\"");
            EndContext();
            BeginWriteAttribute("poster", " \r\n                poster=\"", 902, "\"", 947, 1);
#line 43 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
WriteAttributeValue("", 929, Model.Videos[i,3], 929, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("title", "\r\n                title=\"", 948, "\"", 991, 1);
#line 44 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
WriteAttributeValue("", 973, Model.Videos[i,4], 973, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(992, 115, true);
            WriteLiteral("\r\n                width=\"640\"\r\n                height=\"360\"\r\n                allowfullscreen>\r\n                <img");
            EndContext();
            BeginWriteAttribute("src", " src=", 1107, "", 1130, 1);
#line 48 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
WriteAttributeValue("", 1112, Model.Videos[i,3], 1112, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1130, 26, true);
            WriteLiteral(">\r\n            </iframe>\r\n");
            EndContext();
#line 50 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
        } else if (@Model.Videos[i,1] == "vm-video")
        {
            

#line default
#line hidden
            BeginContext(1235, 19, true);
            WriteLiteral("            <iframe");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 1254, "\"", 1320, 3);
            WriteAttributeValue("", 1260, "https://player.vimeo.com/video/", 1260, 31, true);
#line 53 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
WriteAttributeValue("", 1291, Model.Videos[i,2], 1291, 18, false);

#line default
#line hidden
            WriteAttributeValue("", 1309, "?autoplay=1", 1309, 11, true);
            EndWriteAttribute();
            BeginContext(1321, 35, true);
            WriteLiteral(" \r\n                type=\"video/mp4\"");
            EndContext();
            BeginWriteAttribute("poster", " \r\n                poster=\"", 1356, "\"", 1401, 1);
#line 55 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
WriteAttributeValue("", 1383, Model.Videos[i,3], 1383, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("title", "\r\n                title=\"", 1402, "\"", 1445, 1);
#line 56 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
WriteAttributeValue("", 1427, Model.Videos[i,4], 1427, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1446, 115, true);
            WriteLiteral("\r\n                width=\"640\"\r\n                height=\"360\"\r\n                allowfullscreen>\r\n                <img");
            EndContext();
            BeginWriteAttribute("src", " src=", 1561, "", 1584, 1);
#line 60 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
WriteAttributeValue("", 1566, Model.Videos[i,3], 1566, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1584, 26, true);
            WriteLiteral(">\r\n            </iframe>\r\n");
            EndContext();
#line 62 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"

        }

#line default
#line hidden
#line 63 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
         
    }

#line default
#line hidden
            BeginContext(1630, 40, true);
            WriteLiteral("</div>\r\n\r\n<div>   \r\n    <h1>Links</h1>\r\n");
            EndContext();
#line 69 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
     for (int i = 0; i < @Model.Links.GetLength(0); i++)
    {

#line default
#line hidden
            BeginContext(1735, 10, true);
            WriteLiteral("        <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1745, "\"", 1769, 1);
#line 71 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
WriteAttributeValue("", 1752, Model.Links[i,0], 1752, 17, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1770, 15, true);
            WriteLiteral(">\r\n            ");
            EndContext();
            BeginContext(1786, 16, false);
#line 72 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
       Write(Model.Links[i,1]);

#line default
#line hidden
            EndContext();
            BeginContext(1802, 30, true);
            WriteLiteral("\r\n        </a>\r\n        <br>\r\n");
            EndContext();
#line 75 "/Users/britthenderson/Desktop/MadeWithUnityShowCase/MadeWithUnityShowCase/Pages/Index.cshtml"
    }

#line default
#line hidden
            BeginContext(1839, 8, true);
            WriteLiteral("</div>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel>)PageContext?.ViewData;
        public IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
