// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!\n");

List<string> foods = new List<string> { "Nut", "Banana", "Cup of coffee" };

foreach (string food in foods)
{
    string msg = $"I have a {food.ToString()}.\n";
    Console.WriteLine(msg);
}