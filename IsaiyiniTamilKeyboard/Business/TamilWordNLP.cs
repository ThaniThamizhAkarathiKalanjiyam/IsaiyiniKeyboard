//////////////////////////////////////////////////////////////////////////
//                                                                      //
//  Anybody can Use, Modify, Redistribute this code freely. If this     // 
//  module has been helpful to you then just leave a comment on Website //
// Name:        IsaiyiniKeyboard
// Purpose:	 Accessing the TACE keyboard easily
//
// Author:      Pitchaimuthu
//
// Created:     24/04/2019
// Copyright:   (c) Pitchaimuthu 2019
// Licence:     This work is licensed under a CCA 4.0 International License. 
//              http://creativecommons.org/licenses/by/4.0/
//                                                                      //
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CaretPosition.Entity.ITRANS;

namespace CaretPosition
{
    public partial class TamilWordNLP
    {
        //List<ITRANS_ta_Schema> consonants = new List<ITRANS_ta_Schema> { 
        //new ITRANS_ta_Schema("k","க",""),
        //new ITRANS_ta_Schema("~N","ங",""),
        //new ITRANS_ta_Schema("ch","ச",""),
        //new ITRANS_ta_Schema("~n","ஞ",""),
        //new ITRANS_ta_Schema("T","ட",""),
        //new ITRANS_ta_Schema("N","ண",""),
        //new ITRANS_ta_Schema("t","த",""),
        //new ITRANS_ta_Schema("n","ந",""),
        //new ITRANS_ta_Schema("^n","ன",""),
        //new ITRANS_ta_Schema("p","ப",""),
        //new ITRANS_ta_Schema("m","ம",""),
        //new ITRANS_ta_Schema("y","ய",""),
        //new ITRANS_ta_Schema("r","ர",""),
        //new ITRANS_ta_Schema("l","ல",""),
        //new ITRANS_ta_Schema("v","வ",""),
        //new ITRANS_ta_Schema("z","ழ",""),
        //new ITRANS_ta_Schema("L","ள",""),
        //new ITRANS_ta_Schema("R","ற",""),
        //new ITRANS_ta_Schema("Sh","ஷ",""),
        //new ITRANS_ta_Schema("s","ஸ",""),
        //new ITRANS_ta_Schema("j","ஜ",""),
        //new ITRANS_ta_Schema("h","ஹ","")        
        //};

        List<ITRANS_ta_Schema> consonants = new List<ITRANS_ta_Schema> { 
new ITRANS_ta_Schema("k",			"க",			""),
new ITRANS_ta_Schema("~N",			"ங",			""),
new ITRANS_ta_Schema("ch",			"ச",			""),
new ITRANS_ta_Schema("~n",			"ஞ",			""),
new ITRANS_ta_Schema("T",			"ட",			""),
new ITRANS_ta_Schema("N",			"ண",			""),
new ITRANS_ta_Schema("t",			"த",			""),
new ITRANS_ta_Schema("n",			"ந",			""),
new ITRANS_ta_Schema("^n",			"ன",			""),
new ITRANS_ta_Schema("p",			"ப",			""),
new ITRANS_ta_Schema("m",			"ம",			""),
new ITRANS_ta_Schema("y",			"ய",			""),
new ITRANS_ta_Schema("r",			"ர",				""),
new ITRANS_ta_Schema("l",			"ல",			""),
new ITRANS_ta_Schema("v",			"வ",			""),
new ITRANS_ta_Schema("z",			"ழ",			""),
new ITRANS_ta_Schema("L",			"ள",			""),
new ITRANS_ta_Schema("R",			"ற",			""),
new ITRANS_ta_Schema("Sh",			"ஷ",			""),
new ITRANS_ta_Schema("s",			"ஸ",			""),
new ITRANS_ta_Schema("j",			"ஜ",			""),
new ITRANS_ta_Schema("h",			"ஹ",			"")        
};

