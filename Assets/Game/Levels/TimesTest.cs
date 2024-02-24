using System.Collections;
using System.Collections.Generic;
using System;

public class TimesTest : ILevel
{
    public int[,] inputs { get; set; } = new int[6, 100];
    public int[,] outputs { get; set; } = new int[6, 100];
    public string name { get; set; } = "Multiplier";
    public string rule { get; set; } = "Receive two numbers. Print the first value times the second value.";
    public string story { get; set; } = @"Hello,

Our guest need a large number of multiplier. They need to amplify some singal.

John Smith";

    public void Method()
    {
        for (var iter = 0; iter < 100; iter++)
        {
            var rand = new Random(115141 + iter*2);
            inputs[0, iter] = rand.Next(0, 20);
            var rands = new Random(871415 + iter*2);
            inputs[1, iter] = rands.Next(0, 5);
            outputs[0, iter] = inputs[0, iter] * inputs[1, iter];
            for (var j = 2; j < 6; j++)
            {
                inputs[j, iter] = -1;
                outputs[j, iter] = -1;
            }
            outputs[1, iter] = -1;
        }
    }
}
