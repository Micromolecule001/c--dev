using PuzzleSolver;

namespace PuzzleSolver.Tests;
{
    public class PuzzleSolverTests
    {
        [Fact]
        public void TestFindWord()
        {
            char[,] grid = {
                { 'p', 'r', 'o', 'b', 'l' },
                { 'e', 'm', 'x', 'x', 'x' },
                { 'x', 'x', 'x', 'x', 'x' },
                { 'x', 'x', 'x', 'x', 'x' },
                { 'x', 'x', 'x', 'x', 'x' }
            };

            var solver = new PuzzleSolver.PuzzleSolver(grid, 5);  // Use fully qualified class name
            bool result = solver.FindWord("problem");

            Assert.True(result);
        }

        [Fact]
        public void TestRemoveWord()
        {
            char[,] grid = {
                { 'p', 'r', 'o', 'b', 'l' },
                { 'e', 'm', 'x', 'x', 'x' },
                { 'x', 'x', 'x', 'x', 'x' },
                { 'x', 'x', 'x', 'x', 'x' }
                { 'x', 'x', 'x', 'x', 'x' }
            };

            var solver = new PuzzleSolver.PuzzleSolver(grid, 5);
            solver.RemoveWord("problem");
            var remaining = solver.GetRemainingLetters();

            Assert.DoesNotContain('p', remaining);
        }
    
        [Fact]
        public void TestGetRemainingLetters()
        {
            char[,] grid = {
                { 'p', 'r', 'o', 'b', 'l' },
                { 'e', 'm', 'x', 'x', 'x' },
                { 'x', 'x', 'x', 'x', 'x' },
                { 'x', 'x', 'x', 'x', 'x' },
                { 'x', 'x', 'x', 'x', 'x' }
            };

            var solver = new PuzzleSolver.PuzzleSolver(grid, 5);
            solver.RemoveWord("problem");
            List<char> remainingLetters = solver.GetRemainingLetters();

            var expectedRemaining = new List<char> { 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x' };

            Assert.Equal(expectedRemaining, remainingLetters);
        }

        [Fact]
        public void TestPrintGrid()
        {
            char[,] grid = {
                { 'p', 'r', 'o', 'b', 'l' },
                { 'e', 'm', 'x', 'x', 'x' },
                { 'x', 'x', 'x', 'x', 'x' },
                { 'x', 'x', 'x', 'x', 'x' },
                { 'x', 'x', 'x', 'x', 'x' }
            };

            var solver = new PuzzleSolver.PuzzleSolver(grid, 5);
            solver.PrintGrid();
        }
    }
}
