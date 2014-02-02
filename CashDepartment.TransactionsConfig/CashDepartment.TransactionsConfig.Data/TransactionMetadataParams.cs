namespace CashDepartment.TransactionsConfig.Data
{
    using CashDepartment.Server.DomainModel.Enities.Transactions;
    using CashDepartment.Shared.ComponentModel;
    using CashDepartment.WellKnownBusinessObjects;
    using CashDepartment.WellKnownBusinessObjects.Core;
    using CashDepartment.WellKnownBusinessObjects.Transactions;

    /// <summary>
    /// The transaction metadata parameters.
    /// </summary>
    public class TransactionMetadataParams : ValidatableUiFriendlyObject
    {
        /// <summary>
        /// Gets or sets the debit source.
        /// </summary>
        public BusinessProcessSourceType DebitSource { get; set; }

        /// <summary>
        /// Gets or sets the debit type.
        /// </summary>
        public AccountType DebitType { get; set; }

        /// <summary>
        /// Gets or sets the credit source.
        /// </summary>
        public BusinessProcessSourceType CreditSource { get; set; }

        /// <summary>
        /// Gets or sets the credit type.
        /// </summary>
        public AccountType CreditType { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public TransactionAmount Amount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether transaction is union.
        /// </summary>
        public bool IsUnion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether transaction need symbol.
        /// </summary>
        public bool NeedSymbol { get; set; }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the transaction type.
        /// </summary>
        public TransactionType Type { get; set; }

        /// <summary>
        /// Gets or sets the transaction subtype.
        /// </summary>
        public TransactionSubtype? Subtype { get; set; }

        /// <summary>
        /// Gets or sets the document type.
        /// </summary>
        public TransactionDocType DocType { get; set; }

        /// <summary>
        /// Gets or sets the payment purpose type.
        /// </summary>
        public PaymentPurposeType PaymentPurpose { get; set; }

        /// <summary>
        /// Gets or sets the additional parameters.
        /// </summary>
        public string AdditionalParams { get; set; }
    }
}