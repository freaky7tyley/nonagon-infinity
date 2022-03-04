using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace solarlogMailParser
{
	public class DataModel : DbContext
	{

		public DbSet<Address> Addresses { get; set; } = default!;
		public DbSet<Street> Streets { get; set; } = default!;
		public DbSet<City> Cities { get; set; } = default!;
		public DbSet<Email> Emails { get; set; } = default!;
		public DbSet<PVSite> PVSites { get; set; } = default!;
		public DbSet<MessageSubscriber> MessageSubscribers { get; set; } = default!;
		public DbSet<EmailAccount> EmailAccounts { get; set; } = default!;
		public DbSet<AppSetting> AppSettings { get; set; } = default!;
		public DbSet<SolarLog> SolarLogs { get; set; } = default!;


		public string DbPath { get; }

		public DataModel()
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			DbPath = System.IO.Path.Join(path, "Solarlog.db");
		}

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
		}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
    #region misc
    public class Address
	{
		public City cityFK { get; set; } = default!;

		public Street streetFK { get; set; } = default!;

		public string number { get; set; } = default!;
	}

	public class Street
	{
		[Key]
		public string street { get; set; } = default!;
	}

	public class City
	{
		public string city { get; set; } = default!;
		public string postalCode { get; set; } = default!;
	}

	public class Email
	{
		[Key]
		public string address { get; set; } = default!;
	}

	public class PVSite
	{
		[Key]
		public ulong serial { get; set; } = default!;
		public Address addressFK { get; set; } = default!;
		public bool monitored { get; set; } = true;
	}

	public class MessageSubscriber
	{
		[Key]
		public Email emailFK { get; set; } = default!;
	}

	public class EmailAccount
	{
		[DefaultValue("1")]
		[Key]
		public int ID { get; set; }
		public Email emailFK { get; set; } = default!;
		public string mailUrl { get; set; } = default!;
		public string mailUser { get; set; } = default!;
		public string mailPW { get; set; } = default!;
        public int port { get; set; }
    }

	public class AppSetting
    {
		[DefaultValue("1")]
		[Key]
		public int ID { get; set; }
		public string someSetting { get; set; } = default!;
        public int daylyProfitAlarmDifference { get; set; }
    }



	#endregion

	#region LogData
	/// <summary>
    /// Mail schaut so aus
    /// </summary>
	/*Tag:
	Summe        44.21 kWh
	Spez.         4.72 kWh/kWp
	Soll         24.15 kWh
	Ist-Ertrag     183 %
	Verbrauchszähler        28.71 kWh

	Monat:
	Summe          117 kWh
	Spez.         12.5 kWh/kWp
	Mittel        39.2 kWh
	Soll          72.4 kWh
	Ist-Ertrag     162 %

	Jahr:
	Summe         1041 kWh
	Spez.          111 kWh/kWp
	*/

	public class SolarLog
	{
		[Key]
		public int ID { get; set; }
		[Timestamp]
		public DateTime timestamp { get; set; } = default!;
		public PVSite site { get; set; } = default!;

        public double Summe_Day { get; set; } = default!;
		public double Spez_Day { get; set; } = default!;
		public double Soll_Day { get; set; }
		public double IstErtrag_Day { get; set; } = default!;
		public double Verbrauchstzaehler_Day { get; set; }

		public double Summe_Month { get; set; }
		public double Spez_Month { get; set; }
		public double Mittel_Month { get; set; }
		public double Soll_Month { get; set; }
		public double IstErtrag_Month { get; set; }

		public double Summe_Year { get; set; }
		public double Spez_Year { get; set; }
	}

	#endregion
}

