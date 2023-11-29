class RPG : Game
{
    public string Type { get; set; }
    public bool CharacterCreator { get; set; }

    // Constructor

    public RPG(string name, string platform, int releaseYear, bool multiplayer, string type, bool characterCreator) : base(name, platform, releaseYear, multiplayer)
    {
        Type = type;
        CharacterCreator = characterCreator;
    }


    public override void ShowInfo()
    {
        base.ShowInfo();
        Console.WriteLine($"Type : {Type}");
        Console.WriteLine($"Character Creator : {CharacterCreator}");

    }

}