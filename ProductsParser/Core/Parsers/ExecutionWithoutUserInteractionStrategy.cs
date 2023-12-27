using ProductsParser.Abstraction.Data;
using ProductsParser.Abstraction.ExecutionStrategy;
using ProductsParser.Abstraction.Templates;


namespace ProductsParser.Core.Parsers;

public class ExecutionWithoutUserInteractionStrategy:IExecutionStrategy
{

    private readonly IParsingTemplate _parsingTemplate;

    private readonly IDataStorage _dataStorage;

    public ExecutionWithoutUserInteractionStrategy (IParsingTemplate template,IDataStorage dataStorage)
    {
        _parsingTemplate = template;
        _dataStorage = dataStorage;
    }
    
    public void Parse()
    {
        try
        {
            _parsingTemplate.StartParing();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        var products = _parsingTemplate.GetProducts();

        _dataStorage.Save(products);
    }
}