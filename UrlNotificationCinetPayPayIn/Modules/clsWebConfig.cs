using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Configuration;

namespace UrlNotificationCinetPayPayIn.Modules
{
    public class clsWebConfig
    {

        #region  declaration unique de la classe

        //declaration unique de la classe clsWebConfig pour tout le projet
        private readonly static clsWebConfig ClassesWebConfig = new clsWebConfig();
        int vapCompteur = 0;

        //constructeur privé de la classe clsDeclaration
        //empêchant l'utilisateur d'instancier lui même la classe
        private clsWebConfig()
        {
        }
        //constructeur public de la classe fonction 
        public static clsWebConfig ClasseWebConfig
        {
            get { return ClassesWebConfig; }
        }

        #endregion


        //--Cette fonction permet de lire toutes les configurations
       public static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }


        //--Cette fonction permet de lire une valeure à partire de sa clée
        public static string ReadSetting(string key)
        {
            string result = "";
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                 result = appSettings[key] ?? "NO";
               
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
            return result;
        }

        //--cette fonction permet de mettre à jour le web.config à partie de sa clée si la clée n'existe pas elle la crée
        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
        //


       
    }
}
