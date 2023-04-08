// EN: Command Design Pattern
//
// Intent: Turns a request into a stand-alone object that contains all
// information about the request. This transformation lets you parameterize
// methods with different requests, delay or queue a request's execution, and
// support undo-able operations.
//
// RU: Паттерн Команда
//
// Назначение: Превращает запросы в объекты, позволяя передавать их как
// аргументы при вызове методов, ставить запросы в очередь, логировать их, а
// также поддерживать отмену операций.

using System;

namespace RefactoringGuru.DesignPatterns.Command.Conceptual
{
    // EN: The Command interface declares a method for executing a command.
    //
    // RU: Интерфейс Команды объявляет метод для выполнения команд.
    public interface ICommand
    {
        void Execute();
    }

    // EN: Some commands can implement simple operations on their own.
    //
    // RU: Некоторые команды способны выполнять простые операции самостоятельно.
    internal class SimpleCommand : ICommand
    {
        private string _payload = string.Empty;

        public SimpleCommand(string payload)
        {
            this._payload = payload;
        }

        public void Execute()
        {
            Console.WriteLine($"SimpleCommand: See, I can do simple things like printing ({this._payload})");
        }
    }

    // EN: However, some commands can delegate more complex operations to other
    // objects, called "receivers."
    //
    // RU: Но есть и команды, которые делегируют более сложные операции другим
    // объектам, называемым «получателями».
    internal class ComplexCommand : ICommand
    {
        private Receiver _receiver;

        // EN: Context data, required for launching the receiver's methods.
        //
        // RU: Данные о контексте, необходимые для запуска методов получателя.
        private string _a;

        private string _b;

        // EN: Complex commands can accept one or several receiver objects along
        // with any context data via the constructor.
        //
        // RU: Сложные команды могут принимать один или несколько
        // объектов-получателей вместе с любыми данными о контексте через
        // конструктор.
        public ComplexCommand(Receiver receiver, string a, string b)
        {
            this._receiver = receiver;
            this._a = a;
            this._b = b;
        }

        // EN: Commands can delegate to any methods of a receiver.
        //
        // RU: Команды могут делегировать выполнение любым методам получателя.
        public void Execute()
        {
            Console.WriteLine("ComplexCommand: Complex stuff should be done by a receiver object.");
            this._receiver.DoSomething(this._a);
            this._receiver.DoSomethingElse(this._b);
        }
    }

    // EN: The Receiver classes contain some important business logic. They know
    // how to perform all kinds of operations, associated with carrying out a
    // request. In fact, any class may serve as a Receiver.
    //
    // RU: Классы Получателей содержат некую важную бизнес-логику. Они умеют
    // выполнять все виды операций, связанных с выполнением запроса. Фактически,
    // любой класс может выступать Получателем.
    internal class Receiver
    {
        public void DoSomething(string a)
        {
            Console.WriteLine($"Receiver: Working on ({a}.)");
        }

        public void DoSomethingElse(string b)
        {
            Console.WriteLine($"Receiver: Also working on ({b}.)");
        }
    }

    // EN: The Invoker is associated with one or several commands. It sends a
    // request to the command.
    //
    // RU: Отправитель связан с одной или несколькими командами. Он отправляет
    // запрос команде.
    internal class Invoker
    {
        private ICommand _onStart;

        private ICommand _onFinish;

        // EN: Initialize commands.
        //
        // RU: Инициализация команд
        public void SetOnStart(ICommand command)
        {
            this._onStart = command;
        }

        public void SetOnFinish(ICommand command)
        {
            this._onFinish = command;
        }

        // EN: The Invoker does not depend on concrete command or receiver
        // classes. The Invoker passes a request to a receiver indirectly, by
        // executing a command.
        //
        // RU: Отправитель не зависит от классов конкретных команд и
        // получателей. Отправитель передаёт запрос получателю косвенно,
        // выполняя команду.
        public void DoSomethingImportant()
        {
            Console.WriteLine("Invoker: Does anybody want something done before I begin?");
            bool isOnStartICommand = this._onStart is ICommand;
            if (isOnStartICommand)
            {
                this._onStart.Execute();
            }

            Console.WriteLine("Invoker: ...doing something really important...");

            Console.WriteLine("Invoker: Does anybody want something done after I finish?");
            bool isOnFinishICommand = this._onFinish is ICommand;
            if (isOnFinishICommand)
            {
                this._onFinish.Execute();
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // EN: The client code can parameterize an invoker with any
            // commands.
            //
            // RU: Клиентский код может параметризовать отправителя любыми
            // командами.
            Invoker invoker = new Invoker();
            string simpleCommandMsg = "Say Hi!";
            SimpleCommand simpleCommand = new SimpleCommand(simpleCommandMsg);

            invoker.SetOnStart(simpleCommand);

            string complexCommandMsg1 = "Send email";
            string complexCommandMsg2 = "Save report";
            Receiver receiver = new Receiver();
            ComplexCommand complexCommand = new ComplexCommand(receiver, complexCommandMsg1, complexCommandMsg2);
            invoker.SetOnFinish(complexCommand);

            invoker.DoSomethingImportant();
        }
    }
}