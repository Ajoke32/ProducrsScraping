using ProductsParser.Abstraction.Output;

namespace ProductsParser.Core.OutputHelpers;

public class ConsoleOutputHelper:IOutputHelper
{
    public void Show(string message)
    {
        Console.WriteLine(message);
    }

    public void ClickButtonWait()
    {
        Console.ReadKey();
    }
}