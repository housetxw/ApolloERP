using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace Ae.B.Product.Api.Common.Util
{
    internal class Transform<T> : ITransformCollection<T>, ITransformExcel<T> where T : new()
    {
        /// <summary>
        /// Excel最大行数
        /// </summary>
        internal const int DefaultMaxRow = 65535;
        /// <summary>
        /// 集合数据转换为电子表本
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public IWorkbook ToWorkbook(IList<T> source, string fileName)
        {
            if (source == null)
            {
                throw new ArgumentNullException("ToWorkbook source null error");
            }
            //遍历T的Property 获取Property上面的标记信息
            Tuple<bool, ExcelColumnAttribute[], PropertyInfo[]> searchResult = ExcelRelationType.GetAttributes<T, ExcelColumnAttribute>();
            var haventCols = searchResult.Item1;
            var properties = searchResult.Item3;
            var attributes = searchResult.Item2;
            //设置Sheet方式,数据填入到Sheet中去
            var workbook = InitializeWorkbook(fileName);
            //获取最大的行数
            int maxRowSize = DefaultMaxRow;
            ExcelSheetAttribute sheetAttr = ExcelRelationType.GetClassAttribute<T, ExcelSheetAttribute>();
            if (sheetAttr != null) { maxRowSize = sheetAttr.MaxRow; }
            //轮训的创建Sheet集合
            if (source.Count <= maxRowSize)
            {
                CreateSheet(workbook, source, haventCols, properties, attributes);
            }
            else
            {
                int sourceCount = source.Count;
                int modValue = sourceCount % maxRowSize;
                int times = sourceCount / maxRowSize;
                if (modValue != 0)
                    times += 1;
                for (int i = 0; i < times; i++)
                {
                    int start = i * maxRowSize;
                    if ((i == times - 1) && (modValue != 0))
                    {
                        IList<T> inputSource = ((List<T>)source).GetRange(start, modValue);
                        CreateSheet(workbook, inputSource, haventCols, properties, attributes);
                    }
                    else
                    {
                        IList<T> inputSource = ((List<T>)source).GetRange(start, maxRowSize);
                        CreateSheet(workbook, inputSource, haventCols, properties, attributes);
                    }
                }
            }
            return workbook;
        }

        /// <summary>
        /// 创建Sheet
        /// </summary>
        private void CreateSheet(IWorkbook workbook, IList<T> source, bool haventCols, PropertyInfo[] properties, ExcelColumnAttribute[] attributes)
        {
            var sheet = workbook.CreateSheet();
            //设置值
            int rowIndex = 1;
            ExcelRelationType.PutValueToCell<T>(source, sheet, ref rowIndex, haventCols, properties, attributes);
            //设置样式
            ExcelRelationType.SetSheetStyle(workbook, sheet, ref rowIndex, attributes, haventCols);
            //设置第一行样式与内容
            ExcelRelationType.SetHeaderContent(workbook, sheet, haventCols, properties, attributes);
            //设置表达式列和行,设置冻结列和行
            if (rowIndex > 0)
            {
                //设置静态列,统计表达式列
                var statistics = typeof(T).GetCustomAttributes(typeof(StatisticsAttribute), true) as StatisticsAttribute[];
                if (statistics != null && statistics.Length > 0)
                {
                    var first = statistics[0];
                    var lastRow = sheet.CreateRow(rowIndex);
                    var cell = lastRow.CreateCell(0);
                    cell.SetCellValue(first.Name);
                    foreach (var column in first.Columns)
                    {
                        cell = lastRow.CreateCell(0);
                        // cell.CellFormula = $"{first.Formula}({GetCellPosition(1, column)}:{GetCellPosition(rowIndex - 1, column)})";
                    }
                }
                //设置冻结列
                var fattrs = typeof(T).GetCustomAttributes(typeof(FreezeAttribute), true) as FreezeAttribute[];
                if (fattrs != null && fattrs.Length > 0)
                {
                    var freeze = fattrs[0];
                    sheet.CreateFreezePane(freeze.ColSplit, freeze.RowSplit, freeze.LeftMostColumn, freeze.TopRow);
                }
                //设置过滤
                var filters = typeof(T).GetCustomAttributes(typeof(FilterAttribute), true) as FilterAttribute[];
                if (filters != null && filters.Length > 0)
                {
                    var filter = filters[0];
                    sheet.SetAutoFilter(new CellRangeAddress(filter.FirstRow, filter.LastRow ?? rowIndex, filter.FirstCol, filter.LastCol));
                }
            }
            //列设置自动大小
            for (int i = 0, length = properties.Length; i < length; i++)
            {
                sheet.AutoSizeColumn(i);
            }
        }
        /// <summary>
        /// 电子表本转换为Stream
        /// </summary>
        /// <param name="workbook">电子表本</param>
        /// <returns></returns>
        public Stream WriteWorkbookToStream(IWorkbook workbook)
        {
            if (workbook == null)
            {
                throw new ArgumentNullException("WriteWorkbookToStream workbook null error");
            }
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// 将Excel数据流转换为IList的数据集合
        /// </summary>
        /// <param name="source">Excel数据流</param>
        /// <param name="fileName">Filename文件的名称</param>
        /// <param name="startRow">开始转换的起始行</param>
        /// <param name="valueConverter">转换委托</param>
        /// <returns></returns>
        public IList<T> ToCollection(Stream source, string fileName, int startRow = 1, ValueConverter valueConverter = null)
        {
            //开始从Stream中获取数据集合
            IList<T> list = new List<T>();
            IWorkbook workbook = InitializeWorkbook(fileName, source);
            int sheetCount = workbook.NumberOfSheets;
            for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
            {
                ISheet sheet = workbook.GetSheetAt(sheetIndex);
                var rows = sheet.GetRowEnumerator();

                //遍历T的Property 获取Property上面的标记信息
                Tuple<bool, ExcelColumnAttribute[], PropertyInfo[]> searchResult = ExcelRelationType.GetAttributes<T, ExcelColumnAttribute>();
                var haventCols = searchResult.Item1;
                var properties = searchResult.Item3;
                var attributes = searchResult.Item2;

                while (rows.MoveNext())
                {
                    IRow row = rows.Current as IRow;

                    if (row.RowNum < startRow)
                    {
                        continue;
                    }
                    T item = new T();
                    for (int i = 0, l = properties.Length; i < l; i++)
                    {
                        PropertyInfo prop = properties[i];

                        //确定Property对应在Excel中列的位置
                        int index = i;
                        if (!haventCols)
                        {
                            var column = attributes[i];
                            if (column == null)
                                continue;
                            else
                                index = column.Index;
                        }

                        var value = row.GetCellValue(index);
                        if (valueConverter != null)
                        {
                            value = valueConverter(row.RowNum, index, value);
                        }
                        if (value != null)
                        {
                            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            var safeValue = Convert.ChangeType(value, propType, CultureInfo.CurrentCulture);
                            prop.SetValue(item, safeValue, null);
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 初始化带有公司名称,作者,主题的文档
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        private static IWorkbook InitializeWorkbook(string fileName, Stream dataSource = null)
        {
            string fileExtension = Path.GetExtension(fileName);
            IWorkbook workbook = null;
            switch (fileExtension)
            {
                case ".xls":
                    if (dataSource == null)
                    {
                        workbook = new HSSFWorkbook();
                    }
                    else
                    {
                        workbook = new HSSFWorkbook(dataSource);
                    }

                    break;
                case ".xlsx":
                    if (dataSource == null)
                    {
                        workbook = new XSSFWorkbook();
                    }
                    else
                    {
                        workbook = new XSSFWorkbook(dataSource);
                    }
                    break;
                default:
                    break;
            }
            return workbook;
        }

        /// <summary>
        /// 计算表达式中行列的表示符
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <returns></returns>
        private static string GetCellPosition(int row, int col)
        {
            col = Convert.ToInt32('A') + col;
            row = row + 1;
            return ((char)col) + row.ToString();
        }
    }

    /// <summary>
    /// 转化接口 来源集合
    /// </summary>
    public interface ITransformCollection<T> where T : new()
    {
        /// <summary>
        /// 泛类型转换为Workbook
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        IWorkbook ToWorkbook(IList<T> source, string fileName);
        /// <summary>
        /// 将Workbook写入到流中
        /// </summary>
        /// <param name="workbook">Workbook</param>
        /// <returns></returns>
        Stream WriteWorkbookToStream(IWorkbook workbook);
    }


    /// <summary>
    /// 转化接口 来源Excel
    /// </summary>
    public interface ITransformExcel<T> where T : new()
    {
        /// <summary>
        /// 从Excel转化的流中获取数据
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="source">Excel数据流</param>
        /// <returns></returns>
        IList<T> ToCollection(Stream source, string fileName, int startRow = 1, ValueConverter valueConverter = null);
    }

    /// <summary>
    /// 值转换委托类
    /// </summary>
    /// <param name="row">行</param>
    /// <param name="cell">列</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public delegate object ValueConverter(int row, int cell, object value);

    /// <summary>
    /// NPOI 支持的列Attribute属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ExcelColumnAttribute : Attribute
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 排序的位置
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 同一列中,连续行中值相同的时候 进行合并
        /// </summary>
        public bool AllowMerge { get; set; }
    }

    internal static class ExcelRelationType
    {
        /// <summary>
        /// 获取数据类型T 的属性标记信息
        /// </summary>
        /// <typeparam name="T">类型T</typeparam>
        /// <typeparam name="A">标记的Attribute</typeparam>
        /// <returns></returns>
        internal static Tuple<bool, A[], PropertyInfo[]> GetAttributes<T, A>() where A : Attribute
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
            var haventCols = true;
            var attributes = new A[properties.Length];
            for (int i = 0, l = properties.Length; i < l; i++)
            {
                PropertyInfo property = properties[i];
                var attrs = property.GetCustomAttributes(typeof(A), true) as A[];
                if (attrs != null && attrs.Length > 0)
                {
                    attributes[i] = attrs[0];
                    haventCols = false;
                }
                else
                {
                    attributes[i] = null;
                }
            }
            return new Tuple<bool, A[], PropertyInfo[]>(haventCols, attributes, properties);
        }
        /// <summary>
        /// 获取类的Attribute
        /// </summary>
        /// <typeparam name="T">类型T</typeparam>
        /// <typeparam name="A">标记的属性</typeparam>
        /// <returns></returns>
        internal static A GetClassAttribute<T, A>() where A : Attribute
        {
            Type type = typeof(T);
            var classAttribute = type.GetCustomAttribute(typeof(A), true) as A;
            return classAttribute;
        }
        /// <summary>
        /// 设置值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="sheet">Excel的Sheet</param>
        /// <param name="rowIndex">行开始索引</param>
        /// <param name="haventCols">是否有列上有标记</param>
        /// <param name="properties">T类型的属性集合</param>
        /// <param name="attributes">T类型中的标记集合</param>
        internal static void PutValueToCell<T>(IList<T> source, ISheet sheet, ref int rowIndex, bool haventCols, PropertyInfo[] properties, ExcelColumnAttribute[] attributes)
        {
            foreach (var item in source)
            {
                var row = sheet.CreateRow(rowIndex);
                //取列值进行填充
                for (int i = 0, l = properties.Length; i < l; i++)
                {
                    PropertyInfo property = properties[i];
                    int columnIndex = i;
                    if (!haventCols)
                    {
                        var column = attributes[i];
                        if (column == null)
                            continue;
                        else
                            columnIndex = column.Index;
                    }
                    var value = property.GetValue(item);
                    var cell = row.CreateCell(columnIndex);
                    if (value is ValueType)
                    {
                        if (property.PropertyType == typeof(bool))
                        {
                            cell.SetCellValue((bool)value);
                        }
                        else if (property.PropertyType == typeof(DateTime))
                        {
                            cell.SetCellValue(Convert.ToDateTime(value));
                        }
                        else if (property.PropertyType == typeof(Guid))
                        {
                            cell.SetCellValue(Convert.ToString(value));
                        }
                        else
                        {
                            cell.SetCellValue(Convert.ToDouble(value));
                        }
                    }
                    else
                    {
                        cell.SetCellValue(value + "");
                    }
                }
                rowIndex++;
            }
        }
        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="workbook">Excel电子薄本</param>
        /// <param name="sheet">Excel的Sheet页</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="attributes">T类型中的标记集合</param>
        /// <param name="haventCols">是否有列上有标记</param>
        internal static void SetSheetStyle(IWorkbook workbook, ISheet sheet, ref int rowIndex, ExcelColumnAttribute[] attributes, bool haventCols)
        {
            if (!haventCols)
            {
                //合并单元格样式
                var vStyle = workbook.CreateCellStyle();
                vStyle.VerticalAlignment = VerticalAlignment.Center;

                //合并单元格
                for (int j = 0, length = attributes.Length; j < length; j++)
                {
                    var column = attributes[j];
                    if (column == null)
                    {
                        continue;
                    }
                    var previous = "";
                    int rowspan = 0, row = 1;
                    if (column.AllowMerge)
                    {
                        for (row = 1; row < rowIndex; row++)
                        {
                            var value = sheet.GetRow(row).Cells[column.Index].StringCellValue;
                            if (previous == value && !string.IsNullOrEmpty(value))
                            {
                                rowspan++;
                            }
                            else
                            {
                                if (rowspan > 1)
                                {
                                    sheet.GetRow(row - rowspan).Cells[column.Index].CellStyle = vStyle;
                                    sheet.AddMergedRegion(new CellRangeAddress(row - rowspan, row - 1, column.Index, column.Index));
                                }
                                rowspan = 1;
                                previous = value;
                            }
                        }
                        if (rowspan > 1)
                        {
                            sheet.GetRow(row - rowspan).Cells[column.Index].CellStyle = vStyle;
                            sheet.AddMergedRegion(new CellRangeAddress(row - rowspan, row - 1, column.Index, column.Index));
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 设置行头内容
        /// </summary>
        /// <param name="workbook">Excel电子薄本</param>
        /// <param name="sheet">Excel的Sheet页</param>
        /// <param name="haventCols">是否有列上有标记</param>
        /// <param name="properties">T类型的属性集合</param>
        /// <param name="attributes">T类型中的标记集合</param>
        internal static void SetHeaderContent(IWorkbook workbook, ISheet sheet, bool haventCols, PropertyInfo[] properties, ExcelColumnAttribute[] attributes)
        {
            //设置第一行样式
            var style = workbook.CreateCellStyle();
            //style.Alignment = HorizontalAlignment.Center;
            //style.VerticalAlignment = VerticalAlignment.Center;
            //style.FillBackgroundColor = HSSFColor.White.Index;
            //style.FillPattern = FillPattern.Bricks;
            //style.FillForegroundColor = HSSFColor.Grey40Percent.Index;

            //第一行的列名称
            var firstRow = sheet.CreateRow(0);
            for (int i = 0, length = properties.Length; i < length; i++)
            {
                var property = properties[i];
                var title = property.Name;
                int index = i;
                if (!haventCols)
                {
                    var column = attributes[i];
                    if (column == null)
                        continue;
                    else
                    {
                        index = column.Index;
                        if (!string.IsNullOrEmpty(column.Title))
                        {
                            title = column.Title;
                        }
                    }
                }
                var cell = firstRow.CreateCell(index);
                cell.CellStyle = style;
                cell.SetCellValue(title);
            }
        }
    }

    /// <summary>
    /// 对部分类进行扩展操作
    /// </summary>
    internal static class Extension
    {
        /// <summary>
        /// 扩展IRow功能
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="index">行中的列顺序号</param>
        /// <returns></returns>
        internal static object GetCellValue(this IRow row, int index)
        {
            var cell = row.GetCell(index);
            if (cell == null)
            {
                return null;
            }
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return cell.NumericCellValue;
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Formula:
                    return cell.ToString();
                case CellType.Blank:
                case CellType.Unknown:
                default:
                    return null;
            }
        }
        /// <summary>
        /// 根据类型获取默认值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        internal static object GetDefault(this Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

    }

    /// <summary>
    /// Excel统计的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class StatisticsAttribute : Attribute
    {
        /// <summary>
        /// 表达式统计格的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Formula 表达式,比如SUM,AVERAGE等等
        /// </summary>
        public string Formula { get; set; }
        /// <summary>
        /// 当Formula为统计表达式的时候,比如SUM,则Columns可以为[1,3]
        /// </summary>
        public int[] Columns { get; set; }
    }

    /// <summary>
    /// NPOI 支持Sheet的Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ExcelSheetAttribute : Attribute
    {
        /// <summary>
        /// 最大支持行
        /// </summary>
        public int MaxRow { get; set; }
    }

    /// <summary>
    /// 控制Excel冻结列行为
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class FreezeAttribute : Attribute
    {
        public FreezeAttribute()
        {
            ColSplit = 0;
            RowSplit = 1;
            LeftMostColumn = 0;
            TopRow = 1;
        }
        /// <summary>
        /// 设置分割列号
        /// </summary>
        public int ColSplit { get; set; }
        /// <summary>
        /// 设置分割行号
        /// </summary>
        public int RowSplit { get; set; }
        /// <summary>
        /// 设置靠左边最大列号
        /// </summary>
        public int LeftMostColumn { get; set; }
        /// <summary>
        /// 设置顶部最大行号
        /// </summary>
        public int TopRow { get; set; }
    }

    /// <summary>
    /// 设置Excel过滤行为
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class FilterAttribute : Attribute
    {
        public FilterAttribute()
        {
            LastRow = null;
        }
        /// <summary>
        /// 设置第一个行的 序号
        /// </summary>
        public int FirstRow { get; set; }
        /// <summary>
        /// 设置最后一个行的 序号
        /// </summary>
        public int? LastRow { get; set; }
        /// <summary>
        /// 设置第一个列的 序号
        /// </summary>
        public int FirstCol { get; set; }
        /// <summary>
        /// 设置最后一个列的 序号
        /// </summary>
        public int LastCol { get; set; }
    }
}
