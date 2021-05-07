using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hard.App.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-action")]   
    public class EraseElementByActionTagHelper : TagHelper    
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EraseElementByActionTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var action = _httpContextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

            if (!ActionName.Contains(action)) output.SuppressOutput();
        }
    }

}
