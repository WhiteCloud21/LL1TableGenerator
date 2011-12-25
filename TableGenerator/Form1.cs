using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace TableGenerator
{
    public partial class Form1 : Form
    {
        DataTable cf_tblResult = null;
        cParser cf_parser = null;
        cLLTableGenerator cf_generator = new cLLTableGenerator();

        public Form1()
        {
            InitializeComponent();
        }

        private void cm_showStatus(string a_text, bool a_showMessageBox)
        {
            f_tsslStatus.Text = a_text;
            if (a_showMessageBox)
                MessageBox.Show(a_text);
        }

        private void cm_showGram(cNotLL1Exception _ex)
        {
            f_rtbGram.Clear();
            Color _col = f_rtbGram.SelectionColor;
            foreach (cLexem _lexem in cf_generator.cp_Lexems)
            {
                foreach (List<cLexem> _lstRightLex in _lexem.cp_ListProducts.Values)
                {
                    cm_addLexemToRichText(_lexem, f_rtbGram);
                    f_rtbGram.AppendText("-> ");
                    bool _first = true;
                    foreach (cLexem _rightLex in _lstRightLex)
                    {
                        if (_ex != null && _first && _lexem == _ex.cf_LeftLexem)
                            if (_rightLex.cp_Type == eLexType.NonTerminal && _ex.cf_Lexem.cm_IsLeadingLexem(_rightLex)
                                || _rightLex == _ex.cf_Lexem)
                                f_rtbGram.SelectionColor = Color.Red;
                        if (_first && !_rightLex.cm_HasEpsilonProduct())
                            _first = false;
                        cm_addLexemToRichText(_rightLex, f_rtbGram);
                        f_rtbGram.SelectionColor = _col;
                    }
                    f_rtbGram.AppendText("\n");
                }
            }
        }

        private void cm_showGram(cParserException _ex)
        {
            f_rtbGram.Clear();
            foreach (cToken _token in _ex.cf_Tokens)
            {
                switch (_token.cf_Type)
                {
                    case eTokenType.перевод_строки:
                        f_rtbGram.AppendText(Environment.NewLine);
                        break;
                    case eTokenType.стрелка:
                        f_rtbGram.AppendText(" -> ");
                        break;
                    case eTokenType.Null:
                        f_rtbGram.AppendText(" -> ");
                        break;
                    default:
                        cm_addLexemToRichText(_token.cf_Value as cLexem, f_rtbGram);
                        break;
                }
            }
        }

        private void cm_addLexemToRichText(cLexem a_lexem, RichTextBox a_rtb)
        {
            if (a_lexem.cp_Type == eLexType.NonTerminal)
                a_rtb.AppendText("<");
            else if (a_lexem.cp_Type == eLexType.Action)
                a_rtb.AppendText("{");
            a_rtb.SelectionFont = new Font(a_rtb.SelectionFont, FontStyle.Bold);
            a_rtb.AppendText(a_lexem.ToString());
            a_rtb.SelectionFont = new Font(a_rtb.SelectionFont, FontStyle.Regular);
            if (a_lexem.cp_Type == eLexType.NonTerminal)
                a_rtb.AppendText(">");
            else if (a_lexem.cp_Type == eLexType.Action)
                a_rtb.AppendText("}");
            a_rtb.AppendText(" ");
        }

        private void f_btnBrowseFileGram_Click(object sender, EventArgs e)
        {
            OpenFileDialog _fd = new OpenFileDialog();
            _fd.AddExtension = true;
            _fd.CheckFileExists = true;
            _fd.CheckPathExists = true;
            _fd.DereferenceLinks = true;
            _fd.Filter = "Поддерживаемые файлы(*.xml, *.txt)|*.xml;*.txt|XML-файлы(*.xml)|*.xml|Текстовые файлы(*.txt)|*.txt";
            _fd.FilterIndex = 0;
            _fd.Multiselect = false;
            _fd.RestoreDirectory = false;
            _fd.Title = "Загрузить грамматику";
            _fd.ValidateNames = true;
            if (_fd.ShowDialog() == DialogResult.OK)
                f_txtFileGram.Text = _fd.FileName;
        }

        private void f_btnLoadGram_Click(object sender, EventArgs e)
        {
            if (File.Exists(f_txtFileGram.Text))
            {
                try
                {
                    cScanner _scanner = null;
                    if (f_txtFileGram.Text.EndsWith(".xml"))
                    {
                        _scanner = new cXMLScanner(f_txtFileGram.Text);
                    }
                    else if (f_txtFileGram.Text.EndsWith(".txt"))
                    {
                        _scanner = new cTextScanner(f_txtFileGram.Text);
                    }

                    if (_scanner != null)
                    {
                        f_gbStep3.Enabled = false;
                        f_btnViewTable.Enabled = false;
                        f_gbStep4.Enabled = false;
                        f_rtbGram.Clear(); 
                        
                        cf_parser = new cParser(_scanner, Application.StartupPath + @"\metagram.xml");
                        cf_parser.cm_Parse();
                        cf_generator.cm_Init(cf_parser);
                        _scanner.Dispose();

                        // Вывод текста грамматики
                        cm_showGram(null as cNotLL1Exception);
                        f_gbStep3.Enabled = true;
                        f_btnViewTable.Enabled = false;
                        f_gbStep4.Enabled = false;
                        cm_showStatus("Загрузка грамматики завершена.", false);
                    }
                    else
                    {
                        cm_showStatus("Неподдерживаемый формат файла.", true);
                    }
                }
                catch (cNotLL1Exception _ex)
                {
                    cm_showGram(_ex);
                    cm_showStatus(_ex.Message, true);
                }
                catch (cParserException _ex)
                {
                    cm_showGram(_ex);
                    cm_showStatus(_ex.Message, true);
                }
                catch (Exception _ex)
                {
                    cm_showStatus(_ex.Message, true);
                }
            }
            else
            {
                cm_showStatus("Выбранного файла не существует.", true);
            }
        }

        private void f_btnGenerateTable_Click(object sender, EventArgs e)
        {
            try
            {
                cf_tblResult = cf_generator.cm_GenerateTable();
                f_btnViewTable.Enabled = true;
                f_gbStep4.Enabled = true;
                cm_showStatus("Генерация таблицы разбора завершена.", false);
            }
            catch (cNotLL1Exception _ex)
            {
                // Вывод текста грамматики, который загружен целиком к данному моменту
                cm_showGram(_ex);
                cm_showStatus(_ex.Message, true);
            }
            catch (Exception _ex)
            {
                cm_showStatus(_ex.Message, true);
            }
        }

        private void f_btnViewTable_Click(object sender, EventArgs e)
        {
            (new fShowTable(cf_tblResult)).ShowDialog();
        }

        private void f_btnBrowseFileTable_Click(object sender, EventArgs e)
        {
            SaveFileDialog _fd = new SaveFileDialog();
            _fd.AddExtension = true;
            _fd.CheckFileExists = false;
            _fd.CheckPathExists = true;
            _fd.DereferenceLinks = true;
            _fd.Filter = "XML-файлы(*.xml)|*.xml";
            _fd.FilterIndex = 0;
            _fd.OverwritePrompt = true;
            _fd.RestoreDirectory = false;
            _fd.Title = "Сохранить таблицу разбора как";
            _fd.ValidateNames = true;
            if (_fd.ShowDialog() == DialogResult.OK)
                f_txtFileTable.Text = _fd.FileName;
        }

        private void f_btnSaveTable_Click(object sender, EventArgs e)
        {
            try
            {

                if (f_txtFileTable.Text.EndsWith(".xml"))
                {
                    cFileTableWriter.cm_SaveXML(f_txtFileTable.Text, cf_tblResult);
                    cm_showStatus("Сохранение успешно завершено.", true);
                }
                else
                {
                    cm_showStatus("Неподдерживаемый формат файла.", true);
                }
            }
            catch (Exception _ex)
            {
                cm_showStatus("Ошибка при сохранении: " + _ex.Message, true);
            }
        }
    }
}
