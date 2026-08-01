Console.Write("Enter your name: ");
string? name = Console.ReadLine();

// Console.WriteLine($"Welcome {name}");

Console.Write("Enter your age: ");
string? ageInput = Console.ReadLine();

int age = Convert.ToInt32(ageInput);

Console.WriteLine($"\nWelcome {name!.ToUpper()}");

 if (age < 18)
    {
        Console.WriteLine("Under 18!! Not eligble for DL");
    }
    else
    {
        Console.WriteLine("Congrats!! You eligble for DL");
    }

string[] courses =
{
    "C#",
    "Angular",
    "React",
    "Python"
};

Array.Sort(courses);

Console.WriteLine("\nAvailable Courses:");

foreach (string course in courses)
{
    if (course == "Python")
    {
        Console.WriteLine($"{course} - New Course");
    }
    else
    {
        Console.WriteLine($"{course} - Existing Course");
    }
}

Console.WriteLine("\nStudent Results");
Console.WriteLine("----------------");

// Array
int[] marks = { 85, 45, 72, 90, 30 };

// foreach loop
foreach (int mark in marks)
{
    // if statement
    if (mark >= 50)
    {
        Console.WriteLine($"Mark: {mark} - Pass");
    }
    else
    {
        Console.WriteLine($"Mark: {mark} - Fail");
    }
}