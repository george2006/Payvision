// <copyright file="PositiveBitCounter.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Algorithms.CountingBits
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Xml.Schema;

    /// <summary>
    /// No so happy with my implementation, telling you the truth 
    /// I had no idea of how to get a binary representation of a integer wit bit operators.
    /// Sorry very much. I just say it works!!!
    /// </summary>
    public class PositiveBitCounter
    {
        private const char oneByte = '1';
        public IEnumerable<int> Count(int input)
        {
            if (input < 0)
                throw new ArgumentException($"{nameof(input)} must be greather than zero");

            return IntToBinaryString(input);
        }

        private IEnumerable<int> IntToBinaryString(int number)
        {
            // Calculate the reversed binary representation of number.

            string binary = ReversedBinaryRepresentationOfNumber(ref number);

            IEnumerable<int> result = PositiveBytesNumberAndPositiveBytesPositions(binary);
            return result;
        }

        private string ReversedBinaryRepresentationOfNumber(ref int number)
        {
            const int mask = 1;
            var binary = string.Empty;

            // Calculate the reversed binary representation of number.

            while (number > 0)
            {
                binary = binary + (number & mask);
                number = number >> 1;
            }

            return binary;
        }

        private IEnumerable<int> PositiveBytesNumberAndPositiveBytesPositions(string binary) 
        {
            List<int> positiveBytePositions = new List<int>();
            int positiveByteCount = 0;
            int position = 0;
            
            foreach (char character in binary)
            {
                // Count positive byte number.
                // Remember position of positive bytes.
                if (character == oneByte)
                {
                    positiveByteCount += 1;
                    positiveBytePositions.Add(position);
                }
                position += 1;
            }

            int resultLength = (1 + positiveBytePositions.Count());
            List<int> result = new List<int>(resultLength);
            result.Add(positiveByteCount);
            result.AddRange(positiveBytePositions);
            return result;
        }
        
    }
   
}
