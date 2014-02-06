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
    [System.Xml.Serialization.XmlInclude(typeof(InterbankEncashTransactionMetadata))]
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

        /// <summary>
        /// Краткое представление метаданных для View Params
        /// </summary>
        public virtual string Introduction {
            get
            {
                return string.Format("EventResult: {0}, ForDefaultCurrency: {1}",
                    this.EventResult.HasValue ? this.EventResult.Value.ToString() : "-",
                    this.ForDefaultCurrency);
            }
        }
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

        public override string Introduction
        {
            get
            {
                return 
                    base.Introduction + 
                    string.Format(", PartyType: {0}, IsOurBank: {1}, IsResident: {2}, Solvency: {3}, IsExchange: {4}, IsOwnEncashService: {5}, IsDefaultCurrency: {6}, IsPrepaid: {7}",
                    this.PartyType,
                    this.IsOurBank,
                    this.IsResident,
                    this.Solvency,
                    this.IsExchange,
                    this.IsOwnEncashService,
                    this.IsDefaultCurrency,
                    this.IsPrepaid.HasValue ? this.IsPrepaid.Value.ToString() : "-");
            }
        }
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