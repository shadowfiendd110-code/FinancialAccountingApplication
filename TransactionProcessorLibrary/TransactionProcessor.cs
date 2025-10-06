using TransactionLibrary;
using WalletLibrary;
using ValidationLibrary;
using InputHelperLibrary;

namespace TransactionProcessorLibrary
{
    /// <summary>
    /// Обработчик транзакций.
    /// </summary>
    public class TransactionProcessor
    {
        #region Константы.
        /// <summary>
        /// Нижняя граница рандомайзера.
        /// </summary>
        private const int LowThreshold = 100;

        /// <summary>
        /// Верхняя граница рандомайзера.
        /// </summary>
        private const int HighThreshold = 10000;
        #endregion

        #region Методы транзакций.
        /// <summary>
        /// Генерирует транзакцию.
        /// </summary>
        /// <param name="transactionType">Тип транзакции.</param>
        /// <param name="wallet">Кошелёк.</param>
        /// <returns>Возвращает транзакцию с типом транзакции <paramref name="transactionType"/>.</returns>
        public static Transaction? GenerateTransaction(TransactionType transactionType, Wallet wallet)
        {
            var sum = InputHelper.ReadInt("Введите сумму: ");

            if (transactionType == TransactionType.Expense && !ValidationHelper.CheckBalancePositive(sum, wallet, transactionType))
            {
                Console.WriteLine("Ошибка: Недостаточно средств на счете!");
                return null;
            }

            var idTransaction = Random.Shared.Next(LowThreshold, HighThreshold);
            var dateTransaction = DateTime.Now;

            var description = transactionType == TransactionType.Income
                ? "Пополнение счёта"
                : "Списание со счёта";

            return new Transaction(idTransaction, dateTransaction, sum, description, transactionType);
        }

        /// <summary>
        /// Получает тип транзакции.
        /// </summary>
        /// <returns>Возвращает тип транзакции.</returns>
        public static TransactionType GetTransactionTypeFromUser()
        {
            while (true)
            {
                Console.WriteLine("Выберите тип транзакции:");
                Console.WriteLine("1) Пополнение счёта");
                Console.WriteLine("2) Списание со счёта");

                var input = InputHelper.ReadString("");

                if (input == "1")
                {
                    return TransactionType.Income;
                }
                else if (input == "2")
                {
                    return TransactionType.Expense;
                }

                Console.WriteLine("❌ Ошибка: введите 1 или 2\n");
            }
        }

        /// <summary>
        /// Обрабатывает транзакцию.
        /// </summary>
        /// <param name="transaction">Транзакция.</param>
        /// <param name="wallet">Кошелёк.</param>
        public static void AddTransaction(Transaction transaction, Wallet wallet)
        {
            wallet.Transactions.Add(transaction);
        }

        /// <summary>
        /// Проверяет транзакцию на null.
        /// </summary>
        /// <param name="wallet">Кошелёк.</param>
        /// <returns>Возвращает true, если транзакция не равна null.</returns>
        public static bool TryProcessTransaction(Wallet wallet)
        {
            TransactionType transactionType = GetTransactionTypeFromUser();
            var transaction = GenerateTransaction(transactionType, wallet);

            if (transaction == null)
            {
                Console.WriteLine("❌ Не удалось создать транзакцию\n");
                return false;
            }

            AddTransaction(transaction, wallet);

            return true;
        }
        #endregion
    }
}