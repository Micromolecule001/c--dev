using Xunit;

public class PuzzleSolverTests
{
    [Fact]
    public void Test_SearchWord_True()
    {
        char[,] grid = {
            {'A', 'B', 'C', 'D'},
            {'E', 'F', 'G', 'H'},
            {'I', 'J', 'K', 'L'},
            {'M', 'N', 'O', 'P'}
        };

        var solver = new PuzzleSolver(grid, 4);
        
        bool result = solver.SearchWord("ABCGKOP", 0, 0, 0);
        
        Assert.True(result);
    }

    [Fact]
    public void Test_FindWord_True()
    {
        char[,] grid = {
            {'A', 'B'},
            {'C', 'D'}
        };

        var solver = new PuzzleSolver(grid, 2);

        bool result = solver.FindWord("BDCA");

        Assert.True(result);
    }

    [Fact]
    public void Test_SearchAndRemove_True()
    {
        char[,] grid = {
            {'A', 'B', 'C', 'D'},
            {'E', 'F', 'G', 'H'},
            {'I', 'J', 'K', 'L'},
            {'M', 'N', 'O', 'P'}
        };

        var solver = new PuzzleSolver(grid, 4);

        bool result = solver.SearchAndRemove("ABCGKOP", 0, 0, 0);
        
        Assert.True(result);
    }

    [Fact]
    public void Test_GetRemainingLetters_True()
    {
        char[,] grid = {
            {'-', '-', '-', '-'},
            {'-', 'F', '-', 'H'},
            {'-', '-', '-', 'L'},
            {'M', 'N', '-', '-'}
        };

        var solver = new PuzzleSolver(grid, 4);

        List<char> result = solver.GetRemainingLetters();
        
        List<char> expected = new List<char> { 'F', 'H', 'L', 'M', 'N' };

        Assert.Equal(expected, result);
    }
}
