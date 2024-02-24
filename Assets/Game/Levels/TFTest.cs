using System.Collections;
using System.Collections.Generic;
using System;

public class TFTest : ILevel
{
    public int[,] inputs { get; set; } = new int[6, 100];
    public int[,] outputs { get; set; } = new int[6, 100];
    public string name { get; set; } = "True or False";
    public string rule { get; set; } = "If first number equals to second number, print 255, else, print 0.";
    public string story { get; set; } = @"Hi,

Do you know? Brainfuck has boolean. True is 255, False is 0.

Why? Because Loop need a value not equals to 0, and 0 minus 1 equals to 255.

Lili Li";

    public void Method()
    {
        for (var iter = 0; iter < 100; iter++)
        {
            for (var i = 0; i < 2; i++)
            {
                var rand = new Random(11441 + iter*3 + i*2);
                inputs[i, iter] = rand.Next(0, 5);
            }
            outputs[0, iter] = inputs[0, iter] == inputs[1, iter] ? 255 : 0;
            for (var j = 2; j < 6; j++)
            {
                inputs[j, iter] = -1;
                outputs[j, iter] = -1;
            }
            outputs[1, iter] = -1;
        }
    }
}
