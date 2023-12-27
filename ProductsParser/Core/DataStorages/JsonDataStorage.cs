using System.Text.Encodings.Web;
using System.Text.Json;
using ProductsParser.Abstraction.Data;
using ProductsParser.Models;

namespace ProductsParser.Core.DataStorages;

public class JsonDataStorage:IDataStorage
{
    public string Path { get; }

    public JsonDataStorage(string path = "Data.json")
    {
        Path = path;
    }
    
    public void Save(ICollection<Product> products)
    {

        var json = JsonSerializer.Serialize(products, new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
        
        File.WriteAllText(Path,json);
    }
}