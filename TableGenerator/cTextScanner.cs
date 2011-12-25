using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TableGenerator
{
    class cTextScanner: cScanner
    {
        TextReader cf_reader;
        eState cf_state;
        string cf_name;
        int cf_buf = -1;

        public cTextScanner(string a_filename)
        {
            cf_reader = new StreamReader(a_filename, Encoding.Unicode);
            cf_state = eState.Initial;
        }

        ~cTextScanner()
        {
            cf_reader.Close();
        }

        public override cToken cm_GetNextToken()
        {
            cToken _retToken = null;

            bool _valid = true;
            while (_valid && _retToken == null)
            {
                int _ch;
                if (cf_buf != -1)
                    _ch = cf_buf;
                else
                    _ch = cf_reader.Read();

                cf_buf = -1;

                while (_ch.Equals('\r'))
                    _ch = cf_reader.Read();
                switch (cf_state)
                {
                    case eState.Initial:
                        if (_ch.Equals('-'))
                        {
                            cf_state = eState.A;
                        }
                        else if (_ch.Equals('\n'))
                        {
                            cf_state = eState.N;
                        }
                        else if (_ch.Equals(cLexem.cc_Epsilon))
                        {
                            _retToken = new cToken(eTokenType.пустая_лексема, cLexem.cc_EpsilonLexem);
                        }
                        else if (_ch.Equals('{'))
                        {
                            cf_state = eState.B;
                        }
                        else if (cm_isLetter(_ch))
                        {
                            cf_state = eState.L;
                            cf_name = ((char)_ch).ToString();
                        }
                        else if (cm_isSeparator(_ch))
                        {
                            // разделители пропускаются
                        }
                        else if (_ch == -1)
                        {
                            _retToken = new cToken(eTokenType.Null, null);
                        }
                        else
                        {
                            _valid = false;
                        }
                        break;
                    case eState.A:
                        if (_ch.Equals('>'))
                        {
                            cf_state = eState.Initial;
                            _retToken = new cToken(eTokenType.стрелка, null);
                        }
                        else
                        {
                            _valid = false;
                        }
                        break;
                    case eState.B:
                        if (cm_isLetter(_ch))
                        {
                            cf_state = eState.E;
                            cf_name = ((char)_ch).ToString();
                        }
                        else
                        {
                            _valid = false;
                        }
                        break;
                    case eState.E:
                        if (cm_isLetter(_ch))
                        {
                            cf_name += ((char)_ch).ToString();
                        }
                        else if (_ch.Equals('}'))
                        {
                            cf_state = eState.Initial;
                            _retToken = new cToken(eTokenType.действие, cm_getAction());
                        }
                        else
                        {
                            _valid = false;
                        }
                        break;
                    case eState.L:
                        if (cm_isLetter(_ch))
                        {
                            cf_name += ((char)_ch).ToString();
                        }
                        else
                        {
                            cf_state = eState.Initial;
                            cf_buf = _ch;
                            _retToken = new cToken(eTokenType.лексема, cm_getLexem());
                        }
                        break;
                    case eState.N:
                        if (_ch.Equals('\n'))
                        {
                            
                        }
                        else
                        {
                            cf_state = eState.Initial;
                            cf_buf = _ch;
                            _retToken = new cToken(eTokenType.перевод_строки, null);
                        }
                        break;
                    default:
                        throw new Exception("Неизвестное состояние автомата.");
                }

                if (cf_buf == -1)
                    cf_buf = cf_reader.Read();
            }

            return _retToken;
        }

        #region Члены IDisposable

        public override void Dispose()
        {
            cf_reader.Close();
        }

        #endregion

        private bool cm_isSeparator(int _ch)
        {
            return _ch.Equals(' ') || _ch.Equals('\t')/* || _ch.Equals('\r')*/;
        }

        private bool cm_isLetter(int _ch)
        {
            return char.IsLetterOrDigit((char)_ch) || char.IsPunctuation((char)_ch) && _ch != '{' && _ch != '}'
                 || _ch.Equals('*') || _ch.Equals('/') || _ch.Equals('+') || _ch.Equals('-')
                 || _ch.Equals('=') || _ch.Equals('<') || _ch.Equals('>');
        }

        private object cm_getAction()
        {
            cLexem _retLex = cLexem.cm_GetLexem(cf_name);
            _retLex.cp_Type = eLexType.Action;
            return _retLex;
        }

        private object cm_getLexem()
        {
            return cLexem.cm_GetLexem(cf_name);
        }

        private enum eState
        {
            Initial, A, B, E, L, N
        }
    }
}
