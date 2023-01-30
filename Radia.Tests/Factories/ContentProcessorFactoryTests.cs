using FluentAssertions;
using Radia.Factories.ContentProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Factories
{
    public class ContentProcessorFactoryTests
    {
        [TestClass]
        public class TheCreateMethod
        {
            [TestMethod]
            public void WhenCalled_ThenWillReturnAListOfProcessors()
            {
                IContentProcessorFactory contentProcessorFactory = new ContentProcessorFactory();

                var result = contentProcessorFactory.Create();

                result.Should().NotBeNull();
            }
        }
    }
}
