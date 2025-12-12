using ProjetPersonnelle;
using System.Text.Json;

class Program
{
    static List<TodoItem> todos = new();
    static string filePath = "todos.json";

    static void Main()
    {
        LoadTodos();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== TODO APP ===");
            Console.WriteLine("1. Ajouter une tâche");
            Console.WriteLine("2. Lister les tâches");
            Console.WriteLine("3. Marquer comme terminée");
            Console.WriteLine("4. Supprimer une tâche");
            Console.WriteLine("0. Quitter");

            Console.Write("Choix : ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddTodo(); break;
                case "2": ListTodos(); break;
                case "3": CompleteTodo(); break;
                case "4": DeleteTodo(); break;
                case "0": SaveTodos(); return;
            }
        }
    }

    static void AddTodo()
    {
        Console.Write("Titre : ");
        string title = Console.ReadLine();

        todos.Add(new TodoItem
        {
            Id = todos.Count + 1,
            Title = title,
            IsDone = false
        });

        SaveTodos();
    }

    static void ListTodos()
    {
        Console.WriteLine("\nTâches :");

        foreach (var todo in todos)
        {
            Console.ForegroundColor = todo.IsDone ? ConsoleColor.Green : ConsoleColor.Yellow;
            Console.WriteLine($"{todo.Id}. [{(todo.IsDone ? "X" : " ")}] {todo.Title}");
            Console.ResetColor();
        }

        Console.ReadKey();
    }


    static void CompleteTodo()
    {
        Console.Write("ID à terminer : ");

        if (!int.TryParse(Console.ReadLine(), out int id))
            return;

        var todo = todos.Find(t => t.Id == id);
        if (todo == null)
            return;

        todo.IsDone = true;
        SaveTodos();
    }


    static void DeleteTodo()
    {
        Console.Write("ID à supprimer : ");

        if (!int.TryParse(Console.ReadLine(), out int id))
            return;

        todos.RemoveAll(t => t.Id == id);
        SaveTodos();
    }


    static void SaveTodos()
    {
        File.WriteAllText(filePath, JsonSerializer.Serialize(todos));
    }

    static void LoadTodos()
    {
        if (File.Exists(filePath))
            todos = JsonSerializer.Deserialize<List<TodoItem>>(File.ReadAllText(filePath));
    }
}
