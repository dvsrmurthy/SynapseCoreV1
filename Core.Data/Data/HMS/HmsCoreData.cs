using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Dtos.Responses.HMS.HealthAlerts;
using Core.Data.Utilities;
using Core.Data.IDataInterfaces.HMS;
using Core.Data.Utilities;
using Core.Models.Enums;
using Core.Models.Dtos.Requests.HMS.HealthAlerts;
using Core.Models.Dtos.Responses.HMS.HealthAlerts.AdminOnResponse;
using Core.Models.Dtos.Requests.HMS.HealthAlerts.AdminOnRequest;
using Core.Models.Dtos.Requests.HMS.HealthAlerts.UserOnRequest;
using Core.Models.Dtos.Responses.HMS.HealthAlerts.UserOnResponse;
using Core.Models.Dtos.CommonDtos;
using System.Xml.Linq;

namespace Core.Data.Data.HMS
{
    public class HmsCoreData : IHmsCoreData
    {
        #region Health Alerts
        #region CustomerTriggers
        /// <summary>
        /// Method Name : GetCustomers
        /// Created By : CH Rajeswari
        /// Created On : 29/09/2016
        /// Purpose : To get all the data of Customer Triggers
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
        public async Task<List<CustomerOnResponse>> GetCustomers(CustomerOnRequest Request)
        {
            using (var _consumer = new CoreDBConsumer())
            {
                var result = await _consumer.DbConsumerForMultiItems<CustomerOnResponse>("HA_GETCUSTOMERS", SqlEventTypes.Select,
                                                                      new Dictionary<string, object>
                                                                      {
                                                                          
                                                                      });

                return result;
            }
        }
        /// <summary>
        /// Method Name : GetTransMsgTypes
        /// Created By : CH Rajeswari
        /// Created On : 29/09/2016
        /// Purpose : To get all the data of Transaction Message Types
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
        public async Task<List<TransMsgTypesOnResponse>> GetTransMsgTypes(TransMsgTypesOnRequest Request)
        {
            using (var _consumer = new CoreDBConsumer())
            {
                return await _consumer.DbConsumerForMultiItems<TransMsgTypesOnResponse>("HA_GETTRANSACTIONMSGTYPES", SqlEventTypes.Select,
                                                                                        new Dictionary<string, object>
                                                                                        {
                                                                                            {"@USERID",Request.UserId}
                                                                                        });
            }
        }
        /// <summary>
        /// Method Name : GetSelectedTransMsgTypes
        /// Created By : CH Rajeswari
        /// Created On : 29/09/2016
        /// Purpose : To get selected the data of Transaction Message Types of particular customer
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
        public async Task<List<SelectedTransMsgTypesOnResponse>> GetSelectedTransMsgTypes(SelectedTransMsgTypesOnRequest Request)
        {
            using (var _consumer = new CoreDBConsumer())
            {
                return await _consumer.DbConsumerForMultiItems<SelectedTransMsgTypesOnResponse>("HA_SELECTEDTRANSTYPES", SqlEventTypes.Select,
                                                                                                new Dictionary<string, object>
                                                                                                 {
                                                                                                     {"@CUSTID",Request.CustId},
                                                                                                     {"@ADDEDBY",Request.AddedBy}
                                                                                                 });
            }
        }
        /// <summary>
        /// Method Name : GetCustomerTriggers
        /// Created By : CH Rajeswari
        /// Created On : 29/09/2016
        /// Purpose : To get all the data of Customer Triggers
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        public async Task<List<GetCustomerTriggersOnResponse>> GetCustomerTriggers(GetCustomerTriggersOnRequest Request)
        {
            using (var _consumer = new CoreDBConsumer())
            {
                return await _consumer.DbConsumerForMultiItems<GetCustomerTriggersOnResponse>("HA_GETCUSTOMERTRIGGERS", SqlEventTypes.Select,
                                                                                               new Dictionary<string, object>
                                                                                               {
                                                                                                   {"@USERID",Request.userId}
                                                                                               });
            }
        }
        /// <summary>
        /// Method Name : GetCustomerTriggersChangeStatus
        /// Created By : CH Rajeswari
        /// Created On : 29/09/2016
        /// Purpose : To change the Status of Customer Triggers
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        public async Task<int> SetCustomerTriggersChangeStatus(CustomerTriggerChangeStatusOnRequest Request)
        {
            //string ReturnResult = string.Empty;
            
           using(var _consumer = new CoreDBConsumer())
           {
               var result = await _consumer.DbConsumer<int>("HA_CUSTOMERTRIGGERSCHANGESTATUS", SqlEventTypes.Update,
                                                  new Dictionary<string, object>
                                                  {
                                                      {"@AUTOID",Request.AutoId},
                                                      {"@STATUS",Request.Status},
                                                      {"@ADDEDBY",Request.AddedBy}
                                                  });
               //if (result > 0)
               //{
               //    ReturnResult = "Status changed successfully.";
               //}
               //else
               //{
               //    ReturnResult = "Statsu not changed.";
               //}

               return result;

           }

           //return ReturnResult;
        }
        /// <summary>
        /// Method Name : GetCustomerPerferenceCount
        /// Created By : CH Rajeswari
        /// Created On : 30/09/2016
        /// Purpose : To get the Preference count of Customer 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        public async Task<List<CustomerPerferenceCountOnRespone>> GetCustomerPerferenceCount(CustomerPerferenceCountOnRequest Request)
        {
            using (var _consumer = new CoreDBConsumer())
            {
                return await _consumer.DbConsumerForMultiItems<CustomerPerferenceCountOnRespone>("HA_CUSTOMERPREFERENCES", SqlEventTypes.Select,
                                                                                                 new Dictionary<string, object>
                                                                                                 {
                                                                                                     {"@CUSTID",Request.CustId}
                                                                                                 });
            }
        }
        /// <summary>
        /// Method Name : GetCustomerExistingTriggersCount
        /// Created By : CH Rajeswari
        /// Created On : 30/09/2016
        /// Purpose : To get the existing triggers count of Customer 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        public async Task<List<CustomerExistingTriggersCountOnResponse>> GetCustomerExistingTriggersCount(CustomerExistingTriggersCountOnRequest Request)
        {
            using (var _consumer = new CoreDBConsumer())
            {
                return await _consumer.DbConsumerForMultiItems<CustomerExistingTriggersCountOnResponse>("HA_EXISTINGCUSTOMERTRIGGERS", SqlEventTypes.Select,
                                                                                                        new Dictionary<string, object>
                                                                                                        {
                                                                                                            {"@CUSTID",Request.CustId}
                                                                                                        });                   
            }
        }
        /// <summary>
        /// Method Name : SetCustomerTriggers
        /// Created By : CH Rajeswari
        /// Created On : 30/09/2016
        /// Purpose : To add the customer triggers 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        public async Task<int> SetCustomerTriggers(SetCustomerTriggersOnRequest Request)
        {
           // string ReturnResult = string.Empty;
            using (var _consumer = new CoreDBConsumer())
            {
                var result = await _consumer.DbConsumer<int>("HA_SETCUSTMTRIGGERS", SqlEventTypes.Insert,
                                                              new Dictionary<string, object>
                                                              {
                                                                {"@CUSTID",Request.CustId},
                                                                {"@TRANSID",Request.TransId},
                                                                {"@STATUS",Request.Status},
                                                                {"@ADDEDBY",Request.AddedBy}
                                                              });
                //if (result > 0)
                //{
                //    ReturnResult = "Customer trigger saved successfully.";
                //}
                //else
                //{
                //    ReturnResult = "Customer trigger not saved.";
                //}

                return result;
            }
            //return ReturnResult;
        }
        /// <summary>
        /// Method Name : DeleteExistingCustomerTriggers
        /// Created By : CH Rajeswari
        /// Created On : 30/09/2016
        /// Purpose : To delete the existing triggers of customers
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        public async Task<int> DeleteExistingCustomerTriggers(DeleteExistingCustomerTriggersOnRequest Request)
        {
           // string ReturnResult = string.Empty;
            using(var _consumer=new CoreDBConsumer())
            {
                var result = await _consumer.DbConsumer<int>("HA_DELETEEXISTINGCUSTRIGGERS", SqlEventTypes.Delete,
                                                             new Dictionary<string, object>
                                                             {
                                                                 {"@CUSTID",Request.CustId}
                                                             });
                //if (result > 0)
                //{
                //    ReturnResult = "Customer trigger deleted successfully.";
                //}
                //else
                //{
                //    ReturnResult = "Customer trigger not deleted.";
                //}
                return result;
            }
           // return ReturnResult;
        }
        /// <summary>
        /// Method Name : UpdateCustomeTriggers
        /// Created By : CH Rajeswari
        /// Created On : 30/09/2016
        /// Purpose : To update the existing triggers of customers
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        public async Task<int> UpdateCustomeTriggers(UpdateCustomeTriggersOnRequest Request)
        {
           // string ReturnResult = string.Empty;
            using (var _consumer = new CoreDBConsumer())
            {
                var result = await _consumer.DbConsumer<int>("HA_UPDATECUSTMTRIGGERS", SqlEventTypes.Insert,
                                                              new Dictionary<string, object>
                                                              {
                                                                {"@CUSTID",Request.CustId},
                                                                {"@TRANSID",Request.TransId},
                                                                {"@STATUS",Request.Status},
                                                                {"@ADDEDBY",Request.AddedBy}
                                                              });
                //if (result > 0)
                //{
                //    ReturnResult = "Customer trigger saved successfully.";
                //}
                //else
                //{
                //    ReturnResult = "Customer trigger not saved.";
                //}
                return result;
            }
           // return ReturnResult;
        }
        #endregion
        #region AlertTypes
        /// <summary>
        /// Method Name : GetTransactionTypes
        /// Created By : CH Rajeswari
        /// Created On : 03/10/2016
        /// Purpose : To get all the transaction types 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
        public async Task<List<AlertTypesOnResponse>> GetTransactionTypes(GetAlertTypesRequest Request)
        {
            using (var _consumer = new CoreDBConsumer())
            {
                var result= await _consumer.DbConsumerForMultiItems<AlertTypesOnResponse>("HA_GETTRANSACTIONTYPES", SqlEventTypes.Select,
                                                                             new Dictionary<string, object>
                                                                             {
                                                                                
                                                                             });
                return result;
            }
        }
        /// <summary>
        /// Method Name : SetTransactionTypes
        /// Created By : CH Rajeswari
        /// Created On : 03/10/2016
        /// Purpose : To add the transaction types 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
        public async Task<int> SetTransactionTypes(AlertTypesOnRequest Request)
        {
           // string ReturnResult = string.Empty;
           using(var _consumer=new CoreDBConsumer())
           {
               var result = await _consumer.DbConsumer<ReUsableResponse>("HA_ADDTRANSACTIONTYPES", SqlEventTypes.Select,
                                                          new Dictionary<string, object>
                                                          {
                                                              {"@TRANSACTIONTYPE",Request.TRANSACTIONTYPE},
                                                              {"@NAME",Request.TRANSNAME},
                                                              {"@MSGTYPE",Request.TRANSMSGTYPE},
                                                              {"@ID",Request.Id}
                                                          });
               //if(result > 0)
               //{
               //    ReturnResult = "Transaction types details saved successfully.";
               //}
               //else
               //{
               //    ReturnResult = "Transaction types details are not saved.";
               //}
               return result.ReturnValue;
           }
          // return ReturnResult;
        }
        /// <summary>
        /// Method Name : RemoveSegments
        /// Created By : CH Rajeswari
        /// Created On : 03/10/2016
        /// Purpose : To remove all the segments of particular transaction types 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        public async Task<int> RemoveSegments(RemoveSegmentsOnrequest Request)
        {
           // string ReturnResult = string.Empty;
            using (var _consumer = new CoreDBConsumer())
            {
                var result = await _consumer.DbConsumer<int>("HA_REMOVESEGMNETS", SqlEventTypes.Delete,
                                                             new Dictionary<string, object> {
                                                             {"@strTransType",Request.TransType}
                                                             });
                //if(result > 0)
                //{
                //    ReturnResult = "Segments deleted successfully.";
                //}
                //else
                //{
                //    ReturnResult = "Segments are not deleted.";
                //}
                return result;

            }
           // return ReturnResult;
        }
        /// <summary>
        /// Method Name : ImportSegments
        /// Created By : CH Rajeswari
        /// Created On : 12/12/2016
        /// Purpose : To import all the segments of particular transaction types 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        public async Task<int> ImportSegments(ImportSegmentsListOnRequest Request)
        {
            using(var _consumer =new CoreDBConsumer())
            {
                var xEle = new XElement("segments",
                               from Segment in Request.SegmentsList
                               select new XElement("segment1",
                              new XElement("SEGMENT", Segment.SegmentName ?? ""),
                             new XElement("SEGMENTTYPE", Segment.NodeType ?? ""),
                             new XElement("SEGMENTPARENT", Segment.ParentNode ?? ""),
                             new XElement("DESCRIPTION", Segment.Description ?? "")
                            
                         ));
                var response = await _consumer.DbConsumer<ReUsableResponse>("HA_IMPORTSEGMENTS", SqlEventTypes.Select,
                     new Dictionary<string, object>(){
                                             {"@xmlSegments", xEle.ToString()},
                                             {"@userid",Request.UserId},
                                             {"@TRANSACTIONTYPE",Request.Id},
                                             //DBNull.Value
                                              {"@NRETVAL", 0}
                                             
                         });
                return response.NRETVAL;
            }
        }
        /// <summary>
        /// Method Name : ImportSegments
        /// Created By : CH Rajeswari
        /// Created On : 13/12/2016
        /// Purpose : To get all the segments of particular transaction types 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        public async Task<List<GetSegmentsOnResponse>> GetSegments(GetSegmentsOnRequest Request)
        {
            using(var _consumer = new CoreDBConsumer())
            {
                var result= await _consumer.DbConsumerForMultiItems<GetSegmentsOnResponse>("HA_GETSEGMENTS", SqlEventTypes.Select,
                                                                             new Dictionary<string, object>
                                                                             {
                                                                                 {"@NTRANSACTIONTYPEID",Request.Id},
                                                                                 {"@USERID",Request.UserId}
                                                                             });
                return result;
            }

        }
        //SegmentsBulckcopy
        #endregion       
        #region ConfigAlerts
        /// <summary>
        /// Method Name : GetUsers
        /// Created By : CH Rajeswari
        /// Created On : 06/10/2016
        /// Purpose : To get all the data of Users in Synapse Admin Application
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
       public async Task<List<ConfigAlertsOnResponse>> GetUsers(ConfigAlertsOnRequest Request)
        {
            using (var _consumer = new CoreDBConsumer())
            {
                return await _consumer.DbConsumerForMultiItems<ConfigAlertsOnResponse>("HA_GETUSERS", SqlEventTypes.Select,
                                                                                      new Dictionary<string, object>
                                                                                      {
                                                                                          {"@CUSTID",Request.CustId},
                                                                                          {"@USERID",Request.UserId},
                                                                                          {"@STATUS",Request.Status}
                                                                                      });
            }
        }
        /// <summary>
        /// Method Name : GetSegmentsConfig
        /// Created By : CH Rajeswari
        /// Created On : 06/10/2016
        /// Purpose : To get all the Segments
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
       public async Task<List<SegmentsConfigOnResponse>> GetSegmentsConfig(SegmentsConfigOnRequest Request)
       {
           using(var _consumer=new CoreDBConsumer())
           {
               return await _consumer.DbConsumerForMultiItems<SegmentsConfigOnResponse>("HA_GETSEGMENTSCONFIG", SqlEventTypes.Select,
                                                                                        new Dictionary<string, object>
                                                                                        {
                                                                                            {"@nTransID",Request.TransId}
                                                                                        });
           }
       }
        /// <summary>
        /// Method Name : GetSegmentsSelected
        /// Created By : CH Rajeswari
        /// Created On : 07/10/2016
        /// Purpose : To get selected segments for the particular segments ids
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
       public async Task<List<SegmentsSelectedOnResponse>> GetSegmentsSelected(SegmentsSelectedOnRequest Request)
       {
           using (var _consumer = new CoreDBConsumer())
           {
               return await _consumer.DbConsumerForMultiItems<SegmentsSelectedOnResponse>("HA_GETSEGMENTSSELECTED", SqlEventTypes.Select,
                                                             new Dictionary<string, object>
                                                             {
                                                                 {"@strSelectedSegments",Request.SelectedSegments}
                                                               
                                                             });              
           }
           
       }
        /// <summary>
        /// Method Name : GetSenderIds
        /// Created By : CH Rajeswari
        /// Created On : 12/10/2016
        /// Purpose : To get SenderIds for the particular User Id
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
       public async Task<List<SenderIdsOnResponse>> GetSenderIds(SenderIdsOnRequest Request)
       {
           using (var _consumer = new CoreDBConsumer())
           {
               return await _consumer.DbConsumerForMultiItems<SenderIdsOnResponse>("GETGLBINTLSENDERS", SqlEventTypes.Select,
                                                                                   new Dictionary<string, object> 
                                                                                   { 
                                                                                      {"@nUserId",Request.UserId},
                                                                                      {"@requestedby",Request.RequestedBy ?? "" },
                                                                                      {"@NRETVAL",Request.Retval}
                                                                                   });
           }
       }
        /// <summary>
       /// Method Name : GetTransTypesCustomer
        /// Created By : CH Rajeswari
        /// Created On : 12/10/2016
        /// Purpose : To get Transaction Details for the particular Trans Id
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
       public async Task<List<TransTypesCustomerOnResponse>> GetTransTypesCustomer(TransTypeCustomerOnRequest Request)
       {
            using(var _consumer=new CoreDBConsumer())
            {
                return await _consumer.DbConsumerForMultiItems<TransTypesCustomerOnResponse>("HA_GETTRANSTYPESCUSTOMER", SqlEventTypes.Select,
                                                                 new Dictionary<string, object> 
                                                                {
                                                                    {"@nCustomerID",Request.CustomerId},
                                                                    {"@strTransType",Request.TransType}
                                                                });
            }
       }
        /// <summary>
       /// Method Name : GetSegmentsforTemplate
        /// Created By : CH Rajeswari
        /// Created On : 12/10/2016
        /// Purpose : To get Segments Details for the particular Template
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        public async Task<List<SegmentsforTemplateOnResponse>> GetSegmentsforTemplate(SegmentsforTemplateOnRequest Request)
       {
            using(var _consumer=new CoreDBConsumer())
            {
                return await _consumer.DbConsumerForMultiItems<SegmentsforTemplateOnResponse>("HA_GETSEGFORTEMPLATE", SqlEventTypes.Select,
                                                                                              new Dictionary<string, object> 
                                                                                              {
                                                                                                {"@nTempID",Request.TemplateId}
                                                                                              });
            }

       }
        /// <summary>
        /// Method Name : GetSegmentsforTemplate
        /// Created By : CH Rajeswari
        /// Created On : 13/10/2016
        /// Purpose : To delete placeholders for the particular TransTypeId
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        public async Task<int> DeletePlaceholders(DeletePlaceHoldersOnRequest Request)
        {
           // string ReturnResult = string.Empty;
            using(var _consumer=new CoreDBConsumer())
            {
                var result = await _consumer.DbConsumer<int>("HA_DELETEPLACEHOLDERS", SqlEventTypes.Delete,
                                                           new Dictionary<string, object> { 
                                                           {"@nTransTypeID",Request.TransTypeId}
                                                           });
                //if(result > 0)
                //{
                //    ReturnResult = "Deleted successfully.";                    
                //}
                //else
                //{
                //    ReturnResult = "Not Deleted.";
                //}
                return result;
            }
           // return ReturnResult;
        }
        /// <summary>
        /// Method Name : GetSegmentsforTemplate
        /// Created By : CH Rajeswari
        /// Created On : 13/10/2016
        /// Purpose : To insert placeholders details
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        public async Task<int> SetPlaceHolders(SetPlaceholdersOnRequest Request)
        {
           // string ReturnResult = string.Empty;
            using(var _consumer=new CoreDBConsumer())
            {
                var result = await _consumer.DbConsumer<int>("HA_SETPLACEHOLDERS", SqlEventTypes.Insert,
                                                           new Dictionary<string, object> 
                                                           { 
                                                            {"@nTemplateID",Request.TemplateId},
                                                            {"@nSegID",Request.SegId},
                                                            {"@nRETVAL",Request.RetVal},
                                                            {"@strErr",Request.StrErr}
                                                           });
                //if(result > 0)
                //{
                //    ReturnResult = "Inserted successfully.";

                //}
                //else
                //{
                //    ReturnResult = "Not inserted.";
                //}
                return result;
            }
           // return ReturnResult;
        }
        /// <summary>
        /// Method Name : GetSegmentForTransType
        /// Created By : CH Rajeswari
        /// Created On : 14/10/2016
        /// Purpose : To get segments for Trans Type details
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        public async Task<List<SegmentForTransTypeOnResponse>> GetSegmentForTransType(SegmentForTransTypeOnRequest Request)
        {
            using(var _consumer=new CoreDBConsumer())
            {
                return await _consumer.DbConsumerForMultiItems<SegmentForTransTypeOnResponse>("HA_GETSEGFORTRANSTYPE", SqlEventTypes.Select,
                                                                                             new Dictionary<string, object> 
                                                                                             { 
                                                                                              {"@nCustomerID",Request.CustomerId},
                                                                                              {"@nUserID",Request.UserId},
                                                                                              {"@nTransType",Request.TransType}
                                                                                             });
            }
        }
        /// <summary>
        /// Method Name : GetFilePathsAlertType
        /// Created By : CH Rajeswari
        /// Created On : 14/10/2016
        /// Purpose : To get FilePaths for Alerttype
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        public async Task<List<FilePathsAlertTypeOnResponse>> GetFilePathsAlertType(FilePathsAlertTypeOnRequest Request)
        {
            using(var _consumer=new CoreDBConsumer())
            {
                return await _consumer.DbConsumerForMultiItems<FilePathsAlertTypeOnResponse>("HA_GETFILEPATHSALERTTYPE", SqlEventTypes.Select,
                                                                                             new Dictionary<string, object>
                                                                                             {
                                                                                               {"@STRFILEPATH",Request.FilePath},
                                                                                               {"@NALERTTYPE",Request.AlertType},
                                                                                               {"@NSTATUS",Request.Status},
                                                                                               {"@NRETVAL",Request.Retval},
                                                                                               {"@STRRETMESSAGE",Request.ReturnMsg}
                                                                                             });
            }
        }
        /// <summary>
        /// Method Name : GetFilePathsAlertType
        /// Created By : CH Rajeswari
        /// Created On : 14/10/2016
        /// Purpose : To get FilePaths for Alerttype
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        public async Task<int> SetAlertTemplates(AlertTemplatesOnRequest Request)
        {
            //string ReturnResult = string.Empty;
          using(var _consumer=new CoreDBConsumer())
          {
              var result = await _consumer.DbConsumer<ReUsableResponse>("HA_SETALERTTEMPLATES", SqlEventTypes.Select,
                                                         new Dictionary<string, object> 
                                                         { 
                                                          {"@nTransTypeID",Request.TransTypeId},
                                                          {"@strFileType",Request.FileType},
                                                          {"@nSourceType",Request.SourceType},
                                                          {"@strFILEPATH",Request.FilePath},
                                                          {"@strSENTFILEPATH",Request.SentFilePath},
                                                          {"@strFAILEDFILEPATH",Request.FailedFilePath},
                                                          {"@strDLRPATH",Request.DlrPath},
                                                          {"@strSOURCEIP",Request.SourceIp},
                                                          {"@strSOURCEPORT",Request.SourcePort},
                                                          {"@strDESTIP",Request.DestIp},
                                                          {"@strDESTPORT",Request.DestPort},
                                                          {"@strMSGTEMPLATE",Request.MsgTemplate},
                                                          {"@strPLACEHOLDERS",Request.PlcaeHolders},
                                                          {"@nMobileNo",Request.SegMobileNoId},
                                                          {"@nSTATUS",Request.Status},
                                                          {"@nCustomerID",Request.CustomerId},
                                                          {"@nUserID",Request.UserId},
                                                          {"@nSenderID",Request.SenderId},
                                                          {"@nCREATEDBY",Request.CreatedBy},
                                                          {"@nRETVAL",Request.Retval},
                                                          {"@strErr",Request.StrErr}
                                                         });
              //if (result > 0)
              //{
              //    ReturnResult = "Saved Successfully.";
              //}
              //else
              //{
              //    ReturnResult = "Not Saved.";
              //}
              return result.NRETVAL;
          }
          //return ReturnResult;
        }
        /// <summary>
        /// Method Name : GetFilePathsAlertType
        /// Created By : CH Rajeswari
        /// Created On : 06/01/2017
        /// Purpose : To get Segments Details for SegmentId
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        public async Task<List<GetSegmentsbyAutoIdOnResponse>> GetSegmentsbyAutoId(GetSegmentsbyAutoIdOnRequest Request)
        {
            using (var _consumer = new CoreDBConsumer())
            {
                var result = await _consumer.DbConsumerForMultiItems<GetSegmentsbyAutoIdOnResponse>("HA_GETSEGMENTSBYAUTOID", SqlEventTypes.Select,
                                                                      new Dictionary<string, object>
                                                                      {
                                                                          {"@STRAUTOIDS",Request.StrAutoIds}
                                                                      });
                return result;
            }
        }
        /// <summary>
        /// Method Name : GetFilePathsAlertType
        /// Created By : CH Rajeswari
        /// Created On : 06/01/2017
        /// Purpose : To get ALert Template Details 
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        public async Task<List<GetAlertTemplatedetOnResponse>> GetAlertTemplateDet(GetAlertTemplatedetOnRequest Request)
        {
            using(var _consumer = new CoreDBConsumer())
            {
                var result = await _consumer.DbConsumerForMultiItems<GetAlertTemplatedetOnResponse>("HA_GETALERTTEMPLATEDET", SqlEventTypes.Select,
                                                                             new Dictionary<string, object>
                                                                             {
                                                                                 {"@nTemplateID",Request.TemplateId},
                                                                                 {"@strRetMsg",Request.StrRetMsg},
                                                                                 {"@nStatus",Request.Status}
                                                                             });
                return result;

            }
        }
        #endregion
        #endregion
    }
}
