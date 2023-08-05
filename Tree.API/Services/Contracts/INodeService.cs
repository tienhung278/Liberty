using Tree.API.Models;

namespace Tree.API.Services.Contracts;

public interface INodeService<T>
{
    Task<Node<T>?> GetNodeByNameAsync(string name);
    Task<Node<T>> AddNodeAsync(string name, T? data, string? parentName = null);
    Task UpdateNodeAsync(string name, T? data);
    Task DeleteNodeAsync(string name);
}