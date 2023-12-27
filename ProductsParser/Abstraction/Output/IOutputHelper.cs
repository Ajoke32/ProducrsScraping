namespace ProductsParser.Abstraction.Output;

public interface IOutputHelper
{
    public void Show(string message);

    public void ClickButtonWait();
}