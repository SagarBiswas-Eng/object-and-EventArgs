using System;

// Define the Order class
public class Order
{
    public string OrderId { get; set; }
    public double Amount { get; set; }

    public Order(string orderId, double amount)
    {
        OrderId = orderId;
        Amount = amount;
    }
}

// Define an event publisher class
class EventPublisher
{
    // Declare an event using EventHandler delegate
    public event EventHandler<OrderPlacedEventArgs> OrderPlaced;

    // Method that raises the event
    public void PlaceOrder(Order order)
    {
        Console.WriteLine($"Order placed: {order.OrderId}, Amount: {order.Amount}");

        // Raise the event with OrderPlacedEventArgs
        OnOrderPlaced(new OrderPlacedEventArgs(order));
    }

    // Helper method to raise the event
    protected virtual void OnOrderPlaced(OrderPlacedEventArgs e)
    {
        OrderPlaced?.Invoke(this, e);
    }
}

// Define an event subscriber class
class EventSubscriber
{
    // Event handler method that responds to the event
    public void HandleOrderPlaced(object sender, OrderPlacedEventArgs e)
    {
        Console.WriteLine($"EventSubscriber: Received order - {e.Order.OrderId}");
    }
}

// Define custom event argument class
public class OrderPlacedEventArgs : EventArgs
{
    public Order Order { get; }

    public OrderPlacedEventArgs(Order order)
    {
        Order = order;
    }
}

class Program
{
    static void Main()
    {
        EventPublisher orderProcessor = new EventPublisher();
        EventSubscriber subscriber = new EventSubscriber();

        // Subscribe the subscriber to the OrderPlaced event
        orderProcessor.OrderPlaced += subscriber.HandleOrderPlaced;

        // Place an order (event will carry order-related data)
        Order order = new Order("12345", 100.0);
        orderProcessor.PlaceOrder(order);
    }
}
