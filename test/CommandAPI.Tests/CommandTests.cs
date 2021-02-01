using System;
using Xunit;
using CommandAPI.Models;
namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {
        
        Command testCommand;

        public CommandTests()
        {
            testCommand = new Command
            {
                HowTo = "Do something awesome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };
        }
        
        public void Dispose()
        {
            testCommand = null;
        }
        
        [Fact]
        public void CanChangeHowTo()
        {
            
            //Act
            testCommand.Platform = ".Net Core";
            //Assert
            Assert.Equal(".Net Core", testCommand.Platform);

            
        }
        
        [Fact]
        public void puedoCambiarPlatform() 
        {
            
            //Act
            testCommand.Platform = "Execute Unit Tests";
            //Assert
            Assert.Equal("Execute Unit Tests", testCommand.Platform);
        }

        [Fact]
        public void puedoCambiarCommandLine()
        {
            

            testCommand.CommandLine = "dotnet --version";
            Assert.Equal("dotnet --version", testCommand.CommandLine);
        }
    }
}