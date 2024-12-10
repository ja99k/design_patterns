class Program
{

    // Step 1: Define notification strategy interface
    public interface INotificationStrategy
    {
        void Send(string message, string recipient);
    }

    // Step 2: Implement concrete notification strategies
    public class InAppNotification : INotificationStrategy
    {
        public void Send(string message, string recipient)
        {
            Console.WriteLine($"In app message sent to {recipient}: {message}");
        }
    }

    public class EmailNotification : INotificationStrategy
    {
        public void Send(string message, string recipient)
        {
            Console.WriteLine($"Email sent to {recipient}: {message}");
        }
    }

    public class RSSNotification : INotificationStrategy
    {
        public void Send(string message, string recipient)
        {
            Console.WriteLine($"RSS notification sent to {recipient}: {message}");
        }
    }

    // Step 3: Create the subscriber class (the Observer)
    public class User
    {
        public string Name { get; }
        private INotificationStrategy notificationStrategy;

        public User(string name, INotificationStrategy strategy)
        {
            Name = name;
            notificationStrategy = strategy;
        }

        public void SetNotificationStrategy(INotificationStrategy strategy)
        {
            notificationStrategy = strategy;
        }

        public void Notify(string message)
        {
            notificationStrategy.Send(message, Name);
        }
    }

    // Step 4: Create the Notification service (the Observable)
    public class NotificationService
    {
        private readonly List<User> subscribers = new();

        public void Subscribe(User user)
        {
            subscribers.Add(user);
        }

        public void Unsubscribe(User user)
        {
            subscribers.Remove(user);
        }

        public void NotifyAll(string message)
        {
            foreach (var user in subscribers)
            {
                user.Notify(message);
            }
        }
    }

    // Step 5: Use the system
    static void Main(string[] args)
    {
        var rssNotification = new RSSNotification();
        var inAppNotification = new InAppNotification();
        var emailNotification = new EmailNotification();

        var user1 = new User("Alice", emailNotification);
        var user2 = new User("Bob", inAppNotification);
        var user3 = new User("Charlie", rssNotification);

        var notificationService = new NotificationService();

        notificationService.Subscribe(user1);
        notificationService.Subscribe(user2);
        notificationService.Subscribe(user3);

        Console.WriteLine("Sending notification to all:");
        notificationService.NotifyAll("New notification available!");

        // Change user's notification type preference:
        user2.SetNotificationStrategy(emailNotification);
        Console.WriteLine("\nChanging Bob's notification strategy to Email:");

        // We can also remove a subscriber:
        notificationService.Unsubscribe(user1);
        Console.WriteLine($"\nRemoved {user1.Name} from the subscription list");

        notificationService.NotifyAll("Another update just happened!");

    }
}
