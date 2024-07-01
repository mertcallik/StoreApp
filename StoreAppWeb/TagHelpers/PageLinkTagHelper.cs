using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StoreAppWeb.Models;

namespace StoreAppWeb.TagHelpers
{
    [HtmlTargetElement("div",Attributes = "page-model")]
    public class PageLinkTagHelper:TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory= urlHelperFactory;
        }

        [ViewContext]
        public ViewContext? ViewContext { get; set; }
        public PageInfo? PageModel { get; set; }
        public string? PageAction { get; set; }
        public string PageClass { get; set; } = string.Empty;
        public string PageClassLink { get; set; } = string.Empty;
        public string PageClassActive { get; set; } = string.Empty;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext!=null&&PageModel!=null)
            {
                TagBuilder div = new TagBuilder("div");
                var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
                for (int i = 1; i <= PageModel.TotalPages; i++)
                {
                    TagBuilder a=new TagBuilder("a");
                    a.Attributes["href"] = urlHelper.Action(PageAction, new { page = i });
                    a.AddCssClass(PageClass);
                    a.AddCssClass(i==PageModel.CurrentPage?PageClassActive:PageClassLink);
                    a.InnerHtml.Append(i.ToString());
                    div.InnerHtml.AppendHtml(a);
                }

                output.Content.AppendHtml(div);
            }
        }
    }
}
