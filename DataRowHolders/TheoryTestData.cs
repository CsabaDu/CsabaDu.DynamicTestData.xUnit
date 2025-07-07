// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.xUnit.DataRowHolders;

public sealed class TheoryTestData<TTestData>
: TheoryData,
ITheoryTestData,
ITypedTestDataRow<object?[], TTestData>
where TTestData : notnull, ITestData
{
    private TheoryTestData(ArgsCode argsCode)
    {
        DataStrategy = GetStoredDataStrategy(
            argsCode.Defined(nameof(argsCode)),
            typeof(TTestData).IsAssignableTo(typeof(IExpected)));
    }

    public TheoryTestData(
        TTestData testData,
        ArgsCode argsCode)
    : this(argsCode)
    {
        Add(testData);
    }

    public TheoryTestData(
        IEnumerable<ITestData> testDataList,
        ArgsCode argsCode)
    : this(argsCode)
    {
        AddRange(testDataList);
    }

    internal TheoryTestData(
        IEnumerable<ITestDataRow> testDataRows,
        ArgsCode argsCode)
    : this(argsCode)
    {
        AddRange(testDataRows.Select(x => x.GetTestData()));
    }

    public TheoryTestData(
        TheoryTestData<TTestData> other,
        ArgsCode argsCode)
    : this(argsCode)
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

    private readonly List<ITestData> _testDataList = [];

    public IDataStrategy DataStrategy { get; init; }

    public Type TestDataType => typeof(TTestData);

    public void Add(ITestData testData)
    {
        ArgumentNullException.ThrowIfNull(
            testData,
            nameof(testData));

        if (testData is not TTestData)
        {
            throw new ArgumentException(
                "Test data must be of type " +
                $"{typeof(TTestData).Name}.",
                nameof(testData));
        }

        AddRow(testData.ToParams(
            DataStrategy.ArgsCode,
            DataStrategy.WithExpected));

        _testDataList.Add(testData);
    }

    public IDataRowHolder<object?[]> GetDataRowHolder(IDataStrategy dataStrategy)
    {
        var argsCode = dataStrategy.ArgsCode;

        if (argsCode == DataStrategy.ArgsCode) return this;

        return new TheoryTestData<TTestData>(
            this,
            argsCode);
    }

    public IEnumerable<object?[]>? GetRows(ArgsCode? argsCode)
    {
        argsCode ??= DataStrategy.ArgsCode;

        return _testDataList.Select(td => td.ToParams(
            argsCode.Value,
            DataStrategy.WithExpected));
    }

    public IEnumerable<ITestDataRow>? GetTestDataRows()
    => _testDataList.Select(td => CreateTestDataRow((TTestData)td));

    public ITestDataRow<object?[], TTestData> CreateTestDataRow(TTestData testData)
    => new ObjectArrayRow<TTestData>(testData);

    public void AddRange(IEnumerable<ITestData> testDataList)
    {
        Type testDataType = testDataList?.GetType().GetGenericArguments()[0]
            ?? throw new ArgumentNullException(
                nameof(testDataList));

        if (!testDataList.Any())
        {
            throw new ArgumentException(
                "Test data list must not be empty.",
                nameof(testDataList));
        }

        if (_testDataList.Any() && testDataType != TestDataType)
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

    public IDataStrategy GetDataStrategy(ArgsCode? argsCode)
    => DataStrategy.ArgsCode == argsCode ?
        DataStrategy
        : GetStoredDataStrategy(
            argsCode ?? DataStrategy.ArgsCode,
            DataStrategy.WithExpected);
}