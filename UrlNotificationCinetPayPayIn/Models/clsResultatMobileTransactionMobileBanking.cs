using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrlNotificationCinetPayPayIn.Models
{
    public class clsResultatMobileTransactionMobileBanking
    {
        private string _AG_CODEAGENCE = "";
        private string _TW_DATESAISIE = "";
        private string _SO_CODESOUSCRIPTION = "";
        private string _TW_CODEVALIDATION = "";
        private string _TR_NUMSEQUENCE = "";
        private string _TW_OPERATIONTRANSACTION = "";
        private string _TW_TELEPHONEHT = "";
        private string _TW_TELEPHONECLIENT = "";
        private string _ENVOISMS = "";
        private string _PV_CODEPOINTVENTE = "";
        private string _CO_CODECOMPTE = "";
        private string _OB_NOMOBJET = "";
        private string _CL_TELEPHONE = "";
        private string _SO_INDICATIF = "";
        private string _OP_CODEOPERATEUR = "";
        private string _MC_DATEPIECE = "";
        private string _CL_IDCLIENT = "";
        private string _SL_MESSAGECLIENT = "";
        private string _SL_LIBELLE1 = "";
        private string _SL_LIBELLE2 = "";
        private string _SL_RESULTATAPI = "";
        private string _SL_MESSAGEAPI = "";

        private string _TW_TAUXHT = "";
        private string _TW_TAUXMF = "";
        private string _TW_TAUXCINETPAY = "";
        private string _SL_URLNOTIFICATION = "";
        private string _SL_CODEMESSAGE = "";
        private string _SL_MESSAGE = "";
        private string _SL_RESULTAT = "";




        public string AG_CODEAGENCE
        {
            get { return _AG_CODEAGENCE; }
            set { _AG_CODEAGENCE = value; }
        }

        public string TW_DATESAISIE
        {
            get { return _TW_DATESAISIE; }
            set { _TW_DATESAISIE = value; }
        }


        public string SO_CODESOUSCRIPTION
        {
            get { return _SO_CODESOUSCRIPTION; }
            set { _SO_CODESOUSCRIPTION = value; }
        }

        public string TW_CODEVALIDATION
        {
            get { return _TW_CODEVALIDATION; }
            set { _TW_CODEVALIDATION = value; }
        }


        public string TR_NUMSEQUENCE
        {
            get { return _TR_NUMSEQUENCE; }
            set { _TR_NUMSEQUENCE = value; }
        }

        public string TW_OPERATIONTRANSACTION
        {
            get { return _TW_OPERATIONTRANSACTION; }
            set { _TW_OPERATIONTRANSACTION = value; }
        }
        public string TW_TELEPHONEHT
        {
            get { return _TW_TELEPHONEHT; }
            set { _TW_TELEPHONEHT = value; }
        }

        public string TW_TELEPHONECLIENT
        {
            get { return _TW_TELEPHONECLIENT; }
            set { _TW_TELEPHONECLIENT = value; }
        }

        public string ENVOISMS
        {
            get { return _ENVOISMS; }
            set { _ENVOISMS = value; }
        }

        public string OB_NOMOBJET
        {
            get { return _OB_NOMOBJET; }
            set { _OB_NOMOBJET = value; }
        }

        public string PV_CODEPOINTVENTE
        {
            get { return _PV_CODEPOINTVENTE; }
            set { _PV_CODEPOINTVENTE = value; }
        }
        public string CO_CODECOMPTE
        {
            get { return _CO_CODECOMPTE; }
            set { _CO_CODECOMPTE = value; }
        }
        public string CL_TELEPHONE
        {
            get { return _CL_TELEPHONE; }
            set { _CL_TELEPHONE = value; }
        }

        public string SO_INDICATIF
        {
            get { return _SO_INDICATIF; }
            set { _SO_INDICATIF = value; }
        }

        public string OP_CODEOPERATEUR
        {
            get { return _OP_CODEOPERATEUR; }
            set { _OP_CODEOPERATEUR = value; }
        }
        public string MC_DATEPIECE
        {
            get { return _MC_DATEPIECE; }
            set { _MC_DATEPIECE = value; }
        }

        public string CL_IDCLIENT
        {
            get { return _CL_IDCLIENT; }
            set { _CL_IDCLIENT = value; }
        }
        public string SL_MESSAGECLIENT
        {
            get { return _SL_MESSAGECLIENT; }
            set { _SL_MESSAGECLIENT = value; }
        }

        public string SL_LIBELLE1
        {
            get { return _SL_LIBELLE1; }
            set { _SL_LIBELLE1 = value; }
        }

        public string SL_LIBELLE2
        {
            get { return _SL_LIBELLE2; }
            set { _SL_LIBELLE2 = value; }
        }

        public string SL_RESULTATAPI
        {
            get { return _SL_RESULTATAPI; }
            set { _SL_RESULTATAPI = value; }
        }
        public string SL_MESSAGEAPI
        {
            get { return _SL_MESSAGEAPI; }
            set { _SL_MESSAGEAPI = value; }
        }




        public string TW_TAUXHT
        {
            get { return _TW_TAUXHT; }
            set { _TW_TAUXHT = value; }
        }
        public string TW_TAUXMF
        {
            get { return _TW_TAUXMF; }
            set { _TW_TAUXMF = value; }
        }

        public string TW_TAUXCINETPAY
        {
            get { return _TW_TAUXCINETPAY; }
            set { _TW_TAUXCINETPAY = value; }
        }

        public string SL_URLNOTIFICATION
        {
            get { return _SL_URLNOTIFICATION; }
            set { _SL_URLNOTIFICATION = value; }
        }


        public string SL_CODEMESSAGE
        {
            get { return _SL_CODEMESSAGE; }
            set { _SL_CODEMESSAGE = value; }
        }

        public string SL_MESSAGE
        {
            get { return _SL_MESSAGE; }
            set { _SL_MESSAGE = value; }
        }


        public string SL_RESULTAT
        {
            get { return _SL_RESULTAT; }
            set { _SL_RESULTAT = value; }
        }


        public clsResultatMobileTransactionMobileBanking() { }


        public clsResultatMobileTransactionMobileBanking(clsResultatMobileTransactionMobileBanking clsResultatMobileTransactionMobileBanking)
        {

            this.AG_CODEAGENCE = clsResultatMobileTransactionMobileBanking.AG_CODEAGENCE;
            this.TW_DATESAISIE = clsResultatMobileTransactionMobileBanking.TW_DATESAISIE;
            this.SO_CODESOUSCRIPTION = clsResultatMobileTransactionMobileBanking.SO_CODESOUSCRIPTION;
            this.TW_CODEVALIDATION = clsResultatMobileTransactionMobileBanking.TW_CODEVALIDATION;
            this.TR_NUMSEQUENCE = clsResultatMobileTransactionMobileBanking.TR_NUMSEQUENCE;
            this.TW_OPERATIONTRANSACTION = clsResultatMobileTransactionMobileBanking.TW_OPERATIONTRANSACTION;
            this.TW_TELEPHONEHT = clsResultatMobileTransactionMobileBanking.TW_TELEPHONEHT;
            this.TW_TELEPHONECLIENT = clsResultatMobileTransactionMobileBanking.TW_TELEPHONECLIENT;
            this.ENVOISMS = clsResultatMobileTransactionMobileBanking.ENVOISMS;
            this.PV_CODEPOINTVENTE = clsResultatMobileTransactionMobileBanking.PV_CODEPOINTVENTE;
            this.CO_CODECOMPTE = clsResultatMobileTransactionMobileBanking.CO_CODECOMPTE;
            this.OB_NOMOBJET = clsResultatMobileTransactionMobileBanking.OB_NOMOBJET;
            this.CL_TELEPHONE = clsResultatMobileTransactionMobileBanking.CL_TELEPHONE;
            this.SO_INDICATIF = clsResultatMobileTransactionMobileBanking.SO_INDICATIF;
            this.OP_CODEOPERATEUR = clsResultatMobileTransactionMobileBanking.OP_CODEOPERATEUR;
            this.MC_DATEPIECE = clsResultatMobileTransactionMobileBanking.MC_DATEPIECE;
            this.CL_IDCLIENT = clsResultatMobileTransactionMobileBanking.CL_IDCLIENT;
            this.SL_MESSAGECLIENT = clsResultatMobileTransactionMobileBanking.SL_MESSAGECLIENT;
            this.SL_LIBELLE1 = clsResultatMobileTransactionMobileBanking.SL_LIBELLE1;
            this.SL_LIBELLE2 = clsResultatMobileTransactionMobileBanking.SL_LIBELLE2;
            this.SL_RESULTATAPI = clsResultatMobileTransactionMobileBanking.SL_RESULTATAPI;
            this.SL_MESSAGEAPI = clsResultatMobileTransactionMobileBanking.SL_MESSAGEAPI;

            this.TW_TAUXHT = clsResultatMobileTransactionMobileBanking.TW_TAUXHT;
            this.TW_TAUXMF = clsResultatMobileTransactionMobileBanking.TW_TAUXMF;
            this.TW_TAUXCINETPAY = clsResultatMobileTransactionMobileBanking.TW_TAUXCINETPAY;
            this.SL_URLNOTIFICATION = clsResultatMobileTransactionMobileBanking.SL_URLNOTIFICATION;
            this.SL_CODEMESSAGE = clsResultatMobileTransactionMobileBanking.SL_CODEMESSAGE;
            this.SL_MESSAGE = clsResultatMobileTransactionMobileBanking.SL_MESSAGE;
            this.SL_RESULTAT = clsResultatMobileTransactionMobileBanking.SL_RESULTAT;
        }
    }
}
