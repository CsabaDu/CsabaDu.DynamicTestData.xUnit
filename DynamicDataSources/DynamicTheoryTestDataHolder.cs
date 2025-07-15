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
        bool rowCreated = TryCreateTestDataRow<object?[], TTestData>(
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

        argsCode ??= ArgsCode;

        if (DataRowHolder is TheoryTestData<TTestData> theoryTestData)
        {
            return argsCode == ArgsCode ?
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

    protected override bool TryCreateTestDataRow<TDataRow, TTestData>(
        TTestData testData,
        out ITestDataRow<object?[], TTestData>? testDataRow)
    {
        if (DataRowHolder is not ITheoryTestData)
        {
            return base.TryCreateTestDataRow<TDataRow, TTestData>(
                testData,
                out testDataRow);
        }

        bool isValidDataRowHolder =
            Equals(DataRowHolder.DataStrategy) &&
            GetTestDataType() == typeof(TTestData);

        bool? withExpected = testData is IExpected;

        return TryCreateTestDataRow(
            testData,
            isValidDataRowHolder,
            withExpected,
            out testDataRow);
    }
}
