using System;
using System.Collections;
using System.Collections.Generic;

public class SmallTest : ILevel
{
    public int[,] inputs { get; set; } = new int[6, 100];
    public int[,] outputs { get; set; } = new int[6, 100];
    public string name { get; set; } = "Small Test";
    public string rule { get; set; } = "Print every input number and plus one.";
    public string story { get; set; } = @"Hello,

Welcome to the Brainfuck Company! We are a company make solutions with Brainfuck.

This newage language are very simple and neet, they are more stronger than Java C# or any other languages.

Today is your first day to become a part of our big family, and here are some tasks you need to solve them, don't worry they are easy for you.

Thank you for your assistance.

John Smith";
    public void Method()
    {
        for (var iter = 0; iter < 100; iter++)
        {
            var rand = new Random(1145141 + iter);
            inputs[0, iter] = rand.Next(0, 22);
            outputs[0, iter] = inputs[0, iter] + 1;
            for (var j = 1; j < 6; j++)
            {
                inputs[j, iter] = -1;
                outputs[j, iter] = -1;
            }
        }
    }

}
