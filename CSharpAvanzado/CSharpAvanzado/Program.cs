using System;
using System.Collections.Generic;
using System.Linq;
using CSharpAvanzado.Models;

namespace CSharpAvanzado
{
    class MainClass
    {
        private static string _sentence = "I love cats";
        private static string[] _catNames = { "Lucky", "Bella", "Luna", "Oreo", "Simba", "Toby", "Loki", "Oscar" };
        private static int[] _numbers = { 5, 6, 3, 2, 1, 5, 6, 7, 8, 4, 234, 54, 14, 653, 3, 4, 5, 6, 7 };
        private static object[] _mix = { 1, "string", 'd', new List<int>() { 1, 2, 3, 4 }, new List<int>() { 5, 2, 3, 4 }, "dd", 's', "Hello Kitty", 1, 2, 3, 4, };

        public static void Main(string[] args)
        {
            List<Person> people = new List<Person>()
            {
                new Person("Tod", 180, 70, Gender.Male),
                new Person("John", 170, 88, Gender.Male),
                new Person("Anna", 150, 48, Gender.Female),
                new Person("Kyle", 164, 77, Gender.Male),
                new Person("Anna", 164, 77, Gender.Male),
                new Person("Maria", 160, 55, Gender.Female),
                new Person("John", 160, 55, Gender.Female)
            };

            var persons = people.Where(p => p.Weight <= 55).OrderByDescending(p => p.Weight < 55).ThenBy(p => p.Name);
            //var getNumbers = from n in _numbers
            //where (n <= 5)
            //select n;

            var allIntegers = _mix.OfType<int>();

            var average = _catNames.Average(c => c.Length);
            var max = _catNames.Max(c => c.Length);
            var min = _catNames.Min(c => c.Length);
            var sum = _catNames.Sum(c => c.Length);

            var getNumbers = _numbers.Where(n => n < 5);

            //var catsWithA = from cat in _catNames
            //where (cat.Contains("a")) && (cat.Length < 5)
            //orderby cat descending
            //select cat;

            var catsWithA = _catNames.Where(c => (c.Contains("a")) && (c.Length < 5));

            //Console.WriteLine(string.Join(" , ", persons));

            people.ForEach(p => Console.WriteLine($"{p}"));

            foreach (var p in people)
            {
                //Console.WriteLine($"{p.FullPerson}");
            }

            //Console.WriteLine($"Promedio: {average}");
            //Console.WriteLine($"Mínimo: {min}");
            //Console.WriteLine($"Máximo: {max}");
            //Console.WriteLine($"Suma total: {sum}");

            //EXPRESIÓN LAMBDA 

            //Denotadas por el operador => LAMBDA

            //(input) => (work);

            //n => ((n % 2==1)

            //var oddNumbers = from n in _numbers
            //where (n%2==1)
            //select n;


            //LINQ (LANGUAGE INTEGRATED QUERY)

            //Similiar a SQL, trabaja sobre Collections

            //Operaciones básicas:

            //1.Get Source 
            //2.Create Query
            //3.Execute Query

            //Definir la Fuente (SOURCE) - from .... in
            //Definir las condicionales - where
            //Tomar la salida fitlra - select


        }
    }
}
