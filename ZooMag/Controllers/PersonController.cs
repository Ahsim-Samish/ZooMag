using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Models;
[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Person>> GetUsers(Person request)
    {
            Person person = new Person();

            // Повторяющиеся проверки
            if (request != null)
            {
                if (request.Name != null && request.Name.Trim() != "" && request.Name != "string")
                {
                    person.Name = request.Name;
                }
                if (request.MiddleName != null && request.MiddleName.Trim() != "" && request.MiddleName != "string")
                {
                    person.MiddleName = request.MiddleName;
                }
                if (request.LastName != null && request.LastName.Trim() != "" && request.LastName != "string")
                {
                    person.LastName = request.LastName;
                }

                // Избыточные вычисления
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                DateOnly birthDate = request.BirthDate;
                if (birthDate != today)
                {
                    person.BirthDate = birthDate;

                    // Дублирование вычислений
                    int yearDiff = today.Year - birthDate.Year;
                    int age = yearDiff;
                    if (birthDate > today.AddYears(-yearDiff))
                    {
                        age = yearDiff - 1;
                    }
                    person.Age = age;
                }
            }
            return person;
    }
    [HttpPost]
    public async Task<ActionResult<Person>> GetUsersNew([FromBody] Person request)
    {
        if (request == null)
            return BadRequest("Request is required.");

        var person = new Person
        {
            Name = NormalizeString(request.Name),
            MiddleName = NormalizeString(request.MiddleName),
            LastName = NormalizeString(request.LastName),
            BirthDate = request.BirthDate
        };

        person.Age = CalculateAge(person.BirthDate);

        return Ok(person);
    }

    // Вынесенные методы (способ 5: вынесение повторяющихся конструкций)
    static string? NormalizeString(string? input) =>
        !string.IsNullOrWhiteSpace(input) && input != "string" ? input.Trim() : null;

    static int CalculateAge(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - birthDate.Year;
        return birthDate > today.AddYears(-age) ? age - 1 : age;
    }

}