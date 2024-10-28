using Api;
public class SimplexViaArchivo
{
    public AtributosSimplex ReadFromCSV(string filePath)
    {
        // lee todas las líneas
        var lines = File.ReadAllLines(filePath);

        // lee el tipo de optimizacion = 1 si es maximizacion, 0 si es minimizacion
        bool isMaximization = int.Parse(lines[0].Trim()) == 1;

        // iniciaalizando los arreglos 
        int numConstraints = int.Parse(lines[1].Trim());
        double[][] constraints = new double[numConstraints][];
        double[] constraintsValues = new double[numConstraints];
        string[] constraintTypes = new string[numConstraints];

        // lee restricciones
        for (int i = 0; i < numConstraints; i++)
        {
            var parts = lines[i + 2].Trim().Split(',');
            constraints[i] = new double[parts.Length - 2]; // -2 por que es para el valor ej 400 y el tipo <=
            for (int j = 0; j < parts.Length - 2; j++)
            {
                constraints[i][j] = double.Parse(parts[j].Trim());
            }
            constraintsValues[i] = double.Parse(parts[parts.Length - 2].Trim());
            constraintTypes[i] = parts[parts.Length - 1].Trim(); // se recuerda que la última es el tipo de restriccion
        }

        // lee la f.obj
        var objectiveParts = lines[numConstraints + 2].Trim().Split(',');
        double[] objectiveFunction = new double[objectiveParts.Length];
        for (int j = 0; j < objectiveParts.Length; j++)
        {
            objectiveFunction[j] = double.Parse(objectiveParts[j].Trim());
        }

        // retorna un objeto del modelo que representan los atributos para el simplex
        // que ya tiene todo lo que el archivo contenía y ya lo procesará el algoritmo
        return new AtributosSimplex
        {
            ObjectiveFunction = objectiveFunction,
            Constraints = constraints,
            ConstraintsValues = constraintsValues,
            ConstraintTypes = constraintTypes,
            IsMaximization = isMaximization
        };
    }
}
