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
            getArgsCode(),
            testData is IExpected)!;

        #region Local methods
        ArgsCode getArgsCode()
        {
            try
            {
                var testMethodType = (MemberType?.DeclaringType)
                    ?? throw new InvalidOperationException(
                        "Test method type is null");

                object memberValue = getMemberValue(
                    testMethodType,
                    BindingFlags.Static |
                    BindingFlags.Public |
                    BindingFlags.NonPublic)
                    ?? throw new InvalidOperationException(
                        "static data source member is not found " +
                        $"in the test method {testMethodType.Name}");

                var argCodeProperty = MemberType?.GetProperty("ArgsCode");

                if (argCodeProperty?.GetValue(memberValue) is ArgsCode argsCode)
                {
                    return argsCode;
                }

                throw new InvalidOperationException(
                    "'ArgsCode' property is not found in the member " +
                    $"{MemberType?.Name}.{MemberName}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Failed to retrieve ArgsCode from " +
                    $"{MemberType?.Name}.{MemberName}",
                    ex is TargetInvocationException tiex ?
                        tiex.InnerException
                        : ex);
            }
        }

        object? getMemberValue(Type testMethodType, BindingFlags flags)
        {
            if (testMethodType.GetProperty(MemberName, flags)
                is { PropertyType: var propertyType } propertyInfo
                && propertyType == MemberType)
            {
                return propertyInfo.GetValue(null);
            }

            if (testMethodType.GetMethod(MemberName, flags,
                null,
                Type.EmptyTypes,
                null)
                is { ReturnType: var returnType } methodInfo
                && returnType == MemberType)
            {
                return methodInfo.Invoke(null, null);
            }

            if (testMethodType.GetField(MemberName, flags)
                is { FieldType: var fieldType } fieldInfo
                && fieldType == MemberType)
            {
                return fieldInfo.GetValue(null);
            }

            return null;
        }
        #endregion
    }
}
