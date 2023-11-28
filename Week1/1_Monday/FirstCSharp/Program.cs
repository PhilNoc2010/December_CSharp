// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

// Console.WriteLine("Another Line of Text!");

string name = "Winter";

// Console.WriteLine($"Hello {name}!");


int[] numArray = new int[5];

int[] numArray2 = new int[] { 1, 2, 3, 4, 5, 6 };

numArray[1] = 39;

// foreach (int num in numArray)
// {
//     Console.WriteLine(num);
// }



// Lists

List<string> games = new List<string>();

games.Add("Stardew Valley");
games.Add("Lethal Company");
games.Add("Rainbow Six");
games.Add("Final Fantasy XIV");
games.Add("Wizard 101");
games.Add("System Shock");

// for (int idx = 0; idx < games.Count; idx++)
// {
//     Console.WriteLine(games[idx]);
// }

games.RemoveAt(3);

// games.ForEach(Console.WriteLine);


// Dictionaries

Dictionary<string, int> pets = new Dictionary<string, int>();

pets.Add("Stella", 2);
pets.Add("Scout", 8);
pets.Add("Roman", 3);
pets.Add("Soup", 3);

// Console.WriteLine(pets["Soup"]);

// foreach (KeyValuePair<string, int> pet in pets)
// {
//     Console.WriteLine($"Name : {pet.Key} - Age : {pet.Value}");
// }




// Functions
/* With dotnet6 and higher, our Program.cs file doesn't *require* the static keyword, 
    since we're inside the "class" of the program. But it's good practice, and prepares us for OOP coming up.
    Refresher on what static methods are: Static methods can be accessed in the class without needing to be attached to an instance of the class.
*/
// void is used when we want a function to not return anything, and instead simply perform an action.
// Otherwise we need to ensure we state what data type is expected to be returned in our function declaration.

static void SayHello()
{
    Console.WriteLine("Hello how are you doing today?");
}

SayHello();


static int AddTwoNums(int num1, int num2 = 20)
{
    return num1 + num2;
}