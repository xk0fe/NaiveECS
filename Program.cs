using NaiveECS.Tests;


Console.WriteLine("Creating world...");
var test = new Test();
Console.WriteLine("Creating entity...");
test.CreateEntity();
Console.WriteLine("Getting component of created entity...");
test.GetComponent(0);
Console.WriteLine("Changing component of created entity...");
test.ChangeComponent(0, "Test");
test.GetComponent(0);
test.CreateEntity();
test.RemoveEntity(0);
test.CreateEntity();
test.CreateEntity();
test.RemoveEntity(25);


test.GetAllNames();