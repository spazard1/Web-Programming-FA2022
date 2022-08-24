using System;

namespace DependencyInjection.Services
{
	/*
	*	The only change you should make to this class is to implement an interface.
	*
	*	This class logs output to the console.
	*/
	public class ConsoleLogger
    {
		private static ConsoleLogger instance = new ConsoleLogger(); 

		public static ConsoleLogger Instance { get { return instance; } }

		public ConsoleLogger()
		{
			if (instance != null)
			{
				throw new InvalidOperationException("Tried to create a second ConsoleLogger. That's bad.");
			}
		}

		public void Log(string message)
		{
			Console.WriteLine(message);
		}
    }
}
