using System;
using System.Collections.Generic;

namespace PuzzleSolver
{
    public class Puzzle
    {
        public char[,] _grid;
        public bool[,] _visited;

        // Directions for moving in the grid (up, down, left, right)
        public static readonly (int, int)[] Directions = 
        {
            (-1, 0), // Up
            (1, 0),  // Down
            (0, -1), // Left
            (0, 1)   // Right
        };

        public Puzzle(char[,] grid)
        {
            _grid = grid;
            _visited = new bool[grid.GetLength(0), grid.GetLength(1)];
        }

        public List<char> Solve(List<string> words)
        {
            // Find and mark each word
            foreach (var word in words)
            {
                for (int i = 0; i < _grid.GetLength(0); i++)
                {
                    for (int j = 0; j < _grid.GetLength(1); j++)
                    {
                        if (FindWord(i, j, word, 0))
                        {
                            MarkWord(i, j, word);
                        }
                    }
                }
            }

            // Collect remaining letters
            var remainingLetters = new HashSet<char>();
            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.GetLength(1); j++)
                {
                    if (!_visited[i, j])
                    {
                        remainingLetters.Add(_grid[i, j]);
                    }
                }
            }

            var remainingList = new List<char>(remainingLetters);
            remainingList.Sort();
            return remainingList;
        }

        public bool FindWord(int x, int y, string word, int index)
        {
            if (index == word.Length)
            {
                return true; // Found the word
            }

            if (x < 0 || x >= _grid.GetLength(0) || y < 0 || y >= _grid.GetLength(1) || 
                _visited[x, y] || _grid[x, y] != word[index])
            {
                return false; // Out of bounds or already visited or letter mismatch
            }

            // Mark the cell as visited
            _visited[x, y] = true;

            // Check all four directions
            foreach (var (dx, dy) in Directions)
            {
                if (FindWord(x + dx, y + dy, word, index + 1))
                {
                    return true;
                }
            }

            // Unmark the cell (backtrack)
            _visited[x, y] = false;
            return false;
        }

        public void MarkWord(int x, int y, string word)
        {
            // Mark the word in the grid
            for (int i = 0; i < word.Length; i++)
            {
                if (x >= 0 && x < _grid.GetLength(0) && y >= 0 && y < _grid.GetLength(1) &&
                    _grid[x, y] == word[i])
                {
                    _visited[x, y] = true;
                }
            }
        }
    }
}
