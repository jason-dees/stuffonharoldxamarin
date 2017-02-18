using StuffOnHarold.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StuffOnHarold.Implementations {
	public class ServerCommunicator : ITalkToServers {

		private readonly HttpClient _client;
		private readonly string _baseAddress;

		public ServerCommunicator() {
			_client = new HttpClient();
			_baseAddress = "http://stuffonharold.jhdees.local/";
			_client.BaseAddress = new Uri(_baseAddress);
		}

		public async Task<byte[]> DownloadImage(string image) {
			return await _client.GetByteArrayAsync(image);
		}

		public async Task<List<string>> GetImageList() {
			var imageList = await _client.GetStringAsync("images.php");
			return JsonConvert.DeserializeObject<List<string>>(imageList);
		}

	}
}
