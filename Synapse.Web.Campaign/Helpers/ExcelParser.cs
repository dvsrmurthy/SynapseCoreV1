using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Models.Extensions;
using Excel;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Reflection.Emit;
using Synapse.Web.CampaignPlugin.Models;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;
using Synapse.Web.CampaignPlugin.Helpers.SecureAccess;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet;

namespace Synapse.Web.CampaignPlugin.Helpers
{
    public class ExcelParser
    {
        public static List<FileUploadDet> ParseFile(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                var fileExtension = Path.GetExtension(filePath);
                switch (fileExtension)
                {
                    case ".xls":
                    case ".xlsx":
                        return DynamicParseExcel(filePath, fileExtension);
                    case ".csv":
                    case ".txt":
                        var datatable = Core.Models.Extensions.IEnumerableExtension.ParseCsvDataTable<DataTable>(filePath);
                        if (datatable != null)
                        {
                            var response = new List<FileUploadDet>();
                            var dynamicList = ToDynamicList(ToDictionary(datatable), getNewObject(datatable.Columns, "DynamicClass"));
                            response.Add(new FileUploadDet
                            {
                                SheetName = datatable.TableName,
                                Columns = datatable.Columns.Cast<DataColumn>().Select(s => s.ColumnName).ToList(),
                                FileRecord = dynamicList.FirstOrDefault(),
                                FilePath = filePath,
                                FileRecords = dynamicList
                            });
                            return response;
                        }
                        break;
                }
            }
            return new List<FileUploadDet>();
        }
        private static List<FileUploadDet> DynamicParseExcel(string filePath, string extension)
        {
            List<FileUploadDet> response =null; //
            var res = new List<dynamic>();
            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader excelReader = null;
                    switch (extension)
                    {
                        case ".xls":
                            /*  Reading from a binary Excel file ('97-2003 format; *.xls)   */
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                            break;
                        case ".xlsx":
                            /*  Reading from a OpenXml Excel file (2007 format; *.xlsx) */
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            break;
                    }
                    /*  DataSet - Create column names from first row    */
                   // var result1 = excelReader.AsDataSet();
                    excelReader.IsFirstRowAsColumnNames = true;
                    var result = excelReader.AsDataSet();
                    
                    if (result.Tables.Count > 0)
                    {
                        response = new List<FileUploadDet>();
                        foreach (DataTable dt in result.Tables)
                        {
                            
                            var dynamicList = ToDynamicList(ToDictionary(dt), getNewObject(dt.Columns, "DynamicClass"));
                            response.Add(new FileUploadDet
                            {
                                SheetName = dt.TableName,
                                Columns = dt.Columns.Cast<DataColumn>().Select(s => s.ColumnName).ToList(),
                                FileRecord = dynamicList.FirstOrDefault(),
                                FilePath = filePath,
                                FileRecords = dynamicList
                            });

                            //  var tblname = dt.TableName;
                            //  var columns = dt.Columns.Cast<DataColumn>().Select(s=>s.ColumnName).ToList();
                            //response.AddRange(ToDynamicList(ToDictionary(dt), getNewObject(dt.Columns, "DynamicClass")));
                        }
                    }
                    // response = result.Tables[0].ToList<ImportContact>();
                    excelReader.Close();
                }
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                extension = extension.Equals(".xls") ? ".xlsx" : ".xls";
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader excelReader = null;
                    switch (extension)
                    {
                        case ".xls":
                            /*  Reading from a binary Excel file ('97-2003 format; *.xls)   */
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                            break;
                        case ".xlsx":
                            /*  Reading from a OpenXml Excel file (2007 format; *.xlsx) */
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            break;
                    }
                    /*  DataSet - Create column names from first row    */
                    excelReader.IsFirstRowAsColumnNames = true;
                    var result = excelReader.AsDataSet();
                    if (result.Tables.Count > 0)
                    {
                        response = new List<FileUploadDet>();
                        foreach (DataTable dt in result.Tables)
                        {
                            var dynamicList = ToDynamicList(ToDictionary(dt), getNewObject(dt.Columns, "DynamicClass"));
                            response.Add(new FileUploadDet
                            {
                                SheetName = dt.TableName,
                                Columns = dt.Columns.Cast<DataColumn>().Select(s => s.ColumnName).ToList(),
                                FileRecord = dynamicList.FirstOrDefault(),
                                FilePath = filePath,
                                FileRecords = dynamicList
                            });

                            //  var tblname = dt.TableName;
                            //  var columns = dt.Columns.Cast<DataColumn>().Select(s=>s.ColumnName).ToList();
                            //response.AddRange(ToDynamicList(ToDictionary(dt), getNewObject(dt.Columns, "DynamicClass")));
                        }
                    }
                    // response = result.Tables[0].ToList<ImportContact>();
                    excelReader.Close();
                }
            }
            return response;
        }
        private static List<Dictionary<string, object>> ToDictionary(DataTable dt)
        {
            var columns = dt.Columns.Cast<DataColumn>();
            var Temp = dt.AsEnumerable().Select(dataRow => columns.Select(column =>
                                 new { Column = column.ColumnName, Value = dataRow[column] })
                             .ToDictionary(data => data.Column, data => data.Value)).ToList();


            //int previewCnt = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["PreViewCnt"]);
            //var prvRecords = (from cnt in Temp
            //                  select cnt).Take(previewCnt);
            //SessionExtensions.AddItem<List<Dictionary<string, object>>>( Controller.Session, prvRecords);



            return Temp.ToList();
        }
        private static List<dynamic> ToDynamicList(List<Dictionary<string, object>> list, Type TypeObj)
        {
            dynamic temp = new List<dynamic>();
            foreach (Dictionary<string, object> step in list)
            {
                object Obj = Activator.CreateInstance(TypeObj);

                PropertyInfo[] properties = Obj.GetType().GetProperties();

                Dictionary<string, object> DictList = (Dictionary<string, object>)step;

                foreach (KeyValuePair<string, object> keyValuePair in DictList)
                {
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.Name == keyValuePair.Key)
                        {
                            if (keyValuePair.Value != null && keyValuePair.Value.GetType() != typeof(System.DBNull))
                            {
                                if (keyValuePair.Value.GetType() == typeof(System.Guid))
                                {
                                    property.SetValue(Obj, keyValuePair.Value, null);
                                }
                                else
                                {
                                    property.SetValue(Obj, keyValuePair.Value, null);
                                }
                            }
                            break;
                        }
                    }
                }
                temp.Add(Obj);
            }
            return temp;
        }
        private static Type getNewObject(DataColumnCollection columns, string className)
        {
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "YourAssembly";
            //System.Reflection.Emit.AssemblyBuilder assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder module = assemblyBuilder.DefineDynamicModule("YourDynamicModule");
            TypeBuilder typeBuilder = module.DefineType(className, TypeAttributes.Public);

            foreach (DataColumn column in columns)
            {
                string propertyName = column.ColumnName;
                FieldBuilder field = typeBuilder.DefineField(propertyName, column.DataType, FieldAttributes.Public);
                PropertyBuilder property = typeBuilder.DefineProperty(propertyName, System.Reflection.PropertyAttributes.None, column.DataType, new Type[] { column.DataType });
                MethodAttributes GetSetAttr = MethodAttributes.Public | MethodAttributes.HideBySig;
                MethodBuilder currGetPropMthdBldr = typeBuilder.DefineMethod("get_value", GetSetAttr, column.DataType, new Type[] { column.DataType }); // Type.EmptyTypes);
                ILGenerator currGetIL = currGetPropMthdBldr.GetILGenerator();
                currGetIL.Emit(OpCodes.Ldarg_0);
                currGetIL.Emit(OpCodes.Ldfld, field);
                currGetIL.Emit(OpCodes.Ret);
                MethodBuilder currSetPropMthdBldr = typeBuilder.DefineMethod("set_value", GetSetAttr, null, new Type[] { column.DataType });
                ILGenerator currSetIL = currSetPropMthdBldr.GetILGenerator();
                currSetIL.Emit(OpCodes.Ldarg_0);
                currSetIL.Emit(OpCodes.Ldarg_1);
                currSetIL.Emit(OpCodes.Stfld, field);
                currSetIL.Emit(OpCodes.Ret);
                property.SetGetMethod(currGetPropMthdBldr);
                property.SetSetMethod(currSetPropMthdBldr);
            }
            Type obj = typeBuilder.CreateType();
            return obj;
        }



        public static DataTable ParseFileOnPreView(string filePath, string SheetName)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                var fileExtension = Path.GetExtension(filePath);
                switch (fileExtension)
                {
                    case ".xls":
                    case ".xlsx":
                        return ParseExcelToDataTable(filePath, fileExtension, SheetName);
                    case ".csv":
                    case ".txt":
                        return Core.Models.Extensions.IEnumerableExtension.ParseCsvDataTable<DataTable>(filePath); 
                }
            }
            return new DataTable();
        }

        public static DataTable ParseExcelToDataTable(string filePath, string fileExtension,string SheetName)
        {
            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader excelReader = null;
                    switch (fileExtension)
                    {
                        case ".xls":
                            /*  Reading from a binary Excel file ('97-2003 format; *.xls)   */
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                            break;
                        case ".xlsx":
                            /*  Reading from a OpenXml Excel file (2007 format; *.xlsx) */
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            break;
                    }
                    /*  DataSet - Create column names from first row    */
                    excelReader.IsFirstRowAsColumnNames = true;
                    var result = excelReader.AsDataSet();
                    DataTable returnData = new DataTable();
                    if (result.Tables.Count > 0)
                    {
                        if (SheetName != "")
                        {
                            foreach (DataTable dt in result.Tables)
                            {

                                if (SheetName == dt.TableName)
                                    returnData = dt;
                            }
                        }
                        else {
                            return result.Tables[0];
                        }
                    }
                    excelReader.Close();
                    return returnData;
                }
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
            return new DataTable();
        }














        
    }
}