using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var word = "---";

            var pos = Possibilities(word);


            foreach (var p in pos)
            {
                Console.WriteLine(p);
            }

            Console.ReadLine();
        }

        public static List<string> Possibilities(string word)
        {
            if (word.Length > 3)
            {
                throw new ArgumentOutOfRangeException("Max length for word is 3");
            }

            var firstNode = GetDecodeTree();

            var possibilitiesNodes = new List<Node>();

            foreach (var c in word)
            {
                switch (c)
                {
                    case '.':
                        DecodeDot(possibilitiesNodes, firstNode);
                        break;
                    case '-':
                        DecodeDash(possibilitiesNodes, firstNode);
                        break;
                    case '?':
                        DecodeNoise(possibilitiesNodes, firstNode);
                        break;
                }

            }

            return possibilitiesNodes.Select(p => p.Value.ToString()).ToList();
        }

        private static void DecodeNoise(List<Node> possibilitiesNodes, Node firstNode)
        {
            if (possibilitiesNodes.Any())
            {
                var possibilitiesNodesClone = CloneList(possibilitiesNodes);
                possibilitiesNodes.Clear();

                foreach (var possibility in possibilitiesNodesClone)
                {
                    possibilitiesNodes.Add(possibility.NextDot);
                    possibilitiesNodes.Add(possibility.NextDash);
                }
            }
            else
            {
                possibilitiesNodes.Add(firstNode.NextDot);
                possibilitiesNodes.Add(firstNode.NextDash);
            }
        }

        private static void DecodeDash(List<Node> possibilitiesNodes, Node firstNode)
        {
            if (possibilitiesNodes.Any())
            {
                for (var index = 0; index < possibilitiesNodes.Count; index++)
                {
                    possibilitiesNodes[index] = possibilitiesNodes[index].NextDash;
                }
            }
            else
            {
                possibilitiesNodes.Add(firstNode.NextDash);
            }
        }

        private static void DecodeDot(List<Node> possibilitiesNodes, Node firstNode)
        {
            if (possibilitiesNodes.Any())
            {
                for (var index = 0; index < possibilitiesNodes.Count; index++)
                {
                    possibilitiesNodes[index] = possibilitiesNodes[index].NextDot;
                }
            }
            else
            {
                possibilitiesNodes.Add(firstNode.NextDot);
            }
        }

        private static List<Node> CloneList(List<Node> possibilitiesNodes)
        {
            return new List<Node>(possibilitiesNodes);
        }

        private static Node GetDecodeTree()
        {
            var nodeS = GetNode('S');
            var nodeU = GetNode('U');

            var nodeR = GetNode('R');
            var nodeW = GetNode('W');

            var nodeD = GetNode('D');
            var nodeK = GetNode('K');

            var nodeG = GetNode('G');
            var nodeO = GetNode('O');

            var nodeI = GetNode('I', nodeS, nodeU);
            var nodeA = GetNode('A', nodeR, nodeW);
            var nodeN = GetNode('N', nodeD, nodeK);
            var nodeM = GetNode('M', nodeG, nodeO);

            var nodeE = GetNode('E', nodeI, nodeA);
            var nodeT = GetNode('T', nodeN, nodeM);

            var nodeInital = GetNode(null, nodeE, nodeT);

            return nodeInital;
        }

        private static Node GetNode(char? charValue, Node nextDot = null, Node nextDash = null)
        {
            return new Node
            {
                Value = charValue,
                NextDot = nextDot,
                NextDash = nextDash,
            };
        }

        private class Node
        {
            public char? Value { get; set; }
            public Node NextDot { get; set; }
            public Node NextDash { get; set; }
        }
    }
}
