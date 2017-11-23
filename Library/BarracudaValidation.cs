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
			var split = ip.Split(new char[] { '.'}).ToList();
			split.Reverse();
			return string.Join(".", split);
		}

		private const string BarracudaSuffix = "b.barracudacentral.org";

		public void ReadBarracudaResponse(IPCheck check)
		{
			string reversedIp = ReverseIP(check.IPToCheck);
			string nsToLookup = $"{reversedIp}.{BarracudaSuffix}";
			Console.WriteLine($"[{check.Name}] Trying to lookup: {nsToLookup}");
			check.Status = CheckDNSRecordInBarracuda(check.IPToCheck);
		}

		private IPCheckStatus CheckDNSRecordInBarracuda(string hostname)
		{
			try 
			{
				IPHostEntry host = Dns.GetHostEntry(hostname);
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