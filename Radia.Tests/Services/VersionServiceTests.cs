using FluentAssertions;
using Radia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Services
{
    public class VersionServiceTests
    {
        [TestClass]
        public class TheVersionProperty
        {
            [TestMethod]
            public void WhenCalled_WillReturnTheCorrectVersion()
            {
                string existingVersion = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? String.Empty;

                IVersionService versionService = new VersionService(existingVersion);

                string result = versionService.Version;

                result.Should().Be(existingVersion);
            }
        }

        [TestClass]
        public class TheGetVersionLinkedMethod
        {
            [TestMethod]
            public void WhenCalled_WillReturnTheCorrectLinkedVersion()
            {
                string existingVersion = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? String.Empty;
                string existingLink = $"<a href=\"https://github.com/DanielGilbert/Radia/releases/tag/v{existingVersion}\">v{existingVersion}</a>";
                IVersionService versionService = new VersionService(existingVersion);

                string result = versionService.GetVersionLinked();

                result.Should().Be(existingLink);
            }

            [TestMethod]
            public void WhenVersionIsEmpty_WillReturnAnEmptyString()
            {
                IVersionService versionService = new VersionService(string.Empty);

                string result = versionService.GetVersionLinked();

                result.Should().BeEmpty();
            }
        }
    }
}
