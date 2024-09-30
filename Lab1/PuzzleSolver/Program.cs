using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Puzzle Solver!");
        }
    }


    public class Puzzle
    {
        private char[,] grid;
        private bool[,] visited;
        private int N;

        public Puzzle(char[,] grid)
        {
            N = grid.GetLength(0);
            this.grid = grid;
            visited = new bool[N, N];
        }

        public List<char> Solve(List<string> words)
        {
            foreach (var word in words)
            {
                FindWord(word);
            }

            return GetRemainingLetters();
        }

        private void FindWord(string word)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (grid[i, j] == word[0])
                    {
                        if (SearchWord(i, j, word, 0))
                        {
                            return;
                        }
                    }
                }
            }
        }

        private bool SearchWord(int x, int y, string word, int index)
        {
            if (index == word.Length)
                return true;

            if (x < 0 || x >= N || y < 0 || y >= N || visited[x, y] || grid[x, y] != word[index])
                return false;

            visited[x, y] = true;

            bool found = SearchWord(x + 1, y, word, index + 1) ||
                         SearchWord(x - 1, y, word, index + 1) ||
                         SearchWord(x, y + 1, word, index + 1) ||
                         SearchWord(x, y - 1, word, index + 1);

            if (!found)
                visited[x, y] = false; // backtrack

            return found;
        }

        private List<char> GetRemainingLetters()
        {
            List<char> remaining = new List<char>();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (!visited[i, j])
                        remaining.Add(grid[i, j]);
                }
            }

            remaining.Sort();
            return remaining;
        }
    }
}

