using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Proxies;

namespace ClientApplication
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.Title = "Client Application";
         
         Console.WriteLine("Press <ENTER> to start.");
         Console.ReadLine();

         int publicationTaskID = 1;

         Publication(publicationTaskID);

         Console.WriteLine();
         Console.WriteLine("Press <ENTER> to exit.");
      }

      static void Publication(int publicationTaskId)
      {
         PublicationClient publicationService = new PublicationClient();
         
         try
         {
            publicationService.Publish(publicationTaskId);
            Console.WriteLine(DateTime.Now.ToLongTimeString() + " Publish " + publicationTaskId.ToString());
         }
         catch(Exception ex)
         {
            Console.WriteLine("Error " + ex.Message);
         }

         Console.ForegroundColor = ConsoleColor.Gray;
         Console.WriteLine();
         Console.WriteLine("Press <ENTER> to exit.");
         Console.ReadLine();

         publicationService.Close();
      }
   }
}
