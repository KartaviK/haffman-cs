using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Core;

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
        public void TestWeightAfterFill()
        {
            var map = InitTestMap();

            tree.Fill(map);

            long weight = 0;
            map.ToList().ForEach(pair => weight += pair.Value);

            Assert.True(weight == tree.Root.Weight);
        }

        [Test]
        public void TestPriceMap()
        {
            var map = InitTestMap();

            tree.Fill(map);

            var price = tree.ToPriceMap();
            
            Assert.True(price.Count() == 7, $"Actual count is {price.Count()}");
            Assert.AreEqual(
                new Dictionary<byte, BitArray>()
                {
                    {0, new BitArray(new bool[6] {false, false, false, false, false, false})},
                    {1, new BitArray(new bool[6] {false, false, false, false, false, true})},
                    {2, new BitArray(new bool[5] {false, false, false, false, true})},
                    {3, new BitArray(new bool[4] {false, false, false, true})},
                    {4, new BitArray(new bool[3] {false, false, true})},
                    {5, new BitArray(new bool[2] {false, true})},
                    {6, new BitArray(new bool[1] {true})},
                },
                price
            );
        }

        private Dictionary<byte, long> InitTestMap()
        {
            return new Dictionary<byte, long>
            {
                {0, 1},
                {1, 1},
                {2, 2},
                {3, 10},
                {4, 15},
                {5, 15},
                {6, 20}
            };
        }
    }
}