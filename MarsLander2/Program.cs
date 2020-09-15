using System;

namespace MarsLander2
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }


    /** * Auto-generated code below aims at helping you parse * the standard input according to the problem statement. **/
    class Player
    {
        static void Main(string[] args)
        {
            string[] inputs; int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.        
            for (int i = 0; i < surfaceN; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)            
                int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.        

            }
            double a = -3.711;
            double modhSpeed = 0;
            double modvSpeed = 0;
            double modX = 0; double modY = 0; int loop = 0;
            // game loop       
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int X = int.Parse(inputs[0]);
                int Y = int.Parse(inputs[1]);
                int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.            
                int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.            
                int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.            
                int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).            
                int power = int.Parse(inputs[6]); // the thrust power (0 to 4).
                if (loop == 0)
                {
                    modhSpeed = hSpeed;
                    modvSpeed = vSpeed;
                    modX = X;
                    modY = Y;
                }
                loop++;
                int modRotate = GetNextRotate(rotate, -20); int modPower = GetNextPower(power, 3);
                double modRad = (System.Math.PI / 180) * (90 + modRotate);

                // Write an action using Console.WriteLine()            
                // To debug: Console.Error.WriteLine("Debug messages...");            
                modX = modX + modhSpeed + System.Math.Cos(modRad) * modPower / 2;
                modhSpeed = modhSpeed + System.Math.Cos(modRad) * modPower;
                modY = modY + modvSpeed + (System.Math.Sin(modRad) * modPower + a) / 2;
                modvSpeed = modvSpeed + System.Math.Sin(modRad) * modPower + a;

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
                        "Fuel={0}l, Angle={1}°, Power={2}",
                        fuel - modPower,
                        modRotate,
                        modPower
                        )
                    );
                // rotate power. rotate is the desired rotation angle. power is the desired thrust power.            
                Console.WriteLine(String.Format("{0} {1}", modRotate, modPower));
            }
        }
        public static int GetNextPower(int power, int targetPower)
        {
            if (targetPower > power)
            {
                return power <= 3 ? power + 1 : power;
            }
            else if (targetPower < power)
            {
                return power >= 1 ? power - 1 : power;
            }
            return power;
        }
        public static int GetNextRotate(int rotate, int targetRotate)
        {
            if (targetRotate > rotate)
            {
                return rotate + Math.Min(45 - rotate, Math.Min(targetRotate - rotate, 15));
            }
            else if (targetRotate < rotate)
            {
                return rotate + Math.Max(-45 - rotate, Math.Max(targetRotate - rotate, -15));
            }
            return rotate;
        }
    }
    class Gene
    {
        public int Rotate { get; set; }
        public int Power { get; set; }

    }


}