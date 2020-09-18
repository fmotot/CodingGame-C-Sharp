using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.CodeDom;

/** * Auto-generated code below aims at helping you parse * the standard input according to the problem statement. **/
class Player
{
    public static Land land;
    static void Main(string[] args)
    {
        land = new Land();
        string[] inputs; int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.        
        for (int i = 0; i < surfaceN; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            //int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
            //int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
            land.Add(new Point(int.Parse(inputs[0]), int.Parse(inputs[1])));
        }

        double modhSpeed = 0;
        double modvSpeed = 0;
        double modX = 0;
        double modY = 0;
        int loop = 0;
        // game loop       
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int X = int.Parse(inputs[0]);
            int Y = int.Parse(inputs[1]);
            int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
            int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
            int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
            int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
            int power = int.Parse(inputs[6]);

            if (loop == 0)
            {
                modhSpeed = hSpeed;
                modvSpeed = vSpeed;
                modX = X;
                modY = Y;
            }
            loop++;

            int modRotate = 0;
            int modPower = 0;
            // Write an action using Console.WriteLine()            
            // To debug: Console.Error.WriteLine("Debug messages...");         

            modRotate = GetNextRotate(rotate, gene.Rotate);
            modPower = GetNextPower(power, gene.Power);
            double modRad = (System.Math.PI / 180) * (90 + modRotate);

            modX = modX + modhSpeed + System.Math.Cos(modRad) * modPower / 2;
            modhSpeed = modhSpeed + System.Math.Cos(modRad) * modPower;
            modY = modY + modvSpeed + (System.Math.Sin(modRad) * modPower + Land.G) / 2;
            modvSpeed = modvSpeed + System.Math.Sin(modRad) * modPower + Land.G;

            // rotate power. rotate is the desired rotation angle. power is the desired thrust power.            
            Console.WriteLine(modRotate + " " + modPower);
        }
    }
    public static int GetNextPower(int power, int diffPower)
    {
        if (power + diffPower > Module.MAX_POWER)
        {
            return Module.MAX_POWER;
        }
        else if (power + diffPower < Module.MIN_POWER)
        {
            return Module.MIN_POWER;
        }
        return power + diffPower;
    }
    public static int GetNextRotate(int rotate, int diffRotate)
    {
        if (rotate + diffRotate > Module.MAX_ROTATE)
        {
            return Module.MAX_ROTATE;
        }
        else if (rotate + diffRotate < Module.MIN_ROTATE)
        {
            return Module.MIN_ROTATE;
        }
        return rotate + diffRotate;
    }
}

class Land
{
    public const double G = -3.711;

    public List<Point> Surface { get; set; }

    public Land()
    {
        Surface = new List<Point>();
    }
    public void Add(Point landPoint)
    {
        Surface.Add(landPoint);
    }
}

class Point
{
    public double X { get; set; }
    public double Y { get; set; }
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
}

class Collision
{
    public Point Point { get; set; }
    public Collision(Point p1, Point p2, Land land)
    {
        int l = 0;

        while (!State && l < land.Surface.Count - 1)
        {
            Point s1 = land.Surface.ElementAt(l);
            Point s2 = land.Surface.ElementAt(l + 1);

            if ((s1.X < p1.X && p1.X < s2.X)
                || (s1.X < p2.X && p2.X < s2.X))
            {
                Point intersection = Intersection(p1, p2, s1, s2);
                if (Math.Min(p1.X, p2.X) <= intersection.X && intersection.X <= Math.Max(p1.X, p2.X))
                {
                    Point = intersection;
                    State = true;
                }
            }

            l++;
        }
    }

    public bool State { get; private set; }

    private Tuple<double, double> Affine(Point p1, Point p2)
    {
        double a = (p2.Y - p1.Y) / (p2.X - p1.X);
        double b = p1.Y - a * p1.X;

        return new Tuple<double, double>(a, b);
    }

