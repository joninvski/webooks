using System;
using System.IO;
using System.Configuration;

namespace WeBooksLibrary
{
  public class Logger {
      private static String logDirectory = "c:\\log\\";

      public void MudaDirectorio(string dir){
          logDirectory = dir;
      }

      public static void escreve(String message) {
        DateTime dt = DateTime.Now;
        String filePath = logDirectory + dt.ToString("yyyyMMdd") + ".log";
        if (!File.Exists(filePath)) {
          FileStream fs = File.Create(filePath);
          fs.Close();
        }
        try {
          StreamWriter sw = File.AppendText(filePath);
          sw.WriteLine(dt.ToString("hh:mm:ss") + "|" + message);
          sw.Flush();
          sw.Close();
        } catch (Exception e) {
          Console.WriteLine(e.Message.ToString());
        }
      }
    }
}
 