using System;
using StackExchange.Redis;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter server address: ");
            var address = Console.ReadLine();
            address = string.IsNullOrWhiteSpace(address) ? "127.0.0.1" : address;

            Console.WriteLine("Enter your name: ");
            var userName = Console.ReadLine();

            var chat = RedisChat.Create(address ,userName, (isMyMessage, message) =>
            {
                if (!isMyMessage)
                {
                    Console.WriteLine($"[{message.User.Name}] {message.Text}");
                }
            });

            Console.WriteLine($"Connected to {address}, go chatting!");

            while (true)
            {
                var message = Console.ReadLine();
                chat.Send(message);
            }
        }
    }
}