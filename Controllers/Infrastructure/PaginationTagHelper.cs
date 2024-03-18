using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TheWaterProject.Models.ViewModels;

namespace TheWaterProject.Controllers.Infrastructure

    // This is going to build an instance of the PaginationTagHelper and then it's going to build the a tags
    // that we are going to need
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        // Private instance of IUrlHelpFactory (Look up)
        private IUrlHelperFactory urlHelperFactory;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            urlHelperFactory = temp;
        }

        // When this is called its going to give us some information about that view context.
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }
        public string? PageAction { get; set; }
        public PaginationInfo PageModel { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Checks to see if ViewContext has information and the PageModel is not null (Hover over to see information)
            if (ViewContext != null && PageModel != null)
            {
                // Building a new urlHelper (Look up)
                IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

                TagBuilder result = new TagBuilder("div");

                // This loop pretty much building an <a> tag with a href 
                for (int i = 1; i <= PageModel.TotalNumPage; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = i });
                    tag.InnerHtml.Append(i.ToString());

                    // It's then going to append the result to <div> tags
                    result.InnerHtml.AppendHtml(tag);
                }

                // Go to the output of this tag which is the <divs> and <a> tags and goes to the Index.cshtml
                // and replaces the < div page-model> lines that we deleted from earlier.
                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}
