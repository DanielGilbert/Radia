using FluentAssertions;
using Radia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Services
{
    public class ByteSizeServiceTests
    {
        [TestClass]
        public class TheBuildMethod
        {
            [TestMethod]
            [DataRow((ulong)1039 * 1024 * 348, "353.1 MB", true, DisplayName = "353.1 MB (Grouped)")]
            [DataRow((ulong)1039 * 1024 * 348, "353.1", true, UnitDisplayMode.AlwaysHide, DisplayName = "353.1 (Grouped, Hidden)")]
            [DataRow((ulong)256, "256", true, UnitDisplayMode.HideOnlyForBytes, DisplayName = "256 (HiddenForBytes)")]
            [DataRow((ulong)1024 * 1024, "1 MB", true, UnitDisplayMode.HideOnlyForBytes, DisplayName = "1 MB (HiddenForBytes)")]
            [DataRow((ulong)1024 * 1024, "1 MB", DisplayName = "1 MB")]
            [DataRow((ulong)256, "256 bytes", DisplayName = "256 bytes")]
            [DataRow((ulong)1, "1 byte", DisplayName = "1 byte")]
            public void GivenAByteSizeWithCustomOptions_ThenItWillReturnAFormattedString(ulong byteSize,
                                                                                             string expectedResult,
                                                                                             bool groupDigits = false,
                                                                                             UnitDisplayMode unitDisplayMode = UnitDisplayMode.AlwaysDisplay)
            {
                ByteSizeOptions byteSizeOptions = new()
                {
                    GroupDigits = groupDigits,
                    UnitDisplayMode = unitDisplayMode
                };
                IByteSizeService sut = new ByteSizeService();

                string result = sut.Build(byteSize, byteSizeOptions);

                result.Should().Be(expectedResult);
            }

            [TestMethod]
            [DataRow((ulong)1024 * 1024, "1 MB", DisplayName = "1 MB")]
            [DataRow((ulong)256, "256 bytes", DisplayName = "256 bytes")]
            [DataRow((ulong)1, "1 byte", DisplayName = "1 byte")]
            public void GivenAByteSizeWithDefaultFormatting_ThenItWillReturnAFormattedString(ulong byteSize,
                                                                                 string expectedResult)
            {
                IByteSizeService sut = new ByteSizeService();

                string result = sut.Build(byteSize);

                result.Should().Be(expectedResult);
            }
        }
    }
}
