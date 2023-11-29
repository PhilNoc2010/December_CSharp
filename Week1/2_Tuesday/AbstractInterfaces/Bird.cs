
class Bird : Pet, IRun, IFly
{
    public int FlySpeed { get; set; } = 26;
    public int RunSpeed { get; set; } = 20;
    public Bird(string name) : base(name) { }

    public void Run()
    {
        Console.WriteLine($"{Name} runs away with the speed of {RunSpeed} mph!");
    }
    public void Fly()
    {
        Console.WriteLine($"{Name} soars in the sky with the speed of {FlySpeed} mph!");
    }
}