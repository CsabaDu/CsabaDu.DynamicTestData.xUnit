// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.xUnit.DynamicDataSources;

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
public abstract class DynamicTheoryDataHolder()
: DynamicDataSource<TheoryData>(
    ArgsCode.Instance,
    PropsCode.Expected)
{
    #region Add
    protected void Add<TTestData>(TTestData testData)
    where TTestData : notnull, ITestData
    {
        ArgumentNullException.ThrowIfNull(testData, nameof(testData));

        if (DataHolder is not TheoryData<TTestData> theoryData)
        {
            DataHolder = new TheoryData<TTestData>(testData);
            return;
        }

        foreach (var item in theoryData)
        {
            if (testData.Equals(item))
            {
                return;
            }
        }

        theoryData.Add(testData);
    }
    #endregion
}
