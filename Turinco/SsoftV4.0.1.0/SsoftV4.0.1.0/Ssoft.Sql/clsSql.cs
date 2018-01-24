using System;
using System.Collections.Generic;
using System.Text;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;

namespace Ssoft.Sql
{
    public class Sql
    {
        public Sql()
        {
        }
        public string SqlSentencia(TipoComando TipoSql, string[] Fields, string[] Values, TipoCampo[] Tipo, string From, string Where, string Order, int NumFields)
        {
            string cFields = string.Empty;
            string cSql = string.Empty;
            try
            {
                switch (TipoSql)
                {
                    case TipoComando.Select:
                        for (int i = 0; i < NumFields; i++)
                        {
                            if ((i + 1) < NumFields)
                                cFields = cFields + Fields[i].ToString() + ", ";
                            else
                                cFields = cFields + Fields[i].ToString();
                        }
                        cSql = "  SELECT ";
                        if (NumFields == 0)
                            cSql = cSql + " * ";
                        else
                            cSql = cSql + cFields + " ";
                        cSql = cSql + " FROM " + From + " ";
                        if (Where != null)
                            cSql = cSql + " WHERE " + Where + " ";
                        if (Order != null)
                            cSql = cSql + " ORDER BY " + Order;
                        break;

                    case TipoComando.Update:
                        for (int i = 0; i < NumFields; i++)
                        {
                            if ((i + 1) < NumFields)
                            {
                                if (Tipo[i].Equals(TipoCampo.String) || Tipo[i].Equals(TipoCampo.DateTime))
                                {
                                    cFields = cFields + Fields[i].ToString() + " = '" + Values[i].ToString() + "', ";
                                }
                                else
                                {
                                    cFields = cFields + Fields[i].ToString() + " = " + Values[i].ToString() + ", ";
                                }
                            }
                            else
                            {
                                if (Tipo[i].Equals(TipoCampo.String) || Tipo[i].Equals(TipoCampo.DateTime))
                                {
                                    cFields = cFields + Fields[i].ToString() + " = '" + Values[i].ToString() + "' ";
                                }
                                else
                                {
                                    cFields = cFields + Fields[i].ToString() + " = " + Values[i].ToString();
                                }
                            }
                        }
                        cSql = "  UPDATE " + From + " SET " + cFields;
                        if (Where != null)
                            cSql = cSql + " WHERE " + Where;
                        break;

                    case TipoComando.Insert:
                        cFields = "(";
                        for (int i = 0; i < NumFields; i++)
                        {
                            if ((i + 1) < NumFields)
                            {
                                cFields = cFields + Fields[i].ToString() + ", ";
                            }
                            else
                            {
                                cFields = cFields + Fields[i].ToString();
                            }
                        }
                        cFields = cFields + ") VALUES (";
                        for (int i = 0; i < NumFields; i++)
                        {
                            if ((i + 1) < NumFields)
                            {
                                if (Tipo[i].Equals(TipoCampo.String) || Tipo[i].Equals(TipoCampo.DateTime))
                                {
                                    cFields = cFields + " '" + Values[i].ToString() + "', ";
                                }
                                else
                                {
                                    cFields = cFields + Values[i].ToString() + ", ";
                                }
                            }
                            else
                            {
                                if (Tipo[i].Equals(TipoCampo.String) || Tipo[i].Equals(TipoCampo.DateTime))
                                {
                                    cFields = cFields + " '" + Values[i].ToString() + "' ";
                                }
                                else
                                {
                                    cFields = cFields + Values[i].ToString();
                                }
                            }
                        }

                        cFields = cFields + ")";
                        cSql = "  INSERT INTO " + From + " " + cFields;
                        if (Where != null)
                            cSql = cSql + " WHERE " + Where;
                        break;

                    case TipoComando.Delete:
                        cSql = "  DELETE " + From + " ";
                        if (Where != null)
                            cSql = cSql + " WHERE " + Where;
                        break;
                }
                return cSql;
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                throw new Exception(ex.Message);
            }
        }
        public string SqlSentencia(string Select, string From, string Where, string Order)
        {
            string cSql = string.Empty;
            try
            {
                if (Select == null)
                    Select = " * ";
                cSql = "  SELECT " + Select + " ";
                cSql = cSql + " FROM " + From + " ";
                if (Where != null)
                    cSql = cSql + " WHERE " + Where + " ";
                if (Order != null)
                    cSql = cSql + " ORDER BY " + Order;

                return cSql;
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                throw new Exception(ex.Message);
            }
        }
    }
}
