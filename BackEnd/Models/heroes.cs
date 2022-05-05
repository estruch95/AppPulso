using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backPulso.Models
{
    public class heroes
    {
        [Key]
        public int Id { get; set; } //getters y setters predeterminados
        public string nombre { get; set; }
        public string imagen { get; set; }
        public DateTime fecha_aparicion { get; set; }
        
        //[NotMapped]
        public string? bbdd { get; set; }




        public string getBbdd() {
            return this.bbdd;
        }

        public void setBbdd(string bbdd) {
            this.bbdd = bbdd;
        }

    }
}

