using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventTable.Models.Entities
{
    public class Note
    {
		[Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string NoteText { get; set; }
		public string MessageId { get; set; }
		public DateTime HappenDate { get; set; }
		public DateTime	NotifyDate  { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
