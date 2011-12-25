using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;

namespace TableGenerator
{
    public class cLLTableGenerator
    {
        cLexem cf_root = null;
        bool cf_valid = false;
        Dictionary<cLexem, cSet<cLexem>> cf_follow = null;

        public cLexem cp_Root
        {
            get { return cf_root; }
        }

        public ICollection<cLexem> cp_Lexems
        {
            get { return cLexem.cf_LexemDic.Values; }
        }

        public void cm_Init(cParser a_parser)
        {
            cf_valid = false;
            cf_root = a_parser.cp_Root;

            // Заполнение множества FOLLOW
            cf_follow = new Dictionary<cLexem,cSet<cLexem>>();
            foreach (cLexem _nonTerminal in cp_Lexems)
            {
                if (_nonTerminal.cp_Type == eLexType.NonTerminal)
                {
                    cf_follow[_nonTerminal] = new cSet<cLexem>();
                }
            }

            // 2 
            cSet<cLexem> _first = new cSet<cLexem>();
            foreach (cLexem _nonTerminal in cp_Lexems)
            {
                if (_nonTerminal.cp_Type == eLexType.NonTerminal)
                {
                    foreach (List<cLexem> _lstLex in _nonTerminal.cp_ListProducts.Values)
                    {
                        int _count = _lstLex.Count;
                        _first.Clear();
                        for (int i = _lstLex.Count - 1; i >= 0; i--)
                        {
                            cLexem _lex = _lstLex[i];
                            switch (_lex.cp_Type)
                            {
                                case eLexType.NonTerminal:
                                    cf_follow[_lex].AddRange(_first);
                                    if (!_lex.cm_IsLeadingLexem(cLexem.cc_EpsilonLexem))
                                    {
                                        _first.Clear();
                                    }
                                    _first.AddRange(_lex.cp_LeadLexems);
                                    break;
                                case eLexType.Terminal:
                                    _first.Clear();
                                    _first.Add(_lex);
                                    break;
                                case eLexType.Epsilon:
                                    _first.Clear();
                                    break;
                            }
                        }
                    }
                }
            }

            // 3
            bool _added = true;
            while (_added)
            {
                _added = false;
                foreach (cLexem _nonTerminal in cp_Lexems)
                {
                    if (_nonTerminal.cp_Type == eLexType.NonTerminal)
                    {
                        foreach (List<cLexem> _lstLex in _nonTerminal.cp_ListProducts.Values)
                        {
                            int _count = _lstLex.Count;
                            _first.Clear();
                            bool _break = false;
                            for (int i = _lstLex.Count - 1; i >= 0; i--)
                            {
                                cLexem _lex = _lstLex[i];
                                switch (_lex.cp_Type)
                                {
                                    case eLexType.NonTerminal:
                                        cf_follow[_lex].AddRange(_first);
                                        if (!_lex.cm_IsLeadingLexem(cLexem.cc_EpsilonLexem))
                                        {
                                            _break = true;
                                        }
                                        //else
                                            _added = cf_follow[_lex].AddRange(cf_follow[_nonTerminal]) | _added;
                                        break;
                                    case eLexType.Action:
                                        break;
                                    default:
                                        _break = true;
                                        break;
                                }
                                if (_break) break;
                            }
                        }
                    }
                }
            }
            cf_valid = true;
        }

        public DataTable cm_GenerateTable()
        {
            DataTable _retDT = null;
            if (cf_valid)
            {
                List<cProductInfo> _listProducts = new List<cProductInfo>();
                int _counter = 1;
                _listProducts.AddRange(cProductInfo.cm_getProductsByRoot(cf_root, ref  _counter));
                foreach (cLexem _lex in cLexem.cf_LexemDic.Values)
                    if (_lex.cp_Type == eLexType.NonTerminal && _lex != cf_root)
                        _listProducts.AddRange(cProductInfo.cm_getProductsByRoot(_lex, ref _counter));

                _retDT = cm_fillDataTable(_listProducts.ToArray());
            }
            return _retDT;
        }

