namespace CashDepartment.Server.DomainModel.Enities.Transactions
{
    /// <summary>
    /// ��������� �� �������� ����� ��� ��������
    /// </summary>
    public enum TransactionAmount
    {
        /// <summary>
        /// ����������� ����� 
        /// </summary>
        Declared = 0,
        /// <summary>
        /// ����� ����� ���������
        /// </summary>
        Fact = 1,
        /// <summary>
        /// ����� ��������
        /// </summary>
        Surplus = 2,
        /// <summary>
        /// ����� ���������
        /// </summary>
        Shortage = 3,
        /// <summary>
        /// ����� ������������
        /// </summary>
        Doubtful,
        /// <summary>
        /// ����� �����������
        /// </summary>
        NonPayment,
        /// <summary>
        /// ����� ����������
        /// </summary>
        Commission,
        /// <summary>
        /// ����� ����� ��������� - ����� ����������
        /// </summary>
        FactDeductCommission,

        /// <summary>
        /// ����� �������� � �����������
        /// </summary>
        SurplusEq,
        /// <summary>
        /// ����� ��������� � �����������
        /// </summary>
        ShortageEq,
        /// <summary>
        /// ����� ������������ � �����������
        /// </summary>
        DoubtfulEq,
        /// <summary>
        /// ����� ����������� � �����������
        /// </summary>
        NonPaymentEq,
        /// <summary>
        /// ����� ����� ��������� - ����� ��������
        /// </summary>
        FactDeductSurplus,
        /// <summary>
        /// ����� ��������� (��� ������������)
        /// </summary>
        ShortageDeductDoubtful,
        /// <summary>
        /// ����� ��������� (��� ������������) � �����������
        /// </summary>
        ShortageDeductDoubtfulEq
    }

    public struct TransactionAmounts
    {
        /// <summary>
        /// ����� � �����������
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