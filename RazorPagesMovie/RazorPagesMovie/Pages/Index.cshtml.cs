using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesMovie.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string GetResult<T>(T pa)
        {
            return pa.ToString() + " By Student";
        }
    }
}
