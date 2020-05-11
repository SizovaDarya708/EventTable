using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventTable.Models.Entities
{
    /// <summary>
    /// Сущность событие/мероприятие
    /// </summary>
    public class Event
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public DateTime Happen_date { get; set; }
        public DateTime Notify_date { get; set; }
        public IList<UserEvents> UserEvents { get; set; }
    }   
}
