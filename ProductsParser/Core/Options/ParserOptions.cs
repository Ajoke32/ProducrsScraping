using OpenQA.Selenium;
using ProductsParser.Abstraction.Data;
using ProductsParser.Abstraction.Options;
using ProductsParser.Abstraction.Templates;


namespace ProductsParser.Core.Options;

public class ParserOptions:IAppParserOptions
{
    public IWebDriver Driver { get;}
    
    public IParsingTemplate ParsingTemplate { get; }
    
    public IDataStorage DataStorage { get; }
    
    public ParserOptions(IWebDriver driver, IDataStorage storage,IParsingTemplate template)
    {
        Driver = driver;
        ParsingTemplate = template;
        DataStorage = storage;
    }
}