/*
===========================================================================================
Descripción: Componente corporativo que provee la funcionalidad para el
acceso a datos sobre repositorios relacionales como son MSSQLServer.
Autor: José Faustino Posas
URL: http://www.consultinltda.com
Email: consultinltda@hotmail.com
Fecha: 2006-03-29
===========================================================================================
*/
using System;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;
using Ssoft.Utils;

namespace Ssoft.Data
{
    /// <summary>
    /// La Clase AccesoSQL es Encapsulada para logra alto performance, practica mejor escalabilidad para 
    /// usuarios comunes de sqlClient
    /// </summary>
    /// <remarks>
    /// Autor: José Faustino Posas
    /// URL: http://www.consultinltda.com
    /// Email: consultinltda@hotmail.com
    /// Fecha: 2006-03-29
    /// </remarks>

    public sealed class AccesoSQL
    {
        #region Utilidades metodos y constructores
        //Esta Clase provee solo metodos static, hace el constructor private por defecto para prevenir
        //instacias de inicio creadas con "new AccesoSQL()"
        private AccesoSQL() { }

        /// <summary>
        /// Este metodo es usado para adjuntar un vector de tipo SqlParameter a un SqlComand
        /// 
        /// Este metodo asignara un valor de DbNull a cualquier parametro con una dirección de 
        /// Entrada-Salida y un valor de null
        /// 
        /// Este Comportamiento prevendra valores por defecto de usuario iniciado.
        /// </summary>
        /// <param name="command">El comando para el cual el parametro sera adicionado</param>
        /// <param name="commandParameters">Un Vector de SqlParameters para ser adicionado a command</param>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        //Chequea por recivido valor salida con valor no asignado
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        /// <summary>
        /// Este Metodo asignas valores a columnas dataRow a un vector de SqlParametros
        /// </summary>
        /// <param name="commandParameters">Vector de SqlParameters para valores asignados</param>
        /// <param name="dataRow">El dataRow usado espera el valor parametro del procedimiento almacenado</param>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>

