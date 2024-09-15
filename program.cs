using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Directions for moving in the grid (up, down, left, right)
    static readonly int[] dx = { -1, 1, 0, 0 };
    static readonly int[] dy = { 0, 0, -1, 1 };

    static void Main(string[] args)
    {
        // Step 1: Read input from the console
        Console.WriteLine("Enter the grid size (N) and number of words (M) separated by space:");
        string[] firstLine = Console.ReadLine().Split(' ');
        
        int N = int.Parse(firstLine[0]);  // Grid size (N x N)
        int M = int.Parse(firstLine[1]);  // Number of words

        Console.WriteLine("User input: N={0} M={1}", N, M);

        char[,] grid = new char[N, N];

        // Read/add new row to the grid each itteration 
        Console.WriteLine("Enter the grid (one line at a time):");
        for (int i = 0; i < N; i++)
        {
            string line = Console.ReadLine();
            for (int j = 0; j < N; j++)
            {
                grid[i, j] = line[j];
            }
        }

        // Get list of words from user the list of words
        List<string> words = new List<string>();
        Console.WriteLine("Enter the list of words (one word per line):");
        for (int i = 0; i < M; i++)
        {
            words.Add(Console.ReadLine());
        }

        // Process each word and remove it from the grid
        foreach (string word in words)
        {
            Console.WriteLine($"Searching for the word: {word}");
            bool found = FindAndRemoveWord(grid, word, N);
            if (found)
            {
                Console.WriteLine($"Word '{word}' found and removed from the grid.");
            }
            else
            {
                Console.WriteLine($"Word '{word}' not found in the grid.");
            }
        }

        // Collect remaining letters in the grid
        List<char> remainingLetters = new List<char>();

        Console.WriteLine("Remaining grid after removing all words:");
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                Console.Write(grid[i, j] + " "); // Print the updated grid
                if (grid[i, j] != '*')  // '*' marks a used letter
                {
                    remainingLetters.Add(grid[i, j]);
                }
            }
            Console.WriteLine();  // Newline after each row of the grid
        }

        // Sort and output the remaining letters in alphabetical order
        remainingLetters.Sort();
        Console.WriteLine("Remaining letters in alphabetical order:");
        Console.WriteLine(string.Join("", remainingLetters));
    }

    // Function to perform DepthFirstSearch and find the word in the grid
    static bool DFS(char[,] grid, string word, int x, int y, int index, int N)
    {
        if (index == word.Length)  // If we've found the entire word
        {
            return true;
        }

        // If out of bounds or the current cell doesn't match the letter
        if (x < 0 || x >= N || y < 0 || y >= N || grid[x, y] != word[index])
        {
            return false;
        }

        // Mark the current cell as used
        char temp = grid[x, y];
        grid[x, y] = '*';

        Console.WriteLine($"DFS at ({x}, {y}) - matched letter '{temp}' for word '{word}'");

        // Explore in all four directions
        for (int i = 0; i < 4; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];

            if (DFS(grid, word, newX, newY, index + 1, N))
            {
                return true;
            }
        }

        // Backtrack (restore the cell's original value)
        grid[x, y] = temp;
        Console.WriteLine($"Backtracking at ({x}, {y}), restoring letter '{temp}'");
        return false;
    }

    // Function to find and remove a word from the grid
    static bool FindAndRemoveWord(char[,] grid, string word, int N)
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (DFS(grid, word, i, j, 0, N))
                {
                    return true;  // Word found and removed, no need to search further
                }
            }
        }
        return false;  // Word not found
    }
}

