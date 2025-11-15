// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.xUnit.DynamicDataRowSources;

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
public abstract class DynamicTheoryTestDataSource(ArgsCode argsCode)
: DynamicExpectedObjectArrayRowSource(argsCode)
{
    protected override void Add<TTestData>(TTestData testData)
    {
        var theoryTestData = DataHolder as TheoryTestData<TTestData>;
        var testDataRows = theoryTestData?.GetTestDataRows();

        Add(theoryTestData is not null,
            testData,
            testDataRows!,
            theoryTestData!.Add);
    }

    public TheoryTestData<TTestData>? GetTheoryTestData<TTestData>(ArgsCode? argsCode)
    where TTestData : notnull, ITestData
    {
        if (DataHolder is not TheoryTestData<TTestData> theoryTestData)
        {
            return null;
        }

        argsCode ??= ArgsCode;

        return argsCode == ArgsCode ?
            theoryTestData
            : new TheoryTestData<TTestData>(
                theoryTestData,
                argsCode.Value);
    }

    protected override void InitDataHolder<TTestData>(TTestData testData)
    => DataHolder = new TheoryTestData<TTestData>(
        testData,
        ArgsCode);
}
