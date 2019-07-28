using System;
using IPBlackListCheck.Library;
using System.Linq;
using System.Collections.Generic;

namespace IPBlackListCheck
{
    class Program
    {
        static void Main(string[] args)
        {
			var ipsToCheck = new IPListRetriever().GetIpList().ToList();
			Console.WriteLine($"Received {ipsToCheck.Count()} items to check");
			
            var checkedIPs = new IPListingChecker().ValidateAddresses(ipsToCheck);
			checkedIPs.ToList().ForEach(i => EchoIPConfig(i));
        }

		private static void EchoIPConfig(IPCheck check)
		{
			var oldColor = Console.ForegroundColor;
			var newColor = oldColor;
			string messagePrefix = "";
			switch(check.Status)
			{
				case IPCheckStatus.BlackListed: newColor = ConsoleColor.Red; messagePrefix = "X"; break;
				case IPCheckStatus.NotOnBlackList:
				case IPCheckStatus.WhiteListed: newColor = ConsoleColor.Green; messagePrefix = "✓"; break;
				case IPCheckStatus.Unknown:  newColor = ConsoleColor.Yellow;  messagePrefix = "?"; break;
				case IPCheckStatus.Error:  newColor = ConsoleColor.Yellow;  messagePrefix = "X"; break;
				default: break;
			}
			Console.ForegroundColor = newColor;
			System.Console.WriteLine($"{messagePrefix} {(check.IPToCheck).PadRight(16)} {(check.Name).PadRight(20)} {check.Message}");
			Console.ForegroundColor = oldColor;
		}
    }
}
