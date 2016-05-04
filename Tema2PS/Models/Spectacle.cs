using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Tema2PS.Models
{
    public class SpectacleContext : DbContext
    {
        public SpectacleContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<SpectacleContext>(null);
        }
        public DbSet<Spectacle> Spectacle { get; set; }
    }

    [Table("Spectacole")]
    public class Spectacle
    {
        [Key]
        public int SpectacleId { get; set; }
        public string Titlu { get; set; }
        public string Regia { get; set; }
        public string Distributia { get; set; }
        public DateTime Premiera { get; set; }
        public int Bilete { get; set; }
    }
}