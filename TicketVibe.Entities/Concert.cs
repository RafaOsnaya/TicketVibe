using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVibe.Entities
{
    public class Concert :EntityBase
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Place { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public DateTime DateEvent { get; set; }
        public int TicketQuantity { get; set; }
        public bool Finalized { get; set; }
        public int GenreId { get; set; }
        public string? ImageUrl { get; set; } 

        // Navigation properties
        public Genre Genre { get; set; } = default!;

    }
}
