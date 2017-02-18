using System.Collections.Generic;
using System.Threading.Tasks;

namespace StuffOnHarold.Interfaces {
	public interface ITalkToServers {
		Task<List<string>> GetImageList();

		Task<byte[]> DownloadImage(string image);
	}
}
