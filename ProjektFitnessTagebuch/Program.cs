namespace ProjektFitnessTagebuch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Variablen für Benutzereingaben
            string geschlecht;
            int alter;
            double gewicht, größe;
            int aktivitätslevel;

            // Begrüßung und Eingabeaufforderungen
            Console.WriteLine("Willkommen zum Fitness-Tagebuch!");
            Console.WriteLine("Wir berechnen deinen täglichen Kalorien- und Nährstoffbedarf.");
            Console.WriteLine(); // Leerzeile für bessere Lesbarkeit 

            // Eingabe des Geschlechts, Alters, Gewichts, Größe und Aktivitätslevels
            Console.Write("Bitte gib dein Geschlecht ein (m/w): ");
            geschlecht = Console.ReadLine();

            Console.Write("Bitte gib dein Alter ein (in Jahren): ");
            alter = int.Parse(Console.ReadLine());

            Console.Write("Bitte gib dein Gewicht ein (in kg): ");
            gewicht = double.Parse(Console.ReadLine());

            Console.Write("Bitte gib deine Größe ein (in cm): ");
            größe = double.Parse(Console.ReadLine()); 

            Console.WriteLine();
            Console.WriteLine("Aktivitätslevel:(Wähle eine Zahl aus)");
            Console.WriteLine("1 - Wenig oder keine Bewegung");
            Console.WriteLine("2 - Leichte Bewegung (1-3 Tage pro Woche)");
            Console.WriteLine("3 - Mäßige Bewegung (3-5 Tage pro Woche)");
            Console.WriteLine("4 - Intensive Bewegung (6-7 Tage pro Woche)");
            Console.Write("Deine Wahl: ");
            aktivitätslevel = int.Parse(Console.ReadLine());


        }
    }
}
