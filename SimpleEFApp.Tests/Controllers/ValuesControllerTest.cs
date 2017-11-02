using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleEFApp;
using SimpleEFApp.Models;
using SimpleEFApp.Controllers;
using FakeItEasy;

namespace SimpleEFApp.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        private static NewsContext context = new NewsContext();
        private static ValuesController controller = new ValuesController
        {
            Request = new System.Net.Http.HttpRequestMessage(),
            Configuration = new System.Web.Http.HttpConfiguration()
        };

        [TestMethod]
        public void Get()
        {
            // Act
            var result = controller.Get();
            var result_db = context.Articles;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result_db.Count(), result.Count());
            Assert.AreEqual(result_db.Find(1).ArticleTitle, result.ElementAt(0).ArticleTitle);

        }

        [TestMethod]
        public void GetById()
        {
            var result = controller.Get(1);
            var result_db = context.Articles.Find(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result_db.ArticleTitle, result.ArticleTitle);

        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            ValuesController controller = new ValuesController();


        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            ValuesController controller = new ValuesController();

        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ValuesController controller = new ValuesController();

        }

        [TestMethod]
        public void SampleFakeItTest()
        {
            var a = A.Fake<Article>();
            var res = A.CallTo(() => a.ArticleId).Returns(1);

        }

        class B<T>
        {
            private List<T> list = new List<T>();
            public void FakeList(int size)
            {

            }
        }
    }

}
