// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.xUnit.DataRowHolders;

public abstract class TheoryTestData(ArgsCode argsCode)
: TheoryData,
IDataRowHolder<object?[]>
{
    public IDataStrategy DataStrategy
    => GetDataStrategy(
        argsCode.Defined(nameof(argsCode)),
        PropsCode.Expected);

    public IDataStrategy GetDataStrategy(ArgsCode? argsCode)
    => GetStoredDataStrategy(
        argsCode,
        DataStrategy);

    public IDataStrategy GetDataStrategy(ArgsCode? argsCode, PropsCode? propsCode)
    => GetStoredDataStrategy(
        argsCode ?? DataStrategy.ArgsCode,
        propsCode ?? DataStrategy.PropsCode);

    public abstract IEnumerable<ITestDataRow>? GetTestDataRows();
    public abstract IDataRowHolder<object?[]> GetDataRowHolder(IDataStrategy dataStrategy);
    public abstract IEnumerable<object?[]>? GetRows(ArgsCode? argsCode);
    public abstract IEnumerable<object?[]>? GetRows(ArgsCode? argsCode, PropsCode? propsCode);
}

public sealed class TheoryTestData<TTestData>
: TheoryTestData,
 ITestDataRowFactory<object?[], TTestData>
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

        AddRange(testDataArrayList.Select(x => (TTestData)x[0]!));
    }

    private readonly List<TTestData> testDataList = [];

    public override IEnumerable<object?[]>? GetRows(ArgsCode? argsCode)
    => testDataList.Select(td => td.ToParams(
        argsCode ?? DataStrategy.ArgsCode,
        DataStrategy.PropsCode));

    public void AddRange(IEnumerable<TTestData> testDataList)
    {
        ArgumentNullException.ThrowIfNull(testDataList, nameof(testDataList));

        if (!testDataList.Any())
        {
            throw new ArgumentException(
                "Test data list must not be empty.",
                nameof(testDataList));
        }

        foreach (var testData in testDataList)
        {
            Add(testData);
        }
    }

    public void Add(TTestData testData)
    {
        AddRow(testData.ToParams(
            DataStrategy.ArgsCode,
            DataStrategy.PropsCode));

        testDataList.Add(testData);
    }


    public override IEnumerable<object?[]>? GetRows(ArgsCode? argsCode, PropsCode? propsCode)
    => testDataList.Select(td => td.ToParams(
        argsCode ?? DataStrategy.ArgsCode,
        propsCode ?? DataStrategy.PropsCode));


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
