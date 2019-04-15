using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CaretPosition
{
    public partial class TamilWordNLP
    {
        List<FontMapChars> lstiTRANSMapChars = new List<FontMapChars>();

        TaEncoding encoding = TaEncoding.TACE;

        internal TaEncoding TaEncoding
        {
            get { return encoding; }
            set { encoding = value; }
        }

        public string[] getSuggestedWordslist(StringBuilder sb)
        {

            if (lstiTRANSMapChars.Count() == 0)
            {
                lstiTRANSMapChars = getItransMap();
            }
            List<string> sugg = new List<string>();
            if (sb.ToString().EndsWith(" ") == true)
            {
                sugg.Add(sb.ToString());

                foreach (char wordLetter in sb.ToString())
                {
                    FontMapChars objFontMapChars = lstiTRANSMapChars.FirstOrDefault(W => W.TaChar == wordLetter.ToString().Trim());

                    if (objFontMapChars != null)
                    {
                        sugg.Add(getCorrespondingEncodeVal(objFontMapChars));
                    }
                }
            }
            else
            {
                foreach (FontMapChars fmc in lstiTRANSMapChars)
                {
                    sb = sb.Replace(fmc.TaChar,
                            getCorrespondingEncodeVal(fmc));
                }
                sugg.Add(sb.ToString());
            }

            return sugg.ToArray();
        }

        private string getCorrespondingEncodeVal(FontMapChars fmc)
        {
            if (encoding == TaEncoding.TACE)
            {
                return fmc.TaCharTACE;
            }
            else
            {
                return fmc.TaCharUtf8;
            }
        }


        public List<FontMapChars> getItransMap()
        {
            string[] fontToUTF8Map = File.ReadAllLines("ITRANS.txt",
                  Encoding.UTF8);

            List<FontMapChars> lstFontMapChars = new List<FontMapChars>();


            foreach (string strLine in fontToUTF8Map)
            {
                string[] mapstr = strLine.Split(' ');

                if (mapstr[0].Contains(','))
                {
                    string[] subtachars = mapstr[0].Split(',');
                    foreach (string substr in subtachars)
                    {
                        lstFontMapChars.Add(new FontMapChars()
                        {
                            TaChar = substr,
                            TaCharUtf8 = mapstr[1],
                            TaCharTACE = mapstr[2]
                        });
                    }
                }
                else
                {
                    lstFontMapChars.Add(new FontMapChars()
                    {
                        TaChar = mapstr[0],
                        TaCharUtf8 = mapstr[1],
                        TaCharTACE = mapstr[2]
                    });
                }
            }
            return lstFontMapChars;
        }
    }
}
