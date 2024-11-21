using System;
using System.Collections.Generic;

public class DictionaryRepository<TKey, TValue> where TKey : IComparable
{
    private Dictionary<TKey, TValue> items = new Dictionary<TKey, TValue>();

    public void Add(TKey id, TValue item)
    {
        if (items.ContainsKey(id))
        {
            Console.WriteLine($"Error: Key {id} already exists.");
            return;
        }
        items[id] = item;
        Console.WriteLine($"Item with key {id} added successfully.");
    }

    public TValue Get(TKey id)
    {
        if (items.TryGetValue(id, out TValue value))
        {
            return value;
        }
        throw new KeyNotFoundException($"Key {id} not found.");
    }

    public void Update(TKey id, TValue newItem)
    {
        if (!items.ContainsKey(id))
        {
            Console.WriteLine($"Error: Key {id} does not exist.");
            return;
        }
        items[id] = newItem;
        Console.WriteLine($"Item with key {id} updated successfully.");
    }

    public void Delete(TKey id)
    {
        if (items.Remove(id))
        {
            Console.WriteLine($"Item with key {id} deleted successfully.");
        }
        else
        {
            Console.WriteLine($"Error: Key {id} does not exist.");
        }
    }
}

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }

    public override string ToString()
    {
        return $"ProductId: {ProductId}, ProductName: {ProductName}";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var productRepository = new DictionaryRepository<int, Product>();

        productRepository.Add(1, new Product { ProductId = 1, ProductName = "Laptop" });
        productRepository.Add(2, new Product { ProductId = 2, ProductName = "Phone" });

        try
        {
            var product = productRepository.Get(1);
            Console.WriteLine("Retrieved: " + product);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }

        productRepository.Update(2, new Product { ProductId = 2, ProductName = "Smartphone" });

        productRepository.Delete(1);

        productRepository.Delete(3);
    }
}

