namespace Coding.Karat;

using System;
using System.Collections.Generic;
using Xunit;

public class NonogramValidatorTests
{
    [Fact]
    public void Test_ValidNonogram()
    {
        int[][] matrix = {
            new int[] {1, 1, 1, 1},
            new int[] {0, 1, 1, 1},
            new int[] {0, 1, 0, 0},
            new int[] {1, 1, 0, 1},
            new int[] {0, 0, 1, 1}
        };

        List<List<int>> rowConstraints = new List<List<int>> {
            new List<int>(),
            new List<int> {1},
            new List<int> {1, 2},
            new List<int> {1},
            new List<int> {2}
        };

        List<List<int>> colConstraints = new List<List<int>> {
            new List<int> {2, 1},
            new List<int> {1},
            new List<int> {2},
            new List<int> {1}
        };

        bool result = NonogramValidatorCode.IsValidNonogram(matrix, rowConstraints, colConstraints);
        Assert.True(result);
    }

    [Fact]
    public void Test_InvalidNonogram_RowMismatch()
    {
        int[][] matrix = {
            new int[] {1, 1, 1, 1},
            new int[] {0, 1, 1, 1},
            new int[] {0, 1, 0, 0},
            new int[] {1, 1, 0, 1},
            new int[] {0, 0, 1, 1}
        };

        List<List<int>> rowConstraints = new List<List<int>> {
            new List<int>(),
            new List<int> {1},
            new List<int> {1, 1}, // 与实际矩阵不符
            new List<int> {1},
            new List<int> {2}
        };

        List<List<int>> colConstraints = new List<List<int>> {
            new List<int> {2, 1},
            new List<int> {1},
            new List<int> {2},
            new List<int> {1}
        };

        bool result = NonogramValidatorCode.IsValidNonogram(matrix, rowConstraints, colConstraints);
        Assert.False(result);
    }

    [Fact]
    public void Test_InvalidNonogram_ColumnMismatch()
    {
        int[][] matrix = {
            new int[] {1, 1},
            new int[] {0, 0},
            new int[] {0, 0},
            new int[] {1, 0}
        };

        List<List<int>> rowConstraints = new List<List<int>> {
            new List<int>(),
            new List<int> {2},
            new List<int> {2},
            new List<int> {1}
        };

        List<List<int>> colConstraints = new List<List<int>> {
            new List<int> {1, 1}, // 第一列的黑色块不符合约束
            new List<int> {3}
        };

        bool result = NonogramValidatorCode.IsValidNonogram(matrix, rowConstraints, colConstraints);
        Assert.False(result);
    }

    [Fact]
    public void Test_IsValid_Works()
    {
        var data = new List<int>
        {
            0, 1, 0, 0
        };
        var pattern = new List<int>
        {
            1, 2
        };
        bool result = NonogramValidatorCode.IsValid(data, pattern);
        Assert.True(result);
    }
}