        //List<ITRANS_ta_Schema> vowels = new List<ITRANS_ta_Schema> { 
        //new ITRANS_ta_Schema("a","அ",""),
        //new ITRANS_ta_Schema("A","ஆ",""),
        //new ITRANS_ta_Schema("i","இ",""),
        //new ITRANS_ta_Schema("I","ஈ",""),
        //new ITRANS_ta_Schema("u","உ",""),
        //new ITRANS_ta_Schema("U","ஊ",""),
        //new ITRANS_ta_Schema("e","எ",""),
        //new ITRANS_ta_Schema("E","ஏ",""),
        //new ITRANS_ta_Schema("ai","ஐ",""),
        //new ITRANS_ta_Schema("o","ஒ",""),
        //new ITRANS_ta_Schema("O","ஓ",""),
        //new ITRANS_ta_Schema("au","ஔ","")      
        //};

        List<ITRANS_ta_Schema> vowels = new List<ITRANS_ta_Schema> { 
new ITRANS_ta_Schema("a",			"அ",			""),
new ITRANS_ta_Schema("A",			"ஆ",			""),
new ITRANS_ta_Schema("i",			"இ",			""),
new ITRANS_ta_Schema("I",			"ஈ",			""),
new ITRANS_ta_Schema("u",			"உ",			""),
new ITRANS_ta_Schema("U",			"ஊ",			""),
new ITRANS_ta_Schema("e",			"எ",			""),
new ITRANS_ta_Schema("E",			"ஏ",			""),
new ITRANS_ta_Schema("ai",			"ஐ",			""),
new ITRANS_ta_Schema("o",			"ஒ",			""),
new ITRANS_ta_Schema("O",			"ஓ",			""),
new ITRANS_ta_Schema("au",			"ஔ",			"")      
};

        //    List<ITRANS_ta_Schema> punctuations = new List<ITRANS_ta_Schema> { 
        //    new ITRANS_ta_Schema("`","`",""),
        //    new ITRANS_ta_Schema("!","!",""),
        //    new ITRANS_ta_Schema("-","-",""),
        //    new ITRANS_ta_Schema("@","@",""),
        //    new ITRANS_ta_Schema("$","$",""),
        //    new ITRANS_ta_Schema("%","%",""),
        //    new ITRANS_ta_Schema("&","&",""),
        //    new ITRANS_ta_Schema("#","#",""),
        //    new ITRANS_ta_Schema("(","(",""),
        //    new ITRANS_ta_Schema(")",")",""),
        //    new ITRANS_ta_Schema("_","_",""),
        //    new ITRANS_ta_Schema("+","+",""),
        //    new ITRANS_ta_Schema("=","=",""),
        //    new ITRANS_ta_Schema("|","|",""),
        //    new ITRANS_ta_Schema("\\","\\",""),
        //    new ITRANS_ta_Schema("[","[",""),
        //    new ITRANS_ta_Schema("]","]",""),
        //    new ITRANS_ta_Schema("<","<",""),
        //    new ITRANS_ta_Schema(">",">",""),
        //    new ITRANS_ta_Schema("?","?",""),
        //    new ITRANS_ta_Schema("/","/",""),
        //    new ITRANS_ta_Schema(";",";",""),
        //    new ITRANS_ta_Schema(":",":",""),
        //    new ITRANS_ta_Schema("\"","\"",""),
        //    new ITRANS_ta_Schema("'","'",""),
        //    new ITRANS_ta_Schema(",",",","")
        //};
        List<ITRANS_ta_Schema> punctuations = new List<ITRANS_ta_Schema> { 
new ITRANS_ta_Schema("`",			"`",			"`"),
new ITRANS_ta_Schema("!",			"!",			"!"),
new ITRANS_ta_Schema("-",			"-",			"-"),
new ITRANS_ta_Schema("@",			"@",			"@"),
new ITRANS_ta_Schema("$",			"$",			"$"),
new ITRANS_ta_Schema("%",			"%",			"%"),
new ITRANS_ta_Schema("&",			"&",			"&"),
new ITRANS_ta_Schema("#",			"#",			"#"),
new ITRANS_ta_Schema("(",			"(",			"("),
new ITRANS_ta_Schema(")",			")",			")"),
new ITRANS_ta_Schema("_",			"_",			"_"),
new ITRANS_ta_Schema("+",			"+",			"+"),
new ITRANS_ta_Schema("=",			"=",			"="),
new ITRANS_ta_Schema("|",			"|",			"|"),
new ITRANS_ta_Schema("\\",			"\\",			"\\"),
new ITRANS_ta_Schema("[",			"[",			"["),
new ITRANS_ta_Schema("]",			"]",			"]"),
new ITRANS_ta_Schema("<",			"<",			"<"),
new ITRANS_ta_Schema(">",			">",			">"),
new ITRANS_ta_Schema("?",			"?",			"?"),
new ITRANS_ta_Schema("/",			"/",			"/"),
new ITRANS_ta_Schema(";",			";",			";"),
new ITRANS_ta_Schema(":",			":",			":"),
new ITRANS_ta_Schema("\"",			"\"",			"\""),
new ITRANS_ta_Schema("'",			"'",			"'"),
new ITRANS_ta_Schema(",",				",",			",")
};

