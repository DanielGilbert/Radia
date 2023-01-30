using FluentAssertions;
using Radia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Models
{
    public class RadiaAncestorInfoTests
    {
        [TestClass]
        public class TheCtorMethod
        {
            [TestMethod]
            public void WhenGivenAllParameters_WillAssignThemToTheProperties()
            {
                string url = "https://testtld.com";
                string relativePath = "this/is/a/path/";
                string name = "HiFive";

                IRadiaAncestorInfo radiaAncestorInfo = new RadiaAncestorInfo(url, relativePath, name);

                radiaAncestorInfo.Url.Should().Be(url);
                radiaAncestorInfo.RelativePath.Should().Be(relativePath);
                radiaAncestorInfo.Name.Should().Be(name);
            }
        }
    }
}
