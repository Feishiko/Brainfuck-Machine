using System.Collections;
using System.Collections.Generic;
using System;

public class AndAndAndGateTest : ILevel
{
    public int[,] inputs { get; set; } = new int[6, 100];
    public int[,] outputs { get; set; } = new int[6, 100];
    public string name { get; set; } = "AndAndAndGate";
    public string rule { get; set; } = "Receive 6 inputs, if all inputs are 1, print 1, else, print 0.";
    public string story { get; set; } = @"Hi,

Our cooperative company from ShenZhen need some logic gates. It's not a usual logic gate. It is a mix of 3 AND gates.

That was designed to deal with more complex circuit. General AND gate is everywhere in the market.

Lili Li";

    public void Method()
    {
        for (var iter = 0; iter < 100; iter++)
        {
            var rand = new Random(1145141 + iter);
            var trueNum = 0;
            for (var i = 0; i < 6; i++)
            {
                var rands = new Random(9862521 + iter*2);
                inputs[i, iter] = rand.Next(0, 2);
                if (inputs[i, iter] == 1)
                {
                    trueNum++;
                }
            }
            outputs[0, iter] = trueNum == 6 ? 1 : 0;
            for (var j = 1; j < 6; j++)
            {
                outputs[j, iter] = -1;
            }
        }
    }
}
