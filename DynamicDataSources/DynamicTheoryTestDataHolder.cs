// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.xUnit.ynamicDataSources;

/// <summary>
/// Abstract base class for providing dynamic theory test data sources with type-safe argument handling.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as a foundation for creating strongly-typed test data sources
/// that can be used with xUnit theory tests. It maintains type consistency across
/// all added test data and provides various methods for adding different kinds of
/// test cases (normal, return value, and exception cases).
/// </para>
/// <para>
/// The class ensures all test data added to a single instance maintains consistent
/// generic type parameters through runtime checks.
/// </para>
/// </remarks>
/// <param name="argsCode">The strategy for converting test data to method arguments</param>
public abstract class DynamicTheoryTestDataHolder(ArgsCode argsCode)
: DynamicObjectArraySource(argsCode, typeof(IExpected))
{
    protected override void Add<TTestData>(TTestData testData)
    {
        bool rowCreated = TryGetTestDataRow<object?[], TTestData>(
            testData,
            out ITestDataRow<object?[], TTestData>? testDataRow);

        if (rowCreated && DataRowHolder is TheoryTestData<TTestData> theoryTestData)
        {
            theoryTestData.Add(testDataRow!.TestData);
        }
    }

    public TheoryTestData<TTestData>? GetTheoryTestData<TTestData>(ArgsCode? argsCode)
    where TTestData : notnull, ITestData
    {
        if (DataRowHolder is null)
        {
            return null;
        }

        ArgsCode dataRowHolderArgsCode = DataRowHolder.DataStrategy.ArgsCode;
        argsCode ??= dataRowHolderArgsCode;

        if (DataRowHolder is TheoryTestData<TTestData> theoryTestData)
        {
            return argsCode == dataRowHolderArgsCode ?
                theoryTestData
                : new TheoryTestData<TTestData>(
                    theoryTestData,
                    argsCode.Value);
        }

        if (DataRowHolder is IEnumerable<ITestDataRow> testDataRows
            && testDataRows.All(x => x.GetTestData() is TTestData))
        {
            return new TheoryTestData<TTestData>(
                testDataRows,
                argsCode.Value);
        }

        return null;
    }

    protected override void InitDataRowHolder<TTestData>(TTestData testData)
    => DataRowHolder = new TheoryTestData<TTestData>(
        testData,
        ArgsCode);

    protected override bool TryGetTestDataRow<TDataRow, TTestData>(
        TTestData testData,
        out ITestDataRow<object?[], TTestData>? testDataRow)
    {
        if (DataRowHolder is not ITheoryTestData theoryTestData)
        {
            return base.TryGetTestDataRow<TDataRow, TTestData>(
                testData,
                out testDataRow);
        }

        testDataRow = default;

        if (!Equals(theoryTestData.DataStrategy)
            || theoryTestData.TestDataType != typeof(TTestData))
        {
            WithExpected = testData is IExpected;

            InitDataRowHolder(testData);
            return false;
        }

        testDataRow = CreateTestDataRow(testData);
        return testDataRow != default;
    }
}
