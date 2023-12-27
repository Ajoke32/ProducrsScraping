using ProductsParser.Abstraction.App;
using ProductsParser.Abstraction.ExecutionStrategy;
using ProductsParser.Abstraction.Options;
using ProductsParser.Core.Parsers;
using ProductsParser.Factories;

namespace ProductsParser.Core;

public class ProductsParserApp:IProductParser
{

    private readonly IAppParserOptions _parserOptions;

    private IExecutionStrategy? _executionStrategy;
    
    public ProductsParserApp(IAppParserOptions options)
    {
        _parserOptions = options;
    }

    
    public void ConfigureExecutionStrategy<T>() where T:IExecutionStrategy
    {
        _executionStrategy = ExecutionStrategyFactory.CreateInstance<T>
            (_parserOptions.ParsingTemplate,_parserOptions.DataStorage);
    }
    
    public void Run()
    {
        _executionStrategy ??= new ExecutionWithoutUserInteractionStrategy(_parserOptions.ParsingTemplate,
            _parserOptions.DataStorage);
        
        Console.WriteLine("App started");
        
        _executionStrategy.Parse();

        Console.WriteLine("Parsing completed, press any key to exit");
    }

    public void Restart()
    {
        Run();
    }
    public void Stop()
    {
        _parserOptions.Driver.Close();
        _parserOptions.Driver.Quit();
    }
}