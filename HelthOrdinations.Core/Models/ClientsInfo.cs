using System;
namespace HelthOrdinations.Core.Models
{
	public class ClientsInfo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public int UserStatusId { get; set; }
		public ClientsInfo()
		{
		}
	}
}

