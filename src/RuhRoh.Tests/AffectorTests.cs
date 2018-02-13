﻿using System;
using RuhRoh.Affectors;
using Xunit;
using Xunit.Sdk;

namespace RuhRoh.Tests
{
    public class AffectorTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Delayer_Should_Delay_With_The_Given_Time(int seconds)
        {
            var affector = new Delayer(TimeSpan.FromSeconds(seconds));

            var t = new ExecutionTimer();
            t.Aggregate(() => affector.Affect());

            Assert.InRange(t.Total, seconds - 1, seconds + 1);
        }

        [Fact]
        public void ExceptionThrower_Should_Throw_An_Exception()
        {
            var exception = new InvalidOperationException();
            var affector = new ExceptionThrower(exception);

            var e = Assert.Throws<InvalidOperationException>(() => affector.Affect());
            Assert.Equal(e, exception);
        }
    }
}