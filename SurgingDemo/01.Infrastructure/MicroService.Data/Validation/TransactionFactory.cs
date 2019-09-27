using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace MicroService.Data.Validation
{
    public class TransactionFactory
    {

        public static TransactionScope Default()
        {
            return CreateTransactionScope(TransactionScopeOption.Required, IsolationLevel.ReadCommitted);

        }

        /// <summary>
        /// 参与环境事务
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static TransactionScope Required(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, TimeSpan? timeout = null)
        {
            if (timeout == null)
            {
                return CreateTransactionScope(TransactionScopeOption.Required, isolationLevel);
            }

            return CreateTransactionScope(TransactionScopeOption.Required, isolationLevel, timeout.Value);

        }

        /// <summary>
        /// 参与新事务（将成为根范围）
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static TransactionScope RequiresNew(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, TimeSpan? timeout = null)
        {
            if (timeout == null)
            {
                return CreateTransactionScope(TransactionScopeOption.RequiresNew, isolationLevel);
            }

            return CreateTransactionScope(TransactionScopeOption.RequiresNew, isolationLevel, timeout.Value);

        }

        /// <summary>
        /// 不参与任何事务
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别-不参与任何事务</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static TransactionScope Suppress(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, TimeSpan? timeout = null)
        {
            if (timeout == null)
            {
                return CreateTransactionScope(TransactionScopeOption.Suppress, isolationLevel);
            }

            return CreateTransactionScope(TransactionScopeOption.Suppress, isolationLevel, timeout.Value);

        }

        private static TransactionScope CreateTransactionScope(TransactionScopeOption scopeOption, IsolationLevel isolationLevel)
        {
            return new TransactionScope(scopeOption, new TransactionOptions { IsolationLevel = isolationLevel }, TransactionScopeAsyncFlowOption.Enabled);

        }

        private static TransactionScope CreateTransactionScope(TransactionScopeOption scopeOption, IsolationLevel isolationLevel, TimeSpan timeout)
        {
            return new TransactionScope(scopeOption, new TransactionOptions { IsolationLevel = isolationLevel, Timeout = timeout }, TransactionScopeAsyncFlowOption.Enabled);

        }
    }
}
