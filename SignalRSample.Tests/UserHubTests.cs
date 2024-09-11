using Microsoft.AspNetCore.SignalR;
using Moq;
using SignalRSample.Hubs;
using System.Threading.Tasks;
using Xunit;
namespace SignalRSample.Tests;
public class UserHubTests
{
    private readonly UserHub _hub;
    private readonly Mock<IHubCallerClients> _mockClients;
    private readonly Mock<HubCallerContext> _mockContext;
    private readonly Mock<IClientProxy> _mockClientProxy;

    public UserHubTests()
    {
        _mockClients = new Mock<IHubCallerClients>();
        _mockContext = new Mock<HubCallerContext>();
        _mockClientProxy = new Mock<IClientProxy>();

        _hub = new UserHub
        {
            Clients = _mockClients.Object,
            Context = _mockContext.Object
        };
    }

    [Fact]
    public async Task OnConnectedAsync_ShouldIncrementTotalUsersAndNotifyClients()
    {
        // Arrange
        _mockClients.Setup(c => c.All).Returns(_mockClientProxy.Object);

        // Act
        await _hub.OnConnectedAsync();

        // Assert
        Assert.Equal(1, UserHub.TotalUsers);
        _mockClientProxy.Verify(c => c.SendCoreAsync(
            "updateTotalUsers",
            It.Is<object[]>(o => (int)o[0] == UserHub.TotalUsers),
            default),
            Times.Once);
    }

    [Fact]
    public async Task OnDisconnectedAsync_ShouldDecrementTotalUsersAndNotifyClients()
    {
        // Arrange
        UserHub.TotalUsers = 1;
        _mockClients.Setup(c => c.All).Returns(_mockClientProxy.Object);

        // Act
        await _hub.OnDisconnectedAsync(null);

        // Assert
        Assert.Equal(0, UserHub.TotalUsers);
        _mockClientProxy.Verify(c => c.SendCoreAsync(
            "updateTotalUsers",
            It.Is<object[]>(o => (int)o[0] == UserHub.TotalUsers),
            default),
            Times.Once);
    }

    [Fact]
    public async Task NewWindowLoaded_ShouldIncrementTotalViewsAndNotifyClients()
    {
        // Arrange
        var name = "TestName";
        _mockClients.Setup(c => c.All).Returns(_mockClientProxy.Object);

        // Act
        var result = await _hub.NewWindowLoaded(name);

        // Assert
        Assert.Equal(1, UserHub.TotalViews);
        _mockClientProxy.Verify(c => c.SendCoreAsync(
            "updateTotalViews",
            It.Is<object[]>(o => (int)o[0] == UserHub.TotalViews),
            default),
            Times.Once);
        Assert.Equal($"Total views: 1. Name= {name}", result);
    }
}
