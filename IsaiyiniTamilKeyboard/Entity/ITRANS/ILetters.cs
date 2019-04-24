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

namespace CaretPosition.Entity.ITRANS
{
    public class IDictionaryTamil
    {
        public string taWordUnicode { get; set; }
        public string engWord { get; set; }
        public string taWordTace { get; set; }
        public bool lastUsed { get; set; }
    }
}
