using System;
using System.Threading;

namespace HelperClasses
{
   public static class Output
   {
      public static void WriteMessage(string message)
      {   
         int threadId = Thread.CurrentThread.ManagedThreadId;
         string output = string.Format("{0} [{1}] {2}", DateTime.Now.ToLongTimeString(), threadId.ToString(), message);
         
         Console.WriteLine(output);
      }

      public static void ThrowIfFailed(bool statusSet, string message)
      {
         if(!statusSet)
         {
            throw new Exception(message);
         }
      }
   }
}
