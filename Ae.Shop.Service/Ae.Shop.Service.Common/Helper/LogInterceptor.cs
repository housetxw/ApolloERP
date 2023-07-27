using Castle.DynamicProxy;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Common.Helper
{
    /// <summary>
    /// 日志切面帮助类
    /// </summary>
    public class LogInterceptorHelpher<T, L> where T: class  where L : BaseLogDO,new ()
    {
        private readonly ProxyGenerator _proxyGenerator;

        public LogInterceptorHelpher()
        {
            _proxyGenerator = new ProxyGenerator();
        }

        public T CreateInterfaceProxyWithTarget(T repository, L insertLog)
        {
            var interceptor = new LogInterceptor<L>(insertLog);
            var newRepository = _proxyGenerator.CreateInterfaceProxyWithTarget<T>(repository, interceptor);

            return newRepository;
        }
    }

    /// <summary>
    /// 门店日志代理
    /// </summary>
    public class LogInterceptor<Tp> : AbstractRepository<Tp>, IInterceptor where Tp: BaseLogDO,new ()
    {
        //利用反射获得等待返回值的异步方法，以便在后面构造泛型参数
        // private MethodInfo _methodInfo = typeof(LogInterceptor<>).GetMethod("HandleAsync", BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly Tp _baseLogRequest;
        private MethodInfo _getMethod;
        private object _interceptorObj;
        private Type _compareType;

        public LogInterceptor(Tp baseLogRequest)
        {
            SetDbType(DbType.MySql);
            _baseLogRequest = baseLogRequest;
        }

        public void Intercept(IInvocation invocation)
        {

            //反射取得代理的对象
            //反射取得代理对象的连接字符串
            _interceptorObj = Activator.CreateInstance(invocation.InvocationTarget?.GetType());
            _compareType = _interceptorObj.GetType().BaseType.GetGenericArguments()?.FirstOrDefault();
            _getMethod = invocation.InvocationTarget?.GetType().GetMethods()?.Where(_ => _.Name == "Get")?.FirstOrDefault();
            var interceptorConnectString = invocation.InvocationTarget?.GetType().GetField("ConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            var interceptorSlaveConnectionString = invocation.InvocationTarget?.GetType().GetField("SlaveConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            ConnectionString = interceptorConnectString.GetValue(_interceptorObj).ToString();
            SlaveConnectionString = interceptorSlaveConnectionString.GetValue(_interceptorObj).ToString();

            _baseLogRequest.BeforeValue = GetBeforeValue();
            invocation.Proceed();
            var returnType = invocation.Method.ReturnType;
            if (returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                HandleAsyncWithReflection(invocation);
            }
            else
            {
                if (string.IsNullOrEmpty(_baseLogRequest.Identifier))
                    _baseLogRequest.Identifier = invocation.ReturnValue.ToString();
                _baseLogRequest.AfterValue = GetAfterValue();
                InsertOrderLog();
            }
        }

        //利用反射获取方法类型信息来构造等待返回值的异步方法
        private void HandleAsyncWithReflection(IInvocation invocation)
        {
            var resultType = invocation.Method.ReturnType.GetGenericArguments()[0];
            //  var mi = _methodInfo.MakeGenericMethod(resultType);
            // invocation.ReturnValue = mi.Invoke(this, new[] { invocation.ReturnValue });

            //新建对象 为了解决后期反射晚加载的问题
            Type rawType = typeof(LogInterceptor<>);

            Type genericType = rawType.MakeGenericType(typeof(Tp));

            var newMethod = genericType.GetMethod("HandleAsync", BindingFlags.Instance | BindingFlags.NonPublic);

            MethodInfo myMethod = newMethod.MakeGenericMethod(resultType);

            invocation.ReturnValue = myMethod.Invoke(this, new[] { invocation.ReturnValue });
        }

        //构造等待返回值的异步方法
        private async Task<T> HandleAsync<T>(Task<T> task)
        {
            var t = await task;
            if (t is long || t is int)
            {
                int.TryParse(t.ToString(), out var result);
                if (result > 0)
                {
                    //给插入的日志赋值主键，在AfterValue 的时候才可以拿到记录
                    if (string.IsNullOrEmpty(_baseLogRequest.Identifier))
                    {
                        _baseLogRequest.Identifier = result.ToString();
                        //为了解决插入的日志表就是主表,插入的时候filter1 没有赋值的情况
                        if (string.IsNullOrEmpty(_baseLogRequest.Filter1))
                        {
                            _baseLogRequest.Filter1 = result.ToString();
                        }
                    }

                    _baseLogRequest.AfterValue = GetAfterValue();
                    InsertOrderLog();
                }
            }
            if (t is bool boolResult)
            {
                if (boolResult)
                {
                    _baseLogRequest.AfterValue = GetAfterValue();
                    InsertOrderLog();
                }
            }

            return t;
        }

        /// <summary>
        /// 得到BeforeValue
        /// </summary>
        /// <returns></returns>
        private string GetBeforeValue()
        {
            try
            {
                if (_interceptorObj != null && _getMethod != null)
                {
                    if (!string.IsNullOrEmpty(_baseLogRequest.Identifier))
                    {
                        Object[] paras = new Object[] { _baseLogRequest.Identifier, true };
                        var getValue = _getMethod.Invoke(_interceptorObj, paras);
                        if (getValue != null)
                        {
                            return JsonConvert.SerializeObject(getValue);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //TODO 记录日志
            }

            return string.Empty;


        }

        /// <summary>
        /// 得到操作之后的Value
        /// </summary>
        /// <returns></returns>
        private string GetAfterValue()
        {
            try
            {
                if (_interceptorObj != null)
                {
                    if (!string.IsNullOrEmpty(_baseLogRequest.Identifier))
                    {
                        Object[] paras = new Object[] { _baseLogRequest.Identifier, true };
                        var getValue = _getMethod.Invoke(_interceptorObj, paras);
                        if (getValue != null)
                        {
                            return JsonConvert.SerializeObject(getValue);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //TODO 记录日志
            }

            return string.Empty;
        }

        /// <summary>
        /// 插入日志记录
        /// </summary>
        private void InsertOrderLog()
        {
            try
            {
                var getDiffSummarys = typeof(CompareUtil).GetMethods()?.Where(_ => _.Name == "GetDiffText")?.ToList();

                if (getDiffSummarys?.Count > 1)
                {
                    var getDiffSummary = getDiffSummarys[1];
                    MethodInfo genericMethod = getDiffSummary.MakeGenericMethod(new[] { _compareType });
                    var returnValue = genericMethod.Invoke(null, new[] { _baseLogRequest.BeforeValue, _baseLogRequest.AfterValue });
                    _baseLogRequest.CompareText = returnValue.ToString();
                }

                Tp tp = new Tp();
                Object[] paras = new Object[] { tp };
                tp.GetType().GetProperties()?.ToList()?.ForEach(_ =>
                {
                    var prop = _baseLogRequest.GetType().GetProperty(_.Name);
                    if (prop != null)
                    {
                        object value = prop.GetValue(_baseLogRequest);
                        _.SetValue(tp, value);
                        if (_.Name.Equals("CreateTime") || _.Name.Equals("UpdateTime"))
                        {
                            _.SetValue(tp, DateTime.Now);
                        }
                    }
                });

                this.Insert(tp);
            }
            catch (Exception e)
            {
                return;
                //TODO 记录日志
            }


        }
    }

    public class BaseLogDO
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 标识符（用来搜索的标识）
        /// </summary>
        [Column("identifier")]
        public string Identifier { get; set; } = string.Empty;
        /// <summary>
        /// 自定义类型1
        /// </summary>
        [Column("type1")]
        public string Type1 { get; set; } = string.Empty;
        /// <summary>
        /// 自定义类型2
        /// </summary>
        [Column("type2")]
        public string Type2 { get; set; } = string.Empty;
        /// <summary>
        /// 自定义过滤1
        /// </summary>
        [Column("filter1")]
        public string Filter1 { get; set; } = string.Empty;
        /// <summary>
        /// 自定义过滤2
        /// </summary>
        [Column("filter2")]
        public string Filter2 { get; set; } = string.Empty;
        /// <summary>
        /// 操作摘要
        /// </summary>
        [Column("summary")]
        public string Summary { get; set; } = string.Empty;
        /// <summary>
        /// 操作前值
        /// </summary>
        [Column("before_value")]
        public string BeforeValue { get; set; } = string.Empty;
        /// <summary>
        /// 操作后值
        /// </summary>
        [Column("after_value")]
        public string AfterValue { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 比对差异数据
        /// </summary>
        [Column("compare_text")]
        public string CompareText { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }


    /// <summary>
    /// 对比工具
    /// </summary>
    public static class CompareUtil
    {
        /// <summary>
        /// 该类型是否可直接进行值的比较
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsCanCompare(Type type)
        {
            if (type.IsValueType)
            {
                return true;
            }
            else
            {
                //String是特殊的引用类型，它可以直接进行值的比较
                if (type.FullName == typeof(string).FullName)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 获取新旧对象差异字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldObj">旧对象</param>
        /// <param name="newObj">新对象</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDiffs<T>(T oldObj, T newObj)
        {
            var diffs = new Dictionary<string, string>();
            try
            {
                var type = typeof(T);
                if (oldObj == null)
                {
                    oldObj = (T)Activator.CreateInstance(type);
                }
                if (newObj == null)
                {
                    newObj = (T)Activator.CreateInstance(type);
                }
                var properties = type.GetProperties();
                if (properties == null || !properties.Any())
                {
                    return diffs;
                }
                foreach (var property in properties)
                {
                    var attributes = property.GetCustomAttributesData();
                    if (attributes == null || !attributes.Any())
                    {
                        continue;
                    }
                    var findCompareDiffAttribute = attributes.Where(_ => _.AttributeType == typeof(CompareDiffAttribute))?.FirstOrDefault();
                    if (findCompareDiffAttribute != null)
                    {
                        var nameArgument = findCompareDiffAttribute.NamedArguments?.Where(_ => _.MemberName == "Name")?.FirstOrDefault();
                        if (nameArgument != null)
                        {
                            var oldValue = property.GetValue(oldObj);
                            var newValue = property.GetValue(newObj);
                            string key = nameArgument.Value.TypedValue.ToString().Trim('"');
                            string value = string.Empty;
                            if (IsCanCompare(property.PropertyType))
                            {
                                if (!Equals(oldValue, newValue))
                                {
                                    value = $"由【{oldValue}】更新为【{newValue}】";
                                    diffs.Add(key, value);
                                }
                            }
                            else
                            {
                                var oldValueJson = JsonConvert.SerializeObject(oldValue);
                                var newValueJson = JsonConvert.SerializeObject(newValue);
                                if (!string.Equals(oldValueJson, newValueJson))
                                {
                                    value = $"由【{oldValueJson}】更新为【{newValueJson}】";
                                    diffs.Add(key, value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return diffs;
        }

        /// <summary>
        /// 获取新旧对象差异文本
        /// </summary>
        /// <typeparam name="T">对象类型（可省略）</typeparam>
        /// <param name="oldObj">原对象</param>
        /// <param name="newObj">新对象</param>
        /// <returns></returns>
        public static string GetDiffText<T>(T oldObj, T newObj)
        {
            var diffText = new StringBuilder();
            var diffs = GetDiffs(oldObj, newObj);
            foreach (var item in diffs)
            {
                diffText.Append($"{item.Key}{item.Value};\n");
            }
            return diffText.ToString();
        }

        /// <summary>
        /// 获取新旧对象差异文本
        /// </summary>
        /// <typeparam name="T">Json对象类型（不可省略）</typeparam>
        /// <param name="oldJson">原Json</param>
        /// <param name="oldJson">新Json</param>
        /// <returns></returns>
        public static string GetDiffText<T>(string oldJson, string newJson)
        {
            var oldObj = JsonConvert.DeserializeObject<T>(oldJson);
            var newObj = JsonConvert.DeserializeObject<T>(newJson);
            return GetDiffText<T>(oldObj, newObj);
        }
    }

    /// <summary>
    /// 对比差异特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CompareDiffAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
