﻿using System;
using BCLExtensions.Tests.TestHelpers;
using Xunit;
using Xunit.Extensions;

namespace BCLExtensions.Tests.IntExtensions
{
    public class IsBetweenExclusiveExclusiveTests
    {
        [Theory]
        [InlineData(0, -1, 1)]
        [InlineData(0, int.MinValue, int.MaxValue)]
        [InlineData(42, 30, 60)]
        [InlineData(-30, -40, -20)]
        public void InputToIsBetweenWithLimitsReturnsTrue(int input, int lowerLimit, int upperLimit)
        {
            var result = input.IsBetween(lowerLimit.Exclusive(), upperLimit.Exclusive());

            Assert.True(result);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(42, 42, 42)]
        [InlineData(5, 5, 6)]
        [InlineData(6, 5, 6)]
        [InlineData(0, 1, 2)]
        [InlineData(0, -2, -1)]
        [InlineData(42, 5, 12)]
        [InlineData(42, 50, 100)]
        [InlineData(-30, -20, -10)]
        [InlineData(-30, -50, -40)]
        public void InputToIsBetweenWithLimitsReturnsFalse(int input, int lowerLimit, int upperLimit)
        {
            var result = input.IsBetween(lowerLimit.Exclusive(), upperLimit.Exclusive());

            Assert.False(result);
        }

        [Fact]
        public void LimitsAreReversedThrowsException()
        {
            Func<int, ExclusiveInteger, ExclusiveInteger, bool> isBetween = BCLExtensions.IntExtensions.IsBetween;

            Assert.Throws<InvalidOperationException>(isBetween.AsActionUsing(0, 100.Exclusive(), 20.Exclusive()));
        }
    }
}
