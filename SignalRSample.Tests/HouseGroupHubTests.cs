using Microsoft.AspNetCore.SignalR;
using Moq;
using SignalRSample.Hubs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class HouseGroupHubTests
{
    private readonly HouseGroupHub _hub;
    private readonly Mock<IHubCallerClients> _mockClients;
    private readonly Mock<IGroupManager> _mockGroups;
    private readonly Mock<HubCallerContext> _mockContext;

    public HouseGroupHubTests()
    {
        _mockClients = new Mock<IHubCallerClients>();
        _mockGroups = new Mock<IGroupManager>();
        _mockContext = new Mock<HubCallerContext>();

        _hub = new HouseGroupHub
        {
            Clients = _mockClients.Object,
            Groups = _mockGroups.Object,
            Context = _mockContext.Object
        };
    }

    [Fact]
    public async Task JoinHouse_ShouldAddHouseToGroupJoined()
    {
        // Arrange
        var connectionId = "connectionId";
        var houseName = "TestHouse";
        _mockContext.Setup(c => c.ConnectionId).Returns(connectionId);

        // Act
        await _hub.JoinHouse(houseName);

        // Assert
        Assert.Contains($"{connectionId}:{houseName}", HouseGroupHub.GroupJoined);
    }

    [Fact]
    public async Task LeaveHouse_ShouldRemoveHouseFromGroupJoined()
    {
        // Arrange
        var connectionId = "connectionId";
        var houseName = "TestHouse";
        HouseGroupHub.GroupJoined.Add($"{connectionId}:{houseName}");
        _mockContext.Setup(c => c.ConnectionId).Returns(connectionId);

        // Act
        await _hub.LeaveHouse(houseName);

        // Assert
        Assert.DoesNotContain($"{connectionId}:{houseName}", HouseGroupHub.GroupJoined);
    }

    [Fact]
    public async Task GetHouseList_ShouldReturnHouseNames()
    {
        // Arrange
        var connectionId = "connectionId";
        var houseName = "TestHouse";
        HouseGroupHub.GroupJoined.Add($"{connectionId}:{houseName}");
        _mockContext.Setup(c => c.ConnectionId).Returns(connectionId);

        // Act
        var result = await _hub.GetHouseList();

        // Assert
        Assert.Equal(houseName, result);
    }

    [Fact]
    public async Task TriggerNotifyHouse_ShouldSendNotificationToGroup()
    {
        // Arrange
        var houseName = "TestHouse";
        var mockClients = new Mock<IHubCallerClients>();
        var mockClient = new Mock<IClientProxy>();
        mockClients.Setup(clients => clients.Group(houseName)).Returns(mockClient.Object);
        _hub.Clients = mockClients.Object;

        // Act
        await _hub.TriggerNotifyHouse(houseName);

        // Assert
        mockClient.Verify(client => client.SendCoreAsync(
            "triggerNotification",
            It.Is<object[]>(o => o.Length == 1 && (string)o[0] == houseName),
            default),
            Times.Once);
    }
}
