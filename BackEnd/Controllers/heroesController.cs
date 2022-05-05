using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using backPulso.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backPulso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] //Autorización de token JWT que aplica a todas las peticiones http del controlador
    public class heroesController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        private readonly AplicationDbContext_DC _context_DC;


        public heroesController(AplicationDbContext context, AplicationDbContext_DC context_DC)
        {
            _context = context;
            _context_DC = context_DC;

            //Implemento dos foreach para hacer el set del campo no mapeado bbdd del modelo heroe
            foreach (var heroe in _context.heroesMARVEL)
            {
                heroe.setBbdd("MARVEL");
            }

            foreach (var heroe in _context_DC.heroesDC)
            {
                heroe.setBbdd("DC");
            }

        }

        // GET: api/heroes
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //No solicitar autorización de token JWT en el controlador de login NUNCA.
        public async Task<ActionResult<IEnumerable<heroes>>> Getheroes() //IEnumerable = list
        {

            try {

                var list = (_context.heroesMARVEL).Concat(_context_DC.heroesDC).ToList<heroes>();
                return list;
            }
            catch {
                return NotFound("BD NOT FOUND");
            }
            
        }

        // GET: api/heroes
        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //No solicitar autorización de token JWT en el controlador de login NUNCA.
        public async Task<ActionResult<IEnumerable<heroes>>> GetHeroId(int id)
        {

            //var heroe = (_context.heroesDC).Concat(_context_DC.heroes).ToList<heroes>().Find(x => x.Id == id);
            var heroes = (_context.heroesMARVEL).Concat(_context_DC.heroesDC).ToList<heroes>().Where(x => x.Id == id);

            //var heroes = new List<heroes>();
            //heroes.Add(heroe);

            return heroes.ToArray<heroes>();

        }

        [HttpGet("{bbdd}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //No solicitar autorización de token JWT en el controlador de login NUNCA.
        public async Task<ActionResult<IEnumerable<heroes>>> GetHeroHouseList(string bbdd)
        {
            var heroes = new List<heroes>();

            try {
                if (bbdd == "DC")
                {
                    heroes = (_context_DC.heroesDC).ToList<heroes>();
                }
                else if (bbdd == "MARVEL")
                {
                    heroes = (_context.heroesMARVEL).ToList<heroes>();
                }
            }
            catch {
                return NotFound("BD NOT FOUND");
            }

            
            return heroes;
        }

        [HttpGet("{bbdd}/{nombre}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //No solicitar autorización de token JWT en el controlador de login NUNCA.
        public async Task<ActionResult<IEnumerable<heroes>>> GetHeroNumber(string? bbdd , string nombre)
        {
            var result = new List<heroes>();
            var heroe = new Models.heroes();

            try
            {
                if (bbdd == "DC") {
                    heroe = _context_DC.heroesDC.ToList<heroes>().Find(x => x.nombre == nombre); //Find me devuelve un objeto (modelo) héroe
                }
                else if(bbdd == "MARVEL") {
                    heroe = _context.heroesMARVEL.ToList<heroes>().Find(x => x.nombre == nombre);
                }

                result.Add(heroe);
            }
            catch {
                return NotFound("BD NOT FOUND");
            }


            return result;
        }




        // PUT: api/Hero/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHero(int id, heroes hero)
        {
            if (id != hero.Id)
            {
                return BadRequest();
            }


           


            return NoContent();
        }

        // POST: api/Hero
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<heroes>> PostHero(heroes hero)
        {

            
            return Ok("Saved");
        }



        // DELETE: api/Hero/5
        [HttpDelete("{house}/{id}")]
        public async Task<IActionResult> DeleteheroeHouse(string house, int id)
        {

           
            return NoContent();
        }

       



    }
}
