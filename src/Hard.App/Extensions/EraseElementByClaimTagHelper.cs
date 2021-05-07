using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hard.App.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "supress-by-claim-value")]
    public class EraseElementByClaimTagHelper : TagHelper    
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EraseElementByClaimTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HtmlAttributeName("supress-by-claim-name")]
        public string ClaimName { get; set; }

        [HtmlAttributeName("supress-by-claim-value")]
        public string ClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!CustomAuthorization.ValidateUserClaim(_httpContextAccessor.HttpContext, ClaimName, ClaimValue))
                output.SuppressOutput();
        }
    }

}
