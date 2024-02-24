using System.Collections;
using System.Collections.Generic;
using System;

public class Plus2Test : ILevel
{
    public int[,] inputs { get; set; } = new int[6, 100];
    public int[,] outputs { get; set; } = new int[6, 100];
    public string name { get; set; } = "Plus2";
    public string rule { get; set; } = "Receive random numbers, and plus them together as an output.";
    public string story { get; set; } = @"Hello,

This time we need to receive many numbers and then plus them together.

We need to BUILD a plus operation model. And use it on our Brainfuck Computer.

John Smith";

    public void Method()
    {
        for (var iter = 0; iter < 100; iter++)
        {
            var rand = new Random(11141 + iter*2);
            inputs[0, iter] = rand.Next(0, 10);
            var randNum = rand.Next(2, 7);
            for (var i = 0; i < randNum; i++)
            {
                var rands = new Random(87623 + i*3 + iter*2);
                inputs[i, iter] = rands.Next(0, 10);
                outputs[0, iter] += inputs[i, iter];
            }
            for (var j = 1; j < 6; j++)
            {
                outputs[j, iter] = -1;
            }
            for (var k = randNum; k < 6; k++)
            {
                inputs[k, iter] = -1;
            }
        }
    }
}
