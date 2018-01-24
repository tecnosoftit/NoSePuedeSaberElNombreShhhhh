using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using Ssoft.Utils;

namespace Ssoft.DataNet
{
    public class clsDataNet
    {
        private const string TABLA_PAG_GALERY = "tPagGalery";

        private const string COLUMN_ID = "Id";
        private const string COLUMN_STYLE = "style";
        private const string COLUMN_DISPLAY = "display";

        private const string DISPLAY_OFF = "display:none;";
        private const string DISPLAY_ON = "display:block;";
        /// <summary>
        /// Procedimiento para convertir un DataRow[] en DataTable
        /// </summary>
        /// <param name="drData">DaraRow[]</param>
        /// <param name="dtData">DataTable Original</param>
        /// <returns>DataTable</returns>
        public static DataTable dsDataRow(DataRow[] drData, DataTable dtData)
        {
            DataTable dtDataNew = dtData.Clone();
            try
            {
                foreach (DataRow row in drData)
                {
                    dtDataNew.ImportRow(row);
                }
            }
            catch { }
            return dtDataNew;
        }
        public static void dsDataTableAdd(DataTable dtData, DataTable dtDataAux)
        {
            try
            {
                foreach (DataRow row in dtDataAux.Rows)
                {
                    dtData.ImportRow(row);
                }
            }
            catch (Exception Ex)
            {
                string sMesage = Ex.Message.ToString();
            }
        }
        public static void dsDataTableAdd(DataSet dsData, DataTable dtData)
        {
            try
            {
                DataTable dtDataNew = dtData.Clone();
                foreach (DataRow row in dtData.Rows)
                {
                    dtDataNew.ImportRow(row);
                }
                dsData.Tables.Add(dtDataNew);
            }
            catch { }
        }
        public static void dsDataTableAdd(DataSet dsData, DataSet dsDataAux)
        {
            try
            {
                int iCounTables = dsDataAux.Tables.Count;
                for (int i = 0; i < iCounTables; i++)
                {
                    string sNameTable = dsDataAux.Tables[i].TableName;
                    string sNameTablePpal = sNameTable;
                    int iNameTable = 1;
                    try
                    {
                        DataTable dtDataNew = dsDataAux.Tables[i].Clone();
                        foreach (DataRow row in dsDataAux.Tables[i].Rows)
                        {
                            dtDataNew.ImportRow(row);
                        }
                        for (int j = 0; j < dsData.Tables.Count; j++)
                        {
                            if (dsData.Tables[j].TableName.Equals(sNameTable))
                            {
                                sNameTable = sNameTablePpal + iNameTable.ToString();
                                iNameTable++;
                            }
                        }
                        dtDataNew.TableName = sNameTable;
                        dsData.Tables.Add(dtDataNew);
                    }
                    catch { }
                }
            }
            catch { }
        }
        /// <summary>
        /// Metodo para adicionar registros en una tabla con als mismas caracteristicas
        /// </summary>
        /// <param name="dsData"></param>
        /// <param name="dsDataAux"></param>
        /// <param name="iTablaIni"></param>
        /// <param name="iCounTables"></param>
        public static void dsDataSetAddRows(DataSet dsData, DataSet dsDataAux, int iTablaIni, int iCounTables)
        {
            try
            {
                if (iCounTables.Equals(0))
                    iCounTables = dsDataAux.Tables.Count;

                for (int i = iTablaIni; i < iCounTables; i++)
                {
                    foreach (DataRow row in dsDataAux.Tables[i].Rows)
                    {
                        dsData.Tables[i].ImportRow(row);
                    }
                }
                dsData.AcceptChanges();
            }
            catch { }
        }
        /// <summary>
        /// Procedimiento para convertir un DataRow[] en DataTable
        /// </summary>
        /// <param name="drData">DaraRow[]</param>
        /// <param name="dtData">DataTable Original</param>
        /// <returns>DataTable</returns>
        public static DataTable dsDataWhere(string sWhere, DataTable dtData)
        {
            DataTable dtDataNew = new DataTable();
            try
            {
                DataView vsDatos = new DataView(dtData);
                vsDatos.RowFilter = sWhere;
                /*pasamos los datos filtrados a un DataTable*/
                dtDataNew = vsDatos.ToTable();
            }
            catch { }
            return dtDataNew;
        }
        public static void dsDataElimina(string sColumna, string sData, DataTable dtData)
        {
            try
            {
                int iCountNivel = dtData.Rows.Count;
                int n = 0;
                int iPosElimina = 0;
                while (n < dtData.Rows.Count)
                {
                    bool bEliminar = false;
                    for (int i = 0; i < iCountNivel; i++)
                    {
                        if (!dtData.Rows[i][sColumna].ToString().Equals(sData))
                        {
                            bEliminar = true;
                            iPosElimina = i;
                            break;
                        }
                    }
                    if (bEliminar)
                    {
                        dtData.Rows.Remove(dtData.Rows[iPosElimina]);
                        n = 0;
                        iCountNivel--;
                    }
                    else
                    {
                        n++;
                    }
                }
            }
            catch { }
            dtData.AcceptChanges();
        }
        /// <summary>
        /// Procedimiento para Ordenar un datatable
        /// </summary>
        /// <param name="drData">DaraRow[]</param>
        /// <param name="dtData">DataTable Original</param>
        /// <returns>DataTable</returns>
        public static DataTable dsDataOrder(string sOrder, DataTable dtData)
        {
            DataTable dtDataNew = new DataTable();
            try
            {
                DataView vsDatos = new DataView(dtData);
                vsDatos.Sort = sOrder;
                /*pasamos los datos filtrados a un DataTable*/
                dtDataNew = vsDatos.ToTable();
            }
            catch { }
            return dtDataNew;
        }
        public static DataTable dsDataWhereOrder(string sWhere, string sOrder, DataTable dtData)
        {
            DataTable dtDataNew = new DataTable();
            try
            {
                DataView vsDatos = new DataView(dtData);
                vsDatos.RowFilter = sWhere;
                vsDatos.Sort = sOrder;
                dtDataNew = vsDatos.ToTable();
            }
            catch { }
            return dtDataNew;
        }
        /// <summary>
        /// Procedimiento para Ordenar un datatable
        /// </summary>
        /// <param name="drData">DaraRow[]</param>
        /// <param name="dtData">DataTable Original</param>
        /// <returns>DataTable</returns>
        public static DataSet dsDataXml(DataTable dtData, string sEncabezado)
        {
            StringBuilder strConsulta = new StringBuilder();
            DataSet dsData = new DataSet();
            XmlDocument xDocumento = new XmlDocument();
            try
            {
                bool bEntra = false;
                int iPosTotal = 0;
                strConsulta.AppendLine("<?xml version='1.0' encoding='utf-8' ?>");
                foreach (DataRow row in dtData.Rows)
                {
                    if (iPosTotal > 1)
                    {
                        break;
                    }
                    else
                    {
                        if (row[0].ToString().Contains("<" + sEncabezado + ">"))
                        {
                            if (iPosTotal.Equals(0))
                                bEntra = true;
                            iPosTotal++;
                        }
                        if (row[0].ToString().Contains("</" + sEncabezado + ">"))
                        {
                            iPosTotal++;
                        }
                        if (bEntra)
                        {
                            strConsulta.AppendLine(row[0].ToString());
                        }
                    }
                }
                clsSerializer cSerializer = new clsSerializer();

                xDocumento.LoadXml(strConsulta.ToString().Replace("&", ""));
                dsData = cSerializer.CrearDataSet(xDocumento, sEncabezado);
            }
            catch (Exception Ex)
            {
                string sMesage = Ex.Message.ToString();
            }
            return dsData;
        }
        public static DataTable dtPaginacionStyle(int iPaginas, string sStyleUno, string sStyleTodos)
        {
            DataTable dtDataNew = new DataTable(TABLA_PAG_GALERY);
            try
            {
                dtDataNew.Columns.Add(COLUMN_ID, typeof(string));
                dtDataNew.Columns.Add(COLUMN_STYLE, typeof(string));
                dtDataNew.Columns.Add(COLUMN_DISPLAY, typeof(string));
                for (int i = 1; i <= iPaginas; i++)
                {
                    DataRow drTableRow = dtDataNew.NewRow();
                    drTableRow[COLUMN_ID] = i.ToString();
                    if (i.Equals(1))
                    {
                        drTableRow[COLUMN_STYLE] = sStyleUno;
                    }
                    else
                    {
                        drTableRow[COLUMN_STYLE] = sStyleTodos;
                        drTableRow[COLUMN_DISPLAY] = DISPLAY_OFF;
                    }
                    dtDataNew.Rows.Add(drTableRow);
                }
                DataRow drTableRowPag = dtDataNew.NewRow();
                drTableRowPag[COLUMN_ID] = "aginacionGaleria";
                dtDataNew.Rows.Add(drTableRowPag);
                dtDataNew.AcceptChanges();
            }
            catch { }
            return dtDataNew;
        }
        public static DataTable dtPaginacionDetalle(int iPaginaIni, int iPaginaTotal, DataTable dtData)
        {
            DataTable dtDataNew = dtData.Clone();
            try
            {
                for (int i = iPaginaIni; i < iPaginaTotal; i++)
                {
                    dtDataNew.Rows.Add(dtData.Rows[i].ItemArray);
                }
                dtDataNew.AcceptChanges();
            }
            catch { }
            return dtDataNew;
        }
    }
}
