using FluentAssertions;
using Moq;
using Radia.Factories;
using Radia.Services.ContentProcessors;
using Radia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Factories
{
    public class ViewFactoryTests
    {
        [TestClass]
        public class TheCreateMethod
        {
            [TestMethod]
            public void GivenAFolderViewModel_WillReturnTheCorrectFolderView()
            {
                FolderViewModel folderViewModel = new(string.Empty,
                                                      string.Empty,
                                                      string.Empty,
                                                      string.Empty);

                var sut = new ViewFactory();
                var result = sut.Create(folderViewModel);

                result.Should().Be("FolderView");
            }

            [TestMethod]
            public void GivenAPathNotFoundViewModel_WillReturnTheCorrectPathNotFoundView()
            {
                PathNotFoundViewModel folderViewModel = new(string.Empty,
                                                            string.Empty,
                                                            string.Empty,
                                                            string.Empty);

                var sut = new ViewFactory();
                var result = sut.Create(folderViewModel);

                result.Should().Be("PathNotFoundView");
            }

            [TestMethod]
            public void GivenAPhysicalFileViewModel_WillReturnTheCorrectPhysicalFileView()
            {
                var contentResult = new Mock<IContentResult<string>>();

                PhysicalFileViewModel folderViewModel = new(contentResult.Object,
                                                            string.Empty,
                                                            string.Empty,
                                                            string.Empty,
                                                            string.Empty,
                                                            string.Empty);

                var sut = new ViewFactory();
                var result = sut.Create(folderViewModel);

                result.Should().Be("PhysicalFileView");
            }

            [TestMethod]
            public void GivenAnInvalidViewModel_WillReturnAnEmptyString()
            {
                var invalidViewModel = new Mock<IViewModel>();

                var sut = new ViewFactory();
                var result = sut.Create(invalidViewModel.Object);

                result.Should().BeEmpty();
            }
        }
    }
}
