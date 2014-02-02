namespace CashDepartment.TransactionsConfig.Data
{
    using System;

    using CashDepartment.Shared.ComponentModel;
    using CashDepartment.WellKnownBusinessObjects;
    using CashDepartment.WellKnownBusinessObjects.Core;
    using CashDepartment.WellKnownBusinessObjects.Encashment;
    using CashDepartment.WellKnownBusinessObjects.Enquiries;
    using CashDepartment.WellKnownBusinessObjects.Recalculation;
    using CashDepartment.WellKnownBusinessObjects.Revenue;
    using CashDepartment.WellKnownBusinessObjects.Transactions;

    /// <summary>
    /// The transaction metadata.
    /// </summary>
    public abstract class TransactionMetadata : ValidatableUiFriendlyObject
    {
        /// <summary>
        /// The backfield for <see cref="Id"/>.
        /// </summary>
        private Guid _id;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <remarks>Must be changed of one of current <see cref="TransactionMetadata"/> properties 
        /// or one of <see cref="TransactionMetadataParams"/> of <see cref="Params"/> property was changed.</remarks>
        public Guid Id
        {
            get
            {
                return this._id;
            }
            set
            {
                if (this._id == value) return;
                this._id = value;
                this.RaisePropertyChangedEvent(() => this.Id);
            }
        }

        /// <summary>
        /// Gets or sets the event result.
        /// </summary>
        public TransactionEventResult? EventResult { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether transaction for default currency.
        /// </summary>
        public bool ForDefaultCurrency { get; set; }

        /// <summary>
        /// Gets or sets the operating time.
        /// </summary>
        public OperatingTime? OperatingTime { get; set; }

        /// <summary>
        /// Gets or sets the list of transaction parameters.
        /// </summary>
        public BindingListEx<TransactionMetadataParams> Params { get; set; }
    }

    /// <summary>
    /// The default transaction metadata.
    /// </summary>
    public class DefaultTransactionMetadata : TransactionMetadata
    {
        /// <summary>
        /// Gets or sets the transaction is for return.
        /// </summary>
        public bool? IsForReturn { get; set; }

        /// <summary>
        /// Gets or sets the return type.
        /// </summary>
        public EncashmentReturnType? ReturnType { get; set; }
    }

    /// <summary>
    /// The ATM input encashment transaction metadata.
    /// </summary>
    public class AtmInCashTransactionMetadata : TransactionMetadata
    {
        /// <summary>
        /// Gets or sets a value indicating whether is money out of cases.
        /// </summary>
        public bool IsOutOfCases { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether encashment without document.
        /// </summary>
        public bool WithoutDocument { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is reserve.
        /// </summary>
        public bool IsReserve { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is return.
        /// </summary>
        public bool IsReturn { get; set; }

        /// <summary>
        /// Gets or sets the return type.
        /// </summary>
        public EncashmentReturnType? ReturnType { get; set; }
    }

    /// <summary>
    /// The ATM output encashment transaction metadata.
    /// </summary>
    public class AtmOutCashTransactionMetadata : TransactionMetadata
    {
        /// <summary>
        /// Gets or sets a value indicating whether is reserve.
        /// </summary>
        public bool IsReserve { get; set; }
    }

    /// <summary>
    /// The client payment for input encashment transaction metadata.
    /// </summary>
    public class ClientPaymentForInCashTransactionMetadata : TransactionMetadata
    {
        /// <summary>
        /// Gets or sets the payment terms.
        /// </summary>
        public PaymentTerms PaymentTerms { get; set; }
    }

    /// <summary>
    /// The client transaction metadata.
    /// </summary>
    public class ClientTransactionMetadata : TransactionMetadata
    {
        /// <summary>
        /// Gets or sets the enrollment type.
        /// </summary>
        public EnrollmentType EnrollmentType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is our bank account.
        /// </summary>
        public bool IsOurBankAccount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether need commission writeoff.
        /// </summary>
        public bool NeedCommissionWriteoff { get; set; }
    }

    /// <summary>
    /// The interbank encashment transaction metadata.
    /// </summary>
    public class InterbankEncashTransactionMetadata : TransactionMetadata
    {
        /// <summary>
        /// Тип контрагента (Unit или Bank)
        /// </summary>
        public PartyType PartyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsOurBank { get; set; }

        /// <summary>
        /// Операция с банком-резидентом?
        /// </summary>
        public bool IsResident { get; set; }

        /// <summary>
        /// Платежность купюр (Годные или Ветхие)
        /// </summary>
        public EnquirySolvency Solvency { get; set; }

        /// <summary>
        /// Является операция обменом?
        /// </summary>
        public bool IsExchange { get; set; }

        /// <summary>
        /// Своя инкассация?
        /// </summary>
        public bool IsOwnEncashService { get; set; }

        /// <summary>
        /// Оплата за безналичную гривну?
        /// </summary>
        public bool IsDefaultCurrency { get; set; }

        /// <summary>
        /// По предоплате?
        /// </summary>
        public bool? IsPrepaid { get; set; }
    }

    /// <summary>
    /// The unit input encashment transaction metadata.
    /// </summary>
    public class UnitInCashTransactionMetadata : TransactionMetadata
    {
        /// <summary>
        /// Gets or sets the guilty.
        /// </summary>
        public GuiltyOfShortage Guilty { get; set; }

        /// <summary>
        /// Gets or sets the is our bank account.
        /// </summary>
        public bool? IsOurBankAccount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether for return.
        /// </summary>
        public bool ForReturn { get; set; }

        /// <summary>
        /// Gets or sets the return type.
        /// </summary>
        public EncashmentReturnType? ReturnType { get; set; }
    }

    /// <summary>
    /// The unit output encashment transaction metadata.
    /// </summary>
    public class UnitOutCashTransactionMetadata : TransactionMetadata
    {
        /// <summary>
        /// Gets or sets the is our MFO.
        /// </summary>
        public bool? IsOurMfo { get; set; }
    }
}