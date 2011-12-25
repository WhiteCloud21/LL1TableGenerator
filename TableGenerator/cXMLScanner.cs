using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TableGenerator
{
    public class cXMLScanner: cScanner
    {
        private const string cc_product = "production";
        private const string cc_leftNonTerminal = "left";
        private const string cc_lexem = "lexem";
        private const string cc_epsilon = "epsilon";
        private const string cc_action = "action";

        private readonly XmlReader cf_reader;
        private readonly Stack<cToken> cf_buffer = new Stack<cToken>();

        public cXMLScanner(string a_filename)
        {
            cf_reader = new XmlTextReader(a_filename);
            cf_reader.MoveToContent();
            cf_reader.ReadStartElement();
        }

        ~cXMLScanner()
        {
            cf_reader.Close();
        }

        public override cToken cm_GetNextToken()
        {
            cToken _retToken;
            if (cf_buffer.Count > 0)
            {
                _retToken = cf_buffer.Pop();
            }
            else
            {
                _retToken = cm_readToken();
            }
            return _retToken;
        }

        #region Члены IDisposable

        public override void Dispose()
        {
            cf_reader.Close();
        }

        #endregion

        private cToken cm_readToken()
        {
            cToken _retToken = new cToken(eTokenType.Null, null);
            cf_reader.MoveToContent();
            if (cf_reader.IsStartElement(cc_product))
            {
                cf_reader.ReadStartElement(cc_product);
                cf_reader.MoveToContent();
                cf_buffer.Push(new cToken(eTokenType.стрелка, null));
                cLexem _lex = cLexem.cm_GetLexem(cf_reader.Value.Trim());

                _retToken = new cToken(eTokenType.лексема, _lex);
                cf_reader.Skip();
            }
            else if (cf_reader.IsStartElement(cc_epsilon))
            {
                cf_reader.Skip();
                _retToken = new cToken(eTokenType.пустая_лексема, cLexem.cc_EpsilonLexem);
            }
            else if (cf_reader.IsStartElement(cc_lexem))
            {
                XmlReader _subTree = cf_reader.ReadSubtree();
                _subTree.MoveToContent();
                _subTree.ReadStartElement(cc_lexem);
                _subTree.MoveToContent();
                _retToken = new cToken(eTokenType.лексема, cLexem.cm_GetLexem(_subTree.Value));
                _subTree.Close();
                cf_reader.Skip();
            }
            else if (cf_reader.IsStartElement(cc_action))
            {
                XmlReader _subTree = cf_reader.ReadSubtree();
                _subTree.MoveToContent();
                _subTree.ReadStartElement(cc_action);
                _subTree.MoveToContent();
                cLexem _lex = cLexem.cm_GetLexem(cf_reader.Value);
                _lex.cp_Type = eLexType.Action;
                _retToken = new cToken(eTokenType.действие, _lex);
                _subTree.Close();
                cf_reader.Skip();
            }
            else if (cf_reader.NodeType == XmlNodeType.EndElement && cf_reader.Name == cc_product)
            {
                _retToken = new cToken(eTokenType.перевод_строки, null);
                cf_reader.ReadEndElement();
            }
            return _retToken;
        }
    }
}
