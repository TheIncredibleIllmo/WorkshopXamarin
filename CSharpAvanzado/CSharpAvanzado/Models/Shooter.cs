using System;
namespace CSharpAvanzado.Models
{
    public class Shooter
    {
        public string Name { get; set; }
        public int Shots { get; set; }

        //1.Delegate
        //public delegate void KillingHandler(object sender, EventArgs args);

        //2.Event
        //public event KillingHandler KillingCompleted;

        public event EventHandler<ShooterEventArgs> KillingCompleted;

        //3.Alcanzar el evento
        public void OnShoot()
        {
            if (KillingCompleted == null) return;
            KillingCompleted.Invoke(this, new ShooterEventArgs(Name, Shots));
        }

        public Shooter()
        {
            Name = "Juanito";
            Shots = 10;
        }
    }

    public class ShooterEventArgs : EventArgs
    {
        public string Name { get; set; }
        public int Shots { get; set; }

        public ShooterEventArgs(string name, int shots)
        {
            Name = name;
            Shots = shots;
        }
    }
}
