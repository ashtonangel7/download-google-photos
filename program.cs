using DownloadPhotos.InputOutput;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace DownloadPhotos
{
	class Program
	{

		static void Main(string[] args)
		{
			var inputOutput = new ConsoleInputOutput();
			MakeChoice(inputOutput);
		}

		private static void MakeChoice(IInputOutput inputOutput)
		{
			var choice = GetUserInput(inputOutput);
			switch (choice)
			{
				case "1":
					DownloadMediaItems();
					break;
				case "2":
					inputOutput.Write("Bye!");
					break;
				default:
					inputOutput.Write("Bye!");
					break;
			}

			Console.Clear();
			MakeChoice(inputOutput);
		}

		private static string GetUserInput(IInputOutput inputOutput)
		{
			inputOutput.Write("1: Download Media Items.");
			inputOutput.Write("2: Exit.");
			inputOutput.Write(" ");
			inputOutput.Write("Please make a selection: ");

			string? choice = inputOutput.Read();
			return choice ?? "";
		}

		private static void DownloadMediaItems()
		{
			Console.Write("Please Eneter your access token: ");
			string? accessToken = Console.ReadLine();

			Console.WriteLine(accessToken?.Length);

			if (string.IsNullOrWhiteSpace(accessToken) || accessToken.Length != 210)
			{
				Console.Write("Access token length incorrect. Press any key to continue ...");
				Console.ReadLine();
			}

			string url = "https://photoslibrary.googleapis.com/v1/mediaItems";

			// Create HttpClient instance
			using (var client = new HttpClient())
			{
				// Set authorization header
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applcation/json"));

				// Send GET request to the Google Photos API
				HttpResponseMessage response = client.GetAsync(url).Result;

				if (response.IsSuccessStatusCode)
				{
					// Read the response content
					string responseContent = response.Content.ReadAsStringAsync().Result;

					var node = JsonNode.Parse(responseContent);

					Console.WriteLine(node);
				}
				else
				{
					Console.WriteLine($"Error: {response.StatusCode}");
				}
			}

			// Make rest requests
			// Save results to a local JSON file.
		}
	}
}