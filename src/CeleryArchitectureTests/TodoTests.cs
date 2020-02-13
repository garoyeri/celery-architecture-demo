namespace CeleryArchitectureTests
{
    using System.Threading.Tasks;
    using CeleryArchitectureDemo.Features.Todo;
    using Shouldly;
    using static Testing;

    public class TodoTests
    {
        public async Task SetUp()
        {
            await DeleteTable();
            await CreateTable();
        }

        public async Task ShouldCreateTodoItem()
        {
            var response = await Send(new AddItem.Command() {Description = "First Item [ShouldCreateTodoItem]"});

            var itemFound = await QueryContext().LoadAsync<TodoItem>(response.Id);
            itemFound.Id.ShouldBe(response.Id);
            itemFound.Description.ShouldBe("First Item [ShouldCreateTodoItem]");
        }
    }
}