using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestSQLApp.Data;
using TestSQLApp.Models;

namespace TestSQLApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //удаление и пересоздание БД
            using (TestSqlAppContext db = new TestSqlAppContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            };

            //создание экземляров сущностей и показ их в виде списков
            using (TestSqlAppContext db = new TestSqlAppContext())
            {
                Position manager = new Position { Title = "Manager" };
                Position accounter = new Position { Title = "Accounter" };
                Position engineer = new Position { Title = "Engineer" };
                Position mechanic = new Position { Title = "Mechanic" };
                Position driver = new Position { Title = "Driver" };

                db.Positions.AddRange(manager, accounter, engineer, mechanic, driver);
                db.SaveChanges();

                Employee employee1 = new Employee { LastName = "Kiriluk", FirstName = "Sergey", PositionId = manager.Id };
                Employee employee2 = new Employee { LastName = "Yablokova", FirstName = "Alina", PositionId = accounter.Id };
                Employee employee3 = new Employee { LastName = "Petrenko", FirstName = "Vlad", PositionId = engineer.Id };
                Employee employee4 = new Employee { LastName = "Ivanov", FirstName = "Andrey", PositionId = mechanic.Id };
                Employee employee5 = new Employee { LastName = "Sergeev", FirstName = "Dmitriy", PositionId = driver.Id };

                db.Employees.AddRange(employee1, employee2, employee3, employee4, employee5);
                db.SaveChanges();

                Console.WriteLine("List of positions:");
                foreach (var position in db.Positions.ToList())
                {
                    Console.WriteLine($"{position.Id}. {position.Title}");
                }

                Console.WriteLine("---------------------------");
                Console.WriteLine("List of employees:");
                foreach (var employee in db.Employees.ToList())
                {
                    Console.WriteLine($"{employee.Id}. {employee.LastName} {employee.FirstName} works as a {employee.Position?.Title}");
                }
            };

            Console.WriteLine("---------------------------");
            //изменение должности для одного из сотрудников
            using (TestSqlAppContext db = new TestSqlAppContext())
            {
                var mechanic = db.Positions
                    .Where(position => position.Title == "Mechanic")
                    .FirstOrDefault();
                if (mechanic is Position)
                {
                    mechanic.Title = "Constractor";
                }
                db.SaveChanges();

                Console.WriteLine("List of employees after 1 position was changed:");
                foreach (var employee in db.Employees.ToList())
                {
                    Console.WriteLine($"{employee.Id}. {employee.LastName} {employee.FirstName} works as a {employee.Position?.Title}");
                }
            }

            //изменение фамилии одного из сотрудников
            Console.WriteLine("---------------------------");
            using (TestSqlAppContext db = new TestSqlAppContext())
            {
                var yablokova = db.Employees
                    .Where(employee => employee.LastName == "Yablokova")
                    .FirstOrDefault();
                if (yablokova is Employee)
                {
                    yablokova.LastName = "Snigireva";
                }
                db.SaveChanges();

                Console.WriteLine("List of employees after data of 1 employee were changed:");
                foreach (var employee in db.Employees.ToList())
                {
                    Console.WriteLine($"{employee.Id}. {employee.LastName} {employee.FirstName} works as a {employee.Position?.Title}");
                }
            }

            ///удаление одной из должностей
            ///(в нашем случае и соответственно удаление сотрудника,
            ///закрепленного за удаляемой должностью 
            Console.WriteLine("---------------------------");
            using (TestSqlAppContext db = new TestSqlAppContext())
            {
                var driver = db.Positions
                     .Where(position => position.Title == "Driver")
                     .FirstOrDefault();
                if (driver is Position)
                {
                    db.Remove(driver);
                }
                db.SaveChanges();

                Console.WriteLine("List of positions since last change:");
                foreach (var position in db.Positions.ToList())
                {
                    Console.WriteLine($"{position.Id}. {position.Title}");
                }
            }

            //удаление одного из сотрудников
            Console.WriteLine("---------------------------");
            using (TestSqlAppContext db = new TestSqlAppContext())
            {
                var petrenko = db.Employees
                    .Where(employee => employee.LastName == "Petrenko")
                    .FirstOrDefault();
                if (petrenko is Employee)
                {
                    db.Remove(petrenko);
                }
                db.SaveChanges();

                Console.WriteLine("List of employees after 1 of employee's was deleted:");
                foreach (var employee in db.Employees.ToList())
                {
                    Console.WriteLine($"{employee.Id}. {employee.LastName} {employee.FirstName} works as a {employee.Position?.Title}");
                }
            }
        }
    }
}
