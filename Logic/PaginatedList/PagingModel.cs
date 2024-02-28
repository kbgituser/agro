using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PlatF.Logic.PaginatedList
{
    public class PagingModel : PageModel
    {
        [BindProperty]
        public IEnumerable<CultureInfo> Cultures { get; set; }

        [BindProperty]
        public int TotalRecords { get; set; }

        [BindProperty]
        public int PageNo { get; set; }

        [BindProperty]
        public int PageSize { get; set; }

        public void OnGet(int p = 1, int s = 10)
        {
            Cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .OrderBy(x => x.EnglishName).Skip((p - 1) * s).Take(s);

            TotalRecords = CultureInfo.GetCultures(CultureTypes.AllCultures).Count();
            PageNo = p;
            PageSize = s; // required if page size dropdown is enabled
        }
    }
}
