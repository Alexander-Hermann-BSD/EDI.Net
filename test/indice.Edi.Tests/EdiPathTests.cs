﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace indice.Edi.Tests
{
	/// <summary>
	/// Edi path tests.
	/// </summary>
    public class EdiPathTests
    {
		/// <summary>
		/// Parses the handles URI and index formats.
		/// </summary>
		/// <param name="text">Text.</param>
        [InlineData("DTM[0][0]")]
        [InlineData("DTM/0/0")]
        [InlineData("DTM/0")]
        [InlineData("DTM")]
        [Theory]
        public void ParseHandlesUriAndIndexFormats(string text)
        {
            var path = EdiPath.Parse(text);
            Assert.True(path.Equals("DTM[0][0]"));  
        }

		/// <summary>
		/// Parses the handles two letter segment names.
		/// </summary>
		/// <param name="text">Text.</param>
        [InlineData("GS[0][0]")]
        [InlineData("GS/0/0")]
        [InlineData("GS/0")]
        [InlineData("GS")]
        [Theory]
        public void ParseHandlesTwoLetterSegmentNames(string text) {
            var path = EdiPath.Parse(text);
            Assert.Equal("GS[0][0]", path.ToString());
        }

		/// <summary>
		/// Parses the handles one letter two number segment names.
		/// </summary>
		/// <param name="text">Text.</param>
        [InlineData("B10[0][0]")]
        [InlineData("B10/0/0")]
        [InlineData("B10/0")]
        [InlineData("B10")]
        [Theory]
        public void ParseHandlesOneLetterTwoNumberSegmentNames(string text) {
            var path = EdiPath.Parse(text);
            Assert.Equal("B10[0][0]", path.ToString());
        }

		/// <summary>
		/// Orders the by structure test.
		/// </summary>
        [Fact]
        public void OrderByStructureTest() {
            var grammar = EdiGrammar.NewEdiFact();

            var input =    new[] { "BGM", "DTM/0/1", "DTM", "DTM/1", "CUX", "UNA", "UNT", "UNB", "UNZ" }.Select(i => (EdiPath)i).ToArray();             
            var expected = new[] { "UNA", "UNB", "BGM", "DTM", "DTM/0/1", "DTM/1/0", "CUX", "UNT", "UNZ" }.Select(i => (EdiPath)i).ToArray();
            var output = input.OrderBy(p => p, new EdiPathComparer(grammar));
            Assert.True(Enumerable.SequenceEqual(expected, output));
        }
    }
}