    private Point Intersection(Point p1, Point p2, Point q1, Point q2)
    {
        Tuple<double, double> line1 = Affine(p1, p2);
        Tuple<double, double> line2 = Affine(q1, q2);

        double x = (line2.Item2 - line1.Item2) / (line1.Item1 - line2.Item1);
        double y = line1.Item1 * x + line1.Item2;

        return new Point(x, y);
    }
}

class Module
{
    public const int NB_POPULATION = 20;
    public const int MAX_ROTATE = 45;
    public const int MIN_ROTATE = -45;
    public const int MAX_POWER = 4;
    public const int MIN_POWER = 0;
    public int Power { get; set; }
    public int Rotate { get; set; }
    public double HSpeed { get; set; }
    public double VSpeed { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public int Fuel { get; set; }


    private List<Chromosome> Population { get; set; }

    public Module()
    {

    }

    public void populate()
    {
        for (int p = 0; p < NB_POPULATION; p++)
        {
            Population.Add(new Chromosome());
        }
    }

    public void Log()
    {
        Console.Error.WriteLine("Landing in progress");
        Console.Error.WriteLine(
            String.Format(
                "X={0}m, Y={1}m, HSpeed={2}m/s VSpeed={3}m/s",
                (int)Math.Round(X),
                (int)Math.Round(Y),
                (int)Math.Round(HSpeed),
                (int)Math.Round(VSpeed)
                )
            );
        Console.Error.WriteLine(
            String.Format(
                "Fuel={0}l, Angle={1}°, Power={2} ({2}.0m/s2)",
                Fuel,
                Rotate,
                Power
                )
            );
    }
}

class Chromosome
{
    public const int MIN_NB_GENE = 40;
    public const int MAX_NB_GENE = 150;
    public const int MUTATION = 5; // Proba de mutation sur 100
    public List<Gene> Genes { get; set; }

    /// <summary>
    /// doit prendre en compte:
    /// - la distance avec la zone d'atterissage
    /// - la vitesse à la collision
    /// - l'angle à la collision
    /// - la présence ou non d'une collision
    /// </summary>
    public double Score
    {
        get
        {
            return 0;
        }
    }

    /// <summary>
    /// Création d'un chromosome de base
    /// </summary>
    public Chromosome()
    {
        Genes = new List<Gene>();

        int nbGene = 0;

        while (nbGene < MIN_NB_GENE)
        {
            this.addGene();
            nbGene++;
        }
    }

    /// <summary>
    /// Croisement de 2 chromosomes
    /// </summary>
    /// <param name="mother">
    /// Parent 1
    /// </param>
    /// <param name="father">
    /// Parent 2
    /// </param>
    public Chromosome(Chromosome mother, Chromosome father)
    {
        Genes = new List<Gene>();

        Genes.AddRange(mother.Genes.GetRange(0, mother.Genes.Count / 2));
        Genes.AddRange(father.Genes.GetRange(mother.Genes.Count / 2, father.Genes.Count / 2));
    }

    public void mutate()
    {
        Random rand = new Random();
        if (rand.Next(0, 100) < MUTATION)
        {
            Genes[rand.Next(0, Genes.Count)] = new Gene();
        }
    }


    /// <summary>
    /// Add a new Gene
    /// </summary>
    public void addGene()
    {
        Genes.Add(new Gene());
    }

    /// <summary>
    /// cut this Gene and those after
    /// </summary>
    /// <param name="gene"></param>
    public void cut(Gene gene)
    {
        Genes.RemoveRange(Genes.FirstOrDefault(x => x == gene), Genes.Count);
    }
}

class Gene
{
    public const int MAX_ROTATE = 15;
    public const int MIN_ROTATE = -16;
    public const int MAX_POWER = 2;
    public const int MIN_POWER = -1;
    public int Rotate { get; set; }
    public int Power { get; set; }

    public Gene()
    {
        Random rand = new Random();
        Rotate = rand.Next(MIN_ROTATE, MAX_ROTATE);
        Power = rand.Next(MIN_POWER, MAX_POWER);
    }

    public override string ToString()
    {
        return String.Format("rotate = {0}, power = {1}", Rotate, Power);
    }
}

