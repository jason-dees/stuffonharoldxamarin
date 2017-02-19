using System.IO;

namespace StuffOnHarold.Interfaces {
	public interface IHelpWithFiles {

		byte[] GetFile(string fileName);

		Stream GetFileStream(string fileName);

		bool DoesFileExist(string fileName);

		void SaveFile(string fileName, byte[] data);

		void DeleteFile(string fileName);

		string GetFullPath(string fileName);
	}
}
