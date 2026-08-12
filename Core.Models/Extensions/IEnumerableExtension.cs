using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Excel;
using Core.Models.Dtos.Requests.Synapse.UserMoCampaignConfiguration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Net;
using Core.Models.Helpers;
using ExportToExcel;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Localizations;



namespace Core.Models.Extensions
{
    public static class IEnumerableExtension
    {
        //Specific Country code start
        static int? conCode = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CountryCode"]); //countrycode
        static int conMobLength = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CountryMobileLength"]); //mobilelength   
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IEnumerable<string> WordCount(this string str)
        {
            return !string.IsNullOrWhiteSpace(str) ? str.Split(new[] { '\r', '\n' }, StringSplitOptions.None) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T item, T start, T end)
        {
            return Comparer<T>.Default.Compare(item, start) >= 0
                && Comparer<T>.Default.Compare(item, end) <= 0;
        }

        public static bool IsCheckerRequiredVerification<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            // return source.Where(predicate).Any();
            return source != null && source.Where(predicate).Any();
        }
        public static string GetConfigVal(string strKey)
        {
            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings[strKey] != null)
                    return System.Configuration.ConfigurationManager.AppSettings[strKey].ToString().Trim();
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        //public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        //{
        //    return condition ? source.Where(predicate) : source;
        //}

        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            var columns = table.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
            var properties = typeof(T).GetProperties().Where(x => !x.Name.Equals("ActionResult")).ToList();
            return (from object row in table.Rows select CreateItemFromRow<T>((DataRow)row, properties, columns)).ToList();
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties, List<string> columns)
            where T : new()
        {
            T item = new T();

            foreach (var c in columns)
            {
                var prop = properties.FirstOrDefault(x => x.Name.Equals(c, StringComparison.OrdinalIgnoreCase));
                if (prop != null)
                {
                    if (prop.PropertyType == typeof(Boolean))
                    {
                        var value = !string.IsNullOrWhiteSpace(row[prop.Name].ToString()) &&
                                    (Convert.ToBoolean(row[prop.Name].ToString()));
                        prop.SetValue(item, value, null);
                    }
                    else if (prop.PropertyType == typeof(Int32))
                    {
                        var value = !string.IsNullOrWhiteSpace(row[prop.Name].ToString())
                            ? Convert.ToInt32(row[prop.Name].ToString())
                            : 0;
                        prop.SetValue(item, value, null);
                    }
                    else if (prop.PropertyType == typeof(String))
                    {
                        if (!string.IsNullOrWhiteSpace(row[prop.Name].ToString()))
                            prop.SetValue(item, row[prop.Name].ToString(), null);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(row[prop.Name].ToString()))
                            prop.SetValue(item, row[prop.Name], null);
                    }
                }
            }

            #region Commented working code

            //foreach (var property in properties)
            //{
            //    if (property.PropertyType == typeof(Boolean))
            //    {
            //        var value = !string.IsNullOrWhiteSpace(row[property.Name].ToString()) && (Convert.ToBoolean(row[property.Name].ToString()));
            //        property.SetValue(item, value, null);
            //    }
            //    else if (property.PropertyType == typeof(Int32))
            //    {
            //        var value = !string.IsNullOrWhiteSpace(row[property.Name].ToString()) ? Convert.ToInt32(row[property.Name].ToString()) : 0;
            //        property.SetValue(item, value, null);
            //    }
            //    else if (property.PropertyType == typeof (String))
            //    {
            //        if (!string.IsNullOrWhiteSpace(row[property.Name].ToString()))
            //            property.SetValue(item, row[property.Name].ToString(), null);
            //    }
            //    else
            //    {
            //        if (!string.IsNullOrWhiteSpace(row[property.Name].ToString()))
            //            property.SetValue(item, row[property.Name], null);
            //    }
            //}

            #endregion

            return item;
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static List<T> ParseFile<T>(this string filePath) where T : new()
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                var fileExtension = Path.GetExtension(filePath);
                switch (fileExtension)
                {
                    case ".xls":
                    case ".xlsx":
                        return ParseExcel<T>(filePath, fileExtension);
                    case ".csv":
                    case ".txt":
                        return filePath.ParseCsv<T>();
                }
            }
            return new List<T>();
        }

        public static IEnumerable<string> GetSubStrings(this string input, string start, string end)
        {
            Regex r = new Regex(Regex.Escape(start) + "(.*?)" + Regex.Escape(end));
            MatchCollection matches = r.Matches(input);
            return from Match match in matches select match.Groups[1].Value;
        }

        public static T ParseFileToDataTable<T>(this string filePath) where T : new()
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                var fileExtension = Path.GetExtension(filePath);
                switch (fileExtension)
                {
                    case ".xls":
                    case ".xlsx":
                        return ParseExcelDataTable<T>(filePath, fileExtension);
                    case ".csv":
                    case ".txt":
                        return ParsetextDataTable<T>(filePath, fileExtension);
                        // break;
                }
            }
            return new T();
        }


        public static List<string> ParseTextFile(string filePath, string extension = "")
        {
            var res = new List<string>();

            try
            {
                var allLines = File.ReadAllLines(filePath, Encoding.UTF8);
                if (allLines != null && allLines.Any())
                {
                    res = allLines.Skip(1).Select(s => s).ToList();
                    return res;
                }
                return res;
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
            return res;
        }

        private static T ParseExcelDataTable<T>(string filePath, string extension) where T : new()
        {
            var response = new T();

            try
            {
                var result = new DataSet();
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
                            //result = GetFileDataFromFormatedExcel(stream);
                            break;
                        case ".csv":
                        case ".txt":
                            /* Reading from a openxml Csv file */
                            //excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            break;
                    }
                    /*  DataSet - Create column names from first row    */
                    excelReader.IsFirstRowAsColumnNames = true;
                    result = excelReader.AsDataSet();
                    if (result.Tables.Count > 0)
                    {
                        foreach (DataTable table in result.Tables)
                        {
                            response = (T)Convert.ChangeType(table, typeof(T));
                        }
                    }
                    // response = result.Tables[0].ToList<ImportContact>();
                    excelReader.Close();
                }
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
            return response;
        }

        public static T ParsetextDataTable<T>(this string filePath, string extension) where T : new()
        {
            DataTable dt = new DataTable();
            var lines = File.ReadAllLines(filePath);
            var delimit = ((lines[0].Split('|')) ?? (lines[0].Split('$')) ?? (lines[0].Split('%')));
            string[] headers = delimit;

            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            var txt = ((lines[1].Split('|')) ?? (lines[1].Split('$')) ?? (lines[1].Split('%')));
            DataRow dr = dt.NewRow();
            if (txt != null && txt.Length > 0)
            {

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dr[i] = txt[i];
                }

                dt.Rows.Add(dr);
            }

            return (T)Convert.ChangeType(dt, typeof(T));
        }


        private static List<T> ParseExcel<T>(string filePath, string extension) where T : new()
        {
            var response = new List<T>();
            var res = new List<T>();
            try
            {
                var result = new DataSet();
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
                            // result = GetFileDataFromFormatedExcel(stream);
                            break;
                        case ".csv":
                            /* Reading from a openxml Csv file */
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            break;
                    }
                    /*  DataSet - Create column names from first row    */
                    excelReader.IsFirstRowAsColumnNames = true;
                    result = excelReader.AsDataSet();
                    if (result.Tables.Count > 0)
                    {
                        foreach (DataTable table in result.Tables)
                        {
                            res = table.ToList<T>();
                            response.AddRange(res);
                        }
                    }
                    // response = result.Tables[0].ToList<ImportContact>();
                    excelReader.Close();
                }
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
            return response;
        }
        /// <summary>
        /// CSV to List<T/> parsing
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<T> ParseCsv<T>(this string filePath) where T : new()
        {
            var rows = GetRows(filePath);
            var enumerable = rows as string[][] ?? rows.ToArray();
            var headers = enumerable.FirstOrDefault();

            return MakeContacts<T>(headers, enumerable.Skip(1));
        }

        private static IEnumerable<string[]> GetRows(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var csv = from line in lines
                      select (line.Split(',')).ToArray();
            return csv.ToList();
        }

        private static List<T> MakeContacts<T>(string[] headers, IEnumerable<string[]> rows) where T : new()
        {
            var contacts = new List<T>();
            foreach (var row in rows)
            {
                var contact = new T();
                for (var e = 0; e < row.Length; e++)
                {
                    var header = headers[e];
                    var value = row[e];
                    contact = MatchHeader(contact, header, value);
                }
                contacts.Add(contact);
            }
            return contacts;
        }

        private static T MatchHeader<T>(T user, string header, string value) where T : new()
        {
            var properties = user.GetType().GetProperties();
            var property = properties.FirstOrDefault(p => p.Name.Equals(header, StringComparison.OrdinalIgnoreCase));
            if (property != null && !string.IsNullOrWhiteSpace(value))
            {
                if (property.PropertyType == typeof(Int32))
                {
                    property.SetValue(user, Convert.ToInt32(value));
                }
                else if (property.PropertyType == typeof(DateTime))
                {
                    property.SetValue(user, DateTime.Parse(value));
                }
                else if (property.PropertyType == typeof(Boolean))
                {
                    property.SetValue(user, Convert.ToBoolean(value));
                }
                else
                {
                    property.SetValue(user, value);
                }
            }
            return user;
        }

        /// <summary>
        /// Parsing DataTable to CSV file
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string DataTableToCsvParse(this DataTable source, string name, string path, string extension = ".csv")
        {
            try
            {
                //if (extension == ".xls" || extension == ".xlsx")
                //{
                //    CreateExcelFile.CreateExcelDocument(source, (path + '/' + name + extension));
                //    return string.Empty;
                //}

                var fileContent = new StringBuilder();
                IEnumerable<string> columnNames = source.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                fileContent.AppendLine(string.Join(",", columnNames));
                foreach (DataRow row in source.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                    fileContent.AppendLine(string.Join(",", fields));
                }

                File.WriteAllText(path + '/' + name + ".csv", fileContent.ToString());

                return path + '/' + name + ".csv";
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
            return string.Empty;
        }

        public static string DataTableToExceParse(this DataTable source, string name, string path,
            string extension = ".csv")
        {
            var modifiedPath = (path + '/' + name + extension);
            CreateExcelFile.CreateExcelDocument(source, modifiedPath);
            return modifiedPath;
        }

        public static string DataTableToXmlParse(this DataTable source, string name, string path, int metaIndex = 0)
        {
            try
            {
                var xdoc = new XDocument(
                    new XElement(source.TableName,
                        from column in source.Columns.Cast<DataColumn>()
                        where column != source.Columns[metaIndex]
                        select new XElement(column.ColumnName,
                            from row in source.AsEnumerable()
                            select new XElement(row.Field<string>(metaIndex), row[column])
                            )
                        ));
                File.WriteAllText(path + '/' + name + ".xml", xdoc.ToString());
                return path + '/' + name + ".xml";
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
            return string.Empty;
        }

        public static DataSet ParseExcelToDataSet(string filePath)
        {
            try
            {
                var result = new DataSet();
                var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                var extesion = Path.GetExtension(filePath);
                IExcelDataReader excelReader = null;
                switch (extesion)
                {
                    case ".xls":
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                        break;
                    case ".xlsx":
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        //result = GetFileDataFromFormatedExcel(stream);
                        break;
                }

                excelReader.IsFirstRowAsColumnNames = true;
                result = excelReader.AsDataSet();
                excelReader.Close();
                return result;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return new DataSet();
        }

        public static string WriteExcelFileFromDataTable(string outputPath, DataTable table)
        {
            using (var document = SpreadsheetDocument.Create(outputPath, SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                var sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };

                sheets.Append(sheet);

                var headerRow = new Row();

                var columns = new List<string>();
                foreach (DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);
                    Cell cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);

                foreach (DataRow dsrow in table.Rows)
                {
                    var newRow = new Row();
                    foreach (Cell cell in columns.Select(col => new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(dsrow[col].ToString())
                    }))
                    {
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }

                workbookPart.Workbook.Save();
            }
            return outputPath;
        }

        public static DataSet XmlToDataSetParse(this string xmlfilePath)
        {
            XmlReader xmlFile;
            xmlFile = XmlReader.Create(xmlfilePath, new XmlReaderSettings());
            var ds = new DataSet();
            ds.ReadXml(xmlFile);
            return ds;
        }

        public static string DataSetToXml(this DataSet ds)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(DataSet));
                    xmlSerializer.Serialize(streamWriter, ds);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }

        public static bool IsNumeric(this string source)
        {
            return source.All(char.IsDigit);
        }

        public static string GetXMLString(MoCompaginsForXMLOnRequest value, string location)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(location);
            if (xmlDoc != null)
            {
                var xDoc = XDocument.Parse(xmlDoc.OuterXml);
                var element = xDoc.Root.Element(XName.Get("RECURRENCE"));
                element.Attribute(XName.Get("SCHEDULETIME")).Value = value.nMOCVARSCHEDULE.ToString();
                element.Attribute(XName.Get("STARTTIME")).Value = value.strStartime;
                element.Attribute(XName.Get("ENDTIME")).Value = value.strEndTime;
                element.Attribute(XName.Get("INTERVAL")).Value = value.nInterval.ToString();
                element.Attribute(XName.Get("STARTDATE")).Value = value.strStartDate;
                element.Attribute(XName.Get("ENDON")).Value = value.nEndOn.ToString();
                element.Attribute(XName.Get("ENDONDATE")).Value = value.strEndOn;
                element.Attribute(XName.Get("SENDALERTON")).Value = value.strAlertOn;
                element.Attribute(XName.Get("EVERYNWEEK")).Value = value.nWeek.ToString();
                element.Attribute(XName.Get("WEEKDAYS")).Value = value.nWeek.ToString();
                element.Attribute(XName.Get("DAYS")).Value = value.strDays;
                element.Attribute(XName.Get("MONTHS")).Value = value.strMonths;
                return element.ToString();
                //return (T)Convert.ChangeType(xDoc, typeof(T));

            }
            return string.Empty;
            //return (T)Convert.ChangeType(null, typeof(T));
        }

        public static DataTable GenerateTransposedTable(this DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            // Add columns by looping rows

            // Header row's first column is same as in inputTable
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());
            DataRow HRow = outputTable.NewRow();
            HRow[0] = inputTable.Columns[0].ColumnName.ToString();
            // Header row's second column onwards, 'inputTable's first column taken
            var i = 0;
            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName);
                HRow[i + 1] = newColName;
                i++;
            }
            outputTable.Rows.Add(HRow);
            // Add rows by looping columns        
            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();

                // First column is inputTable's Header row's second column
                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }
            return outputTable;
        }


        public static T ParseCsvDataTable<T>(this string filePath) where T : new()
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(filePath))
            {
                // Regex regx = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    // var s = sr.ReadLine();
                    //  string[] rows = regx.Split(s);
                    string[] rows = sr.ReadLine().Split(',');
                    if (headers.Count() == rows.Count())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i];
                        }
                        dt.Rows.Add(dr);
                    }
                }
                sr.Close();
                sr.Dispose();
            }
            return (T)Convert.ChangeType(dt, typeof(T));
        }

        public static DataTable ConvertToDatatable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        //public static DataTable ConvertToDatatable1<T>(IList<T> data, string mobColumnName, string cmobColumnName,string updateval)
        //{
        //    PropertyDescriptorCollection props =
        //        TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    //for (int i = 0; i < props.Count; i++)
        //    //{
        //    //    PropertyDescriptor prop = props[i];
        //    //    table.Columns.Add(prop.Name, prop.PropertyType);
        //    //}
        //    table.Columns.Add(mobColumnName, typeof(String));
        //    table.Columns.Add(cmobColumnName, typeof(String));
        //    object[] values = new object[props.Count + 1];
        //    foreach (T item in data)
        //    {
        //        for (int i = 0; i < values.Length - 1; i++)
        //        {
        //            values[i] = props[i].GetValue(item);
        //        }
        //        values[values.Length - 1] = updateval;
        //        table.Rows.Add(values);
        //    }
        //    return table;
        //}

        //public static DataTable FullOuterJoinDataTables(params DataTable[] datatables) // supports as many datatables as you need.
        //{
        //    DataTable result = datatables.First().Clone();

        //    var commonColumns = result.Columns.OfType<DataColumn>();

        //    foreach (var dt in datatables.Skip(1))
        //    {
        //        commonColumns = commonColumns.Intersect(dt.Columns.OfType<DataColumn>(), new DataColumnComparer());
        //    }

        //    result.PrimaryKey = commonColumns.ToArray();

        //    foreach (var dt in datatables)
        //    {
        //        result.Merge(dt, false, MissingSchemaAction.AddWithKey);
        //    }

        //    return result;
        //} 


        public static int CreditsCountValidation(this string mesg, int langId)
        {
            // english 160, other than english - 70
            var creditRange = langId == 2 ? 70 : 160;
            //if (langId == 2 && mesg.Length > creditRange)
            //    creditRange = 67;
            //if (langId == 1 && mesg.Length > creditRange)
            //    creditRange = 153;
            creditRange = (langId == 2 && mesg.Length > creditRange) ? 67 : ((langId == 1 && mesg.Length > creditRange) ? 153 : creditRange);
            int ccredits = (int)Math.Ceiling((double)mesg.Length / creditRange);
            return ccredits;
        }


        #region File To XML

        public static List<KeyValuePair<string, string>> BuildXmlFromFile(string fileName, string filePath, string userId, string UserName, string campaignId,
            string sender, string message, string mobColumnName, int languageId, List<string> fileNames, string SheetName, int isPromo, bool AllowDuplicates = false, bool isCustome = false)
        {

            if (!string.IsNullOrWhiteSpace(fileName))
            {

                var extension = Path.GetExtension(fileName);
                switch (extension)
                {
                    case ".csv":
                    case ".txt":
                        return BuildCsvToXml(fileName, filePath, userId, UserName, campaignId, sender, message, mobColumnName, languageId,
                            isCustome, fileNames, SheetName, isPromo, AllowDuplicates);
                    case ".xls":
                    case ".xlsx":
                        return BuildXlsToXml(fileName, filePath, userId, UserName, campaignId, sender, message, mobColumnName, languageId,
                            isCustome, fileNames, SheetName, isPromo, AllowDuplicates);

                }
            }
            return new List<KeyValuePair<string, string>>();
        }

        public static string validateMessage(this string message, int lid)
        {
            var msg = message.Trim();
            var svalcountspl = splCount(msg);
            var objCount = svalcountspl;
            var nlen = msg.ToArray().Length;
            var strCredits = 0;
            var divider1 = 0;
            double strNextCountOne = nlen;
            var nlangid = 0;
            if (nlangid == 0) { nlangid = 0; }
            //var regUnicode = @"[\u0600-\u06FF\s]+$";
            //  var regUnicode = "[\u0600-\u06FF-\u4E00-\u9FA5-\u0C00-\u0C7F]+";
            var regUnicode = "[^\u0000-\u007F]+";
            if (Regex.Match(msg, regUnicode).Length > 0)
            {
                divider1 = 67;
                if (nlen <= 70)
                {
                    strCredits = 1;
                    lid = 2;
                }
                else if (nlen > 70 && nlen <= 134)
                {
                    strCredits = 2;
                    lid = 2;
                }
                else
                {
                    var strTotal = Math.Round(strNextCountOne / divider1, 2);
                    strCredits = (int)Math.Ceiling(strTotal);
                    lid = 2;
                }
                nlangid = 2;
                lid = 2;
            }
            else
            {
                nlen = nlen + svalcountspl;
                divider1 = 153;
                if (nlen <= 160)
                { strCredits = 1; }
                else if (nlen > 160 && nlen <= 306)
                { strCredits = 2; }
                else
                {
                    var strTotal = Math.Round(strNextCountOne / divider1, 2);
                    strCredits = (int)Math.Ceiling(strTotal);
                }
                nlangid = 1;
                lid = 1;
            }
            if (nlen == 0)
            {
                strCredits = 0;
            }

            return Convert.ToString(strCredits) + "," + lid;
        }

        private static int splCount(string msg)
        {
            var regex = @"/([€|{}\[\]\\~^])/g";
            var allFoundCharacters = Regex.Matches(msg, regex);
            if (allFoundCharacters != null)
            {
                if (allFoundCharacters.Count > 0)
                {
                    return allFoundCharacters.Count * 1;
                }
            }
            else
            {
                return 0;
            }
            return 0;
        }

        public static List<KeyValuePair<string, string>> BuildCsvToXml(string fileName, string filePath, string userId, string UserName, string campaignId,
            string sender, string message, string mobColumnName, int languageId, bool isCustome, List<string> fileNames, string SheetName, int isPromo, bool AllowDuplicates)
        {
            var returnElements = new List<KeyValuePair<string, string>>();

            try
            {
                var dt = BuildCsvToTable(filePath);
                dt = dt.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value =>
                { return value.ToString().Length == 0; })).CopyToDataTable();

                dt.AsEnumerable().Where(row => row.Field<object>(mobColumnName).ToString().Trim().Length > 0)
               .Select(b => b[mobColumnName] = b[mobColumnName].ToString().Trim().TrimEnd(','))
               .ToList();

                var nonumbers = dt.Rows.Cast<DataRow>().Where(w => IsHavingSpecialChar(w[mobColumnName].ToString())).ToList();

                dt = nonumbers.CopyToDataTable();
                //To Remove if zeros/zero(0,0000) is there in the mobile number column
                var zeroRows = dt.Rows.Cast<DataRow>().Where(w => (Convert.ToInt64(w[mobColumnName]) == 0)).ToList();
                zeroRows.ForEach(f => dt.Rows.Remove(f));
                dt.AcceptChanges();

                StringBuilder xElement = new StringBuilder();// string.Empty;
                dynamic mobileColumn;
                if (!AllowDuplicates)
                {
                    List<DataRow> mobRows = new List<DataRow>();
                    var tcsv = fileNames.FirstOrDefault() + "_duplicate";
                    var InvalidMobsCollection = new List<string>();
                    var emptyMobileNo =
                        dt.Rows.Cast<DataRow>()
                           .Where(w => string.IsNullOrWhiteSpace(w[mobColumnName].ToString()))
                           .ToList();
                    if (emptyMobileNo.Any())
                    {
                        InvalidMobsCollection.AddRange(emptyMobileNo.Select(s => s[mobColumnName].ToString()));
                        emptyMobileNo.ForEach(f => dt.Rows.Remove(f));
                    }
                    var Duplicates = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s[mobColumnName].ToString()).ToList();
                    if (Duplicates.Any())
                    {


                        if (conCode != null && conCode == 91)
                        {
                            for (int i = 0; i < Duplicates.Count; i++)
                            {
                                if (Duplicates[i].Length == conMobLength)
                                {
                                    Duplicates[i] = Duplicates[i].Replace(Duplicates[i], (conCode.ToString() + Duplicates[i]));
                                }
                            }
                        }
                        //Specific Country code end
                        if (!isCustome)
                        {
                            // var t = fileNames.FirstOrDefault() + "_duplicate";
                            xElement.Clear();
                            var mobilJoinedString = string.Join(",", Duplicates);
                            returnElements.Add(new KeyValuePair<string, string>(tcsv,
                                "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "' priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                WebUtility.HtmlEncode(sender) +
                                "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                "' mobile=''><mobile>" + mobilJoinedString + "</mobile></sendsms></root>"));
                            mobRows = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();

                            //   var splitedFiles = new List<string>();
                            var cieledvalue = 0;
                            cieledvalue = (int)Math.Ceiling((double)mobRows.Count / (double)(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"]) : 25000));
                            var actualFileName = Path.GetFileNameWithoutExtension(fileName);
                            fileNames.Clear();
                            for (var i = 0; i < cieledvalue; i++)
                            {
                                fileNames.Add(actualFileName + "_" + i);
                            }
                        }
                        else
                        {

                            var messageTemplates = StringBetween(message, "<$", "$>");
                            xElement.Clear();
                            // var rows = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();
                            var rows = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Distinct().ToList();
                            //var rows1 = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Distinct().ToList();
                            foreach (DataRow row in rows)//.Where(row => messageTemplates.Any()))
                            {
                                var msg = message;
                                foreach (var item in messageTemplates)
                                {
                                    msg = msg.Replace("<$" + item + "$>", (row[item] != null && row[item].ToString() != "") ? row[item].ToString() : "");
                                }
                                //  var ccount = msg.CreditsCountValidation(languageId);
                                var lid = languageId;
                                var ccountlid = msg.validateMessage(lid).Split(',');
                                var ccount = ccountlid[0];
                                lid = Convert.ToInt32(ccountlid[1]);
                                languageId = lid;
                                msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                                if (conCode != null && conCode == 91)
                                {
                                    xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                    WebUtility.HtmlEncode(sender) +
                                    "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                    "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");

                                }
                                else
                                {
                                    xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                    WebUtility.HtmlEncode(sender) +
                                    "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                    "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                                }

                            }

                            returnElements.Add(new KeyValuePair<string, string>(tcsv,
                                "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                                "</root>"));
                            // mobRows = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Distinct().ToList();

                            mobRows = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();

                            //   var splitedFiles = new List<string>();
                            var cieledvalue = 0;
                            cieledvalue = (int)Math.Ceiling((double)mobRows.Count / (double)(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"]) : 25000));
                            var actualFileName = Path.GetFileNameWithoutExtension(fileName);
                            fileNames.Clear();
                            for (var i = 0; i < cieledvalue; i++)
                            {
                                fileNames.Add(actualFileName + "_" + i);
                            }

                        }
                    }
                    mobileColumn = (Duplicates.Any()) ? mobRows.Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList() : dt.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList();
                }
                else
                {
                    mobileColumn = dt.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList();
                }

                //var itrations = System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"]) : 25000; //(int)Math.Ceiling((double)mobileColumn.Count / (double)25000);//fileNames.Count);

                var itrations = (int)Math.Ceiling((double)mobileColumn.Count / (double)fileNames.Count);
                var currentItreation = 0;
                int topFiltercount = System.Configuration.ConfigurationManager.AppSettings["CampaignTopFiltercount"] != null ? Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CampaignTopFiltercount"]) : 100;

                if (!isCustome)
                {
                    for (int i = 0; i < mobileColumn.Count; i++)
                    {
                        if (mobileColumn[i].Length == conMobLength)
                        {
                            mobileColumn[i] = mobileColumn[i].Replace(mobileColumn[i], (conCode.ToString() + mobileColumn[i]));
                        }
                    }
                    int filecount = fileNames.Count();
                    int curfile = 1;
                    var top100file = fileNames.FirstOrDefault() + "_priority";
                    var mobileNumbersTopBottom100 = "";

                    foreach (var t in fileNames)
                    {
                        xElement.Clear();

                        if (curfile == 1)
                        {
                            if (mobileColumn.Count <= (2 * topFiltercount))
                            {
                                mobileNumbersTopBottom100 = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(itrations).ToList());
                                currentItreation = currentItreation + itrations;
                                curfile = curfile + 1;
                                break;
                            }
                            else
                            {
                                mobileNumbersTopBottom100 = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(topFiltercount).ToList());
                                int itrations1;
                                if (filecount > 1)
                                    itrations1 = itrations - topFiltercount;
                                else
                                    itrations1 = itrations - (2 * topFiltercount);

                                var mobileNumbers = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation + topFiltercount).Take(itrations1).ToList());
                                if (mobileNumbers.Trim() != "")
                                {
                                    returnElements.Add(new KeyValuePair<string, string>(t,
                                        "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                        "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                        "' mobile=''><mobile>" + mobileNumbers + "</mobile></sendsms></root>"));
                                    //currentItreation = currentItreation + itrations;
                                }
                                currentItreation = (currentItreation + topFiltercount) + itrations1;
                                curfile = curfile + 1;

                                //var selectmobilenos = mobileNumbers.Split(',');
                                //var top100nos = mobileNumbersTopBottom100.Split(',');

                                if (curfile > filecount)
                                    mobileNumbersTopBottom100 = mobileNumbersTopBottom100 + "," + string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(topFiltercount).ToList());
                            }

                        }
                        else if (curfile < filecount)
                        {
                            var mobileNumbers = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(itrations).ToList());

                            if (mobileNumbers.Trim() != "")
                            {
                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                    WebUtility.HtmlEncode(sender) +
                                    "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                    "' mobile=''><mobile>" + mobileNumbers + "</mobile></sendsms></root>"));
                                //currentItreation = currentItreation + itrations;
                            }
                            currentItreation = currentItreation + itrations;
                            curfile = curfile + 1;

                        }
                        else if (curfile == filecount)
                        {
                            // var mobileNumbers = string.Join(",", mobileColumn.Skip(currentItreation).Take(itrations).ToList());
                            var LastIterationNumbers = (mobileColumn as List<string>).Skip(currentItreation).Take(itrations).ToList();
                            var mobileNumbers = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(LastIterationNumbers.Count() - topFiltercount).ToList());

                            if (mobileNumbers.Trim() != "")
                            {
                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                    WebUtility.HtmlEncode(sender) +
                                    "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                    "' mobile=''><mobile>" + mobileNumbers + "</mobile></sendsms></root>"));
                                //currentItreation = currentItreation + itrations;
                            }

                            currentItreation = currentItreation + (LastIterationNumbers.Count - topFiltercount);
                            mobileNumbersTopBottom100 = mobileNumbersTopBottom100 + "," + string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(topFiltercount).ToList());
                            curfile = curfile + 1;
                        }
                    }

                    returnElements.Add(new KeyValuePair<string, string>(top100file,
                                      "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                      "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                      "' sender='" + WebUtility.HtmlEncode(sender) +
                                      "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                      "' mobile=''><mobile>" + WebUtility.HtmlEncode(mobileNumbersTopBottom100) + "</mobile></sendsms></root>"));

                }
                else //custom campaign
                {
                    var messageTemplates = StringBetween(message, "<$", "$>");
                    var top100file = fileNames.FirstOrDefault() + "_priority";
                    var rowstop100 = new List<DataRow>();
                    int filecount = fileNames.Count();
                    int curfile = 1;
                    var lid = languageId;

                    foreach (var t in fileNames)
                    {
                        xElement.Clear();

                        if (curfile == 1)
                        {
                            //var lid = languageId;
                            //languageId = lid;
                            var rows = new List<DataRow>();
                            if (dt.Rows.Count <= (2 * topFiltercount))
                            {

                                rowstop100 = (AllowDuplicates) ? dt.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList() :
                                dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                                .Skip(currentItreation)
                                                .Take(itrations)
                                                .ToList();

                                currentItreation = currentItreation + itrations;
                                curfile = curfile + 1;
                                break;
                            }
                            else
                            {
                                rowstop100 = (AllowDuplicates) ? dt.Rows.Cast<DataRow>().Skip(currentItreation).Take(topFiltercount).ToList() :
                                dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                                .Skip(currentItreation)
                                                .Take(topFiltercount)
                                                .ToList();
                                int itrations1;
                                if (filecount > 1)
                                    itrations1 = itrations - topFiltercount;
                                else
                                    itrations1 = itrations - (2 * topFiltercount);

                                rows = (AllowDuplicates) ? dt.Rows.Cast<DataRow>().Skip(currentItreation + topFiltercount).Take(itrations1).ToList() :
                                dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                             .Skip(currentItreation + topFiltercount)
                                             .Take(itrations1)
                                             .ToList();

                                if (rows.Count > 0)
                                {

                                    foreach (DataRow row in rows)//.Where(row => messageTemplates.Any()))
                                    {
                                        var msg = message;
                                        foreach (var item in messageTemplates)
                                        {
                                            msg = msg.Replace("<$" + item + "$>", row[item].ToString());
                                        }
                                        // var ccount = msg.CreditsCountValidation(languageId);
                                        lid = languageId;
                                        var ccountlid = msg.validateMessage(lid).Split(',');
                                        var ccount = ccountlid[0];
                                        lid = Convert.ToInt32(ccountlid[1]);
                                        languageId = lid;
                                        msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                                        if (conCode != null && conCode == 91)
                                        {
                                            xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                                WebUtility.HtmlEncode(sender) +
                                                "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                                "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                                        }
                                        else
                                        {
                                            xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                                WebUtility.HtmlEncode(sender) +
                                                "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                                "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                                        }
                                    }

                                    returnElements.Add(new KeyValuePair<string, string>(t,
                                        "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                                        "</root>"));
                                }
                                currentItreation = (currentItreation + topFiltercount) + (itrations1);
                                curfile = curfile + 1;

                                //var selectmobilenos = dt.Rows.Count;
                                //var top100nos = rowstop100.Count();

                                if (curfile > filecount)
                                    rowstop100.AddRange((AllowDuplicates) ? dt.Rows.Cast<DataRow>().Skip(currentItreation).Take(topFiltercount).ToList() :
                                    dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                            .Skip(currentItreation)
                                            .Take(topFiltercount)
                                            .ToList());

                            }
                        }
                        else if (curfile < filecount)
                        {
                            lid = languageId;
                            languageId = lid;
                            var rows = new List<DataRow>();
                            rows = (AllowDuplicates) ? dt.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList() :
                            dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                            .Skip(currentItreation)
                                            .Take(itrations)
                                            .ToList();
                            if (rows.Count > 0)
                            {
                                foreach (DataRow row in rows)//.Where(row => messageTemplates.Any()))
                                {
                                    var msg = message;
                                    foreach (var item in messageTemplates)
                                    {
                                        msg = msg.Replace("<$" + item + "$>", row[item].ToString());
                                    }
                                    // var ccount = msg.CreditsCountValidation(languageId);
                                    lid = languageId;
                                    var ccountlid = msg.validateMessage(lid).Split(',');
                                    var ccount = ccountlid[0];
                                    lid = Convert.ToInt32(ccountlid[1]);
                                    languageId = lid;
                                    msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                                    if (conCode != null && conCode == 91)
                                    {
                                        xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                            WebUtility.HtmlEncode(sender) +
                                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                            "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                                    }
                                    else
                                    {
                                        xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                            WebUtility.HtmlEncode(sender) +
                                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                            "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                                    }
                                }

                                returnElements.Add(new KeyValuePair<string, string>(t,
                                       "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                                       "</root>"));
                            }
                            currentItreation = currentItreation + itrations;
                            curfile = curfile + 1;
                        }
                        else if (curfile == filecount)
                        {
                            lid = languageId;
                            languageId = lid;
                            var rows = new List<DataRow>();

                            rows = (AllowDuplicates) ? dt.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations - topFiltercount).ToList() :
                            dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                            .Skip(currentItreation)
                                            .Take(itrations - topFiltercount)
                                            .ToList();

                            if (rows.Count > 0)
                            {

                                foreach (DataRow row in rows)//.Where(row => messageTemplates.Any()))
                                {
                                    var msg = message;
                                    foreach (var item in messageTemplates)
                                    {
                                        msg = msg.Replace("<$" + item + "$>", row[item].ToString());
                                    }
                                    // var ccount = msg.CreditsCountValidation(languageId);
                                    lid = languageId;
                                    var ccountlid = msg.validateMessage(lid).Split(',');
                                    var ccount = ccountlid[0];
                                    lid = Convert.ToInt32(ccountlid[1]);
                                    languageId = lid;
                                    msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                                    if (conCode != null && conCode == 91)
                                    {
                                        xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                            WebUtility.HtmlEncode(sender) +
                                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                            "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                                    }
                                    else
                                    {
                                        xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                            WebUtility.HtmlEncode(sender) +
                                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                            "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                                    }
                                }

                                returnElements.Add(new KeyValuePair<string, string>(t,
                                       "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                                       "</root>"));
                            }

                            currentItreation = currentItreation + (rows.Count);
                            curfile = curfile + 1;

                            rowstop100.AddRange((AllowDuplicates) ? dt.Rows.Cast<DataRow>().Skip(currentItreation).Take(topFiltercount).ToList() :
                            dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                            .Skip(currentItreation)
                                            .Take(topFiltercount)
                                            .ToList());
                        }
                    }

                    var lid100 = languageId;
                    languageId = lid100;

                    if (rowstop100.Count > 0)
                    {
                        xElement.Clear();
                        foreach (DataRow row in rowstop100)//.Where(row => messageTemplates.Any()))
                        {
                            var msg = message;
                            foreach (var item in messageTemplates)
                            {
                                msg = msg.Replace("<$" + item + "$>", row[item].ToString());
                            }
                            // var ccount = msg.CreditsCountValidation(languageId);
                            lid = languageId;
                            var ccountlid = msg.validateMessage(lid).Split(',');
                            var ccount = ccountlid[0];
                            lid = Convert.ToInt32(ccountlid[1]);
                            languageId = lid;
                            msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                            if (conCode != null && conCode == 91)
                            {
                                xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                    WebUtility.HtmlEncode(sender) +
                                    "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                    "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                            }
                            else
                            {
                                xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                    WebUtility.HtmlEncode(sender) +
                                    "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                    "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                            }
                        }

                        returnElements.Add(new KeyValuePair<string, string>(top100file,
                               "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                               "</root>"));
                    }

                }
            }
            //            var mobileNumbers = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(itrations).ToList());
            //            returnElements.Add(new KeyValuePair<string, string>(t,
            //                "<root iscustome='" + (isCustome == true ? "true" : "false") +
            //                "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
            //                WebUtility.HtmlEncode(sender) +
            //                "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
            //                "' mobile=''><mobile>" + mobileNumbers + "</mobile></sendsms></root>"));
            //            currentItreation = currentItreation + itrations;
            //        }

            //    }
            //    else
            //    {
            //        var messageTemplates = StringBetween(message, "<$", "$>");
            //        foreach (var t in fileNames)
            //        {
            //            xElement.Clear();
            //            // var rows = dt.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList();
            //            var rows = new List<DataRow>();
            //            rows = (AllowDuplicates) ? dt.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList() :
            //            dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
            //                            .Skip(currentItreation)
            //                            .Take(itrations)
            //                            .ToList();
            //            foreach (DataRow row in rows)//.Where(row => messageTemplates.Any()))
            //            {
            //                var msg = message;
            //                foreach (var item in messageTemplates)
            //                {
            //                    msg = msg.Replace("<$" + item + "$>", row[item].ToString());
            //                }
            //                // var ccount = msg.CreditsCountValidation(languageId);
            //                var lid = languageId;
            //                var ccountlid = msg.validateMessage(lid).Split(',');
            //                var ccount = ccountlid[0];
            //                lid = Convert.ToInt32(ccountlid[1]);
            //                languageId = lid;
            //                msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

            //                if (conCode != null && conCode == 91)
            //                {
            //                    xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
            //                        WebUtility.HtmlEncode(sender) +
            //                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
            //                        "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
            //                }
            //                else
            //                {
            //                    xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
            //                        WebUtility.HtmlEncode(sender) +
            //                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
            //                        "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
            //                }
            //            }

            //            returnElements.Add(new KeyValuePair<string, string>(t,
            //                "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
            //                "</root>"));
            //            currentItreation = currentItreation + itrations;
            //        }
            //    }
            //}
            catch (Exception ex)
            {
                Logger.ErrorFormat("BuildCsvToXml,While parsing message from bulk sms / custome sms fatal error throughing :: {0}",
                    ex.StackTrace);
            }
            return returnElements;
        }

        public static List<KeyValuePair<string, string>> BuildXlsToXml(string fileName, string filePath, string userId, string UserName, string campaignId,
            string sender, string message, string mobColumnName, int languageId, bool isCustome, List<string> fileNames, string SheetName, int isPromo, bool AllowDuplicates)
        {
            var returnElements = new List<KeyValuePair<string, string>>();
            var resultSet = new DataTable();
            var result = new DataSet();
            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    var extension = Path.GetExtension(fileName);
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
                            //result = GetFileDataFromFormatedExcel(stream);
                            break;
                    }

                    if (result.Tables.Count == 0)
                    {
                        /*  DataSet - Create column names from first row    */
                        excelReader.IsFirstRowAsColumnNames = true;
                        result = excelReader.AsDataSet();
                        excelReader.Close();
                    }
                    resultSet = !string.IsNullOrWhiteSpace(SheetName) ? result.Tables[SheetName] : result.Tables[0];

                    resultSet = resultSet.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value =>
                    { return value.ToString().Length == 0; })).CopyToDataTable();

                    var nullspaces = resultSet.Rows.Cast<DataRow>().Where(w => string.IsNullOrWhiteSpace(w[mobColumnName].ToString())).ToList();
                    nullspaces.ForEach(f => resultSet.Rows.Remove(f));

                    //To Remove if zeros/zero(0,0000) is there in the mobile number column
                    var zeroRows = resultSet.Rows.Cast<DataRow>().Where(w => (Convert.ToInt64(w[mobColumnName]) == 0)).ToList();
                    zeroRows.ForEach(f => resultSet.Rows.Remove(f));
                    resultSet.AcceptChanges();

                    resultSet.AsEnumerable().Where(row => row.Field<object>(mobColumnName).ToString().Trim().Length > 0)
                .Select(b => b[mobColumnName] = b[mobColumnName].ToString().Trim().TrimEnd(','))
                .ToList();

                    var nonumbers = resultSet.Rows.Cast<DataRow>().Where(w => IsHavingSpecialChar(w[mobColumnName].ToString())).ToList();

                    resultSet = nonumbers.CopyToDataTable();

                    // result = resultSet;
                    dynamic mobileColumn;
                    if (!AllowDuplicates)
                    {

                        List<DataRow> mobRows = new List<DataRow>();
                        var t = fileNames.FirstOrDefault() + "_duplicate";
                        var InvalidMobsCollection = new List<string>();
                        var emptyMobileNo =
                            resultSet.Rows.Cast<DataRow>()
                               .Where(w => string.IsNullOrWhiteSpace(w[mobColumnName].ToString()))
                               .ToList();
                        if (emptyMobileNo.Any())
                        {
                            InvalidMobsCollection.AddRange(emptyMobileNo.Select(s => s[mobColumnName].ToString()));
                            emptyMobileNo.ForEach(f => resultSet.Rows.Remove(f));
                        }

                        var Duplicates = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s[mobColumnName].ToString()).ToList();
                        if (Duplicates.Any())
                        {
                            if (conCode != null && conCode == 91)
                            {
                                for (int i = 0; i < Duplicates.Count; i++)
                                {
                                    if (Duplicates[i].Length == conMobLength)
                                    {
                                        Duplicates[i] = Duplicates[i].Replace(Duplicates[i], (conCode.ToString() + Duplicates[i]));
                                    }
                                }
                            }
                            if (!isCustome)
                            {
                                var mobilJoinedString = string.Join(",", Duplicates);
                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                    WebUtility.HtmlEncode(sender) +
                                    "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                    "' mobile=''><mobile>" + WebUtility.HtmlEncode(mobilJoinedString) + "</mobile></sendsms></root>"));
                                mobRows = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();
                                var cieledvalue = 0;
                                cieledvalue = (int)Math.Ceiling((double)mobRows.Count / (double)(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"]) : 25000));
                                var actualFileName = Path.GetFileNameWithoutExtension(fileName);
                                fileNames.Clear();
                                for (var i = 0; i < cieledvalue; i++)
                                {
                                    fileNames.Add(actualFileName + "_" + i);
                                }
                            }
                            else
                            {

                                var messageTemplates = StringBetween(message, "<$", "$>");
                                var lid = languageId;
                                languageId = lid;
                                // var rows = result.Tables[0].Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();
                                var rows = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s).ToList();
                                var el = from row in rows
                                         let msg //= message
                                         = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", (row[item] != null && row[item].ToString() != "") ? row[item].ToString() : ""))
                                         let ccountlid = msg.validateMessage(lid).Split(',')
                                         let ccount = ccountlid[0]
                                         select "<sendsms ccount='" + ccount + "' userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                         "' sender='" + WebUtility.HtmlEncode(sender) +
                                         "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                                         "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                                         "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                                         "'></sendsms>";

                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + string.Join("", el) +
                                    "</root>"));


                                //mobRows = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();
                                mobRows = result.Tables[0].Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();
                                var cieledvalue = 0;
                                cieledvalue = (int)Math.Ceiling((double)mobRows.Count / (double)(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"]) : 25000));
                                var actualFileName = Path.GetFileNameWithoutExtension(fileName);
                                fileNames.Clear();
                                for (var i = 0; i < cieledvalue; i++)
                                {
                                    fileNames.Add(actualFileName + "_" + i);
                                }
                            }

                        }
                        mobileColumn = (Duplicates.Any()) ? mobRows.Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList() : resultSet.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList();
                    }
                    else
                    {
                        mobileColumn = resultSet.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList();

                    }

                    if (resultSet != null)
                    {
                        // mobileColumn = result.Tables[0].Rows.Cast<DataRow>().Select(s => s[mobColumnName].ToString()).ToList();
                        // mobileColumn = (mobileColumn as List<string>).Where(x => !string.IsNullOrWhiteSpace(x));
                        if (conCode != null && conCode == 91)
                        {
                            for (int i = 0; i < mobileColumn.Count; i++)
                            {
                                if (mobileColumn[i].Length == conMobLength)
                                {
                                    mobileColumn[i] = mobileColumn[i].Replace(mobileColumn[i], (conCode.ToString() + mobileColumn[i]));
                                }
                            }
                        }

                        var xElement = string.Empty;
                        //var itrations = System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"]) : 25000;
                        var itrations = (int)Math.Ceiling((double)mobileColumn.Count / (double)fileNames.Count);
                        var currentItreation = 0;
                        int topFiltercount = System.Configuration.ConfigurationManager.AppSettings["CampaignTopFiltercount"] != null ? Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CampaignTopFiltercount"]) : 100;

                        if (!isCustome)
                        {
                            int filecount = fileNames.Count();
                            int curfile = 1;
                            var top100file = fileNames.FirstOrDefault() + "_priority";
                            var mobileNumbersTopBottom100 = "";

                            foreach (var t in fileNames)
                            {
                                if (curfile == 1)
                                {
                                    if (mobileColumn.Count <= (2 * topFiltercount))
                                    {
                                        mobileNumbersTopBottom100 = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(itrations).ToList());
                                        //  returnElements.Add(new KeyValuePair<string, string>(top100file,
                                        //"<root iscustome='" + (isCustome == true ? "true" : "false") +
                                        //"' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                        //"' sender='" + WebUtility.HtmlEncode(sender) +
                                        //"' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                        //"' mobile=''><mobile>" + WebUtility.HtmlEncode(mobileNumbersTopBottom100) + "</mobile></sendsms></root>"));

                                        currentItreation = currentItreation + itrations;
                                        curfile = curfile + 1;
                                        break;
                                    }
                                    else
                                    {
                                        mobileNumbersTopBottom100 = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(topFiltercount).ToList());
                                        int itrations1;
                                        if (filecount > 1)
                                            itrations1 = itrations - topFiltercount;
                                        else
                                            itrations1 = itrations - (2 * topFiltercount);

                                        var mobileNumbers = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation + topFiltercount).Take(itrations1).ToList());
                                        if (mobileNumbers.Trim() != "")
                                        {
                                            returnElements.Add(new KeyValuePair<string, string>(t,
                                                "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                                "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                                "' sender='" + WebUtility.HtmlEncode(sender) +
                                                "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                                "' mobile=''><mobile>" + WebUtility.HtmlEncode(mobileNumbers) + "</mobile></sendsms></root>"));
                                        }
                                        currentItreation = (currentItreation + topFiltercount) + itrations1;
                                        curfile = curfile + 1;

                                        var selectmobilenos = mobileNumbers.Split(',');
                                        var top100nos = mobileNumbersTopBottom100.Split(',');

                                        if (curfile > filecount)
                                            mobileNumbersTopBottom100 = mobileNumbersTopBottom100 + "," + string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(topFiltercount).ToList());
                                    }

                                }
                                else if (curfile < filecount)
                                {
                                    var mobileNumbers = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(itrations).ToList());

                                    if (mobileNumbers.Trim() != "")
                                    {
                                        returnElements.Add(new KeyValuePair<string, string>(t,
                                            "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                            "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                            "' sender='" + WebUtility.HtmlEncode(sender) +
                                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                            "' mobile=''><mobile>" + WebUtility.HtmlEncode(mobileNumbers) + "</mobile></sendsms></root>"));
                                    }
                                    currentItreation = currentItreation + itrations;
                                    curfile = curfile + 1;

                                }
                                else if (curfile == filecount)
                                {
                                    // var mobileNumbers = string.Join(",", mobileColumn.Skip(currentItreation).Take(itrations).ToList());

                                    var LastIterationNumbers = (mobileColumn as List<string>).Skip(currentItreation).Take(itrations).ToList();

                                    var mobileNumbers = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(LastIterationNumbers.Count() - topFiltercount).ToList());

                                    if (mobileNumbers.Trim() != "")
                                    {
                                        returnElements.Add(new KeyValuePair<string, string>(t,
                                            "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                            "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                            "' sender='" + WebUtility.HtmlEncode(sender) +
                                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                            "' mobile=''><mobile>" + WebUtility.HtmlEncode(mobileNumbers) + "</mobile></sendsms></root>"));
                                    }
                                    currentItreation = currentItreation + (LastIterationNumbers.Count - topFiltercount);


                                    mobileNumbersTopBottom100 = mobileNumbersTopBottom100 + "," + string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(topFiltercount).ToList());
                                    curfile = curfile + 1;
                                }
                            }

                            returnElements.Add(new KeyValuePair<string, string>(top100file,
                                      "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                      "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                      "' sender='" + WebUtility.HtmlEncode(sender) +
                                      "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                      "' mobile=''><mobile>" + WebUtility.HtmlEncode(mobileNumbersTopBottom100) + "</mobile></sendsms></root>"));
                        }
                        else //custom campaign
                        {
                            var messageTemplates = StringBetween(message, "<$", "$>");
                            var top100file = fileNames.FirstOrDefault() + "_priority";
                            var rowstop100 = new List<DataRow>();
                            int filecount = fileNames.Count();
                            int curfile = 1;
                            foreach (var t in fileNames)
                            {
                                //  var ccount = msg.validateMessage(lid);
                                if (curfile == 1)
                                {
                                    var lid = languageId;
                                    languageId = lid;
                                    var rows = new List<DataRow>();

                                    if (resultSet.Rows.Count <= (2 * topFiltercount))
                                    {

                                        rowstop100 = (AllowDuplicates) ? resultSet.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList() :
                                        resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                                        .Skip(currentItreation)
                                                        .Take(itrations)
                                                        .ToList();

                                        currentItreation = currentItreation + itrations;
                                        curfile = curfile + 1;
                                        break;
                                    }
                                    else
                                    {
                                        rowstop100 = (AllowDuplicates) ? resultSet.Rows.Cast<DataRow>().Skip(currentItreation).Take(topFiltercount).ToList() :
                                        resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                                        .Skip(currentItreation)
                                                        .Take(topFiltercount)
                                                        .ToList();

                                        int itrations1;
                                        if (filecount > 1)
                                            itrations1 = itrations - topFiltercount;
                                        else
                                            itrations1 = itrations - (2 * topFiltercount);

                                        rows = (AllowDuplicates) ? resultSet.Rows.Cast<DataRow>().Skip(currentItreation + topFiltercount).Take(itrations1).ToList() :
                                        resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                                        .Skip(currentItreation + topFiltercount)
                                                        .Take(itrations1)
                                                        .ToList();

                                        if (rows.Count > 0)
                                        {
                                            var el = from row in rows
                                                     let msg //= message
                                                             // = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", row[item].ToString()))
                                                       = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", (row[item] != null && row[item].ToString() != "") ? row[item].ToString() : ""))
                                                     //  let ccount=msg.CreditsCountValidation(languageId)
                                                     let ccountlid = msg.validateMessage(lid).Split(',')
                                                     let ccount = ccountlid[0]
                                                     select "<sendsms ccount='" + ccount + "' userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                                     "' sender='" + WebUtility.HtmlEncode(sender) + "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                                                     "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                                                     "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                                                     "'></sendsms>";

                                            returnElements.Add(new KeyValuePair<string, string>(t,
                                                "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + string.Join("", el) +
                                                "</root>"));
                                        }

                                        currentItreation = (currentItreation + topFiltercount) + (itrations1);
                                        curfile = curfile + 1;

                                        var selectmobilenos = resultSet.Rows.Count;
                                        var top100nos = rowstop100.Count();

                                        if (curfile > filecount)
                                            rowstop100.AddRange((AllowDuplicates) ? resultSet.Rows.Cast<DataRow>().Skip(currentItreation).Take(topFiltercount).ToList() :
                                            resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                                    .Skip(currentItreation)
                                                    .Take(topFiltercount)
                                                    .ToList());
                                    }
                                }
                                else if (curfile < filecount)
                                {
                                    var lid = languageId;
                                    languageId = lid;
                                    var rows = new List<DataRow>();
                                    rows = (AllowDuplicates) ? resultSet.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList() :
                                    resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                                    .Skip(currentItreation)
                                                    .Take(itrations)
                                                    .ToList();
                                    if (rows.Count > 0)
                                    {
                                        var el = from row in rows
                                                 let msg //= message
                                                         // = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", row[item].ToString()))
                                                   = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", (row[item] != null && row[item] != "") ? row[item].ToString() : ""))
                                                 //  let ccount=msg.CreditsCountValidation(languageId)
                                                 let ccountlid = msg.validateMessage(lid).Split(',')
                                                 let ccount = ccountlid[0]
                                                 select "<sendsms ccount='" + ccount + "' userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                                 "' sender='" + WebUtility.HtmlEncode(sender) + "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                                                 "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                                                 "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                                                 "'></sendsms>";

                                        returnElements.Add(new KeyValuePair<string, string>(t,
                                            "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + string.Join("", el) +
                                            "</root>"));
                                    }
                                    currentItreation = currentItreation + itrations;
                                    curfile = curfile + 1;

                                }
                                else if (curfile == filecount)
                                {
                                    var lid = languageId;
                                    languageId = lid;
                                    var rows = new List<DataRow>();

                                    rows = (AllowDuplicates) ? resultSet.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations - topFiltercount).ToList() :
                                    resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                                    .Skip(currentItreation)
                                                    .Take(itrations - topFiltercount)
                                                    .ToList();
                                    if (rows.Count > 0)
                                    {
                                        var el = from row in rows
                                                 let msg //= message
                                                         // = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", row[item].ToString()))
                                                   = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", (row[item] != null && row[item].ToString() != "") ? row[item].ToString() : ""))
                                                 //  let ccount=msg.CreditsCountValidation(languageId)
                                                 let ccountlid = msg.validateMessage(lid).Split(',')
                                                 let ccount = ccountlid[0]
                                                 select "<sendsms ccount='" + ccount + "' userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                                 "' sender='" + WebUtility.HtmlEncode(sender) + "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                                                 "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                                                 "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                                                 "'></sendsms>";

                                        returnElements.Add(new KeyValuePair<string, string>(t,
                                            "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + string.Join("", el) +
                                            "</root>"));
                                    }

                                    currentItreation = currentItreation + (rows.Count);

                                    curfile = curfile + 1;

                                    rowstop100.AddRange((AllowDuplicates) ? resultSet.Rows.Cast<DataRow>().Skip(currentItreation).Take(topFiltercount).ToList() :
                                    resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                                    .Skip(currentItreation)
                                                    .Take(topFiltercount)
                                                    .ToList());
                                }
                            }
                            var lid100 = languageId;
                            languageId = lid100;
                            var el100 = from row in rowstop100
                                        let msg //= message
                                                // = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", row[item].ToString()))
                                          = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", (row[item] != null && row[item] != "") ? row[item].ToString() : ""))
                                        //  let ccount=msg.CreditsCountValidation(languageId)
                                        let ccountlid = msg.validateMessage(lid100).Split(',')
                                        let ccount = ccountlid[0]
                                        select "<sendsms ccount='" + ccount + "' userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                        "' sender='" + WebUtility.HtmlEncode(sender) + "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                                        "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                                        "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                                        "'></sendsms>";

                            returnElements.Add(new KeyValuePair<string, string>(top100file,
                                "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + string.Join("", el100) +
                                "</root>"));
                        }
                        return returnElements;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("BuildXlsToXml,  While parsing message from bulk sms / custome sms fatal error throughing :: {0}", ex.StackTrace);
                //  Logger.ErrorFormat("While processing xls throuing fatal error :: {0} ", ex.StackTrace);
                //using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                //{
                //    var extension = Path.GetExtension(fileName);
                //    extension = extension.Equals(".xls") ? ".xlsx" : ".xls";
                //    IExcelDataReader excelReader = null;
                //    switch (extension)
                //    {
                //        case ".xls":
                //            /*  Reading from a binary Excel file ('97-2003 format; *.xls)   */
                //            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                //            break;
                //        case ".xlsx":
                //            /*  Reading from a OpenXml Excel file (2007 format; *.xlsx) */
                //            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                //            //result = GetFileDataFromFormatedExcel(stream);
                //            break;
                //    }
                //    /*  DataSet - Create column names from first row    */
                //    excelReader.IsFirstRowAsColumnNames = true;
                //    result = excelReader.AsDataSet();
                //    excelReader.Close();
                //    dynamic mobileColumn;
                //    if (!AllowDuplicates)
                //    {

                //        List<DataRow> mobRows = new List<DataRow>();
                //        var t = fileNames.FirstOrDefault() + "_duplicate";
                //        var InvalidMobsCollection = new List<string>();
                //        var emptyMobileNo =
                //            resultSet.Rows.Cast<DataRow>()
                //               .Where(w => string.IsNullOrWhiteSpace(w[mobColumnName].ToString()))
                //               .ToList();
                //        if (emptyMobileNo.Any())
                //        {
                //            InvalidMobsCollection.AddRange(emptyMobileNo.Select(s => s[mobColumnName].ToString()));
                //            emptyMobileNo.ForEach(f => resultSet.Rows.Remove(f));
                //        }
                //        var Duplicates = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s[mobColumnName].ToString()).ToList();
                //        if (Duplicates.Any())
                //        {

                //            //Indian Synapse
                //            if (conCode != null && conCode == 91)
                //            {
                //                for (int i = 0; i < Duplicates.Count; i++)
                //                {
                //                    if (Duplicates[i].Length == conMobLength)
                //                    {
                //                        Duplicates[i] = Duplicates[i].Replace(Duplicates[i], (conCode + Duplicates[i]));
                //                    }
                //                }
                //            }
                //            if (!isCustome)
                //            {
                //                var mobilJoinedString = string.Join(",", Duplicates);
                //                returnElements.Add(new KeyValuePair<string, string>(t,
                //                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                //                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                //                    WebUtility.HtmlEncode(sender) +
                //                    "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                //                    "' mobile=''><mobile>" + mobilJoinedString + "</mobile></sendsms></root>"));
                //                mobRows = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();
                //                var cieledvalue = 0;
                //                cieledvalue = (int)Math.Ceiling((double)mobRows.Count / (double)(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"]) : 25000));
                //                var actualFileName = Path.GetFileNameWithoutExtension(fileName);
                //                fileNames.Clear();
                //                for (var i = 0; i < cieledvalue; i++)
                //                {
                //                    fileNames.Add(actualFileName + "_" + i);
                //                }
                //            }
                //            else
                //            {
                //                var lid = languageId;
                //                languageId = lid;
                //                var messageTemplates = StringBetween(message, "<$", "$>");
                //                // var rows = result.Tables[0].Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();
                //                var rows = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Distinct().ToList();
                //                var el = from row in rows
                //                         let msg //= message
                //                         = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", (row[item] != null && row[item] != "") ? row[item].ToString() : ""))
                //                         // let ccount = msg.CreditsCountValidation(languageId)
                //                         let ccountlid = msg.validateMessage(lid).Split(',')
                //                         let ccount = ccountlid[0]

                //                         select "<sendsms ccount='" + ccount + "' userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                //                         "' sender='" + WebUtility.HtmlEncode(sender) +
                //                         "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                //                         "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                //                         "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                //                         "'></sendsms>";

                //                returnElements.Add(new KeyValuePair<string, string>(t,
                //                    "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + string.Join("", el) +
                //                    "</root>"));
                //                mobRows = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();

                //                //  mobRows = result.Tables[0].Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Distinct().ToList();
                //                var cieledvalue = 0;
                //                cieledvalue = (int)Math.Ceiling((double)mobRows.Count / (double)(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CampaignXMLGeneratedCount"]) : 25000));
                //                var actualFileName = Path.GetFileNameWithoutExtension(fileName);
                //                fileNames.Clear();
                //                for (var i = 0; i < cieledvalue; i++)
                //                {
                //                    fileNames.Add(actualFileName + "_" + i);
                //                }
                //            }

                //        }
                //        mobileColumn = (Duplicates.Any()) ? mobRows.Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList() : resultSet.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList();
                //    }
                //    else
                //    {
                //        mobileColumn = resultSet.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList();

                //    }

                //    if (resultSet != null)
                //    {
                //        //mobileColumn = result.Tables[0].Rows.Cast<DataRow>().Select(s => s[mobColumnName].ToString()).ToList();
                //        if (conCode != null && conCode == 91)
                //        {
                //            for (int i = 0; i < mobileColumn.Count; i++)
                //            {
                //                if (mobileColumn[i].Length == conMobLength)
                //                {
                //                    mobileColumn[i] = mobileColumn[i].Replace(mobileColumn[i], (conCode.ToString() + mobileColumn[i]));
                //                }
                //            }
                //        }

                //        var xElement = string.Empty;
                //        var itrations = (int)Math.Ceiling((double)mobileColumn.Count / (double)fileNames.Count);
                //        var currentItreation = 0;
                //        if (!isCustome)
                //        {
                //            foreach (var t in fileNames)
                //            {
                //                xElement = string.Empty;
                //                //  var mobileNumbers = string.Join(",", mobileColumn.Skip(currentItreation).Take(itrations).ToList());
                //                var mobileNumbers = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(itrations).ToList());
                //                returnElements.Add(new KeyValuePair<string, string>(t,
                //                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                //                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                //                    "' sender='" + WebUtility.HtmlEncode(sender) +
                //                    "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                //                    "' mobile=''><mobile>" + mobileNumbers + "</mobile></sendsms></root>"));
                //                currentItreation = currentItreation + itrations;
                //            }
                //        }
                //        else
                //        {
                //            var messageTemplates = StringBetween(message, "<$", "$>");
                //            foreach (var t in fileNames)
                //            {
                //                xElement = string.Empty;
                //                var rows = new List<DataRow>();
                //                rows = (AllowDuplicates) ? resultSet.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList() :
                //                resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                //                                .Skip(currentItreation)
                //                                .Take(itrations)
                //                                .ToList();
                //                foreach (DataRow row in rows)
                //                {

                //                    var msg = message;
                //                    //if (messageTemplates.Any())
                //                    //{

                //                    //}
                //                    var lid = languageId;
                //                    languageId = lid;
                //                    msg = messageTemplates.Aggregate(msg, (current, item) => current.Replace("<$" + item + "$>", row[item].ToString()));
                //                    var ccountlid = msg.validateMessage(lid).Split(',');
                //                    var ccount = ccountlid[0];
                //                    xElement += "<sendsms  ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                //                                "' sender='" +
                //                                WebUtility.HtmlEncode(sender) +
                //                                "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                //                                "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                //                                "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                //                                "'></sendsms>";
                //                }
                //                returnElements.Add(new KeyValuePair<string, string>(t,
                //                    "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement +
                //                    "</root>"));
                //                currentItreation = currentItreation + itrations;
                //            }
                //        }
                //        return returnElements;
                //    }
                //}
            }
            return returnElements;
        }

        private static string GetColumnName(string cellReference)
        {
            var regex = new Regex("[A-Za-z]+");
            var match = regex.Match(cellReference);

            return match.Value;
        }

        public static DataSet GetFileDataFromFormatedExcel(FileStream fs)
        {
            var ds = new DataSet();
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
            {
                WorkbookPart workbookPart = doc.WorkbookPart;
                //SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                ///SharedStringTable sst = sstpart.SharedStringTable;

                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                Worksheet sheet = worksheetPart.Worksheet;

                var cells = sheet.Descendants<Cell>();
                var rows = sheet.Descendants<Row>();

                Console.WriteLine("Row count = {0}", rows.LongCount());
                Console.WriteLine("Cell count = {0}", cells.LongCount());

                var firstrow = rows.First();
                DataTable table1 = new DataTable(sheet.LocalName);
                foreach (Cell c in firstrow.Elements<Cell>())
                {
                    //if ((c.DataType != null) && (c.DataType == CellValues.SharedString))
                    //{
                    //    int ssid = int.Parse(c.CellValue.Text);
                    //}
                    //else 
                    if (c.CellValue != null)
                    {
                        table1.Columns.Add(c.CellValue.Text);
                        Console.WriteLine("Cell contents: {0}", c.CellValue.Text);
                    }
                }
                var allrows = rows.Skip(1).ToList().Select(s => BuildRow(table1, s));
                //foreach (var item in allrows)
                //{
                //    table1.ImportRow(item);
                //} 
                var i = 0;
                foreach (Row row in rows.Skip(1))
                {
                    i = 0;
                    DataRow dtrow = table1.NewRow();
                    foreach (Cell c in row.Elements<Cell>())
                    {
                        if (c.CellValue != null)
                        {
                            dtrow[i] = c.CellValue.Text;
                        }
                        i++;
                    }
                    table1.Rows.Add(dtrow);
                }
                //foreach (Row row in from row in rows.Skip(1) let dtrow = BuildRow(table1, row) select row)
                //{
                //    table1.Rows.Add(BuildRow(table1, row));
                //} 
                ds.Tables.Add(table1);
            }
            return ds;
        }

        private static DataRow BuildRow(DataTable table, Row row)
        {
            var i = 0;
            var dtrow = table.NewRow();
            foreach (Cell c in row.Elements<Cell>())
            {
                if (c.CellValue != null)
                {
                    dtrow[i] = c.CellValue.Text;
                }
                i++;
            }
            return dtrow;
        }

        public static DataTable BuildTableFromLinesOfList(List<string> source, string columnName)
        {
            var dtCsv = new DataTable();
            try
            {
                dtCsv.Columns.Add(columnName);
                var bodyRows = source;
                foreach (var row in bodyRows)
                {
                    DataRow dr = dtCsv.NewRow();
                    var colrow = row.Trim().TrimEnd(',').Split(',');
                    for (var ind = 0; ind < colrow.Length; ind++)
                    {
                        dr[ind] = colrow[ind].ToString();
                    }
                    dtCsv.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("BuildTableFromLinesOfList :: " + ex.ToString());
            }
            return dtCsv;
        }

        public static DataTable BuildCsvToTable(string filePath, string sheetName = "")
        {
            var dtCsv = new DataTable();
            string colrowArrayForLog = string.Empty;
            try
            {
                var extension = Path.GetExtension(filePath);
                var result = new DataSet();
                if (extension.Equals(".xls") || extension.Equals(".xlsx"))
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
                                //  result = GetFileDataFromFormatedExcel(stream);
                                break;
                        }
                        if (excelReader != null)
                        {
                            /*  DataSet - Create column names from first row    */
                            excelReader.IsFirstRowAsColumnNames = true;
                            result = excelReader.AsDataSet();
                            excelReader.Close();
                            if (result.Tables[sheetName] == null)
                            {
                                sheetName = "Table1";
                                if (result.Tables[sheetName] == null) { sheetName = ""; }
                            }
                            if (sheetName != string.Empty && sheetName != null && sheetName != "")
                            {
                                dtCsv = !string.IsNullOrWhiteSpace(sheetName) ? result.Tables[sheetName] : result.Tables[0];
                                return dtCsv;
                            }
                            else
                                return dtCsv = new DataTable();
                            //DataTable dt = dtCsv.Copy();
                            //dtCsv = dt.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value => 
                            //       {return value.ToString().Length == 0; })).CopyToDataTable();
                        }
                    }
                }
                else
                {
                    var lines = File.ReadAllLines(filePath);
                    var headers = lines[0].Split(',');
                    //if (headers.Length == 1)
                    //{
                    //    var res = headers[0].All(char.IsDigit);
                    //}
                    //else
                    //{
                    //    foreach (var hrows in headers)
                    //    {
                    //        dtCsv.Columns.Add(hrows);
                    //    }
                    //}

                    foreach (var hrows in headers)
                    {
                        dtCsv.Columns.Add(hrows);
                    }

                    var bodyRows = lines.Skip(1);
                    foreach (var row in bodyRows)
                    {
                        DataRow dr = dtCsv.NewRow();
                        //var colrow = row.Trim().TrimEnd(',').Split(',');
                        //for (var ind = 0; ind < colrow.Length; ind++)
                        //{
                        //    dr[ind] = colrow[ind].ToString();
                        //}
                        //  Regex regx = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                        var colrow = row.Trim().TrimEnd(',').Split(',');
                        //  var colrow = regx.Split(row.Trim().TrimEnd(','));
                        for (var ind = 0; ind < colrow.Length; ind++)
                        {
                            // dr[ind] = WebUtility.HtmlEncode(colrow[ind]).ToString();
                            dr[ind] = colrow[ind].ToString();
                            colrowArrayForLog = colrow[0].ToString();
                        }
                        dtCsv.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.StackTrace;
                var extension = Path.GetExtension(filePath);
                Logger.Error("CSV File Process Error @: " + colrowArrayForLog);                
                var result = new DataSet();
                extension = extension.Equals(".xls") ? ".xlsx" : ".xls";
                if (extension.Equals(".xls") || extension.Equals(".xlsx"))
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
                                //  result = GetFileDataFromFormatedExcel(stream);
                                break;
                        }
                        if (excelReader != null)
                        {
                            /*  DataSet - Create column names from first row    */
                            excelReader.IsFirstRowAsColumnNames = true;
                            result = excelReader.AsDataSet();
                            excelReader.Close();
                            // return dtCsv = result.Tables[0];
                            if (sheetName != string.Empty && sheetName != null && sheetName != "")
                            {
                                return dtCsv = !string.IsNullOrWhiteSpace(sheetName) ? result.Tables[sheetName] : result.Tables[0];
                            }
                            else
                                return dtCsv = new DataTable();
                        }
                    }
                }
            }
            return dtCsv;
        }

        public static List<string> StringBetween(this string source, string start, string end)
        {
            var results = new List<string>();

            var pattern = string.Format(
                "{0}({1}){2}",
                Regex.Escape(start),
                ".+?",
                 Regex.Escape(end));

            foreach (Match m in Regex.Matches(source, pattern))
            {
                results.Add(m.Groups[1].Value);
            }

            return results;
        }

        public static IEnumerable<T> Duplicates<T>(this IEnumerable<T> source, bool distinct = true)
        {
            // select the elements that are repeated
            IEnumerable<T> result = source.GroupBy(a => a).SelectMany(a => a.Skip(1));
            // distinct?
            if (distinct == true)
            {
                // deferred execution helps us here
                result = result.Distinct();
            }

            return result;
        }

        //public static string IsSpecialCharHaving(string value)
        //{
        //    return value.All(char.IsDigit) ? string.Empty : value;
        //}

        public static string IsSpecialCharHaving(string value)
        {
            return value.All(char.IsDigit) ? string.Empty : string.Empty;
        }

        public static bool IsHavingSpecialChar(string value)
        {
            return value.All(char.IsDigit);
        }


        public static int IsHavingAttheRate(string value)
        {
            return value.Contains('@') || value.Contains('}') || value.Contains('{') ? 1 : 1;

        }

        #endregion



        public static LocalizationResponse BuildLocalizations(string lang)
        {
            var response = new LocalizationResponse();
            var responseProperties = response.GetType().GetProperties();
            switch (lang)
            {
                case "en":
                    var target = new Synapse_EN();
                    var enproperties = target.GetType().GetProperties();
                    foreach (var propety in responseProperties)
                    {
                        var firstprop = enproperties.FirstOrDefault(f => f.Name.Equals(propety.Name));
                        var tvalue = firstprop.GetValue(target, null);
                        propety.SetValue(response, tvalue, null);
                    }
                    break;
                case "ab":
                    var targetab = new Synapse_AB();
                    var abproperties = targetab.GetType().GetProperties();
                    foreach (var propety in responseProperties)
                    {
                        var firstprop = abproperties.FirstOrDefault(f => f.Name.Equals(propety.Name));
                        var tvalue = firstprop.GetValue(targetab, null);

                        propety.SetValue(response, tvalue, null);

                    }
                    break;
            }
            return response;
        }


    }
}
