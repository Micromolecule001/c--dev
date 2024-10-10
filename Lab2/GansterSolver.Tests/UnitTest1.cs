using Xunit;
using System.IO;

public class GansterSolverTests 
{
    [Fact]
    public void ParseLineTests()
    {
        // Arrange
        var input = new[] { "4 10 20", "10 16 8 16", "10 11 15 11", "0 7 1 8" };

        int[] expectedOutput1 = new[] {4, 10, 20};
        int[] expectedOutput2 = new[] {10, 16, 8, 16};
        int[] expectedOutput3 = new[] {10, 11, 15, 11};
        int[] expectedOutput4 = new[] {0, 7, 1, 8};

        // Act
        int[] result1 = Program.ParseLine(input[0]);  
        int[] result2 = Program.ParseLine(input[1]);  
        int[] result3 = Program.ParseLine(input[2]);  
        int[] result4 = Program.ParseLine(input[3]);  
        
        // Assert
        Assert.Equal(expectedOutput1, result1);
        Assert.Equal(expectedOutput2, result2);
        Assert.Equal(expectedOutput3, result3);
        Assert.Equal(expectedOutput4, result4);
    }

    [Fact]
    public void ValidateInputDataTest_ValidData_ReturnsTrue()
    {
        // Arrange
        int N = 4;
        int[] arrivalTimes = { 10, 16, 8, 16 };
        int[] wealths = { 10, 11, 15, 11 };
        int[] requiredDoorStates = { 0, 7, 1, 8 };

        // Act
        bool result = Program.ValidateInputData(N, arrivalTimes, wealths, requiredDoorStates);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateInputDataTest_InvalidData_ReturnsFalse()
    {
        // Arrange
        int N = 4;
        int[] arrivalTimes = { 10, 16, 8 };  // Invalid length
        int[] wealths = { 10, 11, 15, 11 };
        int[] requiredDoorStates = { 0, 7, 1, 8 };

        // Act
        bool result = Program.ValidateInputData(N, arrivalTimes, wealths, requiredDoorStates);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CreateGangstersTest()
    {
        // Arrange
        int N = 2;
        int[] arrivalTimes = { 10, 16 };
        int[] wealths = { 100, 200 };
        int[] requiredDoorStates = { 1, 2 };

        var expectedGangsters = new Gangster[]
        {
            new Gangster { ArrivalTime = 10, Wealth = 100, RequiredDoorState = 1 },
            new Gangster { ArrivalTime = 16, Wealth = 200, RequiredDoorState = 2 }
        };

        // Act
        var result = Program.CreateGangsters(N, arrivalTimes, wealths, requiredDoorStates);

        // Assert
        Assert.Equal(expectedGangsters[0].ArrivalTime, result[0].ArrivalTime);
        Assert.Equal(expectedGangsters[0].Wealth, result[0].Wealth);
        Assert.Equal(expectedGangsters[0].RequiredDoorState, result[0].RequiredDoorState);

        Assert.Equal(expectedGangsters[1].ArrivalTime, result[1].ArrivalTime);
        Assert.Equal(expectedGangsters[1].Wealth, result[1].Wealth);
        Assert.Equal(expectedGangsters[1].RequiredDoorState, result[1].RequiredDoorState);
    }

    [Fact]
    public void WriteToFileTest()
    {
        // Arrange
        string filePath = "TestOutput.txt";
        string content = "Test line content";

        // Act
        Program.WriteToFile(filePath, content);

        // Assert
        string fileContent = File.ReadAllText(filePath);
        Assert.Contains(content, fileContent);

        // Cleanup
        File.Delete(filePath);
    }

    [Fact]
    public void PrintArrayToFileTest()
    {
        // Arrange
        string filePath = "TestArrayOutput.txt";
        int[] array = { 1, 2, 3, 4 };
        string label = "Array: ";

        // Act
        Program.PrintArrayToFile(filePath, label, array);

        // Assert
        string fileContent = File.ReadAllText(filePath);
        Assert.Contains("Array: 1 2 3 4", fileContent);

        // Cleanup
        File.Delete(filePath);
    }

    [Fact]
    public void PrintGangsterDetailsToFileTest()
    {
        // Arrange
        string filePath = "GangsterDetails.txt";
        var gangsters = new Gangster[]
        {
            new Gangster { ArrivalTime = 10, Wealth = 100, RequiredDoorState = 1 },
            new Gangster { ArrivalTime = 16, Wealth = 200, RequiredDoorState = 2 }
        };

        // Act
        Program.PrintGangsterDetailsToFile(filePath, gangsters);

        // Assert
        string fileContent = File.ReadAllText(filePath);
        Assert.Contains("Gangster 1:", fileContent);
        Assert.Contains("Arrival Time: 10", fileContent);
        Assert.Contains("Wealth: 100", fileContent);
        Assert.Contains("Gangster 2:", fileContent);

        // Cleanup
        File.Delete(filePath);
    }
}
