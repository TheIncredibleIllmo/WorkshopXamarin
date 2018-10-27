using System;
using System.Collections.Generic;
using System.Linq;
using CSharpAvanzado.Models;

namespace CSharpAvanzado
{
    class MainClass
    {
        #region FIELDS
        private static string _sentence = "I love cats";
        private static string[] _catNames = { "Lucky", "Bella", "Luna", "Oreo", "Simba", "Toby", "Loki", "Oscar" };
        private static int[] _numbers = { 5, 6, 3, 2, 1, 5, 6, 7, 8, 4, 234, 54, 14, 653, 3, 4, 5, 6, 7 };
        private static object[] _mix = { 1, "string", 'd', new List<int>() { 1, 2, 3, 4 }, new List<int>() { 5, 2, 3, 4 }, "dd", 's', "Hello Kitty", 1, 2, 3, 4, };
        #endregion

        #region PROPERTIES

        #endregion
        private static int _maxShoots = 10;

        public static void Main(string[] args)
        {
            RunEvents();
        }

        #region EVENTS
        private static void RunEvents()
        {

            var shooter = new Shooter();

            shooter.KillingCompleted += OnKillingCompleted;
            shooter.KillingCompleted += OnKillingCompleted;
            shooter.KillingCompleted += OnKillingCompleted;

            StartShooting(shooter);

            //Publisher y un Subscriber

            //Anatomía de un Event - Publisher

            //1.Delegate que coincida con la firma del Event
            //2. Event del mismo tipo que el Delegate
            //3. Alcanzar el evento en algún punto

            //Anatomía de un Event - Subscriber

            //1. Un método que cuadre o coincida con la firma
            //2. Subscribirse al evento.

            //Ayudan al desacople entre código.
        }

        private static void StartShooting(Shooter shooter)
        {
            if (shooter == null) return;

            for (int i = 0; i < 100; i++)
            {
                if (i == _maxShoots)
                {
                    shooter.OnShoot();
                    return;
                }
            }

        }

        private static void OnKillingCompleted(object sender, ShooterEventArgs args)
        {
            Console.WriteLine("RECARGA!!!!!!!!!!!!!!!!!");
        }
        #endregion

        #region DELEGATES

        //ACTIONS: No regresan valores, pero reciben
        static Action<string> FilteringAction;

        //FUNCTIONS: Regresan y reciben.
        public static Func<string, bool> FilteringFunc { get; set; }

        //public delegate bool Filters(string name);
        //private static Filters _filters;

        private static void RunDelegates()
        {
            //FilteringFunc = MoreThanFive;
            FilteringAction = PrintAction;
            Print(FilteringAction);
        }

        private static void Print(Action<string> func)
        {
            func.Invoke("Hola Mundo");
        }

        private static void PrintAction(string text)
        {
            Console.WriteLine(text);
        }

        private static bool LessThanFive(string name)
        {
            return name.Length < 5;
        }

        private static bool MoreThanFive(string name)
        {
            return name.Length > 5;
        }

        private static bool SameThanFive(string name)
        {
            return name.Length == 5;
        }

        #endregion

        #region GENERICS

        private static int _first = 2;
        private static int _second = 4;

        private static void RunGenerics()
        {
            var person1 = new Person("Tod", 180, 70, Gender.Male);
            var person2 = new Person("John", 180, 88, Gender.Male);

            //var result = CompareNumbers(_first, _second);
            var result = CompareValues<Person>(person1, person2);
            Console.WriteLine(result);
        }

        private static bool CompareValues<T>(T one, T two) where T : IComparable<T>
        {
            return one.CompareTo(two) == 0;
            //Si -1 one < two
            //Si 1 one > two
            //Si 0 one == two
        }

        private static bool CompareNumbers(int firstNumber, int secondNumber)
        {
            return firstNumber < secondNumber;
        }

        private static bool CompareStr(string firstStr, string secondStr)
        {
            return firstStr.Equals(secondStr);
        }
        #endregion

        #region LINQ
        private static void RunLinq()
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

            var catsWithA = _catNames?.Where(c => (c.Contains("a")) && (c.Length < 5));

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
        #endregion

    }
}
