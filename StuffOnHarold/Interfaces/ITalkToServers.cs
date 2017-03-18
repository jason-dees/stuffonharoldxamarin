using System.Collections.Generic;
using System.Threading.Tasks;
using StuffOnHarold.Models;

namespace StuffOnHarold.Interfaces {
	public interface ITalkToServers {
		Task<List<string>> GetImageList();

		Task<List<StuffStruct>> GetStuffList();

		Task<byte[]> DownloadImage(string image);
	}
}
