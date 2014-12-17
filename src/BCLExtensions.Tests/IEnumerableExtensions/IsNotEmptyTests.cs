﻿using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BCLExtensions.Tests.IEnumerableExtensions
{
    public class IsNotEmptyTests
    {
        public abstract class GivenABase<T>
        {
            protected abstract IEnumerable<T> GetEmptyEnumerable();

            protected abstract IEnumerable<T> GetEnumerableWithOneNonNullItem();

            [Fact]
            public void WhenNullThenReturnsTrue()
            {
                IEnumerable<T> input = null;
                var result = input.IsNotEmpty();
                Assert.False(result);
            }

            [Fact]
            public void WhenEmptyThenReturnsTrue()
            {
                IEnumerable<T> input = GetEmptyEnumerable();
                var result = input.IsNotEmpty();
                Assert.False(result);
            }

            [Fact]
            public void WhenNonEmptyThenReturnsFalse()
            {
                IEnumerable<T> input = GetEnumerableWithOneNonNullItem();
                var result = input.IsNotEmpty();
                Assert.True(result);
            }
        }

        public abstract class GivenAnEnumerableOfBase<T> : GivenABase<T>
        {
            protected override IEnumerable<T> GetEmptyEnumerable()
            {
                return Enumerable.Empty<T>();
            }

            protected override IEnumerable<T> GetEnumerableWithOneNonNullItem()
            {
                return Enumerable.Range(1, 1).Select(n => CreateItem());
            }

            protected abstract T CreateItem();
        }

        public class GivenAnEnumerableOfString : GivenAnEnumerableOfBase<string>
        {
            protected override string CreateItem()
            {
                return "Test string";
            }
        }

        public class GivenAnEnumerableOfInt : GivenAnEnumerableOfBase<int>
        {
            protected override int CreateItem()
            {
                return 42;
            }
        }

        public abstract class GivenAnArrayOfBase<T> : GivenABase<T>
        {
            protected override IEnumerable<T> GetEmptyEnumerable()
            {
                return new T[0];
            }

            protected override IEnumerable<T> GetEnumerableWithOneNonNullItem()
            {
                return new[] { CreateItem() };
            }

            protected abstract T CreateItem();
        }

        public class GivenAnArrayOfString : GivenAnArrayOfBase<string>
        {
            protected override string CreateItem()
            {
                return "Test string";
            }
        }

        public class GivenAnArrayOfInt : GivenAnArrayOfBase<int>
        {
            protected override int CreateItem()
            {
                return 42;
            }
        }

        public abstract class GivenAListOfBase<T> : GivenABase<T>
        {
            protected override IEnumerable<T> GetEmptyEnumerable()
            {
                return new List<T>();
            }

            protected override IEnumerable<T> GetEnumerableWithOneNonNullItem()
            {
                return new[] { CreateItem() };
            }

            protected abstract T CreateItem();
        }

        public class GivenAListOfString : GivenAListOfBase<string>
        {
            protected override string CreateItem()
            {
                return "Test string";
            }
        }

        public class GivenAListOfInt : GivenAListOfBase<int>
        {
            protected override int CreateItem()
            {
                return 42;
            }
        }

        public abstract class GivenADictionaryOfBase<T> : GivenABase<KeyValuePair<object, T>>
        {
            protected override IEnumerable<KeyValuePair<object, T>> GetEmptyEnumerable()
            {
                return new Dictionary<object, T>();
            }

            protected override IEnumerable<KeyValuePair<object, T>> GetEnumerableWithOneNonNullItem()
            {
                return new Dictionary<object, T> { { new object(), CreateItem() } };
            }

            protected abstract T CreateItem();
        }

        public class GivenADictionaryOfObjectString : GivenADictionaryOfBase<string>
        {
            protected override string CreateItem()
            {
                return "Test string";
            }
        }

        public class GivenADictionaryOfObjectInt : GivenADictionaryOfBase<int>
        {
            protected override int CreateItem()
            {
                return 42;
            }
        }
    }
}
