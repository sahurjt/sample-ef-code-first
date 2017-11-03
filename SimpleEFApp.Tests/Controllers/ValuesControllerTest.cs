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
using SimpleEFApp.Interface;
using System.Web.Http.Results;

namespace SimpleEFApp.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        // Dont use database resource in testing.
        // Instead use Mocker  and Fakers
        // Here i am using FakeItEasy library
        private readonly static Article DEMO_ARTICLE = new Article { ArticleId = 1, ArticleTitle = "anon", CategoryId = 1 };

        [TestMethod]
        public void Get()
        {
            // Arrange 
            var mockObject = A.Fake<ICommonOperations>();
            A.CallTo(() => mockObject.GetAllArticles()).Returns(new List<Article> { DEMO_ARTICLE });
            var controller = new ValuesController(mockObject);

            // Act
            IHttpActionResult result = controller.Get();
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<Article>>;
            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.Content.Count());
            Assert.AreEqual("anon", contentResult.Content.ElementAt(0).ArticleTitle);

        }

        [TestMethod]
        public void GetById()
        {
            var mockObject = A.Fake<ICommonOperations>();
            A.CallTo(() => mockObject.GetArticle(1)).Returns(DEMO_ARTICLE);
            var controller = new ValuesController(mockObject);

            // Act
            IHttpActionResult result = controller.Get(1);
            var contentResult = result as OkNegotiatedContentResult<Article>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("anon", contentResult.Content.ArticleTitle);

        }

        [TestMethod]
        public void Add()
        {
            // Arrange
            var mockObject = A.Fake<ICommonOperations>();
            A.CallTo(() => mockObject.AddArticle(DEMO_ARTICLE)).Returns(true);
            var controller = new ValuesController(mockObject);

            // Act
            IHttpActionResult result = controller.AddNew(DEMO_ARTICLE);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotInstanceOfType(result, typeof(OkResult));

        }

        [TestMethod]
        public void Init()
        {
            // Arrange
            var mockObject = A.Fake<ICommonOperations>();
            A.CallTo(() => mockObject.Init());
            var controller = new ValuesController(mockObject);

            // Act
            IHttpActionResult result = controller.Init();
            var contentResult = result as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual("Operation Completed", contentResult.Content);
        }

        [TestMethod]
        public void GetByCategory()
        {
            // Arrange
            var mockObject = A.Fake<ICommonOperations>();
            A.CallTo(() => mockObject.GetByCategory(1)).Returns(new List<Article> { DEMO_ARTICLE });
            var controller = new ValuesController(mockObject);

            // Act
            IHttpActionResult result = controller.ByCat(1);
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<Article>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual(1, contentResult.Content.Count());
            Assert.AreEqual("anon", contentResult.Content.ElementAt(0).ArticleTitle);
        }
    }

}
