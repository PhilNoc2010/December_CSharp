// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello! 🌻");

//! Goals: 
//? Build an abstract base class for Pet.
//? Create some inheriting classes: Cat, Bird, Alligator
//? Create Interfaces for IRun, IFly, ISwim
// *******************************************************************
//? Cat will use IRun
//? Bird wil use IFly and IRun
//? Alligator will use ISwim and IRun

//! You can not make an instance of an abstract class
// Pet cat = new Pet("Minerva");

Cat oneCat = new Cat("Mabel");
Cat twoCat = new Cat("Dutchess");

// oneCat.Run();
// twoCat.Run();

Bird oneBird = new Bird("Greater Roadrunner");

// oneBird.Fly();
// oneBird.Run();


List<Pet> AllPets = new() { oneCat, twoCat, oneBird };

// foreach (Pet pet in AllPets)
// {
//     if (pet is Cat c)
//     {
//         c.Run();
//     }
// }


foreach (Pet pet in AllPets)
{
    if (pet is IRun r)
    {
        r.Run();
    }

    if (pet is IFly f)
    {
        f.Fly();
    }

}