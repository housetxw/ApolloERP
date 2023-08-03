using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Dal.Model;
using Ae.ConsumerOrder.Service.Dal.Repository;

namespace Ae.ConsumerOrder.Service.Imp.Helpers
{
    /// <summary>
    /// 订单日志代理
    /// </summary>
    public class OrderLoggerInterceptor : AbstractRepository<OrderLogDO>, IInterceptor
    {
        //利用反射获得等待返回值的异步方法，以便在后面构造泛型参数
        private MethodInfo _methodInfo = typeof(OrderLoggerInterceptor).GetMethod("HandleAsync", BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly OrderLoggerRequest _orderLoggerRequest;
        public OrderLoggerInterceptor(OrderLoggerRequest orderLoggerRequest)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
            _orderLoggerRequest = orderLoggerRequest;
        }

        public void Intercept(IInvocation invocation)
        {
            _orderLoggerRequest.BeforeValue = GetBeforeValue();
            invocation.Proceed();
            var returnType = invocation.Method.ReturnType;
            if (returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                HandleAsyncWithReflection(invocation);
            }
            else
            {
                _orderLoggerRequest.AfterValue = GetAfterValue();
                InsertOrderLog();
            }
        }

        //利用反射获取方法类型信息来构造等待返回值的异步方法
        private void HandleAsyncWithReflection(IInvocation invocation)
        {
            var resultType = invocation.Method.ReturnType.GetGenericArguments()[0];
            var mi = _methodInfo.MakeGenericMethod(resultType);
            invocation.ReturnValue = mi.Invoke(this, new[] { invocation.ReturnValue });
        }

        //构造等待返回值的异步方法
        private async Task<T> HandleAsync<T>(Task<T> task)
        {
            var t = await task;

            if (t is long result)
            {
                if (result > 0)
                {
                    _orderLoggerRequest.AfterValue = GetAfterValue();
                    InsertOrderLog();
                }
            }
            if (t is bool boolResult)
            {
                if (boolResult)
                {
                    _orderLoggerRequest.AfterValue = GetAfterValue();
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
                var order = GetList<OrderDO>("where id=@OrderId ", new { OrderId = _orderLoggerRequest.OrderId }, false);

                if (_orderLoggerRequest.IsRecordOrderProduct)
                {
                    var orderProductList = GetList<OrderProductDO>("where id=@OrderId ", new { OrderId = _orderLoggerRequest.OrderId }, false);

                    return JsonConvert.SerializeObject(new
                    {
                        Order = order,
                        Products = orderProductList
                    });
                }

                return JsonConvert.SerializeObject(order?.FirstOrDefault());
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
                var order = GetList<OrderDO>("where id=@OrderId", new { OrderId = _orderLoggerRequest.OrderId }, true);

                if (_orderLoggerRequest.IsRecordOrderProduct)
                {
                    var orderProductList = GetList<OrderProductDO>("where id=@OrderId ", new { OrderId = _orderLoggerRequest.OrderId }, false);

                    return JsonConvert.SerializeObject(new
                    {
                        Order = order,
                        Products = orderProductList
                    });
                }

                return JsonConvert.SerializeObject(order?.FirstOrDefault());
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
                Insert(new OrderLogDO()
                {
                    OrderId = _orderLoggerRequest.OrderId,
                    Type1 = _orderLoggerRequest.Type1,
                    Type2 = _orderLoggerRequest.Type2,
                    Filter1 = _orderLoggerRequest.Filter1,
                    Filter2 = _orderLoggerRequest.Filter2,
                    Summary = _orderLoggerRequest.Summary,
                    BeforeValue = _orderLoggerRequest.BeforeValue,
                    AfterValue = _orderLoggerRequest.AfterValue,
                    CreateBy = _orderLoggerRequest.CreateBy,
                    CreateTime = DateTime.Now,
                    UpdateBy = _orderLoggerRequest.UpdateBy,
                    UpdateTime = DateTime.Now
                });

            }
            catch (Exception e)
            {
                //TODO 记录日志
            }
        }


    }
}
