using Microsoft.AspNetCore.Mvc;
using PetStoreApi.Models;
[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult<Person>> GetUsers(Person request)
    {
        Person person = new Person
        {
            BirthDate = new DateOnly(2006, 5, 8),
            Name = "Александр",
            MiddleName = "Петрович",
            LastName = "Овечкин"

        };
        if (request != null)
        {
            if (!string.IsNullOrWhiteSpace(request.Name) && request.Name != "string")
                person.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.MiddleName) && request.Name != "string")
                person.MiddleName = request.MiddleName;

            if (!string.IsNullOrWhiteSpace(request.LastName) && request.Name != "string")
                person.LastName = request.LastName;

            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            if (request.BirthDate != today)
                person.BirthDate = request.BirthDate;

            int age = today.Year - person.BirthDate.Year;

            if (person.BirthDate > today.AddYears(-age))
            {
                age--;
            }


            person.Age = age;

        }


        return person;
    }
    
}
