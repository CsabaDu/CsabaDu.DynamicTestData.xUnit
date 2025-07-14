// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.xUnit.DataRowHolders.Interfaces;

public interface ITheoryTestData
: IDataRowHolder<object?[]>
{
    void AddRange(IEnumerable<ITestData> testDataList);
}

public interface ITheoryTestData<TTestData>
: ITheoryTestData, ITestDataRowFactory<object?[], TTestData>
where TTestData : notnull, ITestData
{
    void Add(TTestData testData);
}
