using Tree.API.Services;

namespace Tree.TEST;

public class NodeServiceTest
{
    [Fact]
    public async void AddNode_WhenCalled_ReturnNode()
    {
        //Arrange
        var nodeService = new NodeService<string>();

        //Act
        var actual = await nodeService.AddNodeAsync("A", "Data A");

        //Assert
        Assert.True(actual.Name.Equals("A"));
        Assert.True(actual.Data?.Equals("Data A"));
    }

    [Fact]
    public async void GetNodeByNameAsync_WhenCalled_ReturnNode()
    {
        //Arrange
        var nodeService = new NodeService<string>();
        var nodeA = await nodeService.AddNodeAsync("A", "Data A");
        var nodeB = await nodeService.AddNodeAsync("B", "Data B", "A");

        //Act
        var actual = await nodeService.GetNodeByNameAsync("B");

        //Assert
        Assert.True(actual?.Name.Equals("B"));
        Assert.True(actual?.Data?.Equals("Data B"));
    }

    [Fact]
    public async void DeleteNodeAsync_WhenCalled_ReturnNothing()
    {
        //Arrange
        var nodeService = new NodeService<string>();
        var nodeA = await nodeService.AddNodeAsync("A", "Data A");
        var nodeB = await nodeService.AddNodeAsync("B", "Data B", "A");
        var nodeC= await nodeService.AddNodeAsync("C", "Data C", "A");
        var nodeD= await nodeService.AddNodeAsync("D", "Data D", "B");

        //Act
        await nodeService.DeleteNodeAsync("B");
        var actual = await nodeService.GetNodeByNameAsync("B");
        var actual2 = await nodeService.GetNodeByNameAsync("D");

        //Assert
        Assert.True(actual == null);
        Assert.True(actual2 == null);
    }

    [Fact]
    public async void UpdateNodeAsync_WhenCalled_ReturnNothing()
    {
        //Arrange
        var nodeService = new NodeService<string>();
        var nodeA = await nodeService.AddNodeAsync("A", "Data A");
        var nodeB = await nodeService.AddNodeAsync("B", "Data B", "A");
        var nodeC = await nodeService.AddNodeAsync("C", null, "B");

        //Act
        await nodeService.UpdateNodeAsync("C", "Data C");
        var actual = await nodeService.GetNodeByNameAsync("C");

        //Assert
        Assert.True(actual?.Data?.Equals("Data C"));
    }
}