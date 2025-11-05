namespace ProductStoreApi.Models;

public class Person
{
    public string Name { get; set; } = "Иван";
    public string MiddleName { get; set; } = "Иванов";
    public string LastName { get; set; } = "Пожарный";
    public DateOnly BirthDate { get; set; } = new DateOnly(1985, 8, 12);
    public int Age { get; set; } = 0;

}
