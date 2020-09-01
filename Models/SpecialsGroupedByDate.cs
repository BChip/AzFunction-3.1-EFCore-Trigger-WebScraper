using System;
using System.Collections.Generic;
using System.Text;

namespace PullEvikeSpecials.Models
{
    public class SpecialsGroupedByDate
    {
        public DateTime Date { get; private set; }
        public string Title { get; private set; }
        public decimal Price { get; private set; }
    }
}
