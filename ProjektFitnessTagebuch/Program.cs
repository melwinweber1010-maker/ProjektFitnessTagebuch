namespace ProjektFitnessTagebuch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Endlosschleife, um das Programm mehrfach auszuführen
            while (true)
            {         // Hauptmenü anzeigen
                Console.WriteLine("\n--- Fitness-Tagebuch Hautmenü---"); // \n für neue Zeile
                Console.WriteLine("1. Registrieren");
                Console.WriteLine("2. Anmelden");
                Console.WriteLine("3. Beenden");
                Console.Write("Bitte wähle eine Option (1-3): ");

                // Liest die Benutzereingabe
                string hauptmenüAuswahl = Console.ReadLine();

                if (hauptmenüAuswahl == "1")
                {
                    Registrieren();
                }
                else if (hauptmenüAuswahl == "2")
                {
                    Anmelden();
                }
                else if (hauptmenüAuswahl == "3")
                {
                    Console.WriteLine("Programm wird beendet. Auf Wiedersehen!");
                    break; // Beendet die Endlosschleife und somit das Programm
                }
                else
                {
                    Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut.");
                }
                // Methode zur Eingabe der Benutzerdaten
                static void Registrieren()
                {
                    Console.WriteLine("\n--- Registrierung ---");
                    Console.Write("Benutzername: ");
                    string benutzername = Console.ReadLine();
                    Console.Write("Passwort: ");
                    string passwort = Console.ReadLine();

                    using (StreamWriter sw = new StreamWriter("BenutzerDaten.txt", true))
                    {
                        sw.WriteLine($"{benutzername},{passwort}");
                    }
                    // Hier könnten weitere Registrierungsdetails abgefragt werden
                    Console.WriteLine($"Registrierung erfolgreich! Willkommen, {benutzername}.");
                }

                static void Anmelden()
                {
                    Console.WriteLine("\n--- Anmeldung ---");
                    Console.Write("Benutzername: ");
                    string benutzername = Console.ReadLine();
                    Console.Write("Passwort: ");
                    string passwort = Console.ReadLine();

                    bool eingeloggt = false;

                    if (!File.Exists("BenutzerDaten.txt"))
                    {
                        Console.WriteLine("Es sind keine Benutzerdaten vorhanden. Bitte registriere dich zuerst.");
                        return;
                    }

                    string[] zeilen = File.ReadAllLines("BenutzerDaten.txt");

                    foreach (string zeile in zeilen)
                    {
                        string[] teile = zeile.Split(',');
                        if (teile.Length == 2)
                        {
                            string gespeicherterBenutzername = teile[0];
                            string gespeichertesPasswort = teile[1];
                            if (benutzername == gespeicherterBenutzername && passwort == gespeichertesPasswort)
                            {
                                eingeloggt = true;
                                break;
                            }
                        }
                    }
                    if (eingeloggt)
                    {
                        Console.WriteLine("Anmeldung erfolgreich! Willkommen zurück, " + benutzername + ".");
                        //  BenutzerDatenEingeben();
                    }
                    else
                    {
                        Console.WriteLine("Anmeldung fehlgeschlagen. Bitte überprüfe deinen Benutzernamen und dein Passwort.");
                    }
                    // Hier könnte die Authentifizierung gegen gespeicherte Daten erfolgen
                    Console.WriteLine($"Anmeldung erfolgreich! Willkommen zurück, {benutzername}.");
                    //  BenutzerDatenEingeben();
                }

                static void BenutzerMenü(string aktuellerBenutzer)
                {
                    while (true)
                    {
                        Console.WriteLine($"\n--- Benutzer Menü ({aktuellerBenutzer}) ---");
                        Console.WriteLine("1. Benutzerdaten eingeben");
                        Console.WriteLine("2. Trainingsplan erstellen");
                        Console.WriteLine("3. Ernährungstagebuch führen");
                        Console.WriteLine("4. Fortschritt verfolgen");
                        Console.WriteLine("5. Abmelden");
                        Console.Write("Bitte wähle eine Option (1-5): ");
                        string benutzerMenüAuswahl = Console.ReadLine();
                        if (benutzerMenüAuswahl == "1")
                        {
                            // BenutzerDatenEingeben();
                        }
                        else if (benutzerMenüAuswahl == "2")
                        {
                            // TrainingsplanErstellen();
                        }
                        else if (benutzerMenüAuswahl == "3")
                        {
                            //  ErnährungstagebuchFühren();
                        }
                        else if (benutzerMenüAuswahl == "4")
                        {
                            // FortschrittVerfolgen();
                        }
                        else if (benutzerMenüAuswahl == "5")
                        {
                            Console.WriteLine("Du wurdest abgemeldet.");
                            break; // Beendet die Benutzermenü-Schleife und kehrt zum Hauptmenü zurück
                        }
                        else
                        {
                            Console.WriteLine("Ungültige Auswahl. Bitte versuche es erneut.");
                        }
                    }
                }

                static void NeuenEintragErstellen(string aktuellerBenutzer)
                {
                    Console.WriteLine($"\n--- Neuer Eintrag für {aktuellerBenutzer} ---");

                    Console.Write("Datum (TT.MM.JJJJ): ");
                    string datum = Console.ReadLine();

                    Console.Write("Gewicht (in kg): ");
                    string gewicht = Console.ReadLine();

                    Console.Write("Heutige Kalorienzufuhr (in kcal): ");
                    int kalorien = int.Parse(Console.ReadLine());

                    Console.Write("Notizen: ");
                    string notizen = Console.ReadLine();
                    // Speichern des Eintrags in einer Datei
                    using (StreamWriter sw = new StreamWriter($"{aktuellerBenutzer}_Tagebuch.txt", true))
                    {
                        sw.WriteLine($"{datum},{gewicht},{kalorien},{notizen}");
                    }
                    Console.WriteLine("Eintrag erfolgreich gespeichert!");
                }

                static void BenutzerDatenEingeben(string aktuellerBenutzer)
                {
                    Console.WriteLine("\n--- Benutzerdaten Eingeben ---");
                    Console.WriteLine("Bitte gib die folgenden Informationen ein:");
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
    }
}
