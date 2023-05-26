using System;
namespace Pulsenics.Models
{
	public class UserFile
	{
		public UserFile()
		{
		}
        public int UserId { get; set; }
        public User User { get; set; }

        public int FileId { get; set; }
        public File File { get; set; }
    }
}

