using Microsoft.EntityFrameworkCore;
using backPulso.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace backPulso
{
    public class AplicationDbContext_DC
    {

        public string conexionBD = @"C:\Candidatos\Geovanny\Prueba\Services\DataBase\usuarioRe.json"; //PARA QUE SE USA??? 

        public AplicationDbContext_DC()
        {
            using StreamReader rd = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Database/BBDD_DC.json"));
            heroesDC = JsonConvert.DeserializeObject<List<heroes>>(rd.ReadToEnd());
        }

        public List<heroes> heroesDC;



       
        
    }
}
