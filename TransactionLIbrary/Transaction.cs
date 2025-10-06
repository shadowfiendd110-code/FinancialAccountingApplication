namespace TransactionLibrary
{
    /// <summary>
    /// Транзакция.
    /// </summary>
    public class Transaction
    {
        #region Свойства.
        /// <summary>
        /// Id транзакции.
        /// </summary>
        public int TransactionId { get; set; }

        /// <summary>
        /// Дата и время транзакции.
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Сумма транзакции.
        /// </summary>
        public int TransactionSum { get; set; }

        /// <summary>
        /// Описание транзакции.
        /// </summary>
        public string TransactionDescription { get; set; }

        /// <summary>
        /// Тип транзакции.
        /// </summary>
        public TransactionType TransactionType { get; set; }
        #endregion

        #region Конструкторы.
        /// <summary>
        /// Инициализация транзакции.
        /// </summary>
        /// <param name="TransactionId">Id транзакции.</param>
        /// <param name="TransactionDate">Дата транзакции.</param>
        /// <param name="TransactionSum">Сумма транзакции.</param>
        /// <param name="TransactionDescription">Описание транзакции.</param>
        /// <param name="TransactionType">Тип транзакции.</param>
        public Transaction(int TransactionId, DateTime TransactionDate, int TransactionSum, string TransactionDescription, TransactionType TransactionType)
        {
            this.TransactionId = TransactionId;
            this.TransactionDate = TransactionDate;
            this.TransactionSum = TransactionSum;
            this.TransactionDescription = TransactionDescription;
            this.TransactionType = TransactionType;
        }
        #endregion

        #region Методы.
        /// <summary>
        /// Переопределяет вывод транзакции в консоль.
        /// </summary>
        /// <returns>Возвращает всю информацию о транзакции.</returns>
        public override string ToString()
        {
            return $"\nID Транзакции: {TransactionId}\nДата транзакции: {TransactionDate}\nСумма транзакции: {TransactionSum}\n" +
                $"Описание транзакции: {TransactionDescription}\n";
        }
        #endregion
    }

    #region Перечисления.
    /// <summary>
    /// Типы транзакций.
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Доход.
        /// </summary>
        Income = 1,

        /// <summary>
        /// Расход.
        /// </summary>
        Expense = 2,
    }
    #endregion
}