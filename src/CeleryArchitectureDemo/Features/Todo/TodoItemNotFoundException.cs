namespace CeleryArchitectureDemo.Features.Todo
{
    [System.Serializable]
    public class TodoItemNotFoundException : System.Exception
    {
        public TodoItemNotFoundException() { }
        public TodoItemNotFoundException(string message) : base(message) { }
        public TodoItemNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected TodoItemNotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}