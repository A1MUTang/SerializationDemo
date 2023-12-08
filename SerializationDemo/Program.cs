using System.Diagnostics;
using RateRouteEngine.Core.Utilities;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("This is a result of serialization performance test:");

        // 创建测试数据
        var person = new Person
        {
            Name = "John Doe",
            Age = 30,
            Email = "john@example.com"
        };

        // 预热（JIT编译优化）
        var dataContractJsonSerializer = JsonHelper.ObjectToJson(person);
        var systemTextJson = System.Text.Json.JsonSerializer.Serialize(person);

        // 测试 DataContractJsonSerializer
        var sw = Stopwatch.StartNew();
        for (int i = 0; i < 10000; i++)
        {
            JsonHelper.ObjectToJson(person);
        }
        sw.Stop();
        Console.WriteLine($"DataContractJsonSerializer: {sw.ElapsedMilliseconds} ms");

        // 测试 System.Text.Json
        sw.Restart();
        for (int i = 0; i < 10000; i++)
        {
            System.Text.Json.JsonSerializer.Serialize(person);
        }
        sw.Stop();
        Console.WriteLine($"System.Text.Json: {sw.ElapsedMilliseconds} ms");
    }
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}
