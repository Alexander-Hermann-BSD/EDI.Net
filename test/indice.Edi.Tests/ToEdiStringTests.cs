using indice.Edi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace indice.Edi.Tests
{
	/// <summary>
	/// To edi string tests.
	/// </summary>
    public class ToEdiStringTests
    {
		/// <summary>
		/// Integers to string test.
		/// </summary>
        [Fact]
        public void IntegerToStringTest() {
            int value = 12;
            var result1 = EdiExtensions.ToEdiString(value, (Picture)"X(5)");
            var result2 = EdiExtensions.ToEdiString(value, (Picture)"9(5)");
            Assert.Equal("00012", result1);
            Assert.Equal("ZZZ12", result2);
        }
    }
}
