/* ----------------------------------
  Procedimiento para configurar el certificado de seguridad en .NET
 ------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Security.Cryptography.Pkcs;
using System.IO;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;

namespace Ssoft.Security
{
    /// <summary>
    /// Metodos para el manejo de certificados de seguridad
    /// </summary>
    ///<remarks>
    /// Autor:          José Faustino Posas
    /// Company:        Ssoft Colombia
    /// Fecha:          2012-01-16
    /// -------------------
    /// Control de Cambios
    /// -------------------
    /// Autor:          
    /// Fecha:         
    /// Descripción:    
    /// </remarks>
    public class clsCertificate
    {
        /// <summary>
        /// Metodo para abrir el certificado de seguridad
        /// </summary>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-01-16
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public string setCertificateOpen(string sMensaje)
        {
            string sResponse = string.Empty;
            try
            {
                
                clsSerializer cSerializer = new clsSerializer();
                string sDocumento = "eServices_vasaca.crt";
                string sFile = clsValidaciones.XMLDatasetCreaGen() + sDocumento;

                string sFileDes = clsValidaciones.XMLDatasetCreaGen() + sDocumento;
                string sDocumentoToken = cSerializer.RecuperaFileGen(sFileDes);

                X509Certificate2 objCertGen = new X509Certificate2(sFile);

                string sCerticicate = setCertificateWriten(objCertGen);
                string sCertificateValid = setCertificateValidation(objCertGen);
                byte[] sCertificateFirma = setCertificateFirma(objCertGen, sMensaje);

                byte[] sCertificateFirmaValid = setCertificateFirma(objCertGen, sDocumentoToken);
                try
                {
                    string sConvertCert = Convert.ToBase64String(sCertificateFirma);
                    string sConvertCertValid = Convert.ToBase64String(sCertificateFirmaValid);
                }
                catch { }
                byte[] sCertificateFirmaGen = setCertificateFirmaCreate(objCertGen, sMensaje);
                byte[] sCertificateFirmaGenValid = setCertificateFirmaCreate(objCertGen, sDocumentoToken);
                try
                {
                    string sCert1 = setCertificateFirmaValidate(sCertificateFirmaGen, sMensaje);
                    string sCert1Valid = setCertificateFirmaValidate(sCertificateFirmaGenValid, sDocumentoToken);
                }
                catch { }
                byte[] sCertificateFirmaGenUltimo = setCertificateFirmaEncrypted(objCertGen, sMensaje);
                byte[] sCertificateFirmaGenUltimoValid = setCertificateFirmaEncrypted(objCertGen, sDocumentoToken);
                try
                {
                    string sConvertCertGen = Convert.ToBase64String(sCertificateFirmaGenUltimo);
                    sResponse = sConvertCertGen;

                    byte[] sCertificateFirmaDes = setCertificateFirmaEncryptedDecode(sCertificateFirmaGenUltimo);
                    string sDecode = Encoding.ASCII.GetString(sCertificateFirmaDes);
                }
                catch { }
                try
                {
                    string sConvertCertGenValid = Convert.ToBase64String(sCertificateFirmaGenUltimoValid);

                    byte[] sCertificateFirmaDesValid = setCertificateFirmaEncryptedDecode(sCertificateFirmaGenUltimoValid);
                    string sDecodeValid = Encoding.ASCII.GetString(sCertificateFirmaDesValid);
                }
                catch { }
                //Token tkn = new Token();
                //sResponse = tkn.TokenGenerate(sMensaje, objCertGen);


                //X509Store objStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                //objStore.Open(OpenFlags.ReadOnly);
                //foreach (X509Certificate2 objCert in objStore.Certificates)
                //    Debug.Print(objCert.SubjectName.Name + ": " + objCert.Thumbprint);
                //objStore.Close();
            }
            catch { }
            return sResponse;
        }
        /// <summary>
        /// Metodo para escribir el certificado de seguridad
        /// </summary>
        /// <param name="xCertificate"></param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-01-16
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public string setCertificateWriten(X509Certificate2 xCertificate)
        {
            string sCertificate = null;
            try
            {
                X509Certificate2 objCert = xCertificate; //Acá tenemos que poner el certificado
                StringBuilder objSB = new StringBuilder("Detalle del certificado: \n\n");
                SymmetricAlgorithm algoritmo = SymmetricAlgorithm.Create("Rijndael");
                
                //Detalle
                objSB.AppendLine("Persona = " + objCert.Subject);
                objSB.AppendLine("Emisor = " + objCert.Issuer);
                objSB.AppendLine("Válido desde = " + objCert.NotBefore.ToString());
                objSB.AppendLine("Válido hasta = " + objCert.NotAfter.ToString());
                objSB.AppendLine("Tamaño de la clave = " + objCert.PublicKey.Key.KeySize.ToString());
                objSB.AppendLine("Valor de la clave = " + objCert.GetPublicKeyString().ToString());
                objSB.AppendLine("Algoritmo de la clave = " + objCert.GetKeyAlgorithm().ToString());
                objSB.AppendLine("Número de serie = " + objCert.SerialNumber);
                objSB.AppendLine("Hash = " + objCert.Thumbprint);
                //Extensiones
                objSB.AppendLine("\nExtensiones:\n");
                foreach (X509Extension objExt in objCert.Extensions)
                {
                    objSB.AppendLine(objExt.Oid.FriendlyName + " (" + objExt.Oid.Value + ')');
                    if (objExt.Oid.FriendlyName == "Key Usage")
                    {
                        X509KeyUsageExtension ext = (X509KeyUsageExtension)objExt;
                        objSB.AppendLine("    " + ext.KeyUsages);
                    }
                    if (objExt.Oid.FriendlyName == "Basic Constraints")
                    {
                        X509BasicConstraintsExtension ext = (X509BasicConstraintsExtension)objExt;
                        objSB.AppendLine("    " + ext.CertificateAuthority);
                        objSB.AppendLine("    " + ext.HasPathLengthConstraint);
                        objSB.AppendLine("    " + ext.PathLengthConstraint);
                    }
                    if (objExt.Oid.FriendlyName == "Subject Key Identifier")
                    {
                        X509SubjectKeyIdentifierExtension ext = (X509SubjectKeyIdentifierExtension)objExt;
                        objSB.AppendLine("    " + ext.SubjectKeyIdentifier);
                    }
                    if (objExt.Oid.FriendlyName == "Enhanced Key Usage") //2.5.29.37
                    {
                        X509EnhancedKeyUsageExtension ext = (X509EnhancedKeyUsageExtension)objExt;
                        OidCollection objOids = ext.EnhancedKeyUsages;
                        foreach (Oid oid in objOids)
                            objSB.AppendLine("    " + oid.FriendlyName + " (" + oid.Value + ')');
                    }
                }
                sCertificate = objSB.ToString();
                //Debug.Print(objSB.ToString());
            }
            catch { }
            return sCertificate;
        }
        /// <summary>
        /// Metodo para validar el certificado de seguridad
        /// </summary>
        /// <param name="xCertificate"></param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-01-16
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public string setCertificateValidation(X509Certificate2 xCertificate)
        {
            StringBuilder objSB = new StringBuilder("Validation del certificado: \n\n");
            try
            {
                X509Certificate2 objCert = xCertificate; //Acá tenemos que poner el certificado
                X509Chain objChain = new X509Chain();
                //Verifico toda la cadena de revocación
                objChain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                objChain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                //Timeout para las listas de revocación
                objChain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 0, 30);
                //Verificar todo
                objChain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;
                //Se puede cambiar la fecha de verificación
                //objChain.ChainPolicy.VerificationTime = new DateTime(1999, 1, 1);
                objChain.Build(objCert);
                if (objChain.ChainStatus.Length != 0)
                    foreach (X509ChainStatus objChainStatus in objChain.ChainStatus)
                        objSB.AppendLine(objChainStatus.Status.ToString() + " - " + objChainStatus.StatusInformation);
                else
                    objSB.AppendLine("OK");
            }
            catch { }
            return objSB.ToString();
        }
        /// <summary>
        /// Metodo para firmar el certificado de seguridad
        /// </summary>
        /// <param name="xCertificate"></param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-01-16
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public byte[] setCertificateFirma(X509Certificate2 xCertificate, string sMensaje)
        {
            try
            {
                X509Certificate2 objCert = xCertificate; //Acá tenemos que poner el certificado
                //Creamos el ContentInfo
                ContentInfo objContent = new ContentInfo(Encoding.ASCII.GetBytes(sMensaje));
                //Creamos el objeto que representa los datos firmados
                SignedCms objSignedData = new SignedCms(objContent);
                //Creamos el "firmante"
                CmsSigner objSigner = new CmsSigner(objCert);
                //Firmamos los datos
                objSignedData.ComputeSignature(objSigner);
                //Obtenemos el resultado
                byte[] bytSigned = objSignedData.Encode();
                return bytSigned;
                //string sFirma = "Documento con firma: " + Convert.ToBase64String(bytSigned);
            }
            catch { return null; }
        }
        /// <summary>
        /// Metodo para crear la firma del certificado de seguridad
        /// </summary>
        /// <param name="xCertificate"></param>
        /// <returns></returns>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-01-16
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public byte[] setCertificateFirmaCreate(X509Certificate2 xCertificate, string sMensaje)
        {
            try
            {
                X509Certificate2 objCert = xCertificate; //Acá tenemos que poner el certificado
                //Creamos el ContentInfo
                ContentInfo objContent = new ContentInfo(Encoding.ASCII.GetBytes(sMensaje));
                //Creamos el objeto que representa los datos firmados
                SignedCms objSignedData = new SignedCms(objContent);
                objSignedData.Certificates.Add(objCert);
                //Creamos el "firmante"
                CmsSigner objSigner = new CmsSigner(objCert);
                //Firmamos los datos
                objSignedData.ComputeSignature(objSigner);
                //Obtenemos el resultado
                byte[] bytSigned = objSignedData.Encode();
                return bytSigned;
                //string sFirma = "Firma digital: " + Convert.ToBase64String(bytSigned);
            }
            catch { return null; }
        }
        /// <summary>
        /// Metodo para validar el certificado de seguridad
        /// </summary>
        /// <param name="bytSigned"></param>
        /// <param name="sDatosFirma"></param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-01-16
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public string setCertificateFirmaValidate(byte[] bytSigned, string sDatosFirma)
        {
            string sFirma = "Error - La firma no concuerda con los datos";
            try
            {
                byte[] bytFirma = bytSigned; //Acá tenemos que poner la firma digital calculada en el ejemplo anterior
                //ContentInfo objContent = new ContentInfo(Encoding.ASCII.GetBytes("Scientia Soluciones Informáticas, la mejor consultora de desarrollo"));
                ContentInfo objContent = new ContentInfo(Encoding.ASCII.GetBytes(sDatosFirma));
                SignedCms objDatos = new SignedCms(objContent, true);
                //Deserealizamos la firma
                objDatos.Decode(bytFirma);
                try
                {
                    //Verificamos si la firma concuerda con los datos
                    objDatos.CheckSignature(true);
                    sFirma = "Ok - La firma concuerda con los datos";
                }
                catch
                {
                }
            }
            catch { }
            return sFirma;
        }
        /// <summary>
        /// Metodo para validar el documento del certificado de seguridad
        /// </summary>
        /// <param name="bytSignedDoc"></param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-01-16
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public string setCertificateFirmaValidateDocument(byte[] bytSignedDoc)
        {
            string sContent = string.Empty;
            try
            {
                byte[] bytDocFirmado = bytSignedDoc; //Acá tenemos que poner el documento firmado obtenido en el ejemplo anterior
                SignedCms objDatos = new SignedCms();
                //Deserializo los bytes PKCS#7
                objDatos.Decode(bytDocFirmado);
                //Verifico la firma y obtengo el documento
                try
                {
                    objDatos.CheckSignature(true);
                    sContent = Encoding.ASCII.GetString(objDatos.ContentInfo.Content);
                    //Debug.Print("Ok - La firma concuerda con los datos");
                }
                catch
                {
                    //Debug.Print("Error - La firma no concuerda con los datos");
                }
            }
            catch { }
            return sContent;
        }
        /// <summary>
        /// Metodo para encriptar el certificado de seguridad
        /// </summary>
        /// <param name="xCertificate"></param>
        /// <param name="sDatosFirma"></param>
        /// <returns></returns>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-01-16
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public byte[] setCertificateFirmaEncrypted(X509Certificate2 xCertificate, string sDatosFirma)
        {
            try
            {
                X509Certificate2 objCert = xCertificate; //Acá tenemos que poner el certificado
                //Creamos el ContentInfo
                //ContentInfo objContent = new ContentInfo(Encoding.ASCII.GetBytes("Scientia Soluciones Informáticas, la mejor consultora de desarrollo"));
                ContentInfo objContent = new ContentInfo(Encoding.ASCII.GetBytes(sDatosFirma));
                //Creamos el objeto que representa los datos firmados
                EnvelopedCms objEncryptedData = new EnvelopedCms(objContent);
                
                //Creamos el destino
                CmsRecipient objRecipient = new CmsRecipient(objCert);
                //Firmamos los datos
                objEncryptedData.Encrypt(objRecipient);
                //Datos encriptados
                byte[] bytResult = objEncryptedData.Encode();
                return bytResult;
                //Debug.Pring("Datos encriptados:  " + Convert.ToBase64String(bytResult));
            }
            catch { return null; }
        }
        /// <summary>
        /// Metodo para desencriptar el certificado de seguridad
        /// </summary>
        /// <param name="bytDatos"></param>
        /// <returns></returns>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-01-16
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public byte[] setCertificateFirmaEncryptedDecode(byte[] bytDatos)
        {
            try
            {
                EnvelopedCms objEncryptedData = new EnvelopedCms();
                //Leemos los datos encriptados
                objEncryptedData.Decode(bytDatos);
                //Desencriptamos los datos
                objEncryptedData.Decrypt();
                //Documento original
                byte[] bytDoc = objEncryptedData.ContentInfo.Content;
                return bytDoc;
                //Mostramos el resultado
                //Debug.Print("Datos desencriptados: " + Encoding.ASCII.GetString(bytDoc));
            }
            catch { return null; }
        }
        public string Cifrar(Byte modo, string cadena)
        {
            try
            {
                Byte[] plaintext;
                string VecI = "20270430";
                Byte Algoritmo = 3;
                string key = "Visa-Preferida";
                /*** Procedimiento de cifrado ***/
                //encriptar
                if (modo == 1)
                {
                    plaintext = Encoding.ASCII.GetBytes(cadena);
                }
                else
                {
                    //desencriptar
                    plaintext = Convert.FromBase64String(cadena);
                }

                Byte[] keys = Encoding.ASCII.GetBytes(key);
                MemoryStream memdata = new MemoryStream();
                ICryptoTransform transforma = null;

                switch (Algoritmo)
                {
                    case 1:
                        /*** DES ***/
                        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                        des.Mode = CipherMode.CBC;
                        des.Padding = PaddingMode.PKCS7;
                        if (modo == 1)
                        {
                            transforma = des.CreateEncryptor(keys, Encoding.ASCII.GetBytes(VecI));
                        }
                        else
                        {
                            if (modo == 2)
                            {
                                transforma = des.CreateDecryptor(keys, Encoding.ASCII.GetBytes(VecI));
                            }
                            else
                            {
                                transforma = des.CreateDecryptor(keys, Encoding.ASCII.GetBytes(VecI));
                            }
                        }
                        break;
                    case 2:
                        /*** TripleDES ***/
                        TripleDESCryptoServiceProvider des3 = new TripleDESCryptoServiceProvider();
                        des3.Mode = CipherMode.CBC;
                        des3.Padding = PaddingMode.PKCS7;
                        if (modo == 1)
                        {
                            transforma = des3.CreateEncryptor(keys, Encoding.ASCII.GetBytes(VecI));
                        }
                        else
                        {
                            if (modo == 2)
                            {
                                transforma = des3.CreateDecryptor(keys, Encoding.ASCII.GetBytes(VecI));
                            }
                            else
                            {
                                transforma = des3.CreateDecryptor(keys, Encoding.ASCII.GetBytes(VecI));
                            }
                        }
                        break;
                    case 3:
                        /*** RC2 ***/
                        RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
                        rc2.Mode = CipherMode.CBC;
                        rc2.Padding = PaddingMode.PKCS7;
                        if (modo == 1)
                        {
                            transforma = rc2.CreateEncryptor(keys, Encoding.ASCII.GetBytes(VecI));
                        }
                        else
                        {
                            if (modo == 2)
                            {
                                transforma = rc2.CreateDecryptor(keys, Encoding.ASCII.GetBytes(VecI));
                            }
                            else
                            {
                                transforma = rc2.CreateDecryptor(keys, Encoding.ASCII.GetBytes(VecI));
                            }
                        }
                        break;
                    case 4:
                        /*** Rijndael ***/
                        RijndaelManaged rj = new RijndaelManaged();
                        rj.Mode = CipherMode.CBC;
                        rj.Padding = PaddingMode.PKCS7;
                        if (modo == 1)
                        {
                            transforma = rj.CreateEncryptor(keys, Encoding.ASCII.GetBytes(VecI));
                        }
                        else
                        {
                            if (modo == 2)
                            {
                                transforma = rj.CreateDecryptor(keys, Encoding.ASCII.GetBytes(VecI));
                            }
                            else
                            {
                                transforma = rj.CreateDecryptor(keys, Encoding.ASCII.GetBytes(VecI));
                            }
                        }
                        break;
                }

                CryptoStream encstream = new CryptoStream(memdata, transforma, CryptoStreamMode.Write);
                encstream.Write(plaintext, 0, plaintext.Length);
                encstream.FlushFinalBlock();
                encstream.Close();

                if (modo == 1)
                {
                    cadena = Convert.ToBase64String(memdata.ToArray());
                }
                else
                {
                    if (modo == 2)
                    {
                        cadena = Encoding.ASCII.GetString(memdata.ToArray());
                    }
                    else
                    {
                        cadena = Convert.ToBase64String(memdata.ToArray());
                    }
                }
                return cadena;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    /// <summary>
    /// Metodos para el manejo de token de seguridad
    /// </summary>
    ///<remarks>
    /// Autor:          José Faustino Posas
    /// Company:        Ssoft Colombia
    /// Fecha:          2012-01-16
    /// -------------------
    /// Control de Cambios
    /// -------------------
    /// Autor:          
    /// Fecha:         
    /// Descripción:    
    /// </remarks>
    public class clsToken
    {
        public string CodificarCompleto(string sMensaje)
        {
            string sDocumento = "aviacert.pfx";
            string spassword = clsValidaciones.GetKeyOrAdd("sLlaveToken", "AviaMk");
            string srutaCertificado = clsValidaciones.XMLDatasetCreaGen() + sDocumento;

            string sResponse = CodificarCompleto(sMensaje, srutaCertificado, spassword);
            return sResponse;
        }
        public string CodificarCompleto(string sMensaje, string srutaCertificado, string spassword)
        {
            string sText = string.Empty;
            try
            {
                //abrir el certificado
                X509Certificate2 cert = new X509Certificate2(srutaCertificado, spassword);

                //armar el contenido
                byte[] data = System.Text.Encoding.UTF8.GetBytes(sMensaje);
                ContentInfo info = new ContentInfo(data);
                //preparar la firma
                SignedCms cms = new SignedCms(info, false);
                CmsSigner signer = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, cert);

                //firmo
                cms.ComputeSignature(signer);

                //codifico
                data = cms.Encode();

                //Convertir a BASE 64 con saltos de linea
                sText = System.Convert.ToBase64String(data, Base64FormattingOptions.InsertLineBreaks);

                //agregar header y trailer
                sText = "-----BEGIN PKCS7-----\r\n" + sText + "\r\n-----END PKCS7-----";
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Error al cifrar";
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ExceptionHandled.Publicar(cParametros);
            }
            return sText;
        }
        public string DecodificarCompleto(string textoEntrada)
        {

            //quitamos caracteres de salto de linea
            string text = textoEntrada.Replace("\n", "");
            text = text.Replace("\r", "");
            string infoS = string.Empty;

            //decodificamos HTTP
            //text = Uri.UnescapeDataString(text);
            //text = HttpUtility.UrlDecode(text);
            try
            {
                //Quitamos encabezado y final
                text = text.Replace("-----BEGIN PKCS7-----", "");
                text = text.Replace("-----END PKCS7-----", "");

                //transformamos de base 64 a binario
                byte[] data = System.Convert.FromBase64String(text);

                //creamos el SignedCMS y decodificamos
                SignedCms cms = new SignedCms();
                cms.Decode(data);

                //aqui podemos mostrar informacion si queremos verificar la firma
                /*
                SignerInfoEnumerator enu = cms.SignerInfos.GetEnumerator();
                while (enu.MoveNext())
                {
                    SignerInfo si = enu.Current;
                    Console.Out.WriteLine(si.Certificate);

                }*/


                //tomamos el contenido y lo mostramos
                byte[] info = cms.ContentInfo.Content;
                infoS = System.Text.Encoding.UTF8.GetString(info);
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Error al descifrar";
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ExceptionHandled.Publicar(cParametros);
            }
            return infoS;
        }
    }
}
