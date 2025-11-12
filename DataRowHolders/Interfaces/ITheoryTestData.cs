// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.xUnit.DataRowHolders.Interfaces;

public interface ITheoryTestData
: IDataRowHolder<object?[]>;

public interface ITheoryTestData<TTestData>
: ITheoryTestData, ITestDataRowFactory<object?[], TTestData>
where TTestData : notnull, ITestData
{
    void AddRange(IEnumerable<TTestData> testDataList);
    void Add(TTestData testData);
}
