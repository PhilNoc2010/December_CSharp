
class Game
{
    private string Name;
    public string _Name { get; set; } = "Default Name";

    public List<string> CharacterList { get; set; } = new List<string>();
    private string Platform;
    public string _Platform
    {
        get { return Platform; }
    }
    private int ReleaseYear;
    public int _ReleaseYear
    {
        get { return ReleaseYear; }
        set { if (value > 1980) ReleaseYear = value; }
    }
    private bool Multiplayer;
    public bool _Multiplayer
    {
        get { return Multiplayer; }
    }

    // Constructor
    public Game(string name, string platform, int releaseYear, bool multiplayer)
    {
        Name = name;
        Platform = platform;
        ReleaseYear = releaseYear;
        Multiplayer = multiplayer;
    }

    public Game(string name, int releaseYear, bool multiplayer)
    {
        Name = name;
        ReleaseYear = releaseYear;
        Multiplayer = multiplayer;
    }

    // Methods

    public virtual void ShowInfo()
    {
        Console.WriteLine("------------------------------");

        Console.WriteLine($"Name : {Name}\nPlatform : {Platform}\nRelease Year : {ReleaseYear}");

    }

    public virtual void ShowInfo(int times)
    {
        for (int i = 0; i < times; i++)
        {
            this.ShowInfo();
        }
    }

}