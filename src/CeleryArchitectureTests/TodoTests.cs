namespace CeleryArchitectureTests
{
    using System.Threading.Tasks;
    using CeleryArchitectureDemo.Features.Todo;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using static Testing;
    using TodoItem = CeleryArchitectureDemo.Domain.TodoItem;

    public class TodoTests
    {
        public async Task ShouldCreateTodoItem()
        {
            var response = await Send(new AddItem.Command() {Description = "First Item [ShouldCreateTodoItem]"});

            (await Query<TodoItem>().SingleAsync(i => i.Id == response.Id)).Description.ShouldBe(
                "First Item [ShouldCreateTodoItem]");
        }
    }
}