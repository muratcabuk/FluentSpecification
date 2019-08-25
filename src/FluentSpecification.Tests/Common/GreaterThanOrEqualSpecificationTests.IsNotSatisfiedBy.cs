﻿using System.Collections.Generic;
using FluentSpecification.Abstractions.Validation;
using FluentSpecification.Common;
using FluentSpecification.Tests.Data;
using FluentSpecification.Tests.Sdk;
using Xunit;

namespace FluentSpecification.Tests.Common
{
    public partial class GreaterThanOrEqualSpecificationTests
    {
        public class IsNotSatisfiedBy
        {
            [Theory]
            [CorrectData(typeof(GreaterThanOrEqualData), AsNegation = true)]
            public void NotGreaterThanOrEqualIntCandidate_ReturnTrue<T>(T candidate, T greaterThan,
                IComparer<T> comparer)
            {
                candidate = candidate?.ToString() != "null" ? candidate : default;
                var sut = new GreaterThanOrEqualSpecification<T>(greaterThan, comparer);

                var result = sut.IsNotSatisfiedBy(candidate);

                Assert.True(result);
            }

            [Theory]
            [IncorrectData(typeof(GreaterThanOrEqualData), AsNegation = true)]
            public void GreaterThanOrEqualCandidate_ReturnFalse<T>(T candidate, T greaterThan, IComparer<T> comparer)
            {
                candidate = candidate?.ToString() != "null" ? candidate : default;
                var sut = new GreaterThanOrEqualSpecification<T>(greaterThan, comparer);

                var result = sut.IsNotSatisfiedBy(candidate);

                Assert.False(result);
            }

            [Theory]
            [CorrectData(typeof(GreaterThanOrEqualNullableData), AsNegation = true)]
            public void NullableCandidate_ReturnTrue(int? candidate, int? greaterThan)
            {
                var sut = new GreaterThanOrEqualSpecification<int?>(greaterThan);

                var result = sut.IsNotSatisfiedBy(candidate);

                Assert.True(result);
            }

            [Theory]
            [IncorrectData(typeof(GreaterThanOrEqualNullableData), AsNegation = true)]
            public void NullableCandidate_ReturnFalse(int? candidate, int? greaterThan)
            {
                var sut = new GreaterThanOrEqualSpecification<int?>(greaterThan);

                var result = sut.IsNotSatisfiedBy(candidate);

                Assert.False(result);
            }
        }

        public class IsNotSatisfiedBySpecificationResult
        {
            [Theory]
            [CorrectValidationData(typeof(GreaterThanOrEqualData), AsNegation = true)]
            public void NotGreaterThanOrEqualIntCandidate_ReturnExpectedResultObject<T>(T candidate, T greaterThan,
                IComparer<T> comparer, SpecificationResult expected)
            {
                candidate = candidate?.ToString() != "null" ? candidate : default;
                var sut = new GreaterThanOrEqualSpecification<T>(greaterThan, comparer);

                var overall = sut.IsNotSatisfiedBy(candidate, out var result);

                Assert.True(overall);
                Assert.Equal(expected, result, new SpecificationResultComparer());
            }

            [Theory]
            [IncorrectValidationData(typeof(GreaterThanOrEqualData), AsNegation = true)]
            public void GreaterThanOrEqualCandidate_ReturnExpectedResultObject<T>(T candidate, T greaterThan,
                IComparer<T> comparer, SpecificationResult expected)
            {
                candidate = candidate?.ToString() != "null" ? candidate : default;
                var sut = new GreaterThanOrEqualSpecification<T>(greaterThan, comparer);

                var overall = sut.IsNotSatisfiedBy(candidate, out var result);

                Assert.False(overall);
                Assert.Equal(expected, result, new SpecificationResultComparer(candidate));
            }

            [Theory]
            [CorrectValidationData(typeof(GreaterThanOrEqualNullableData), AsNegation = true)]
            public void CorrectNullableCandidate_ReturnExpectedResultObject(int? candidate, int? greaterThan,
                SpecificationResult expected)
            {
                var sut = new GreaterThanOrEqualSpecification<int?>(greaterThan);

                var overall = sut.IsNotSatisfiedBy(candidate, out var result);

                Assert.True(overall);
                Assert.Equal(expected, result, new SpecificationResultComparer());
            }

            [Theory]
            [IncorrectValidationData(typeof(GreaterThanOrEqualNullableData), AsNegation = true)]
            public void IncorrectNullableCandidate_ReturnExpectedResultObject(int? candidate, int? greaterThan,
                SpecificationResult expected)
            {
                var sut = new GreaterThanOrEqualSpecification<int?>(greaterThan);

                var overall = sut.IsNotSatisfiedBy(candidate, out var result);

                Assert.False(overall);
                Assert.Equal(expected, result, new SpecificationResultComparer());
            }
        }
    }
}