using ProductsParser.Models;

namespace ProductsParser.Abstraction.Templates;

public interface IParsingTemplate
{
    public void StartParing();

    public ICollection<Product> GetProducts();
}