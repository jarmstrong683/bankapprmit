#pragma checksum "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "327d8dfac60a2e28aa4bc530b492383535a1c6dd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Accounts_Statement), @"mvc.1.0.view", @"/Views/Accounts/Statement.cshtml")]
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
#line 1 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\_ViewImports.cshtml"
using A3_MiBank_App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\_ViewImports.cshtml"
using A3_MiBank_App.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
using X.PagedList.Web.Common;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
using System.Globalization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"327d8dfac60a2e28aa4bc530b492383535a1c6dd", @"/Views/Accounts/Statement.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"89cee20c187cc987cfeffe00b460d0050edab692", @"/Views/_ViewImports.cshtml")]
    public class Views_Accounts_Statement : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<A3_MiBank_App.ViewModels.StatementViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Customers", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CustomerAccounts", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 10 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
  
    ViewData["Title"] = "Statement";
    CultureInfo culture = new CultureInfo("en-au");

 

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html lang=\"en-au\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "327d8dfac60a2e28aa4bc530b492383535a1c6dd5350", async() => {
                WriteLiteral("\r\n        <meta name=\"viewport\" content=\"width=device-width\" />\r\n        <title>Statement</title>\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "327d8dfac60a2e28aa4bc530b492383535a1c6dd6429", async() => {
                WriteLiteral("\r\n\r\n        <h3>");
#nullable restore
#line 24 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
       Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h3>\r\n        <h4>Customer:  ");
#nullable restore
#line 25 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
                  Write(Context.Session.GetString(nameof(Customer.CustomerName)));

#line default
#line hidden
#nullable disable
                WriteLiteral("</h4>\r\n        <br />\r\n        <div class=\"form-background\">\r\n\r\n            <h5><span style=\"margin-right: 50px;\"><b>Account:</b> ");
#nullable restore
#line 29 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
                                                             Write(Html.DisplayFor(x => x.Account.AccountNumber));

#line default
#line hidden
#nullable disable
                WriteLiteral("</span><span><b>Balance:</b>");
#nullable restore
#line 29 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
                                                                                                                                       Write(Model.Account.Balance.ToString("C2",culture));

#line default
#line hidden
#nullable disable
                WriteLiteral("</span></h5>\r\n\r\n            <table class=\"table\">\r\n                <tr>\r\n                    <th>");
#nullable restore
#line 33 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
                   Write(Html.DisplayNameFor(x => x.Account.Transactions[0].TransactionID));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n                    <th>");
#nullable restore
#line 34 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
                   Write(Html.DisplayNameFor(x => x.Account.Transactions[0].TransactionType));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n                    <th>");
#nullable restore
#line 35 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
                   Write(Html.DisplayNameFor(x => x.Account.Transactions[0].AccountNumber));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n                    <th>");
#nullable restore
#line 36 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
                   Write(Html.DisplayNameFor(x => x.Account.Transactions[0].DestinationAccountNumber));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n                    <th>");
#nullable restore
#line 37 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
                   Write(Html.DisplayNameFor(x => x.Account.Transactions[0].Amount));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n                    <th>");
#nullable restore
#line 38 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
                   Write(Html.DisplayNameFor(x => x.Account.Transactions[0].Comment));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n                    <th>");
#nullable restore
#line 39 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
                   Write(Html.DisplayNameFor(x => x.Account.Transactions[0].ModifyDate));

#line default
#line hidden
#nullable disable
                WriteLiteral("</th>\r\n\r\n                </tr>\r\n\r\n");
#nullable restore
#line 43 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
                 foreach (var transaction in Model.PagedTransactionsList)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <tr>\r\n\r\n                <td>");
#nullable restore
#line 47 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
               Write(Html.DisplayFor(x => transaction.TransactionID));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 48 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
               Write(Html.DisplayFor(x => transaction.TransactionType));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 49 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
               Write(Html.DisplayFor(x => transaction.AccountNumber));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 50 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
               Write(Html.DisplayFor(x => transaction.DestinationAccountNumber));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 51 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
               Write(transaction.Amount.ToString("C2", culture));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 52 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
               Write(Html.DisplayFor(x => transaction.Comment));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                <!--<td>");
#nullable restore
#line 53 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
                   Write(transaction.ModifyDate.ToLocalTime());

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>-->\r\n                <td>");
#nullable restore
#line 54 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
               Write(Html.FormatValue(transaction.ModifyDate, "{0:dd/MM/yyyy HH:mm:ss}"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n\r\n\r\n            </tr>\r\n");
#nullable restore
#line 58 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
                }

#line default
#line hidden
#nullable disable
                WriteLiteral("            </table>\r\n\r\n            <div style=\"float: right\">\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "327d8dfac60a2e28aa4bc530b492383535a1c6dd13992", async() => {
                    WriteLiteral("Back to Accounts");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            </div>\r\n\r\n            ");
#nullable restore
#line 65 "D:\Users\jarms\Source\Repos\a3-mibank-s3163558_s3694289\A3_MiBank_App\Views\Accounts\Statement.cshtml"
       Write(Html.PagedListPager(Model.PagedTransactionsList, page => Url.Action("Statement", new { page }),
                        new PagedListRenderOptionsBase
                        {
                            LiElementClasses = new[] { "page-item" },
                            PageClasses = new[] { "page-link" }
                        }

                    ));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n            \r\n        </div>\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </html>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<A3_MiBank_App.ViewModels.StatementViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
