namespace Api
{
    public class Restriccion
    {
        /*
         * como se define una restriccion
         * tipo = si es <= or = or =>
         * coeficiencies = si una restriccion es x + y + z <= 400 <---
         * x = 1, y = 1, z = 1                                        |
         * valor = para el caso de la restricción de arriba sería 400-|
        */

        public required string Tipo { get; set; }
        public required double[] Coeficientes { get; set; }
        public double Valor { get; set; }
    }

}
