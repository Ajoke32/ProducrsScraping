using OpenQA.Selenium;
using ProductsParser.Abstraction.Data;
using ProductsParser.Abstraction.Templates;


namespace ProductsParser.Abstraction.Options;

public interface IAppParserOptions
{
    public IWebDriver Driver { get; }
    
    public IParsingTemplate ParsingTemplate { get; }
    
    public IDataStorage DataStorage { get; }
}