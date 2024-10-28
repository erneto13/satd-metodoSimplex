namespace Api
{
    public class Resultado
    {
        /*
         * en la estructura de la solucion a un problema del metodo simplex
         * solucion = LD de la última tabla donde se encontro la S.OP.
         * ej:   x          y          z         s1         s2          U       LD
         *       3.00       0.00       0.00      12.00      12.00       0.00   10800.00 <- solucion optima
                 1.00       1.00       0.00       2.00      -1.00       0.00     300.00 <- solucion
                 0.00       0.00       1.00      -1.00       1.00       0.00     100.00 <- solucion
                 -1.00       0.00       0.00      -1.00      -2.00       1.00    100.00 <- solucion
         * 
         * optimalvalue = a la solucion optima del problema, en este ejemplo 10,800 pelucholares
         * isoptimal = si la solucion es optima o no xd
        */

        public required double[] Solution { get; set; }
        public double OptimalValue { get; set; }
        public bool IsOptimal { get; set; }
    }
}
