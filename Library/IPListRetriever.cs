using System.Collections.Generic;
namespace IPBlackListCheck.Library
{
    public class IPListRetriever
	{
		public IEnumerable<IPCheck> GetIpList()
		{
			List<IPCheck> c = new List<IPCheck>();
			c.AddRange(GetItemsForTest());
			c.AddRange(GetItemsFromIpListFile());
			return c;
		}

		public IEnumerable<IPCheck> GetItemsForTest()
		{
			yield return new IPCheck("Barracuda test", "127.0.0.2");
		}

		public IEnumerable<IPCheck> GetItemsFromIpListFile() 
		{
			//Read the file ip.list
			//Format: IP;Name
			return new List<IPCheck>();
			//return new IPCheckList().GetCheckItems();
		}
	}
}