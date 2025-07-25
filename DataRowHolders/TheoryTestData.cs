﻿// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.xUnit.DataRowHolders;

public abstract class TheoryTestData(ArgsCode argsCode)
: TheoryData,
ITheoryTestData
{
    protected readonly List<ITestData> testDataList = [];

    public IDataStrategy DataStrategy
    => GetDataStrategy(argsCode.Defined(nameof(argsCode)));

    public abstract Type TestDataType { get; }

    public IDataStrategy GetDataStrategy(ArgsCode? argsCode)
    => GetStoredDataStrategy(
        GetArgsCode(argsCode),
        TestDataType.IsAssignableTo(typeof(IExpected)));

    public IEnumerable<object?[]>? GetRows(ArgsCode? argsCode)
    => testDataList.Select(td => td.ToParams(
        GetArgsCode(argsCode),
        DataStrategy.WithExpected));

    public void AddRange(IEnumerable<ITestData> testDataList)
    {
        ArgumentNullException.ThrowIfNull(testDataList, nameof(testDataList));

        if (!testDataList.Any())
        {
            throw new ArgumentException(
                "Test data list must not be empty.",
                nameof(testDataList));
        }

        if (testDataList.Any(td => td.GetType() != TestDataType))
        {
            throw new ArgumentException(
                $"Test data must be of type {TestDataType.Name}.",
                nameof(testDataList));
        }

        foreach (var testData in testDataList)
        {
            Add(testData);
        }
    }

    protected void Add(ITestData testData)
    {
        AddRow(testData.ToParams(
            DataStrategy.ArgsCode,
            DataStrategy.WithExpected));

        testDataList.Add(testData);
    }

    public abstract IDataRowHolder<object?[]> GetDataRowHolder(IDataStrategy dataStrategy);
    public abstract IEnumerable<ITestDataRow>? GetTestDataRows();

    private ArgsCode GetArgsCode(ArgsCode? argsCode)
    => argsCode.HasValue ?
        argsCode.Value.Defined(nameof(argsCode))
        : DataStrategy.ArgsCode;
}

public sealed class TheoryTestData<TTestData>
: TheoryTestData,
ITheoryTestData<TTestData>
where TTestData : notnull, ITestData
{
    public TheoryTestData(
        TTestData testData,
        ArgsCode argsCode)
    : base(argsCode)
    {
        Add(testData);
    }

    public TheoryTestData(
        IEnumerable<ITestData> testDataList,
        ArgsCode argsCode)
    : base(argsCode)
    {
        AddRange(testDataList);
    }

    internal TheoryTestData(
        IEnumerable<ITestDataRow> testDataRows,
        ArgsCode argsCode)
    : base(argsCode)
    {
        AddRange(testDataRows.Select(x => x.GetTestData()));
    }

    public TheoryTestData(
        TheoryTestData<TTestData> other,
        ArgsCode argsCode)
    : base(argsCode)
    {
        var testDataArrayList = other?.GetRows(ArgsCode.Instance)
            ?? throw new ArgumentNullException(
                nameof(other),
                "Other test data must not be null.");

        if (!testDataArrayList.Any())
        {
            throw new ArgumentException(
                "Test data list must not be empty.",
                nameof(other));
        }

        AddRange(testDataArrayList.Select(x => (ITestData)x[0]!));
    }

    public override Type TestDataType => typeof(TTestData);

    public void Add(TTestData testData)
    => base.Add(testData);

    public override IDataRowHolder<object?[]> GetDataRowHolder(IDataStrategy dataStrategy)
    {
        var argsCode = dataStrategy.ArgsCode;

        return argsCode == DataStrategy.ArgsCode ?
            this
            : new TheoryTestData<TTestData>(
                this,
                argsCode);
    }

    public override IEnumerable<ITestDataRow>? GetTestDataRows()
    => testDataList.Select(td => CreateTestDataRow((TTestData)td));

    public ITestDataRow<object?[], TTestData> CreateTestDataRow(TTestData testData)
    => new ObjectArrayRow<TTestData>(testData);
}