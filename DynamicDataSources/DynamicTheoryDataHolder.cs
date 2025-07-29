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
    PropertyCode.Expected)
{
    #region Add
    protected override void Add<TTestData>(TTestData testData)
    {
        if (DataHolder is TheoryData<TTestData> theoryData)
        {
            theoryData.Add(testData);
            return;
        }

        InitDataHolder(testData);
    }

    //protected override void Add<TResult, T1>(
    //    ITestData<TResult, T1> testData)
    //{
    //    base.Add(testData);

    //    if (DataHolder is TheoryData<TResult, T1?> expectedTheoryData)
    //    {
    //        expectedTheoryData.Add(
    //            testData.Expected,
    //            testData.Arg1);
    //        return;
    //    }

    //    if (DataHolder is TheoryData<T1?> theoryData)
    //    {
    //        theoryData.Add(testData.Arg1);
    //        return;
    //    }

    //    InitDataHolder(testData);
    //}

    //protected override void Add<TResult, T1, T2>(
    //    ITestData<TResult, T1, T2> testData)
    //{
    //    base.Add(testData);

    //    if (DataHolder is TheoryData<TResult, T1?, T2?> expectedTheoryData)
    //    {
    //        expectedTheoryData.Add(
    //            testData.Expected,
    //            testData.Arg1,
    //            testData.Arg2);
    //        return;
    //    }

    //    if (DataHolder is TheoryData<T1?, T2?> generalTheoryData)
    //    {
    //        generalTheoryData.Add(
    //            testData.Arg1,
    //            testData.Arg2);
    //        return;
    //    }

    //    InitDataHolder(testData);
    //}

    //protected override void Add<TResult, T1, T2, T3>(
    //    ITestData<TResult, T1, T2, T3> testData)
    //{
    //    base.Add(testData);

    //    if (DataHolder is TheoryData<TResult, T1?, T2?, T3?> expectedTheoryData)
    //    {
    //        expectedTheoryData.Add(
    //            testData.Expected,
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3);
    //        return;
    //    }

    //    if (DataHolder is TheoryData<T1?, T2?, T3?> generalTheoryData)
    //    {
    //        generalTheoryData.Add(
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3);
    //        return;
    //    }

    //    InitDataHolder(testData);
    //}

    //protected override void Add<TResult, T1, T2, T3, T4>(
    //    ITestData<TResult, T1, T2, T3, T4> testData)
    //{
    //    base.Add(testData);

    //    if (DataHolder is TheoryData<TResult, T1?, T2?, T3?, T4?> expectedTheoryData)
    //    {
    //        expectedTheoryData.Add(
    //            testData.Expected,
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4);
    //        return;
    //    }

    //    if (DataHolder is TheoryData<T1?, T2?, T3?, T4?> generalTheoryData)
    //    {
    //        generalTheoryData.Add(
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4);
    //        return;
    //    }

    //    InitDataHolder(testData);
    //}

    //protected override void Add<TResult, T1, T2, T3, T4, T5>(
    //    ITestData<TResult, T1, T2, T3, T4, T5> testData)
    //{
    //    base.Add(testData);

    //    if (DataHolder is TheoryData<TResult, T1?, T2?, T3?, T4?, T5?> expectedTheoryData)
    //    {
    //        expectedTheoryData.Add(
    //            testData.Expected,
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5);
    //        return;
    //    }

    //    else if (DataHolder is TheoryData<T1?, T2?, T3?, T4?, T5?> generalTheoryData)
    //    {
    //        generalTheoryData.Add(
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5);
    //        return;
    //    }

    //    InitDataHolder(testData);
    //}

    //protected override void Add<TResult, T1, T2, T3, T4, T5, T6>(
    //    ITestData<TResult, T1, T2, T3, T4, T5, T6> testData)
    //{
    //    base.Add(testData);

    //    if (DataHolder is TheoryData<TResult, T1?, T2?, T3?, T4?, T5?, T6?> expectedTheoryData)
    //    {
    //        expectedTheoryData.Add(
    //            testData.Expected,
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5,
    //            testData.Arg6);
    //        return;
    //    }

    //    if (DataHolder is TheoryData<T1?, T2?, T3?, T4?, T5?, T6?> generalTheoryData)
    //    {
    //        generalTheoryData.Add(
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5,
    //            testData.Arg6);
    //        return;
    //    }

    //    InitDataHolder(testData);
    //}

    //protected override void Add<TResult, T1, T2, T3, T4, T5, T6, T7>(
    //    ITestData<TResult, T1, T2, T3, T4, T5, T6, T7> testData)
    //{
    //    base.Add(testData);

    //    if (DataHolder is TheoryData<TResult, T1?, T2?, T3?, T4?, T5?, T6?, T7?> expectedTheoryData)
    //    {
    //        expectedTheoryData.Add(
    //            testData.Expected,
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5,
    //            testData.Arg6,
    //            testData.Arg7);
    //        return;
    //    }

    //    else if (DataHolder is TheoryData<T1?, T2?, T3?, T4?, T5?, T6?, T7?> generalTheoryData)
    //    {
    //        generalTheoryData.Add(
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5,
    //            testData.Arg6,
    //            testData.Arg7);
    //        return;
    //    }

    //    InitDataHolder(testData);
    //}

    //protected override void Add<TResult, T1, T2, T3, T4, T5, T6, T7, T8>(
    //    ITestData<TResult, T1, T2, T3, T4, T5, T6, T7, T8> testData)
    //{
    //    base.Add(testData);

    //    if (DataHolder is TheoryData<TResult, T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?> expectedTheoryData)
    //    {
    //        expectedTheoryData.Add(
    //            testData.Expected,
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5,
    //            testData.Arg6,
    //            testData.Arg7,
    //            testData.Arg8);
    //        return;
    //    }

    //    if (DataHolder is TheoryData<T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?> generalTheoryData)
    //    {
    //        generalTheoryData.Add(
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5,
    //            testData.Arg6,
    //            testData.Arg7,
    //            testData.Arg8);
    //        return;
    //    }

    //    InitDataHolder(testData);
    //}

    //protected override void Add<TResult, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    //    ITestData<TResult, T1, T2, T3, T4, T5, T6, T7, T8, T9> testData)
    //{
    //    base.Add(testData);

    //    if (DataHolder is TheoryData<TResult, T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?> expectedTheoryData)
    //    {
    //        expectedTheoryData.Add(
    //            testData.Expected,
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5,
    //            testData.Arg6,
    //            testData.Arg7,
    //            testData.Arg8,
    //            testData.Arg9);
    //        return;
    //    }

    //    else if (DataHolder is TheoryData<T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?> generalTheoryData)
    //    {
    //        generalTheoryData.Add(
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5,
    //            testData.Arg6,
    //            testData.Arg7,
    //            testData.Arg8,
    //            testData.Arg9);
    //        return;
    //    }

    //    InitDataHolder(testData);
    //}
    #endregion

    #region InitDataHolder
    protected override void InitDataHolder<TTestData>(TTestData testData)
    {
        DataHolder = new TheoryData<TTestData>(testData);
        TestDataType = typeof(TTestData);
    }

    //protected override void InitDataHolder<TResult, T1>(
    //    ITestData<TResult, T1> testData)
    //{
    //    base.InitDataHolder(testData);

    //    if (testData is IExpected)
    //    {
    //        DataHolder = new TheoryData<TResult, T1?>()
    //        {
    //            {
    //                testData.Expected,
    //                testData.Arg1
    //            }
    //        };
    //        return;
    //    }

    //    DataHolder =
    //        new TheoryData<T1?>(testData.Arg1);
    //}

    //protected override void InitDataHolder<TResult, T1, T2>(
    //    ITestData<TResult, T1, T2> testData)
    //{
    //    base.InitDataHolder(testData);

    //    if (testData is IExpected)
    //    {
    //        DataHolder = new TheoryData<TResult, T1?, T2?>()
    //        {
    //            {
    //                testData.Expected,
    //                testData.Arg1,
    //                testData.Arg2
    //            }
    //        };
    //        return;
    //    }

    //    DataHolder = new TheoryData<T1?, T2?>()
    //    {
    //        {
    //            testData.Arg1,
    //            testData.Arg2
    //        }
    //    };
    //}


    //protected override void InitDataHolder<TResult, T1, T2, T3>(
    //     ITestData<TResult, T1, T2, T3> testData)
    //{
    //    base.InitDataHolder(testData);

    //    if (testData is IExpected)
    //    {
    //        DataHolder = new TheoryData<TResult, T1?, T2?, T3?>()
    //        {
    //            {
    //                testData.Expected,
    //                testData.Arg1,
    //                testData.Arg2,
    //                testData.Arg3
    //            }
    //        };
    //        return;
    //    }

    //    DataHolder = new TheoryData<T1?, T2?, T3?>()
    //    {
    //        {
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3
    //        }
    //    };
    //}

    //protected override void InitDataHolder<TResult, T1, T2, T3, T4>(
    //     ITestData<TResult, T1, T2, T3, T4> testData)
    //{
    //    base.InitDataHolder(testData);

    //    if (testData is IExpected)
    //    {
    //        DataHolder = new TheoryData<TResult, T1?, T2?, T3?, T4?>()
    //        {
    //            {
    //                testData.Expected,
    //                testData.Arg1,
    //                testData.Arg2,
    //                testData.Arg3,
    //                testData.Arg4
    //            }
    //        };
    //        return;
    //    }

    //    DataHolder = new TheoryData<T1?, T2?, T3?, T4?>()
    //    {
    //        {
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4
    //        }
    //    };
    //}

    //protected override void InitDataHolder<TResult, T1, T2, T3, T4, T5>(
    //     ITestData<TResult, T1, T2, T3, T4, T5> testData)
    //{
    //    base.InitDataHolder(testData);

    //    if (testData is IExpected)
    //    {
    //        DataHolder = new TheoryData<TResult, T1?, T2?, T3?, T4?, T5?>()
    //        {
    //            {
    //                testData.Expected,
    //                testData.Arg1,
    //                testData.Arg2,
    //                testData.Arg3,
    //                testData.Arg4,
    //                testData.Arg5
    //            }
    //        };
    //        return;
    //    }

    //    DataHolder = new TheoryData<T1?, T2?, T3?, T4?, T5?>()
    //    {
    //        {
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5
    //        }
    //    };
    //}

    //protected override void InitDataHolder<TResult, T1, T2, T3, T4, T5, T6>(
    //     ITestData<TResult, T1, T2, T3, T4, T5, T6> testData)
    //{
    //    base.InitDataHolder(testData);

    //    if (testData is IExpected)
    //    {
    //        DataHolder = new TheoryData<TResult, T1?, T2?, T3?, T4?, T5?, T6?>()
    //        {
    //            {
    //                testData.Expected,
    //                testData.Arg1,
    //                testData.Arg2,
    //                testData.Arg3,
    //                testData.Arg4,
    //                testData.Arg5,
    //                testData.Arg6
    //            }
    //        };
    //        return;
    //    }

    //    DataHolder = new TheoryData<T1?, T2?, T3?, T4?, T5?, T6?>()
    //    {
    //        {
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5,
    //            testData.Arg6
    //        }
    //    };
    //}

    //protected override void InitDataHolder<TResult, T1, T2, T3, T4, T5, T6, T7>(
    //     ITestData<TResult, T1, T2, T3, T4, T5, T6, T7> testData)
    //{
    //    base.InitDataHolder(testData);

    //    if (testData is IExpected)
    //    {
    //        DataHolder = new TheoryData<TResult, T1?, T2?, T3?, T4?, T5?, T6?, T7?>()
    //        {
    //            {
    //                testData.Expected,
    //                testData.Arg1,
    //                testData.Arg2,
    //                testData.Arg3,
    //                testData.Arg4,
    //                testData.Arg5,
    //                testData.Arg6,
    //                testData.Arg7
    //            }
    //        };
    //        return;
    //    }

    //    DataHolder = new TheoryData<T1?, T2?, T3?, T4?, T5?, T6?, T7?>()
    //    {
    //        {
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5,
    //            testData.Arg6,
    //            testData.Arg7
    //        }
    //    };
    //}

    //protected override void InitDataHolder<TResult, T1, T2, T3, T4, T5, T6, T7, T8>(
    //     ITestData<TResult, T1, T2, T3, T4, T5, T6, T7, T8> testData)
    //{
    //    base.InitDataHolder(testData);

    //    if (testData is IExpected)
    //    {
    //        DataHolder = new TheoryData<TResult, T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?>()
    //        {
    //            {
    //                testData.Expected,
    //                testData.Arg1,
    //                testData.Arg2,
    //                testData.Arg3,
    //                testData.Arg4,
    //                testData.Arg5,
    //                testData.Arg6,
    //                testData.Arg7,
    //                testData.Arg8
    //            }
    //        };
    //        return;
    //    }

    //    DataHolder = new TheoryData<T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?>()
    //    {
    //        {
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5,
    //            testData.Arg6,
    //            testData.Arg7,
    //            testData.Arg8
    //        }
    //    };
    //}

    //protected override void InitDataHolder<TResult, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    //     ITestData<TResult, T1, T2, T3, T4, T5, T6, T7, T8, T9> testData)
    //{
    //    base.InitDataHolder(testData);

    //    if (testData is IExpected)
    //    {
    //        DataHolder = new TheoryData<TResult, T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?>()
    //        {
    //            {
    //                testData.Expected,
    //                testData.Arg1,
    //                testData.Arg2,
    //                testData.Arg3,
    //                testData.Arg4,
    //                testData.Arg5,
    //                testData.Arg6,
    //                testData.Arg7,
    //                testData.Arg8,
    //                testData.Arg9
    //            }
    //        };
    //        return;
    //    }

    //    DataHolder = new TheoryData<T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?>()
    //    {
    //        {
    //            testData.Arg1,
    //            testData.Arg2,
    //            testData.Arg3,
    //            testData.Arg4,
    //            testData.Arg5,
    //            testData.Arg6,
    //            testData.Arg7,
    //            testData.Arg8,
    //            testData.Arg9
    //        }
    //    };
    //}
    #endregion
}
