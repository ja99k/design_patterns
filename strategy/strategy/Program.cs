using System;

namespace StrategyPatternExample
{

    // Step 1: Create the interfaces
    public interface IFlyBehavior
    {
        void Fly();
    }

    public interface IQuackBehavior
    {
        void Quack();
    }
    // Step 2: Create the concrete implementations
    public class SimpleFly : IFlyBehavior
    {
        public void Fly() => Console.WriteLine("I'm simply flying!");
    }

    public class SimpleQuack : IQuackBehavior
    {
        public void Quack() => Console.WriteLine("I'm simply quacking!");
    }


    // Step 3: Create the base class
    public abstract class Duck
    {
        protected IFlyBehavior flyBehavior;
        protected IQuackBehavior quackBehavior;

        public void PerformFly() => flyBehavior.Fly();
        public void PerformQuack() => quackBehavior.Quack();

        public void SetFlyBehavior(IFlyBehavior newFlyBehavior) => flyBehavior = newFlyBehavior;
        public void SetQuackBehavior(IQuackBehavior newQuackBehavior) => quackBehavior = newQuackBehavior;

        public abstract void Display();
    }
    // Step 4: Implement specific Duck types
    public class WildDuck : Duck
    {
        public WildDuck()
        {
            flyBehavior = new SimpleFly();
            quackBehavior = new SimpleQuack();
        }

        public override void Display() => Console.WriteLine("I'm a Wild Duck!");
    }

    public class CityDuck : Duck
    {
        public CityDuck()
        {
            flyBehavior = new SimpleFly();
            quackBehavior = new SimpleQuack();
        }

        public override void Display() => Console.WriteLine("I'm a City Duck!");
    }

    // Step 5: Using the Ducks
    class Program
    {
        static void Main(string[] args)
        {
            Duck wildDuck = new WildDuck();
            wildDuck.Display();
            wildDuck.PerformFly();
            wildDuck.PerformQuack();

            Console.WriteLine();

            Duck cityDuck = new CityDuck();
            cityDuck.Display();
            cityDuck.PerformFly();
            cityDuck.PerformQuack();

            Console.WriteLine("\nChanging City Duck behavior:");
            cityDuck.SetFlyBehavior(new SimpleFly());
            cityDuck.PerformFly();
        }
    }
}

