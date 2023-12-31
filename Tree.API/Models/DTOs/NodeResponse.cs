namespace Tree.API.Models.DTOs;

public class NodeResponse<T>
{
    public string? Name { get; set; }
    public T? Data { get; set; }
    public ICollection<NodeResponse<T>>? Children { get; set; }
}