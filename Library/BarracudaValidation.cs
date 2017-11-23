using System.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace IPBlackListCheck.Library
{
	public class BarracudaValidation
	{
		private string ReverseIP(string ip)
		{
			if(ip.Count(c => c == '.') != 3) { throw new Exception($"{ip} is not an IP address"); }
			var split = ip.Split(new char[] { '.'}).ToList();
			split.Reverse();
			return string.Join(".", split);
		}

		private const string BarracudaSuffix = "b.barracudacentral.org";

		public void ReadBarracudaResponse(IPCheck check)
		{
			
			check.Status = CheckDNSRecordInBarracuda(check.IPToCheck);
		}

		private IPCheckStatus CheckDNSRecordInBarracuda(string hostname)
		{
			try 
			{
				string reversedIp = ReverseIP(hostname);
				string nsToLookup = $"{reversedIp}.{BarracudaSuffix}";
				Console.WriteLine($"Trying to lookup: {nsToLookup}");
				

				IPHostEntry host = Dns.GetHostEntry(nsToLookup);
				System.Console.WriteLine($"{host.HostName}");
				host?.Aliases.ToList().ForEach(a => System.Console.WriteLine($"Alias: {a}"));
				host?.AddressList.ToList().ForEach(a => System.Console.WriteLine($"Address: {a}"));
				return IPCheckStatus.Unknown;
			}
			catch(Exception e)
			{
				Console.WriteLine($"Error: {e.Message}");
				return IPCheckStatus.Unknown;
			}
		}
	}
}