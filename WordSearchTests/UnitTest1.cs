using System;
using Xunit;

public class WordSearchTests
{
    [Fact]
    public void TestWordFoundInGrid()
    {
        // Arrange
        char[,] grid = new char[,]
        {
            {'C', 'A', 'T'},
            {'X', 'X', 'X'},
            {'Y', 'Z', 'Z'}
        };
        string word = "CAT";
        int N = 3;  // Grid size

        // Act
        bool result = Program.FindAndRemoveWord(grid, word, N);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestWordNotFoundInGrid()
    {
        // Arrange
        char[,] grid = new char[,]
        {
            {'C', 'A', 'T'},
            {'X', 'X', 'X'},
            {'Y', 'Z', 'Z'}
        };
        string word = "DOG";
        int N = 3;  // Grid size

        // Act
        bool result = Program.FindAndRemoveWord(grid, word, N);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestPartialWordFoundInGrid()
    {
        // Arrange
        char[,] grid = new char[,]
        {
            {'C', 'A', 'T'},
            {'X', 'X', 'X'},
            {'Y', 'Z', 'Z'}
        };
        string word = "CA";  // Partial word
        int N = 3;

        // Act
        bool result = Program.FindAndRemoveWord(grid, word, N);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestGridUpdateAfterWordRemoved()
    {
        // Arrange
        char[,] grid = new char[,]
        {
            {'C', 'A', 'T'},
            {'X', 'X', 'X'},
            {'Y', 'Z', 'Z'}
        };
        string word = "CAT";
        int N = 3;

        // Act
        bool result = Program.FindAndRemoveWord(grid, word, N);

        // Assert
        Assert.True(result);
        
        // Check if the grid was updated with '*' in place of 'CAT'
        Assert.Equal('*', grid[0, 0]);  // C -> *
        Assert.Equal('*', grid[0, 1]);  // A -> *
        Assert.Equal('*', grid[0, 2]);  // T -> *
    }

    [Fact]
    public void TestMultipleWordsRemovedFromGrid()
    {
        // Arrange
        char[,] grid = new char[,]
        {
            {'C', 'A', 'T'},
            {'D', 'O', 'G'},
            {'M', 'O', 'O'}
        };
        string[] words = { "CAT", "DOG" };
        int N = 3;

        // Act
        foreach (var word in words)
        {
            Program.FindAndRemoveWord(grid, word, N);
        }

        // Assert
        // Ensure CAT and DOG have been removed
        Assert.Equal('*', grid[0, 0]);
        Assert.Equal('*', grid[0, 1]);
        Assert.Equal('*', grid[0, 2]);

        Assert.Equal('*', grid[1, 0]);
        Assert.Equal('*', grid[1, 1]);
        Assert.Equal('*', grid[1, 2]);

        // Ensure the rest of the grid is untouched
        Assert.Equal('M', grid[2, 0]);
        Assert.Equal('O', grid[2, 1]);
        Assert.Equal('O', grid[2, 2]);
    }
}
