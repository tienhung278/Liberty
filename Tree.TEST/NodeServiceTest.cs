using Tree.API.Services;

namespace Tree.TEST;

public class NodeServiceTest
{
    private readonly NodeService<string> _nodeService = new NodeService<string>();
    
    private async Task PrepareDataAsync()
    {
        await _nodeService.AddNodeAsync("A", "Data A");
        await _nodeService.AddNodeAsync("B", "Data B", "A");
        await _nodeService.AddNodeAsync("C", "Data C", "A");
        await _nodeService.AddNodeAsync("D", "Data D", "A");
        await _nodeService.AddNodeAsync("E", "Data E", "B");
        await _nodeService.AddNodeAsync("F", "Data F", "B");
        await _nodeService.AddNodeAsync("G", "Data G", "B");
        await _nodeService.AddNodeAsync("H", "Data H", "C");
        await _nodeService.AddNodeAsync("I", "Data I", "D");
        await _nodeService.AddNodeAsync("J", "Data J", "D");
        await _nodeService.AddNodeAsync("K", "Data K", "I");
        await _nodeService.AddNodeAsync("L", "Data L", "I");
        await _nodeService.AddNodeAsync("M", "Data M", "I");
    }
    
    [Fact]
    public async void AddNode_NameDuplicated_ReturnArgumentException()
    {
        //Arrange
        await PrepareDataAsync();
        
        //Assert
        await Assert.ThrowsAsync<ArgumentException>( async () =>
        {
            //Act
            await _nodeService.AddNodeAsync("A", "Data A");
            await _nodeService.AddNodeAsync("A", "Data A");
        });
    }
    
    [Fact]
    public async void AddNode_WhenCalled_ReturnNode()
    {
        //Arrange
        await PrepareDataAsync();
        
        //Act
        var actual = await _nodeService.AddNodeAsync("N", "Data N", "M");

        //Assert
        Assert.True(actual.Name.Equals("N"));
        Assert.True(actual.Data?.Equals("Data N"));
    }

    [Fact]
    public async void GetNodeByNameAsync_WhenCalled_ReturnNode()
    {
        //Arrange
        await PrepareDataAsync();
        
        //Act
        var actual = await _nodeService.GetNodeByNameAsync("M");

        //Assert
        Assert.True(actual?.Name.Equals("M"));
        Assert.True(actual?.Data?.Equals("Data M"));
    }

    [Fact]
    public async void DeleteNodeAsync_WhenCalled_ReturnNothing()
    {
        //Arrange
        await PrepareDataAsync();
        
        //Act
        await _nodeService.DeleteNodeAsync("D");
        var actual = await _nodeService.GetNodeByNameAsync("D");
        var actual2 = await _nodeService.GetNodeByNameAsync("J");
        var actual3 = await _nodeService.GetNodeByNameAsync("L");

        //Assert
        Assert.True(actual == null);
        Assert.True(actual2 == null);
        Assert.True(actual3 == null);
    }

    [Fact]
    public async void UpdateNodeAsync_WhenCalled_ReturnNothing()
    {
        //Arrange
        await PrepareDataAsync();
        
        //Act
        await _nodeService.UpdateNodeAsync("H", "Hello World");
        var actual = await _nodeService.GetNodeByNameAsync("H");

        //Assert
        Assert.True(actual?.Data?.Equals("Hello World"));
    }
}