        DataTable cm_fillDataTable(cProductInfo[] a_data)
        {
            cm_checkProducts(a_data);
            Dictionary<cLexem, int> _dicJump = new Dictionary<cLexem, int>();
            for (int k = 0; k < a_data.Length; k++)
            {
                if (!_dicJump.ContainsKey(a_data[k].cp_Root.Key))
                    _dicJump.Add(a_data[k].cp_Root.Key, a_data[k].cp_Root.Value);
            }

            DataTable _retDT = new DataTable("table");
            _retDT.Columns.Add("i", typeof(int));
            _retDT.Columns.Add("terminals", typeof(string[]) /*typeof(object)*/);
            _retDT.Columns.Add("jump", typeof(int));
            _retDT.Columns.Add("accept", typeof(bool));
            _retDT.Columns.Add("stack", typeof(bool));
            _retDT.Columns.Add("return", typeof(bool));
            _retDT.Columns.Add("error", typeof(bool));
            _retDT.Columns.Add("action", typeof(string));

            int _count = a_data.Length;
            bool _warning = false;
            for (int k = 0; k < _count; k++)
            {
                cProductInfo _info = a_data[k];

                // Проверка LL(1)
                if (k == 0 || a_data[k - 1].cp_Root.Key != _info.cp_Root.Key)
                {
                    _warning = _info.cp_Root.Key.cm_HasEpsilonProduct();
                }
                foreach (cLexem _lex in _info.cp_ArrDirLexems)
                {
                    if (cf_follow[_info.cp_Root.Key].ContainsKey(_lex))
                    {
                        if (_warning)
                            throw new Exception("Невозможно однозначно определить переход из-за " + cLexem.cc_Epsilon + "-порождающей продукции в " + _info.cp_Root.Key + ".");
                         if (_lex.cp_Type != eLexType.Epsilon)
                            _warning = true;
                    }
                    else if (_lex.cp_Type == eLexType.Epsilon)
                    {
                        if (_warning)
                            throw new Exception("Невозможно однозначно определить переход из-за " + cLexem.cc_Epsilon + "-порождающей продукции в " + _info.cp_Root.Key + ".");
                        _warning = true;
                    }
                }

                int _i = _info.cp_Root.Value;
                cSet<string> _terminals = new cSet<string>();
                foreach (cLexem _lex in _info.cp_ArrDirLexems)
                    if (_lex.cp_Type == eLexType.Terminal)
                        _terminals.Add(_lex.cf_Name);
                int _jump = _info.cp_ArrLexems[0].Value;
                bool _accept = false;
                bool _stack = false;
                bool _return = false;
                bool _error = false;
                if (k == _count - 1 || a_data[k + 1].cp_Root.Key != _info.cp_Root.Key)
                    _error = true;
                string _action = "";
                //_terminals.Sort();
                _retDT.Rows.Add(_i, _terminals.ToArray(), _jump, _accept, _stack, _return, _error, _action);

                KeyValuePair<cLexem, int>[] _arrLexems = _info.cp_ArrLexems;
                int _lexCount = _arrLexems.Length;
                for (int j = 0; j < _lexCount; j++)
                {
                    cLexem _lexem = _arrLexems[j].Key;
                    _i = _arrLexems[j].Value;
                    
                    // ERROR
                    _error = true;

                    // TERMINALS
                    switch (_lexem.cp_Type)
                    {
                        case eLexType.Epsilon:
                            _terminals = new cSet<string>();
                            //foreach (cLexem _lex in _info.cp_ArrDirLexems)
                            //    _terminals.Add(_lex.cf_Name);
                            break;
                        case eLexType.Terminal:
                            _terminals = new cSet<string>();
                            _terminals.Add(_lexem.cf_Name);
                            break;
                        case eLexType.NonTerminal:
                            _terminals = new cSet<string>();
                            foreach (cLexem _lex in _lexem.cp_LeadLexems)
                                if (_lex.cp_Type == eLexType.Epsilon)
                                {
                                    if (j == _lexCount - 1)
                                    {
                                        _retDT.Rows.Add(_i + 1, new string[0], 0, false, false, true, true, "");
                                    }
                                    _error = false;
                                }
                                else
                                    _terminals.Add(_lex.cf_Name);
                            break;
                        case eLexType.Action:
                            _terminals = new cSet<string>();
                            break;
                    }

                    // JUMP
                    if (_lexem.cp_Type == eLexType.NonTerminal)
                    {
                        _jump = _dicJump[_lexem];
                    }
                    else
                    {
                        // Не последний терминал или действие
                        //if (j < _lexCount - 1)
                            _jump = _i + 1;
                        // Последний терминал или действие
                        //else
                        //    _jump = 0;
                    }

                    // ACCEPT
                    _accept = _lexem.cp_Type == eLexType.Terminal;

                    // STACK
                    // Не последний нетерминал правой части
                    _stack = (_lexem.cp_Type == eLexType.NonTerminal && j < _lexCount - 1);

                    // RETURN
                    // Крайний правый терминал или действие
                    _return = ((_lexem.cp_Type != eLexType.NonTerminal) && j == _lexCount - 1);

                    // ACTION
                    _action = "";
                    if (_lexem.cp_Type == eLexType.Action)
                        _action = _lexem.cf_Name;

                    //_terminals.Sort();
                    _retDT.Rows.Add(_i, _terminals.ToArray(), _jump, _accept, _stack, _return, _error, _action);
                }
            } 

            // Сортировка строк
            DataTable _oldDT = _retDT;
            _retDT = _oldDT.Clone();

            foreach (DataRow _dr in _oldDT.Select(null, "i"))
                _retDT.Rows.Add(_dr.ItemArray);

            return _retDT;
        }

