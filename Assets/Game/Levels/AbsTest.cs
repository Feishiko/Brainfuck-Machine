using System.Collections;
using System.Collections.Generic;
using System;

public class AbsTest : ILevel
{
        public int[,] inputs { get; set; } = new int[6, 100];
    public int[,] outputs { get; set; } = new int[6, 100];
    public string name { get; set; } = "ABS";
    public string rule { get; set; } = "Print the absolute value of first input minus second input.";
    public string story { get; set; } = @"Hi,

ABS is a very common function in many programming language, and math.

They solve many problems in our daily life.

Such like we need flip the minus part of sin function.

Lili Li";

    public void Method()
    {
        for (var iter = 0; iter < 100; iter++)
        {
            var rand = new Random(11121 + iter*2);
            inputs[0, iter] = rand.Next(0, 8);
            var rands = new Random(19121 + iter*2);
            inputs[1, iter] = rands.Next(0, 8);
            outputs[0, iter] = Math.Abs(inputs[0, iter] - inputs[1, iter]);
            for (var j = 2; j < 6; j++)
            {
                inputs[j, iter] = -1;
                outputs[j, iter] = -1;
            }
            outputs[1, iter] = -1;
        }
    }
}
