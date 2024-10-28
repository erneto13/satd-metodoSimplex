using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/simplex")]
    public class SimplexController : ControllerBase
    {
        private readonly Simplex _simplexSolver;
        private readonly SimplexViaArchivo _simplexSolverFromFile;

        // constructor
        public SimplexController(Simplex simplexSolver, SimplexViaArchivo simplexSolverFromFile)
        {
            _simplexSolver = simplexSolver;
            _simplexSolverFromFile = simplexSolverFromFile;
        }

        // sobrecarga 1: por medio de parametros en json 
        [HttpPost("solve")]
        public IActionResult Solve([FromBody] AtributosSimplex input)
        {
            // esta misma condicion se puede hacer en la otra sobrecarga 
            // para ver si un campo es vacio o si de plano no mando un file (pero no quiero)
            if (input == null || input.ObjectiveFunction == null || input.Constraints == null ||
                input.ConstraintsValues == null || input.ConstraintTypes == null)
            {
                return BadRequest("Los datos de entrada son inválidos.");
            }

            // llama al metodo pa que resuelva
            var result = _simplexSolver.Solve(
                input.ObjectiveFunction,
                input.Constraints,
                input.ConstraintsValues,
                input.ConstraintTypes,
                input.IsMaximization
            );

            return Ok(result);
        }

        // sobrecarga 2: por medio de un archivo txt
        [HttpPost("solve-file")]
        public IActionResult SolveFromFile(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // lee el archivo y lo convierte en un objeto de atributos
            var simplexViaArchivo = new SimplexViaArchivo();
            var atributos = simplexViaArchivo.ReadFromCSV(filePath);

            var simplex = new Simplex();
            var result = simplex.Solve( // llama al metodo pues pa que resuelva
                atributos.ObjectiveFunction,
                atributos.Constraints,
                atributos.ConstraintsValues,
                atributos.ConstraintTypes,
                atributos.IsMaximization
            );

            return Ok(result); // ola
        }
    }
}
