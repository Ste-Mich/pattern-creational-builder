using System;

/*  Pomocí Builderu se můžeme zbavit velkého konstruktoru.
 *  - Nastavení počátečních hodnot se děje, ne při vytvoření objektu,
 *  - ale zavoláním specifických metod za sebou.
 *  - O volání těchto metod se může starat direktor.
 */


namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            Director director = new(new CarBuilder());
            var sportsCar = director.ConstructSportsCar();
            var familyCar = director.ConstructFamilyCar();

            Console.WriteLine(sportsCar.Name + " engine power is: " + sportsCar.EnginePower);
            Console.WriteLine(familyCar.Name + " has " + familyCar.Seats + " seats.");
        }
    }


    public class Director
    {
        private IBuilder builder;

        public Director(IBuilder builder)
        {
            this.builder = builder;
        }

        public Car ConstructSportsCar()
        {
            builder.Reset();
            builder.SetEnginePower(600);
            builder.SetWheels(4);
            builder.SetSeats(4);
            builder.SetHasGps(false);
            builder.SetName("Sports Car");
            return builder.GetCar();
        }

        public Car ConstructFamilyCar()
        {
            builder.Reset();
            builder.SetEnginePower(250);
            builder.SetWheels(4);
            builder.SetSeats(8);
            builder.SetHasGps(true);
            builder.SetName("Family Car");
            return builder.GetCar();
        }
    }
    
    public interface IBuilder
    {
        public void Reset();
        public Car GetCar();
        public void SetEnginePower(int power);
        public void SetWheels(int wheels);
        public void SetSeats(int seats);
        public void SetHasGps(bool hasGps);
        public void SetName(string name);
    }

    class CarBuilder : IBuilder
    {
        private Car car;

        public CarBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            car = new Car();
        }

        public Car GetCar()
        {
            var product = this.car;
            this.Reset();
            return product;
        }

        public void SetEnginePower(int power)
        {
            car.EnginePower = power;
        }

        public void SetHasGps(bool hasGps)
        {
            car.HasGps = hasGps;
        }

        public void SetName(string name)
        {
            car.Name = name;
        }

        public void SetSeats(int seats)
        {
            car.Seats = seats;
        }

        public void SetWheels(int wheels)
        {
            car.Wheels = wheels;
        }
    }

    public class Car
    {
        public int EnginePower { get; set; }
        public int Wheels { get; set; }
        public int Seats { get; set; }
        public bool HasGps { get; set; }
        public string Name { get; set; }
    }
}
