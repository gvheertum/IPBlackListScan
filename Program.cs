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
            ipsToCheck.ForEach(i => new BarracudaValidation().ReadBarracudaResponse(i));
        }
    }
}
