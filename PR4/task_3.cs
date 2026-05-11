using System;

// State

interface IRobotState
{
    void Handle(RobotVacuum robot);
}

class CleaningState : IRobotState
{
    public void Handle(RobotVacuum robot)
    {
        Console.WriteLine("Battery full: robot is cleaning.");
    }
}

class LowBatteryState : IRobotState
{
    public void Handle(RobotVacuum robot)
    {
        Console.WriteLine("Battery low: robot is looking for a charging station.");
    }
}

class StuckState : IRobotState
{
    public void Handle(RobotVacuum robot)
    {
        Console.WriteLine("Robot stucked: NEED HELP :D");
    }
}

class RobotVacuum
{
    private IRobotState _state;

    public RobotVacuum(IRobotState state)
    {
        _state = state;
    }

    public void SetState(IRobotState state)
    {
        _state = state;
    }

    public void Work()
    {
        _state.Handle(this);
    }
}

class Program
{
    static void Main()
    {
        var robot = new RobotVacuum(new CleaningState());
        robot.Work();

        robot.SetState(new LowBatteryState());
        robot.Work();

        robot.SetState(new StuckState());
        robot.Work();
    }
}

