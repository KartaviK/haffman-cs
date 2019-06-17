using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Core;

namespace App
{
    internal class Program
    {
        private const string Extension = ".huffman";
        private const string CompressCommand = "--compress";
        private const string DecompressCommand = "--decompress";

        private static string target;
        private static Archive archive;
        private static Compressor compressor = new Compressor();

        private static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("File to compress not given!");

                return;
            }

            if (Array.Exists(args, v => v == CompressCommand))
            {
                Compress(args.TakeWhile(arg => arg != CompressCommand).First());

                return;
            }
            
            if (Array.Exists(args, v => v == DecompressCommand))
            {
                Decompress(args.TakeWhile(arg => arg != DecompressCommand).First());

                return;
            }
        }

        private static void Compress(string target)
        {
            if (!IsFileValid(target))
            {
                Console.WriteLine($"File {target} not exist.");

                return;
            }

            compressor.Compress(
                File.Open(target, FileMode.Open),
                File.Open($"{target}{Extension}", FileMode.Create)
            );
        }

        private static void Decompress(string target)
        {
            if (!IsFileValid(target))
            {
                Console.WriteLine($"File {target} not exist.");

                return;
            }
        }

        private static bool IsFileValid(string target)
        {
            return File.Exists(target);
        }
    }
}
