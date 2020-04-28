using System;
using System.Collections.Generic;
using System.Text;

namespace EventTable.Models.Entities
{
    /// <summary>
    /// Сущность событие/мероприятие
    /// </summary>
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string Place { get; set; }
    }
}
