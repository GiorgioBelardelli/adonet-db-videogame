using System.Data.SqlClient;

namespace adonet_db_videogame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
                try
                {
                    bool continua = true;
                    VideogameManager manager = new VideogameManager();  
                    
                    while (continua)
                    {
                        Console.WriteLine("Menu:");
                        Console.WriteLine("1. Inserire un nuovo videogioco");
                        Console.WriteLine("2. Ricercare un videogioco per ID");
                        Console.WriteLine("3. Ricercare tutti i videogiochi aventi il nome contenente una determinata stringa");
                        Console.WriteLine("4. Cancellare un videogioco");
                        Console.WriteLine("5. Chiudere il programma");
                        Console.Write("Seleziona un'opzione: ");
                        string scelta = Console.ReadLine();

                        switch (scelta)
                        {
                            case "1":
                            Console.Write("Inserisci il nome del videogioco: ");
                            string nome = Console.ReadLine();

                            Console.Write("Inserisci l'overview del videogioco: ");
                            string overview = Console.ReadLine();

                            Console.Write("Inserisci la data di rilascio del videogioco (YYYY-MM-DD): ");
                            string releaseDateStr = Console.ReadLine();
                            DateTime releaseDate = DateTime.Parse(releaseDateStr);

                            Console.Write("Inserisci la Software House id: ");
                            int shId = int.Parse(Console.ReadLine());

                            Videogioco nuovoVideogioco = new ( nome, overview, releaseDate, DateTime.Now, DateTime.Now, shId);

                            manager.InserisciVideogioco(nuovoVideogioco);
                            break;

                            case "2":
                            Console.Write("Inserisci il codice ID del videogioco: ");
                            int videogameId = int.Parse(Console.ReadLine());

                            manager.GetVideogameById(videogameId);
                            break;

                            case "3":
                            Console.Write("Inserisci una stringa di ricerca per il nome del videogioco: ");
                            string userSearch = Console.ReadLine();

                            manager.SearchVideogamesByName(userSearch);
                            break;

                            case "4":
                            Console.Write("Inserisci il codice ID del videogioco che vuoi cancellare: ");
                            long videogameIdDelete = long.Parse(Console.ReadLine());

                            manager.DeleteVideoGameById(videogameIdDelete);
                            break;
                            
                            case "5":
                                continua = false;
                                Console.WriteLine("Programma chiuso.");
                                break;
                            default:
                                Console.WriteLine("Scelta non valida. Riprova.");
                                break;
                        }
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);                
                }
        }
    }
}
