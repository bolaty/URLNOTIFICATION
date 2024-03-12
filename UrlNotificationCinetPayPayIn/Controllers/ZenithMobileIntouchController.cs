using CinetPay.Plugin.Rules;
using MS_SDK.CinetPayNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using UrlNotificationCinetPayPayIn.Modules;


namespace UrlNotificationCinetPayPayIn.Controllers
{
    public class ZenithMobileIntouchController : Controller
    {

        String AG_CODEAGENCE = "";
        String SO_TELEPHONE = "";
        public static System.Net.Security.RemoteCertificateValidationCallback ServerCertificateValidationCallback { get; set; }
        public ActionResult Index(string cpm_trans_id, string code)
        {
            ViewData["cpm_trans_id"] = cpm_trans_id;
            return View("Affiche");
        }

        Modules.Logger loggerHT;

        [HttpPost]
        public ActionResult Index(string service_id, string gu_transaction_id, string status, string partner_transaction_id, string call_back_url, string commission, string message)
        {
            var culture = new System.Globalization.CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentCulture = culture;

            string cinqPremiersCaracteres = partner_transaction_id.Substring(0, 5);

            string URL_ROOT_ADRESSE_API = ConfigurationManager.AppSettings["URLZENITHMOBILE" + cinqPremiersCaracteres];

            string path = Server.MapPath("~/Loggers/");


            //--Envoi de sms Appel url notification
            loggerHT = new Modules.Logger();
            loggerHT.Log(path, "Cinetpay", "Appel url notification ok rest traitement 0 : " + partner_transaction_id);

            /*string partner_id = "CI53655";
            string login_api = "0564926180";
            string password_api = "mosWac3lSp0hUTH";
            string username = "FB0FF065B8B0ADBFE57705C7197E2FBB4C74C561C027A16EB4B11F9AC40F533A";
            string password = "9D755991B7E46E09408EEB94075531EF42AC35C1D2D91FC27260CCAC82A52167";*/

            string partner_id = "";//clsChaineCaractere.ClasseChaineCaractere.pvgDeCrypter(clsWebConfig.ReadSetting("partner_id"));// "CI53655";
            string login_api = "";// clsChaineCaractere.ClasseChaineCaractere.pvgDeCrypter(clsWebConfig.ReadSetting("login_api"));// "0564926180";
            string password_api = "";// clsChaineCaractere.ClasseChaineCaractere.pvgDeCrypter(clsWebConfig.ReadSetting("password_api"));// "mosWac3lSp0hUTH";
            string username = "";//clsChaineCaractere.ClasseChaineCaractere.pvgDeCrypter(clsWebConfig.ReadSetting("username"));//"FB0FF065B8B0ADBFE57705C7197E2FBB4C74C561C027A16EB4B11F9AC40F533A";
            string password = "";//clsChaineCaractere.ClasseChaineCaractere.pvgDeCrypter(clsWebConfig.ReadSetting("password"));// "9D755991B7E46E09408EEB94075531EF42AC35C1D2D91FC27260CCAC82A52167";

            loggerHT.Log(path, "InTouch", "Information postées : service_id" + service_id + " gu_transaction_id : " + gu_transaction_id + " status : " + status + " partner_transaction_id : " + partner_transaction_id + " partner_transaction_id : " + call_back_url + " commission : " + commission + " message : " + message);




            //sendsms("Appel url notification ok rest traitement 0 : " + cpm_trans_id);

            //--Log Test
            clsLog.EcrireDansFichierLog("Cinetpay :" + partner_transaction_id);

            //--1-vérification des paramètres
            //

            ViewData["cpm_trans_id"] = partner_transaction_id;
            if (partner_transaction_id == null || partner_transaction_id == "")
                return View("Affiche");


            string tran_id = partner_transaction_id;
            string CODESERVICE = "";





            /* clsParams clsParams = new clsParams();
             clsParams = PvgVERIFICATION_STATUT_TRANSFERT_INTOUCH(partner_id, login_api, password_api, username, password, partner_transaction_id);
             if (clsParams.SL_RESULTAT != "TRUE")
             {
                 ViewData["cpm_trans_id"] = partner_transaction_id;
                 return View("Affiche");
             }*/



            UrlNotificationCinetPayPayIn.Models.clsResultatMobileTransactionMobileBanking Resultats1 = new Models.clsResultatMobileTransactionMobileBanking();

            try
            {
                using (var webClient = new WebClient())
                {
                    //--3--Récupération des informations de l'agence
                    UrlNotificationCinetPayPayIn.Models.clsObjetEnvoiInfoAgence clsObjetEnvoiInfoAgence = new Models.clsObjetEnvoiInfoAgence();
                    clsObjetEnvoiInfoAgence.DT_NUMEROTRANSACTION = tran_id;
                    clsObjetEnvoiInfoAgence.LG_CODELANGUE = "FR";
                    string jsonAgence = JsonConvert.SerializeObject(clsObjetEnvoiInfoAgence);

                    var httpWebRequest0 = (HttpWebRequest)WebRequest.Create(URL_ROOT_ADRESSE_API + "pvgInfoAgenceOperation");
                    httpWebRequest0.ContentType = "application/json";
                    httpWebRequest0.Method = "POST";


                    using (var streamWriter = new StreamWriter(httpWebRequest0.GetRequestStream()))
                    {
                        streamWriter.Write(jsonAgence);
                    }
                    ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();


                    var httpResponse0 = (HttpWebResponse)httpWebRequest0.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse0.GetResponseStream()))
                    {
                        var response = streamReader.ReadToEnd();
                        String responses = response;
                        string NewISOmsg = responses.Substring(1, responses.Length - 2);
                        NewISOmsg = NewISOmsg.Replace("\"pvgInfoAgenceOperationResult\":", "");
                        UrlNotificationCinetPayPayIn.Models.clsInfoSouscriptionMobile Resultats = JsonConvert.DeserializeObject<UrlNotificationCinetPayPayIn.Models.clsInfoSouscriptionMobile>(NewISOmsg);

                        AG_CODEAGENCE = Resultats.AG_CODEAGENCE;
                        SO_TELEPHONE = Resultats.SO_TELEPHONE;
                        commission = Resultats.TW_MONTANTOPERATION;

                        partner_id = Resultats.AG_CINETAPIKEY;
                        login_api = Resultats.AG_CINETAPIPWD;
                        password_api = Resultats.AG_CINETSITEID;
                        username = Resultats.AG_TOKENTOUCHLOGIN;
                        password = Resultats.AG_TOKENTOUCHPWD;

                        CODESERVICE = pvgServiceIdInToush(SO_TELEPHONE, "CASHIN");

                        if (string.IsNullOrEmpty(AG_CODEAGENCE))
                        {
                            //ViewBag.Message = "Echec, votre paiement a échoué";
                            ViewData["data"] = "Error: Récuperation d'agence !!!";
                            // sendsms("Traitement url notification ok : Error : Echec, votre paiement a échoué: " + cpm_trans_id);

                            loggerHT = new Modules.Logger();
                            loggerHT.Log(path, "Error", "Récuperation d'agence !!! : " + partner_transaction_id);

                            return View();
                        }


                    }
                }




