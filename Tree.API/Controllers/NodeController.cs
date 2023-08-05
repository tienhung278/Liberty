using Microsoft.AspNetCore.Mvc;
using Service1.API.Extensions;
using Tree.API.Models.DTOs;
using Tree.API.Services.Contracts;

namespace Tree.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NodeController : Controller
{
    private readonly INodeService<string> _nodeService;

    public NodeController(INodeService<string> nodeService)
    {
        _nodeService = nodeService;
    }

    [HttpGet("{name}")]
    public async Task<ActionResult> Get(string name)
    {
        var node = await _nodeService.GetNodeByNameAsync(name);
        return Ok(node?.ToNodeResponse());
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] NodeRequest nodeRequest)
    {
        var node = await _nodeService.AddNodeAsync(nodeRequest.Name!, nodeRequest.Data, nodeRequest.ParentName);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] NodeRequest nodeRequest)
    {
        await _nodeService.UpdateNodeAsync(nodeRequest.Name!, nodeRequest.Data);
        return NoContent();
    }

    [HttpDelete("name")]
    public async Task<ActionResult> Delete(string name)
    {
        await _nodeService.DeleteNodeAsync(name);
        return NoContent();
    }
}