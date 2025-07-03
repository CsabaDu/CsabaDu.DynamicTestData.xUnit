// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.xUnit.DataRowHolders.Interfaces;

public interface ITheoryTestData
: IDataRowHolder<object?[]>
{
    void Add(ITestData testData);

    void AddRange(IEnumerable<ITestData> testDataList);
}
