using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductStoreApi.Data;
using ProductStoreApi.Models;
[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Person>> GetUsers(Person request)
    {
        Person person = new Person();
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
    [HttpGet]
    public async Task<ActionResult<Person>> Get(Person request)
    {
        return request;
    }

}