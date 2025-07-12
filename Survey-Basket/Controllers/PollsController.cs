using Mapster;
using Microsoft.AspNetCore.Mvc;
using Survey_Basket.Contracts.Poll;
using SurveyBasket.Services;

namespace SurveyBasket.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class PollsController(IPollService pollService) : ControllerBase
{
    private readonly IPollService _pollService = pollService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var polls = await _pollService.GetAllAsync();
        var response = polls.Adapt<IEnumerable<PollResponse>>();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var pollResult = await _pollService.GetAsync(id);
        return pollResult.IsSuccess
            ? Ok(pollResult.Value)
            : Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: pollResult.Error.Code,
                detail: pollResult.Error.Description
            );
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] PollRequest request, CancellationToken cancellationToken)
    {
        var newPoll = await _pollService.AddAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request, CancellationToken cancellationToken)
    {
        var isUpdated = await _pollService.UpdateAsync(id, request, cancellationToken);
        return isUpdated.IsSuccess
            ? Ok("The Poll is updated")
            : Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: isUpdated.Error.Code,
                detail: isUpdated.Error.Description
            );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var isDeleted = await _pollService.DeleteAsync(id, cancellationToken);
        return isDeleted.IsSuccess
            ? Ok("The Poll is deleted")
            : Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: isDeleted.Error.Code,
                detail: isDeleted.Error.Description
            );
    }

    [HttpPut("{id}/togglePublish")]
    public async Task<IActionResult> TogglePublish([FromRoute] int id, CancellationToken cancellationToken)
    {
        var isUpdated = await _pollService.TogglePublishStatusAsync(id, cancellationToken);
        return isUpdated.IsSuccess
            ? Ok("The Poll togglePublish status is updated")
            : Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: isUpdated.Error.Code,
                detail: isUpdated.Error.Description
            );
    }
}