        void cm_checkProducts(cProductInfo[] a_data)
        {
            int _count = a_data.Length;
            Dictionary<cLexem, cLexem> _dic = new Dictionary<cLexem, cLexem>();
            for (int k = 0; k < _count; k++)
            {
                cProductInfo _info = a_data[k];
                KeyValuePair<cLexem, int>[] _arrLexems = _info.cp_ArrLexems;
                int _lexCount = _arrLexems.Length;
                bool _clearDic = true;
                for (int j = 0; j < _lexCount; j++)
                {
                    if (_clearDic)
                        _dic.Clear();
                    _clearDic = true;
                    cLexem _lexem = _arrLexems[j].Key;

                    switch (_lexem.cp_Type)
                    {
                        case eLexType.Terminal:
                            if (_dic.ContainsKey(_lexem))
                                throw new cNotLL1Exception(cLexem.cc_EpsilonLexem, cLexem.cc_EpsilonLexem, "Невозможно однозначно определить продукцию для символа " + _lexem.ToString() + " правой части " + _info.cp_Root.Key.ToString() + " из-за " + cLexem.cc_Epsilon + "-порождающих нетерминалов.");
                            _dic.Add(_lexem, _lexem);
                            break;
                        case eLexType.NonTerminal:
                            foreach (cLexem _lex in _lexem.cp_LeadLexems)
                            {
                                if (_lex.cp_Type == eLexType.Terminal)
                                {
                                    if (_dic.ContainsKey(_lex))
                                        throw new cNotLL1Exception(cLexem.cc_EpsilonLexem, cLexem.cc_EpsilonLexem, "Невозможно однозначно определить продукцию для символа " + _lex.ToString() + " правой части " + _info.cp_Root.Key.ToString() + " из-за " + cLexem.cc_Epsilon + "-порождающих нетерминалов.");
                                    _dic.Add(_lex, _lex);                                    
                                }
                                else if (_lex.cp_Type == eLexType.Epsilon)
                                {
                                    _clearDic = false;
                                }

                            }
                            break;
                        case eLexType.Epsilon:
                            _clearDic = true;
                            break;
                    }
                }
            } 
        }
    }
}
