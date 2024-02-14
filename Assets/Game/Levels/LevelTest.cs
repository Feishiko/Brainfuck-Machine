using System;
using System.Collections;
using System.Collections.Generic;

public class LevelTest : ILevel
{
    public int[,] inputs { get; set; } = new int[1, 100];
    public int[,] outputs { get; set; } = new int[1, 100];
    public string name { get; set; } = "Test Level";
    public string rule { get; set; } = "Turn 1 to 0, Turn 0 to 1";

    public void Method()
    {
        for (var iter = 0; iter < 100; iter++)
        {
            var rand = new Random(10);
            inputs[0, iter] = rand.Next(0, 1);
            outputs[0, iter] = inputs[0, iter] == 1 ? 0 : 1;
        }
    }
}
