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
                        Console.WriteLine("Fil inläst");
                        break;
                    case "B":
                        // Här behöver vi skriva ut alla mätvärden i listan, det kan göras i en foreach loop
                        break;
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




    }
}