        private static void AssignParameterValues(SqlParameter[] commandParameters, DataRow dataRow)
        {
            if ((commandParameters == null) || (dataRow == null))
            {
                //No hacer nada si nosotros no obtenemos datos
                return;
            }

            int i = 0;
            //Poner los valores de los parametros
            foreach (SqlParameter commandParameter in commandParameters)
            {
                //Chequea el nombre del parametro
                if (commandParameter.ParameterName == null ||
                    commandParameter.ParameterName.Length <= 1)
                    throw new Exception(
                        string.Format(
                            "Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.",
                            i, commandParameter.ParameterName));
                if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) != -1)
                    commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
                i++;
            }
        }

        /// <summary>
        /// Este metodo asignas un vector de valores a un vector de SqlParameters
        /// </summary>
        /// <param name="commandParameters">Vector de SqlParameters para ser asignado valores</param>
        /// <param name="parameterValues">Vector de objetos obtienen los valores para ser asignados</param>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                // Hacer nada si nosotros no obtenemos dato
                return;
            }

            // Nosotros tenemos que tener el mismo numero de valor
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                //Si el valor del vector concurrente entrega desde IDbDataParameter, entonces asigna esta propiedad valor
                if (parameterValues[i] is IDbDataParameter)
                {
                    IDbDataParameter paramInstance = (IDbDataParameter)parameterValues[i];
                    if (paramInstance.Value == null)
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = paramInstance.Value;
                    }
                }
                else if (parameterValues[i] == null)
                {
                    commandParameters[i].Value = DBNull.Value;
                }
                else
                {
                    commandParameters[i].Value = parameterValues[i];
                }
            }
        }

        /// <summary>
        /// Este metodo abre(Si necesita) y asigna una conexion, transacción, tipo comando y parametros
        /// </summary>
        /// <param name="command">El SqlCommand estar preparado</param>
        /// <param name="connection">Un SqlConenection valido, sobre el cual ejecuta este command</param>
        /// <param name="transaction">Un SqlTransaction valido, o 'null'</param>
        /// <param name="commandType">El commandType (procedimiento almacenado, texto, etc.)</param>
        /// <param name="commandText">El nombre del procedimiento almacenado o comando T-SQL</param>
        /// <param name="commandParameters">Un Vector de SqlParameters para ser asociado con el comando o 'null' si los parametros no son requeridos</param>
        /// <param name="mustCloseConnection"><c>true</c> True, Si la conexión fue abierta por el metodo, en otro caso es False</param>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            //Si el conexion no esta abierta, nosotros la abriremos
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }
            //Asocio la conexión con el command
            command.Connection = connection;

            //Entrego al comando text(nombre del procedimiento almacenado o el estamento SQL)
            command.CommandText = commandText;

            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            //Entrego el tipo de comando
            command.CommandType = commandType;
            // Amplio el tiempo de espera
            int iTimeOutBD = 0;
            try
            {
                iTimeOutBD = int.Parse(clsValidaciones.GetKeyOrAdd("iTimeOutBD", "0"));
            }
            catch{}
            if(iTimeOutBD > 0)
                command.CommandTimeout = iTimeOutBD;

            // Adjunto el parametro command si ellos estan proveidos.
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        #endregion private utility methods & constructors

        #region Metodos para ExecuteNonQuery

        /// <summary>
        /// Ejecuta un SqlCommand contra la baseDatos especifica en
        /// la cadena de conexión.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de conexión valida para un SqlConnection</param>
        /// <param name="commandType">El CommandType (Procedimiento Almacenado, Texto, etc.)</param>
        /// <param name="commandText">El Nombre del procedimiento Almacenado o comando T-SQL</param>
        /// <returns>Un int representando el numero de filas afectadas por el command</returns>

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            // Pasa a través del llamado provellendo null para colocar en el SqlParameter
            return ExecuteNonQuery(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (Aquello retorna no resultset) contra la base de datos especificado en la cadena de conexión
        /// usando los proveedores de parametros
        /// 
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una cadena de conexión para un SqlConnection</param>
        /// <param name="commandType">El CommandType (Procedimiento Almacenado, Texto, etc.)</param>
        /// <param name="commandText"> El Nombre del procedimiento Almacenado o el comando</param>
        /// <param name="commandParameters">Un Vector de SqlParametros usado para ejecutar el command</param>
        /// <returns>Un entero Representando el número de filas afectadas por el command</returns>

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

            // Crea y abre una SqlConnection, y disponer de este 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Llama la sobrecarga que toma una conexion en espacio de la cadena de conexión
                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado por un SqlCommand (Aquel retorna no resultset) contra la basedatos especifica en       
        /// la cadena de conexión usando el proveedor de parametros. Este metodo preguntará a la base de datos por los parametros del 
        /// procedimiento almacenado (la primera vez cada procedimiento almacenado es llamado), y asigna el valor basado en order de los parametros.
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso a parametros de salida o el procedimiento almacenado retorna valor del parametro
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, "PublishOrders", 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una cadena de conexión valido para un SqlConnection</param>
        /// <param name="spName">El nombre del procedimiento almacenado</param>
        /// <param name="parameterValues">Un Vector de Objetos para ser asignado como un valor de entrada del procedimiento almacenado</param>
        /// <returns> Un  int representando el numero de filas afectadas por el command</returns>

        public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // Si nosotros recibimos valores de parametros, nosotros necesitamos formar la salida para donde ellos van
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Arrancar los parametros para este procedimiento almacenado desde el parametro de cache (o descubrirlos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connectionString, spName);

                //Asigna el valor del proveedor para estos parametros basados en el orden de los parametro
                AssignParameterValues(commandParameters, parameterValues);

                // Llama la sobrecarga que toma un vector de sqlparameters
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otro Caso nosotros prodemos llamar el SP sin parametros
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SqlCommand (Aquel Retorna no resultset y no toma parametros) contra la SqlConnection proveida
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Una SqlConnection Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre del procedimiento Almacenado o el comando de T-SQL</param>
        /// <returns>Un int representando el numero de filas afectadas por el comando</returns>

        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connection, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (Aquel retorna no resulset) contra la SqlConnection especificada
        /// Usando el parametro provisto
        /// 
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29

        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection valido</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre del procedimiento Almacenado o el comando T-SQL</param>
        /// <param name="commandParameters">Un vector de SqlParameters usado para Ejecutar el Command</param>
        /// <returns>Un Entero Representando el numero de filas afectadas por el command</returns>

        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            //Crea un command y prepara para ejecución
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            //Finalmente, executa el command
            int retval = cmd.ExecuteNonQuery();

            //Separa el SqlParameters desde el objeto command, asi ellos pueden ser usados de nuevo
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }

        /// <summary>
        /// Ejecuta un SP via SqlCommand (Aquel retorna no resultset) contra la SqlConnection especificada       
        /// Usando el valor del parametro provisto. Este metodo preguntará a la base de datos los parametros para el        
        /// procedimiento almacenado (la primera vez cada SP es llamado), y asignado el valor basado en el orden del parametro
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para salida de parametros o el SP retorna parametros valor
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection valido</param>
        /// <param name="spName">El nombre de el Procedimiento Almacenado</param>
        /// <param name="parameterValues">Un vector de objetos para ser asignado como valor de entrada del SP</param>
        /// <returns>Un int Representando el número de filas afectadas por el command</returns>

        public static int ExecuteNonQuery(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // Si nosotros recibimos valores parametros, necesitamos para salida donde ellos van
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connection, spName);

                // Asignar el valor proveido para estos parametros basados en orden de los parametros
                AssignParameterValues(commandParameters, parameterValues);

                // Llamar la sobrecarga que toma un vector de SqlParameters
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otro Caso podemos llamar el SP sin Parametros
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejectuta un SqlCommand (Aquel retorna no resultset y no toma parametros) contra la SqlTransaction proveida
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <returns>Un entero Representando el número de filas afectadas por el command</returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
        {

            return ExecuteNonQuery(transaction, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna no resultset) contra la sqlTransaction especificada
        /// usando los parametros proveidos
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="commandParameters">Un Vector de SqlParameters usado para ejecutar el comando</param>
        /// <returns>Un entero Representando el número de filas afectadas por el command</returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Crea un command y lo prepara para ejecución
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            // Finalmente, ejecuta el command
            int retval = cmd.ExecuteNonQuery();

            // Separa el SqlParameters desde el objeto command, asi pueden ser usados de nuevo
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna no resultset) contra el 
        /// SqlTransaction especificado usando el valor del parametro proveido. Este metodo preguntará a la base de datos para descubrir los parametros para los
        /// SP (la primera vez cada uno SP es llamado), y asignar el valor basado en el orden del parametro
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para salida de parametros o el retorno valor parametro del SP
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, trans, "PublishOrders", 24, 36);
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="parameterValues">Un vector de objetos para ser asignado como valor de entrada del SP</param>
        /// <returns>Un entero Representando el número de filas afectadas por el command</returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(transaction.Connection, spName);


                AssignParameterValues(commandParameters, parameterValues);

                // Llamar la sobrecarga que toma un vector de SqlParameters
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                //Otro Caso podemos llamar el SP sin Parámetros
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteNonQuery

        #region Metodos para ExecuteDataset

        /// <summary>
        /// Ejecuta un SqlCommand (Que retorna un resultset y no toma parametros) contra la base de datos especificada en
        /// la cadena de conexión.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>         
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <returns>Un dataset conteniendo el resultset generado por el commmand</returns>

        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset) contra la base de datos especificada en la cadena conexión
        /// usando los parametros proveidos
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="commandParameters">Un vector de SqlParametros usado para ejecutar el command</param>
        /// <returns>Un dataset conteniendo el resultset generado por el commmand</returns>
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

            //Crea y abre un SqlConnection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                return ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (Que retorna un resultset) contra la base de datos especificado en
        /// la cadena de conexión usando los valores parametros proveidos. Este metodo preguntará la base de datos para descubrir los parametros para los
        /// SP (la primera vez cada uno SP es llamado), y asignar los valores basados en el orden de los parametros
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o parametro valor de los SP
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, "GetOrders", 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="parameterValues">Un vector de objetos para ser asignado como valores de entrada de el SP</param>
        /// <returns>Un dataset conteniendo el resultset generado por el commmand</returns>
        public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connectionString, spName);

                //Asignar los valores proveidos para estos parametros basados en el orden de los parametros
                AssignParameterValues(commandParameters, parameterValues);

                // Llamar la sobrecarga que toma un vector de SqlParameters
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                //Otro Caso podemos llamar el SP sin Parámetros
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecutar un SqlCommad (Que retorna un resultset y no toma parametros) contra la SqlConnection proveida.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <returns>Un dataset conteniendo el resultset generado por el commmand</returns>
        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connection, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset) contra la SqlConnection especificada
        /// usando los parametros proveidos
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="commandParameters">Un vector de SqlParameters usado para ejecutar el command</param>
        /// <returns>Un dataset conteniendo el resultset generado por el commmand</returns>
        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            //Crea un comando y prepara para ejecutarlo
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            //Crea el DataAdapter y Dataset
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();

                // Llena el Dataset usando valores por defecto para nombres DataTable, etc
                da.Fill(ds);

                // Separa los SqlParameters desde el objeto command, asi pueden ser usados de nuevo
                cmd.Parameters.Clear();

                if (mustCloseConnection)
                    connection.Close();

                // Retorna el dataset
                return ds;
            }
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommmand (que retorna un resultset) contra el SqlConnection especificado
        /// usando los valores parametros proveidos. Este metodo preguntará la base de datos para descubrir los parametros para los
        /// SP (la primera vez cada uno de los SP son llamados), y asigna los valores basados en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o valor parametros retorno del SP
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, "GetOrders", 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="parameterValues">Un vector de objetos para ser asignados como los valores de entrada de los SP</param>
        /// <returns>Un dataset conteniendo el resultset generado por el commmand</returns>
        public static DataSet ExecuteDataset(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connection, spName);

                // asignar los valores proveidos para estos parametros basados en el orden de los parametros
                AssignParameterValues(commandParameters, parameterValues);

                //llamar la sobrecarga que toma un vector de SqlParameters
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                //Otro Caso podemos llamar el SP sin Parámetros
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset y no toma parametros) contra el SqlTransaction proveido.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <returns>Un dataset conteniendo el resultset generado por el commmand</returns>
        public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteDataset(transaction, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset) contra el SqlTransaction especificado
        /// usando el parametro proveido
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>Un dataset conteniendo el resultset generado por el commmand</returns>
        public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Crea un comando y prepara para ejecución
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            //Crea el DataAdapter y Dataset
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();

                //Llena el Dataset usando valores por defecto para nombres DataTable, etc
                da.Fill(ds);

                //Separa el Sqlparameters desde el objeto command, asi pueden ser utilizados de nuevo
                cmd.Parameters.Clear();

                // Retorna el DataSet
                return ds;
            }
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (Que retorna un resultset) contra la SqlTransaction especificada
        /// usando el valor parametro proveido.  Este metodo preguntará la base de datos para descubrir los parametros del 
        /// SP (la primera vez cada SP son llamados), y asigna el valor basado en el orden de los parametros 
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o retorna parametro valor del SP
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, "GetOrders", 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="parameterValues">Un vector de objetos para ser asignados como valores de entrada del SP</param>
        /// <returns>Un dataset conteniendo el resultset generado por el commmand</returns>
        public static DataSet ExecuteDataset(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(transaction.Connection, spName);

                //Asigna valores proveidos por estos parametros basado en el orden de los parametros.
                AssignParameterValues(commandParameters, parameterValues);

                // Llamar la sobrecarga que toma un vector de SqlParameters
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                //Otro Caso podemos llamar el SP sin Parámetros
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteDataset

        #region Metodos para ExecuteReader

        /// <summary>
        ///  ??? Este numeral es usado para indicar sea que la conexión fue proveida por el llamador, o creado por AccesoSQL, asi que
        /// podemos poner el apropiado CommandBehavior cuando llamamos el ExecuteReader
        /// </summary>
        private enum SqlConnectionOwnership
        {
            /// <summary>Enumerado es propiedad y administrada por AccesoSQL</summary>
            Internal,
            /// <summary>Enumerado es propietario y administrado por el llamador</summary>
            External
        }

        /// <summary>
        /// Crea y prepara un SqlCommand, y llama ExecuteReader con el apropiado CommmandBehavior.
        /// </summary>
        /// <remarks>
        /// Si creamos y abrimos la conexión, nosotros queremos que la conexión se cierre cuando el DataReader este cerrado.
        /// 
        /// Si el Llamador provee la conexión, queremos dejarlo como para ellos lo manejen
        /// /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido, en el cual ejecute este command</param>
        /// <param name="transaction">Una SqlTransaction Valida, o 'null'</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="commandParameters">Un vector de SqlParameters para ser asociado con el command o 'null' si no requiere estos parametros</param>
        /// <param name="connectionOwnership">Indica sea que el parametro conexión fue proveido por el llamador, o creado por AccesoSQL</param>
        /// <returns>SqlDataReader conteniendo los resultados de el command</returns>
        private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, SqlConnectionOwnership connectionOwnership)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            bool mustCloseConnection = false;
            // Crea un commmand y prepara para ejecución
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

                // Crea un reader
                SqlDataReader dataReader;

                //Llama ExecuteReader con el CommandBehavior apropiado
                if (connectionOwnership == SqlConnectionOwnership.External)
                {
                    dataReader = cmd.ExecuteReader();
                }
                else
                {
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }

                //Separa el SqlParameters desde el objeto command, asi estos pueden ser usados de nuevo
                //??? HACK: Hay un problema aqui, la valores parametros salida son fletched
                //cuando el reader es cerrado, asi los parametros sean separados desde el command
                //entonces el Sqlreder no puede colocar estos valores
                //cuando esto sucede, los parametros no pueden ser usados de nuevo en otro command
                bool canClear = true;
                foreach (SqlParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                        canClear = false;
                }

                if (canClear)
                {
                    cmd.Parameters.Clear();
                }

                return dataReader;
            }
            catch
            {
                if (mustCloseConnection)
                    connection.Close();
                throw;
            }
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset y no toma parametros) contra la base de datos especificado en
        /// la cadena de conexión.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <returns>Un SqlDataReader conteniendo los resulset generados por el command</returns>
        /// 
        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {

            return ExecuteReader(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset) contra la base de datos especificada en la cadena de conexión
        /// usando los parametros proveidos
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        ///  		
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="commandParameters">Un vector de SqlParametros usados para ejecutar el command</param>
        /// <returns>Un SqlDataReader conteniendo los resulset generados por el command</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();


                return ExecuteReader(connection, null, commandType, commandText, commandParameters, SqlConnectionOwnership.Internal);
            }
            catch
            {
                // si fallamos para retornar el SqlDataReader, nosotros mismos cerramos la conexion 
                if (connection != null) connection.Close();
                throw;
            }

        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un resultset) contra la base de datos especificada en 
        /// la cadena de conexión usando el valor parametro proveido. este metodo preguntará la base de datos para descubrir los parametros  para el
        /// SP (la primera vez cada SP es llamado), y asigna los valores basados en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o retorno parametro valor del SP
        /// 
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(connString, "GetOrders", 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="parameterValues">Un vector de objetos para ser asignados como valores de entrada de los SP</param>
        /// <returns>Un SqlDataReader conteniendo los resulset generados por el command</returns>
        public static SqlDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connectionString, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                //Otro Caso podemos llamar el SP sin Parámetros
                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset y no toma parametros) contra la SqlConnetion proveida.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <returns>Un SqlDataReader conteniendo los resulset generados por el command</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteReader(connection, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommad (que retorna un resultset) contra SqlConnection especificada
        /// usando el parametro proveido
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="commandParameters">Un vector de SqlParametros usado para ejecutar los command</param>
        /// <returns>Un SqlDataReader conteniendo los resulset generados por el command</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(connection, (SqlTransaction)null, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un resultset) contra el SqlConnection especificada
        /// usando el valor parametro proveido. este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna el valor basado en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o retorno parametro valor del SP
        /// 
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(conn, "GetOrders", 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="parameterValues">Un Vector de objetos para ser asignado como valor de entrada del SP</param>
        /// <returns>Un SqlDataReader conteniendo los resulset generados por el command</returns>
        public static SqlDataReader ExecuteReader(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");


            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connection, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                //Otro Caso podemos llamar el SP sin Parámetros
                return ExecuteReader(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset y no toma parametros) contra el SqlTransaction proveido
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <returns>Un SqlDataReader conteniendo los resulset generados por el command</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {

            return ExecuteReader(transaction, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset) contra el SqlTransaction especificado
        /// usando los parametros proveidos.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   SqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        ///   
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="commandParameters">Un vector de SqlParamers usado para ejecutar el command</param>
        /// <returns>Un SqlDataReader conteniendo los resulset generados por el command</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");


            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un resultset) contra el SqlTransaction especificado
        /// usando el valor parametro proveido.  Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado)
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o retorno parametros valor del SP
        /// 
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(trans, "GetOrders", 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="parameterValues">Un vector de objectos para ser asignado como valores de entrada del SP</param>
        /// <returns>Un SqlDataReader conteniendo los resulset generados por el command</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");


            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(transaction.Connection, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otro Caso podemos llamar el SP sin Parámetros
                return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteReader

        #region Metodos para ExecuteScalar

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna 1x1 resultset y no toma parametros) contra la base de datos especificada en
        /// la cadena de conexión
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <returns>Un objeto conteniendo el valor en el 1x1 resultset generado por el command</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {

            return ExecuteScalar(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un 1x1 resultset) contra la base de datos especificada en la cadena de conexión
        /// usando los parametros proveidos
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="commandParameters">Un vector de SqlParameters usado para ejecutar el command</param>
        /// <returns>Un objeto conteniendo el valor en el 1x1 resultset generado por el command</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            //Crea y abre un Sqlconnection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                return ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un 1x1 resultset) contra la base de datos especificado en
        /// la cadena de conexión usando el valor parametro proveido.  este metodo pregunta la base de datos para descubrir los parametros para el
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en el orden de los parametros
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o retorna valor parametro del SP
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, "GetOrderCount", 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="parameterValues">Un vector de objetos para ser asignado como el valor de entrada de los SP</param>
        /// <returns>Un objeto conteniendo el valor en el 1x1 resultset generado por el command</returns>
        public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");


            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connectionString, spName);

                // Asigna los valores proveidos por estos parametros basado en el orden de los parametros
                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otro Caso podemos llamar el SP sin Parámetros
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un 1x1 resultset y no toma parametros) contra el SqlConnection proveido
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <returns>Un objeto conteniendo el valor en el 1x1 resultset generado por el command</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
        {

            return ExecuteScalar(connection, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un 1x1 resultset) contra la SqlConnection especificada
        /// usando los parametros proveidos.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="commandParameters">Un vector de SqlParameters usado para ejecutar el command</param>
        /// <returns>Un objeto conteniendo el valor en el 1x1 resultset generado por el command</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            //Crea un command y prepara para ejecución
            SqlCommand cmd = new SqlCommand();

            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Ejecuta el command y retorna los resultados
            object retval = cmd.ExecuteScalar();

            // Separa los SqlParameters desde el objeto command, asi pueden ser usados de nuevo
            cmd.Parameters.Clear();

            if (mustCloseConnection)
                connection.Close();

            return retval;
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un 1x1 resultset) contra el SqlConnection especificado
        /// usando el valor parametro proveido.  este metodo preguntará la base de datos para descubrir los parametros para el
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o retorno valor parametro del SP
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="parameterValues">Un vector de objetos para ser asignado como valores de entrada de los SP</param>
        /// <returns>Un objeto conteniendo el valor en el 1x1 resultset generado por el command</returns>
        public static object ExecuteScalar(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connection, spName);

                // Asigna el valor proveido para estos parametros basado en el orden de los parametros
                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otro Caso podemos llamar el SP sin Parámetros
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un 1x1 resultset y no toma parametros) contra el SqlTransaction proveido
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <returns>Un objeto conteniendo el valor en el 1x1 resultset generado por el command</returns>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
        {

            return ExecuteScalar(transaction, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un 1x1 resultset) contra el SqlTransaction
        /// usando los parametros proveidos.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="commandParameters">Un vector de SqlParameters usado  para ejecutar el command</param>
        /// <returns>Un objeto conteniendo el valor en el 1x1 resultset generado por el command</returns>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Crea un command y prepara para ejecución
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            // Ejecuta el command y retorna los resultados
            object retval = cmd.ExecuteScalar();

            // Sepera los SqlParameters desde el objeto command, asi pueden ser usados de nuevo.
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// Ejectua un SP por un SqlCommand (que retorna un 1x1 resultset) contra el SqlTransaction especificado
        /// usando el valor parametro proveido.  este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna el valor basado en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o retorno parametro valor del SP
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, "GetOrderCount", 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="parameterValues">Un vector de objectos para ser asignado como los valores entrada del SP</param>
        /// <returns>Un objeto conteniendo el valor en el 1x1 resultset generado por el command</returns>
        public static object ExecuteScalar(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");


            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Asignar los valores proveidos para estos parametros basados en el orden de los parametros
                AssignParameterValues(commandParameters, parameterValues);


                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otro Caso podemos llamar el SP sin Parámetros
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteScalar

        #region Metodos para ExecuteXmlReader
        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset y no toma parametros) contra el SqlConnection proveido
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL using "FOR XML AUTO"</param>
        /// <returns>Un XmlReader conteniendo los resultset generados por el command</returns>
        public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText)
        {

            return ExecuteXmlReader(connection, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset) contra el SqlConnection especificado
        /// usando los parametros proveidos
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL using "FOR XML AUTO"</param>
        /// <param name="commandParameters">Un Vector de SqlParameters usado para ejecutar el command</param>
        /// <returns>Un XmlReader conteniendo los resultset generados por el command</returns>
        public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            bool mustCloseConnection = false;

            // Crea un command y lo prepara para ejecución
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

                // Crea el DataAdapter y Dataset
                XmlReader retval = cmd.ExecuteXmlReader();

                // Separa el SqlParameters desde el objeto command, asi puede ser utilizado de nuevo
                cmd.Parameters.Clear();

                return retval;
            }
            catch
            {
                if (mustCloseConnection)
                    connection.Close();
                throw;
            }
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un resultset) contra el SqlConnection especificado
        /// usando los valores parametros proveidos.  este metodo pregunta la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basados en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o retorno parametros valor del SP
        /// 
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(conn, "GetOrders", 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado using "FOR XML AUTO"</param>
        /// <param name="parameterValues">Un vector de objectos para ser asignado como los valores de entrada de los SP</param>
        /// <returns>Un XmlReader conteniendo los resultset generados por el command</returns>
        public static XmlReader ExecuteXmlReader(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");


            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connection, spName);

                // Asigna los valores proveidos por estos parametros basado en el orden de los parametros.
                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otro Caso podemos llamar el SP sin Parámetros
                return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset y no toma parametros) contra el SqlTransaction proveido
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL using "FOR XML AUTO"</param>
        /// <returns>Un XmlReader conteniendo los resultset generados por el command</returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {

            return ExecuteXmlReader(transaction, commandType, commandText, (SqlParameter[])null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset) contra el SqlTransaction especificado
        /// usando el parametro proveido
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL using "FOR XML AUTO"</param>
        /// <param name="commandParameters">Un Vector de SqlParameters usado para ejecutar el command</param>
        /// <returns>Un XmlReader conteniendo los resultset generados por el command</returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            //Crea un commando y lo prepara para ejecución
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            //Crea el DataAdapter y Dataset
            XmlReader retval = cmd.ExecuteXmlReader();

            // Separa los SqlParameters desde el objeto command, asi pueden ser usados de nuevo
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// Ejectua un SP por un SqlCommand (que retorna un resultset) contra el SqlTransaction especificado
        /// usando el valor parametro proveido. este metodo preguntará la base de datos para descubrir los parametros para el
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o retorno parametro valordel SP.
        /// 
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(trans, "GetOrders", 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="parameterValues">Un vector de objectos para ser asignado como los valores entrada de los SP</param>
        /// <returns>Un dataset conteniendo el resultset generado por el commmand</returns>
        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Asigna los valores proveidos para estos parametros basados en el orden de los parametros
                AssignParameterValues(commandParameters, parameterValues);


                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                // Otro Caso podemos llamar el SP sin Parámetros
                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion ExecuteXmlReader

        #region Metodos para FillDataset
        /// <summary>
        /// 
        /// Ejecuta un SqlCommand (que retorna un resultset y no toma parametros) contra la base de datos especificada en 
        /// la cadena de conexión.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="dataSet">Un dataset contendrá el resultset generado por el command</param>
        /// <param name="tableNames">Este vector sera usado para crear tablas mapeadas permitiendo que las DataTables sean referenciadas
        /// por un usuario de nombre definido (probablemente el nombre actual de la tabla)</param>
        public static void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (dataSet == null) throw new ArgumentNullException("dataSet");

            // crea y abre un SqlConnection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                FillDataset(connection, commandType, commandText, dataSet, tableNames);
            }
        }

        /// <summary>
        /// Ejectua un SqlCommand (que retorna un resultset) contra la base de datos especificada en la cadena de conexión
        /// usando los parametros proveidos.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="commandParameters">Un vector de SqlParameters usado para ejecutar el command</param>
        /// <param name="dataSet">Un dataset contendra el resultset generado por el command</param>
        /// <param name="tableNames">Este vector sera usado para crear tablas mapeadas permitiendo que las dataTables sean referenciadas
        /// por un usuario de nombre definido (probablemente el nombre actual de la tabla)
        /// </param>
        public static void FillDataset(string connectionString, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames,
            params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            // Crea y abre un SqlConnection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters);
            }
        }

        /// <summary>
        /// Ejectua un SP por un SqlCommand (que retorna un resultset) contra la base de datos especificado en 
        /// la cadena de conexión usando los valores parametros proveidos.  este metodo preguntará la base de datos para descubrir los parametros para el
        /// SP (la primera vez cada SP es llamado), y asigna los valores basados en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o retorno parametro valor del SP
        /// 
        /// e.g.:  
        ///  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, 24);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataSet">Un dataset contendra el resulset generado por el command</param>
        /// <param name="tableNames">Este vector sera usado para crear tablas mapeadas permitiendo que las dataTables sean referenciadas
        /// por un usuario de nombre definido (probablemente el nombre actual de la tabla)
        /// </param>    
        /// <param name="parameterValues">Un vector de objetos seran asignados como valores entrada de el SP</param>
        public static void FillDataset(string connectionString, string spName,
            DataSet dataSet, string[] tableNames,
            params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            //Crea y abre un SqlConnection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                FillDataset(connection, spName, dataSet, tableNames, parameterValues);
            }
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset y no toma parametros) contra la SqlConnection proveida.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(conn, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="dataSet">Un dataset contendra el resultset generado por el command</param>
        /// <param name="tableNames">Este vector sera usado para crear tablas mapeadas permitiendo que las dataTables sean referenciadas
        /// por un usuario de nombre definido (probablemente el nombre actual de la tabla)
        /// </param>    
        public static void FillDataset(SqlConnection connection, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames)
        {
            FillDataset(connection, commandType, commandText, dataSet, tableNames, null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resultset) contra el SqlConnection especificado
        /// usando los parametros proveidos
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(conn, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="dataSet">Un dataset contendra el resultset generado por el command</param>
        /// <param name="tableNames">Este vector sera usado para crear tablas mapeadas permitiendo que las dataTables sean referenciadas
        /// por un usuario de nombre definido (probablemente el nombre actual de la tabla)
        /// </param>
        /// <param name="commandParameters">Un vector de SqlParameters usado para ejecutar el command</param>

        public static void FillDataset(SqlConnection connection, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames,
            params SqlParameter[] commandParameters)
        {
            FillDataset(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        /// <summary>
        /// Ejecutando un SP por un SqlCommand (que retorna un resultset) contra el SqlConnection especificado
        /// usando el valor parametro proveido.  Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en el orden de los parametros
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o retorno parametro valor del SP
        /// 
        /// e.g.:  
        ///  FillDataset(conn, "GetOrders", ds, new string[] {"orders"}, 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataSet">Un dataset contendra el resultset generado por el command</param>
        /// <param name="tableNames">Este vector sera usado para crear tablas mapeadas permitiendo que las dataTables sean referenciadas
        /// por un usuario de nombre definido (probablemente el nombre actual de la tabla)
        /// </param>
        /// <param name="parameterValues">Un vector de objectos para ser asignado como valores entrada de el SP</param>

        public static void FillDataset(SqlConnection connection, string spName,
            DataSet dataSet, string[] tableNames,
            params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connection, spName);

                // Asigna los valores proveidos para estos parametros basado en el orden de los parametros
                AssignParameterValues(commandParameters, parameterValues);

                FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
            }
            else
            {
                // Otro Caso podemos llamar el SP sin Parámetros
                FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }
        }

        /// <summary>
        /// Ejecuta un SqlCommad (que retorna un resultset y no toma parametros) contra el SqlTransacción proveido
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="dataSet">Un dataset contendra el resultset generado por el command</param>
        /// <param name="tableNames">Este vector sera usado para crear tablas mapeadas permitiendo que las dataTables sean referenciadas
        /// por un usuario de nombre definido (probablemente el nombre actual de la tabla)
        /// </param>
        public static void FillDataset(SqlTransaction transaction, CommandType commandType,
            string commandText,
            DataSet dataSet, string[] tableNames)
        {
            FillDataset(transaction, commandType, commandText, dataSet, tableNames, null);
        }

        /// <summary>
        /// Ejecuta un SqlCommand (que retorna un resulset) contra el SqlTransacción especificado
        /// usando los parametros proveidos
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="dataSet">Un dataset contendra el resultset generado por el command</param>
        /// <param name="tableNames">Este vector sera usado para crear tablas mapeadas permitiendo que las dataTables sean referenciadas
        /// por un usuario de nombre definido (probablemente el nombre actual de la tabla)
        /// </param>
        /// <param name="commandParameters">Un vector de SqlParameters usado para ejecutar el command</param>

        public static void FillDataset(SqlTransaction transaction, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames,
            params SqlParameter[] commandParameters)
        {
            FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un resultset) contra el SqlTransaction especificado
        /// usando los valores parametro proveido.  Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Este metodo provee no acceso para parametros salida o retorno parametros valor del SP
        /// 
        /// e.g.:  
        ///  FillDataset(trans, "GetOrders", ds, new string[]{"orders"}, 24, 36);
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataSet">Un dataset contendra el resultset generado por el command</param>
        /// <param name="tableNames">Este vector sera usado para crear tablas mapeadas permitiendo que las dataTables sean referenciadas
        /// por un usuario de nombre definido (probablemente el nombre actual de la tabla)
        /// </param>
        /// <param name="parameterValues">Un vector de objectos para ser asignado como valores de entrada del SP</param>

        public static void FillDataset(SqlTransaction transaction, string spName,
            DataSet dataSet, string[] tableNames,
            params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");


            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Asigna los valores proveidos por estos parametros basado en el orden de los parametros
                AssignParameterValues(commandParameters, parameterValues);


                FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
            }
            else
            {
                // Otro Caso podemos llamar el SP sin Parámetros
                FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }
        }

        /// <summary>
        /// Metodo private helper que ejecuta un SqlCommand (que retorna un resultset) contra el SqlTransaccion y SqlConnection especificado
        /// usando los parametros proveidos.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(conn, trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido</param>
        /// <param name="transaction">Una SqlTransaction Valida</param>
        /// <param name="commandType">El CommandType (SP, Texto, etc.)</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="dataSet">Un dataset contendra el resultset generado por el command</param>
        /// <param name="tableNames">Este vector sera usado para crear tablas mapeadas permitiendo que las dataTables sean referenciadas
        /// por un usuario de nombre definido (probablemente el nombre actual de la tabla)
        /// </param>
        /// <param name="commandParameters">Un vector de SqlParameters usado para ejecutar el command</param>

        private static void FillDataset(SqlConnection connection, SqlTransaction transaction, CommandType commandType,
            string commandText, DataSet dataSet, string[] tableNames,
            params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (dataSet == null) throw new ArgumentNullException("dataSet");

            // Crea un command y lo prepara para ejecución
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            //Crea el DataAdapter y Dataset
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {

                // Adiciona la tabla mapeada especificada por el usuario
                if (tableNames != null && tableNames.Length > 0)
                {
                    string tableName = "Table";
                    for (int index = 0; index < tableNames.Length; index++)
                    {
                        if (tableNames[index] == null || tableNames[index].Length == 0) throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                        dataAdapter.TableMappings.Add(tableName, tableNames[index]);
                        tableName += (index + 1).ToString();
                    }
                }

                // Llena el Dataset usando valores por defecto para nombres Datatable, etc
                dataAdapter.Fill(dataSet);

                // Separa los SqlParameters desde el objeto command, asi prodran ser usados de nuevo
                command.Parameters.Clear();
            }

            if (mustCloseConnection)
                connection.Close();
        }
        #endregion

        #region Metodos para UpdateDataset
        /// <summary>
        /// Ejecuta el Respectivo command para cada insert, update, o delete de filas en el Dataset
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  UpdateDataset(conn, insertCommand, deleteCommand, updateCommand, dataSet, "Order");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="insertCommand">Una transacción valida SQL o SP para insercion de nuevos registros de la tabla</param>
        /// <param name="deleteCommand">Una transacción valida SQL o SP para borrado de registros de la tabla</param>
        /// <param name="updateCommand">Una transacción valida SQL o SP usado para actualizar registros de la tabla</param>
        /// <param name="dataSet">El dataset usado para actualizar los datos</param>
        /// <param name="tableName">El Datatable usado para actualizar los datos</param>

        public static void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataSet dataSet, string tableName)
        {
            if (insertCommand == null) throw new ArgumentNullException("insertCommand");
            if (deleteCommand == null) throw new ArgumentNullException("deleteCommand");
            if (updateCommand == null) throw new ArgumentNullException("updateCommand");
            if (tableName == null || tableName.Length == 0) throw new ArgumentNullException("tableName");

            // Crea un SqlDataAdapter
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {

                dataAdapter.UpdateCommand = updateCommand;
                dataAdapter.InsertCommand = insertCommand;
                dataAdapter.DeleteCommand = deleteCommand;

                //Actualiza el dataset 
                dataAdapter.Update(dataSet, tableName);

                // Commit
                dataSet.AcceptChanges();
            }
        }
        #endregion

        #region Metodos para CreateCommand
        /// <summary>
        /// Simplifica la creacion de un Sql objeto command(permitido)
        /// un SP y opcionales parametros para ser proveidos
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlCommand command = CreateCommand(conn, "AddCustomer", "CustomerID", "CustomerName");
        ///  
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido object</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="sourceColumns">Un vector de cadena para ser asignado como columnas de parametros del SP</param>
        /// <returns>Un objeto SqlCommand valido</returns>

        public static SqlCommand CreateCommand(SqlConnection connection, string spName, params string[] sourceColumns)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // Crea un SqlCommand
            SqlCommand cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;


            if ((sourceColumns != null) && (sourceColumns.Length > 0))
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connection, spName);

                for (int index = 0; index < sourceColumns.Length; index++)
                    commandParameters[index].SourceColumn = sourceColumns[index];


                AttachParameters(cmd, commandParameters);
            }

            return cmd;
        }
        #endregion

        #region Metodos para ExecuteNonQueryTypedParams
        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna no resultset) contra la base de datos especificada en
        /// la cadena de conexion usando los valores columnas dataRow como valores parametros del SP.
        /// Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en los valores de las filas.
        /// </summary>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los valores de los parametros del SP.</param>
        /// <returns>Un entero Representando el número de filas afectadas por el command</returns>
        public static int ExecuteNonQueryTypedParams(String connectionString, String spName, DataRow dataRow)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connectionString, spName);

                // Colocar los valores a los parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna no resultset) contra el SqlConnection especificado
        /// usando los valores columnas datarow como valores parametros del SP.
        /// Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en las filas.
        /// </summary>
        /// <param name="connection">Un SqlConnection Valido object</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los parametros del SP.</param>
        /// <returns>Un entero Representando el número de filas afectadas por el command</returns>
        public static int ExecuteNonQueryTypedParams(SqlConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connection, spName);

                // Colocar los valores de los parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna no resultset) contra el SqlTransaction especificado
        /// usando los valores columnas datarow como valores parametros del SP.
        /// Este metodo preguntará la base de datos para descubrir los parametros para el
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en las filas
        /// </summary>
        /// <param name="transaction">Una SqlTransaction Valida object</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los valores parametros del SP.</param>
        /// <returns>Un entero Representando el número de filas afectadas por el command</returns>
        public static int ExecuteNonQueryTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");


            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Coloca los valores a parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            }
        }
        #endregion

        #region Metodos para ExecuteDatasetTypedParams
        /// <summary>
        /// Ejecuta un SP por un SqlCommmand (que retorna un resultset) contra la base de datos especifica en 
        /// la cadena de conexión usando los valores columna datarow como valores parametros del SP
        /// Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es ejecutado),y asigna los valores basados en las filas
        /// </summary>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los valores parametro del SP.</param>
        /// <returns>Un dataset conteniendo el resultset generado por el commmand</returns>
        public static DataSet ExecuteDatasetTypedParams(string connectionString, String spName, DataRow dataRow)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connectionString, spName);

                // colocar los valores parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un resultset) contra el SqlConnection especificado
        /// usando los valores columna datarow como los valores parametros del SP
        /// Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado),y asigna el valor basado en las filas
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido object</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los valores del SP .</param>
        /// <returns>Un dataset conteniendo el resultset generado por el commmand</returns>
        public static DataSet ExecuteDatasetTypedParams(SqlConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connection, spName);

                // Colocar los valores a los parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteDataset(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommmand (que retorna un resultset) contra el SqlTransaction especificado
        /// usando el valor columna datarow como los valores parametros del SP
        /// Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en las filas.
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida object</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los valores parametro del SP.</param>
        /// <returns>Un dataset conteniendo el resultset generado por el commmand</returns>
        public static DataSet ExecuteDatasetTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Coloca los valores parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            }
        }

        #endregion

        #region Metodos para ExecuteReaderTypedParams
        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un resulset) contra la base de datos especifica en 
        /// la cadena de conexión usando los valores columna datarow como los valores parametro del SP
        /// Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en el orden de los parametros
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los valores parametros del SP</param>
        /// <returns>Un SqlDataReader conteniendo los resulset generados por el command</returns>
        public static SqlDataReader ExecuteReaderTypedParams(String connectionString, String spName, DataRow dataRow)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connectionString, spName);

                // Colocar los valores de los parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
            }
        }


        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un resultset) contra el SqlConnection especificado
        /// usando los valores columna datarow como valores parametros del SP
        /// Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido object</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los valores del SP.</param>
        /// <returns>Un SqlDataReader conteniendo los resulset generados por el command</returns>
        public static SqlDataReader ExecuteReaderTypedParams(SqlConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connection, spName);

                // Coloca los valores a los parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteReader(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un resultset) contra el SqlTransaction especificado 
        /// usando los valores columna datarow como los valores parametros del SP
        /// Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Un objecto SqlTransaction Valido</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los valores parametro del SP.</param>
        /// <returns>Un SqlDataReader conteniendo los resulset generados por el command</returns>
        public static SqlDataReader ExecuteReaderTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Coloca los valores a los parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            }
        }
        #endregion

        #region Metodos para ExecuteScalarTypedParams
        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un 1x1 resultset) contra la base de datos especifica en 
        /// la cadena de conexión usando los valores columna datarow como valores parametros del SP
        /// Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en el orden de los parametros
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los valores parametro del SP.</param>
        /// <returns>Un objeto conteniendo el valor en el 1x1 resultset generado por el command</returns>
        public static object ExecuteScalarTypedParams(String connectionString, String spName, DataRow dataRow)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connectionString, spName);

                // Coloca los valores a los parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un 1x1 resultset) contra el SqlConnection especificado
        /// usando los valores columna datarow como valores parametros del SP
        /// Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basao en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido object</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los valores parametro del SP.</param>
        /// <returns>Un objeto conteniendo el valor en el 1x1 resultset generado por el command</returns>
        public static object ExecuteScalarTypedParams(SqlConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connection, spName);

                // Coloca los valores a los parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un 1x1 resultset) contra el SqlTransaction especificada
        /// usando los valores columna datarow comom los valores parametros del SP.
        /// Este metodo preguntara la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basados en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Un objeto SqlTransaction Valido</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los valores parametro del SP.</param>
        /// <returns>Un objeto conteniendo el valor en el 1x1 resultset generado por el command</returns>
        public static object ExecuteScalarTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Coloca los valores a los parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            }
        }
        #endregion

        #region Metodos para ExecuteXmlReaderTypedParams
        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un resultset) contra el SqlConnection especificado
        /// usando el valores column datarow como valores parametros del SP
        /// Este metodo preguntará la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido object</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los valores parametro del SP.</param>
        /// <returns>Un XmlReader conteniendo los resultset generados por el command</returns>
        public static XmlReader ExecuteXmlReaderTypedParams(SqlConnection connection, String spName, DataRow dataRow)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(connection, spName);

                // Coloca los valores a los parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
            }
        }

        /// <summary>
        /// Ejecuta un SP por un SqlCommand (que retorna un resultset) contra el SqlTransaction especificado 
        /// usando los valores columna datarow como valores parametros del SP
        /// Este metodo preguntara la base de datos para descubrir los parametros para el 
        /// SP (la primera vez cada SP es llamado), y asigna los valores basado en el orden de los parametros.
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="transaction">Una SqlTransaction Valida object</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="dataRow">El datarow usado para tener los valores parametro del SP.</param>
        /// <returns>Un XmlReader conteniendo los resultset generados por el command</returns>
        public static XmlReader ExecuteXmlReaderTypedParams(SqlTransaction transaction, String spName, DataRow dataRow)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            if (dataRow != null && dataRow.ItemArray.Length > 0)
            {
                // Sacar los parametros para este SP desde el parametro cache (o descubrilos y poblar el cache)
                SqlParameter[] commandParameters = AccesoSQLParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Coloca los valores a los parametros
                AssignParameterValues(commandParameters, dataRow);

                return AccesoSQL.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            else
            {
                return AccesoSQL.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
            }
        }
        #endregion

    }

    /// <summary>
    /// AccesoSQLParameterCache provee funciones para una cache static de parametros procedimiento, y la 
    /// habilidad para descubrir parametros para SP en tiempo de ejecución.
    /// </summary>
    /// <remarks>
    /// Autor: José Faustino Posas
    /// URL: http://www.consultinltda.com
    /// Email: consultinltda@hotmail.com
    /// Fecha: 2006-03-29
    /// </remarks>

    public sealed class AccesoSQLParameterCache
    {
        #region Metodos privados, variables y constructores

        //Desde esta clase se provee solo metodos estaticos, hace el private constructor por defecto para prevenir
        //instancias de inicio creados con "new AccesoSQLParameterCache()"
        private AccesoSQLParameterCache() { }

        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Resuelve en tiempo de ejecucion apropiada SqlParameters para un SP
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un SqlConnection Valido object</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="includeReturnValueParameter">Sea que o no incluyen retorno valor parametro</param>
        /// 
        /// <returns>El vector parametro descubierto.</returns>
        private static SqlParameter[] DiscoverSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            SqlCommand cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlCommandBuilder.DeriveParameters(cmd);
            connection.Close();

            if (!includeReturnValueParameter)
            {
                cmd.Parameters.RemoveAt(0);
            }

            SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];

            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // Inicializa los parametros con un valor DBNull
            foreach (SqlParameter discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }
            return discoveredParameters;
        }

        /// <summary>
        ///  Copia de cache vector SqlParameter
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="originalParameters"></param>
        /// <returns></returns>
        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion private methods, variables, and constructors

        #region Mecanismos de cache

        /// <summary>
        /// Adiciona vector parametro para la cache
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <param name="commandParameters">Un vector de SqlParameters para ser cacheado</param>

        public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        /// <summary>
        /// Recuperar un vector parametro desde la cache
        /// </summary>
        /// <remarks>
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="commandText">El nombre de el Procedimiento Almacenado o comando T-SQL</param>
        /// <returns>Un vector de SqlParameters</returns>
        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            SqlParameter[] cachedParameters = paramCache[hashKey] as SqlParameter[];
            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }

        #endregion caching functions

        #region Funciones para el descubrimiento de parametros

        /// <summary>
        /// Recupera los apropiados Sqlparameters para los SP
        /// </summary>
        /// <remarks>
        /// Este metodo pregunta la base de datos por esta informacion, y la almacena en cache para futuras solicitudes.
        ///
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <returns>Un vector de SqlParameters</returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        /// <summary>
        /// Recupera los SqlParameters apropiados paa el SP
        /// </summary>
        /// <remarks>
        /// Este metodo pregunta la base de datos por esta informacion, y la almacena en cache para futuras solicitudes.
        /// 
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connectionString">Una Cadena de Conexión valida para un SqlConnection</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="includeReturnValueParameter">Un valor bool indicando que parametro podria ser incluido en los resultados</param>
        /// <returns>Un vector de SqlParameters</returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>
        /// recupera los SqlParameters apropiados para el SP
        /// </summary>
        /// <remarks>
        /// Este metodo pregunta la base de datos por esta información, y almacena en cache para futuras solicitudes.
        /// 
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un objecto valido SqlConnection</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <returns>Un vector de Sqlparameters</returns>
        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName)
        {
            return GetSpParameterSet(connection, spName, false);
        }

        /// <summary>
        /// Recupera los SqlParameters apropidados para el SP
        /// </summary>
        /// <remarks>
        /// Este metodo pregunta la base de datos por esta información, y la almacena en cache para futuras solicitudes.
        /// 
        /// Autor: José Faustino Posas
        /// URL: http://www.consultinltda.com
        /// Email: consultinltda@hotmail.com
        /// Fecha: 2006-03-29
        /// </remarks>
        /// <param name="connection">Un objecto valido SqlConnection</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
        /// <returns>An array of SqlParameters</returns>
        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            using (SqlConnection clonedConnection = (SqlConnection)((ICloneable)connection).Clone())
            {
                return GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <param name="connection">Un SqlConnection Valido object</param>
        /// <param name="spName">El nombre del Procedimiento Almacenado</param>
        /// <param name="includeReturnValueParameter">Un valor bool indicando que parametro podria ser incluido en los resultados</param>
        /// <returns>Un vector de SqlParameters</returns>
        private static SqlParameter[] GetSpParameterSetInternal(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            string hashKey = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

            SqlParameter[] cachedParameters;

            cachedParameters = paramCache[hashKey] as SqlParameter[];
            if (cachedParameters == null)
            {
                SqlParameter[] spParameters = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                paramCache[hashKey] = spParameters;
                cachedParameters = spParameters;
            }

            return CloneParameters(cachedParameters);
        }

        #endregion Parameter Discovery Functions

    }
}
