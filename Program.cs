namespace Grupp_14_Lagerhantering
{
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
                Console.WriteLine("A Öppna fil");
                Console.WriteLine("B Visa värden");
                Console.WriteLine("C Whatever");
                Console.WriteLine("D Whatever");
                Console.WriteLine("E Whatever");
                Console.WriteLine("F Whatever");
                Console.WriteLine("G Whatever");
                Console.WriteLine("X Avsluta");
                Console.WriteLine("");
                Console.Write("Ange val: ");

                menyVal = Console.ReadLine().ToUpper();
                Console.Write(""); // Tom rad för designs skull, don't touch

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
                        // Vi får se vad vi gör
                        break;
                    case "D":
                        // Vi får se vad vi gör
                        break;
                    case "E":
                        // Vi får se vad vi gör
                        break;
                    case "F":
                        // Vi får se vad vi gör
                        break;
                    case "G":
                        // Vi får se vad vi gör
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