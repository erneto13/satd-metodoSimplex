namespace Api
{
    public class Simplex
    {
        public Resultado Solve(double[] objectiveFunction, double[][] constraints, double[] constraintsValues, string[] constraintTypes, bool isMaximization)
        {
            /* numero de variables y restricciones. ej: 3x3
             * 
             * x + y + z <= 400
             * x + y + 2z <= 500
             * 2x + 3y + 5z <= 1450
             * 
             */
            int numVariables = objectiveFunction.Length;
            int numConstraints = constraints.Length;

            
            int totalVariables = numVariables + numConstraints; // holguras
            double[,] tableau = new double[numConstraints + 1, totalVariables + 1]; // +1 para el valor de la función objetivo y generar la matriz

            // llenar la tabla con la f.obj
            for (int j = 0; j < numVariables; j++)
            {
                tableau[0, j] = isMaximization ? objectiveFunction[j] : -objectiveFunction[j]; // optimo: max o min si es min * -1
            }

            // se llena la tabla con las restricciones
            for (int i = 0; i < numConstraints; i++)
            {
                for (int j = 0; j < numVariables; j++)
                {
                    tableau[i + 1, j] = constraints[i][j]; // c.e de las restricciones
                }

                // case para el tipo de restriccion
                switch (constraintTypes[i])
                {
                    case "<=":
                        tableau[i + 1, numVariables + i] = 1; // holgura
                        break;
                    case ">=":
                        tableau[i + 1, numVariables + i] = -1; // exceso
                        constraintsValues[i] = -constraintsValues[i]; // negar LD
                        break;
                    case "=":
                        tableau[i + 1, numVariables + i] = 1; // igualidad
                        tableau[numConstraints, numVariables + i] = -1; 
                        constraintsValues[i] = -constraintsValues[i]; // negar LD
                        break;
                }

                tableau[i + 1, tableau.GetLength(1) - 1] = constraintsValues[i];
            }

            // lo "chido"
            while (true)
            {
                PrintTableau(tableau); 

                int pivote = -1; // col pivote
                double laruina = 0; // el valor más bajo

                // primero se busca en toda la tabla el valor más bajo
                for (int j = 0; j < tableau.GetLength(1) - 1; j++)
                {
                    if (tableau[0, j] < laruina)
                    {
                        laruina = tableau[0, j]; // actualizamos el valor más bajo 
                        pivote = j; // actualizamos la columna pivote
                    }
                }

                if (pivote == -1) break; 

                int pivoteFila = -1; // fila pivote
                double minimumRatio = double.MaxValue; // r.minima

                // ahora columna con el vsalor más bajo 
                for (int i = 1; i < tableau.GetLength(0); i++)
                {
                    if (tableau[i, pivote] > 0)
                    {
                        double ratio = tableau[i, tableau.GetLength(1) - 1] / tableau[i, pivote];
                        if (ratio < minimumRatio)
                        {
                            minimumRatio = ratio; // actualiza el valor más bajo
                            pivoteFila = i; // actualiza la fila pivote
                        }
                    }
                }

                if (pivoteFila == -1) throw new Exception("el problema no tiene solucion.");

                double pivotValue = tableau[pivoteFila, pivote]; // valor pivote
                for (int j = 0; j < tableau.GetLength(1); j++)
                {
                    tableau[pivoteFila, j] /= pivotValue;
                }

                for (int i = 0; i < tableau.GetLength(0); i++)
                {
                    if (i != pivoteFila) // si no es la fila pivote
                    {
                        double factor = tableau[i, pivote];
                        for (int j = 0; j < tableau.GetLength(1); j++)
                        {
                            tableau[i, j] -= factor * tableau[pivoteFila, j];
                        }
                    }
                }
            }

            double[] solution = new double[numVariables]; // solucion final
            for (int i = 0; i < numConstraints; i++)
            {
                for (int j = 0; j < numVariables; j++)
                {
                    if (tableau[i + 1, j] == 1) // la real
                    {
                        solution[j] = tableau[i + 1, tableau.GetLength(1) - 1];
                    }
                }
            }

            return new Resultado // retorna un objeto de tipo resultado con los atributos de la clase para mostarlos en la api
            {
                Solution = solution,
                OptimalValue = tableau[0, tableau.GetLength(1) - 1],
                IsOptimal = true
            };
        }

        // cosos pa imprimir la tabla
        private void PrintTableau(double[,] tableau)
        {
            int rows = tableau.GetLength(0);
            int cols = tableau.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{tableau[i, j],10:F2} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
