namespace CeleryArchitectureTests
{
    using System;
    using CeleryArchitectureDemo.Infrastructure;
    using Fixie;
    using static Testing;

    public class TestingConvention : Execution, IDisposable
    {

        public TestingConvention()
        {
            Migrate<TodoContext>();
        }

        public void Execute(TestClass testClass)
        {
            var instance = testClass.Construct();

            testClass.RunCases(@case =>
            {
                @case.Execute(instance);
            });

            instance.Dispose();
        }

        public void Dispose()
        {
            DeleteDatabase<TodoContext>();
        }
    }
}