        //List<string> vowel_sylop = new List<string>();

        //List<string> _vowel_sylop = new List<string>() {         
        ////"",
        //"ா",
        //"ி",
        //"ீ",
        //"ு",
        //"ூ",
        //"ெ",
        //"ே",
        //"ை",
        //"ொ",
        //"ோ",
        //"ௌ",
        //"்"
        //};


        List<ITRANS_ta_Schema> _vowel_sylop = new List<ITRANS_ta_Schema> { 
//new ITRANS_ta_Schema("a",			//"",			""),
new ITRANS_ta_Schema("A",			"ா",			""),
new ITRANS_ta_Schema("i",			"ி",			""),
new ITRANS_ta_Schema("I",			"ீ",			""),
new ITRANS_ta_Schema("u",			"ு",			""),
new ITRANS_ta_Schema("U",			"ூ",			""),
new ITRANS_ta_Schema("e",			"ெ",			""),
new ITRANS_ta_Schema("E",			"ே",			""),
new ITRANS_ta_Schema("ai",			"ை",			""),
new ITRANS_ta_Schema("o",			"ொ",			""),
new ITRANS_ta_Schema("O",			"ோ",			""),
new ITRANS_ta_Schema("au",			"ௌ",			""),    
//new ITRANS_ta_Schema("",			"்",			"்")   
        };

        List<ITRANS_ta_Schema> vowel_consonants = new List<ITRANS_ta_Schema>();

        public TamilWordNLP()
        {

        }



        List<ITRANS_ta_Schema> lstiTRANSMapChars = new List<ITRANS_ta_Schema>();
        List<ITRANS_ta_Schema> lstiUserDefMapChars = new List<ITRANS_ta_Schema>();

        TaEncodingE encoding = TaEncodingE.ANSI;

        internal TaEncodingE taEncoding
        {
            get { return encoding; }
            set { encoding = value; }
        }

        public string getEnglishTransliterationFile(string tamilWordInUTF)
        {
            return "";
        }

        public string getTamil2EnglishTransliteration(string tamilWordInUTF)
        {
            AppendTRANSMapChars();
            //lstiTRANSMapChars.Reverse();
            foreach (ITRANS_ta_Schema fmc in lstiTRANSMapChars)
            {
                if (taEncoding == TaEncodingE.TACE)
                {
                    tamilWordInUTF = tamilWordInUTF.Replace(fmc.TaCharTACE,
                            getCorrespondingEncodeVal(fmc));
                }
                else
                {
                    tamilWordInUTF = tamilWordInUTF.Replace(fmc.TaCharUtf8,
                            getCorrespondingEncodeVal(fmc));
                }
            }

            return tamilWordInUTF;
        }

        public string getEnglishTransliterationByUSerDict(string tamilWordInUTF)
        {
            AppendTRANSMapChars();
            //lstiTRANSMapChars.Reverse();
            foreach (ITRANS_ta_Schema fmc in lstiUserDefMapChars)
            {
                if (taEncoding == TaEncodingE.TACE)
                {
                    tamilWordInUTF = tamilWordInUTF.Replace(fmc.itransEngChar,
                            getCorrespondingEncodeVal(fmc));
                }
                else
                {
                    tamilWordInUTF = tamilWordInUTF.Replace(fmc.itransEngChar,
                            getCorrespondingEncodeVal(fmc));
                }
            }

            return tamilWordInUTF.Replace(" ", "") + " ";
        }

