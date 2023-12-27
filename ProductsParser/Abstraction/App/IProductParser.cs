namespace ProductsParser.Abstraction.App;

public interface IProductParser
{
    public void Run();

    public void Stop();

    public void Restart();
}