                //==============SIRUS
                /* CinetPayDTO model = new CinetPayDTO();
                 //CL_EMETTEUR = clsChaineCaractere.ClasseChaineCaractere.pvgDeCrypter(clsWebConfig.ReadSetting("Cpm_Site_Id"));
                 model.Mode = "PROD";
                 model.Apikey = clsChaineCaractere.ClasseChaineCaractere.pvgDeCrypter(clsWebConfig.ReadSetting("Apikey"+ AG_CODEAGENCE));// "5665035245ff47b56c24de1.90155398";
                 model.Cpm_Site_Id = clsChaineCaractere.ClasseChaineCaractere.pvgDeCrypter(clsWebConfig.ReadSetting("Cpm_Site_Id"+ AG_CODEAGENCE));// "595120";
                 model.Cpm_Trans_Id = tran_id;*/
                //==============



                ////string path = Server.MapPath("~/Loggers/");
                //var cinetpayService = new CinetPaySdk(model);
                //cinetpayService.Path = path;
                //var data = cinetpayService.GetPayStatus();
                if (status != "SUCCESSFUL")
                {
                    //ViewBag.Message = "Echec, votre paiement a échoué";
                    ViewData["data"] = "Error";
                    // sendsms("Traitement url notification ok : Error : Echec, votre paiement a échoué: " + cpm_trans_id);

                    loggerHT = new Modules.Logger();
                    loggerHT.Log(path, "Error", "Traitement url notification ok : Error : Echec, votre paiement a échoué: " + partner_transaction_id);
                }
                else
                {
                    //ViewBag.Message = "Felicitation, votre paiement a été effectué avec succès";
                    if (status != null)
                    {
                        //ViewData["data"] = data;


                        ViewData["service_id"] = service_id;
                        ViewData["gu_transaction_id"] = gu_transaction_id;
                        ViewData["status"] = status;
                        ViewData["partner_transaction_id"] = partner_transaction_id;
                        ViewData["call_back_url"] = call_back_url;

                        if (status == "SUCCESSFUL")
                        {
                            loggerHT = new Modules.Logger();
                            loggerHT.Log(path, "success", "Traitement url notification ok : success : Felicitation, votre paiement a été effectué avec succès: " + partner_transaction_id);
                            // sendsms("Traitement url notification ok : success : Felicitation, votre paiement a été effectué avec succès: " + cpm_trans_id);

                            String DT_NUMEROTRANSACTION = partner_transaction_id;//

                            //string service_id,  string gu_transaction_id,string status,string partner_transaction_id,string call_back_url,double commission,string message

                            using (var webClient = new WebClient())
                            {

                                //--4--Appel du service web de HT pour la comptabilisation


                                String LG_CODELANGUE = "fr";
                                //String AG_CODEAGENCE = "1000";
                                String CO_CODECOMPTE = "";
                                String CL_IDCLIENT = "";
                                String MONTANTOPERATION = commission.ToString(); // CinetPayData.cpm_amount;
                                String MONTANTOPERATIONSANSFRAIS = "0"; // CinetPayData.cpm_amount;
                                String NO_CODENATUREVIREMENT = "0011";
                                String SO_CODESOUSCRIPTION = "";
                                String DATEJOURNEE = "";
                                String TW_COMISSIONHT = "0";
                                String TW_COMISSIONTMF = "0";
                                String TW_COMISSIONOPERATEUR = "0";
                                String TW_CODEVALIDATION = partner_transaction_id;// CinetPayVerificationStatutResultDatas[0].cpm_trans_id;
                                String TO_CODETYPETRANSFERT = "18";
                                string TYPEOPERATION = "10";
                                string SL_LOGIN = "";
                                string SL_MOTDEPASSE = "";
                                string SL_CLESESSION = "";
                                string SL_VERSIONAPK = "2";
                                string OS_MACADRESSE = "1";
                                string SL_UTILISATEUR = "00";
                                String TO_INSERERDANSLATABLETRANSFERT = "N";
                                String TO_VALIDEROPERATIONENCOURS = "O";


                                //--5--Récupération des informations de la souscription
                                UrlNotificationCinetPayPayIn.Models.clsObjetEnvoiInfoSouscriptionMobile clsObjetEnvoiInfoSouscriptionMobile = new UrlNotificationCinetPayPayIn.Models.clsObjetEnvoiInfoSouscriptionMobile();
                                clsObjetEnvoiInfoSouscriptionMobile.AG_CODEAGENCE = AG_CODEAGENCE;// "1000";
                                clsObjetEnvoiInfoSouscriptionMobile.SO_TELEPHONE = SO_TELEPHONE;// CinetPayData.cel_phone_num;
                                clsObjetEnvoiInfoSouscriptionMobile.LG_CODELANGUE = "fr";
                                clsObjetEnvoiInfoSouscriptionMobile.TYPEOPERATION = "01";

                                string jsonNF1 = JsonConvert.SerializeObject(clsObjetEnvoiInfoSouscriptionMobile);

                                var httpWebRequest1 = (HttpWebRequest)WebRequest.Create(URL_ROOT_ADRESSE_API + "pvgInfoSousCriptionMobile");
                                httpWebRequest1.ContentType = "application/json";
                                httpWebRequest1.Method = "POST";

                                //==================18/09/2020
                                // using System.Net;
                                // ServicePointManager.Expect100Continue = true;
                                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                // Use SecurityProtocolType.Ssl3 if needed for compatibility reasons
                                //==================

                                using (var streamWriter = new StreamWriter(httpWebRequest1.GetRequestStream()))
                                {
                                    streamWriter.Write(jsonNF1);
                                }
                                ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();


                                var httpResponse1 = (HttpWebResponse)httpWebRequest1.GetResponse();
                                using (var streamReader = new StreamReader(httpResponse1.GetResponseStream()))
                                {
                                    var response = streamReader.ReadToEnd();
                                    String responses = response;
                                    string NewISOmsg = responses.Substring(1, responses.Length - 2);
                                    NewISOmsg = NewISOmsg.Replace("\"pvgInfoSousCriptionMobileResult\":", "");
                                    UrlNotificationCinetPayPayIn.Models.clsInfoSouscriptionMobile Resultats = JsonConvert.DeserializeObject<UrlNotificationCinetPayPayIn.Models.clsInfoSouscriptionMobile>(NewISOmsg);
                                    CO_CODECOMPTE = Resultats.CO_CODECOMPTE;
                                    CL_IDCLIENT = Resultats.CL_IDCLIENT;
                                    SO_CODESOUSCRIPTION = Resultats.SO_CODESOUSCRIPTION;
                                    DATEJOURNEE = Resultats.DATEJOURNEE;

                                }

                                //--
                                ObjetFRAIS objFRAIS = new ObjetFRAIS();
                                /* objFRAIS.MONTANT = "0";
                                 objFRAIS.MONTANTMF = MONTANTOPERATION;
                                 objFRAIS.CODESERVICE = CODESERVICE;
                                 objFRAIS.LG_CODELANGUE = "FR";
                                 objFRAIS.TYPEOPERATION = "01";*/

                                //  ------------------21/09/2023-------------------------------------------------//
                                objFRAIS.MONTANT = "0";
                                objFRAIS.MONTANTMF = MONTANTOPERATION;
                                objFRAIS.CODESERVICE = CODESERVICE;
                                objFRAIS.TYPEOPERATION = "02";
                                objFRAIS.AG_CODEAGENCE = AG_CODEAGENCE;
                                objFRAIS.TO_CODETYPETRANSFERT = "18";
                                objFRAIS.LG_CODELANGUE = "FR";

                                string jsonNFFRAIS = JsonConvert.SerializeObject(objFRAIS);

                                var httpWebRequestFRAIS = (HttpWebRequest)WebRequest.Create(URL_ROOT_ADRESSE_API + "pvgCommissioncinetpay");
                                httpWebRequestFRAIS.ContentType = "application/json";
                                httpWebRequestFRAIS.Method = "POST";

                                using (var streamWriter = new StreamWriter(httpWebRequestFRAIS.GetRequestStream()))
                                {
                                    streamWriter.Write(jsonNFFRAIS);
                                }
                                ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();


                                var httpResponseFRAIS = (HttpWebResponse)httpWebRequestFRAIS.GetResponse();
                                using (var streamReader = new StreamReader(httpResponseFRAIS.GetResponseStream()))
                                {
                                    var response = streamReader.ReadToEnd();
                                    String responses = response;
                                    string NewISOmsg = responses.Substring(1, responses.Length - 2);
                                    NewISOmsg = NewISOmsg.Replace("\"pvgCommissioncinetpayResult\":", "");
                                    UrlNotificationCinetPayPayIn.Models.clsMontant Resultats = JsonConvert.DeserializeObject<UrlNotificationCinetPayPayIn.Models.clsMontant>(NewISOmsg);
                                    MONTANTOPERATIONSANSFRAIS = Resultats.SL_MONTANT;
                                    TW_COMISSIONTMF = Resultats.SL_COMMISSION;
                                }
                                //  ------------------21/09/2023-------------------------------------------------//
                                //--





                                //--6--Appel du service web de comptabilisation
                                ObjetPaiement objNF = new ObjetPaiement();

                                objNF.LG_CODELANGUE = LG_CODELANGUE;
                                objNF.AG_CODEAGENCE = AG_CODEAGENCE;
                                objNF.CO_CODECOMPTE = CO_CODECOMPTE;
                                objNF.CL_IDCLIENT = CL_IDCLIENT;
                                objNF.MONTANTOPERATION = MONTANTOPERATIONSANSFRAIS;// (double.Parse(MONTANTOPERATION) + double.Parse(TW_COMISSIONTMF)).ToString() ;// MONTANTOPERATIONSANSFRAIS;// MONTANTOPERATION;
                                objNF.NO_CODENATUREVIREMENT = NO_CODENATUREVIREMENT;
                                objNF.SO_CODESOUSCRIPTION = SO_CODESOUSCRIPTION;
                                objNF.DATEJOURNEE = DATEJOURNEE;
                                objNF.TW_COMISSIONHT = TW_COMISSIONHT;
                                objNF.TW_COMISSIONTMF = "0";// TW_COMISSIONTMF;
                                objNF.TW_COMISSIONOPERATEUR = TW_COMISSIONOPERATEUR;
                                objNF.TW_CODEVALIDATION = TW_CODEVALIDATION;
                                objNF.TO_CODETYPETRANSFERT = TO_CODETYPETRANSFERT;
                                objNF.TO_INSERERDANSLATABLETRANSFERT = TO_INSERERDANSLATABLETRANSFERT;
                                objNF.TO_VALIDEROPERATIONENCOURS = TO_VALIDEROPERATIONENCOURS;
                                objNF.TYPEOPERATION = TYPEOPERATION;
                                objNF.SL_LOGIN = SL_LOGIN;
                                objNF.SL_MOTDEPASSE = SL_MOTDEPASSE;
                                objNF.SL_CLESESSION = SL_CLESESSION;
                                objNF.SL_VERSIONAPK = SL_VERSIONAPK;
                                objNF.OS_MACADRESSE = OS_MACADRESSE;
                                objNF.SL_UTILISATEUR = SL_UTILISATEUR;
                                objNF.IN_CODESERVICE = CODESERVICE;
                                objNF.TYPE_APP = "CLIENT";



                                string jsonNF = JsonConvert.SerializeObject(objNF);

                                var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL_ROOT_ADRESSE_API + "pvgMobileTransactionMobileBanking");
                                httpWebRequest.ContentType = "application/json";
                                httpWebRequest.Method = "POST";

                                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                                {
                                    streamWriter.Write(jsonNF);
                                }

                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                {
                                    var response = streamReader.ReadToEnd();
                                    String responses = response;

                                    string NewISOmsg = responses.Substring(1, responses.Length - 2);
                                    NewISOmsg = NewISOmsg.Replace("\"pvgMobileTransactionMobileBankingResult\":", "");
                                    UrlNotificationCinetPayPayIn.Models.clsResultatMobileTransactionMobileBanking Resultats = JsonConvert.DeserializeObject<UrlNotificationCinetPayPayIn.Models.clsResultatMobileTransactionMobileBanking>(NewISOmsg);
                                    Resultats1 = Resultats;
                                }
                            }

                        }
                        //string service_id,  string gu_transaction_id,string status,string partner_transaction_id,string call_back_url,double commission,string message
                        // sendsms("Traitement url notification ok : Transaction ok : " + cpm_trans_id + " " + data.cpm_error_message);
                        loggerHT = new Modules.Logger();
                        loggerHT.Log(path, "Error", "Traitement url notification ok : Transaction ok : " + partner_transaction_id + " " + message + " " + Resultats1.SL_RESULTAT + " " + Resultats1.SL_MESSAGE);
                    }

                }



            }
            catch (Exception ex)
            {
                //Execution du log
                clsLog.EcrireDansFichierLog("Cinetpay :" + ex.Message);
                //sendsms("Traitement url notification ok : Error : " + ex.Message +" " + cpm_trans_id);
                loggerHT = new Modules.Logger();
                loggerHT.Log(path, "error", "Traitement url notification ok: Error : " + ex.Message + " " + partner_transaction_id + " status : " + status);
            }

            return View();
        }


        //Envoi de mail
        public void sendmail(string pvgTitre, string vppMessage, string vppMailExpediteur, string vppMotDePasseExpediteur, string vppMailRecepteur, string vppCheminCompletFichierEnvoyer1, string vppCheminCompletFichierEnvoyer2, string vppCheminCompletFichierEnvoyer3)
        {
            try
            {
                ////-I-Préparation du fichier
                //ReportDocument cryRpt;

                ////string pdfFile = "c:\\csharp.net-informations.pdf";
                //string pdfFile = vppCheminCompletFichierPDFEnvoyer;
                //cryRpt = new ReportDocument();
                //string PATH = Application.StartupPath + "\\Etats\\" + vappFichier;
                //cryRpt.Load(PATH);
                //cryRpt.SetDataSource(vappTable.Tables[0]);
                //for (int Idx = 0; Idx < vappNomFormule.GetLength(0); Idx++)
                //{
                //    string vlpNomFormule = vappNomFormule[Idx].ToString();
                //    string vlpValeurFormule = vappValeurFormule[Idx].ToString();
                //    cryRpt.DataDefinition.FormulaFields[vlpNomFormule].Text = "'" + vlpValeurFormule.Replace("'", "''") + "'";

                //}

                //CrystalReportViewer crystalReportViewer1 = new CrystalReportViewer();
                //crystalReportViewer1.ReportSource = cryRpt;
                //crystalReportViewer1.Refresh();

                ////-II-Exportation du fichier
                //ExportOptions CrExportOptions;
                //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                //PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                //CrDiskFileDestinationOptions.DiskFileName = pdfFile;
                //CrExportOptions = cryRpt.ExportOptions;
                //CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                //CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                //CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                //CrExportOptions.FormatOptions = CrFormatTypeOptions;
                //cryRpt.Export();

                //-III-Envoi du fichier par mail
                System.Net.Mail.MailMessage mm = null;
                //if (clsChaineCaractere.ClasseChaineCaractere.pvgValidationEmail(vppMailExpediteur) != false && clsChaineCaractere.ClasseChaineCaractere.pvgValidationEmail(vppMailRecepteur) != false)
                mm = new System.Net.Mail.MailMessage(vppMailExpediteur, vppMailRecepteur);
                // Contenu du message
                if (mm != null)
                {
                    mm.Subject = pvgTitre;
                    mm.Body = vppMessage;
                }
                //if (clsChaineCaractere.ClasseChaineCaractere.pvgValidationEmail(vppMailExpediteur) != false && clsChaineCaractere.ClasseChaineCaractere.pvgValidationEmail(vppMailRecepteur2) != false)
                //    mm.CC.Add(vppMailRecepteur2);

                //Ajoute des fichiers joints
                if (vppCheminCompletFichierEnvoyer1 != "")
                    mm.Attachments.Add(new Attachment(vppCheminCompletFichierEnvoyer1));
                if (vppCheminCompletFichierEnvoyer2 != "")
                    mm.Attachments.Add(new Attachment(vppCheminCompletFichierEnvoyer2));
                if (vppCheminCompletFichierEnvoyer3 != "")
                    mm.Attachments.Add(new Attachment(vppCheminCompletFichierEnvoyer3));

                // Sending message
                SmtpClient sc = new SmtpClient("smtp.gmail.com", 587);

                if (vppMailExpediteur != null)
                {
                    // Le compte credentials
                    //if (clsChaineCaractere.ClasseChaineCaractere.pvgValidationEmail(vppMailExpediteur) != false)
                    sc.Credentials = new NetworkCredential(vppMailExpediteur, vppMotDePasseExpediteur, "");
                    sc.EnableSsl = true;

                    // Envoie du message
                    try
                    {
                        //if (clsChaineCaractere.ClasseChaineCaractere.pvgValidationEmail(vppMailExpediteur) != false)
                        sc.Send(mm);
                        // MessageBox.Show("Message sent");
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Error: " + ex.Message);
                    }
                }




                ////

                ////SmtpMail.SmtpServer.Insert(587,"smtp.gmail.com");
                ////System.Web.Mail.MailMessage Msg = new System.Web.Mail.MailMessage();
                ////Msg.To = "d.baz1008@gmail.com";
                ////Msg.From = "d.baz1008@gmail.com";
                ////Msg.Subject = "Crystal Report Attachment ";
                ////Msg.Body = "Crystal Report Attachment ";
                ////Msg.Attachments.Add(new MailAttachment(pdfFile));
                ////System.Web.Mail.SmtpMail.Send(Msg);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                return;
            }
        }
        public void sendsms(string Message)
        {
            //=========================================================
            //string Authheader = "HOMETEST" + ":" + "HomeTech2020";
            string Authheader = "celpaid" + ":" + "Celp@#d20#";

            Authheader = Base64Encode(Authheader);


            string sURL;

            StreamReader objReader;

            //if (CL_FOURNISSEURAPI == "INFOBIP")
            //{

            //================================INFOBIP
            string webAddr = "https://api.infobip.com/sms/1/text/single";// ok
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);// ok
            httpWebRequest.ContentType = "application/json";// ok
            httpWebRequest.Accept = "application/json";// ok
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            //==========================================================================================================
            // Le login : TECHNOLOGYH
            //Le mot de passe : Dev1HoM6
            //TECHNOLOGYH:Dev1HoM6= (VEVDSE5PTE9HWUg6RGV2MUhvTTY=) c'est la combinaison du login et du mot de passe  . 
            //--le cryptage de cette combinaison peut se faire sur le site https://www.base64encode.org/ 
            //===========================================================================================================

            string senderName = "celpaid";

            string phone = "2250747839553";
            try
            {
                httpWebRequest.Headers.Add("Authorization", " Basic " + Authheader);
                httpWebRequest.Method = "POST";// ok

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"from\":\"" + senderName + "\", \"to\":\"" + phone + "\",\"text\":\"" + Message + "\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }



                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    result = result.Substring(12, result.Length - 12);

                    result = result.Substring(0, result.Length - 1);
                    String response = result;
                    //messages messages = new messages();

                    //List<message> message = JsonConvert.DeserializeObject<List<message>>(response);

                    //if (message[0].status.groupName == "PENDING")
                    //{
                    //    //clsParams clsParamsss = new clsParams();
                    //    //clsParamsss.SL_MESSAGE = "Opération réalisée avec succès !!!";
                    //    //clsParamsss.SL_RESULTAT = "TRUE";
                    //    //clsParamsss.SL_CODEMESSAGE = "00";
                    //    //clsParamsss.SM_NUMSEQUENCE = clsParams.SM_NUMSEQUENCE;
                    //    //clsParamsListe.Add(clsParamsss);
                    //}
                    //else
                    //{
                    //    //clsParams clsParamsss = new clsParams();
                    //    //clsParamsss.SL_MESSAGE = "Echec d'envoi de sms : le numéro du destinataire est invalide  !!!";
                    //    //clsParamsss.SL_RESULTAT = "FALSE";
                    //    //clsParamsss.SL_CODEMESSAGE = "99";
                    //    //clsParamsss.SM_NUMSEQUENCE = clsParams.SM_NUMSEQUENCE;
                    //    //clsParamsListe.Add(clsParamsss);
                    //}


                    //return clsParamsListe;
                }
            }

            catch (Exception ex)
            {
                //clsParams clsParamsss = new clsParams();
                //clsParamsss.SL_MESSAGE = ex.Message.ToString();
                //clsParamsss.SL_RESULTAT = "FALSE";
                //clsParamsss.SL_CODEMESSAGE = "99";
                //clsParamsss.SM_NUMSEQUENCE = clsParams.SM_NUMSEQUENCE;
                //clsParamsListe.Add(clsParamsss);
                //return clsParamsListe;

            }

            //}

            //=========================================================
        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }


        enum typeOperationIntouch { CASHIN, CASHOUT }
        enum Operationcashin { CASHINOMCIPART, CASHINMTNPART, CASHINMOOVPART }
        enum Operationcashout { PAIEMENTMARCHAND_MOOV_CI, PAIEMENTMARCHAND_MTN_CI, PAIEMENTMARCHANDOMPAYCI }
        public string pvgServiceIdInToush(string numero, string typeOperation)
        {

            string vlpCodeService = "";
            int vlpTaillenumero = numero.Length;
            int prefixenumero = 0;
            prefixenumero = int.Parse(numero.Substring(1, 1));

            if (typeOperation == "CASHIN")
            {
                if (UrlNotificationCinetPayPayIn.Modules.clsNombreMontant1.Between(prefixenumero, 1, 3))
                {
                    Operationcashin a = Operationcashin.CASHINMOOVPART;
                    vlpCodeService = a.ToString();
                }
                else
                if (UrlNotificationCinetPayPayIn.Modules.clsNombreMontant1.Between(prefixenumero, 4, 6))
                {
                    Operationcashin a = Operationcashin.CASHINMTNPART;
                    vlpCodeService = a.ToString();
                }
                else
                {
                    Operationcashin a = Operationcashin.CASHINOMCIPART;
                    vlpCodeService = a.ToString();
                }
            }


            if (typeOperation == "CASHOUT")
            {
                if (UrlNotificationCinetPayPayIn.Modules.clsNombreMontant1.Between(prefixenumero, 1, 3))
                {
                    Operationcashout a = Operationcashout.PAIEMENTMARCHAND_MOOV_CI;
                    vlpCodeService = a.ToString();
                }
                else
                if (UrlNotificationCinetPayPayIn.Modules.clsNombreMontant1.Between(prefixenumero, 4, 6))
                {
                    Operationcashout a = Operationcashout.PAIEMENTMARCHAND_MTN_CI;
                    vlpCodeService = a.ToString();
                }
                else
                {
                    Operationcashout a = Operationcashout.PAIEMENTMARCHANDOMPAYCI;
                    vlpCodeService = a.ToString();
                }
            }



            return vlpCodeService;
        }

        public clsParams PvgVERIFICATION_STATUT_TRANSFERT_INTOUCH(string partner_id, string login_api, string password_api, string username, string password, string partner_transaction_id)
        {


            clsParams clsParams = new clsParams();
            #region=//======================================= INTOUCH
            string Url = "https://api.gutouch.com/v1/SIRIU7265/check_status";
            string CL_FOURNISSEURAPI = "INTOUCH";
            //string Authheader = clsSmsoperateurdetlephoniecompteclients[0].CL_NOMUTILISATEUR + ":" + clsSmsoperateurdetlephoniecompteclients[0].CL_MOTDEPASSE;

            //string Authheader = "FB0FF065B8B0ADBFE57705C7197E2FBB4C74C561C027A16EB4B11F9AC40F533A" + ":" + "9D755991B7E46E09408EEB94075531EF42AC35C1D2D91FC27260CCAC82A52167";

            string Authheader = username + ":" + password;
            //string partner_id = "CI53655";
            //string login_api = "0564926180";
            //string password_api = "mosWac3lSp0hUTH";

            Authheader = Base64Encode(Authheader);
            Objetcheck_status Objetcheck_status = new Objetcheck_status();



            if (CL_FOURNISSEURAPI == "INTOUCH")
            {

                string webAddr = Url;//
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);// ok
                httpWebRequest.ContentType = "application/json";// ok
                httpWebRequest.Accept = "application/json";// ok
                httpWebRequest.Headers.Add("Authorization", " Basic " + Authheader);
                httpWebRequest.Method = "POST";// ok

                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                //=====================nouveau code
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 |SecurityProtocolType.Ssl3;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //===================
                //  Somewhere();

                //==========================================================================================================
                // Le login : TECHNOLOGYH
                //Le mot de passe : Dev1HoM6
                //TECHNOLOGYH:Dev1HoM6= (VEVDSE5PTE9HWUg6RGV2MUhvTTY=) c'est la combinaison du login et du mot de passe  . 
                //--le cryptage de cette combinaison peut se faire sur le site https://www.base64encode.org/ 
                //===========================================================================================================

                try
                {


                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        //string json = "{\"from\":\"" + senderName + "\", \"to\":\"" + phone + "\",\"text\":\"" + clsParams.SMSTEXT + "\"}";

                        string json = "{\"partner_id\":\"" + partner_id + "\",\"login_api\":\"" + login_api + "\",\"password_api\":\"" + password_api + "\",\"partner_transaction_id\":\"" + partner_transaction_id + "\"}";
                        streamWriter.Write(json);
                        streamWriter.Flush();
                    }



                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();

                        //result = result.Substring(12, result.Length - 12);

                        //result = result.Substring(0, result.Length - 1);
                        String response = result;


                        Objetcheck_status = JsonConvert.DeserializeObject<Objetcheck_status>(response);

                        if (Objetcheck_status.status == "SUCCESSFUL")
                        {
                            clsParams = new clsParams();
                            clsParams.SL_MESSAGE = "Opération réalisée avec succès !!!";
                            clsParams.SL_RESULTAT = "TRUE";
                            clsParams.SL_CODEMESSAGE = "00";
                            //clsParamsss.SM_NUMSEQUENCE = clsParams.SM_NUMSEQUENCE;
                            //clsParamsListe.Add(clsParamsss);
                        }
                        else
                        {
                            clsParams = new clsParams();
                            clsParams.SL_MESSAGE = "Transaction invalide  !!!";
                            clsParams.SL_RESULTAT = "FALSE";
                            clsParams.SL_CODEMESSAGE = "99";
                            //clsParamsss.SM_NUMSEQUENCE = clsParams.SM_NUMSEQUENCE;
                            //clsParamsListe.Add(clsParamsss);
                        }


                        //return clsParamsListe;
                    }
                }

                catch (Exception ex)
                {
                    clsParams = new clsParams();
                    clsParams.SL_MESSAGE = ex.Message.ToString();
                    clsParams.SL_RESULTAT = "FALSE";
                    clsParams.SL_CODEMESSAGE = "99";
                    //clsParamsss.SM_NUMSEQUENCE = clsParams.SM_NUMSEQUENCE;
                    //clsParamsListe.Add(clsParamsss);
                    //return clsParamsListe;

                }

            }

            #endregion =//=======================================FIN INTOUCH



            return clsParams;
        }


        public class ObjetPaiement
        {
            public String LG_CODELANGUE { get; set; }
            public String AG_CODEAGENCE { get; set; }
            public String CO_CODECOMPTE { get; set; }
            public String CL_IDCLIENT { get; set; }
            public String MONTANTOPERATION { get; set; }
            public String NO_CODENATUREVIREMENT { get; set; }
            public String SO_CODESOUSCRIPTION { get; set; }
            public String DATEJOURNEE { get; set; }
            public String TW_COMISSIONHT { get; set; }
            public String TW_COMISSIONTMF { get; set; }
            public String TW_COMISSIONOPERATEUR { get; set; }
            public String TW_CODEVALIDATION { get; set; }
            public String TO_CODETYPETRANSFERT { get; set; }
            public String TYPEOPERATION { get; set; }
            public String SL_LOGIN { get; set; }
            public String SL_MOTDEPASSE { get; set; }
            public String SL_CLESESSION { get; set; }
            public String SL_VERSIONAPK { get; set; }
            public String OS_MACADRESSE { get; set; }
            public String SL_UTILISATEUR { get; set; }
            public String IN_CODESERVICE { get; set; }
            public String TYPE_APP { get; set; }

            public String TO_INSERERDANSLATABLETRANSFERT { get; set; }
            public String TO_VALIDEROPERATIONENCOURS { get; set; }


        }


        public class ObjetFRAIS
        {
            public String MONTANT { get; set; }
            public String MONTANTMF { get; set; }
            public String CODESERVICE { get; set; }
            public String TYPEOPERATION { get; set; }
            public String AG_CODEAGENCE { get; set; }
            public String TO_CODETYPETRANSFERT { get; set; }
            public String LG_CODELANGUE { get; set; }

        }


        public class Objetcheck_status
        {
            public String service_id { get; set; }
            public String gu_transaction_id { get; set; }
            public String status { get; set; }
            public String transaction_date { get; set; }
            public String recipient_id { get; set; }
            public String amount { get; set; }
            public String recipient_invoice_id { get; set; }

        }
        public class clsParams
        {
            public String SL_CODEMESSAGE { get; set; }
            public String SL_RESULTAT { get; set; }
            public String SL_MESSAGE { get; set; }


        }

    }
}