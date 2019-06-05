using System.Collections.Generic;
using NUnit.Framework;
using Core;
using NUnit.Framework.Internal.Builders;

namespace Tests
{
    public class Tree
    {
        private Core.Tree tree;
        
        [SetUp]
        public void Setup()
        {
            tree = new Core.Tree();
        }

        [Test]
        public void TestFill()
        {
            var map = new Dictionary<byte, long>
            {
                { 0, 1 },
                { 1, 1 },
                { 2, 2 },
                { 3, 10 },
                { 4, 15 },
                { 5, 15 },
                { 6, 20 }
            };

            tree.Fill(map);

            Assert.Pass();
        }
    }
}