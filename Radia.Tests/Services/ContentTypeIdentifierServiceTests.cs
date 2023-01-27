using FluentAssertions;
using Radia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Services
{
    public class ContentTypeIdentifierServiceTests
    {
        [TestClass]
        public class TheGetContentTypeFromMethod
        {
            [DataRow("testfile.html", "text/html", DisplayName = "text/html")]
            [DataRow("testfile.png", "image/png", DisplayName = "image/png")]
            [DataTestMethod]
            public void GivenAKnownFileExtension_WillReturnAMatchingContentType(string fileExtension, string expectedContentType)
            {
                var contentTypeIdentifierService = new ContentTypeIdentifierService();
                var result = contentTypeIdentifierService.GetContentTypeFrom(fileExtension);

                result.Should().Be(expectedContentType);
            }

            [TestMethod]
            public void GivenAnUnknownFileExtension_WillReturnAnEmptyString()
            {
                var contentTypeIdentifierService = new ContentTypeIdentifierService();
                var result = contentTypeIdentifierService.GetContentTypeFrom(".unknownExtension");

                result.Should().BeEmpty();
            }
        }
    }
}
