// Strategy pattern
// Defines a family of algorithms (in this case the behaviors), encapsulate each one, and makes them interchangeable.
// Strategy lets the algorithm vary independently from clients that use it.
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

        // Client code 
        PaymentProcessor processor = new PaymentProcessor();

        // Paypal payment 
        processor.SetPaymentMethod(new PayPalPayment());
        processor.ProcessPayment();

        //Credit card payment
        processor.SetPaymentMethod(new CreditCardPayment());
        processor.ProcessPayment();

        //Bitcoin payment
        processor.SetPaymentMethod(new BitcoinPayment());
        processor.ProcessPayment();

        //  Sort example;
        // The client code picks a concrete strategy and passes it to the context. 
        // The client should be aware of the differences between 
        // strategies in order to make the right choice.

        var context = new Context();
        System.Console.WriteLine("Client: Strategy is set to normal sorting.");
        context.SetStrategy(new ConcreteStrategyA());
        context.DoSomeBusinessLogic();

        System.Console.WriteLine();

        System.Console.WriteLine("Client: Strategy is set to reverse sorting.");
        context.SetStrategy(new ConcreteStrategyB());
        context.DoSomeBusinessLogic();
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


// Problem Scenario: Payment Processing System
// You are tasked with implementing a payment processing system for an e-commerce platform.
//  The platform accepts payments through various payment gateways, including PayPal, Credit Card, and Bitcoin.
//  Each payment method requires different authentication and transaction processes.


class PaymentProcessor
{

    private IPaymentMethod _paymentMethod;

    public void SetPaymentMethod(IPaymentMethod paymentMethod)
    {
        this._paymentMethod = paymentMethod;
    }
    public void ProcessPayment()
    {
        this._paymentMethod.Authenticate();
        this._paymentMethod.ProcessTransaction();
    }
}

interface IPaymentMethod
{
    void Authenticate();
    void ProcessTransaction();
}

class PayPalPayment : IPaymentMethod
{
    public void Authenticate()
    {
        System.Console.WriteLine("PayPal Authentication");
    }

    public void ProcessTransaction()
    {
        System.Console.WriteLine("PayPal Transaction");
    }
}

class CreditCardPayment : IPaymentMethod
{
    public void Authenticate()
    {
        System.Console.WriteLine("Credit card Authentication");
    }

    public void ProcessTransaction()
    {
        System.Console.WriteLine("Credit card Transaction");
    }
}

class BitcoinPayment : IPaymentMethod
{
    public void Authenticate()
    {
        System.Console.WriteLine("Bitcoin Authentication");
    }

    public void ProcessTransaction()
    {
        System.Console.WriteLine("Bitcoin Transaction");
    }
}

// Another example of strategy pattern with a sorting example.

// The context defines the interface of interest to the clients.
class Context
{
    // The context maintaints a reference to one of the Strategy objects. 
    // The context does not know the concreate class of a strategy. It should
    // work with all strategies via the Strategy interface.
    private IStrategy _strategy;

    public Context()
    {

    }

    //  Usually, the context accepts a strategy through the constructor, but
    //  also provides a setter to change it at runtime.
    public Context(IStrategy strategy)
    {
        this._strategy = strategy;
    }

    // Usually, the Context allows replacing a Strategy object at runtime.
    public void SetStrategy(IStrategy strategy)
    {
        this._strategy = strategy;
    }

    // The Context delegates some work to the Strategy object insted of implementing multiple versions of the algorithm on its own.
    public void DoSomeBusinessLogic()
    {
        System.Console.WriteLine("Context: Sorting data using the strategy (not sure how it'll do it)");

        var result = this._strategy.DoAlgorithm(new List<string> { "a", "b", "c", "d", "e" });

        string resultStr = string.Empty;
        foreach (var element in result as List<string>)
        {
            resultStr += element + ",";
        }

        System.Console.WriteLine(resultStr);
    }

}

// The strategy interface declares operations common to all suported
// versions of some algorithm

// The context uses this interface to call the algorithm defined by Concrete
// Strategies.
public interface IStrategy
{
    object DoAlgorithm(object data);
}

// Concrete Strategies implement the algorithm while following the base
// Strategy interface. The interface makes them interchangeable in the 
// Context.

class ConcreteStrategyA : IStrategy
{
    public object DoAlgorithm(object data)
    {
        var list = data as List<string>;
        list.Sort();

        return list;
    }
}
class ConcreteStrategyB : IStrategy
{
    public object DoAlgorithm(object data)
    {
        var list = data as List<string>;
        list.Sort();
        list.Reverse();

        return list;
    }
}