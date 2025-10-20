using System;
using System.IO; // Für Dateien (StreamWriter, File)
using System.Collections.Generic; // Für die List <string> (Trainingsplan)
using System.Linq; // Brauche ich für die Statistik-Berechnung (.Average())

namespace ProjektFitnessTagebuch
{
    internal class Program
    {
        
        // Hauptmenü
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n--- FITNESS-TAGEBUCH HAUPTMENUE ---");
                Console.WriteLine("1. Registrieren");
                Console.WriteLine("2. Einloggen");
                Console.WriteLine("3. Beenden");
                Console.Write("Deine Wahl: ");

                string wahl = Console.ReadLine();

                if (wahl == "1")
                {
                    Registrieren();
                }
                else if (wahl == "2")
                {
                    Einloggen();
                }
                else if (wahl == "3")
                {
                    Console.WriteLine("Auf Wiedersehen!");
                    break;
                }
                else
                {
                    Console.WriteLine("Ungueltige Eingabe, bitte waehle 1, 2 oder 3.");
                }
            }
        }

        
        // Registrieren
        
        static void Registrieren()
        {
            Console.WriteLine("\n--- Neuer Benutzer registrieren ---");
            Console.Write("Waehle einen Benutzernamen: ");
            string benutzername = Console.ReadLine();
            Console.Write("Waehle ein Passwort: ");
            string passwort = Console.ReadLine();

            using (StreamWriter datei = new StreamWriter("benutzer.txt", true))
            {
                datei.WriteLine(benutzername + "," + passwort);
            }

            Console.WriteLine("Registrierung erfolgreich!");
            Console.WriteLine("Bitte gib nun deine Profildaten ein, um die Einrichtung abzuschliessen.");

            // Ruft die Erfassung der Stammdaten auf
            ProfilDatenErfassen(benutzername);
        }


        // Anmelden

        static void Einloggen()
        {
            Console.WriteLine("\n--- Benutzer-Login ---");
            Console.Write("Benutzername: ");
            string eingegebenerName = Console.ReadLine();
            Console.Write("Passwort: ");
            string eingegebenesPasswort = Console.ReadLine();

            bool eingeloggt = false;

            if (!File.Exists("benutzer.txt"))
            {
                Console.WriteLine("Fehler: Es sind noch keine Benutzer registriert.");
                return;
            }

            string[] zeilen = File.ReadAllLines("benutzer.txt");
            foreach (string zeile in zeilen)
            {
                string[] teile = zeile.Split(',');
                if (eingegebenerName == teile[0] && eingegebenesPasswort == teile[1])
                {
                    eingeloggt = true;
                    break;
                }
            }

            if (eingeloggt)
            {
                Console.WriteLine("\nLogin erfolgreich! Willkommen, " + eingegebenerName + "!");
                BenutzerMenue(eingegebenerName); // Starte das Menü
            }
            else
            {
                Console.WriteLine("Falscher Benutzername oder falsches Passwort.");
            }
        }


        // Benutzermenü

        static void BenutzerMenue(string aktuellerBenutzer)
        {
            while (true)
            {
                Console.WriteLine("\n--- Benutzer Menü (" + aktuellerBenutzer + ") ---");
                Console.WriteLine("1. Profildaten anzeigen / bearbeiten");
                Console.WriteLine("2. Trainingsplan erstellen");
                Console.WriteLine("3. Ernährungstagebuch führen");
                Console.WriteLine("4. Fortschritt verfolgen (Statistik)");
                Console.WriteLine("5. Kalorienrechner");
                Console.WriteLine("6. Abmelden"); 
                Console.Write("Bitte wähle eine Option (1-6): ");

                string benutzerMenüAuswahl = Console.ReadLine();

                if (benutzerMenüAuswahl == "1")
                {
                    ProfilDatenAnzeigen(aktuellerBenutzer);
                }
                else if (benutzerMenüAuswahl == "2")
                {
                    TrainingsplanErstellen(aktuellerBenutzer);
                }
                else if (benutzerMenüAuswahl == "3")
                {
                    ErnährungstagebuchFühren(aktuellerBenutzer);
                }
                else if (benutzerMenüAuswahl == "4")
                {
                    FortschrittVerfolgen(aktuellerBenutzer);
                }
                else if (benutzerMenüAuswahl == "5")
                {
                    
                    KalorienrechnerAnzeigen(aktuellerBenutzer);
                }
                else if (benutzerMenüAuswahl == "6")
                {
                    Console.WriteLine("Du wurdest ausgeloggt.");
                    break;
                }
            }
        }


        // Profildaten anzeigen / bearbeiten 

        static void ProfilDatenAnzeigen(string aktuellerBenutzer)
        {
            Console.WriteLine("\n--- Deine Profildaten ---");
            string dateiName = aktuellerBenutzer + "_profil.txt";

            if (!File.Exists(dateiName))
            {
                Console.WriteLine("Fehler: Konnte deine Profildatei nicht finden.");
                Console.WriteLine("Bitte gib deine Daten einmalig ein.");
                ProfilDatenErfassen(aktuellerBenutzer);
                return;
            }

            string[] zeilen = File.ReadAllLines(dateiName);
            Console.WriteLine("Geschlecht: " + zeilen[0]);
            Console.WriteLine("Alter: " + zeilen[1] + " Jahre");
            Console.WriteLine("Gewicht: " + zeilen[2] + " kg");
            Console.WriteLine("Größe: " + zeilen[3] + " cm");
            Console.WriteLine("Aktivitätslevel: " + zeilen[4] + " (1=wenig, 4=intensiv)");

            Console.WriteLine("\nDrücke 'b', um die Daten zu bearbeiten, oder Enter, um zurückzukehren.");
            string wahl = Console.ReadLine();

            if (wahl == "b")
            {
                ProfilDatenErfassen(aktuellerBenutzer);
            }
        }

        static void ProfilDatenErfassen(string aktuellerBenutzer)
        {
            Console.WriteLine("\n--- Profil-Daten eingeben ---");
            Console.Write("Bitte gib dein Geschlecht ein ('mann'/'frau'): ");
            string geschlecht = Console.ReadLine();

            Console.Write("Bitte gib dein Alter ein (in Jahren): ");
            int alter = int.Parse(Console.ReadLine());

            Console.Write("Bitte gib dein aktuelles Gewicht ein (in kg): ");
            double gewicht = double.Parse(Console.ReadLine());

            Console.Write("Bitte gib deine Größe ein (in cm): ");
            double größe = double.Parse(Console.ReadLine());

            Console.WriteLine("Aktivitätslevel: (Wähle eine Zahl aus)");
            Console.WriteLine("1 - Wenig oder keine Bewegung");
            Console.WriteLine("2 - Leichte Bewegung (1-3 Tage pro Woche)");
            Console.WriteLine("3 - Mäßige Bewegung (3-5 Tage pro Woche)");
            Console.WriteLine("4. Intensive Bewegung (6-7 Tage pro Woche)");
            Console.Write("Deine Wahl: ");

            int aktivitätslevel = int.Parse(Console.ReadLine());

            string dateiName = aktuellerBenutzer + "_profil.txt";
            using (StreamWriter datei = new StreamWriter(dateiName))
            {
                datei.WriteLine(geschlecht);
                datei.WriteLine(alter);
                datei.WriteLine(gewicht);
                datei.WriteLine(größe);
                datei.WriteLine(aktivitätslevel);
            }
            Console.WriteLine("\nDeine Profildaten wurden erfolgreich gespeichert!");
            Console.WriteLine("Drücke Enter, um zum Menü zurückzukehren.");
            Console.ReadLine();
        }



        // Trainingsplan erstellen

        static void TrainingsplanErstellen(string aktuellerBenutzer)
        {
            Console.WriteLine("\n--- Neuen Trainingsplan erstellen ---");
            Console.WriteLine("Gib nacheinander deine Übungen ein (z.B. 'Bankdrücken 3x10').");
            Console.WriteLine("Tippe 'fertig', wenn du alle Übungen eingegeben hast.");

            List<string> übungen = new List<string>();

            while (true)
            {
                Console.Write("Neue Übung (oder 'fertig'): ");
                string eingabe = Console.ReadLine();

                if (eingabe == "fertig")
                {
                    break;
                }
                else
                {
                    übungen.Add(eingabe);
                }
            }

            string dateiName = aktuellerBenutzer + "_trainingsplan.txt";
            using (StreamWriter datei = new StreamWriter(dateiName))
            {
                Console.WriteLine("\nDein neuer Plan wird gespeichert:");
                foreach (string übung in übungen)
                {
                    datei.WriteLine(übung);
                    Console.WriteLine("- " + übung);
                }
            }

            Console.WriteLine("\nTrainingsplan erfolgreich gespeichert!");
            Console.WriteLine("Drücke Enter, um zum Menü zurückzukehren.");
            Console.ReadLine();
        }


        // Ernährungstagebuch führens

        static void ErnährungstagebuchFühren(string aktuellerBenutzer)
        {
            Console.WriteLine("\n--- Neues Ernährungstagebuch ---");

            string datum = DateTime.Now.ToString("dd.MM.yyyy");
            Console.WriteLine("Heutiges Datum: " + datum);

            
            Console.Write("Heutiges Gewicht (in kg): ");
            double gewicht = double.Parse(Console.ReadLine());

            Console.Write("Gesamtkalorienaufnahme (in kcal): ");
            int kalorien = int.Parse(Console.ReadLine());

            Console.Write("Heutige Proteinzufuhr (in g): ");
            int protein = int.Parse(Console.ReadLine());

            Console.Write("Heutige Kohlenhydratzufuhr (in g): ");
            int kohlenhydrate = int.Parse(Console.ReadLine());

            Console.Write("Heutige Fettzufuhr (in g): ");
            int fett = int.Parse(Console.ReadLine());

            
            Console.Write("Heutige Schlafdauer (in h): ");
            double schlaf = double.Parse(Console.ReadLine());

            Console.Write("Notizen: ");
            string notizen = Console.ReadLine();

            string dateiName = aktuellerBenutzer + "_Ernährungstagebuch.txt";

            using (StreamWriter sw = new StreamWriter(dateiName, true))
            {
                
                sw.WriteLine(datum + ";" + gewicht + ";" + kalorien + ";" + protein + ";" + kohlenhydrate + ";" + fett + ";" + schlaf + ";" + notizen);
            }

            Console.WriteLine("Eintrag erfolgreich gespeichert!");
            Console.WriteLine("Drücke Enter, um zum Menü zurückzukehren.");
            Console.ReadLine();
        }


        // Fortschritt verfolgen (Statistik)

        static void FortschrittVerfolgen(string aktuellerBenutzer)
        {
            Console.WriteLine("\n--- Dein Fortschritt (Ernährung) ---");
            string dateiName = aktuellerBenutzer + "_Ernährungstagebuch.txt";

            if (!File.Exists(dateiName))
            {
                Console.WriteLine("Du hast noch keine Einträge im Ernährungstagebuch gemacht.");
                Console.WriteLine("Drücke Enter, um zum Menü zurückzukehren.");
                Console.ReadLine();
                return;
            }

            string[] zeilen = File.ReadAllLines(dateiName);
            if (zeilen.Length == 0)
            {
                Console.WriteLine("Deine Tagebuch-Datei ist noch leer.");
                Console.WriteLine("Drücke Enter, um zum Menü zurückzukehren.");
                Console.ReadLine();
                return;
            }

            // Das sind die Listen für die Statistik
            List<double> alleGewichte = new List<double>();
            List<int> alleKalorien = new List<int>();

            // Das ist die Kopfzeile für die Tabelle
            Console.WriteLine("\nDatum\t\tGewicht\t\tKalorien\tProtein\t\tSchlaf");
            Console.WriteLine("-----------------------------------------------------------------------------------");

            foreach (string zeile in zeilen)
            {
                string[] teile = zeile.Split(';');

                string datum = teile[0];
                double gewicht = double.Parse(teile[1]);
                int kalorien = int.Parse(teile[2]);
                string protein = teile[3];
                string kohlenhydrate = teile[4];
                string fett = teile[5];
                double schlaf = double.Parse(teile[6]);
                string notizen = teile[7];

                // Hier werden die Daten für die Statistik gesammelt
                alleGewichte.Add(gewicht);
                alleKalorien.Add(kalorien);

                // Hier werden die Daten in der Tabelle ausgeben
                Console.WriteLine(datum + "\t" + gewicht + " kg\t\t" + kalorien + " kcal\t" + protein + "g\t\t" + schlaf + "h");
            }

            
            Console.WriteLine("\n--- DEINE STATISTIK ---");

            // Hier wird der Durchschnitt berechnet mit .Average() aus dem System.Linq
            double avgKalorien = alleKalorien.Average();
            Console.WriteLine("Durchschnittl. Kalorienaufnahme: " + Math.Round(avgKalorien, 0) + " kcal");

            // Hier kann man den Gewichtsverlauf sehen
            double startGewicht = alleGewichte[0]; // Das erste gemessene Gewicht
            double aktuellesGewicht = alleGewichte[alleGewichte.Count - 1]; // Das letzte
            double diff = aktuellesGewicht - startGewicht;

            Console.WriteLine("Gewichtsverlauf: " + startGewicht + " kg -> " + aktuellesGewicht + " kg");
            
            Console.WriteLine("Differenz: " + Math.Round(diff, 1) + " kg"); // Mathe.Round rundet auf 1 Nachkommastelle


            Console.WriteLine("\n--- Ende deiner Einträge ---");
            Console.WriteLine("Drücke Enter, um zum Menü zurückzukehren.");
            Console.ReadLine();
        }

        
        // Kalorienrechner
        
        static void KalorienrechnerAnzeigen(string aktuellerBenutzer)
        {
            Console.WriteLine("\n--- Kalorienrechner ---");
            string dateiName = aktuellerBenutzer + "_profil.txt";

            if (!File.Exists(dateiName))
            {
                Console.WriteLine("Fehler: Konnte deine Profildatei nicht finden.");
                Console.WriteLine("Bitte lege deine Profildaten über Menüpunkt 1 an.");
                Console.WriteLine("Drücke Enter, um zum Menü zurückzukehren.");
                Console.ReadLine();
                return;
            }

            // um Profildaten einzulesen
            string[] zeilen = File.ReadAllLines(dateiName);
            string geschlecht = zeilen[0];
            int alter = int.Parse(zeilen[1]);
            double gewicht = double.Parse(zeilen[2]);
            double größe = double.Parse(zeilen[3]);
            int aktivitätslevel = int.Parse(zeilen[4]);

            // Um den Grundumsatz (BMR) zu berechnen nach der Harris-Benedict-Formel
            double bmr = 0;
            if (geschlecht == "mann")
            {
                bmr = 88.362 + (13.397 * gewicht) + (4.799 * größe) - (5.677 * alter);
            }
            else if (geschlecht == "frau")
            {
                bmr = 447.593 + (9.247 * gewicht) + (3.098 * größe) - (4.330 * alter);
            }
            else
            {
                Console.WriteLine("Fehler: 'Geschlecht' in Profildatei ist ungültig (nur 'mann'/'frau').");
                Console.WriteLine("Drücke Enter, um zum Menü zurückzukehren.");
                Console.ReadLine();
                return;
            }

            // Hier wird Aktivitätsfaktor (PAL) bestimmt
            double pal_faktor = 1.0;
            if (aktivitätslevel == 1) pal_faktor = 1.2;
            else if (aktivitätslevel == 2) pal_faktor = 1.375;
            else if (aktivitätslevel == 3) pal_faktor = 1.55;
            else if (aktivitätslevel == 4) pal_faktor = 1.725;

            // Damit berechnet man Gesamtkalorienbedarf 
            double gesamtkalorien = bmr * pal_faktor;

            // Hier werden die Ergebnisse ausgegeben
            Console.WriteLine("\nBasierend auf deinen Profildaten:");
            Console.WriteLine("- Dein Grundumsatz (BMR) ist: " + Math.Round(bmr, 0) + " kcal/Tag");
            Console.WriteLine("- Dein Gesamt-Kalorienbedarf (Erhaltung) ist: " + Math.Round(gesamtkalorien, 0) + " kcal/Tag");

            Console.WriteLine("\nEmpfehlungen:");
            Console.WriteLine("- Zum Abnehmen (Defizit ~500kcal): " + Math.Round(gesamtkalorien - 500, 0) + " kcal/Tag");
            Console.WriteLine("- Zum Zunehmen (Überschuss ~300kcal): " + Math.Round(gesamtkalorien + 300, 0) + " kcal/Tag");

            Console.WriteLine("\nDrücke Enter, um zum Menü zurückzukehren.");
            Console.ReadLine();
        }
    }
}