using System;
using System.Collections.Generic;
using System.Threading;

public interface IVideo
{
    void DisplayName();
    void PlayFullVideo(string userRole);
}

public class RealVideo : IVideo
{
    private string _title;

    public RealVideo(string title)
    {
        _title = title;
        LoadAllHeavyData();
    }

    private void LoadAllHeavyData()
    {
        Console.WriteLine($"Loading heavy data for '{_title}'");
        Thread.Sleep(1000);
        Console.WriteLine($"Data for '{_title}' loaded.");
    }

    public void DisplayName() => Console.WriteLine($"Displaying video names: {_title}");

    public void PlayFullVideo(string userRole) => Console.WriteLine($"Playing '{_title}'");
}

public class VideoStreamProxy : IVideo
{
    private string _title;
    private bool _premium;
    private RealVideo? _realVideo;

    private static readonly Dictionary<string, RealVideo> _cacheData = new();

    public VideoStreamProxy(string title, bool isPremium)
    {
        _title = title;
        _premium = isPremium;
    }

    public void DisplayName()
    {
        Console.WriteLine($"[Proxy] Video name: {_title}");
    }

    public void PlayFullVideo(string userRole)
    {
        Console.WriteLine($"\n[Proxy] View request: '{_title}'");

        if (_premium && userRole != "Premium")
        {
            Console.WriteLine($"[Proxy] REFUSAL: Limited access to '{_title}'.");
            return;
        }

        if (!_cacheData.ContainsKey(_title))
        {
            Console.WriteLine($"[Proxy] Object '{_title}' creating(wasn`t in cache)");
            _realVideo = new RealVideo(_title);
            _cacheData[_title] = _realVideo;
        }
        else
        {
            Console.WriteLine($"[Proxy] Object '{_title}' found in cache.");
            _realVideo = _cacheData[_title];
        }

        _realVideo.PlayFullVideo(userRole);
    }
}

class Program
{
    static void Main()
    {
        List<IVideo> catalog = new List<IVideo>
        {
            new VideoStreamProxy("Harry potter", true),
            new VideoStreamProxy("Intestellar", false),
            new VideoStreamProxy("Avatar", true),
            new VideoStreamProxy("Need for speed", false)
        };

        Console.WriteLine("Catalog:");
        foreach (var movie in catalog) movie.DisplayName();

        catalog[0].PlayFullVideo("Guest");

        catalog[0].PlayFullVideo("Premium");

        catalog[0].PlayFullVideo("Premium");

        catalog[3].PlayFullVideo("Guest");
    }
}