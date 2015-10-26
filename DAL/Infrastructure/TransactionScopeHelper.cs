namespace DAL.Infrastructure
{
    using System.Transactions;

    public static class TransactionScopeHelper
    {
        public static TransactionScope GetDefaultTransactionScope()
        {
            var options = new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted };
            return new TransactionScope(TransactionScopeOption.Required, options);
        }
    }
}