using System.Globalization;
using System.IO;
using System.Text.Encodings.Web;
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
                // Där det står "Whatever" får varje person ändra till det relevanta man jobbar med
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=============================");
                Console.WriteLine("     LAGERHANTERINGSSYSTEM   ");
                Console.WriteLine("=============================");
                Console.ResetColor();

                Console.WriteLine("A Läs in fil");
                Console.WriteLine("B Visa alla produkter");
                Console.WriteLine("C Lägg till Produkt");
                Console.WriteLine("D Finn Högsta ID");
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
                Console.Write(""); // Tom rad för designs skull

                switch (menyVal)
                {
                    case "A":
                        // Samma som Duggan, vi behöver läsa in filen och spara datan i en lista
                        if (!File.Exists(filSökVäg))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Filen finns inte. Skapa filen först (H).");
                            Console.ResetColor();
                            break;
                        }
                        LäsInFrånFil(lager, filSökVäg);
                        Console.ForegroundColor= ConsoleColor.Green;
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
                        LäggTillProdukt(lager);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("✓ Produkt tillagd!");
                        Console.ResetColor();
                        // Lägg till en ny produkt i lagret och spara med append
                        break;
                    case "D":
                        // Söker efter högsta ID i lagret och returnerar det
                        Console.WriteLine("Högsta ID: " + FinnHögstaID(lager));
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

        static int FinnHögstaID(List<Produkt> lager)
        {
            int hogstaID = int.MinValue;

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
        static void LäggTillProdukt(List<Produkt> lager)
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
            File.AppendAllText("Lagervarde.txt", rad + Environment.NewLine);
        }

        static void SökProdukt(List<Produkt> lager)
        {   
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Skriv in sökord: ");
            Console.ResetColor();
            string söktText = Console.ReadLine();
            string söktText = Console.ReadLine().ToLower();

            bool finnsProdukt = false;

            foreach (Produkt p in lager)
            {
                if (p.Namn.Contains(söktText)){
                    p.SkrivUt();
                    finnsProdukt = true;
                }
            }

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



    }
}
