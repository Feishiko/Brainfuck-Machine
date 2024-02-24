using System.Collections;
using System.Collections.Generic;
using System;

public class BiggerTest : ILevel
{
    public int[,] inputs { get; set; } = new int[6, 100];
    public int[,] outputs { get; set; } = new int[6, 100];
    public string name { get; set; } = "Bigger";
    public string rule { get; set; } = "If first number is bigger than the second, print 255, else, print 0.";
    public string story { get; set; } = @"Hello,

This is another true and false test, maybe it's too easy for you?

John Smith";
    public void Method()
    {
        for (var iter = 0; iter < 100; iter++)
        {
            var rand = new Random(890412 + iter * 2);
            inputs[0, iter] = rand.Next(0, 20);
            var rands = new Random(281378 + iter * 2);
            inputs[1, iter] = rands.Next(0, 20);
            outputs[0, iter] = inputs[0, iter] > inputs[1, iter] ? 255 : 0;
            for (var j = 2; j < 6; j++)
            {
                inputs[j, iter] = -1;
                outputs[j, iter] = -1;
            }
            outputs[1, iter] = -1;
        }
    }
}
