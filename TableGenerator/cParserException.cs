using System;
using System.Collections.Generic;
using System.Text;

namespace TableGenerator
{
    class cParserException: Exception
    {
        public readonly cToken[] cf_Tokens = null;
        public readonly bool cf_IsScannerEx = false;
        private readonly string[] cf_exceptedTerminals;

        public override string Message
        {
            get
            {
                string _retStr = "Ошибка синтаксического анализа. Ожидался один из символов: " + string.Join(", ", cf_exceptedTerminals) + ". Поступивший символ: ";
                if (!cf_IsScannerEx && cf_Tokens.Length > 0)
                {
                    _retStr += (cf_Tokens.Length > 0) ? cf_Tokens[cf_Tokens.Length - 1].cf_Type.ToString() : "Нет";
                }
                else if (cf_IsScannerEx)
                {
                    _retStr = "Ошибка лексического анализа.";
                }
                else
                {
                    _retStr = "Неизвестная ошибка";
                }
                return _retStr;
            }
        }

        public cParserException(List<cToken> a_tokens, string[] a_exceptedTerminals)
        {
            cf_exceptedTerminals = a_exceptedTerminals;
            cf_Tokens = a_tokens.ToArray();
            if (cf_Tokens.Length > 0 && cf_Tokens[cf_Tokens.Length - 1] == null)
            {
                cf_IsScannerEx = true;
                cf_Tokens[cf_Tokens.Length - 1] = new cToken(eTokenType.Null, null);
            }
        }
    }
}
