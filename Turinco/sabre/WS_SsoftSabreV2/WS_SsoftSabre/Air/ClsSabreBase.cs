using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace WS_SsoftSabre.Air
{
    abstract class ClsSabreBase
    {
        /// <summary>
        /// This is the method that will be implemented in each SWS class 
        /// </summary>
        /// <param name="vo_SwsRequestInfo">Parameters required for the SWS class</param>
        /// <returns></returns>
        public abstract object getExecuteSWS(params object[] vo_SwsRequestInfo);

        /// <summary>
        /// This is the template method wich is contains the general code (flow)
        /// for SWS classes
        /// </summary>
        /// <param name="vo_SwsRequestInfo">Parameters required for the SWS class</param>
        /// <returns></returns>
        public object setExecuteSWS(params object[] vo_SwsRequestInfo)
        {
            try
            {
                object swsResponse = getExecuteSWS(vo_SwsRequestInfo);
                setValidateSWSResponse(swsResponse);
                return swsResponse;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message, Ex);
            }
        }

        /// <summary>
        /// For invoking Sabre Webservices, it's requiered to add to the request the atribute
        /// MessageHeader which is the same for all SWS classes. This method dinamically loads this atribute
        /// by usgin reflexion.
        /// </summary>
        /// <param name="typeMessageHeader">Type of class expected</param>
        /// <param name="vo_MessageHeader">Parameters to fill the MessageHeader object</param>
        /// <returns></returns>
        internal object getMessageHeader(Type typeMessageHeader, VO_MessageHeader vo_MessageHeader)
        {
            object messageHeader = Activator.CreateInstance(typeMessageHeader);
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            //MessageData
            DateTime dtmDate = DateTime.UtcNow;
            string strDate = dtmDate.ToString("s") + "Z";

            PropertyInfo propertyInfoMessageData = messageHeader.GetType().GetProperty("MessageData");
            Type typeMessageData = assembly.GetType(propertyInfoMessageData.PropertyType.FullName);
            object messageData = Activator.CreateInstance(typeMessageData);

            messageData.GetType().GetProperty("Timestamp").SetValue(messageData, strDate, null);
            messageData.GetType().GetProperty("MessageId").SetValue(messageData, vo_MessageHeader.StrMessageId, null);

            //Service
            System.Reflection.PropertyInfo propertyInfoService = messageHeader.GetType().GetProperty("Service");
            Type typeService = assembly.GetType(propertyInfoService.PropertyType.FullName);
            object service = Activator.CreateInstance(typeService);
            service.GetType().GetProperty("Value").SetValue(service, vo_MessageHeader.StrValue, null);
            messageHeader.GetType().GetProperty("Service").SetValue(messageHeader, service, null);

            messageHeader.GetType().GetProperty("MessageData").SetValue(messageHeader, messageData, null);
            messageHeader.GetType().GetProperty("CPAId").SetValue(messageHeader, vo_MessageHeader.StrCPAId, null);
            messageHeader.GetType().GetProperty("Action").SetValue(messageHeader, vo_MessageHeader.StrAction, null);

            //From
            System.Reflection.PropertyInfo propertyInfoFrom = messageHeader.GetType().GetProperty("From");
            Type typeFrom = assembly.GetType(propertyInfoFrom.PropertyType.FullName);
            object from = Activator.CreateInstance(typeFrom);

            //PartyIdFrom
            PropertyInfo propertyInfoPartyID = from.GetType().GetProperty("PartyId");
            Type typeFromPartyIds = assembly.GetType(propertyInfoPartyID.PropertyType.FullName);
            Array partyIdsAuxiliar = Array.CreateInstance(typeFromPartyIds, 1);
            Type typeFromPartyId = typeFromPartyIds.GetElementType();
            Array partyIds = Array.CreateInstance(typeFromPartyId, 1);

            object fromPartyId = Activator.CreateInstance(typeFromPartyId);
            fromPartyId.GetType().GetProperty("Value").SetValue(fromPartyId, vo_MessageHeader.StrTo, null);
            partyIds.SetValue(fromPartyId, 0);
            from.GetType().GetProperty("PartyId").SetValue(from, partyIds, null);
            messageHeader.GetType().GetProperty("From").SetValue(messageHeader, from, null);

            //To
            System.Reflection.PropertyInfo propertyInfoTo = messageHeader.GetType().GetProperty("To");
            Type typeTo = assembly.GetType(propertyInfoTo.PropertyType.FullName);
            object to = Activator.CreateInstance(typeTo);

            //PartyIdTo
            partyIds = Array.CreateInstance(typeFromPartyId, 1);
            fromPartyId = Activator.CreateInstance(typeFromPartyId);
            fromPartyId.GetType().GetProperty("Value").SetValue(fromPartyId, vo_MessageHeader.StrTo, null);
            partyIds.SetValue(fromPartyId, 0);
            to.GetType().GetProperty("PartyId").SetValue(to, partyIds, null);
            messageHeader.GetType().GetProperty("To").SetValue(messageHeader, to, null);

            //ConversationId
            messageHeader.GetType().GetProperty("ConversationId").SetValue(messageHeader, vo_MessageHeader.StrConversationId, null);

            return messageHeader;
        }

        /// <summary>
        /// Validate if the SWS response contains error.
        /// </summary>
        /// <param name="swsResponse">SWS Response</param>
        private void setValidateSWSResponse(object swsResponse)
        {
            PropertyInfo status = swsResponse.GetType().GetProperty("status");
            ///the only objects with "status" property are SessionCreateRQ and
            ///SessionClose
            ///status == null, means there are some errors
            if (status == null)
            {
                //ErrorRS is just an attibute for sabrecommand
                PropertyInfo errorRS = swsResponse.GetType().GetProperty("ErrorRS");

                if (errorRS == null)
                {
                    PropertyInfo success = swsResponse.GetType().GetProperty("Success");
                    object successValue = success.GetValue(swsResponse, null);

                    if (successValue == null)
                    {
                        object errors = swsResponse.GetType().GetProperty("Errors").GetValue(swsResponse, null);
                        string strErrorMessage = String.Empty;

                        if (errors.GetType().IsArray)
                        {
                            Array array = errors as Array;
                            StringBuilder strErrorMessages = new StringBuilder();

                            foreach (var error in array)
                            {
                                strErrorMessage = String.Empty;
                                PropertyInfo errorMessage = error.GetType().GetProperty("Error");

                                ///BFM services has not the same structure of the other
                                ///SWS, these services has ShortText properties
                                if (errorMessage == null)
                                {
                                    errorMessage = error.GetType().GetProperty("ShortText");
                                    strErrorMessage = (string)errorMessage.GetValue(error, null);
                                }
                                else
                                {
                                    strErrorMessage = getErrorInfo(error);
                                }
                                strErrorMessages.Append(strErrorMessage + Environment.NewLine);
                            }
                            strErrorMessage = strErrorMessages.ToString();
                        }
                        else
                        {
                            strErrorMessage = getErrorInfo(errors);
                        }

                        if (strErrorMessage != String.Empty)
                        {
                            throw new Exception(strErrorMessage);
                        }
                    }
                }
                else
                {
                    object error = errorRS.GetValue(swsResponse, null);

                    if (error != null)
                    {
                        //TODO Sabrecomamnd error
                        throw new Exception("Error genral sabre");
                    }
                }
            }
            else
            {
                string strValue = status.GetValue(swsResponse, null).ToString();
                if (strValue != "Approved")
                {
                    throw new Exception("sesion No aprobada");//ExceptionSWS(Sabre.CO.General.Constant.Error_SWS.SESSION_NOT_APPROVED);
                }
            }
        }

        /// <summary>
        /// Returns a String with the error description
        /// </summary>
        /// <param name="errors">Object that contains the error description</param>
        /// <returns></returns>
        private string getErrorInfo(object errors)
        {
            string strErrorMessage = String.Empty;

            PropertyInfo errorMessage = errors.GetType().GetProperty("Error");
            object error = errorMessage.GetValue(errors, null);
            PropertyInfo errorObject = error.GetType().GetProperty("ErrorInfo");
            object errorInfo = errorObject.GetValue(error, null);
            PropertyInfo message = errorInfo.GetType().GetProperty("Message");
            strErrorMessage = message.GetValue(errorInfo, null).ToString();

            return strErrorMessage;
        }
    }
}
