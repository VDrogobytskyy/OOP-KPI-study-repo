using System;
using System.Collections.Generic;

// Завдання 1. Observer / Спостерігач
// Автор публікує відео, а всі підписники автоматично отримують сповіщення.

interface ISubscriber
{
    void Update(string authorName, string videoTitle);
}

class Subscriber : ISubscriber
{
    private readonly string _name;

    public Subscriber(string name)
    {
        _name = name;
    }

    public void Update(string authorName, string videoTitle)
    {
        Console.WriteLine($"{_name} get notification: {authorName} published video \"{videoTitle}\"");
    }
}

class YouTubeAuthor
{
    private readonly List<ISubscriber> _subscribers = new();

    public string Name { get; }

    public YouTubeAuthor(string name)
    {
        Name = name;
    }

    public void Subscribe(ISubscriber subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void Unsubscribe(ISubscriber subscriber)
    {
        _subscribers.Remove(subscriber);
    }

    public void PublishVideo(string title)
    {
        Console.WriteLine($"\n{Name} upload new video: {title}");
        NotifySubscribers(title);
    }

    private void NotifySubscribers(string title)
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.Update(Name, title);
        }
    }
}

class Program
{
    static void Main()
    {
        var author = new YouTubeAuthor("Kpi news");

        var user1 = new Subscriber("Tanya");
        var user2 = new Subscriber("Vlad");
        var user3 = new Subscriber("Vladimirov");

        author.Subscribe(user1);
        author.Subscribe(user2);
        author.Subscribe(user3);

        author.PublishVideo("New exam tomorrow");
    }
}

