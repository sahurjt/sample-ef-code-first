using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SimpleEFApp.Models;

namespace SimpleEFApp.Controllers
{
    public class ValuesController : ApiController
    {
        NewsContext context = new NewsContext();
        // GET api/values
        public IEnumerable<Article> Get()
        {
            var r = context.Articles.ToList();
            return r;
        }

        // GET api/values/5
       // [ResponseType(typeof(Article))]
        public Article Get(int id)
        {
            var post = context.Articles.Where(a => a.ArticleId == id).SingleOrDefault();
            if (post == null) return null;
            return post;
        }

        [Route("api/values/add")]
        [HttpPost]
        public IHttpActionResult AddNew(Article article)
        {
            article.ArticlePublishDate = DateTime.UtcNow;
            context.Articles.Add(article);
            context.SaveChanges();
            return Ok("Data Stored");
        }

        [Route("api/values/init")]
        [HttpGet]
        public string Init()
        {
            Article article = new Article
            {
                ArticleId = 1,
                ArticleAuthor = "Rajat s",
                ArticleTitle = "second article",
                ArticleDetail = "Some big Details",
                // CategoryId=2};
                Category = new Category { CategoryId = 1, CategoryTitle = "Random" }
            };
            context.Articles.Add(article);
            context.SaveChanges();

            return "Operation Completed";
        }

        [Route("api/values/category")]
        [HttpGet]
        public IHttpActionResult ByCat()
        {
            // select list of (article+category) where categoryid=1 using linq query
            //var r = (from a in context.Articles
            //         join c in context.Categories on a.CategoryId equals c.CategoryId
            //         where c.CategoryId == 1
            //         select new
            //         {
            //             s = c.CategoryTitle,
            //             name = c.CategoryId,
            //             arName = a.ArticleTitle
            //         });
            //--------------------------------------------------------------------------

            //--------------------------------------------------------------------------
            // do same task using lambda expression

            //--------------------------------------------------------------------------
            // projection
            //var r = context.Articles.Select(a => new { aid = a.ArticleId, atitle = a.ArticleTitle, acat = a.CategoryId });

            //--------------------------------------------------------------------------
            // find article whose primary key is 1
            //var r = context.Articles.Find(1);

            //--------------------------------------------------------------------------
            // inner join using lambda exp.
            var r = context.Articles.Join(context.Categories, // second table name
                a => a.CategoryId, // foreign key
                c => c.CategoryId, // primary key
                (a, c) => new { a.ArticleTitle, a.ArticleDetail, c.CategoryTitle }); // projection

            //--------------------------------------------------------------------------
            //// like in lambda expression
            //var start_with = "";
            //var r = context.Articles.Where(a=>a.ArticleTitle.StartsWith(start_with));
            //// similarly Contains,EndWith,Equals method can be used. 

            //--------------------------------------------------------------------------
            // Inner Query with lambda expression
            //--------------------------------------------------------------------------
            //        var accountBalance = context
            //.AccountBalanceByDate
            //.Where(a =>
            //    a.Date == context.AccountBalanceByDate
            //         .Where(b => b.AccountId == a.AccountId && b.Date < date).Max(b => b.Date));

            
            return Ok(r);
        }
    }
}
