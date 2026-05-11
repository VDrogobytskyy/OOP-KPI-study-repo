using System;
using System.Collections.Generic;

// Mediator

interface IAirportMediator
{
    void RequestLanding(Airplane airplane);
    void NotifyGroundServices(Airplane airplane);
}

class AirportControlTower : IAirportMediator
{
    private bool _runwayAvailable = true;

    public void RequestLanding(Airplane airplane)
    {
        Console.WriteLine($"{airplane.Name} requests permission to land.");

        if (_runwayAvailable)
        {
            _runwayAvailable = false;
            Console.WriteLine("Dispatcher: runway is available, landing is allowed.");
            NotifyGroundServices(airplane);
            Console.WriteLine($"{airplane.Name} has landed.");
            _runwayAvailable = true;
        }
        else
        {
            Console.WriteLine("Dispatcher: runway is busy, please wait.");
        }
    }

    public void NotifyGroundServices(Airplane airplane)
    {
        Console.WriteLine($"Ground services: preparing runway, refueling, and technical inspection for {airplane.Name}.");
    }
}

class Airplane
{
    private readonly IAirportMediator _mediator;

    public string Name { get; }

    public Airplane(string name, IAirportMediator mediator)
    {
        Name = name;
        _mediator = mediator;
    }

    public void Land()
    {
        _mediator.RequestLanding(this);
    }
}

class Program
{
    static void Main()
    {
        var tower = new AirportControlTower();

        var plane1 = new Airplane("Boeing 737", tower);
        var plane2 = new Airplane("Airbus A320", tower);

        plane1.Land();
        Console.WriteLine();
        plane2.Land();
    }
}
