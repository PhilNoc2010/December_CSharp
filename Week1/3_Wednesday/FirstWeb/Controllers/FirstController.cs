
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

    [HttpGet("{**path}")]
    public string NoPage()
    {
        return "Oops! This page does not exist! 👻";
    }

}