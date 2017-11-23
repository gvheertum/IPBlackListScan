namespace IPBlackListCheck.Library
{
	public class IPCheck
	{
		public IPCheck() {}
		public IPCheck(string name, string ip) 
		{
			Name = name;
			IPToCheck = ip;
		}
		public string Name {get;set;}
		public string IPToCheck { get; set; }
		public string Message {get;set;}
		public IPCheckStatus Status {get;set;}
	}
	
}