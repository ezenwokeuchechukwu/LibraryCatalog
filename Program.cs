using System;
using System.Collections.Generic;
using System.IO;

struct CatalogStats
{
    public int TotalBooks;
}

class Program
{
    static List<Book> catalog = new List<Book>();
    const string fileName = "catalog.txt";

    static void Main()
    {
        LoadCatalog();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("📚 Library Catalog System");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. View All Books");
            Console.WriteLine("3. Search by Title");
            Console.WriteLine("4. Save & Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddBook(); break;
                case "2": ViewBooks(); break;
                case "3": SearchBooks(); break;
                case "4": SaveCatalog(); return;
                default: Console.WriteLine("Invalid choice."); break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }

    static void AddBook()
    {
        Console.Write("Enter title: ");
        string title = Console.ReadLine();

        Console.Write("Enter author: ");
        string author = Console.ReadLine();

        Console.Write("Enter ISBN: ");
        string isbn = Console.ReadLine();

        Book book = new Book(title, author, isbn);
        catalog.Add(book);
        Console.WriteLine("✅ Book added!");
    }

    static void ViewBooks()
    {
        if (catalog.Count == 0)
        {
            Console.WriteLine("No books in catalog.");
            return;
        }

        CatalogStats stats = new CatalogStats { TotalBooks = catalog.Count };
        Console.WriteLine($"Total Books: {stats.TotalBooks}\n");

        foreach (Book book in catalog)
        {
            Console.WriteLine(book);
        }
    }

    static void SearchBooks()
    {
        Console.Write("Enter search keyword: ");
        string keyword = Console.ReadLine().ToLower();

        var results = catalog.FindAll(b => b.Title.ToLower().Contains(keyword));

        if (results.Count == 0)
        {
            Console.WriteLine("No books found.");
            return;
        }

        foreach (Book book in results)
        {
            Console.WriteLine(book);
        }
    }

    static void SaveCatalog()
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Book book in catalog)
            {
                writer.WriteLine($"{book.Title}|{book.Author}|{book.ISBN}");
            }
        }

        Console.WriteLine("💾 Catalog saved to file.");
    }

    static void LoadCatalog()
    {
        if (!File.Exists(fileName)) return;

        string[] lines = File.ReadAllLines(fileName);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 3)
            {
                Book book = new Book(parts[0], parts[1], parts[2]);
                catalog.Add(book);
            }
        }
    }
}
