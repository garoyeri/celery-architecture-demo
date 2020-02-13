namespace CeleryArchitectureDemo.Features.Todo
{
    using System;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<TodoItemList>> Get()
        {
            var result = await _mediator.Send(new GetItems.Query());

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<ActionResult<TodoItem>> Add([FromBody] AddItem.Command command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<TodoItem>> Edit([FromRoute] Guid id, [FromBody] EditItem.Command command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("{id}/complete")]
        public async Task<ActionResult<TodoItem>> Complete([FromRoute] Guid id)
        {
            var command = new CompleteItem.Command {Id = id};
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("cleanup")]
        public async Task<ActionResult<TodoItemList>> ClearCompletedItems()
        {
            var command = new ClearCompletedItems.Command();
            await _mediator.Send(command);
            var result = await _mediator.Send(new GetItems.Query());
            return Ok(result);
        }
    }
}