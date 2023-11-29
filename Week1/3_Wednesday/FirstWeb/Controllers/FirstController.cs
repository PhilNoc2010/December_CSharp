
using Microsoft.AspNetCore.Mvc;

namespace FirstController.Controllers;

public class FirstController : Controller
{

    [HttpGet]
    [Route("")]
    public string Index()
    {
        return "Hello from the Controller!";
    }

    [HttpGet("pagetwo")]
    public string PageTwo()
    {
        return "The Second Page!";
    }

    // Parameters
    [HttpGet("user/{id}/{food}")]
    public string FavoriteFood(int id, string food)
    {
        return $"User with Id: {id} Their Favorite Food is: {food}";
    }

    // Using a View cshtml File
    [HttpGet("firstview")]
    public ViewResult FirstView()
    {

        ViewBag.Name = "Winter";
        ViewBag.Number = 42;
        ViewBag.Pets = new List<string>() { "Minerva", "Theo" };

        // return View("FirstView");
        return View();
    }




    //! Creating a Form View/Route then using a Post Request Route to submit the form

    [HttpGet("formview")]
    public ViewResult FormView()
    {
        return View();
    }



    //! Pass the data across to a page with a ViewBag



    //* =============================================================================================

    // How to Handle a Redirect with three types:
    //? RedirectResult - return Redirect("view_name");
    // [HttpPost("submitform")]
    // public RedirectResult SubmitForm(string Name, string Animal)
    // {
    //     Console.WriteLine($"Submitted Data : Name : {Name} - Animal : {Animal}");
    //     return Redirect("FormView");
    // }

    //? RedirectToActionResult - return RedirectToAction("action_name");
    // [HttpPost("submitform")]
    // public RedirectToActionResult SubmitForm(string Name, string Animal)
    // {
    //     Console.WriteLine($"Submitted Data : Name : {Name} - Animal : {Animal}");
    //     return RedirectToAction("FormView");
    // }


    //? IActionResult - return any Result/handle different return types. (Views, Actions, etc.)

    [HttpPost("submitform")]
    public IActionResult SubmitForm(string Name, string Animal)
    {
        if (Animal == "Red Panda")
        {
            return View("RedPanda");
        }

        ViewBag.Name = Name;
        ViewBag.Animal = Animal;


        Console.WriteLine($"Submitted Data : Name : {Name} - Animal : {Animal}");
        return View("FormResult");
    }











    [HttpGet("{**path}")]
    public string NoPage()
    {
        return "Oops! This page does not exist! 👻";
    }

}