using Tree.API.Models;
using Tree.API.Services.Contracts;

namespace Tree.API.Services;

public class NodeService<T> : INodeService<T>
{
    private readonly Node<T> _root = new("root");

    public async Task<Node<T>?> GetNodeByNameAsync(string name)
    {
        return await _root.FindChildAsync(name);
    }

    public async Task<Node<T>> AddNodeAsync(string name, T? data, string? parentName = null)
    {
        Node<T> node;

        if (string.IsNullOrEmpty(parentName))
        {
            node = await _root.AddChildAsync(name, data);
        }
        else
        {
            var parent = await GetNodeByNameAsync(parentName);
            node = await parent?.AddChildAsync(name, data)!;
        }

        return node;
    }

    public async Task UpdateNodeAsync(string name, T? data)
    {
        var node = await GetNodeByNameAsync(name);
        if (node != null) node.Data = data;
    }

    public async Task DeleteNodeAsync(string name)
    {
        var node = await GetNodeByNameAsync(name);
        if (node != null)
        {
            var parent = node.Parent;
            await parent?.DeleteChildAsync(node)!;
        }
    }
}