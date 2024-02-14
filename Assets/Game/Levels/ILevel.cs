public interface ILevel
{
    int[,] inputs { get; set; }
    int[,] outputs { get; set; }
    string name { get; set; }
    string rule { get; set; }
    void Method();
}
