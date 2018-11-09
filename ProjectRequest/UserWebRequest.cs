using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MadeWithUnityShowCase.Pages
{
    public class RequestModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "C# SAYS BLEG :D";
        }
    }
}