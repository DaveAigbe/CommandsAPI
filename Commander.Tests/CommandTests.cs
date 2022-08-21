using Commander.Models;

namespace Commander.Tests;

// IDisposable allows us to setup a new instance of the testCommand object for each test (so they are not sharing values)
public class CommandTests : IDisposable
{
   Command testCommand;

    public CommandTests()
    {
        // Arrange
        testCommand = new Command()
        {
            HowTo = "Do something awesome",
            Platform = "xUnit",
            Line = "dotnet test"
        };
    }
    
    // Method must be implemented from IDisposable
    // This cleans up the code
    public void Dispose()
    {
        testCommand = null;
    }
    
    
    
    [Fact]
    public void CanChangeHowTo()
    {
        // Act
        testCommand.HowTo = "Execute Unit Tests";
        
        // Assert
        Assert.Equal("Execute Unit Tests", testCommand.HowTo);
    }

    [Fact]
    public void CanChangePlatform()
    {
        testCommand.Platform = "nUnit";
        
        Assert.Equal("nUnit", testCommand.Platform);
    }

    [Fact]
    public void CanChangeLine()
    {
        testCommand.Line = "nUnit tests all";
        
        Assert.Equal("nUnit tests all", testCommand.Line);
    }
    
}