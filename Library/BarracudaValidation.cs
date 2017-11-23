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

		public IPCheck ValidateBarracudaBlacklist(IPCheck check)
		{
			check.Status = CheckDNSRecordInBarracuda(check.IPToCheck);
			return check;
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
				if(host?.AddressList?.Any() == true)
				{
					System.Console.WriteLine("Element has at least one address match, indicating this is a blacklisted entry");
					return IPCheckStatus.BlackListed;
				}
				System.Console.WriteLine("No addresses found, but the item resolved, unknown status");
				return IPCheckStatus.Unknown;
			}
			catch(Exception e)
			{
				if(string.Equals("Device not configured", e.Message, StringComparison.OrdinalIgnoreCase))
				{
					System.Console.WriteLine("No match found in the barracuda list");
					return IPCheckStatus.NotOnBlackList;
				}
				Console.WriteLine($"Error: {e.Message}");
				return IPCheckStatus.Unknown;
			}
		}
	}
}