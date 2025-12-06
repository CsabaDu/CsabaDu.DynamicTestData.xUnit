// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.xUnit.Attributes;

public abstract class MemberTestDataAttributeBase(
    string memberName, object[]? args)
: MemberDataAttributeBase(
    memberName, args)
{
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
                    object?[] args => args as object[],
                    _ => throw new ArgumentException(
                        $"'{MemberName}' member of '{MemberType.Name}' " +
                        "yielded an item that is not an 'object[]' " +
                        "or an 'ITestData' or 'ITestDataRow' instance"),
                };
            }

            testData = testDataRow.GetTestData();
        }

        return testData.ToParams(
            ArgsCode.Instance,
            PropsCode.Expected)!;
    }
}

public sealed class MemberTestDataAttribute(
    string memberName,
    params object[]? args)
: MemberTestDataAttributeBase(
    memberName,
    args);