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
			var ipsToCheck = new IPCheckList().GetCheckItems().ToList();
			Console.WriteLine($"Received {ipsToCheck.Count()} items to check");
            ipsToCheck = ipsToCheck.Select(i => new BarracudaValidation().ValidateBarracudaBlacklist(i)).ToList();
			ipsToCheck.ForEach(i => EchoIPConfig(i));
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
			System.Console.WriteLine($"{messagePrefix} {check.IPToCheck} ({check.Name}) {check.Message}");
			Console.ForegroundColor = oldColor;
		}
    }
}
