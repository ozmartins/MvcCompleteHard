using Hard.Business.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hard.App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatDocument(this RazorPage page, int documentType, string documentId)
        {
            return documentType == (int)DocumentType.CPF ? Convert.ToUInt64(documentId).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(documentId).ToString(@"00\.000\.000\/0000\-00");
        }
    }
}
