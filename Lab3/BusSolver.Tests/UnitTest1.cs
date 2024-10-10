using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace BusSolverTests
{
    public class BusSolverTests
    {
        [Fact]
        public void Test_EarlyArrival_Scenario()
        {
            // Arrange
            string input = "3\n1 2\n3\n1 0 2 5\n1 1 3 3\n3 4 2 5";
            File.WriteAllText("INPUT.TXT", input);

            // Act
            Program.Main(new string[0]);

            // Assert
            var result = File.ReadAllText("OUTPUT.TXT");
            Assert.Equal("5", result.Trim());
        }

        [Fact]
        public void Test_NoRoute_Scenario()
        {
            // Arrange
            string input = "3\n1 2\n0";
            File.WriteAllText("INPUT.TXT", input);

            // Act
            Program.Main(new string[0]);

            // Assert
            var result = File.ReadAllText("OUTPUT.TXT");
            Assert.Equal("-1", result.Trim());
        }

        [Fact]
        public void Test_SingleVillage_Scenario()
        {
            // Arrange
            string input = "1\n1 1\n0";
            File.WriteAllText("INPUT.TXT", input);

            // Act
            Program.Main(new string[0]);

            // Assert
            var result = File.ReadAllText("OUTPUT.TXT");
            Assert.Equal("0", result.Trim());
        }

        [Fact]
        public void Test_MultipleRoutes_Scenario()
        {
            // Arrange
            string input = "3\n1 3\n4\n1 0 2 5\n1 1 3 3\n2 6 3 8\n3 9 1 10";
            File.WriteAllText("INPUT.TXT", input);

            // Act
            Program.Main(new string[0]);

            // Assert
            var result = File.ReadAllText("OUTPUT.TXT");
            Assert.Equal("3", result.Trim());
        }

        [Fact]
        public void Test_LateDeparture_Scenario()
        {
            // Arrange
            string input = "3\n1 2\n3\n1 5 2 10\n1 1 3 4\n3 4 2 5";
            File.WriteAllText("INPUT.TXT", input);

            // Act
            Program.Main(new string[0]);

            // Assert
            var result = File.ReadAllText("OUTPUT.TXT");
            Assert.Equal("5", result.Trim());
        }
    }
}