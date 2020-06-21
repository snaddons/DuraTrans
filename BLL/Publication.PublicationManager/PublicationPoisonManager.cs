using System;

namespace Manager.Publication
{
   public class PublicationPoisonManager
   {
      public void Publish(int publicationTaskID)
      {
         //throw new FaultException("Blow it up.");
         Console.WriteLine(DateTime.Now.ToLongTimeString() + " POISON: " + publicationTaskID.ToString());
      }

      public void Cancel(int publicationTaskID)
      {
         //No need to implement
      }
   }
}