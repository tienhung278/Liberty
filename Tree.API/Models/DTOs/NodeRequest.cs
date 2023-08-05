using System.ComponentModel.DataAnnotations;

namespace Tree.API.Models.DTOs;

public class NodeRequest
{
    [Required] public string? Name { get; set; }

    public string? Data { get; set; }
    public string? ParentName { get; set; }
}