using OpenQA.Selenium;
using ProductsParser.Abstraction.Data;
using ProductsParser.Abstraction.ExecutionStrategy;
using ProductsParser.Abstraction.Templates;

namespace ProductsParser.Factories;

public static class ExecutionStrategyFactory
{
    public static IExecutionStrategy CreateInstance<T>(IParsingTemplate template,IDataStorage dataStorage) where T : IExecutionStrategy
    {
        var type = typeof(T);

      
        var constructor = type.GetConstructor(new[] { typeof(IParsingTemplate), typeof(IDataStorage) });

        if (constructor != null)
        {
            return (IExecutionStrategy)constructor.Invoke(new object[] { template, dataStorage });
        }

        throw new Exception("Execution strategy must contains a constructor");
    }
}