using ProductsParser.Models;

namespace ProductsParser.Abstraction.Data;


public interface IDataStorage
{
    public string Path { get; }
    
    public void Save(ICollection<Product> products);
}
