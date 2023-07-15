namespace Linq
{
  internal class Program
  {

    static void Main(string[] args)
    {
      //Linq, using method Syntax

      //Test();
      //Sorting();
      //SetOperations();
      Filtering();
      //QuantifierOperations();
      //PartitioningData();
      //JoinOperation();
      //GroupJoinOperation();
      //GroupingData();
      //AggregationOperation();
    }

    public static void Test()
    {
      string sentence = "the quick brown fox jumps over the lazy dog";
      string[] words = sentence.Split(' ');

      var query = words
          .GroupBy(w => w.Length, w => w.ToUpper())
          .OrderBy(o => o.Key);

      foreach (var obj in query)
      {
        Console.WriteLine("Words of length {0}:", obj.Key);
        foreach (string word in obj)
          Console.WriteLine(word);
      }
    }

    public static void Sorting()
    {
      string sentence = "the quick brown fox jumps over the lazy dog";
      string[] words = sentence.Split(' ');

      IEnumerable<string> query = words.OrderBy(w => w.Length).ThenBy(w => w.Substring(0, 1));

      foreach (string str in query)
        Console.WriteLine(str);
    }

    public static void SetOperations()
    {
      string sentence = "the quick brown fox jumps over the lazy dog";
      string[] words = sentence.Split(' ');

      IEnumerable<string> query = words.Distinct().Except(new[] { "dog" });

      foreach (string str in query)
        Console.WriteLine(str);
    }

    public static void Filtering()
    {
      string sentence = "the quick brown fox jumps over the lazy dog";
      string[] words = sentence.Split(' ');

      IEnumerable<string> query = words.Where(w => w.Length == 3);

      foreach (string str in query)
        Console.WriteLine(str);
    }

    public static void QuantifierOperations()
    {
      List<Market> markets = new List<Market>
    {
        new Market { Name = "Emily's", Items = new string[] { "kiwi", "cheery", "banana" } },
        new Market { Name = "Kim's", Items = new string[] { "melon", "mango", "olive" } },
        new Market { Name = "Adam's", Items = new string[] { "kiwi", "apple", "orange" } },
    };

      // All example
      // Determine which market have all fruit names length equal to 5
      //IEnumerable<string> names = markets.Where(market => market.Items.All(item => item.Length == 5))
      //                                   .Select(market => market.Name);


      // Any example
      // Determine which market have any fruit names start with 'o'
      //IEnumerable<string> names = markets.Where(market => market.Items.Any(item => item.StartsWith("o")))
      //                                   .Select(market=>market.Name);

      // Contains example
      // Determine which market contains fruit names equal 'kiwi'
      IEnumerable<string> names = markets.Where(market => market.Items.Contains("kiwi"))
                                         .Select(item => item.Name);

      foreach (string name in names)
      {
        Console.WriteLine($"{name} market");
      }
    }

    public static void PartitioningData()
    {
      List<int> numbers = new List<int>();
      for (int i = 0; i < 10; i++) numbers.Add(i);

      int chunkNumber = 1;
      foreach (int[] i in numbers.Chunk(3))
      {
        Console.WriteLine($"Chunk: {chunkNumber++}");
        foreach (int j in i)
          Console.WriteLine(j);
      }
    }

    public static void JoinOperation()
    {
      List<Product> products = new List<Product>
      {
          new Product { Name = "Cola", CategoryId = 0 },
          new Product { Name = "Tea", CategoryId = 0 },
          new Product { Name = "Apple", CategoryId = 1 },
          new Product { Name = "Kiwi", CategoryId = 1 },
          new Product { Name = "Carrot", CategoryId = 2 },
      };

      List<Category> categories = new List<Category>
      {
          new Category { Id = 0, CategoryName = "Beverage" },
          new Category { Id = 1, CategoryName = "Fruit" },
          new Category { Id = 2, CategoryName = "Vegetable" }
      };

      // Join products and categories based on CategoryId
      var query = products.Join(categories, product => product.CategoryId, category => category.Id, (product, category) => new { product.Name, category.CategoryName });

      foreach (var item in query)
      {
        Console.WriteLine($"{item.Name} - {item.CategoryName}");
      }
    }

    public static void GroupJoinOperation()
    {
      List<Product> products = new List<Product>
    {
        new Product { Name = "Cola", CategoryId = 0 },
        new Product { Name = "Tea", CategoryId = 0 },
        new Product { Name = "Apple", CategoryId = 1 },
        new Product { Name = "Kiwi", CategoryId = 1 },
        new Product { Name = "Carrot", CategoryId = 2 },
    };

      List<Category> categories = new List<Category>
    {
        new Category { Id = 0, CategoryName = "Beverage" },
        new Category { Id = 1, CategoryName = "Fruit" },
        new Category { Id = 2, CategoryName = "Vegetable" }
    };

      // Join categories and product based on CategoryId and grouping result
      var productGroups = categories.GroupJoin(products, category => category.Id, product => product.CategoryId, (category,productGroups) => productGroups);
        
        //from category in categories
        //                  join product in products on category.Id equals product.CategoryId into productGroup
        //                  select productGroup;

      foreach (IEnumerable<Product> productGroup in productGroups)
      {
        Console.WriteLine("Group");
        foreach (Product product in productGroup)
        {
          Console.WriteLine($"{product.Name,8}");
        }
      }
    }

    public static void GroupingData()
    {
      List<int> numbers = new List<int>() { 35, 44, 200, 84, 3987, 4, 199, 329, 446, 208 };

      IEnumerable<IGrouping<int, int>> query = numbers.GroupBy(number => number % 2);

      foreach (var group in query)
      {
        Console.WriteLine(group.Key == 0 ? "\nEven numbers:" : "\nOdd numbers:");
        foreach (int i in group)
          Console.WriteLine(i);
      }
    }

    public static void AggregationOperation()
    {
      int[] numbers = { 1, 2, 3, 4, 5 };
      int min = numbers.Min();
      Console.WriteLine($"Min: {min}");
      int max = numbers.Max();
      Console.WriteLine($"Max: {max}");
      int sum = numbers.Sum();
      Console.WriteLine($"Sum: {sum}");
      double average = numbers.Average();
      Console.WriteLine($"Average: {average}");
      int product = numbers.Aggregate((acc, x) => acc * x);
      Console.WriteLine($"Product: {product}");
    }

    // Models
    class Market
    {
      public string Name { get; set; }
      public string[] Items { get; set; }
    }

    class Product
    {
      public string? Name { get; set; }
      public int CategoryId { get; set; }
    }

    class Category
    {
      public int Id { get; set; }
      public string? CategoryName { get; set; }
    }
  }
}