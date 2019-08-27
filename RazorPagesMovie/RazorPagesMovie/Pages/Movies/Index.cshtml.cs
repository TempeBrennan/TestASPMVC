using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Models.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Models.RazorPagesMovieContext context)
        {
            _context = context;
            // 试一下从数据到UI能否更新
            // 测试双向绑定是否正常。
            //this.SearchString = "lala";
        }

        public IList<Movie> Movie { get; set; }

        // 标记了BindProperty的属性，可以直接在前端使用，双向绑定规则, 用 asp-for 绑定
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }

        //Use ComboBox
        public SelectList Genres { get; set; }


        // 只要标记了 type="submit"的按钮，点击之后都会触发这个方法
        // 如果外层有有form 并且method="post"，则调用OnPostAsync方法，会把Form里面元素的值传递过来。
        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }

        public void ClickHandler()
        {
            //默认 asp-page-handler="Click" --> OnClickAsync

        }

        public override void OnPageHandlerSelected(
                                    PageHandlerSelectedContext context)
        {

        }

        public override void OnPageHandlerExecuting(
                                    PageHandlerExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("test", new string[] { "123" });
        }


        public override void OnPageHandlerExecuted(
                                    PageHandlerExecutedContext context)
        {

        }
    }
}
