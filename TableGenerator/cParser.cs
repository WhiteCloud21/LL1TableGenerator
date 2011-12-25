using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TableGenerator
{
    public class cParser
    {
        private cScanner cf_scanner;
        private DataTable cf_dataTable;

        private List<cToken> cf_lisTokens = new List<cToken>();

        private cLexem cf_root = null;
        private cLexem cf_leftLex = null;
        private cLexem cf_firstLex = null;


        public cParser(cScanner a_scanner, string a_filename)
        {
            cf_scanner = a_scanner;
            cf_dataTable = new DataTable("table");
            cf_dataTable.Columns.Add("i", typeof(int));
            cf_dataTable.Columns.Add("terminals", typeof(string[]));
            cf_dataTable.Columns.Add("jump", typeof(int));
            cf_dataTable.Columns.Add("accept", typeof(bool));
            cf_dataTable.Columns.Add("stack", typeof(bool));
            cf_dataTable.Columns.Add("return", typeof(bool));
            cf_dataTable.Columns.Add("error", typeof(bool));
            cf_dataTable.Columns.Add("action", typeof(string));
            cf_dataTable.ReadXml(a_filename);
        }

        public cLexem cp_Root
        {
            get { return cf_root; }
        }

        public void cm_Parse()
        {
            cLexem.cf_LexemDic.Clear();

            int i = 1;
            Stack<int> _s = new Stack<int>();
            _s.Push(0);
            bool _la = true;
            cToken _token = cf_scanner.cm_GetNextToken();
            cf_lisTokens.Add(_token);
            while (_token != null && i != 0)
            {
                DataRow _row = cf_dataTable.Rows[i - 1];
                if (_token.IsInTerminals(_row["terminals"] as string[]))
                {
                    _la = (bool)_row["accept"];

                    // действие
                    if (!string.IsNullOrEmpty(_row["action"] as string))
                    {
                        cm_doAction(_row["action"] as string);
                    }

                    if ((bool)_row["return"])
                    {
                        i = _s.Pop();
                    }
                    else
                    {
                        if ((bool)_row["stack"])
                            _s.Push(i + 1);
                        i = (int)_row["jump"];
                    }
                }
                else
                {
                    if ((bool)_row["error"])
                        throw new cParserException(cf_lisTokens, _row["terminals"] as string[]);
                    else
                    {
                        i = i + 1;
                        _la = false;
                    }
                }
                if (_la)
                {
                    if (_token.cf_Type == eTokenType.Null)
                        throw new cParserException(cf_lisTokens, _row["terminals"] as string[]);
                    _token = cf_scanner.cm_GetNextToken();
                    cf_lisTokens.Add(_token);
                }
            }

            if (i != 0)
            {
                throw new cParserException(cf_lisTokens, new string[] { });
            }
            if (_token.cf_Type != eTokenType.Null)
            {
                throw new cParserException(cf_lisTokens, new string[] { });
            }
        }

        private void cm_doAction(string a_action)
        {
            switch (a_action)
            {
                case "A1":
                    cm_doA1();
                    break;
                case "A2":
                    cm_doA2();
                    break;
                case "A3":
                    cm_doA3();
                    break;
                case "A4":
                    cm_doA4();
                    break;
                case "A5":
                    cm_doA5();
                    break;
                default:
                    throw new Exception("Неизвестное действие в таблице синтаксического анализа.");
            }
        }

        private void cm_doA1()
        {
            cf_leftLex = cf_lisTokens[cf_lisTokens.Count - 3].cf_Value as cLexem;
            cf_leftLex.cp_Type = eLexType.NonTerminal;
            if (cf_root == null)
                cf_root = cf_leftLex;
        }

        private void cm_doA2()
        {
            cLexem _lexem = cf_lisTokens[cf_lisTokens.Count - 2].cf_Value as cLexem;
            if (!cf_leftLex.cm_AddChildLexem(null, _lexem as cLexem))
                throw new cNotLL1Exception(_lexem, cf_leftLex, "Несколько продукций для " + cf_leftLex + " имеют направляющий символ " + _lexem);
            cf_firstLex = _lexem;
        }

        private void cm_doA3()
        {
            if (!cf_leftLex.cm_AddChildLexem(null, cLexem.cc_EpsilonLexem))
                throw new cNotLL1Exception(cLexem.cc_EpsilonLexem, cf_leftLex, "Несколько продукций для " + cf_leftLex + " имеют направляющий символ " + cLexem.cc_Epsilon);
        }

        private void cm_doA4()
        {
            cLexem _lexem = cf_lisTokens[cf_lisTokens.Count - 2].cf_Value as cLexem;
            cf_leftLex.cm_AddChildLexem(cf_firstLex, _lexem as cLexem);
        }
        private void cm_doA5()
        {
            cLexem _lexem = cf_lisTokens[cf_lisTokens.Count - 2].cf_Value as cLexem;
            cf_leftLex.cm_AddChildLexem(cf_firstLex, _lexem as cLexem);
        }
    }
}
