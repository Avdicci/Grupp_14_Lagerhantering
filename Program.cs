using System.IO;
namespace Grupp_14_Lagerhantering
{
    public struct Produkt     // Produkt är för hur en produkt ser ut i lagret.
    {
        public int Id { get; set; }              // Produktens löpnummer, ex 1
        public string Namn { get; set; }         // Produktens namn, ex "Hammare"
        public double Pris { get; set; }         // Priset i kr, ex 49.90
        public int Antal { get; set; }           // Hur många som finns i lager, ex 15

        // SkrivUt skriver ut en produkts värden på skärmen.
        // Används när vi vill visa lagret för användaren.
        public void SkrivUt()
        {
            Console.WriteLine($"{Id} {Namn} {Pris} kr {Antal} st");

        }
        // Metoden returnerar en sträng som en rad i textfilen.
        // T.ex 1;Hammare;49.90;15
        public string ReturnerarRadTillFil()
        {
            return $"{Id};{Namn};{Pris};{Antal}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            bool avsluta = false;
            string menyVal;

            // Här ska vi deklarera vår lista med all data från txt-filen
            List<Produkt> lager = new List<Produkt>();
            string filSökVäg = "Lagervarde.txt";

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            while (avsluta == false)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=============================");
                Console.WriteLine("     LAGERHANTERINGSSYSTEM   ");
                Console.WriteLine("=============================");
                Console.ResetColor();

                Console.WriteLine("A Läs in fil");
                Console.WriteLine("B Visa alla produkter");
                Console.WriteLine("C Lägg till Produkt");
                Console.WriteLine("D Redigera produkt");
                Console.WriteLine("E Sök Produkt");
                Console.WriteLine("F Ta bort produkt");
                Console.WriteLine("G Sortera lager");
                Console.WriteLine("H Skapa fil");
                Console.WriteLine("X Avsluta");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("-----------------------------");
                Console.ResetColor();

                Console.Write("Ange val: ");

                menyVal = Console.ReadLine().ToUpper();
                Console.WriteLine(); // Tom rad för designs skull

                switch (menyVal)
                {
                    case "A":
                        // Läsa in filen och spara datan i en lista
                        if (!File.Exists(filSökVäg))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Filen finns inte. Skapa filen först (H).");
                            Console.ResetColor();
                            break;
                        }
                        LäsInFrånFil(lager, filSökVäg);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("✓ Fil inläst");
                        Console.ResetColor();
                        break;
                    case "B":
                        foreach (Produkt p in lager)
                        {
                            p.SkrivUt();
                        }
                        break;
                    // Här behöver vi skriva ut alla mätvärden i listan, det kan göras i en foreach loop
                    case "C":
                        LäggTillProdukt(lager, filSökVäg);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("✓ Produkt tillagd!");
                        Console.ResetColor();
                        // Lägg till en ny produkt i lagret och spara med append
                        break;
                    case "D":
                        // Redigerar en befintlig produkt via löpnummer och sparar hela listan till filen
                        RedigeraProdukt(lager, filSökVäg);
                        break;
                    case "E":
                        // Sök produkt
                        SökProdukt(lager);
                        break;
                    case "F":
                        // Tar bort en produkt via löpnummer
                        TaBortProdukt(lager, filSökVäg);
                        break;
                    case "G":
                        SorteraLager(lager);
                        // Sortera lagret på namn eller pris
                        break;
                    case "H":
                        // Skapa fil med testdata, återställer filen om den blivit korrupt
                        SkapaFil(filSökVäg);
                        break;
                    case "X":
                        avsluta = true;
                        break;
                }