        public string getEnglishTransliteration(string tamilWordInUTF)
        {
            AppendTRANSMapChars();
            //lstiTRANSMapChars.Reverse();
            foreach (ITRANS_ta_Schema fmc in lstiTRANSMapChars)
            {
                if (taEncoding == TaEncodingE.TACE)
                {
                    tamilWordInUTF = tamilWordInUTF.Replace(fmc.itransEngChar,
                            getCorrespondingEncodeVal(fmc));
                }
                else
                {
                    tamilWordInUTF = tamilWordInUTF.Replace(fmc.itransEngChar,
                            getCorrespondingEncodeVal(fmc));
                }
            }

            return tamilWordInUTF.Replace(" ", "") + " ";
        }

        private void AppendTRANSMapChars()
        {
            if (lstiTRANSMapChars.Count() == 0)
            {
                //lstiTRANSMapChars.AddRange(getMapTrans3());
                //lstiTRANSMapChars.AddRange(getItransMap());

                //Assets/data/user_dictionary.txt
                lstiUserDefMapChars.AddRange(getItransMap("Assets/data/user_dictionary.txt"));
                //Assets/ITRANS.txt
                lstiTRANSMapChars.AddRange(getItransMap("Assets/ITRANS.txt"));
                //lstiTRANSMapChars.AddRange(getItransMapV2());
            }
        }

        public List<string> getSuggestedWordslistV2(StringBuilder sb)
        {


            List<string> sugg = new List<string>();

            foreach (char wordLetter in sb.ToString())
            {
                if (sugg.Count == 0)
                {
                    List<ITRANS_ta_Schema> objFontMapChars = lstiTRANSMapChars.
                        FindAll(W => W.itransEngChar.StartsWith(wordLetter.ToString().Trim()));
                    if (objFontMapChars != null)
                    {
                        sugg.AddRange(objFontMapChars
                            .Select(
                            s => s.TaCharUtf8
                            ).ToArray());
                    }
                }
                else
                {
                    List<ITRANS_ta_Schema> objFontMapChars = lstiTRANSMapChars.
                            FindAll(W => W.itransEngChar
                                .StartsWith(wordLetter.ToString().Trim()));
                    if (objFontMapChars != null)
                    {
                        sugg.AddRange(objFontMapChars
                            .Select(
                            s => s.TaCharUtf8
                            ).ToArray());
                    }

                }
            }

            if (sb.ToString().EndsWith(" ") == true)
            {
                sugg.Add(sb.ToString());
            }
            else
            {
                foreach (ITRANS_ta_Schema fmc in lstiTRANSMapChars)
                {
                    sb = sb.Replace(fmc.itransEngChar,
                            getCorrespondingEncodeVal(fmc));
                }
                sugg.Add(sb.ToString());
            }

            return sugg;
        }

        public string[] getSuggestedWordslist(StringBuilder sb)
        {
            List<string> sugg = new List<string>();

            if (sb.Length != 0)
            {


                if (sb.ToString().EndsWith(" ") == true)
                {
                    sugg.Add(sb.ToString());

                    foreach (char wordLetter in sb.ToString())
                    {
                        ITRANS_ta_Schema objFontMapChars = lstiTRANSMapChars.FirstOrDefault(W => W.itransEngChar == wordLetter.ToString().Trim());

                        if (objFontMapChars != null)
                        {
                            sugg.Add(getCorrespondingEncodeVal(objFontMapChars));
                        }
                    }
                }
                else
                {
                    foreach (ITRANS_ta_Schema fmc in lstiTRANSMapChars)
                    {
                        sb = sb.Replace(fmc.itransEngChar,
                                getCorrespondingEncodeVal(fmc));
                    }
                    sugg.Add(sb.ToString());
                }
            }
            return sugg.ToArray();
        }

