using TransactionLibrary;
using WalletLibrary;

namespace ValidationLibrary
{
    /// <summary>
    /// Валидатор.
    /// </summary>
    public class ValidationHelper
    {
        /// <summary>
        /// Проверяет выше ли сумма транзакции типа "Снятие" текущего баланса кошелька.
        /// </summary>
        /// <param name="sum">Сумма транзакции.</param>
        /// <param name="wallet">Кошелёк.</param>
        /// <param name="transactionType">Тип транзакции.</param>
        /// <returns>Возвращает false, если сумма транзакции типа "Снятие" больше текущего баланса кошелька или 
        /// возвращает true, если сумма транзакции типа "Снятие" меньше текущего баланса кошелька.</returns>
        public static bool CheckBalancePositive(int sum, Wallet wallet, TransactionType transactionType)
        {
            if (transactionType == TransactionType.Expense && sum > wallet.CurrentBalance)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}