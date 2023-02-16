/*// introduction to linq
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice1
{
    internal class _9febOnwards
    {
        public static void Main()
        {
            List<Employee> employees = DataManager.GetData();
            *//*foreach(Employee emp in employees) {
                Console.WriteLine("ID: " + emp.Id + "Firstname: " + emp.FirstName + " Lastname: " + emp.LastName + " " + emp.Age + " " + emp.Department + " salary " + emp.Salary);
            }*/
/*employees.ForEach(emp =>
{
    Console.WriteLine("ID: " + emp.Id + "Firstname: " + emp.FirstName + " Lastname: " + emp.LastName + " " + emp.Age + " " + emp.Department + " salary " + emp.Salary);
});*//*
//linq queries
*//*  IEnumerable<string> names = employees.Select(e => e.FirstName).Distinct();
  foreach(string name in names)
  {
      Console.WriteLine(name);
  }*/

/*employees = employees.OrderBy(e => e.FirstName).ThenBy(e=>e.LastName).ToList();*/
/* employees = employees.OrderByDescending(e => e.FirstName).ThenByDescending(e => e.LastName).ToList();
 employees.ForEach(emp => Console.WriteLine("ID: " + emp.Id + "Firstname: " + emp.FirstName + " Lastname: " + emp.LastName + " " + emp.Age + " " + emp.Department + " salary " + emp.Salary));*//*

// where clause
*//* employees = employees.Where(e => e.Age > 28 && e.Salary > 20000).ToList();
 employees.ForEach(emp => Console.WriteLine("ID: " + emp.Id + "Firstname: " + emp.FirstName + " Lastname: " + emp.LastName + " " + emp.Age + " " + emp.Department + " salary " + emp.Salary));*/

/*Employee emp = employees.First(e=>e.FirstName=="John");*//* //will throw exception if not found matching entry
*//*Employee emp = employees.FirstOrDefault(e => e.FirstName == "Jdohn");
if (emp != null)
{
    Console.WriteLine("ID: " + emp.Id + "Firstname: " + emp.FirstName + " Lastname: " + emp.LastName + " " + emp.Age + " " + emp.Department + " salary " + emp.Salary);

}
else
{
    Console.WriteLine("not found");
}*//*

//employees = employees.Take(2).ToList();     //takes first two elements
//employees = employees.Skip(2).ToList();     //skips first two elements
//employees = employees.Distinct().ToList;
//employees.ForEach(emp => Console.WriteLine("ID: " + emp.Id + "Firstname: " + emp.FirstName + " Lastname: " + emp.LastName + " " + emp.Age + " " + emp.Department + " salary " + emp.Salary)); 


//boolean returning expressions
Console.WriteLine(employees.Any(e => e.Age > 30));
Console.WriteLine(employees.All(e => e.Age > 30));
Console.WriteLine(employees.Count(e => e.Age > 30));
Console.WriteLine(employees.Sum(e => e.Salary));
Console.WriteLine(employees.Average(e => e.Age));
Console.WriteLine(employees.Max(e => e.Age));

//get min max by particular entry
Console.WriteLine(employees.Min(e=>e.Age));
Employee emp = employees.Where(e=> e.Age ==  employees.Max(em => em.Age)).ToList()[0];
Console.WriteLine("ID: " + emp.Id + "Firstname: " + emp.FirstName + " Lastname: " + emp.LastName + " " + emp.Age + " " + emp.Department + " salary " + emp.Salary);
}
}

internal class Employee
{
public int Id { get; set; }
public string FirstName { get; set; }
public string LastName { get; set; }
public string Department { get; set; }
public int Age { get; set; }
public int Salary { get; set; }
}
internal class DataManager
{
internal static List<Employee> GetData()
{
return new List<Employee>
{
new Employee {Id  = 1,FirstName = "John",LastName = "Smith",Age= 30, Department = "HR",Salary = 25000},
new Employee {Id  = 2,FirstName = "Ava",LastName = "Jones",Age= 24, Department = "QA",Salary = 25000},
new Employee {Id  = 3,FirstName = "William",LastName = "Brown",Age= 27, Department = "Dev",Salary = 25000},
new Employee {Id  = 4,FirstName = "Lily",LastName = "Miller",Age= 35, Department = "HR",Salary = 25000},
new Employee {Id  = 5,FirstName = "Emily",LastName = "Jones",Age= 37, Department = "Dev",Salary = 25000},
new Employee {Id  = 6,FirstName = "Jacob",LastName = "Brown",Age= 26, Department = "Dev",Salary = 25000},
new Employee {Id  = 7,FirstName = "Ava",LastName = "Smith",Age= 32, Department = "Lead",Salary = 25000},
new Employee {Id  = 8,FirstName = "David",LastName = "Wilson",Age= 31, Department = "HR",Salary = 25000},
new Employee {Id  = 9,FirstName = "John",LastName = "Smith",Age= 20, Department = "HR",Salary = 25000},
new Employee {Id  = 10,FirstName = "Emily",LastName = "Harris",Age= 28, Department = "HR",Salary = 25000},
new Employee {Id  = 11,FirstName = "William",LastName = "Harris",Age= 39, Department = "HR",Salary = 25000},
new Employee {Id  = 12,FirstName = "John",LastName = "Brown",Age= 40, Department = "HR",Salary = 25000},
};
}
}
}
*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

namespace LinqDemo
{
    class Mainclass
    {
        public static void Main()
        {
            var books = BookDatabase.GetBooks();
            var data = books.Select(b => b.Author);
            foreach(var i in data)
            {
                Console.WriteLine(i);
            }

        }
    }

    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Author { get; set; }

        public Book(int id,string title,int price,string author) { 
            Id = id;
            Title = title;
            Price = price;
            Author = author;
        }
    }
    public class BookDatabase
    {
        internal static IEnumerable<Book> GetBooks()
        {
            return new List<Book>
            {
                new Book(121,"Harry potter",123,"jk rowling"),
                new Book(122,"rich dad poor dad",123,"Robert"),
                new Book(123,"Henry ford",123,"kiosaki"),
                new Book(124,"Story book",123,"william"),
                new Book(125,"Rahul ki kahani",123,"steve smith")
            };
        }
    }
}