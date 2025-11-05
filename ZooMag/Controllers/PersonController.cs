using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductStoreApi.Data;
using ProductStoreApi.Models;
[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<Person>> GetUsers()
    {
        Person person = new Person
        {
            BirthDate = new DateTime(1999, 12, 7),
            Name = "John Doe",
            MiddleName = "Middle",
            LastName = "Smith"

        };
        return person;
    }
    [HttpGet("Age")]
    public async Task<ActionResult<Person>> GetUser1s()
    {
        Person person = new Person
        {
            BirthDate = new DateTime(1999, 12, 7),
            Name = "John Doe",
            MiddleName = "Middle",
            LastName = "Smith"

        };
        DateTime today = DateTime.Today;
        int age = today.Year - person.BirthDate.Year;

        // Adjust the age if the birthday hasn't occurred yet this year
        if (person.BirthDate.Date > today.AddYears(-age))
        {
            age--;
        }

        // The 'age' variable now holds the correct age in years

        person.Age = age;
        return person;
    }
}
    