using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace TableGenerator
{
    static class cFileTableWriter
    {
        public static void cm_SaveXML(string a_filename, DataTable a_dataTable)
        {
            a_dataTable.WriteXml(a_filename);
        }
    }
}
