namespace CashDepartment.TransactionsConfig.Data
{
    using CashDepartment.Shared.ComponentModel;
    using CashDepartment.WellKnownBusinessObjects;
    using CashDepartment.WellKnownBusinessObjects.Transactions;

    public class TransactionMetadataGroup : ValidatableUiFriendlyObject
    {
        public BusinessProcessSourceType ProcessSourceType { get; set; }

        public TransactionEvent TransactionEvent { get; set; }

        public TransactionExportODBType ODBType { get; set; }

        public BindingListEx<TransactionMetadata> Metadata { get; set; }
    }
}