using System;

namespace DownloadPhotos.InputOutput
{
	internal class ConsoleInputOutput : IInputOutput
	{
		public string? Read()
		{
			return Console.ReadLine();
		}

		public void Write(string input)
		{
			Console.WriteLine(input);
		}
	}
}
