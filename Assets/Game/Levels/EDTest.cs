using System.Collections;
using System.Collections.Generic;
using System;

public class EDTest : ILevel
{
public int[,] inputs { get; set; } = new int[6, 100];
    public int[,] outputs { get; set; } = new int[6, 100];
    public string name { get; set; } = "Staff";
    public string rule { get; set; } = "Congrats!";
    public string story { get; set; } = @"Hello,

Congrats!

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
