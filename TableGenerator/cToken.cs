using System;
using System.Collections.Generic;
using System.Text;

namespace TableGenerator
{
    public class cToken
    {
        public readonly eTokenType cf_Type;
        public readonly object cf_Value;

        public cToken(eTokenType a_type, object a_value)
        {
            cf_Type = a_type;
            cf_Value = a_value;
        }

        public bool IsInTerminals(string[] a_terminals)
        {
            bool _retBool = true;
            foreach (string _str in a_terminals)
            {
                _retBool = this.Equals(_str) || _str == cLexem.cc_Epsilon;
                if (_retBool)
                    break;
            }
            return _retBool;
        }

        public override bool Equals(object obj)
        {
            if (obj is string)
            {
                return this.cf_Type.ToString() == obj as string;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public enum eTokenType
    {
        лексема,
        действие,
        пустая_лексема,
        стрелка,
        перевод_строки,
        Null
    }
}
