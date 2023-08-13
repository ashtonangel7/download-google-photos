namespace DownloadPhotos.InputOutput
{
	internal interface IInputOutput
	{
		void Write(string input);
		string? Read();
	}
}
