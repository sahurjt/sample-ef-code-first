using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleEFApp;
using SimpleEFApp.Models;

namespace SimpleEFApp.Interface
{
    public interface ICommonOperations
    {
        IEnumerable<Article> GetAllArticles();

        Article GetArticle(int id);

        bool AddArticle(Article article);

        IEnumerable<Article> GetByCategory(int id);

        void Init();
    }
}
