using System.Text;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using SportShop.WebUI.Models;

namespace SportShop.WebUI.HtmlHelpers
{
    public static class PaginingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PaginingInfo paginingInfo, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();

            for (int i = 1; i <= paginingInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href",pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == paginingInfo.CurrentPage)
                tag.AddCssClass("selected");

                result.Append(tag);
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}