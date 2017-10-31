using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleEFApp.Models
{
    public class Article
    {


        public Article() { }

        [Key]
        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleDetail { get; set; }
        public string ArticleAuthor { get; set; }
        public Nullable<DateTime> ArticlePublishDate { get; set; }
        public Nullable<DateTime> ArticleModifyDate { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; } // possible nullable
        public virtual Category Category { get; set; }
    }
}