using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Radia.Services;

namespace Radia.Tests.Services
{
    public class ConfigurationServiceTests
    {
        [TestClass]
        public class TheCtorMethod
        {
            [TestMethod]
            public void WhenGivenNoAppConfiguration_ThenWillThrowInvalidOperationException()
            {
                var myConfiguration = new Dictionary<string, string?>();

                var configuration = new ConfigurationBuilder()
                                        .AddInMemoryCollection(myConfiguration)
                                        .Build();

                FluentActions.Invoking(() => new ConfigurationService(configuration)).Should().Throw<InvalidOperationException>();
            }

            [TestMethod]
            public void WhenGivenInvalidAppConfiguration_ThenWillThrowAnException()
            {
                var myConfiguration = new Dictionary<string, string?>()
                {
                    { "AppConfiguration", "NoRealValue" }
                };
                var configuration = new ConfigurationBuilder()
                                        .AddInMemoryCollection(myConfiguration)
                                        .Build();

                FluentActions.Invoking(() => new ConfigurationService(configuration)).Should().Throw<InvalidOperationException>();
            }

            [TestMethod]
            public void WhenGivenAppConfiguration_ThenWillNotThrowAnException()
            {
                var myConfiguration = new Dictionary<string, string?>()
                {
                    { "AppConfiguration:NoValidProperty", "NoRealValue" }
                };
                var configuration = new ConfigurationBuilder()
                                        .AddInMemoryCollection(myConfiguration)
                                        .Build();

                FluentActions.Invoking(() => new ConfigurationService(configuration)).Should().NotThrow();
            }
        }

        [TestClass]
        public class TheGetFooterCopyrightMethod
        {
            [TestMethod]
            public void WhenGivenFooter_ThenWillReturnFooter()
            {
                string footer = "TestCopyright";

                var configuration = new Dictionary<string, string?>
                {
                    {"AppConfiguration:FooterCopyright", footer }
                };

                IConfigurationService configurationService = BuildConfigurationService(configuration);

                configurationService.GetFooterCopyright().Should().Be(footer);
            }

            [TestMethod]
            public void WhenGivenNoFooter_ThenWillReturnEmptyString()
            {
                string footer = "TestCopyright";

                var configuration = new Dictionary<string, string?>
                {
                    {"AppConfiguration:NoFooterCopyright", footer }
                };

                IConfigurationService configurationService = BuildConfigurationService(configuration);

                configurationService.GetFooterCopyright().Should().BeEmpty();
            }
        }

        [TestClass]
        public class TheGetWebsiteTitleMethod
        {
            [TestMethod]
            public void WhenGivenWebsiteTitle_ThenWillWebsiteTitle()
            {
                string websiteTitle = "WebsiteTitle";

                var configuration = new Dictionary<string, string?>
                {
                    {"AppConfiguration:WebsiteTitle", websiteTitle }
                };

                IConfigurationService configurationService = BuildConfigurationService(configuration);

                configurationService.GetWebsiteTitle().Should().Be(websiteTitle);
            }

            [TestMethod]
            public void WhenGivenEmptyWebsiteTitle_ThenWillThrowArgumentException()
            {
                var configuration = new Dictionary<string, string?>
                {
                    {"AppConfiguration:WebsiteTitle", string.Empty }
                };

                IConfigurationService configurationService = BuildConfigurationService(configuration);

                Action act = () => configurationService.GetWebsiteTitle();

                act.Should().Throw<ArgumentException>();
            }

            [TestMethod]
            public void WhenGivenNoWebsiteTitle_ThenWillThrowArgumentException()
            {
                var configuration = new Dictionary<string, string?>
                {
                    {"AppConfiguration:NoWebsiteTitle", string.Empty }
                };

                IConfigurationService configurationService = BuildConfigurationService(configuration);

                Action act = () => configurationService.GetWebsiteTitle();

                act.Should().Throw<ArgumentException>();
            }
        }

        [TestClass]
        public class TheGetPageHeaderMethod
        {
            [TestMethod]
            public void WhenGivenDefaultPageHeader_ThenWillReturnDefaultPageHeader()
            {
                string pageHeader = "PageHeader";

                var configuration = new Dictionary<string, string?>
                {
                    {"AppConfiguration:DefaultPageHeader", pageHeader }
                };

                IConfigurationService configurationService = BuildConfigurationService(configuration);

                configurationService.GetPageHeader().Should().Be(pageHeader);
            }

            [TestMethod]
            public void WhenGivenEmptyWebsiteTitle_ThenWillThrowArgumentException()
            {
                var configuration = new Dictionary<string, string?>
                {
                    {"AppConfiguration:DefaultPageHeader", string.Empty }
                };

                IConfigurationService configurationService = BuildConfigurationService(configuration);

                Action act = () => configurationService.GetPageHeader();

                act.Should().Throw<ArgumentException>();
            }

            [TestMethod]
            public void WhenGivenNoWebsiteTitle_ThenWillThrowArgumentException()
            {
                var configuration = new Dictionary<string, string?>
                {
                    {"AppConfiguration:NoDefaultPageHeader", string.Empty }
                };

                IConfigurationService configurationService = BuildConfigurationService(configuration);

                Action act = () => configurationService.GetPageHeader();

                act.Should().Throw<ArgumentException>();
            }
        }

        [TestClass]
        public class TheGetFileProviderConfigurationsMethod
        {
            [TestMethod]
            public void WhenGivenFileProviderConfigurations_ThenWillReturnSameAmountOfFileProviderConfigurations()
            {
                var configuration = new Dictionary<string, string?>
                {
                    {"AppConfiguration:FileProviderConfigurations:0:Settings:RootDirectory", "/content/"},
                    {"AppConfiguration:FileProviderConfigurations:1:AllowListing", "false"},
                    {"AppConfiguration:FileProviderConfigurations:1:Settings:RootDirectory", "/app/templates/default/views/"},
                    {"AppConfiguration:FileProviderConfigurations:2:AllowListing", "false"},
                    {"AppConfiguration:FileProviderConfigurations:2:Settings:RootDirectory", "/app/templates/default/"},
                };

                IConfigurationService configurationService = BuildConfigurationService(configuration);

                configurationService.GetFileProviderConfigurations().Should().HaveCount(3);
            }

            [TestMethod]
            public void WhenGivenNoWebsiteTitle_ThenWillThrowArgumentException()
            {
                var configuration = new Dictionary<string, string?>
                {
                    {"AppConfiguration:NoFileProviderConfigurations", string.Empty }
                };

                IConfigurationService configurationService = BuildConfigurationService(configuration);

                Action act = () => configurationService.GetFileProviderConfigurations();

                act.Should().Throw<ArgumentException>();
            }
        }


        private static IConfigurationService BuildConfigurationService(Dictionary<string, string?> myConfiguration)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var configurationService = new ConfigurationService(configuration);

            return configurationService;
        }
    }
}
