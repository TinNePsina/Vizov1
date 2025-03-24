using System;
using System.IO;
class Program
{
    public static event Action<User> OnUserCreated = _ => { };
    static void Main()
    {
        OnUserCreated += user =>
        {
            Console.WriteLine($"\nПривет, {user.Name}!");
            
            if (user.Age >= 18)
            {
                try
                {
                    File.AppendAllText("users.txt", $"{user.Name} ({user.Age} лет)\n");
                    Console.WriteLine("Ваши данные сохранены.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка записи: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Доступ запрещён!.");
            }
        };
        try
        {
            OnUserCreated.Invoke(GetUser());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    static User GetUser()
    {
        return new User
        {
            Name = ReadValidName("Введите имя: ", s => !string.IsNullOrWhiteSpace(s)),
            Age = ReadValidAge("Введите возраст: ")
        };
    }
    static string ReadValidName(string prompt, Func<string, bool> check)
    {
        string? input;
        do 
        {
            Console.Write(prompt);
            input = Console.ReadLine();
        } while (!check(input ?? ""));
        
        return input!.Trim();
    }
    static int ReadValidAge(string prompt)
    {
        int result;
        string? input;
        do 
        {
            Console.Write(prompt);
            input = Console.ReadLine();
        } while (!int.TryParse(input, out result));
        
        return result;
    }
}
public class User
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
}