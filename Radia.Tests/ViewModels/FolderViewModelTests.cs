using FluentAssertions;
using Radia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.ViewModels
{
    public class FolderViewModelTests
    {
        [TestClass]
        public class ThePageHeaderProperty
        {
            [TestMethod]
            public void WhenCtorGetsTheDefaultHeader_ThenTheDefaultHeaderIsReturned()
            {
                string defaultHeader = "DefaultHeader";
                var folderViewModel = new FolderViewModel(defaultHeader);

                folderViewModel.PageHeader.Should().Be(defaultHeader);
            }
        }
    }
}
