﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentSpecification.Abstractions.Validation;
using JetBrains.Annotations;

namespace FluentSpecification.Tests.Sdk.Data
{
    public class SpecificationDataRow : ISpecificationResultCreator
    {
        private readonly object[] _data;

        private readonly List<FailedSpecificationDataRow> _failedSpecificationDataRows
            = new List<FailedSpecificationDataRow>();

        private readonly bool _overall;
        private SpecificationResult _dataResult;
        private SpecificationResult _negationDataResult;

        public SpecificationDataRow(bool overall, object[] data)
        {
            _overall = overall;
            _data = data ?? new object[0];
        }

        FailedSpecificationDataRow ISpecificationResultCreator.FailedSpecification(Type specificationType,
            params string[] errors)
        {
            var row = new FailedSpecificationDataRow(specificationType, errors);
            _failedSpecificationDataRows.Add(row);

            return row;
        }

        [PublicAPI]
        public SpecificationDataRow Result(int totalSpecificationsCount, string trace,
            Action<ISpecificationResultCreator> creator = null)
        {
            _failedSpecificationDataRows.Clear();
            creator?.Invoke(this);

            _dataResult = new SpecificationResult(totalSpecificationsCount, _overall, trace,
                _failedSpecificationDataRows.Select(dr => dr.CreateFailedSpecification()).ToArray());
            return this;
        }

        public SpecificationDataRow Result(string trace, Action<ISpecificationResultCreator> creator = null)
        {
            Result(1, trace, creator);
            return this;
        }

        public SpecificationDataRow Result(SpecificationResult result)
        {
            _dataResult = result;
            return this;
        }

        [PublicAPI]
        public SpecificationDataRow NegationResult(int totalSpecificationsCount, string trace,
            Action<ISpecificationResultCreator> creator = null)
        {
            _failedSpecificationDataRows.Clear();
            creator?.Invoke(this);

            _negationDataResult = new SpecificationResult(totalSpecificationsCount, !_overall, trace,
                _failedSpecificationDataRows.Select(dr => dr.CreateFailedSpecification()).ToArray());
            return this;
        }

        public SpecificationDataRow NegationResult(string trace, Action<ISpecificationResultCreator> creator = null)
        {
            NegationResult(1, trace, creator);
            return this;
        }

        public SpecificationDataRow NegationResult(SpecificationResult result)
        {
            _negationDataResult = result;
            return this;
        }

        public object[] GetData(bool withResult = false, bool withNegationResult = false)
        {
            var result = _data;
            if (withResult)
                result = result.Concat(Enumerable.Repeat<object>(_dataResult, 1)).ToArray();
            if (withNegationResult)
                result = result.Concat(Enumerable.Repeat<object>(_negationDataResult, 1)).ToArray();

            return result;
        }
    }
}