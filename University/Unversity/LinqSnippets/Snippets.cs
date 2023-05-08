using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LinqSnippets
{
    public class Snippets
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
            foreach ( var car in carList )
            {
                Console.WriteLine(car);
            }

            var audiList = from car in cars where car.Contains("Audi") select car;
            foreach ( var audi in audiList )
            {
                Console.WriteLine(audi);
            }

        }

        // Number examples
        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1,2,3,4,5,6,7,8,9};

            // Each number multiplied by 3
            // take all number, but 9
            // Order number by ascending value

            var processedNumberList = 
                numbers
                .Select(num => num * 3) // {3, 6, 9 }
                .Where(num => num != 9 ) // all but the 9
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


        // Paging with Skip and Take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }

        // Variables
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquare = Math.Pow(number,2)
                               where nSquare > average
                               select number;

            Console.WriteLine("Average: {0} ", numbers.Average());
            
            foreach ( var number in aboveAverage )
            {
                Console.WriteLine("Query: {0} {1}", number, Math.Pow(number,2));
            }
        }

        // ZIP
        static public void ZipLinq()
        {
            // Tienen que tener el mismo número de elementos
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + " = " + word );
            // 1 = one, 2 = two, ...

        }


        // Repeat & Range
        static public void RepeatRangeLinq(int start, int end)
        {
            //Generate a collection values
            IEnumerable<int> firt1000 = Enumerable.Range(1, 1000);

            // Repeat a value N times
            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5);
        }
        

        static public void StudentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "A",
                    Grade = 90,
                    Certified = true
                },
                new Student
                {
                    Id = 2,
                    Name = "B",
                    Grade = 50,
                    Certified = false
                },
                new Student
                {
                    Id = 3,
                    Name = "C",
                    Grade = 96,
                    Certified = true
                },
                new Student
                {
                    Id = 4,
                    Name = "D",
                    Grade = 10,
                    Certified = false
                },
                new Student
                {
                    Id = 5,
                    Name = "E",
                    Grade = 99,
                    Certified = true
                },
            };

            var certified = from student in classRoom
                            where student.Certified
                            select student;

            var noCertified = from student in classRoom
                              where student.Certified == false
                              select student;

            var approvedStuduentName = from student in classRoom
                           where student.Grade >= 50 && student.Certified
                           select student.Name;

        }

        // All
        static public void AllLinq()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5 };
            bool allAreSmallerThan10 = numbers.All(x => x < 10);  // true
            bool allAreBiggerOrEqual = numbers.All(x => x >= 2); // false

            var emptyList = new List<int>();
            bool allNumbersAreGreaterThan0 = emptyList.All(x => x >= 0); // true
        }

        // Agregate
        static public void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3,4, 5,6,7,8,9,10 };
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);
            // 0, 1 => 1
            // 1, 2 => 3
            // 3, 4 => 7
            // 7, 8 => 15 ...

            string[] words = { "hello, ", "my ", "name ", "is ", "Israel " };
            // Hello my name is israel

            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);
            // "", "hello, " => hello
            // "hello, ", "my " => hello, my
            // "hello, my ", "name " => hello, my name
            // "hello, my name ", "is " => hello, my name is
            // "hello, my name is ", "Israel " => hello, my name is Israel
        }

        // Distint
        static public void distintValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            IEnumerable<int> distintValues = numbers.Distinct();

        }


        // GroupBy
        static public void groupByExample()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9};
            // Obtaint only even numbers and generate two groups

            var grouped = numbers.GroupBy(x => x % 2 == 2);

            // We will have two groups
            // 1. The group that doesn't fit the condition (ood numbers)
            // 2. The group that fit the condition (even numbers)

            foreach ( var group in grouped )
            {
                foreach( var value in group )
                {
                    Console.WriteLine( value ); // 1,3,5,7,9...2,4,6,8
                }
            }

            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "A",
                    Grade = 90,
                    Certified = true
                },
                new Student
                {
                    Id = 2,
                    Name = "B",
                    Grade = 50,
                    Certified = false
                },
                new Student
                {
                    Id = 3,
                    Name = "C",
                    Grade = 96,
                    Certified = true
                },
                new Student
                {
                    Id = 4,
                    Name = "D",
                    Grade = 10,
                    Certified = false
                },
                new Student
                {
                    Id = 5,
                    Name = "E",
                    Grade = 99,
                    Certified = true
                }
            };

            //var aprovedQuery = classRoom.GroupBy(student => student.Certified && student.Grade >= 50);
            var certifiedQuery = classRoom.GroupBy(student => student.Certified);
            // We obtaint two groups
            // 1. Not certified student
            // 2. Certified student

            foreach ( var group in certifiedQuery )
            {
                Console.WriteLine("Group: {0}--------", group.Key);
                foreach( var student in group )
                {
                    Console.WriteLine( student.Name );
                }
            }

        }
        static public void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id=1,
                            Created = DateTime.Now,
                            Title = "My fist comment",
                            Content = "My fist content"
                        },
                        new Comment()
                        {
                            Id=2,
                            Created = DateTime.Now,
                            Title = "My second comment",
                            Content = "My second content"
                        },
                        new Comment()
                        {
                            Id=3,
                            Created = DateTime.Now,
                            Title = "My 3st comment",
                            Content = "My 3st content"
                        }
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My second content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id=3,
                            Created = DateTime.Now,
                            Title = "My fist comment",
                            Content = "My fist content"
                        },
                        new Comment()
                        {
                            Id=4,
                            Created = DateTime.Now,
                            Title = "My second comment",
                            Content = "My second content"
                        },
                        new Comment()
                        {
                            Id=5,
                            Created = DateTime.Now,
                            Title = "My 3st comment",
                            Content = "My 3st content"
                        }
                    }
                }
            };

            var commentsContent = posts.SelectMany(
                post => post.Comments, 
                (post, comment) => new { PostId = post.Id, CommentContent = comment.Content,
            });

        }
    }
}