                if (avsluta == false)
                {
                    Console.WriteLine("\nTryck valfri tangent för att gå tillbaka till menyn");
                    Console.ReadKey();
                }
            }

        } // Main method ends here

        // Skapar en ny textfil med hårdkodade produkter.
        // Använder StreamWriter från lathunden för att skriva rad för rad till filen.
        // Används för att återställa filen om den råkat bli korrupt eller försvunnit.
        static void SkapaFil(string filSökVäg)
        {
            string[] rader =
            {
                "1;Hammare;49,90;15",
                "2;Skruvmejsel;29,90;8",
                "3;Spik 50mm;12,50;200",
                "4;Träskiva 120x60;189,00;5",
                "5;Slippapper grov;9,90;50",
                "6;Målarpensel;35,00;20",
                "7;Vattenpass;149,00;3",
                "8;Borr 8mm;19,90;12",
                "9;Gipsskruv;45,00;500",
                "10;Hyvel;299,00;2"
            };
            StreamWriter writer = new StreamWriter(filSökVäg);

            foreach (string rad in rader)
            {
                writer.WriteLine(rad);
            }
            writer.Close();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Filen har återskapats med 10 produkter");
            Console.ResetColor();
        }
        static void LäsInFrånFil(List<Produkt> lager, string filSökVäg)
        {
            lager.Clear();

            try
            {

                string[] rader = File.ReadAllLines(filSökVäg);
                foreach (string rad in rader)
                {
                    string[] data = rad.Split(';');

                    int Id = int.Parse(data[0]);
                    string Namn = data[1];
                    double Pris = double.Parse(data[2]);
                    int Antal = int.Parse(data[3]);

                    Produkt p = new Produkt
                    {
                        Id = Id,
                        Namn = Namn,
                        Pris = Pris,
                        Antal = Antal
                    };
                    lager.Add(p);
                }
            }
            catch (Exception fel)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fel vid inläsning av fil: " + fel.Message);
                Console.ResetColor();
            }


        }

        static int FinnHögstaID(List<Produkt> lager)
        {
            //Hitta minsta värdet, för att sedan jämföra och lägga till det högsta
            int hogstaID = int.MinValue;

            // Loopa och jämför varje produkts ID med det bredvid, erkätt med det större.
            foreach (Produkt p in lager)
            {
                if (p.Id > hogstaID)
                {
                    hogstaID = p.Id;
                }
            }
            return hogstaID;
        }

        // Lägger till produkt genom att fråga användaren om produkten och läsa in svaret
        // nyttID hämtar högsta ID och lägger till 1 för att få nästa ID
        //AppendAllText lägger till den nya produkten i filen
        static void LäggTillProdukt(List<Produkt> lager, string filSökVäg)
        {   
            int nyttID = FinnHögstaID(lager) + 1;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Skriv in namn på varan: ");
            Console.ResetColor();
            string namn = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Skriv pris på varan: ");
            Console.ResetColor();
            double pris = double.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Skriv in antal: ");
            Console.ResetColor();
            int antal = int.Parse(Console.ReadLine());

            Produkt nyProdukt = new Produkt
            {
                Id = nyttID,
                Namn = namn,
                Pris = pris,
                Antal = antal,
            };

            lager.Add(nyProdukt);

            string rad = nyProdukt.ReturnerarRadTillFil();
            File.AppendAllText(filSökVäg, rad + Environment.NewLine);
        }

        static void RedigeraProdukt(List<Produkt> lager, string filSökVäg)
        {
            // Fråga användaren efter ID på produkten som ska redigeras
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Ange ID på produkten du vill redigera: ");
            Console.ResetColor();
            int söktId;
            while (!int.TryParse(Console.ReadLine(), out söktId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Ogiltigt ID. Ange ett giltigt ID: ");
                Console.ResetColor();
            }

            //Leta efter produkten i listan
            int index = -1;
            for (int i = 0; i < lager.Count; i++)
            {
                if (lager[i].Id == söktId)
                {
                    index = i;
                    break;
                }

            }

            // Om produkten inte hittades
            if (index == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ingen produkt hittades med det ID:t.");
                Console.ResetColor();
                return;
            }

            // Fråga efter nytt namn
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Nytt namn (enter för att behålla): ");
            Console.ResetColor();
            string namn = Console.ReadLine();
            if (namn == "")
            {
                namn = lager[index].Namn;
            }

            // Fråga efter nytt pris
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Nytt pris (enter för att behålla): ");
            Console.ResetColor();
            string prisText = Console.ReadLine();
            double pris;
            if (prisText == "")
            {
                pris = lager[index].Pris;
            }
            else
            {
                while (!double.TryParse(prisText, out pris))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Ogiltigt pris, försök igen: ");
                    Console.ResetColor();
                    prisText = Console.ReadLine();
                }
            }

            // Fråga efter nytt antal
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Nytt antal (enter för att behålla): ");
            Console.ResetColor();

            string antalText = Console.ReadLine();
            int antal;
            if (antalText == "")
            {
                antal = lager[index].Antal;
            }
            else
            {
                while (!int.TryParse(antalText, out antal))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Ogiltigt antal, försök igen: ");
                    Console.ResetColor();
                    antalText = Console.ReadLine();
                }
            }

            // Skapa en kopia av produkten med nya värden och lägg tillbaka i listan
            Produkt redigerad = lager[index];
            redigerad.Namn = namn;
            redigerad.Pris = pris;
            redigerad.Antal = antal;
            lager[index] = redigerad;

            // Spara hela listan till filen
            StreamWriter writer = new StreamWriter(filSökVäg);
            foreach (Produkt p in lager)
            {
                writer.WriteLine(p.ReturnerarRadTillFil());
            }
            writer.Close();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✓ Produkt redigerad!");
            Console.ResetColor();
        }

        static void SökProdukt(List<Produkt> lager)
        {
            // Fråga användaren efter sökord
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Skriv in sökord: ");
            Console.ResetColor();
            string söktText = Console.ReadLine().ToLower();

            // Loopa igenom listan och jämför sökordet med varje produkts namn
            bool finnsProdukt = false;
            foreach (Produkt p in lager)
            {
                // När den hittar, skriv ut och avsluta loopen
                if (p.Namn.ToLower().Contains(söktText))
                {
                    p.SkrivUt();
                    finnsProdukt = true;
                }
            }
            //Om den inte hittar någon produkt, skriv ut ett meddelande
            if (!finnsProdukt)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ingen produkt hittades med det sökordet.");
                Console.ResetColor();
            }
        }

        static void TaBortProdukt(List<Produkt> lager, string filSökVäg)
        {
            // Fråga användaren vilket ID eller namn på produkten som ska tas bort
            Console.WriteLine("Vilken Produkt vill du ta bort? (ID): ");
            // Läs in användarens svar från Console.ReadLine()
            int söktNamn;

            try
            {
                söktNamn = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Ogiltigt format. Vänligen ange ett giltigt ID.");
                return;
            }

            // Skapa en variabel som ska lagra indexet för produkten i 
            // Sätt den först till ett värde som betyder "inte hittad", t.ex. -1
            int index = -1;

            // Gå igenom hela listan med en loop (t.ex. for eller foreach)
            for (int i = 0; i < lager.Count; i++)
            {
                // För varje produkt i listan: jämför användarens söktext med produktens namn eller ID
                // Om produkten matchar:
                if (lager[i].Id == söktNamn)
                {
                    // spara positionen (indexet) i listan
                    index = i;

                    // avbryt loopen eftersom vi hittat rätt produkt
                    break;
                }

            }
            // Efter loopen: kontrollera om indexet fortfarande är -1
            // Om det är -1 betyder det att ingen produkt hittades
            if (index == -1)
            {
                // Om produkten inte hittades:
                // skriv ut ett meddelande till användaren
                Console.WriteLine("Produkten hittades inte :(");
            }
            // Om produkten hittades:
            else
            {
                // använd RemoveAt(index) för att ta bort produkten från listan
                lager.RemoveAt(index);
                // Efter borttagningen behöver textfilen uppdateras
                StreamWriter writer = new StreamWriter(filSökVäg);

                // Gå igenom listan igen med en loop
                foreach (Produkt p in lager)
                {
                    // För varje produkt i listan: skriv en rad till filen
                    // Använd metoden som returnerar produkten i filformat
                    writer.WriteLine(p.ReturnerarRadTillFil());
                }
                // Stäng filen
                writer.Close();
                // Skriv ut ett meddelande till användaren att produkten har tagits bort
                Console.WriteLine("Produkten har tagits bort");
            }

        }

        // Sortera på namn
        static int JämförNamn(Produkt p1, Produkt p2)
        {
            return p1.Namn.CompareTo(p2.Namn);
        }
        // Sortera på pris
        static int JämförPris(Produkt p1, Produkt p2)
        {
            return p1.Pris.CompareTo(p2.Pris);
        }
        // Sortera på ID, eftersom det är passande att ha med enligt mig (Zejd.A)
        static int JämförID(Produkt p1, Produkt p2)
        {
            return p1.Id.CompareTo(p2.Id);
        }
        static void SorteraLager(List<Produkt> lager)
        {
            Console.WriteLine("Vill du sortera på Namn (N), Pris (P), eller ID (ID)");
            string val = Console.ReadLine().ToLower();

            if (val == "n")
            {
                // Sortera på namn
                lager.Sort(JämförNamn);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓ Lagret sorterat på namn!");
                Console.ResetColor();
            }
            else if (val == "p")
            {
                // Sortera på pris
                lager.Sort(JämförPris);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓ Lagret sorterat på pris!");
                Console.ResetColor();
            }
            // Jag (Zejd.A) tycker personligen att man ska kunna sortera på ID
            else if (val == "id")
            {
                lager.Sort(JämförID);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓ Lagret sorterat på ID!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ogiltigt val, försök igen.");
                Console.ResetColor();
            }

        }



    }

}
