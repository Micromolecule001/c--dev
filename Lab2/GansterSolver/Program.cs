using System;
using System.IO;
using System.Linq;

public static class Program
{
    public static void Main(string[] args)
    {
        Solve(args);
    }

    public static int Solve(string[] args)
    {
        // Reading the file INPUT.txt from the current directory
        string inputFilePath = "INPUT.txt";
        string outputFilePath = "OUTPUT.txt";

        if (!File.Exists(inputFilePath))
        {
            WriteToFile(outputFilePath, "Input file not found!");
            return -1;
        }

        string[] input = File.ReadAllLines(inputFilePath);

        Console.WriteLine("File content:");
        foreach (var line in input)
        {
            Console.WriteLine(line);
        }       
        

        if (input == null || input.Length < 4)
        {
            WriteToFile(outputFilePath, "Invalid input format.");
            return -1;
        }

        // Parse the inputs
        int[] firstLine = ParseLine(input[0]);
        if (firstLine == null || firstLine.Length < 3)
        {
            WriteToFile(outputFilePath, "Error parsing first line");
            return -1;
        }

        int N = firstLine[0];  // Number of gangsters
        int K = firstLine[1];  // Maximum door openness
        int T = firstLine[2];  // Restaurant operating time

        int[] arrivalTimes = ParseLine(input[1]);
        int[] wealths = ParseLine(input[2]);
        int[] requiredDoorStates = ParseLine(input[3]);

        // Ensure all lines are parsed correctly
        if (!ValidateInputData(N, arrivalTimes, wealths, requiredDoorStates))
        {
            WriteToFile(outputFilePath, "Invalid input data.");
            return -1;
        }
        
        // List of gangsters
        var gangsters = CreateGangsters(N, arrivalTimes, wealths, requiredDoorStates);

              // Sort gangsters by arrival time
        gangsters = gangsters.OrderBy(g => g.ArrivalTime).ToArray();

        // Calculate total wealth
        int totalWealth = CalculateTotalWealth(gangsters, T);

        // Output the result
        WriteToFile(outputFilePath, totalWealth.ToString());
        Console.WriteLine(totalWealth);
        return totalWealth;
    }

    // Helper method to parse each line into an integer array
    public static int[] ParseLine(string line)
    {
        try
        {
            return line?.Split().Select(int.Parse).ToArray();
        }
        catch
        {
            return null;
        }
    }

    // Helper method to write to a file
    public static void WriteToFile(string filePath, string content)
    {
        File.AppendAllText(filePath, content + Environment.NewLine);
    }

    // Helper method to print arrays to a file
    public static void PrintArrayToFile(string filePath, string label, int[] array)
    {
        WriteToFile(filePath, label + string.Join(" ", array));
    }

    // Validate the input data arrays
    public static bool ValidateInputData(int N, int[] arrivalTimes, int[] wealths, int[] requiredDoorStates)
    {
        return arrivalTimes != null && wealths != null && requiredDoorStates != null &&
               arrivalTimes.Length == N && wealths.Length == N && requiredDoorStates.Length == N;
    }

    // Create gangsters from parsed data
    public static Gangster[] CreateGangsters(int N, int[] arrivalTimes, int[] wealths, int[] requiredDoorStates)
    {
        var gangsters = new Gangster[N];
        for (int i = 0; i < N; i++)
        {
            gangsters[i] = new Gangster
            {
                ArrivalTime = arrivalTimes[i],
                Wealth = wealths[i],
                RequiredDoorState = requiredDoorStates[i]
            };
        }
        return gangsters;
    }

    // Output details of each gangster to a file
    public static void PrintGangsterDetailsToFile(string filePath, Gangster[] gangsters)
    {
        for (int i = 0; i < gangsters.Length; i++)
        {
            WriteToFile(filePath, $"Gangster {i + 1}:");
            WriteToFile(filePath, $"  Arrival Time: {gangsters[i].ArrivalTime}");
            WriteToFile(filePath, $"  Wealth: {gangsters[i].Wealth}");
            WriteToFile(filePath, $"  Required Door State: {gangsters[i].RequiredDoorState}");
            WriteToFile(filePath, ""); // Adds a blank line for formatting
        }
    }

    // Calculate total wealth based on gangster arrival times and door state adjustments
    public static int CalculateTotalWealth(Gangster[] gangsters, int T)
    {
        int currentDoorState = 0;
        int currentTime = 0;
        int totalWealth = 0;

        foreach (var gangster in gangsters)
        {
            // Move time to the gangster's arrival time if needed
            if (currentTime < gangster.ArrivalTime)
            {
                currentTime = gangster.ArrivalTime;
            }

            // Calculate the time needed to adjust the door to the required state
            int timeToAdjustDoor = Math.Abs(currentDoorState - gangster.RequiredDoorState);

            // Check if we can adjust the door to the required state before the gangster arrives
            if (currentTime + timeToAdjustDoor <= T && currentTime + timeToAdjustDoor <= gangster.ArrivalTime)
            {
                // Adjust the door state
                currentTime += timeToAdjustDoor;
                currentDoorState = gangster.RequiredDoorState;

                // the gangster can enter
                totalWealth += gangster.Wealth;
            }

            // Ensure that we don't exceed the restaurant's operating time
            if (currentTime >= T)
            {
                break;
            }
        }

        return totalWealth;
    }
}

public class Gangster
{
    public int ArrivalTime { get; set; }
    public int Wealth { get; set; }
    public int RequiredDoorState { get; set; }
}
