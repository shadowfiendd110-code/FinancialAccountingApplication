using TransactionLibrary;

namespace WalletLibrary
{
    /// <summary>
    /// Кошелёк.
    /// </summary>
    public class Wallet
    {
        #region Свойства.
        /// <summary>
        /// Id кошелька.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текущий баланс кошелька.
        /// </summary>
        public int CurrentBalance
        {
            get
            {
                var income = Transactions.Where(t => t.TransactionType == TransactionType.Income)
                               .Sum(t => t.TransactionSum);
                var expense = Transactions.Where(t => t.TransactionType == TransactionType.Expense)
                                .Sum(t => t.TransactionSum);
                return InitialBalance + income - expense;
            }
        }

        /// <summary>
        /// Название кошелька.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Валюта кошелька.
        /// </summary>
        public string CurrencyType { get; set; }

        /// <summary>
        /// Начальный баланс кошелька.
        /// </summary>
        public int InitialBalance { get; set; }

        /// <summary>
        /// Список транзакций.
        /// </summary>
        public List<Transaction> Transactions { get; set; }
        #endregion

        #region Конструкторы.
        /// <summary>
        /// Инициализация кошелька.
        /// </summary>
        /// <param name="Id">Id кошелька.</param>
        /// <param name="Name">Название кошелька.</param>
        /// <param name="CurrencyType">Тип валюты кошелька.</param>
        /// <param name="InitialBalance">Начальный баланс кошелька.</param>
        public Wallet(int Id, string Name, string CurrencyType, int InitialBalance)
        {
            this.Id = Id;
            this.Name = Name;
            this.CurrencyType = CurrencyType;
            this.InitialBalance = InitialBalance;
            Transactions = new List<Transaction>();
        }
        #endregion
    }
}