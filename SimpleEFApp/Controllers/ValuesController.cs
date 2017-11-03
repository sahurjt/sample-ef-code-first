using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SimpleEFApp.Models;
using SimpleEFApp.Interface;
using SimpleEFApp.ModelOperation;

namespace SimpleEFApp.Controllers
{
    public class ValuesController : ApiController
    {
        private ICommonOperations context;
        public ValuesController(ICommonOperations commonOperations)
        {
            context = commonOperations;
        }

        public ValuesController() {
           context = new CommonOperations();
        }

        // GET api/values
        public IHttpActionResult Get()
        {
            // this can be used to eliminate cyclic refernce problem
            var r = context.GetAllArticles();
            return Ok(r);
        }

        // GET api/values/5
        // [ResponseType(typeof(Article))]
        public IHttpActionResult Get(int id)
        {
            var post = context.GetArticle(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [Route("api/values/add")]
        [HttpPost]
        public IHttpActionResult AddNew(Article article)
        {
            article.ArticlePublishDate = DateTime.UtcNow;
            context.AddArticle(article);
            return Ok("Data Stored");
        }

        // Just for testing
        [Route("api/values/init")]
        [HttpGet]
        public IHttpActionResult Init()
        {
            context.Init();
            return Ok("Operation Completed");
        }

        [Route("api/values/category")]
        [HttpGet]
        public IHttpActionResult ByCat(int id)
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
            //var r = context.Articles.Join(context.Categories, // second table name
            //    a => a.CategoryId, // foreign key
            //    c => c.CategoryId, // primary key
            //    (a, c) => new { a.ArticleTitle, a.ArticleDetail, c.CategoryTitle }); // projection

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


            return Ok(context.GetByCategory(id));
        }
    }
}
