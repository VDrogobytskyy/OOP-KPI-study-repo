using System;
using System.Collections;
using System.Collections.Generic;

// Iterator

class Employee
{
    public string Name { get; }
    public string Position { get; }
    public bool IsActive { get; }

    public Employee(string name, string position, bool isActive)
    {
        Name = name;
        Position = position;
        IsActive = isActive;
    }

    public override string ToString()
    {
        return $"{Name}, {Position}, active={IsActive}";
    }
}

interface IEmployeeCollection : IEnumerable<Employee>
{
}

class Department : IEmployeeCollection
{
    private readonly List<Employee> _employees = new();

    public void Add(Employee employee)
    {
        _employees.Add(employee);
    }

    public IEnumerator<Employee> GetEnumerator()
    {
        foreach (var employee in _employees)
        {
            yield return employee;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class ProjectTeam : IEmployeeCollection
{
    private readonly Employee[] _members;

    public ProjectTeam(Employee[] members)
    {
        _members = members;
    }

    public IEnumerator<Employee> GetEnumerator()
    {
        for (int index = 0; index < _members.Length; index++)
        {
            yield return _members[index];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void PrintActiveDevelopers(IEmployeeCollection collection)
    {
        foreach (var employee in collection)
        {
            if (employee.IsActive && employee.Position == "Developer")
            {
                Console.WriteLine(employee);
            }
        }
    }

    static void Main()
    {
        var department = new Department();
        department.Add(new Employee("Tanya", "Developer", true));
        department.Add(new Employee("Vlad", "Tester", true));
        department.Add(new Employee("Vladimirow", "Developer", false));

        var projectTeam = new ProjectTeam(new[]
        {
            new Employee("Vova", "Developer", true),
            new Employee("Arkhyp", "Designer", true)
        });

        Console.WriteLine("Active developers in site:");
        PrintActiveDevelopers(department);

        Console.WriteLine("\nActive developers in project team:");
        PrintActiveDevelopers(projectTeam);
    }
}

