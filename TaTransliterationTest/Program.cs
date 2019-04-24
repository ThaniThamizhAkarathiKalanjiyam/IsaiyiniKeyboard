using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaretPosition;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace TaTransliterationTest
{
    class Program
    {


        static void Main(string[] args)
        {
            //NewMethod();

            //NewMethod1();

            //mergeTransAndWord();
            //mergeTransAndWord("test.csv",
            //    "tamil_words_list_utf8.csv",
            //    "lookups_dictionary.v2.txt");

            //mergeTransAndWord("lookups_dictionary.v2.txt",
            //    "tamil_words_list_tace16.v2.txt",
            //    "lookups_dictionary.v3.txt");

            ReadBinaryFile();

            //foreach (string ste in File.ReadAllLines(@"D:\GitRepo\IsaiyiniKeyboard\branches\src\IsaiyiniTamilKeyboard\Assets\data\lookups_dictionary.bin",
            //    Encoding.UTF8))
            //{
            //    //sb1.AppendLine(ste);
            //    sw.WriteLine(ste);
            //}


            //sb.Append(File.ReadAllLines(@"D:\GitRepo\IsaiyiniKeyboard\branches\src\IsaiyiniTamilKeyboard\Assets\data\lookups_dictionary.bin",
            //    Encoding.UTF8));
            //File.WriteAllText("test2",
            //    sb1.ToString());
            //objTamilWordNLP.readAllBinaryFile("data/lookups_dictionary.bin",
            //    "OutputFIle.txt");
        }

        private static void mergeTransAndWord(
            string file1,
            string file2,
            string mergedFile)
        {
            
            string[] tamilUtf8Words = File.ReadAllLines(file1, Encoding.UTF8);
            string[] tamilTransEngWords = File.ReadAllLines(file2, Encoding.UTF8);

            StreamWriter sw = new StreamWriter(mergedFile
                ,true,
                Encoding.UTF8);

            for (int i = 0; i < tamilUtf8Words.Length; i++)
            {
                sw.WriteLine(tamilUtf8Words[i] + tamilTransEngWords[i]);
            }

            sw.Close();
        }

        private static void NewMethod1()
        {
            FileStream fs = new FileStream("lookups_dictionary.bin",
                FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            string str = "";
            str = (string)bf.Deserialize(fs);
            fs.Close();

            File.WriteAllText("fgfdgfd", str);
        }

        private static void NewMethod()
        {
            TamilWordNLP objTamilWordNLP = new TamilWordNLP();



            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();

            sb.Append(File.ReadAllText(@"C:\Documents and Settings\muthukumaran\My Documents\Downloads\tamil_words\utf8\tamil_words_list_utf8.csv",
                Encoding.UTF8));

            File.WriteAllText("test.csv",
                objTamilWordNLP.getTamil2EnglishTransliteration(sb.ToString()));

        }

        private static void ReadBinaryFile()
        {
            //FileStream fs = new FileStream("segment_mappings.bin",FileMode.Open);
            //BinaryFormatter bf = new BinaryFormatter();
            //string str = "";
            //str = (string)bf.Deserialize(fs);
            //fs.Close();

            string strlines = File.ReadAllText("segment_mappings.bin",Encoding.UTF8);
            //strlines = Regex.Replace(strlines, @"\p{C}+", string.Empty);
            //string output = new string(strlines.Where(c => !char.IsControl(c)).ToArray());
            strlines = Regex.Replace(strlines, @"[^\u0000-\u007F]+", Environment.NewLine);
            File.WriteAllText("segment_mappings.bin.v2.bak", strlines, Encoding.UTF8);
            int i = strlines.Length;
        }
    }
}
