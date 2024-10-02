using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class PuzzleSolver // Change class to public
{
    public static int[] dx = { -1, 1, 0, 0 }; // Vertical directions (up, down)
    public static int[] dy = { 0, 0, -1, 1 }; // Horizontal directions (left, right)
    public char[,] grid;
    public int N;
    public bool[,] used;

    public PuzzleSolver(char[,] grid, int size)
    {
        this.grid = grid;
        this.N = size;
        this.used = new bool[size, size];
    }

    // Method to check if a word exists in the grid
    public bool FindWord(string word) // Ensure methods are public
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (grid[i, j] == word[0])
                {
                    if (SearchWord(word, i, j, 0)) return true;
                }
            }
        }
        return false;
    }

    public bool SearchWord(string word, int x, int y, int index)
    {
        if (index == word.Length) return true;
        if (x < 0 || x >= N || y < 0 || y >= N || used[x, y] || grid[x, y] != word[index]) return false;

        used[x, y] = true;

        for (int dir = 0; dir < 4; dir++)
        {
            int newX = x + dx[dir];
            int newY = y + dy[dir];
            if (SearchWord(word, newX, newY, index + 1)) return true;
        }

        used[x, y] = false;
        return false;
    }

    public void RemoveWord(string word)
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (grid[i, j] == word[0])
                {
                    // Attempt to search and remove the word
                    if (SearchAndRemove(word, i, j, 0))
                        return; // If found, exit
                }
            }
        }
    }

    public bool SearchAndRemove(string word, int x, int y, int index)
    {
        // If the whole word is found
        if (index == word.Length)
            return true;

        // Check boundaries and if the letter matches
        if (x < 0 || x >= N || y < 0 || y >= N || grid[x, y] != word[index])
            return false;

        char temp = grid[x, y]; // Store the current character
        grid[x, y] = '-'; // Mark as removed

        // Check in all directions (up, down, left, right)
        bool found = SearchAndRemove(word, x + 1, y, index + 1) || // down
                     SearchAndRemove(word, x - 1, y, index + 1) || // up
                     SearchAndRemove(word, x, y + 1, index + 1) || // right
                     SearchAndRemove(word, x, y - 1, index + 1);   // left

        // Restore the character if the word was not found in this path
        if (!found)
        {
            grid[x, y] = temp;
        }
        
        return found;
    }

    public List<char> GetRemainingLetters()
    {
        PrintGrid();
        List<char> remaining = new List<char>();
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (grid[i, j] != '-') // assuming '-' is used to mark removed letters
                {
                    remaining.Add(grid[i, j]);
                }
            }
        }
        PrintGrid();
        return remaining;
    }

    public void PrintGrid()
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                Console.Write(grid[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines("INPUT.TXT");
        var gridSize = input[0].Split();
        int N = int.Parse(gridSize[0]);
        int M = int.Parse(gridSize[1]);

        char[,] grid = new char[N, N];
        for (int i = 0; i < N; i++)
        {
            grid[i, i] = input[i + 1][i];
        }

        var solver = new PuzzleSolver(grid, N);

        for (int i = 0; i < M; i++)
        {
            solver.RemoveWord(input[N + 1 + i]);
        }

        var result = solver.GetRemainingLetters();
        File.WriteAllLines("OUTPUT.TXT", new string[] { string.Join("", result) });
    }
}
