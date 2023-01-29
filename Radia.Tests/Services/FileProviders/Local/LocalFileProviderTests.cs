using FluentAssertions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Moq;
using Radia.Exceptions;
using Radia.Services.FileProviders;
using Radia.Services.FileProviders.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Services.FileProviders.Local
{
    public class LocalFileProviderTests
    {
        [TestClass]
        public class TheCtorMethod
        {
            public TestContext? TestContext { get; set; }
        }

        [TestClass]
        public class TheGetDirectoryContentMethod
        {
            public TestContext? TestContext { get; set; }
        }

        [TestClass]
        public class TheGetFileInfoMethod
        {
            public TestContext? TestContext { get; set; }
        }

        [TestClass]
        public class TheWatchMethod
        {
            public TestContext? TestContext { get; set; }
        }
    }
}
