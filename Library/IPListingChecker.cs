using System.Linq;
using System;
using System.Collections.Generic;
using System.Net;
namespace IPBlackListCheck.Library
{
	public class IPListingChecker
	{
		public IEnumerable<IPCheck> ValidateAddresses(IEnumerable<IPCheck> ips)
		{
			var ipList = ips.ToList();
			System.Threading.Tasks.Parallel.ForEach(ipList, i => new BarracudaValidation().ValidateBarracudaBlacklist(i));
			return ipList;
		}

		public IEnumerable<IPCheck> GetDefaultCheckList() 
		{
			return new IPCheckList().GetCheckItems();
		}
	}
}