
public class Product
{
    public int Id { set; get; }
    public string Name { set; get; }
}

public class dude
{
    public static void Main()
    {

        var _products = new List<Product>
        {
            new Product { Id = 1, Name = "Test Product 1" },
            new Product { Id = 2, Name = "Test Product 2" },
            new Product { Id = 3, Name = "Test Product 3" }
        };

        Console.WriteLine(_products.Single(e => e.Id == 33).Name);


    

    }

}
