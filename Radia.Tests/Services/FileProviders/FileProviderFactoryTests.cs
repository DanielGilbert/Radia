using Radia.Services.FileProviders;
using Radia.Services.FileProviders.Local;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.FileProviders;
using Moq;
using Radia.Factories;
using Radia.Services;
using Microsoft.Extensions.Configuration;

namespace Radia.Tests.Services.FileProviders
{
    public class FileProviderFactoryTests
    {
        [TestClass]
        public class TheCreateMethod
        {
            public TestContext? TestContext { get; set; }

        }
    }
}
