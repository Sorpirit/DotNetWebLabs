using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MyCollections.Test
{
    public class BinaryTreeTest
    {
        [Test]
        public void ItemAdd()
        {
            BinaryTree<int> tree = new() { 1, 2, -10 };
            Assert.True(tree.Contains(1));
            Assert.True(tree.Contains(-10));
            Assert.False(tree.Contains(3));
        }

        [Test]
        public void ItemSearch()
        {
            BinaryTree<int> tree = new() { 1, 2, -10 };
            Assert.True(tree.Contains(1));
            Assert.True(tree.Contains(-10));
            Assert.False(tree.Contains(3));

            Assert.True(tree.TryGetNode(2, out BinaryTree<int>.Node<int>? node));
            Assert.NotNull(node);
            Assert.AreEqual(2, node.Value);

            Assert.False(tree.TryGetNode(6, out node));
            Assert.Null(node);
        }

        [Test]
        public void ClearTest()
        {
            BinaryTree<int> tree = new() { 1, 2, -10 };
            Assert.True(tree.Contains(1));
            
            tree.Clear();
            
            Assert.False(tree.Contains(1));
            Assert.Catch<InvalidOperationException>(() =>
            {
                int t = tree.Max;
            });
        } 

        [Test]
        public void ItemMinMax()
        {
            BinaryTree<int> tree = new() { 1, 2, -10 };
            Assert.AreEqual(-10, tree.Min);
            Assert.AreEqual(2, tree.Max);

            tree.Add(5);
            tree.Add(8);
            tree.Add(6);
            tree.Add(-11);
            tree.Add(-1);

            Assert.AreEqual(-11, tree.Min);
            Assert.AreEqual(8, tree.Max);

            tree = new BinaryTree<int>();

            Assert.Catch<InvalidOperationException>(() =>
            {
                int t = tree.Max;
            });
            Assert.Catch<InvalidOperationException>(() =>
            {
                int t = tree.Min;
            });
        }

        [Test]
        public void CollisionException()
        {
            BinaryTree<int> tree = new() { 1, 2, -10 };
            Assert.Catch<ArgumentException>(() => tree.Add(1));
        }

        [Test]
        public void AddCallback()
        {
            BinaryTree<int> tree = new() { 1, 2, -10 };
            int itemAdd = -1;
            int itemSearch = -1;
            bool treeClear = false;

            tree.OnItemAdded += i => itemAdd = i;
            tree.OnItemSearched += i => itemSearch = i;
            tree.OnClear += () => treeClear = true;

            tree.Add(5);
            tree.Contains(-4);
            tree.Clear();

            Assert.AreEqual(5, itemAdd);
            Assert.AreEqual(-4, itemSearch);
            Assert.True(treeClear);
        }
        
        [Test]
        public void EnumerationTest()
        {
            BinaryTree<int> tree = new() { 1, 2, -10, -6, -11 };
            Assert.AreEqual(new[]{-11, -6, -10, 2, 1},tree.DFS().ToArray());
            Assert.AreEqual(new[]{1, -10, 2, -11, -6},tree.BFS().ToArray());
            
            BinaryTree<int> tree2 = new();
            Assert.AreEqual(Array.Empty<int>(),tree2.DFS().ToArray());
            Assert.AreEqual(Array.Empty<int>(),tree2.BFS().ToArray());
        }
    }
}