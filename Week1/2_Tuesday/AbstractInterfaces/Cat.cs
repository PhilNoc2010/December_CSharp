

class Cat : Pet, IRun
{
    public int RunSpeed { get; set; } = 20;
    public Cat(string name) : base(name) { }

    public void Run()
    {
        Console.WriteLine($"{Name} runs away with the speed of {RunSpeed} mph!");
    }

}