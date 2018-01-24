using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.ManejadorExcepciones;
using System.Data;

namespace Ssoft.Utils
{
    public class clsResultados
    {
        #region [ ATRIBUTOS ]

        private clsParametros clsError;
        private DataSet dtsResultados;
        private DataTable dtresultados;
        private String strresultado = string.Empty;
        private List<String> lstrDatos = new List<String>();
        #endregion

        #region [ CONSTRUCTOR ]

        public clsResultados()
        {
        }

        #endregion

        #region [ PROPIEADES ]
        public clsParametros Error
        {
            get { return clsError; }
            set { clsError = value; }
        }
        public DataSet dsResultados
        {
            get { return dtsResultados; }
            set { dtsResultados = value; }
        }
        public DataTable dtResultados
        {
            get { return dtresultados; }
            set { dtresultados = value; }
        }
        public  String strResultados
        {
            get { return strresultado; }
            set { strresultado = value; }
        }

        public List<String> strDatos
        {
            get { return lstrDatos; }
            set { lstrDatos = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~clsResultados() { }
        #endregion
    }
    [Serializable]
    public class clsEstructura
    {
        #region [ ATRIBUTOS ]

        private int iDataId;
        private string sDataName;
        private List<clsTabla> lcTable;
        private List<clsRelaciones> lcRelations;
        #endregion

        #region [ CONSTRUCTOR ]

        public clsEstructura()
        {
        }

        #endregion

        #region [ PROPIEADES ]
        public int DataId
        {
            get { return iDataId; }
            set { iDataId = value; }
        }
        public string DataName
        {
            get { return sDataName; }
            set { sDataName = value; }
        }
        public List<clsTabla> lTable
        {
            get { return lcTable; }
            set { lcTable = value; }
        }
        public List<clsRelaciones> lRelations
        {
            get { return lcRelations; }
            set { lcRelations = value; }
        }

        #endregion

        #region [ METODOS ]

        public clsEstructura getEstructura(DataSet dsData)
        {
            clsEstructura cEstructura = new clsEstructura();
            List<clsRelaciones> lcRelaciones = new List<clsRelaciones>();
            List<clsTabla> lcTablas = new List<clsTabla>();
            cEstructura.DataId = 0;
            cEstructura.DataName = dsData.DataSetName;
            try
            {
                int iCounTable = dsData.Tables.Count;

                for (int t = 0; t < iCounTable; t++)
                {
                    int iCountColumns = dsData.Tables[t].Columns.Count;
                    clsTabla cTabla = new clsTabla();
                    List<clsColumnas> lcColumnas = new List<clsColumnas>();
                    cTabla.TableId = t;
                    cTabla.TableName = dsData.Tables[t].TableName;
                    for (int c = 0; c < iCountColumns; c++)
                    {
                        clsColumnas cColumnas = new clsColumnas();
                        cColumnas.ColumnId = c;
                        cColumnas.ColumnName = dsData.Tables[t].Columns[c].ColumnName;
                        cColumnas.ColumnType = dsData.Tables[t].Columns[c].DataType.Name;
                        lcColumnas.Add(cColumnas);
                    }
                    cTabla.lColumns = lcColumnas;
                    lcTablas.Add(cTabla);
                }

                int iCounRelations = dsData.Relations.Count;

                for (int r = 0; r < iCounRelations; r++)
                {
                    clsRelaciones cRelations = new clsRelaciones();
                    cRelations.RelationsId = r;
                    cRelations.RelationName = dsData.Relations[r].RelationName;
                    cRelations.RelationParentName = dsData.Relations[r].ParentTable.TableName;
                    cRelations.RelationChildName = dsData.Relations[r].ChildTable.TableName;

                    List<clsColumnas> lcColumnasRelaParent = new List<clsColumnas>();
                    List<clsColumnas> lcColumnasRelaChild = new List<clsColumnas>();

                    int iCountColumnsRela = dsData.Relations[r].ParentColumns.Length;
                    for (int cp = 0; cp < iCountColumnsRela; cp++)
                    {
                        clsColumnas cColumnasRelaParent = new clsColumnas();
                        cColumnasRelaParent.ColumnId = cp;
                        cColumnasRelaParent.ColumnName = dsData.Relations[r].ParentColumns[cp].ColumnName;
                        cColumnasRelaParent.ColumnType = dsData.Relations[r].ParentColumns[cp].DataType.Name;
                        lcColumnasRelaParent.Add(cColumnasRelaParent);
                    }

                    iCountColumnsRela = dsData.Relations[r].ChildColumns.Length;
                    for (int cp = 0; cp < iCountColumnsRela; cp++)
                    {
                        clsColumnas cColumnasRelaChild = new clsColumnas();
                        cColumnasRelaChild.ColumnId = cp;
                        cColumnasRelaChild.ColumnName = dsData.Relations[r].ChildColumns[cp].ColumnName;
                        cColumnasRelaChild.ColumnType = dsData.Relations[r].ChildColumns[cp].DataType.Name;
                        lcColumnasRelaChild.Add(cColumnasRelaChild);
                    }
                    cRelations.lRelationParentColumns = lcColumnasRelaParent;
                    cRelations.lRelationChildColumns = lcColumnasRelaChild;
                    lcRelaciones.Add(cRelations);
                }
                cEstructura.lRelations = lcRelaciones;
                cEstructura.lTable = lcTablas;
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Metodo = "clsEstructura";
                cParametros.Complemento = "Creando estructura dataset " + dsData.DataSetName.ToString();
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                ExceptionHandled.Publicar(cParametros);
            }
            return cEstructura;
        }

        #endregion

        #region [ DESTRUCTOR ]
        ~clsEstructura() { }
        #endregion
    }
    public class clsTabla
    {
        #region [ ATRIBUTOS ]

        private int iTableId;
        private string sTableName;
        private List<clsColumnas> lcColumns;
        #endregion

        #region [ CONSTRUCTOR ]

        public clsTabla()
        {
        }

        #endregion

        #region [ PROPIEADES ]
        public int TableId
        {
            get { return iTableId; }
            set { iTableId = value; }
        }
        public string TableName
        {
            get { return sTableName; }
            set { sTableName = value; }
        }
        public List<clsColumnas> lColumns
        {
            get { return lcColumns; }
            set { lcColumns = value; }
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~clsTabla() { }
        #endregion
    }
    public class clsColumnas
    {
        #region [ ATRIBUTOS ]

        private int iColumnId;
        private string sColumnName;
        private string sColumnWidth;
        private string sColumnType;
        #endregion

        #region [ CONSTRUCTOR ]

        public clsColumnas()
        {
        }

        #endregion

        #region [ PROPIEADES ]
        public int ColumnId
        {
            get { return iColumnId; }
            set { iColumnId = value; }
        }
        public string ColumnName
        {
            get { return sColumnName; }
            set { sColumnName = value; }
        }
        public string ColumnWidth
        {
            get { return sColumnWidth; }
            set { sColumnWidth = value; }
        }
        public string ColumnType
        {
            get { return sColumnType; }
            set { sColumnType = value; }
        }

        #endregion

        #region [ DESTRUCTOR ]
        ~clsColumnas() { }
        #endregion
    }
    public class clsRelaciones
    {
        #region [ ATRIBUTOS ]

        private int iRelationsId;
        private string sRelationName;
        private string sRelationParentName;
        private string sRelationChildName;
        private List<clsColumnas> lcRelationParentColumns;
        private List<clsColumnas> lcRelationChildColumns;
        private bool bConstraints = false;
        #endregion

        #region [ CONSTRUCTOR ]

        public clsRelaciones()
        {
        }

        #endregion

        #region [ PROPIEADES ]
        public int RelationsId
        {
            get { return iRelationsId; }
            set { iRelationsId = value; }
        }
        public string RelationName
        {
            get { return sRelationName; }
            set { sRelationName = value; }
        }
        public string RelationParentName
        {
            get { return sRelationParentName; }
            set { sRelationParentName = value; }
        }
        public string RelationChildName
        {
            get { return sRelationChildName; }
            set { sRelationChildName = value; }
        }
        public List<clsColumnas> lRelationParentColumns
        {
            get { return lcRelationParentColumns; }
            set { lcRelationParentColumns = value; }
        }
        public List<clsColumnas> lRelationChildColumns
        {
            get { return lcRelationChildColumns; }
            set { lcRelationChildColumns = value; }
        }
        public bool Constraints
        {
            get { return bConstraints; }
            set { bConstraints = value; }
        }

        #endregion

        #region [ METODOS ]

        public void getRelaciones(DataSet dsData, List<clsRelaciones> clRelaciones)
        {
            try
            {
                int iCounRelations = clRelaciones.Count;
                for (int r = 0; r < iCounRelations; r++)
                {
                    clsRelaciones cRelaciones = new clsRelaciones();
                    cRelaciones = clRelaciones[r];

                    dsData.Relations.Add(cRelaciones.RelationName, dsData.Tables[cRelaciones.RelationParentName].Columns[cRelaciones.lRelationParentColumns[0].ColumnName], dsData.Tables[cRelaciones.RelationChildName].Columns[cRelaciones.lRelationChildColumns[0].ColumnName], cRelaciones.Constraints);
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Metodo = "clsRelaciones";
                cParametros.Complemento = "Creando relacion: Datset " + dsData.DataSetName.ToString();
                cParametros.Source = Ex.Source;
                cParametros.StackTrace = Ex.StackTrace;
                ExceptionHandled.Publicar(cParametros);
            }
        }

        #endregion

        #region [ DESTRUCTOR ]
        ~clsRelaciones() { }
        #endregion
    }
}

