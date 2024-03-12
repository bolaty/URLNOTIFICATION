using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;


namespace UrlNotificationCinetPayPayIn.Modules
{
    public static class clsLog
    {


        public static void EcrireDansFichierLog(String vlpMessage) 
        {
            //++++++++++++++++
            log4net.Config.XmlConfigurator.Configure();
            log4net.ILog log = log4net.LogManager.GetLogger("LogFileAppender");
            log.Error(vlpMessage);
            //log.Debug("Debug Message TEST");
            //log.Fatal("Fatal Message TEST");
            //log.Info("Info Message TEST");
            //log.Warn("Warning Message TEST");
        }





        //Calling Class

        // List<Employee> lst = ds.Tables[0].ToList<Employee>();
    }
}
