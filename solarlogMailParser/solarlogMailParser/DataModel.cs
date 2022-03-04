using System;
namespace solarlogMailParser
{
	public class DataModel
	{
		public DataModel()
		{
		}
	}
	public class Address
	{
		public City cityFK { get; set; } = default!;

		public Street streetFK { get; set; } = default!;

		public string number { get; set; } = default!;
    }

	public class Street
	{
		public string street { get; set; } = default!;
	}

	public class City
	{
		public string city { get; set; } = default!;
		public string postalCode { get; set; } = default!;
	}

	public class PVSite
    {
		public ulong serial { get; set; } = default!;
		public Address addressFK { get; set; } = default!;
		public bool monitoring { get; set; } = true;
	}

}

