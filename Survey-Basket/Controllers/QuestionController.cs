using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Survey_Basket.Contracts.Questions;
using Survey_Basket.Services;

namespace Survey_Basket.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService questionService;
        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int pollId, int id, CancellationToken cancellationToken = default)
        {
            var result = await questionService.GetAsync(pollId, id, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: result.Error.Code,
                detail: result.Error.Description
            );
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(int pollId, [FromBody] QuestionRequest request, CancellationToken cancellationToken = default)
        {
            var result = await questionService.AddAsync(pollId, request, cancellationToken);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(AddAsync), new { pollId, id = result.Value.Id }, result.Value);
            }
            return Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: result.Error.Code,
                detail: result.Error.Description
            );
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync(int pollId, CancellationToken cancellationToken = default)
        {
            var result = await questionService.GetAllAsync(pollId, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: result.Error.Code,
                detail: result.Error.Description
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int pollId, int id, [FromBody] QuestionRequest request, CancellationToken cancellationToken = default)
        {
            var result = await questionService.UpdateAsync(pollId, id, request, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok("The Question is updated");
            }
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: result.Error.Code,
                detail: result.Error.Description
            );
        }

        [HttpPut("{id}/toggleStatus")]
        public async Task<IActionResult> ToggleStatusAsync(int pollId, int id, CancellationToken cancellationToken = default)
        {
            var result = await questionService.ToggleStatusAsync(pollId, id, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok("The Question status is toggled");
            }
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: result.Error.Code,
                detail: result.Error.Description
            );

        }
    }
}
