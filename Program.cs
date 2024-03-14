using Lab2;
using sorting;

namespace AuthenticationModule
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*SessionManager sessionManager = new SessionManager();
            sessionManager.addSession("Luca", "parola");
            sessionManager.addSession("Dorian", "ceva");
            sessionManager.addSession("Filip", "altceva");
            sessionManager.addSession("Maryio", "parola");
            sessionManager.addSession("Maryio", "parola");
            Console.WriteLine(sessionManager.getSessionTime("Luca", "parola"));
            Console.WriteLine(sessionManager.getNumberOfSessions());
            System.Threading.Thread.Sleep(5000);
            sessionManager.renewSession("Luca", "parola");
            Console.WriteLine(sessionManager.getSessionTime("Luca", "parola"));
            sessionManager.removeSession("Luca", "parola");
            Console.WriteLine(sessionManager.getNumberOfSessions());
            sessionManager.deleteOldSessions();
            Console.WriteLine(sessionManager.getNumberOfSessions());*/

            //Encryptor.EncryptorTest();

            //TestCache.CacheTest();

            List<int> list = new List<int>();
            int n;
            n = int.Parse(Console.ReadLine());
            for(int i = 0; i < n; i++)
                list.Add(int.Parse(Console.ReadLine()));

            SortingBusiness<int> r = new SortingBusiness<int>(list, "Merge");
            List<int> sortedList = r.Sort();
            for(int i = 0;i < sortedList.Count;i++)
                Console.WriteLine(sortedList[i]);

        }
    }
}
