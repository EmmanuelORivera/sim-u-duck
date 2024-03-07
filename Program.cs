internal class Program
{
    private static void Main(string[] args)
    {
        Duck mallard = new MallardDuck();
        mallard.PerformQuack();
        mallard.PerformFly();

        Duck model = new ModelDuck();
        model.PerformFly();
        model.SetFlyBehavior(new FlyRocketPowered());
        model.PerformFly();

    }
}

public abstract class Duck
{
    // reference variables for the behavior interface types.
    // All duck subclasses inherit these
    protected IFlyBehavior flyBehavior;
    protected IQuackBehavior quackBehavior;

    public Duck() { }

    public abstract void Display();

    public void PerformFly()
    {
        flyBehavior.Fly();
    }

    public void PerformQuack()
    {
        quackBehavior.Quack();
    }

    public void swim()
    {
        System.Console.WriteLine("All ducks float, even decoys!");
    }
    public void SetFlyBehavior(IFlyBehavior flyBehavior)
    {
        this.flyBehavior = flyBehavior;
    }
    public void SetQuackBehavior(IQuackBehavior quackBehavior)
    {
        this.quackBehavior = quackBehavior;
    }
}

public interface IFlyBehavior
{
    public void Fly();

}

public class FlyWithWings : IFlyBehavior
{
    public void Fly()
    {
        System.Console.WriteLine("I'm flying!");
    }
}

public class FlyNoWay : IFlyBehavior
{
    public void Fly()
    {
        System.Console.WriteLine("I can't fly");
    }
}

public interface IQuackBehavior
{
    public void Quack();
}

public class Quack : IQuackBehavior
{
    void IQuackBehavior.Quack()
    {
        System.Console.WriteLine("Quack");
    }
}

public class MuteQuack : IQuackBehavior
{
    public void Quack()
    {
        System.Console.WriteLine("<< Silence >>");
    }
}

public class Squeak : IQuackBehavior
{
    public void Quack()
    {
        System.Console.WriteLine("Squeak");
    }
}


public class MallardDuck : Duck
{
    public MallardDuck()
    {
        quackBehavior = new Quack();
        flyBehavior = new FlyWithWings();
    }
    public override void Display()
    {
        System.Console.WriteLine("I'm a real Mallard duck");
    }
}

public class ModelDuck : Duck
{
    public ModelDuck()
    {
        flyBehavior = new FlyNoWay();
        quackBehavior = new Quack();
    }
    public override void Display()
    {
        System.Console.WriteLine("I'm a model duck");
    }
}

public class FlyRocketPowered : IFlyBehavior
{
    public void Fly()
    {
        System.Console.WriteLine("I'm flying with a rocket!");
    }
}