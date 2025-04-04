using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YourApp.DALL.Repositories;
using YourApp.DALL.Models;
using YourApp.DALL;

var services = new ServiceCollection();


services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SchoolDb;Integrated Security=True;Connect Timeout=30;"));


services.AddScoped<StudentRepository>();

var provider = services.BuildServiceProvider();
var repo = provider.GetRequiredService<StudentRepository>();


bool exit = false;
while (!exit)
{
    Console.WriteLine("\n1. Додати студента\n2. Всі студенти\n3. Оновити\n4. Видалити\n5. Пошук за Id\n6. Вийти");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Ім'я: ");
            string firstName = Console.ReadLine()!;
            Console.Write("Прізвище: ");
            string lastName = Console.ReadLine()!;
            Console.Write("Дата народження (yyyy-mm-dd): ");
            DateTime dob = DateTime.Parse(Console.ReadLine()!);
            Console.Write("Email: ");
            string email = Console.ReadLine()!;
            repo.Add(new Student { FirstName = firstName, LastName = lastName, DateOfBirth = dob, Email = email });
            break;

        case "2":
            var students = repo.GetAll();
            foreach (var s in students)
                Console.WriteLine($"{s.Id}: {s.FirstName} {s.LastName} ({s.DateOfBirth.ToShortDateString()}), Email: {s.Email}");
            break;

        case "3":
            Console.Write("ID студента: ");
            int idToUpdate = int.Parse(Console.ReadLine()!);
            var studentToUpdate = repo.GetById(idToUpdate);
            if (studentToUpdate != null)
            {
                Console.Write("Нове ім'я: ");
                studentToUpdate.FirstName = Console.ReadLine()!;
                Console.Write("Нове прізвище: ");
                studentToUpdate.LastName = Console.ReadLine()!;
                Console.Write("Нова дата нар.: ");
                studentToUpdate.DateOfBirth = DateTime.Parse(Console.ReadLine()!);
                Console.Write("Новий email: ");
                studentToUpdate.Email = Console.ReadLine()!;
                repo.Update(studentToUpdate);
            }
            else Console.WriteLine("Не знайдено.");
            break;

        case "4":
            Console.Write("ID для видалення: ");
            int idToDelete = int.Parse(Console.ReadLine()!);
            repo.Delete(idToDelete);
            break;

        case "5":
            Console.Write("ID для пошуку: ");
            int idToFind = int.Parse(Console.ReadLine()!);
            var found = repo.GetById(idToFind);
            if (found != null)
                Console.WriteLine($"{found.FirstName} {found.LastName}, {found.Email}, {found.DateOfBirth}");
            else Console.WriteLine("Не знайдено.");
            break;

        case "6":
            exit = true;
            break;
    }
}
