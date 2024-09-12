using SignalRSample.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRSample.Tests;

public  class DeathlyHallawsHubTests
{
    private readonly DeathlyHallawsHub _hub;

    public DeathlyHallawsHubTests()
    {
        _hub = new DeathlyHallawsHub();
    }

    //[Fact]
    //public void GetRaceStatus_ShouldReturnCorrectRaceStatus()
    //{
    //    // Arrange
    //    var expectedRaceStatus = new Dictionary<string, int>
    //    {
    //        { "Gryffindor", 10 },
    //        { "Slytherin", 8 },
    //        { "Ravenclaw", 12 },
    //        { "Hufflepuff", 6 }
    //    };
    //    SD.DeathlyHallawRace = expectedRaceStatus;

    //    // Act
    //    var result = _hub.GetRaceStatus();

    //    // Assert
    //    Assert.Equal(expectedRaceStatus, result);
    //}
}

