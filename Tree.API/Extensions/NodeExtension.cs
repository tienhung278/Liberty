using Tree.API.Models;
using Tree.API.Models.DTOs;

namespace Service1.API.Extensions;

public static class NodeExtension
{
    public static NodeResponse<T> ToNodeResponse<T>(this Node<T> node)
    {
        return new NodeResponse<T>
        {
            Name = node.Name,
            Data = node.Data,
            Children = node.Children?.Select(n => n.ToNodeResponse()).ToList()
        };
    }
}