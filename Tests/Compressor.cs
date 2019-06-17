using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NUnit.Framework;

namespace Tests
{
    public class Compressor
    {
        private Core.Compressor compressor;

        [SetUp]
        public void Setup()
        {
            compressor = new Core.Compressor();
        }

        [Test]
        public void Compress()
        {
            FileStream input = LoremIpsum(15000);
            var output = new MemoryStream();

            var archive = compressor.Compress(input, output);

            Assert.Pass();
        }

        private FileStream LoremIpsum(int size)
        {
            return File.Open($"./LoremIpsum{size}.txt", FileMode.Open, FileAccess.ReadWrite);
        }
    }
}
