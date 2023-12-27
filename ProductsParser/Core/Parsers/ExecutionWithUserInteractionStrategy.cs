using OpenQA.Selenium;
using ProductsParser.Abstraction.ExecutionStrategy;
using ProductsParser.Abstraction.Output;

namespace ProductsParser.Core.Parsers;

public class ExecutionWithUserInteractionStrategy:IExecutionStrategy
{
    private readonly IWebDriver _driver;

    private readonly IOutputHelper _outputHelper;
    
    public ExecutionWithUserInteractionStrategy(IWebDriver driver, IOutputHelper helper)
    {
        _driver = driver;
        _outputHelper = helper;
    }
    public void Parse()
    {
        _outputHelper.Show("Wait until you page loaded and click any button");
        _outputHelper.ClickButtonWait();
    }
}