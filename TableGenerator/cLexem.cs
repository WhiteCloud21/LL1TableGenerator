using System;
using System.Collections.Generic;
using System.Text;

namespace TableGenerator
{
    public class cLexem
    {
        public static readonly string cc_Epsilon = "ε";
        public static readonly cLexem cc_EpsilonLexem = new cLexem(cc_Epsilon, eLexType.Epsilon);

        public static readonly Dictionary<string, cLexem> cf_LexemDic = new Dictionary<string, cLexem>();

        public readonly string cf_Name;
        eLexType cf_type;

        Dictionary<cLexem, List<cLexem>> cf_listProducts = new Dictionary<cLexem, List<cLexem>>();
        cSet<cLexem> cf_leadLexems = null;

        public cLexem(string a_name, eLexType a_type)
        {
            cf_Name = a_name;
            cf_type = a_type;
            if (a_name == cc_Epsilon)
                cf_type = eLexType.Epsilon;
        }

        public eLexType cp_Type
        {
            get { return cf_type; }
            set 
            {
                if (cf_type == eLexType.Terminal)
                    cf_type = value;
            }
        }

        public Dictionary<cLexem, List<cLexem>> cp_ListProducts
        { 
            get { return cf_listProducts; } 
        }

        public cSet<cLexem> cp_LeadLexems
        {
            get
            {
                if (cf_leadLexems == null)
                {
                    cf_leadLexems = new cSet<cLexem>();
                    try
                    {
                        cf_leadLexems.AddRange(this.cm_getLeadLexemsInternal(new List < cLexem >()));
                    }
                    catch (cNotLL1Exception _ex)
                    {
                        throw new cNotLL1Exception(_ex.cf_Lexem, this, "Несколько продукций для " + this + " имеют направляющий символ " + _ex.cf_Lexem);
                    }
                }
                return cf_leadLexems;
            }
        }

        public bool cm_IsLeadingLexem(cLexem a_lexem)
        {
            if (this.cp_LeadLexems != null)
            {
                foreach (cLexem _lex in this.cp_LeadLexems)
                    if (_lex == a_lexem)
                        return true;
            }
            return false;
        }

        public static cLexem cm_GetLexem(string a_name)
        {
            if (a_name == cc_Epsilon)
                return cc_EpsilonLexem;
            cLexem _retLex = null;
            if (cf_LexemDic.ContainsKey(a_name))
                _retLex = cf_LexemDic[a_name];
            else
            {
                _retLex = new cLexem(a_name, eLexType.Terminal);
                cf_LexemDic.Add(a_name, _retLex);
            }
            return _retLex;
        }

        public bool cm_AddChildLexem(cLexem a_key, cLexem a_nextLex)
        {
            if (a_key == null)
            {
                if (cf_listProducts.ContainsKey(a_nextLex))
                    return false;
                else
                    cf_listProducts.Add(a_nextLex, new List<cLexem>(new cLexem[] { a_nextLex }));
            }
            else
            {
                cf_listProducts[a_key].Add(a_nextLex);
            }
            return true;
        }

        public bool cm_HasEpsilonProduct()
        {
            return cf_listProducts.ContainsKey(cc_EpsilonLexem);
        }

        public cLexem[] cm_GetLeadLexems(cLexem a_productFirstLex)
        {
            List<cLexem> _watchedLexems = new List<cLexem>();
            _watchedLexems.Add(this);
            List<cLexem> _tempLst = new List<cLexem>();
            cm_fillNonTerminalLeadingLexList(_watchedLexems, cf_listProducts[a_productFirstLex], 0, _tempLst);
            List<cLexem> _retLst = new List<cLexem>();
            foreach (cLexem _lex in _tempLst)
                if (_lex.cp_Type == eLexType.Terminal || _lex.cp_Type == eLexType.Epsilon)
                    _retLst.Add(_lex);
            return _retLst.ToArray();
        }

        List<cLexem> cm_getLeadLexemsInternal(List<cLexem> a_watchedLexems)
        {
            List<cLexem> _retLst = new List<cLexem>();
            if (a_watchedLexems.Contains(this))
            {
                //if (cf_type == eLexType.Terminal || cf_type == eLexType.NonTerminal)
                    throw new cNotLL1Exception(this, null, String.Empty);
            }
            else
            {
                a_watchedLexems.Add(this);
                switch (cf_type)
                {
                    case eLexType.NonTerminal:
                        foreach (KeyValuePair<cLexem, List<cLexem>> _kvp in cf_listProducts)
                        {
                            if (_kvp.Key.cp_Type != eLexType.Epsilon)
                            {
                                cm_fillNonTerminalLeadingLexList(a_watchedLexems, _kvp.Value, 0, _retLst);
                            }
                            else
                                _retLst.Add(_kvp.Key);
                        }
                        if (cf_leadLexems == null)
                        {
                            cf_leadLexems = new cSet<cLexem>();
                            cf_leadLexems.AddRange(_retLst);
                        }
                        break;
                    case eLexType.Terminal:
                    case eLexType.Epsilon:
                        _retLst.Add(this);
                        break;
                }
            }
            return _retLst;
        }

        private void cm_fillNonTerminalLeadingLexList(List<cLexem> a_watchedLexems, List<cLexem> a_prodLst, int a_index, List<cLexem> a_retLst)
        {
            List<cLexem> _lstLex = a_prodLst[a_index].cm_getLeadLexemsInternal(a_watchedLexems);
            foreach (cLexem _arrLexItem in _lstLex)
            {
                if (_arrLexItem.cp_Type == eLexType.Epsilon)
                {
                    if (a_prodLst.Count > a_index + 1)
                        cm_fillNonTerminalLeadingLexList(a_watchedLexems, a_prodLst, a_index + 1, a_retLst);
                }
                //else
                //{
                    a_retLst.Add(_arrLexItem);
                //}
            }
        }

        public override string ToString()
        {
            return cf_Name;
        }
    }

    public enum eLexType
    {
        Terminal,
        NonTerminal,
        Epsilon,
        Action
    }
}
