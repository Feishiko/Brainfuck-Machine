using System.Collections;
using System.Collections.Generic;
using System;

public class BitTest : ILevel
{
    public int[,] inputs { get; set; } = new int[6, 100];
    public int[,] outputs { get; set; } = new int[6, 100];
    public string name { get; set; } = "Big Project";
    public string rule { get; set; } = "Turn 1 to 0, Turn 0 to 1";
    public string story { get; set; } = @"Hello,

Our guest need us to make a reverse model for an important machine!

This time we have many input, do the same way as you did before!

Remember, if there was no input numbers, the input command will not working.

John Smith";

    public void Method()
    {
        for (var iter = 0; iter < 100; iter++)
        {
            var rand = new Random(451498 + iter*3);
            var randNum = rand.Next(0, 7);
            for (var i = 0; i < randNum; i++)
            {
                var rands = new Random(114112 + iter + i);
                inputs[i, iter] = rands.Next(0, 2);
                outputs[i, iter] = inputs[i, iter] == 1 ? 0 : 1;
            }
            for (var j = randNum; j < 6; j++)
            {
                inputs[j, iter] = -1;
                outputs[j, iter] = -1;
            }
        }
    }
}
