using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleEFApp.Interface;
using SimpleEFApp.Models;

namespace SimpleEFApp.ModelOperation
{
    public class CommonOperations : ICommonOperations
    {
        private static NewsContext context = new NewsContext();
        public bool AddArticle(Article article)
        {
            context.Articles.Add(article);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return context.Articles.ToList();
        }

        public Article GetArticle(int id)
        {
            return context.Articles.Where(a => a.ArticleId == id).SingleOrDefault();
        }

        public IEnumerable<Article> GetByCategory(int id)
        {
            var r = context.Articles.Join(context.Categories, // second table name
            a => a.CategoryId, // foreign key
            c => c.CategoryId, // primary key
            (a, c) => a).Where(a=>a.CategoryId==id); // projection
            return r;
        }

        public void Init() {
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
        }
    }
}