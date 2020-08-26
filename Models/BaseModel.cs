using System;
using System.ComponentModel.DataAnnotations;

namespace PullEvikeSpecials.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}