using System;
using System.ComponentModel.DataAnnotations;

namespace Pulsenics.Models
{
	public class User
	{
		public User(){}
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<UserFile> UserFiles { get; set; } = new List<UserFile>();

    }
}

