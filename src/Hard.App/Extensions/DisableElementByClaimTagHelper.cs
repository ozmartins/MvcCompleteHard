using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hard.App.Extensions
{
    [HtmlTargetElement("a", Attributes = "disable-by-claim-name")]
    [HtmlTargetElement("a", Attributes = "disable-by-claim-value")]
    public class DisableElementByClaimTagHelper : TagHelper    
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DisableElementByClaimTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HtmlAttributeName("disable-by-claim-name")]
        public string ClaimName { get; set; }

        [HtmlAttributeName("disable-by-claim-value")]
        public string ClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!CustomAuthorization.ValidateUserClaim(_httpContextAccessor.HttpContext, ClaimName, ClaimValue))
            {
                output.Attributes.RemoveAll("href");
                output.Attributes.Add(new TagHelperAttribute("style", "cursor: not-allowed"));
                output.Attributes.Add(new TagHelperAttribute("title", "you don't have permission"));
            }                          
        }
    }

}
