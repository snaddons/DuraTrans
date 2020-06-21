using System;
using System.ServiceModel;
using ServiceModelEx;

namespace Host.Publication
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.Title = "Publication Service Host";

         ServiceHost<ServiceLayer.Publication.PublicationService>       publicationServiceHost       = new ServiceHost<ServiceLayer.Publication.PublicationService>();
         ServiceHost<ServiceLayer.Publication.PublicationDLQService>    publicationDLQServiceHost    = new ServiceHost<ServiceLayer.Publication.PublicationDLQService>();
         ServiceHost<ServiceLayer.Publication.PublicationPoisenService> publicationPoisonServiceHost = new ServiceHost<ServiceLayer.Publication.PublicationPoisenService>();
         
         publicationServiceHost.Open();
         publicationDLQServiceHost.Open();
         publicationPoisonServiceHost.Open();

         //foreach(ServiceEndpoint endpoint in publicationServiceHost.Description.Endpoints)
         //{
            //QueuedServiceHelper.PurgeQueue(endpoint);
         //}

         Console.WriteLine("Started: Publication Service");
         Console.WriteLine("Started: Publication DLQ Service");
         Console.WriteLine("Started: Publication Poisen Service"); 
         
         Console.WriteLine();
         Console.WriteLine("Press <ENTER> to exit.");
         Console.WriteLine();
         Console.ReadLine();

         publicationPoisonServiceHost.Close();
         publicationDLQServiceHost.Close();
         publicationServiceHost.Close();
      }
   }
}
