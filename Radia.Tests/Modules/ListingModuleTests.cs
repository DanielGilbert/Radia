using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Moq;
using Radia.Factories;
using Radia.Modules;
using Radia.Services;
using Radia.Services.FileProviders;
using Radia.Services.FileProviders.Local;

namespace Radia.Tests.Modules
{
    public class ListingModuleTests
    {
        [TestClass]
        public class TheProcessRequestMethod
        {
            public TestContext? TestContext { get; set; }
        }

        [TestClass]
        public class TheProcessRequestWithArgsMethod
        {
            public TestContext? TestContext { get; set; }
        }
    }
}
