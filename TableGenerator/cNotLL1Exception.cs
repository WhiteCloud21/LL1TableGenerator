using System;
using System.Collections.Generic;
using System.Text;

namespace TableGenerator
{
    class cNotLL1Exception: Exception
    {
        public readonly cLexem cf_Lexem = null;
        public readonly cLexem cf_LeftLexem = null;

        public cNotLL1Exception(cLexem a_lexem, cLexem a_leftLexem, string a_customInfo) :
            base("Грамматика не является LL(1).\n" + a_customInfo)
        {
            cf_Lexem = a_lexem;
            cf_LeftLexem = a_leftLexem;
        }
    }
}
