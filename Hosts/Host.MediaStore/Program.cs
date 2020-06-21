using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using ServiceModelEx;

namespace Host.MediaStore
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.Title = "Media Service Host";

         ServiceHost<ServiceLayer.MediaStore.MediaService> mediaServiceHost = new ServiceHost<ServiceLayer.MediaStore.MediaService>();
         mediaServiceHost.Open();

         Console.WriteLine("Media Service Started");
         Console.WriteLine();
         Console.WriteLine("Press <ENTER> to exit.");
         Console.ReadLine();

         mediaServiceHost.Close();
      }
   }
}
