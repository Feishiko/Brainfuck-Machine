using System;
using System.Collections;
using System.Collections.Generic;


public class PlusTest : ILevel
{
    public int[,] inputs { get; set; } = new int[6, 100];
    public int[,] outputs { get; set; } = new int[6, 100];
    public string name { get; set; } = "Plus";
    public string rule { get; set; } = "Receive two numbers, and plus them together as an output.";
    public string story { get; set; } = @"Hello,

Brainfuck can do plus operation. I mean plus two numbers together.

And of course we need to plus it one by one.

John Smith";

    public void Method()
    {
        for (var iter = 0; iter < 100; iter++)
        {
            var rand = new Random(115141 + iter*2);
            inputs[0, iter] = rand.Next(0, 10);
            var rands = new Random(871415 + iter*2);
            inputs[1, iter] = rands.Next(0, 10);
            outputs[0, iter] = inputs[0, iter] + inputs[1, iter];
            for (var j = 2; j < 6; j++)
            {
                inputs[j, iter] = -1;
                outputs[j, iter] = -1;
            }
            outputs[1, iter] = -1;
        }
    }
}