        private string getCorrespondingEncodeVal(ITRANS_ta_Schema fmc)
        {
            if (encoding == TaEncodingE.TACE)
            {
                return fmc.TaCharTACE;
            }
            if (encoding == TaEncodingE.ANSI)
            {
                return fmc.itransEngChar;
            }
            else
            {
                return fmc.TaCharUtf8;
            }
        }

        public List<ITRANS_ta_Schema> getItransMapV2()
        {
            List<ITRANS_ta_Schema> lstFontMapChars = new List<ITRANS_ta_Schema>();

            lstFontMapChars.AddRange(vowel_consonants);//vowel_consonants
            lstFontMapChars.AddRange(vowels);
            lstFontMapChars.AddRange(consonants);


            foreach (ITRANS_ta_Schema vow in _vowel_sylop)
            {
                foreach (ITRANS_ta_Schema con in consonants)
                {
                    lstFontMapChars.Add(new ITRANS_ta_Schema
                    {
                        itransEngChar = con.itransEngChar + vow.itransEngChar,
                        TaCharUtf8 = con.TaCharUtf8 + vow.TaCharUtf8
                    });
                }
            }


            return lstFontMapChars;
        }

        public void readAllBinaryFile(string inputFilename,
            string outputFilename)
        {

            byte[] fileBytes = File.ReadAllBytes(inputFilename);
            StringBuilder sb = new StringBuilder();

            foreach (byte b in fileBytes)
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }

            File.WriteAllText(outputFilename, sb.ToString());
        }

        private List<ITRANS_ta_Schema> getMapTrans3()
        {
            List<ITRANS_ta_Schema> lstFontMapChars3 = new List<ITRANS_ta_Schema>();

            foreach (ITRANS_ta_Schema obj in _vowel_sylop)
            {
                foreach (ITRANS_ta_Schema objcons in consonants)
                {
                    lstFontMapChars3.Add(
                         new ITRANS_ta_Schema(

                             objcons.itransEngChar + obj.itransEngChar,
                             objcons.TaCharUtf8 + obj.TaCharUtf8,
                             ""


                             )

                        );
                }
            }

            return lstFontMapChars3;
        }

        public List<ITRANS_ta_Schema> getItransMap(string givenTransFileName)
        {
            //string[] fontToUTF8Map = File.ReadAllLines("Assets/ITRANS.txt",
            string[] fontToUTF8Map = File.ReadAllLines(givenTransFileName,
                  Encoding.UTF8);

            List<ITRANS_ta_Schema> lstFontMapChars = new List<ITRANS_ta_Schema>();


            foreach (string strLine in fontToUTF8Map)
            {
                string[] mapstr = strLine.Split(' ');

                if (mapstr[0].Contains(','))
                {
                    string[] subtachars = mapstr[0].Split(',');
                    foreach (string substr in subtachars)
                    {
                        lstFontMapChars.Add(new ITRANS_ta_Schema()
                        {
                            itransEngChar = substr,
                            TaCharUtf8 = mapstr[1],
                            TaCharTACE = mapstr[2]
                        });
                    }
                }
                else if (mapstr.Length == 3)
                {
                    lstFontMapChars.Add(new ITRANS_ta_Schema()
                    {
                        itransEngChar = mapstr[0],
                        TaCharUtf8 = mapstr[1],
                        TaCharTACE = mapstr[2]
                    });
                }
            }
            return lstFontMapChars;
        }

        public List<IDictionaryTamil> loadEnglishTranslitDict(string filename)
        {
            List<IDictionaryTamil> obj = new List<IDictionaryTamil>();

            string preEngWord = "";
            File.ReadAllLines(filename)
                .ToList()
                .ForEach(
               fe =>
               {

                   var loc = fe.Split(' ');
                   if (loc.Length == 3)
                   {
                       preEngWord = loc[0];
                       obj.Add(
                           new IDictionaryTamil
                           {
                               engWord = loc[0],
                               taWordUnicode = loc[1],
                               taWordTace = loc[2]
                           }
                           );
                   }
                   else if (loc.Length == 2)
                   {
                       preEngWord = loc[0];
                       obj.Add(
                               new IDictionaryTamil
                               {
                                   engWord = loc[0],
                                   taWordUnicode = loc[1],
                                   taWordTace = EncodeUTF8ToTace(loc[1])
                               }
                               );
                   }
                   else if (loc.Length == 1)
                   {
                       obj.Add(
                               new IDictionaryTamil
                               {
                                   engWord = preEngWord,
                                   taWordUnicode = loc[0],
                                   taWordTace = EncodeUTF8ToTace(loc[0])
                               }
                               );
                   }

               }
                );

            return obj;
        }

