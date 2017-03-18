namespace StuffOnHarold.Models {
	public struct Stuff {
		public string Name { get; set; }
		public int Weight { get; set; }
		public string Grams => $"{Weight}g";
	}
}
