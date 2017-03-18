using StuffOnHarold.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using StuffOnHarold.Implementations;
using StuffOnHarold.Models;

[assembly: Dependency(typeof(ServerCommunicator))]
namespace StuffOnHarold.Implementations {
	public class ServerCommunicator : ITalkToServers {

		private readonly HttpClient _client;
		private readonly string _baseAddress;

		public ServerCommunicator() {
			_client = new HttpClient();
			_baseAddress = "http://stuffonharold.jhdees.com/";
			_client.BaseAddress = new Uri(_baseAddress);
		}

		public async Task<byte[]> DownloadImage(string image) {
			return await _client.GetByteArrayAsync(image);
		}

		public async Task<List<string>> GetImageList() {
			var imageListResponse = await _client.GetAsync("images.php");
			if(!imageListResponse.IsSuccessStatusCode){
				throw new Exception("Server Not Working");
			}
			return JsonConvert.DeserializeObject<List<string>>(await imageListResponse.Content.ReadAsStringAsync());
		}

		public async Task<List<Stuff>> GetStuffList() {
			var stuffListResponse = await _client.GetAsync("stuff.json");

			return JsonConvert.DeserializeObject<List<Stuff>>(await stuffListResponse.Content.ReadAsStringAsync());
			
		}
	}
}
