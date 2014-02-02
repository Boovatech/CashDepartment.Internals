namespace CashDepartment.Server.DomainModel.Enities.Transactions
{
    /// <summary>
    /// Указатель на источник суммы для проводки
    /// </summary>
    public enum TransactionAmount
    {
        /// <summary>
        /// Объявленная сумма 
        /// </summary>
        Declared = 0,
        /// <summary>
        /// Сумма после пересчета
        /// </summary>
        Fact = 1,
        /// <summary>
        /// Сумма излишков
        /// </summary>
        Surplus = 2,
        /// <summary>
        /// Сумма недостачи
        /// </summary>
        Shortage = 3,
        /// <summary>
        /// Сумма сомнительных
        /// </summary>
        Doubtful,
        /// <summary>
        /// Сумма неплатежных
        /// </summary>
        NonPayment,
        /// <summary>
        /// Сумма начислений
        /// </summary>
        Commission,
        /// <summary>
        /// Сумма после пересчета - сумма начислений
        /// </summary>
        FactDeductCommission,

        /// <summary>
        /// Сумма излишков в эквиваленте
        /// </summary>
        SurplusEq,
        /// <summary>
        /// Сумма недостачи в эквиваленте
        /// </summary>
        ShortageEq,
        /// <summary>
        /// Сумма сомнительных в эквиваленте
        /// </summary>
        DoubtfulEq,
        /// <summary>
        /// Сумма неплатежных в эквиваленте
        /// </summary>
        NonPaymentEq,
        /// <summary>
        /// Сумма после пересчета - сумма излишков
        /// </summary>
        FactDeductSurplus,
        /// <summary>
        /// Сумма недостачи (без сомнительных)
        /// </summary>
        ShortageDeductDoubtful,
        /// <summary>
        /// Сумма недостачи (без сомнительных) в эквиваленте
        /// </summary>
        ShortageDeductDoubtfulEq
    }

    public struct TransactionAmounts
    {
        /// <summary>
        /// Суммы в эквиваленте
        /// </summary>
        public static readonly TransactionAmount[] EquivalentAmounts = new[]
                                                                          {
                                                                              TransactionAmount.SurplusEq,
                                                                              TransactionAmount.ShortageEq,
                                                                              TransactionAmount.DoubtfulEq,
                                                                              TransactionAmount.NonPaymentEq,
                                                                              TransactionAmount.ShortageDeductDoubtfulEq
                                                                          };
    }
}