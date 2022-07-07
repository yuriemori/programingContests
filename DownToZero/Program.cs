using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Solution
{
    static void Main(string[] args)
    {
        int Q = Convert.ToInt32(Console.ReadLine());
        Dictionary<int, int> cache = new Dictionary<int, int>(10000);
        Prime prime = new Prime();

        StringBuilder sb = new StringBuilder();

        // build up cache with most used numbers
        for (int i = 0; i < 10000; ++i)
        {
            int output = int.MaxValue;

            MinOps(i, 0, ref output, prime, cache);

            if (!cache.ContainsKey(i))
                cache.Add(i, output);
        }

        for (int i = 0; i < Q; ++i)
        {
            int input = Convert.ToInt32(Console.ReadLine());
            int output = int.MaxValue;

            MinOps(input, 0, ref output, prime, cache);

            sb.AppendLine(output.ToString());
        }

        Console.WriteLine(sb.ToString());
    }

    static void MinOps(int input, int current, ref int min, Prime prime, Dictionary<int, int> cache)
    {
        if (cache.ContainsKey(input))
        {
            current += cache[input];
            input = 0;
        }

        if (current >= min)
            return;

        if (input == 0)
        {
            if (min > current)
                min = current;

            return;
        }

        if (prime.IsPrime(input))
            MinOps(input - 1, current + 1, ref min, prime, cache);
        else
        {
            int x = 2, y = input;
            bool found = false;

            while (x < y)
            {
                y = input / x;
                if (x * y == input)
                {
                    int inputMax = Math.Max(x, y);
                    MinOps(inputMax, current + 1, ref min, prime, cache);
                    found = true;
                }

                x++;
            }

            if (!found)
                throw new Exception("not found!");

            MinOps(input - 1, current + 1, ref min, prime, cache);
        }
    }
}

public enum CacheStrategy
{
    OnlyPrime,
    All
}

public class Prime
{
    Dictionary<int, bool> cache;

    bool cacheEnabled;
    int cacheSize;
    CacheStrategy strategy;


    public Prime(int cacheSize = 0, CacheStrategy strategy = CacheStrategy.OnlyPrime)
    {
        this.cacheSize = cacheSize;
        this.strategy = strategy;

        cacheEnabled = cacheSize > 0;
        if (cacheEnabled)
            cache = new Dictionary<int, bool>(cacheSize);
    }

    public bool IsPrime(int number)
    {
        if (number < 0)
            throw new Exception("number cannot be negative");
        if (number == 0)
            return false;

        if (cacheEnabled && cache.ContainsKey(number))
            return cache[number];

        int sqrt = (int)Math.Sqrt(number);
        bool prime = true;
        for (int i = 2; i <= sqrt; ++i)
            if (number % i == 0)
            {
                prime = false;
                break;
            }

        if (cacheEnabled && cache.Keys.Count < cacheSize && !cache.ContainsKey(number))
            if (strategy == CacheStrategy.All || prime)
                cache.Add(number, prime);

        return prime;
    }
}