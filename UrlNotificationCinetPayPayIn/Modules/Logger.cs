

namespace UrlNotificationCinetPayPayIn.Modules
{
    using System;
    using System.IO;

    public class Logger
    {
        public void Log(string path, string title, string message)
        {

            string pathFile = path + "logControlht.txt";

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (!File.Exists(pathFile))
                {
                    // Create a file to write to.

                     string createText = $"ERROR {(title == string.Empty ? "TRAITEMENT" : title.ToUpper())}\nPERIODE: {DateTime.Now.ToString()} \nMESSAGE: {message}{Environment.NewLine}";

                    using (StreamWriter tw = File.CreateText(pathFile))
                    {
                        tw.WriteLine(createText);
                    }

                }
                else
                {
                    string appendText = $"ERROR {(title == string.Empty ? "TRAITEMENT" : title.ToUpper())}\nPERIODE: {DateTime.Now.ToString()}\nMESSAGE: {message} {Environment.NewLine}";
                   
                    using (TextWriter tw = new StreamWriter(pathFile,true))
                    {
                        tw.WriteLine(appendText);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
