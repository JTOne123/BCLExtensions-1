﻿using System;
using Xunit;
using Xunit.Extensions;

namespace BCLExtensions.Tests.IntTimespanExtensions
{
    public class MinuteTests
    {
        [Fact]
        public void WorksWhenUsedOnAnInlineConstant()
        {
            TimeSpan result = (5).Minute();

            Assert.Equal(5, result.TotalMinutes);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(265)]
        [InlineData(9001)]
        [InlineData(-1)]
        [InlineData(-5)]
        [InlineData(-10)]
        [InlineData(-265)]
        [InlineData(-9001)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void WhenGivenANumberThenReturnsCorrectTimeSpan(int numberOfMinutes)
        {
            TimeSpan result = numberOfMinutes.Minute();

            Assert.Equal(numberOfMinutes, result.TotalMinutes);
        }
    }
}
