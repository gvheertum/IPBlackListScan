using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

		private IEnumerable<IPCheck> GetItemsForTest()
		{
			yield return new IPCheck("Barracuda test (should alwas show as blacklisted)", "127.0.0.2");
		}

        private const string IpFileListName = "ip.list";

		private IEnumerable<IPCheck> GetItemsFromIpListFile() 
		{
			//Read the file ip.list
			//Format: IP;Name
            string scanPath = Path.Combine(GetExecutionPath(), IpFileListName);

            if(!System.IO.File.Exists(scanPath))
            {
                System.Console.WriteLine($"No ip.list found, expected: {scanPath}");
                yield break;
            }

            System.Console.WriteLine($"Using IP list: {scanPath}");
            var lines = System.IO.File.ReadAllLines(scanPath).Where(l => !string.IsNullOrWhiteSpace(l));
            System.Console.WriteLine($"Found {lines.Count()} lines in ip.list");
            foreach(var l in lines)
            {
                yield return ReadFromLine(l);
            }
		}
        private IPCheck ReadFromLine(string line)
        {
            //Expecting calling code to do sanity checks
            if(string.IsNullOrWhiteSpace(line)) { throw new Exception("Empty line, sorry"); }
            var tokens = line.Split(new string[] {";"}, 2, StringSplitOptions.RemoveEmptyEntries);
            return new IPCheck()
            {
                IPToCheck = tokens[0],
                Name = tokens.Length > 1 ? tokens[1] : $"{tokens[1]} (no name specified)"
            };
        }

        private string GetExecutionPath()
        {
            return new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName;
        }
	}
}