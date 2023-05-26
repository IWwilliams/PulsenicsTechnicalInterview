using System;
using System.ComponentModel.DataAnnotations;

namespace Pulsenics.Models
{
	public class File
	{

        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public List<UserFile> UserFiles { get; set; } = new List<UserFile>();

    }
}


