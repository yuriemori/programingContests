﻿using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'compareTriplets' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY a
     *  2. INTEGER_ARRAY b
     */

    public static List<int> compareTriplets(List<int> AlicesTriplets, List<int> BobsTriplets)
    {
        var scores = new List<int> { 0, 0 };

        for (int i = 0; i < 3; i++)
        {
            if (AlicesTriplets[i] > BobsTriplets[i])
            {
                scores[0] ++;
            }
            if (BobsTriplets[i] > AlicesTriplets[i])
            {
                scores[1] ++;
            }
        }
        
        return scores;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        List<int> AlicesTriplets = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(aTemp => Convert.ToInt32(aTemp)).ToList();

        List<int> BobsTriplets = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(bTemp => Convert.ToInt32(bTemp)).ToList();

        List<int> result = Result.compareTriplets(AlicesTriplets, BobsTriplets);

        Console.WriteLine(String.Join(" ", result));

        //textWriter.Flush();
        //textWriter.Close();
    }
}
