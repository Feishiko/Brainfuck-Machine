using System.Collections;
using System.Collections.Generic;
using System;

public class SwapTest : ILevel
{
    public int[,] inputs { get; set; } = new int[6, 100];
    public int[,] outputs { get; set; } = new int[6, 100];
    public string name { get; set; } = "Swap";
    public string rule { get; set; } = "Receive two numbers, swap them, and output.";
    public string story { get; set; } = @"Hi,

Your previous job did so well! So we hopes to see your more excellent code in this task.

Did you know? Output cells and memory cells are in different sequences.

Lili Li";

    public void Method()
    {
        for (var iter = 0; iter < 100; iter++)
        {
            var rand = new Random(1145141 + iter);
            inputs[0, iter] = rand.Next(0, 9);
            inputs[1, iter] = rand.Next(8, 12);
            outputs[0, iter] = inputs[1, iter];
            outputs[1, iter] = inputs[0, iter];
            for (var j = 2; j < 6; j++)
            {
                inputs[j, iter] = -1;
                outputs[j, iter] = -1;
            }
        }
    }
}
