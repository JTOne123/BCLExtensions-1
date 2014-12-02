﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCLExtensions.Tests.StringExtensions
{
    [TestClass]
    public class ValueOrEmptyIfNullTests
    {
        [TestMethod]
        public void WithEmptyInputStringReturnsEmptyString()
        {
            string input = "";
            var result = input.ValueOrEmptyIfNull();
            Assert.AreEqual(string.Empty, result);
        }
        
        [TestMethod]
        public void WithNullInputStringReturnsEmptyString()
        {
            string input = null;
            var result = input.ValueOrEmptyIfNull();
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void WithNewLineInputStringReturnsOriginalString()
        {
            string input = "\n";
            var result = input.ValueOrEmptyIfNull();
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void WithEmptySpacesInputStringReturnsOriginalString()
        {
            string input = "   ";
            var result = input.ValueOrEmptyIfNull();
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void WithNonEmptyInputStringReturnsOriginalString()
        {
            string input = "The quick brown fox jumps over the lazy dog.";
            var result = input.ValueOrEmptyIfNull();
            Assert.AreEqual(input, result);
        }
    }
}