        public string getMissedTransliteration()
        {
            return "";
        }

        public void saveUserDictionary(string englishWord,
            string givenText)
        {
            //if (taEncoding == TaEncodingE.TACE)
            //{
            //    givenText = getTACEText(englishWord,TaEncodingE.TACE) 
            //        + " " + givenText;
            //}
            //else
            //{
            //    givenText = givenText + " " + getTACEText(englishWord, TaEncodingE.UTF);
            //}
            File.AppendAllText("Assets/data/user_dictionary.txt",
                EncodeToUTF8("Assets/data/tace.map", englishWord, givenText)
                .Trim()
                + Environment.NewLine
               );
        }

        private string getTACEText(string englishWord, string givenText)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            sb.AppendLine(englishWord);

            if (taEncoding == TaEncodingE.TACE)
            {
                foreach (ITRANS_ta_Schema item in lstiTRANSMapChars)
                {
                    sb = sb.Replace(item.itransEngChar, item.TaCharUtf8);
                }

                sb1.AppendLine(englishWord + " " + sb.ToString() + " " + givenText);
            }
            else
            {
                foreach (ITRANS_ta_Schema item in lstiTRANSMapChars)
                {
                    sb = sb.Replace(item.itransEngChar, item.TaCharUtf8);
                }

                sb1.AppendLine(englishWord + " " + sb.ToString() + " " + givenText);
            }

            //string utfWord = EncodeToUTF8("", givenText);

            return sb1.ToString();
        }

        public string EncodeUTF8ToTace(
            string inputContent)
        {
            string fileNameFontMap = "Assets/data/tace.map";// +fileNameFontMap;
            string[] fontToUTF8Map
                = File.ReadAllLines(fileNameFontMap,
                Encoding.UTF8);

            foreach (string strLine in fontToUTF8Map)
            {
                string[] mapstr = strLine.Split(' ');
                inputContent = inputContent.Replace(mapstr[1], mapstr[0]);
            }

            return inputContent;
        }

        public string EncodeToUTF8(string fileNameFontMap,
            string englishWord,
            string inputContent)
        {
            //fileNameFontMap = "Assets/data/tace.map";// +fileNameFontMap;
            string[] fontToUTF8Map = File.ReadAllLines(fileNameFontMap,
                Encoding.UTF8);
            inputContent = inputContent.Trim();
            string output = englishWord;
            string taceWord = "";
            string utfWord = "";
            if (taEncoding == TaEncodingE.TACE)
            {
                taceWord = inputContent;
            }
            else
            {
                utfWord = inputContent;
            }
            //List<FontMapChars> lstFontMapChars = new List<FontMapChars>();

            foreach (string strLine in fontToUTF8Map)
            {
                string[] mapstr = strLine.Split(' ');
                //lstFontMapChars.Add(new FontMapChars() { TaChar = mapstr[0], TaCharUtf8 = mapstr[1] });
                if (taEncoding == TaEncodingE.TACE)
                {
                    inputContent = inputContent.Replace(mapstr[0], mapstr[1]);
                }
                else
                {
                    inputContent = inputContent.Replace(mapstr[1], mapstr[0]);
                }
            }

            if (taEncoding == TaEncodingE.TACE)
            {
                //taceWord = inputContent;
                utfWord = inputContent;
            }
            else
            {
                taceWord = inputContent;
            }

            return (

                output.Replace(" ", "")
                + " "
                + utfWord.Replace(" ", "")
                + " "
                + taceWord.Replace(" ", "")

                )
                .Replace("^", "")
                .Replace("~", "").ToLower() + " ";
        }

    }
}
