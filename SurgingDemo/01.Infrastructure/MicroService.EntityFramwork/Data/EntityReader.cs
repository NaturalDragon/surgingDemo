using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.EntityFramwork.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Reflection;
    /// </summary>  
    public sealed class EntityReader
    {
        private const BindingFlags BindingFlag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        //将类型与该类型所有的可写且未被忽略属性之间建立映射  
        private static Dictionary<Type, Dictionary<string, PropertyInfo>> propertyMappings = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
        //存储Nullable<T>与T的对应关系  
        private static Dictionary<Type, Type> genericTypeMappings = new Dictionary<Type, Type>();

        static EntityReader()
        {
            genericTypeMappings.Add(typeof(Byte?), typeof(Byte));
            genericTypeMappings.Add(typeof(SByte?), typeof(SByte));
            genericTypeMappings.Add(typeof(Char?), typeof(Char));
            genericTypeMappings.Add(typeof(Boolean?), typeof(Boolean));
            genericTypeMappings.Add(typeof(Guid?), typeof(Guid));
            genericTypeMappings.Add(typeof(Int16), typeof(Int16));
            genericTypeMappings.Add(typeof(UInt16), typeof(UInt16));
            genericTypeMappings.Add(typeof(Int32), typeof(Int32));
            genericTypeMappings.Add(typeof(UInt32), typeof(UInt32));
            genericTypeMappings.Add(typeof(Int64), typeof(Int64));
            genericTypeMappings.Add(typeof(UInt64), typeof(UInt64));
            genericTypeMappings.Add(typeof(Single), typeof(Single));
            genericTypeMappings.Add(typeof(Double), typeof(Double));
            genericTypeMappings.Add(typeof(Decimal), typeof(Decimal));
            genericTypeMappings.Add(typeof(DateTime), typeof(DateTime));
            genericTypeMappings.Add(typeof(TimeSpan), typeof(TimeSpan));
            genericTypeMappings.Add(typeof(Enum), typeof(Enum));

        }
        /// <summary>  
        /// 将DataTable中的所有数据转换成List&gt;T&lt;集合  
        /// </summary>  
        /// <typeparam name="T">DataTable中每条数据可以转换的数据类型</typeparam>  
        /// <param name="dataTable">包含有可以转换成数据类型T的数据集合</param>  
        /// <returns></returns>  
        public static List<T> GetEntities<T>(DataTable dataTable) where T : new()
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException("dataTable");
            }
            //如果T的类型满足以下条件：字符串、ValueType或者是Nullable<ValueType>  
            if (typeof(T) == typeof(string) || typeof(T).IsValueType)
            {
                return GetSimpleEntities<T>(dataTable);
            }
            else
            {
                return GetComplexEntities<T>(dataTable);
            }
        }
        /// <summary>  
        /// 将DbDataReader中的所有数据转换成List&gt;T&lt;集合  
        /// </summary>  
        /// <typeparam name="T">DbDataReader中每条数据可以转换的数据类型</typeparam>  
        /// <param name="dataTable">包含有可以转换成数据类型T的DbDataReader实例</param>  
        /// <returns></returns>  
        public static List<T> GetEntities<T>(DbDataReader reader) where T : new()
        {
            List<T> list = new List<T>();
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            //如果T的类型满足以下条件：字符串、ValueType或者是Nullable<ValueType>  
            if (typeof(T) == typeof(string) || typeof(T).IsValueType)
            {
                return GetSimpleEntities<T>(reader);
            }
            else
            {
                return GetComplexEntities<T>(reader);
            }

        }
        /// <summary>  
        /// 从DataTable中将每一行的第一列转换成T类型的数据  
        /// </summary>  
        /// <typeparam name="T">要转换的目标数据类型</typeparam>  
        /// <param name="dataTable">包含有可以转换成数据类型T的数据集合</param>  
        /// <returns></returns>  
        private static List<T> GetSimpleEntities<T>(DataTable dataTable) where T : new()
        {
            List<T> list = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add((T)GetValueFromObject(row[0], typeof(T)));
            }
            return list;
        }
        /// <summary>  
        /// 将指定的 Object 的值转换为指定类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <param name="targetType">要转换的目标数据类型</param>  
        /// <returns></returns>  
        private static object GetValueFromObject(object value, Type targetType)
        {
            if (targetType == typeof(string))//如果要将value转换成string类型  
            {
                return GetString(value);
            }
            else if (targetType.IsGenericType)//如果目标类型是泛型  
            {
                return GetGenericValueFromObject(value, targetType);
            }
            else//如果是基本数据类型（包括数值类型、枚举和Guid）  
            {
                return GetNonGenericValueFromObject(value, targetType);
            }
        }

        /// <summary>  
        /// 从DataTable中读取复杂数据类型集合  
        /// </summary>  
        /// <typeparam name="T">要转换的目标数据类型</typeparam>  
        /// <param name="dataTable">包含有可以转换成数据类型T的数据集合</param>  
        /// <returns></returns>  
        private static List<T> GetComplexEntities<T>(DataTable dataTable) where T : new()
        {
            if (!propertyMappings.ContainsKey(typeof(T)))
            {
                GenerateTypePropertyMapping(typeof(T));
            }
            List<T> list = new List<T>();
            Dictionary<string, PropertyInfo> properties = propertyMappings[typeof(T)];
            Dictionary<string, int> propertyColumnOrdinalMapping = GetPropertyColumnIndexMapping(dataTable.Columns, properties);  
            T t;
            foreach (DataRow row in dataTable.Rows)
            {
                t = new T();
                foreach (KeyValuePair<string, PropertyInfo> item in properties)
                {
                    int ordinal = -1;
                    if (propertyColumnOrdinalMapping.TryGetValue(item.Key, out ordinal))
                    {
                        item.Value.SetValue(t, GetValueFromObject(row[ordinal], item.Value.PropertyType), null);
                    }
                    //item.Value.SetValue(t, GetValueFromObject(row[item.Key], item.Value.PropertyType), null);
                }
                list.Add(t);
            }
            return list;
        }

        /// <summary>  
        /// 从DbDataReader的实例中读取复杂的数据类型  
        /// </summary>  
        /// <typeparam name="T">要转换的目标类</typeparam>  
        /// <param name="reader">DbDataReader的实例</param>  
        /// <returns></returns>  
        private static List<T> GetComplexEntities<T>(DbDataReader reader) where T : new()
        {
            if (!propertyMappings.ContainsKey(typeof(T)))//检查当前是否已经有该类与类的可写属性之间的映射  
            {
                GenerateTypePropertyMapping(typeof(T));
            }
            List<T> list = new List<T>();
            Dictionary<string, PropertyInfo> properties = propertyMappings[typeof(T)];
            Dictionary<string, int> propertyColumnOrdinalMapping = GetPropertyColumnIndexMapping(reader, properties);  
            T t;
            while (reader.Read())
            {
                t = new T();
                foreach (KeyValuePair<string, PropertyInfo> item in properties)
                {
                    int ordinal = -1;
                    if (propertyColumnOrdinalMapping.TryGetValue(item.Key, out ordinal))
                    {
                        item.Value.SetValue(t, GetValueFromObject(reader[ordinal], item.Value.PropertyType), null);
                    }
                    //item.Value.SetValue(t, GetValueFromObject(reader[item.Key], item.Value.PropertyType), null);
                }
                list.Add(t);
            }
            return list;
        }
        /// <summary>  
        /// 从DbDataReader的实例中读取简单数据类型（String,ValueType)  
        /// </summary>  
        /// <typeparam name="T">目标数据类型</typeparam>  
        /// <param name="reader">DbDataReader的实例</param>  
        /// <returns></returns>  
        private static List<T> GetSimpleEntities<T>(DbDataReader reader)
        {
            List<T> list = new List<T>();
            while (reader.Read())
            {
                list.Add((T)GetValueFromObject(reader[0], typeof(T)));
            }
            return list;
        }
        /// <summary>  
        /// 将Object转换成字符串类型  
        /// </summary>  
        /// <param name="value">object类型的实例</param>  
        /// <returns></returns>  
        private static object GetString(object value)
        {
            return Convert.ToString(value);
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <param name="targetType"></param>  
        /// <returns></returns>  
        private static object GetEnum(object value, Type targetType)
        {
            return Enum.Parse(targetType, value.ToString());
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetBoolean(object value)
        {
            if (value is Boolean)
            {
                return value;
            }
            else
            {
                byte byteValue = (byte)GetByte(value);
                if (byteValue == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetByte(object value)
        {
            if (value is Byte)
            {
                return value;
            }
            else
            {
                return byte.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetSByte(object value)
        {
            if (value is SByte)
            {
                return value;
            }
            else
            {
                return SByte.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetChar(object value)
        {
            if (value is Char)
            {
                return value;
            }
            else
            {
                return Char.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetGuid(object value)
        {
            if (value is Guid)
            {
                return value;
            }
            else
            {
                return new Guid(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetInt16(object value)
        {
            if (value is Int16)
            {
                return value;
            }
            else
            {
                return Int16.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetUInt16(object value)
        {
            if (value is UInt16)
            {
                return value;
            }
            else
            {
                return UInt16.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetInt32(object value)
        {
            if (value is Int32)
            {
                return value;
            }
            else
            {
                return Int32.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetUInt32(object value)
        {
            if (value is UInt32)
            {
                return value;
            }
            else
            {
                return UInt32.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetInt64(object value)
        {
            if (value is Int64)
            {
                return value;
            }
            else
            {
                return Int64.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetUInt64(object value)
        {
            if (value is UInt64)
            {
                return value;
            }
            else
            {
                return UInt64.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetSingle(object value)
        {
            if (value is Single)
            {
                return value;
            }
            else
            {
                return Single.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetDouble(object value)
        {
            if (value is Double)
            {
                return value;
            }
            else
            {
                return Double.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetDecimal(object value)
        {
            if (value is Decimal)
            {
                return value;
            }
            else
            {
                return Decimal.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetDateTime(object value)
        {
            if (value is DateTime)
            {
                return value;
            }
            else
            {
                return DateTime.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定枚举类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <returns></returns>  
        private static object GetTimeSpan(object value)
        {
            if (value is TimeSpan)
            {
                return value;
            }
            else
            {
                return TimeSpan.Parse(value.ToString());
            }
        }

        /// <summary>  
        /// 将Object类型数据转换成对应的可空数值类型表示  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <param name="targetType">可空数值类型</param>  
        /// <returns></returns>  
        private static object GetGenericValueFromObject(object value, Type targetType)
        {
            if (value == DBNull.Value)
            {
                return null;
            }
            else
            {
                //获取可空数值类型对应的基本数值类型，如int?->int,long?->long  
                Type nonGenericType = genericTypeMappings[targetType];
                return GetNonGenericValueFromObject(value, nonGenericType);
            }
        }

        /// <summary>  
        /// 将指定的 Object 的值转换为指定类型的值。  
        /// </summary>  
        /// <param name="value">实现 IConvertible 接口的 Object，或者为 null</param>  
        /// <param name="targetType">目标对象的类型</param>  
        /// <returns></returns>  
        private static object GetNonGenericValueFromObject(object value, Type targetType)
        {
            if (targetType.IsEnum)//因为  
            {
                return GetEnum(value, targetType);
            }
            else
            {
                switch (targetType.Name)
                {
                    case "Byte": return GetByte(value);
                    case "SByte": return GetSByte(value);
                    case "Char": return GetChar(value);
                    case "Boolean": return GetBoolean(value);
                    case "Guid": return GetGuid(value);
                    case "Int16": return GetInt16(value);
                    case "UInt16": return GetUInt16(value);
                    case "Int32": return GetInt32(value);
                    case "UInt32": return GetUInt32(value);
                    case "Int64": return GetInt64(value);
                    case "UInt64": return GetUInt64(value);
                    case "Single": return GetSingle(value);
                    case "Double": return GetDouble(value);
                    case "Decimal": return GetDecimal(value);
                    case "DateTime": return GetDateTime(value);
                    case "TimeSpan": return GetTimeSpan(value);
                    default: return null;
                }
            }
        }

        /// <summary>  
        /// 获取该类型中属性与数据库字段的对应关系映射  
        /// </summary>  
        /// <param name="type"></param>  
        private static void GenerateTypePropertyMapping(Type type)
        {
            if (type != null)
            {
                PropertyInfo[] properties = type.GetProperties(BindingFlag);
                Dictionary<string, PropertyInfo> propertyColumnMapping = new Dictionary<string, PropertyInfo>(properties.Length);
                string description = string.Empty;
                Attribute[] attibutes = null;
                string columnName = string.Empty;
                bool ignorable = false;
                foreach (PropertyInfo p in properties)
                {
                    ignorable = false;
                    columnName = string.Empty;
                    attibutes = Attribute.GetCustomAttributes(p);
                    foreach (Attribute attribute in attibutes)
                    {
                        //检查是否设置了ColumnName属性  
                        if (attribute.GetType() == typeof(ColumnNameAttribute))
                        {
                            columnName = ((ColumnNameAttribute)attribute).ColumnName;
                            ignorable = ((ColumnNameAttribute)attribute).Ignorable;
                            break;
                        }
                    }
                    //如果该属性是可读并且未被忽略的，则有可能在实例化该属性对应的类时用得上  
                    if (p.CanWrite && !ignorable)
                    {
                        //如果没有设置ColumnName属性，则直接将该属性名作为数据库字段的映射  
                        if (string.IsNullOrEmpty(columnName))
                        {
                            columnName = p.Name;
                        }
                        propertyColumnMapping.Add(columnName, p);
                    }
                }
                propertyMappings.Add(type, propertyColumnMapping);
            }
        }

        private static Dictionary<string, int> GetPropertyColumnIndexMapping(DataColumnCollection dataSource, Dictionary<string, PropertyInfo> properties)
        {
          
            Dictionary<string, int> propertyColumnIndexMapping = new Dictionary<string, int>(dataSource.Count);
            foreach (KeyValuePair<string, PropertyInfo> item in properties)
            {
                for (int i = 0; i < dataSource.Count; i++)
                {
                    if (item.Key.Equals(dataSource[i].ColumnName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        propertyColumnIndexMapping.Add(item.Key, i);
                        break;
                    }
                }
            }
        
            return propertyColumnIndexMapping;
        }

        private static Dictionary<string, int> GetPropertyColumnIndexMapping(DbDataReader dataSource, Dictionary<string, PropertyInfo> properties)
        {
            Dictionary<string, int> propertyColumnIndexMapping = new Dictionary<string, int>(dataSource.FieldCount);
            foreach (KeyValuePair<string, PropertyInfo> item in properties)
            {
                for (int i = 0; i < dataSource.FieldCount; i++)
                {
                    if (item.Key.Equals(dataSource.GetName(i), StringComparison.InvariantCultureIgnoreCase))
                    {
                        propertyColumnIndexMapping.Add(item.Key, i);
                        continue;
                    }
                }
            }
            return propertyColumnIndexMapping;
        }
    }
    /// <summary>  
    /// 自定义属性，用于指示如何从DataTable或者DbDataReader中读取类的属性值  
    /// </summary>  
    public class ColumnNameAttribute : Attribute
    {
        /// <summary>  
        /// 类属性对应的列名  
        /// </summary>  
        public string ColumnName { get; set; }
        /// <summary>  
        /// 指示在从DataTable或者DbDataReader中读取类的属性时是否可以忽略这个属性  
        /// </summary>  
        public bool Ignorable { get; set; }
        /// <summary>  
        /// 构造函数  
        /// </summary>  
        /// <param name="columnName">类属性对应的列名</param>  
        public ColumnNameAttribute(string columnName)
        {
            ColumnName = columnName;
            Ignorable = false;
        }
        /// <summary>  
        /// 构造函数  
        /// </summary>  
        /// <param name="ignorable">指示在从DataTable或者DbDataReader中读取类的属性时是否可以忽略这个属性</param>  
        public ColumnNameAttribute(bool ignorable)
        {
            Ignorable = ignorable;
        }
        /// <summary>  
        /// 构造函数  
        /// </summary>  
        /// <param name="columnName">类属性对应的列名</param>  
        /// <param name="ignorable">指示在从DataTable或者DbDataReader中读取类的属性时是否可以忽略这个属性</param>  
        public ColumnNameAttribute(string columnName, bool ignorable)
        {
            ColumnName = columnName;
            Ignorable = ignorable;
        }
    }
}
