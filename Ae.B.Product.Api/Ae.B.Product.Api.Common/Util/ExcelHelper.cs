using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ae.B.Product.Api.Common.Util
{
  
    /// <summary>
    /// 导入导出Excel操作
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 导出操作
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="fileName">带有文件后缀扩展的文件名,可以是.xls或者.xlsx</param>
        /// <returns></returns>
        public static Stream Report<T>(IList<T> source, string fileName) where T : new()
        {
            if (fileName.HasText())
            {
                bool rightFile = fileName.Trim().EndsWith(".xls");
                if (!rightFile)
                    throw new ArgumentException("file's extension type must be '.xls' type.");
                var transForm = new Transform<T>();
                var workbook = transForm.ToWorkbook(source, fileName);
                return transForm.WriteWorkbookToStream(workbook);
            }
            else
            {
                throw new ArgumentNullException("fileName", "fileName don't be allowed null or empty.");
            }
        }
        /// <summary>
        /// 导入操作
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="fileName">带有文件后缀扩展的文件名,以.xls结尾的</param>
        /// <param name="startRow">开始转换的数据行</param>
        /// <param name="valueConverter">格式转换委托</param>
        /// <returns></returns>
        public static IList<T> Import<T>(Stream source, string fileName, int startRow = 1, ValueConverter valueConverter = null) where T : new()
        {
            if (fileName.HasText())
            {
                bool rightFile = fileName.Trim().EndsWith(".xls") || fileName.Trim().EndsWith(".xlsx");
                if (!rightFile)
                    throw new ArgumentException("file's extension type must be '.xls' or '.xlsx' type.");

                var transForm = new Transform<T>();
                return transForm.ToCollection(source, fileName, startRow, valueConverter);
            }
            else
            {
                throw new ArgumentNullException("fileName", "fileName don't be allowed null or empty.");
            }
        }
    }
}
