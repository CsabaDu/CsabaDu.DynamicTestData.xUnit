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
                        $"'{MemberName}' member of " +
                        $"'{MemberType.Name}' " +
                        "yielded an item that is not an 'object[]'"),
                };
            }

            testData = testDataRow.GetTestData();
        }

        return testData.ToParams(
            getArgsCodeFromDataSourceMember(),
            testData is IExpected)!;

        #region Local methods
        ArgsCode getArgsCodeFromDataSourceMember()
        {
            try
            {
                var dataSource = getDataSourceMemberValue(
                    BindingFlags.Static |
                    BindingFlags.Public |
                    BindingFlags.NonPublic);

                return dataSource is IArgsCode dataStrategyBase ?
                    dataStrategyBase.ArgsCode
                    : default;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Failed to retrieve 'ArgsCode' from " +
                    $"{MemberType.Name}.{MemberName}",
                    ex is TargetInvocationException tiex ?
                    tiex.InnerException
                    : ex);
            }
        }

        object getDataSourceMemberValue(BindingFlags flags)
        {
            // Property
            if (MemberType.GetProperty(MemberName, flags) is { } property
                && property.GetValue(null) is object propertyValue)
            {
                return propertyValue;
            }

            // Method
            if (MemberType.GetMethod(MemberName, flags,
                null, Type.EmptyTypes, null) is { } method
                && method.Invoke(null, null) is object methodValue)
            {
                return methodValue;
            }

            // Field
            if (MemberType.GetField(MemberName, flags) is { } field
                && field.GetValue(null) is object fieldValue)
            {
                return fieldValue;
            }

            throw new InvalidOperationException(
                "Static data source member " +
                $"'{MemberName}' not found in " +
                $"{MemberType.Name}");
        }
        #endregion
    }
}
