using FluentAssertions;
using Radia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Services
{
    public class FooterServiceTests
    {
        [TestClass]
        public class TheGetFormattedFooterMethod
        {
            public TestContext? TestContext { get; set; }

            [TestMethod]
            public void WhenGivenCorrectValues_WillReturnExpectedFooter()
            {
                string result = "&copy; Daniel Gilbert, 2009 - 2023 | <a href=\"https://github.com/DanielGilbert/Radia/releases/tag/v3.0.0\">v3.0.0</a> | Made with ❤ &amp; ☕ using <a href=\"https://github.com/DanielGilbert/Radia\">Radia</a>";
                DefaultRadiaTestContext defaultRadiaTestContext = new(TestContext!.TestRunResultsDirectory!);

                IFooterService footerService = new FooterService(defaultRadiaTestContext.ConfigurationService,
                                                                 defaultRadiaTestContext.DateTimeService,
                                                                 defaultRadiaTestContext.VersionService);

                var footerResult = footerService.GetFormattedFooter();

                footerResult.Should().Be(result);
            }
        }
    }
}
