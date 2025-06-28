// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.xUnit.Attributes;

public abstract class MemberTestDataAttributeBase(
    string memberName, object[]? args)
: MemberDataAttributeBase(
    memberName, args),
    IArgsCode
{
    public ArgsCode ArgsCode { get; set; } =
        ArgsCode.Properties;

    protected override object[] ConvertDataItem(
        MethodInfo testMethod,
        object item)
    {
        if (item is not ITestData testData)
        {
            if (item is not ITestDataRow testDataRow)
            {
                return item switch
                {
                    null => null!,
                    object[] args => args,
                    _ => throw new ArgumentException(
                        $"'{MemberName}' member of '{testMethod.DeclaringType}' " +
                        "yielded an item that is not an 'object[]'"),
                };
            }

            testData = testDataRow.GetTestData();
        }

        return testData.ToParams(
            ArgsCode,
            testData is IExpected)!;
    }
}
