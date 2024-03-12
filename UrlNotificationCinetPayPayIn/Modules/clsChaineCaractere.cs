using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace UrlNotificationCinetPayPayIn.Modules
{
    public class clsChaineCaractere
    {

        #region  declaration unique de la classe

        //declaration unique de la classe clsChaineCaractere pour tout le projet
        private readonly static clsChaineCaractere ClassesChaineCaractere = new clsChaineCaractere();
        int vapCompteur = 0;

        //constructeur privé de la classe clsDeclaration
        //empêchant l'utilisateur d'instancier lui même la classe
        private clsChaineCaractere()
        {
        }
        //constructeur public de la classe fonction 
        public static clsChaineCaractere ClasseChaineCaractere
        {
            get { return ClassesChaineCaractere; }
        }

        #endregion


        //'Extrait une chaine dans une autre chaîne de caractère 
        //en précisant le caractere de separation
        public string pvgExtraireChaine(string vppValeurChaine, string vppCaractereSeparation, string vppPositionDeDepart)
        {
            int i;
            string vppValeurChaineEnCours;
            if (vppPositionDeDepart == "G")
            {
                for (i = 1; i < vppValeurChaine.Length; i++)
                {
                    vppValeurChaineEnCours = vppValeurChaine.Substring(0, i);
                    if (vppValeurChaineEnCours.Contains(vppCaractereSeparation))
                    {
                        return vppValeurChaine.Substring(0, i - 1).Trim();
                    }
                }
            }
            else
            {
                for (i = 1; i < vppValeurChaine.Length; i++)
                {
                    vppValeurChaineEnCours = vppValeurChaine.Substring(0, i);
                    if (vppValeurChaineEnCours.Contains(vppCaractereSeparation)) break;
                }
                vppValeurChaineEnCours = vppValeurChaine.Substring(0, i).Trim();
                if (vppValeurChaineEnCours.Contains(vppCaractereSeparation))
                    return vppValeurChaine.Substring(vppValeurChaineEnCours.Length, vppValeurChaine.Length - vppValeurChaineEnCours.Length).Trim();
                else
                    return vppValeurChaine.Trim();
            }

            return vppValeurChaine.Substring(0, i - 1).Trim();
        }


        public string fpbCryptage(string strChaine, bool blnCryptage, string bvlCleCryptage)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            string strCryptKey = null;
            string strLettre = null;
            string strKeyLettre = null;
            int intLettre = 0;
            int intKeyLettre = 0;
            string strResultat = null;

            if (string.IsNullOrEmpty(strChaine))
            {
                return "";
            }

            strCryptKey = bvlCleCryptage;
            //"chiffrement de Vigenere"

            for (k = 0; k <= 10; k += 1)
            {
                strResultat = "";
                for (i = 1; i <= Strings.Len(strChaine); i += 1)
                {
                    strLettre = Strings.Mid(strChaine, i, 1);
                    j = i;
                    while (j > Strings.Len(strCryptKey))
                    {
                        j = j - Strings.Len(strCryptKey);
                    }
                    strKeyLettre = Strings.Mid(strCryptKey, j, 1);
                    intLettre = Strings.Asc(strLettre);
                    intKeyLettre = Strings.Asc(strKeyLettre);

                    if (blnCryptage == true)
                    {
                        intLettre = intLettre + (intKeyLettre * Strings.Len(strChaine));
                    }
                    else
                    {
                        intLettre = intLettre - (intKeyLettre * Strings.Len(strChaine));
                    }

                    while (intLettre > 255)
                    {
                        intLettre = intLettre - 255;
                    }
                    while (intLettre < 0)
                    {
                        intLettre = intLettre + 255;
                    }
                    strResultat = strResultat + Strings.Chr(intLettre); 
                }
                strChaine = strResultat;
            }
            return strResultat;
        }
        //


        //'Extrait les codes dans une chaîne de caractère avec code et libellé
        public string pvgExtraireCode(string vppValeurChaine)
        {
            string vppValeurChaineEnCours;
            int i;
            for (i = 1; i < vppValeurChaine.Length; i++)
            {
                vppValeurChaineEnCours = vppValeurChaine.Substring(0, i);
                if (vppValeurChaineEnCours.Contains("-") || vppValeurChaineEnCours.Contains(".") || vppValeurChaineEnCours.Contains("(") || vppValeurChaineEnCours.Contains("|"))
                {
                    return vppValeurChaine.Substring(0, i - 1).Trim();
                }
            }
            return vppValeurChaine.Substring(0, i - 1).Trim();
        }

        //'Extrait les libellés dans une chaîne de caractère avec code et libéllé
        public string pvgExtraireLibellé(string vppValeurChaine)
        {
            string vppValeurChaineEnCours = "";
            int i;
            for (i = 1; i < vppValeurChaine.Length; i++)
            {
                vppValeurChaineEnCours = vppValeurChaine.Substring(0, i);
                if (vppValeurChaineEnCours.Contains("-"))// || vppValeurChaineEnCours.Contains(".") || vppValeurChaineEnCours.Contains("(") || vppValeurChaineEnCours.Contains("|")
                {
                    break;
                }
            }
            vppValeurChaineEnCours = vppValeurChaine.Substring(0, i).Trim();
            if (vppValeurChaineEnCours.Contains("-"))
            {
                return vppValeurChaine.Substring(vppValeurChaineEnCours.Length, vppValeurChaine.Length - vppValeurChaineEnCours.Length).Trim();//Trim(Right(BvlObjetTexte, Len(BvlObjetTexte) - Len(ppvObjet)))
            }
            else
            {
                return vppValeurChaine.Trim();
            }
        }


        public bool pvgTestLibelle(string vppValeurSaisie)
        {
            bool vlpResultat = true;
            try
            {
                if(vppValeurSaisie.Length>150)
                {
                    vlpResultat = false;
                }
                return vlpResultat;


            }
            catch
            {
                return false;
                //XtraMessageBox.Show("Cette date n'est pas valide !");
            }
        }

        public bool  pvgValidationEmail(string email)
         {
             Regex _valideMail = new Regex(@"[a-zA-Z0-9_.-]{4,30}@{1}[a-zA-Z\d.-]{3,63}\.{1}[a-zA-Z]{2,4}");
            if(email!="")
            {
                if (_valideMail.IsMatch(email))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
          //set modele = New RegExp;
          //modele.pattern = "^[\w_.~-]+@[\w][\w.\-]*[\w]\.[\w][\w.]*[a-zA-Z]$";
          //modele.global = true;
          //ValidationEmail = modele.test(email);
         }
         public bool pvgValidationHeure(string vppHeure)
         {
             //Regex _valideMail = new Regex("([01][0-9]|2[0-3]):([0-5][0-9])");
             Regex _valideMail = new Regex("([0-1][0-9]|2[0-3]):([0-5][0-9])");
             if (vppHeure != "")
             {
                 if (!_valideMail.IsMatch(vppHeure)) return false;
             }
             return true;
         }

         /// <summary>
         /// Cette fonction permet de crypter une chaine de caractere
         /// </summary>
         /// <param name="strChaine">chaine à crypter</param>
         /// <returns></returns>
         public string pvgCrypter(string strChaine)
         {
             //for(long i=0; strChaine.Split(new str[]{},StringSplitOptions.None )
             string vlpValeur = "";
             for (int i = 0; i < strChaine.Length; i++)
             {
                 vlpValeur += pvgGenererCaractere(3);
                 switch (strChaine.Substring(i, 1))
                 {
                     #region MAJUSCULES
                     case "A":
                         vlpValeur += "PTTUI";
                         break;
                     case "B":
                         vlpValeur += "KDGER";
                         break;
                     case "C":
                         vlpValeur += "ORYHF";
                         break;
                     case "D":
                         vlpValeur += "QSCER";
                         break;
                     case "E":
                         vlpValeur += "SEWJU";
                         break;
                     case "F":
                         vlpValeur += "NHYUI";
                         break;
                     case "G":
                         vlpValeur += "LOMJF";
                         break;
                     case "H":
                         vlpValeur += ";LOPU";
                         break;
                     case "I":
                         vlpValeur += ",DEFG";
                         break;
                     case "J":
                         vlpValeur += "TYEWO";
                         break;
                     case "K":
                         vlpValeur += "XZSAQ";
                         break;
                     case "L":
                         vlpValeur += "M,KL;";
                         break;
                     case "M":
                         vlpValeur += "U;RTG";
                         break;
                     case "N":
                         vlpValeur += "DV.DF";
                         break;
                     case "O":
                         vlpValeur += "EH.,K";
                         break;
                     case "P":
                         vlpValeur += ":JHT,";
                         break;
                     case "Q":
                         vlpValeur += "B-012";
                         break;
                     case "R":
                         vlpValeur += "9URYT";
                         break;
                     case "S":
                         vlpValeur += "120GF";
                         break;
                     case "T":
                         vlpValeur += "0T1ER";
                         break;
                     case "U":
                         vlpValeur += "GR;,.";
                         break;
                     case "V":
                         vlpValeur += "ET,IO";
                         break;
                     case "W":
                         vlpValeur += "5C#9@";
                         break;
                     case "X":
                         vlpValeur += "78JNB";
                         break;
                     case "Y":
                         vlpValeur += "3T45H";
                         break;
                     case "Z":
                         vlpValeur += "4ORTE";
                         break;

                     #endregion Majuscule
                     #region MINUSCULE
                     case "a":
                         vlpValeur += "pttui";
                         break;
                     case "b":
                         vlpValeur += "kdger";
                         break;
                     case "c":
                         vlpValeur += "oryhf";
                         break;
                     case "d":
                         vlpValeur += "qscer";
                         break;
                     case "e":
                         vlpValeur += "sewju";
                         break;
                     case "f":
                         vlpValeur += "nhyui";
                         break;
                     case "g":
                         vlpValeur += "lomjf";
                         break;
                     case "h":
                         vlpValeur += ";lopu";
                         break;
                     case "i":
                         vlpValeur += ",defg";
                         break;
                     case "j":
                         vlpValeur += "tyewo";
                         break;
                     case "k":
                         vlpValeur += "xzsaq";
                         break;
                     case "l":
                         vlpValeur += "m,kl;";
                         break;
                     case "m":
                         vlpValeur += "u;rtg";
                         break;
                     case "n":
                         vlpValeur += "dv.df";
                         break;
                     case "o":
                         vlpValeur += "eh.,k";
                         break;
                     case "p":
                         vlpValeur += ":jht,";
                         break;
                     case "q":
                         vlpValeur += "b-012";
                         break;
                     case "r":
                         vlpValeur += "9uryt";
                         break;
                     case "s":
                         vlpValeur += "120gf";
                         break;
                     case "t":
                         vlpValeur += "0t1er";
                         break;
                     case "u":
                         vlpValeur += "gr;,.";
                         break;
                     case "v":
                         vlpValeur += "et,io";
                         break;
                     case "w":
                         vlpValeur += "5c#9@";
                         break;
                     case "x":
                         vlpValeur += "78jnb";
                         break;
                     case "y":
                         vlpValeur += "3t45h";
                         break;
                     case "z":
                         vlpValeur += "4orte";
                         break;

                     #endregion MINUSCULE
                     #region SYMBOLES
                     case " ":
                         vlpValeur += "W_HRT";
                         break;
                     case ":":
                         vlpValeur += "#FyG7";
                         break;
                     case ",":
                         vlpValeur += "=RaGD";
                         break;
                     case "?":
                         vlpValeur += "+TDG.";
                         break;
                     case "=":
                         vlpValeur += "9-_EW";
                         break;
                     case "{":
                         vlpValeur += "YTe74";
                         break;
                     case "}":
                         vlpValeur += ";)9vR";
                         break;
                     case "(":
                         vlpValeur += "[=-9T";
                         break;
                     case ")":
                         vlpValeur += "A;lFJ";
                         break;
                     case "[":
                         vlpValeur += "e49uE";
                         break;
                     case "]":
                         vlpValeur += ".zaAw";
                         break;
                     case ".":
                         vlpValeur += "qWegl";
                         break;
                     case "\\":
                         vlpValeur += "kKwyu";
                         break;
                     case "@":
                         vlpValeur += "y#udl";
                         break;
                     case "#":
                         vlpValeur += "wen_-";
                         break;
                     case ";":
                         vlpValeur += "A7oWe";
                         break;
                     #endregion SYMBOLES
                     #region CHIFFRES
                     case "0":
                         vlpValeur += "RE09=";
                         break;
                     case "1":
                         vlpValeur += "8UYRM";
                         break;
                     case "2":
                         vlpValeur += "O9)50";
                         break;
                     case "3":
                         vlpValeur += "%67RE";
                         break;
                     case "4":
                         vlpValeur += "i56%W";
                         break;
                     case "5":
                         vlpValeur += "IOPQW";
                         break;
                     case "6":
                         vlpValeur += "R89-=";
                         break;
                     case "7":
                         vlpValeur += "{OTU-";
                         break;
                     case "8":
                         vlpValeur += "}iT_e";
                         break;
                     case "9":
                         vlpValeur += "}-=GF";
                         break;
                     #endregion CHIFFRES
                     default:
                        // DevExpress.XtraEditors.XtraMessageBox.Show(strChaine.Substring(i, 1));
                         break;

                 }


             }
             return vlpValeur;
         }
         /// <summary>
         /// Cette fonction permet de décrypter une chaine de caractere
         /// </summary>
         /// <param name="strChaine">chaine à décrypter</param>
         /// <returns></returns>
         public string pvgDeCrypter(string strChaine)
         {

             string vlpValeurNette = "";
             while (strChaine.Length > 0)
             {
                 //ici on extrait les caracteres de remplissage
                 if (strChaine.Length > 0)
                 {
                     strChaine = strChaine.Remove(0, 3);
                     vlpValeurNette += strChaine.Substring(0, 5);
                     strChaine = strChaine.Remove(0, 5);
                     if (strChaine.Length > 0 && strChaine.Length < 5)
                     {
                         vlpValeurNette = strChaine = "";
                     }
                 }
             }

             string vlpValeur = "";
             for (int i = 0; i < vlpValeurNette.Length; i += 5)
             {
                 switch (vlpValeurNette.Substring(i, 5))
                 {
                     #region MAJUSCULES
                     case "PTTUI":
                         vlpValeur += "A";
                         break;
                     case "KDGER":
                         vlpValeur += "B";
                         break;
                     case "ORYHF":
                         vlpValeur += "C";
                         break;
                     case "QSCER":
                         vlpValeur += "D";
                         break;
                     case "SEWJU":
                         vlpValeur += "E";
                         break;
                     case "NHYUI":
                         vlpValeur += "F";
                         break;
                     case "LOMJF":
                         vlpValeur += "G";
                         break;
                     case ";LOPU":
                         vlpValeur += "H";
                         break;
                     case ",DEFG":
                         vlpValeur += "I";
                         break;
                     case "TYEWO":
                         vlpValeur += "J";
                         break;
                     case "XZSAQ":
                         vlpValeur += "K";
                         break;
                     case "M,KL;":
                         vlpValeur += "L";
                         break;
                     case "U;RTG":
                         vlpValeur += "M";
                         break;
                     case "DV.DF":
                         vlpValeur += "N";
                         break;
                     case "EH.,K":
                         vlpValeur += "O";
                         break;
                     case ":JHT,":
                         vlpValeur += "P";
                         break;
                     case "B-012":
                         vlpValeur += "Q";
                         break;
                     case "9URYT":
                         vlpValeur += "R";
                         break;
                     case "120GF":
                         vlpValeur += "S";
                         break;
                     case "0T1ER":
                         vlpValeur += "T";
                         break;
                     case "GR;,.":
                         vlpValeur += "U";
                         break;
                     case "ET,IO":
                         vlpValeur += "V";
                         break;
                     case "5C#9@":
                         vlpValeur += "W";
                         break;
                     case "78JNB":
                         vlpValeur += "X";
                         break;
                     case "3T45H":
                         vlpValeur += "Y";
                         break;
                     case "4ORTE":
                         vlpValeur += "Z";
                         break;

                     #endregion Majuscule
                     #region MINUSCULE
                     case "pttui":
                         vlpValeur += "a";
                         break;
                     case "kdger":
                         vlpValeur += "b";
                         break;
                     case "oryhf":
                         vlpValeur += "c";
                         break;
                     case "qscer":
                         vlpValeur += "d";
                         break;
                     case "sewju":
                         vlpValeur += "e";
                         break;
                     case "nhyui":
                         vlpValeur += "f";
                         break;
                     case "lomjf":
                         vlpValeur += "g";
                         break;
                     case ";lopu":
                         vlpValeur += "h";
                         break;
                     case ",defg":
                         vlpValeur += "i";
                         break;
                     case "tyewo":
                         vlpValeur += "j";
                         break;
                     case "xzsaq":
                         vlpValeur += "k";
                         break;
                     case "m,kl;":
                         vlpValeur += "l";
                         break;
                     case "u;rtg":
                         vlpValeur += "m";
                         break;
                     case "dv.df":
                         vlpValeur += "n";
                         break;
                     case "eh.,k":
                         vlpValeur += "o";
                         break;
                     case ":jht,":
                         vlpValeur += "p";
                         break;
                     case "b-012":
                         vlpValeur += "q";
                         break;
                     case "9uryt":
                         vlpValeur += "r";
                         break;
                     case "120gf":
                         vlpValeur += "s";
                         break;
                     case "0t1er":
                         vlpValeur += "t";
                         break;
                     case "gr;,.":
                         vlpValeur += "u";
                         break;
                     case "et,io":
                         vlpValeur += "v";
                         break;
                     case "5c#9@":
                         vlpValeur += "w";
                         break;
                     case "78jnb":
                         vlpValeur += "x";
                         break;
                     case "3t45h":
                         vlpValeur += "y";
                         break;
                     case "4orte":
                         vlpValeur += "z";
                         break;
                     #endregion MINUSCULE
                     #region SYMBOLES
                     case "W_HRT":
                         vlpValeur += " ";
                         break;
                     case "#FyG7":
                         vlpValeur += ":";
                         break;
                     case "=RaGD":
                         vlpValeur += ",";
                         break;
                     case "+TDG.":
                         vlpValeur += "?";
                         break;
                     case "9-_EW":
                         vlpValeur += "=";
                         break;
                     case "YTe74":
                         vlpValeur += "{";
                         break;
                     case ";)9vR":
                         vlpValeur += "}";
                         break;
                     case "[=-9T":
                         vlpValeur += "(";
                         break;
                     case "A;lFJ":
                         vlpValeur += ")";
                         break;
                     case "e49uE":
                         vlpValeur += "[";
                         break;
                     case ".zaAw":
                         vlpValeur += "]";
                         break;
                     case "qWegl":
                         vlpValeur += ".";
                         break;
                     case "kKwyu":
                         vlpValeur += "\\";
                         break;
                     case "y#udl":
                         vlpValeur += "@";
                         break;
                     case "wen_-":
                         vlpValeur += "#";
                         break;
                     case "A7oWe":
                         vlpValeur += ";";
                         break;
                     #endregion SYMBOLES
                     #region CHIFFRES
                     case "RE09=":
                         vlpValeur += "0";
                         break;
                     case "8UYRM":
                         vlpValeur += "1";
                         break;
                     case "O9)50":
                         vlpValeur += "2";
                         break;
                     case "%67RE":
                         vlpValeur += "3";
                         break;
                     case "i56%W":
                         vlpValeur += "4";
                         break;
                     case "IOPQW":
                         vlpValeur += "5";
                         break;
                     case "R89-=":
                         vlpValeur += "6";
                         break;
                     case "{OTU-":
                         vlpValeur += "7";
                         break;
                     case "}iT_e":
                         vlpValeur += "8";
                         break;
                     case "}-=GF":
                         vlpValeur += "9";
                         break;
                     #endregion CHIFFRES
                 }

             }
             return vlpValeur;
         }

         public string pvgGenererCaractere(int vppNombreCaractere)
         {
             string vlpChaine = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789[]{}=-_+%^#@!wajeWEJI?OoptT/bnJgFDdVFHfRDScMD1U*%nopqrstuv*/|,.:;àè`ùáéíóýñ~wxyzABCDEFGHIJKLMNOPQRSTU";
             string vlpChaineRetour = "";
             Random random = new Random(DateTime.Now.Millisecond + vapCompteur);
             for (int i = 0; i < vppNombreCaractere; i++)
             {
                 vlpChaineRetour += vlpChaine.Substring(random.Next(0, vlpChaine.Length - 1), 1);
             }
             vapCompteur = vapCompteur < 2000 ? vapCompteur + 1 : 0;
             return vlpChaineRetour;
         }

         /// <summary>
         /// cette procedure permet de generer un numero de bordereau 
         /// </summary>
         /// <param name="vppChaineFixe">chaine fixe de debut</param>
         /// <param name="vppChaineAFormater">chaine a formater</param>
         /// <param name="vppLongueurFormat">longueur du formatage</param>
         /// <returns></returns>
         public string pvgGenererCode(string vppChaineFixe, string vppChaineAFormater, int vppLongueurFormat)
         {
             try
             {
                 string vlpChaineFormat = "{0:0";
                 for (int i = 0; i < vppLongueurFormat - 1; i++)
                 {
                     vlpChaineFormat += "0";
                 }
                 vlpChaineFormat += "}";
                 vppChaineFixe += String.Format(vlpChaineFormat, vppChaineAFormater);
             }
             catch (Exception ex)
             {
                 throw (ex);
             }
             return vppChaineFixe;
         }
    }
}
