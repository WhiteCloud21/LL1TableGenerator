using System;
using System.Collections.Generic;
using System.Text;

namespace TableGenerator
{
    class cProductInfo
    {
        KeyValuePair<cLexem, int> cf_root;

        List<KeyValuePair<cLexem, int>> cf_lstLexems;

        cLexem[] cf_arrDirLexems;

        private cProductInfo() { }

        public KeyValuePair<cLexem, int> cp_Root
        {
            get { return cf_root; }
        }

        public KeyValuePair<cLexem, int>[] cp_ArrLexems
        {
            get { return cf_lstLexems.ToArray(); }
        }

        public cLexem[] cp_ArrDirLexems
        {
            get { return cf_arrDirLexems; }
        }
        
        public static cProductInfo[] cm_getProductsByRoot(cLexem a_root, ref int a_Counter)
        {
            List<cProductInfo> _retLst = new List<cProductInfo>();
            int _counterRoots = a_Counter;
            a_Counter += a_root.cp_ListProducts.Count;
            
            if (a_root.cp_ListProducts.ContainsKey(cLexem.cc_EpsilonLexem))
                a_Counter--;

            foreach (KeyValuePair<cLexem, List<cLexem>> _kvp in a_root.cp_ListProducts)
            {
                // В список не добавляются пустые продукции
                if (_kvp.Key.cp_Type == eLexType.Epsilon)
                    continue;

                cProductInfo _newPro = new cProductInfo();

                _newPro.cf_root = new KeyValuePair<cLexem,int>(a_root, _counterRoots++);

                _newPro.cf_lstLexems = new List<KeyValuePair<cLexem, int>>();
                int i = _kvp.Value.Count;
                foreach (cLexem _lex in _kvp.Value)
                {
                    _newPro.cf_lstLexems.Add(new KeyValuePair<cLexem, int>(_lex, a_Counter++));
                    
                    // Для последнего нетерминала
                    if (--i == 0)
                    {
                        bool _flag = false;
                        if (_lex.cp_Type == eLexType.NonTerminal)
                        {
                            foreach (cLexem _leadLex in _lex.cp_LeadLexems)
                                if (_leadLex.cp_Type == eLexType.Epsilon)
                                {
                                    _flag = true;
                                    break;
                                }
                        }
                        if (_flag)
                        {
                            a_Counter++;
                        }
                    }
                }

                if (_kvp.Key.cp_Type == eLexType.NonTerminal)
                    _newPro.cf_arrDirLexems = _newPro.cf_root.Key.cm_GetLeadLexems(_kvp.Key);
                else
                    _newPro.cf_arrDirLexems = new cLexem[]{ _kvp.Key };

                _retLst.Add(_newPro);
            }
            return _retLst.ToArray();
        }

        public override string ToString()
        {
            if (cf_root.Key == null || cf_lstLexems == null || cf_arrDirLexems == null)
                return "NULL";
            else
            {
                string _retStr = cf_root.ToString() + " -> ";
                foreach (KeyValuePair<cLexem, int> _kvp in cf_lstLexems)
                    _retStr += _kvp.ToString() + " ";
                _retStr += "{";
                foreach (cLexem _lex in cf_arrDirLexems)
                    _retStr += _lex.ToString() + ", ";
                if (cf_arrDirLexems.Length > 0)
                    _retStr = _retStr.Remove(_retStr.Length - 2);
                _retStr += "}";
                return _retStr;
            }
        }
    }
}
