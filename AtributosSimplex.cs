namespace Api
{
    public class AtributosSimplex
    {
        /*
         * funcion chida
         * (lado derecho = los numerotes = contraintsvalues)    
         * 
         * x + y + x <= 400 <- Constraints
         * x + y + 2x <= 500 <- Constraints
         * 2x + 3y + 5z <= 1450 <- Constraints
         * los <= son los ConstraintTypes (pueden ser =, =>)
         * 
         * U = 21x + 24y + 36z <- funcion objetivo que sería la ObjectiveFunction
         * 
        */

        public required double[] ObjectiveFunction { get; set; }
        public required double[][] Constraints { get; set; }
        public required double[] ConstraintsValues { get; set; }
        public bool IsMaximization { get; set; }
        public required string[] ConstraintTypes { get; set; }
    }
}
