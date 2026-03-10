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
        public string RetunerarRadTillFil()
        {
            return $"{Id};{Namn};{Pris};{Antal}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Detta tar jag bort när alla har pullat
            Console.WriteLine("Hello, Nuran och Khattab!");
            Console.WriteLine("Om ni ser detta så har ni använt Git Pull korrekt :)");

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
                Console.WriteLine("A Läs in fil");
                Console.WriteLine("B Visa alla produkter");
                Console.WriteLine("C Lägg till produkt");
                Console.WriteLine("D Sök produkt");
                Console.WriteLine("E Redigera produkt");
                Console.WriteLine("F Ta bort produkt");
                Console.WriteLine("G Sortera lager");
                Console.WriteLine("H Skapa fil");
                Console.WriteLine("X Avsluta");
                Console.Write("Ange val: ");

                menyVal = Console.ReadLine().ToUpper();
                Console.Write(""); // Tom rad för designs skull

                switch (menyVal)
                {
                    case "A":
                        // Samma som Duggan, vi behöver läsa in filen och spara datan i en lista
                        if (!File.Exists(filSökVäg))
                        {
                            Console.WriteLine("Filen finns inte. Skapa filen först (H).");
                            break;
                        }
                        LäsInFrånFil(lager, filSökVäg);
                        Console.WriteLine("Fil inläst");
                        break;
                    case "B":
                        foreach (Produkt p in lager)
                        {
                            p.SkrivUt();
                        }
                        break;
                    // Här behöver vi skriva ut alla mätvärden i listan, det kan göras i en foreach loop
                    case "C":
                        // Lägg till en ny produkt i lagret och spara med append
                        break;
                    case "D":
                        // Söker efter produkt
                        break;
                    case "E":
                        // Redigerar en befintlig produkt via löpnummer
                        break;
                    case "F":
                        // Tar bort en produkt via löpnummer
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
                    Console.WriteLine("\nTryck valfri tangent för att återgå");
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
            Console.WriteLine("Filen har återskapats med 10 produkter");

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


    }
}
