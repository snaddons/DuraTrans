using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using ServiceModelEx;

namespace Host.DistributionHub
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.Title = "DistributionHub Service Host";
 
         ServiceHost<ServiceLayer.DistributionHubService> distributionHubHost = new ServiceHost<ServiceLayer.DistributionHubService>();

         distributionHubHost.Open();

         foreach(ServiceEndpoint endpoint in distributionHubHost.Description.Endpoints)
         {
            //QueuedServiceHelper.PurgeQueue(endpoint);
         }

         Console.WriteLine("DistributionHub Service Started");
         Console.WriteLine();
         Console.WriteLine("Press <ENTER> to exit.");
         Console.ReadLine();

         distributionHubHost.Close();
      }
   }
}