using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using tradegecko.fileprocessor.Domain.Entities;

namespace tradegecko.fileprocessor.Domain.Helpers
{
    public static class CSVDataHelper
    {
        public static ObjectTransaction GetObjectTransactionFromString(string[] columnNames, string[] dataParams)
        {
            var dataObj = new ObjectTransaction();
            foreach (var columnName in columnNames)
            {
                var data = dataParams[Array.IndexOf(columnNames, columnName)];
                data = data == "" ? null : data;
                MapPropertyData(columnName, data, dataObj);
            }

            return dataObj;
        }

        static Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);

        public static string[] SplitCSV(string input)
        {

            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length)
                {
                    list.Add("");
                }

                list.Add(curr.TrimStart(','));
            }

            return list.ToArray();
        }

        private static void MapPropertyData(string columnName, string columnData, ObjectTransaction dataObj)
        {
            switch (columnName)
            {
                case "object_id":
                    SetObjectProperty("ObjectId", dataObj, columnData);
                    break;
                case "object_type":
                    SetObjectProperty("ObjectType", dataObj, columnData);
                    break;
                case "timestamp":
                    SetObjectTimeProperty("Timestamp", dataObj, columnData);
                    break;
                case "object_changes":
                    SetObjectProperty("ObjectChanges", dataObj, columnData);
                    break;
                default:
                    break;
            }
        }

        private static void SetObjectTimeProperty(string propertyName, ObjectTransaction dataObj, string columnData)
        {
            var converData = Convert.ToInt32(columnData);
            var convertedDate = DateTimeOffset.FromUnixTimeSeconds(converData).DateTime;
            dataObj.GetType().GetProperty(propertyName).SetValue(dataObj, convertedDate);
        }

        private static void SetObjectProperty(string propertyName, ObjectTransaction dataObj, string columnData)
        {
            var prop = dataObj.GetType().GetProperty(propertyName);
            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (string.IsNullOrEmpty(columnData))
                {
                    prop.SetValue(dataObj, null);
                }
                else
                {
                    var type2 = Nullable.GetUnderlyingType(prop.PropertyType);
                    var convertedData = Convert.ChangeType(columnData, type2);
                    prop.SetValue(dataObj, convertedData);
                }
            }
            else
            {
                var convertedData = Convert.ChangeType(columnData, prop.PropertyType);
                prop.SetValue(dataObj, convertedData);
            }
        }
    }
}
