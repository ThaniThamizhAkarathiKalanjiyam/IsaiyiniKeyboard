using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaretPosition
{
    public class ITRANS_ta_Schema
    {
        public ITRANS_ta_Schema()
        {
            //Do nothing
        }
        public ITRANS_ta_Schema(string _itransEngChar,
            string _TaCharUtf8,
            string _TaCharTACE)
        {
            itransEngChar = _itransEngChar;
            TaCharUtf8 = _TaCharUtf8;
            TaCharTACE = _TaCharTACE;

        }

        public string itransEngChar { get; set; }
        public string TaCharUtf8 { get; set; }
        public string TaCharTACE { get; set; }
    }
}
