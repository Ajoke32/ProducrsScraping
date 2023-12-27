

using OpenQA.Selenium.Chrome;
using ProductsParser.Core;
using ProductsParser.Core.DataStorages;
using ProductsParser.Core.Options;
using ProductsParser.Core.Parsers;
using ProductsParser.Core.Templates;



var driver = new ChromeDriver();

driver.Url = "https://www.add.ua/ua/";

var parsingTemplate = new ParsingByCategoriesTemplate(driver);

var dataStorage = new JsonDataStorage();

var app = new ProductsParserApp(
    new ParserOptions(
            driver,
            dataStorage,
            parsingTemplate
        )
    );

app.ConfigureExecutionStrategy<ExecutionWithoutUserInteractionStrategy>();

app.Run();

Console.ReadKey();

app.Stop();


