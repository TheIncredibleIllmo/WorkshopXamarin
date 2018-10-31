using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private static int _counter = 0;

        public static void Main(string[] args)
        {
            ProcessAsync();

            Console.ReadKey();
        }

        #region ASYNC
        /// <summary>
        /// Processes the async.
        /// </summary>
        /// <returns>The async.</returns>
        private static async Task ProcessAsync()
        {
            /*
            1.- Task
            Clase que nos ayuda a crear procesos en otro hilo
            Task.Factory.StartNew(); -> Definición que dispone de más elementos para modificar el hilo o tarea,
            Cuando termina la ejecución de la tarea hace una especie de Dispose al hilo y libera recursos. (RECOMENDABLE USARLO)
            Task.Run(); -> Se considera un atajo que no implementa tantas opciones (Si Task.Factory.StartNew() ocasiona problemas, usar Task.Run()


            2.- Keywords Async/Await
            Que un Method tenga async no significa que se ejecutará asincronamente.
            Siempre debe de ir acompañado de await
            Siempre regresar Task
            Los unicos que regresan void son las firmas de los Manejadores de Eventos

            3.-CancellationTokenSource clase que nos permite crear un token para 
            avisar sobre la cancelación de algún proceso que corre en otro hilo.

            CancellationTokenSource y CancellationToken son las clases usadas.

            var cancellationTokenSrc = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSrc.Token;

            evaluar -> cancellationToken.IsCancellationRequested;

            para cancelar -> cancellationTokenSource.Cancel();
            */

            await Counting();
            Console.WriteLine("Tarea terminada");
        }

        /// <summary>
        /// Counting this instance.
        /// </summary>
        /// <returns>The counting.</returns>
        private static Task Counting()
        {
            var longTask = Task.Factory.StartNew(() =>
            {
                int myCount = 0;
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"{myCount}");
                    myCount++;
                }
            });

            return longTask;
        }

        /// <summary>
        /// Cancels a Task with a token.
        /// </summary>
        private static void CancelToken()
        {
            Console.WriteLine("Started");
            Console.WriteLine();


            //Creación del token.
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;


            //Creación de la Tarea (Corre en otro hilo)
            Task task = Task.Run(() =>
            {
                //Pregunta si la cancelación es requerida
                while (!cancellationToken.IsCancellationRequested)
                {
                    _counter++;

                    Console.Write($"{_counter} |");
                    Thread.Sleep(500);
                }
            }, cancellationToken);//---> Aquí pasamos el token

            Console.WriteLine("Press enter to stop the task");
            Console.ReadLine();

            //Manda a cancelar el Token
            cancellationTokenSource.Cancel();

            Console.WriteLine($"Task executed {_counter} times");

            Console.WriteLine();
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }


        #endregion

        #region EVENTS
        private static void RunEvents()
        {

            /*Publisher y un Subscriber

            Anatomía de un Event - Publisher

            1.Delegate que coincida con la firma del Event
            2. Event del mismo tipo que el Delegate
            3. Alcanzar el evento en algún punto

            Anatomía de un Event - Subscriber

            1. Un método que cuadre o coincida con la firma
            2. Subscribirse al evento.

            Ayudan al desacople entre código.*/

            var shooter = new Shooter();


            //Nos subscribimos al Event
            shooter.KillingCompleted += OnKillingCompleted;

            //Función que alcanzará el Event
            StartShooting(shooter);
        }

        /// <summary>
        /// Starts the shooting.
        /// </summary>
        /// <param name="shooter">Shooter.</param>
        private static void StartShooting(Shooter shooter)
        {
            if (shooter == null) return;

            for (int i = 0; i < 100; i++)
            {
                if (i == _maxShoots)
                {
                    //Alcanza el evento.
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
        //Declaración de un genérico, la firma debe de ser respetada.
        public delegate bool Filters(string name);
        private static Filters _filters;

        //ACTIONS: No regresan valores, pero reciben
        static Action<string> FilteringAction;

        //FUNCTIONS: Regresan y reciben.
        public static Func<string, bool> FilteringFunc { get; set; }

        /// <summary>
        /// Runs the delegates.
        /// </summary>
        private static void RunDelegates()
        {
            /*
            1.-Delegates
            a)Apuntadores a funciones
            b)Variables para funciones
            */

            //Añadir la función, la cual debe respetar la firma
            _filters = LessThanFive;

            //Ejecutar mediante Invoke, al hacer uso del Invoke, el delegate
            //requerira los parámetros establecidos por la firma.
            var result = _filters.Invoke("Workshop: Learning Xamarin Vol 1");

            Console.WriteLine($"Is less than five characters: {result}");


            //Asignación del valor de la Function.
            FilteringAction = PrintAction;

            //Pasamos la function
            Print(FilteringAction);
        }

        /// <summary>
        /// Prints the specified func.
        /// </summary>
        /// <param name="func">Func.</param>
        private static void Print(Action<string> func)
        {
            func.Invoke("Hola Mundo");
        }

        /// <summary>
        /// Prints the action.
        /// </summary>
        /// <param name="text">Text.</param>
        private static void PrintAction(string text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// Evaluates if the string is Less the than five.
        /// </summary>
        /// <returns><c>true</c>, if than five was lessed, <c>false</c> otherwise.</returns>
        /// <param name="name">Name.</param>
        private static bool LessThanFive(string name)
        {
            return name.Length < 5;
        }

        /// <summary>
        /// Evaluates if the string is more the than five.
        /// </summary>
        /// <returns><c>true</c>, if than five was mored, <c>false</c> otherwise.</returns>
        /// <param name="name">Name.</param>
        private static bool MoreThanFive(string name)
        {
            return name.Length > 5;
        }

        /// <summary>
        /// Evaluates if the string is equals to five.
        /// </summary>
        /// <returns><c>true</c>, if than five was samed, <c>false</c> otherwise.</returns>
        /// <param name="name">Name.</param>
        private static bool SameThanFive(string name)
        {
            return name.Length == 5;
        }

        #endregion

        #region GENERICS
        private static int _first = 2;
        private static int _second = 4;

        /// <summary>
        /// Runs the generic examples.
        /// </summary>
        private static void RunGenerics()
        {
            /*
            1.- Genericos, denotados por <T>.
            2.- Especificados y/o limitados por los "constraints" 
            Como por ejemplo:
            where T : IComparable<T>
            where T : Person
            where T : new()
            */

            var person1 = new Person("Tod", 180, 70, Gender.Male);
            var person2 = new Person("John", 180, 88, Gender.Male);

            //var result = CompareNumbers(_first, _second);

            var result = CompareValues<Person>(person1, person2);

            Console.WriteLine(result);
        }

        /// <summary>
        /// Generic method which compares two T values.
        /// </summary>
        /// <returns><c>true</c>, if values was compared, <c>false</c> otherwise.</returns>
        /// <param name="one">One.</param>
        /// <param name="two">Two.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        private static bool CompareValues<T>(T one, T two) where T : IComparable<T>
        {
            return one.CompareTo(two) == 0;

            //Si -1 one < two
            //Si 1 one > two
            //Si 0 one == two
        }

        /// <summary>
        /// Compares two numbers.
        /// </summary>
        /// <returns><c>true</c>, if numbers was compared, <c>false</c> otherwise.</returns>
        /// <param name="firstNumber">First number.</param>
        /// <param name="secondNumber">Second number.</param>
        private static bool CompareNumbers(int firstNumber, int secondNumber)
        {
            return firstNumber < secondNumber;
        }

        /// <summary>
        /// Compares two strings.
        /// </summary>
        /// <returns><c>true</c>, if string was compared, <c>false</c> otherwise.</returns>
        /// <param name="firstStr">First string.</param>
        /// <param name="secondStr">Second string.</param>
        private static bool CompareStr(string firstStr, string secondStr)
        {
            return firstStr.Equals(secondStr);
        }
        #endregion

        #region LINQ
        /// <summary>
        /// Runs the linq examples.
        /// </summary>
        private static void RunLinq()
        {
            /*

            1.-LINQ (LANGUAGE INTEGRATED QUERY)

            Similiar a SQL, trabaja sobre Collections

            Operaciones básicas:

            1.Get Source 
            2.Create Query
            3.Execute Query

            Definir la Fuente (SOURCE) - from .... in
            Definir las condicionales - where
            Tomar la salida fitlra - select

            2.-EXPRESIÓN LAMBDA 

            Denotadas por el operador => LAMBDA

            (input) => (work);

            n => ((n % 2==1)

            var oddNumbers = from n in _numbers
            where (n%2==1)
            select n;
            
            */

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
        }
        #endregion

    }
}
