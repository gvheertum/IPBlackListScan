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
        }

		private static void EchoIPConfig(IPCheck check)
		{
			var oldColor = Console.ForegroundColor;
			var newColor = oldColor;
			switch(check.Status)
			{
				case IPCheckStatus.BlackListed: break;
				case IPCheckStatus.NotOnBlackList: break;
				case IPCheckStatus.WhiteListed: break;
				case IPCheckStatus.Unknown: break;
				case IPCheckStatus.Error: break;
				default: break;
			}

			Console.ForegroundColor = oldColor;
		}
    }
}
