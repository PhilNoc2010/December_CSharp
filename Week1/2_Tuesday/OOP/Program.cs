// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


Game LoL = new Game("League of Legends", "PC", 2009, true);

LoL.ShowInfo();

Game Warcraft = new Game("Warcraft: Orcs And Humans", 1992, true);

Warcraft.ShowInfo();

RPG Fallout3 = new RPG("Fallout 3", "PC, Xbox", 2008, false, "Immersive-Sim", true);

Fallout3.ShowInfo();

LoL.ShowInfo(20);