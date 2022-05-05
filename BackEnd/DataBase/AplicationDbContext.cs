using Microsoft.EntityFrameworkCore;
using backPulso.Models;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace backPulso
{
    public class AplicationDbContext 
    {
        public string conexionBD = @"C:\Candidatos\Geovanny\Prueba\Services\DataBase\usuarioRe.json"; //PARA QUE SE USA??? 

        public AplicationDbContext()
        {
            using StreamReader rd = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Database/BBDD_Marvel.json")); //SE APUNTABA A LA BBDD_DC.json
            heroesMARVEL = JsonConvert.DeserializeObject<List<heroes>>(rd.ReadToEnd()); //El nombre de la variable era heroesDC
        }

        public List<heroes> heroesMARVEL;

       

        
    }
}
