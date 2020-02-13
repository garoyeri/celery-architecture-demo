namespace CeleryArchitectureTests
{
    using System;
    using Fixie;

    public class TestingConvention : Discovery, Execution, IDisposable
    {
        public TestingConvention()
        {
            Methods.Where(x => x.Name != "SetUp");
        }

        public void Execute(TestClass testClass)
        {
            testClass.RunCases(@case =>
            {
                var instance = testClass.Construct();

                SetUp(instance);

                @case.Execute(instance);
            });
        }

        public void Dispose()
        {
        }

        static void SetUp(object instance)
        {
            instance.GetType().GetMethod("SetUp")?.Execute(instance);
        }
    }
}