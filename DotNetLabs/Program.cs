using System;
using MyCollections;

BinaryTree<int> tree = new();

tree.Add(5);
tree.Add(1);
tree.Add(2);
tree.Add(3);
tree.Add(4);

Console.WriteLine($"{tree.Contains(4)}");
Console.WriteLine($"{tree.Contains(6)}");
Console.WriteLine($"{tree.Contains(5)}");
Console.WriteLine($"{tree.Contains(1)}");
Console.WriteLine($"{tree.Max}");
Console.WriteLine($"{tree.Min}");

tree.Add(12);
tree.Add(-3);
tree.Add(17);

Console.WriteLine($"{tree.Contains(12)}");
Console.WriteLine($"{tree.Contains(-3)}");
Console.WriteLine($"{tree.Max}");
Console.WriteLine($"{tree.Min}");

foreach (var i in tree.BFS())
{
    Console.Write(" " + i);
}

tree.Clear();