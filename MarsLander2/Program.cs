using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;



/** * Auto-generated code below aims at helping you parse * the standard input according to the problem statement. **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs; int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.        
        for (int i = 0; i < surfaceN; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
            int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.

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

            Gene gene = new Gene();


            int modRotate = 0;
            int modPower = 0;
            // Write an action using Console.WriteLine()            
            // To debug: Console.Error.WriteLine("Debug messages...");         

            modRotate = GetNextRotate(rotate, gene.Rotate) ;
            modPower = GetNextPower(power, gene.Power);
            double modRad = (System.Math.PI / 180) * (90 + modRotate);

            modX = modX + modhSpeed + System.Math.Cos(modRad) * modPower / 2;
            modhSpeed = modhSpeed + System.Math.Cos(modRad) * modPower;
            modY = modY + modvSpeed + (System.Math.Sin(modRad) * modPower + Land.G) / 2;
            modvSpeed = modvSpeed + System.Math.Sin(modRad) * modPower + Land.G;
            Console.Error.WriteLine(gene);
            Console.Error.WriteLine("Landing in progress");
            Console.Error.WriteLine(
                String.Format(
                    "X={0}m, Y={1}m, HSpeed={2}m/s VSpeed={3}m/s",
                    (int)Math.Round(modX),
                    (int)Math.Round(modY),
                    (int)Math.Round(modhSpeed),
                    (int)Math.Round(modvSpeed)
                    )
                );
            Console.Error.WriteLine(
                String.Format(
                    "Fuel={0}l, Angle={1}°, Power={2} ({2}.0m/s2)",
                    fuel - modPower,
                    modRotate,
                    modPower
                    )
                   );


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
    public static double G = -3.711;

    private List<int> surface;

    public List<int> Surface
    {
        get { return surface; }
        set { surface = value; }
    }


}

class Module
{
    public static int MAX_ROTATE = 45;
    public static int MIN_ROTATE = -45;
    public static int MAX_POWER = 4;
    public static int MIN_POWER = 0;
    public int Power { get; set; }
    public int Rotate { get; set; }

    public double HSpeed { get; set; }
    public double VSpeed { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
}


class Chromosome
{
    private List<Gene> genes;

    public Chromosome()
    {
        genes = new List<Gene>();
    }

    public List<Gene> Genes
    {
        get { return genes; }
        set { genes = value; }
    }

    public void addGene(Gene gene)
    {
        genes.Add(gene);
    }
}

class Gene
{
    public static int MAX_ROTATE = 15;
    public static int MIN_ROTATE = -16;
    public static int MAX_POWER = 2;
    public static int MIN_POWER = -1;
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
        return String.Format("rotate = {0}, power = {1}", Rotate, Power) ;
    }
}

