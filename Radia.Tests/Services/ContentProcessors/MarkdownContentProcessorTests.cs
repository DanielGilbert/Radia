using FluentAssertions;
using Radia.Services.ContentProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Services.ContentProcessors
{
    public class MarkdownContentProcessorTests
    {
        [TestClass]
        public class TheProcessContentMethod
        {
            [TestMethod]
            public void WhenGivenMarkdownContentToProcess_WillReturnHtml()
            {
                MarkdownContentProcessor markdownContentProcessor = new MarkdownContentProcessor();
                string contentType = "text/markdown";
                string markdownContent = "## Header2";

                string result = markdownContentProcessor.ProcessContent(contentType, markdownContent);

                result.Should().Be("<h2>Header2</h2>");
            }

            [TestMethod]
            public void WhenGivenNoMarkdownContentToProcess_WillReturnTheSameContent()
            {
                MarkdownContentProcessor markdownContentProcessor = new MarkdownContentProcessor();
                string contentType = "text/html";
                string markdownContent = "<h3>Header3</h3>";

                string result = markdownContentProcessor.ProcessContent(contentType, markdownContent);

                result.Should().Be("<h3>Header3</h3>");
            }
        }
    }
}
