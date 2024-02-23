
using System;

List<ToDo> todo = new List<ToDo>(); // List
ToDo td = new ToDo("", DateTime.Now, "", ""); // Constructor initialization
string input;
string dataFilePath = @"C:\Users\shail\Project.txt"; // File location


if (!File.Exists(dataFilePath))
{
    var myFile = File.Create(dataFilePath);
    myFile.Close();
}
else
{
    using (StreamReader sr = new StreamReader(dataFilePath))
    {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split('|');
            todo.Add(new ToDo(parts[0].Trim(), DateTime.Parse(parts[1].Trim()), parts[2].Trim(), parts[3].Trim()));
        }
    }
}
int CompletedListCount = 0;
int ToDoListCount = 0;

foreach (ToDo list in todo)
{
    if (list.Status.ToUpper().Trim() == "COMPLETED")
    {
        CompletedListCount = ++CompletedListCount; 
    }
    else
    {
        ToDoListCount = ++ToDoListCount;               
    }
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine(">>  Welcome to \"ToDo\" List <<");
Console.Write(">> ");
Console.ForegroundColor = ConsoleColor.White;
Console.Write("You have ");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.Write(ToDoListCount);
Console.ForegroundColor = ConsoleColor.White;
Console.Write(" tasks todo and ");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.Write(CompletedListCount);
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine(" tasks are done!\n");
Console.ResetColor();

td.main_menu(); // calling main menu funtion
Console.ResetColor();

int number = td.Readline();

while (true)
{
    switch (number)
    {
        case -1:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid entry. Try again.");
            Console.ResetColor();
            number = td.Readline();
            break;
        case 0:
            td.main_menu();
            number = td.Readline();
            break;
        case 1:
            Console.WriteLine("Please pick an option");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(">> ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("(1) ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("To see the list by Date");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(">> ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("(2) ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("To see the list by Project");
            number = td.Readline();

            if (number == 1)
            {
                todo.Sort(delegate (ToDo sort1, ToDo sort2)
                {
                    // Sort using Due Date
                    if (sort1.Due_date == null && sort2.Due_date == null) return 0;
                    else if (sort1.Due_date == null) return -1;
                    else if (sort2.Due_date == null) return 1;
                    else return sort1.Due_date.CompareTo(sort2.Due_date);
                });
                Console.WriteLine("\n----------------------------------------------------------------------");
                Console.WriteLine(String.Format("Title".PadRight(25) + "|" + "Due Date".PadRight(15) + "|" + "Status".PadRight(15) + "|" + "Project".PadRight(15)));
                Console.WriteLine("----------------------------------------------------------------------");
                foreach (ToDo list in todo)
                {
                    Console.WriteLine(list);
                }
            }
            else if (number == 2)
            {
                todo.Sort(delegate (ToDo sort1, ToDo sort2)
                {
                    // Sort using Project code
                    if (sort1.Project == null && sort2.Project == null) return 0;
                    else if (sort1.Project == null) return -1;
                    else if (sort2.Project == null) return 1;
                    else return sort1.Project.CompareTo(sort2.Project);
                });
                Console.WriteLine("\n----------------------------------------------------------------------");
                Console.WriteLine(String.Format("Title".PadRight(25) + "|" + "Due Date".PadRight(15) + "|" + "Status".PadRight(15) + "|" + "Project".PadRight(15)));
                Console.WriteLine("----------------------------------------------------------------------");
                foreach (ToDo list in todo)
                {
                    Console.WriteLine(list);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid number");
                Console.ResetColor();
            }
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("");
            td.main_menu();
            number = td.Readline();
            break;
        case 2:
            while (true)
            {
                td.Addlist(); // calling add list function to get user input
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Task has been added successfully");
                Console.ResetColor();
                todo.Add(new ToDo(td.Title, td.Due_date, td.Status, td.Project)); // add user input to List

                //Write data from List to text file
                using (StreamWriter sw = new StreamWriter(dataFilePath))
                {
                    foreach (ToDo list in todo)
                    {
                        sw.WriteLine(list);
                    }
                }
                Console.WriteLine("Do you want another ToDo list to be added? Press Y to Continue or Press Q to Save");
                input = Console.ReadLine();

                if (input.ToUpper() == "Q")
                {
                    break;
                }
                else if (input.ToUpper() == "Y")
                {
                    continue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong input received, list has been saved");
                    Console.ResetColor();
                    break;
                }
            }
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("Your ToDo list has been Saved");
            Console.ResetColor();
            td.main_menu();
            number = td.Readline();
            break;
        case 3:
            Console.WriteLine("Please pick an option");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(">> ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("(1) ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("To EDIT the list");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(">> ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("(2) ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("To mark task to COMPLETED");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(">> ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("(3) ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("To REMOVE task");
            number = td.Readline();
            if (number == 1)
            {
                Console.Write("Please enter the \"Title\" of ToDo list you want to ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("UPDATE");
                Console.ResetColor(); 
                input = Console.ReadLine();
                bool CheckIfExist = true;
                // check if Title is present in the list
                CheckIfExist = todo.Contains(new ToDo(td.Title = input, td.Due_date = DateTime.Now, td.Status = "", td.Project = input));
                if (CheckIfExist)
                {
                    // Remove the first matching occurence 
                    todo.Remove(new ToDo(td.Title = input, td.Due_date = DateTime.Now, td.Status = "", td.Project = input));
                    Console.WriteLine("Please provide details to be updated");
                    td.Addlist(); // Update new record
                    todo.Add(new ToDo(td.Title, td.Due_date, td.Status, td.Project));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Task has been updated successfully");
                    Console.ResetColor();
                    //Write data from List to text file
                    using (StreamWriter sw = new StreamWriter(dataFilePath))
                    {
                        foreach (ToDo list in todo)
                        {
                            sw.WriteLine(list);
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There is no matching Title");
                    Console.ResetColor();
                }
            }
            else if (number == 2)
            {
                Console.Write("Please enter the Title of ToDo list you wish to mark as ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("COMPLETED");
                Console.ResetColor();
                input = Console.ReadLine();
                // check if Title is present in the list
                bool CheckIfExist = todo.Contains(new ToDo(td.Title = input, td.Due_date = DateTime.Now, td.Status = "", td.Project = input));
                if (CheckIfExist)
                {
                    todo.Add(new ToDo(td.Title = input, td.Due_date = td.Due_date, td.Status = "COMPLETED", td.Project = td.Project));
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine("Task has been updated successfully");
                    Console.ResetColor();
                    todo.Remove(new ToDo(td.Title = input, td.Due_date = DateTime.Now, td.Status = "", td.Project = input));
                    //Write data from List to text file
                    using (StreamWriter sw = new StreamWriter(dataFilePath))
                    {
                        foreach (ToDo list in todo)
                        {
                            sw.WriteLine(list);
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There is no matching Title");
                    Console.ResetColor();
                }
            }
            else if (number == 3)
            {
                Console.Write("Please enter the Title of ToDo list you want to ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("DELETE");
                Console.ResetColor();
                input = Console.ReadLine();
                bool CheckIfExist = todo.Contains(new ToDo(td.Title = input, td.Due_date = DateTime.Now, td.Status = "", td.Project = input));
                if (CheckIfExist)
                {
                    //Delete the first matching record
                    todo.Remove(new ToDo(td.Title = input, td.Due_date = DateTime.Now, td.Status = "", td.Project = input));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Your task has been DELETED");
                    Console.ResetColor();

                    //Write data from List to text file
                    using (StreamWriter sw = new StreamWriter(dataFilePath))
                    {
                        foreach (ToDo list in todo)
                        {
                            sw.WriteLine(list);
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There is no matching Title");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid number");
                Console.ResetColor();
            }
            Console.WriteLine("");
            td.main_menu();
            number = td.Readline();
            break;
        case 4:
            Console.WriteLine("Your ToDo list has been saved");
            Console.WriteLine("");
            td.main_menu();
            number = td.Readline();
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("Please enter a valid number");
            Console.ResetColor();
            number = td.Readline();
            break;
    }
    if (number == 4)
        break;
}

public class ToDo : IEquatable<ToDo>
{
    // Constructor definition
    public ToDo(string title, DateTime due_date, string status, string project)
    {
        Title = title;
        Due_date = due_date;
        Status = status;
        Project = project;
    }
    public string Title { get; set; }
    public DateTime Due_date { get; set; }
    public string Status { get; set; }
    public string Project { get; set; }
    //Main menu funtion
    public void main_menu()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(">> ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Please pick an option from the Menu:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(">> ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("(1) ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Show tasks list (By Date or Project)");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(">> ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("(2) ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Add new task");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(">> ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("(3) ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Edit task (Update, Mark as complete, Remove task)");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(">> ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("(4) ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Save and Quit");
    }
    // Read user selection and return integer
    public int Readline()
    {
        try
        {
            return Convert.ToInt32(Console.ReadLine());
        }
        catch (Exception)
        {
            return -1;
        }
    }
    //Get user input
    public void Addlist()
    {
        string input;
        Console.WriteLine("Enter Task Title");
        input = Console.ReadLine();
        Title = input;

    date:
        Console.WriteLine("Enter Due date for this task (DD-MM-YYY)");
        input = Console.ReadLine();
        // Validate if date format is correct
        try { Due_date = DateTime.Parse(input); }
        catch (FormatException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Date format is not valid, Please enter in (DD-MM-YYY) format");
            Console.ResetColor();
            goto date;
        };

    status:
        Console.WriteLine("Enter current status (New / Inprogress / Completed)");
        input = Console.ReadLine();
        //Validate if Status is correct
        if (input.ToUpper() == "NEW" || input.ToUpper() == "INPROGRESS" || input.ToUpper() == "COMPLETED")
            Status = input.ToUpper();
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please select status from the given options(New / Inprogress / Completed)");
            Console.ResetColor();
            goto status;
        }

        Console.WriteLine("Enter the Project name for this task");
        input = Console.ReadLine();
        Project = input;
    }
    public override string ToString()
    {
        return Title.PadRight(25) + "|" + Due_date.ToString("dd-MM-yyyy").PadRight(15) + "|" + Status.PadRight(15) + "|" + Project.PadRight(15);
    }
    public bool Equals(ToDo other)
    {
        if (other == null) return false;
        return (this.Title.Equals(other.Title));
    }
}


