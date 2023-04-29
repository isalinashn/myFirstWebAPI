using MessagePack;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace UniversityApiBackend.Models.DataModels
{
    public class Service
    {
 
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A4",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };

            // 1. Select * of cars
            var carList = from car in cars select car;
            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            var audiList = from car in cars where car.Contains("Audi") select car;
            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }

        }


        // Number examples
        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Each number multiplied by 3
            // take all number, but 9
            // Order number by ascending value

            var processedNumberList =
                numbers
                .Select(num => num * 3) // {3, 6, 9 }
                .Where(num => num != 9) // all but the 9
                .OrderBy(num => num); // at the end, we order ascending
        }

        static public void SearchExamples()
        {
            List<string> textList = new List<string>()
            {
                "a",
                "b",
                "c",
                "c",
                "d",
                "e",
                "f",
                "g"
            };

            // First of all element
            var first = textList.First();

            // First element that in "c"
            var cText = textList.First(text => text.Equals("c"));

            // First element that contains "j"
            var jText = textList.First(text => text.Contains("j"));

            //First element that contains "z"
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z"));

            //Last element that contains "z"
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z"));

            // Singlevalue
            var uniqueTexts = textList.Single();
            var uniqueOrDefaultTexts = textList.SingleOrDefault();

            int[] eveNumber = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumber = { 0, 2, 6 };

            // obtain( 4, 8)
            var myEventNumbers = eveNumber.Except(otherEvenNumber);

        }

        static public void MultipleSelects()
        {
            // Select many

            string[] myOpinions =
            {
                "Opinion 1, Text 1",
                "Opinion 2, Text 2",
                "Opinion 3, Text 3"
            };

            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));
            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Mateo",
                            Email = "mateo@example.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 1,
                            Name = "Juan",
                            Email = "juan@example.com",
                            Salary = 1000
                        },
                        new Employee
                        {
                            Id = 1,
                            Name = "Marcos",
                            Email = "marcos@example.com",
                            Salary = 2000
                        }
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 4,
                            Name = "Maria",
                            Email = "maria@example.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 5,
                            Name = "Martha",
                            Email = "martha@example.com",
                            Salary = 1500
                        },
                        new Employee
                        {
                            Id = 6,
                            Name = "Elizabeth",
                            Email = "elizabeth@exmple.com",
                            Salary = 4000
                        }
                    }
                }
            };

            // Obtain all employeeList
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Know yf any list is empty
            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // All enterprises at list has employees with at least Salary 1000
            bool hasEmployeeWithSalaryMoreThanOrEqual1000 =
                enterprises.Any(enterprise =>
                enterprise.Employees.Any(employee =>
                employee.Salary >= 1000));
        }

        static public void LinqCollections()
        {
            var firstList = new List<string> { "a", "b", "c" };
            var secondList = new List<string> { "a", "c", "d" };

            // Inner Join
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                    secondList,
                    element => element,
                    secondElement => secondElement,
                    (element, secondElement) => new { element, secondElement }
                );

            // Outer Join - Left
            var leftOuterJoin = from element in firstList
                                join secondElemnt in secondList
                                on element equals secondElemnt
                                into temporalList
                                from temporalElemnt in temporalList.DefaultIfEmpty()
                                where element != temporalElemnt
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };

            //Outer join -right
            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElemnt in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElemnt
                                 select new { Element = secondElement };

            var RightOuterJoin2 = from element in firstList
                                  from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                  select new { Element = element, SecondElement = secondElement };

            // Union
            var unionList = leftOuterJoin.Union(rightOuterJoin);

        }

        static public void SkipTakeLink()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            //SKIP
            var skipTwoFirstValues = myList.Skip(2);
            var skipLastTwoValues = myList.SkipLast(2);
            var skipWhileSmallerThan4 = myList.SkipWhile(s => s < 4);

            // TAKE
            var takeFirstTwoValues = myList.Take(2);
            var takeLastTwoValues = myList.TakeLast(2);
            var takeWhileSmallerThan4 = myList.TakeWhile(s => s < 4);

        }
    }
}
