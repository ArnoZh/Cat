using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Cat.Extend
{
    public static class ConvertExtend
    {
        /// <summary>
        /// IDataReader->>>system.data.Datatable
        /// </summary>
        /// <param name="_reader"></param>
        /// <returns></returns>
        public static DataTable GetTable(this IDataReader _reader)
        {
            DataTable _schema_table = _reader.GetSchemaTable();//源框架table
            DataTable dt = new DataTable();
            string[] arrayList = new string[_schema_table.Rows.Count];
            //组织结构
            for (int i = 0; i < _schema_table.Rows.Count; i++)
            {
                DataColumn dataColumn = new DataColumn();
                if (!dt.Columns.Contains(_schema_table.Rows[i]["ColumnName"].ToString()))
                {
                    dataColumn.ColumnName = _schema_table.Rows[i]["ColumnName"].ToString();
                    dataColumn.Unique = Convert.ToBoolean(_schema_table.Rows[i]["IsUnique"]);
                    dataColumn.AllowDBNull = Convert.ToBoolean(_schema_table.Rows[i]["AllowDBNull"]);
                    dataColumn.ReadOnly = Convert.ToBoolean(_schema_table.Rows[i]["IsReadOnly"]);
                    dataColumn.DataType = (Type)_schema_table.Rows[i]["DataType"];
                    arrayList[i] = dataColumn.ColumnName;
                    dt.Columns.Add(dataColumn);
                }
            }
            //填值
            dt.BeginLoadData();
            while (_reader.Read())
            {
                DataRow dataRow = dt.NewRow();
                for (int j = 0; j < arrayList.Length; j++)
                {
                    dataRow[arrayList[j]] = _reader[arrayList[j]];
                }
                dt.Rows.Add(dataRow);
            }
            _reader.Close();
            dt.EndLoadData();
            return dt;
        }

        /// <summary>
        /// SliceDatatable
        /// </summary>
        /// <param name="_dataTable">需要切的源表</param>
        /// <param name="_maxCount">分表中最大行数</param>
        /// <returns></returns>
        private static List<DataTable> SliceDatatable(this DataTable _dataTable, int _maxCount)
        {
            //10万行切成1000一个片（共10片）,耗时340ms左右
            int rowCount = _dataTable.Rows.Count;
            List<DataTable> tables = new List<DataTable>();
            int index = 0;
            do
            {
                DataTable dt = _dataTable.Clone();
                for (int i = 0; i < _maxCount; i++)
                {
                    if (index < rowCount)//加入
                    {
                        dt.ImportRow((DataRow)_dataTable.Rows[index]);
                    }
                    index++;
                }
                tables.Add(dt);
            } while (index < rowCount);
            return tables;
        }

        /// <summary>
        /// color -> hex8
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHex8(this Color color)
        {
            int r = Mathf.RoundToInt(color.r * 255.0f);
            int g = Mathf.RoundToInt(color.g * 255.0f);
            int b = Mathf.RoundToInt(color.b * 255.0f);
            int a = Mathf.RoundToInt(color.a * 255.0f);
            string hex = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
            return hex;
        }

        /// <summary>
        /// hex8或者hex6 ->color
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Color ToColor(this string hex)
        {
            if (hex.Length != 6 && hex.Length != 8)
                throw new System.ArgumentException(nameof(ConvertExtend));
            byte br = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte bg = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte bb = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte cc = byte.Parse("255");
            if (hex.Length > 6)
            {
                cc = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            }
            float r = br / 255f;
            float g = bg / 255f;
            float b = bb / 255f;
            float a = cc / 255f;
            return new Color(r, g, b, a);
        }
    }
}
