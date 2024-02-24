using System;
using System.Collections;
using System.Collections.Generic;
public class SimpleLoopTest : ILevel
{
    public int[,] inputs { get; set; } = new int[6, 100];
    public int[,] outputs { get; set; } = new int[6, 100];
    public string name { get; set; } = "Simple Loop";
    public string rule { get; set; } = "Receive an input, and output the number decrease to 0";
    public string story { get; set; } = @"Hello,

This will be your first time to use a loop, you know how to use it properly.

Don't forget our company has a coffee machine, if you are feeling tired, you can go there and take a sip. If you don't like coffee, the refrigerator has milk.

John Smith";

    public void Method()
    {
        for (var iter = 0; iter < 100; iter++)
        {
            var rand = new Random(1145141 + iter);
            inputs[0, iter] = rand.Next(0, 6);
            var o = 0;
            for (var i = inputs[0, iter]; i >= 0; i--)
            {
                outputs[o, iter] = i;
                o++;
            }
            for (var oo = o; oo < 6; oo++)
            {
                outputs[oo, iter] = -1;
            }
            for (var j = 1; j < 6; j++)
            {
                inputs[j, iter] = -1;
            }
        }
    }
}
