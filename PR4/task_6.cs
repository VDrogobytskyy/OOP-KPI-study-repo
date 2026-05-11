using System;

// Template Method


abstract class OnlineCourse
{
    public void CompleteCourse()
    {
        WatchLectures();
        DoPractice();
        AdditionalStep();
        PassTest();
        DefendFinalProject();
    }

    protected void WatchLectures()
    {
        Console.WriteLine("Student is studying lectures.");
    }

    protected void DoPractice()
    {
        Console.WriteLine("Student does pracs.");
    }

    protected void PassTest()
    {
        Console.WriteLine("Student does tests.");
    }

    protected void DefendFinalProject()
    {
        Console.WriteLine("Student applying final test.");
    }

    protected virtual void AdditionalStep()
    {
    }
}

class ProgrammingCourse : OnlineCourse
{
    protected override void AdditionalStep()
    {
        Console.WriteLine("Additional: auto code check.");
    }
}

class DesignCourse : OnlineCourse
{
    protected override void AdditionalStep()
    {
        Console.WriteLine("Additional: presentation to proffesor.");
    }
}

class ManagementCourse : OnlineCourse
{
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Programming course:");
        OnlineCourse programming = new ProgrammingCourse();
        programming.CompleteCourse();

        Console.WriteLine("\nDesign course:");
        OnlineCourse design = new DesignCourse();
        design.CompleteCourse();

        Console.WriteLine("\nManagmeng course:");
        OnlineCourse management = new ManagementCourse();
        management.CompleteCourse();
    }
}

