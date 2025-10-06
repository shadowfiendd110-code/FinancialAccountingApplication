using TransactionLibrary;
using WalletLibrary;
using InputHelperLibrary;

namespace WalletOperationLibrary
{
    /// <summary>
    /// Помощник работы с кошельком.
    /// </summary>
    public class WalletOperation
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

        /// <summary>
        /// Количество трат за месяц.
        /// </summary>
        private const int ExpenseCount = 3;
        #endregion

        #region Методы кошелька.
        /// <summary>
        /// Подсчитывает месячный доход/расход.
        /// </summary>
        /// <param name="transactions">Список транзакций.</param>
        /// <returns>Возвращает месячный доход/расход.</returns>
        public static (int Income, int Expense) CalculateMonthlyTransactions(
            List<Transaction> transactions, int transactionYear, int transactionMonth)
        {
            var monthlyTransactions = transactions
                .Where(t => t.TransactionDate.Year == transactionYear && t.TransactionDate.Month == transactionMonth)
                .ToList();

            int income = monthlyTransactions
                .Where(t => t.TransactionType == TransactionType.Income)
                .Sum(t => t.TransactionSum);

            int expense = monthlyTransactions
                .Where(t => t.TransactionType == TransactionType.Expense)
                .Sum(t => t.TransactionSum);

            return (income, expense);
        }

        /// <summary>
        /// Группирует (по типу транзакции), сортирует транзакции (по убыванию суммы) и фильтрует (по дате) транзакции.
        /// </summary>
        /// <param name="transactions">Список транзакций за конкретный месяц.</param>
        /// <returns>Отсортированные и сгруппированные транзакции за определённый месяц.</returns>
        public static Dictionary<TransactionType, List<Transaction>> GroupAndSortTransactions(
            List<Transaction> transactions, int transactionYear, int transactionMonth)
        {
            var monthlyTransactions = transactions
                .Where(t => t.TransactionDate.Year == transactionYear && t.TransactionDate.Month == transactionMonth)
                .ToList();

            var grouped = monthlyTransactions
                .GroupBy(t => t.TransactionType)
                .ToDictionary(g => g.Key, g => g.ToList());

            var sortedGroups = grouped
                .OrderByDescending(g => g.Value.Sum(t => t.TransactionSum))
                .ToDictionary(g => g.Key, g => g.Value);

            foreach (var group in sortedGroups)
            {
                sortedGroups[group.Key] = group.Value
                    .OrderBy(t => t.TransactionDate)
                    .ToList();
            }

            return sortedGroups;
        }

        /// <summary>
        /// Получает 3 самые большие траты за определённый месяц для каждого кошелька.
        /// </summary>
        /// <param name="wallets">Список кошельков.</param>
        /// <returns>Возвращает 3 самые большие траты за месяц для каждого кошелька.</returns>
        public static Dictionary<string, List<Transaction>> GetTopThreeExpensesPerWallet(
            List<Wallet> wallets, int transactionYear, int transactionMonth)
        {
            var result = new Dictionary<string, List<Transaction>>();

            foreach (var wallet in wallets)
            {
                var monthlyExpenses = wallet.Transactions
                    .Where(t => t.TransactionDate.Year == transactionYear &&
                               t.TransactionDate.Month == transactionMonth &&
                               t.TransactionType == TransactionType.Expense)
                    .ToList();

                var top3Expenses = monthlyExpenses
                    .OrderByDescending(t => t.TransactionSum)
                    .Take(ExpenseCount)
                    .ToList();

                result[wallet.Name] = top3Expenses;
            }

            return result;
        }

        /// <summary>
        /// Генерирует кошелёк.
        /// </summary>
        /// <returns>Возвращает кошелёк.</returns>
        public static Wallet GenerateWallet()
        {
            return new Wallet(
                Id: Random.Shared.Next(LowThreshold, HighThreshold),
                Name: InputHelper.ReadString("Введите название кошелька: "),
                InitialBalance: InputHelper.ReadInt("Введите начальный баланс кошелька: "),
                CurrencyType: InputHelper.ReadString("Введите тип валюты кошелька: ")
            );
        }

        /// <summary>
        /// Выбирает кошелёк.
        /// </summary>
        /// <param name="wallets">Список кошельков.</param>
        /// <returns>Возвращает выбранный кошелёк.</returns>
        public static Wallet SelectWallet(List<Wallet> wallets)
        {
            if (wallets.Count == 0)
            {
                Console.WriteLine("Нет доступных кошельков. Создайте сначала кошелёк.");
                return null;
            }

            for (var i = 0; i < wallets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {wallets[i].Name} (Баланс: {wallets[i].CurrentBalance} {wallets[i].CurrencyType})");
            }

            var selectedIndex = InputHelper.ReadInt("\nВыберите кошелёк: ") - 1;

            if (selectedIndex >= 0 && selectedIndex < wallets.Count)
            {
                var selected = wallets[selectedIndex];
                Console.WriteLine($"Выбран кошелёк: {selected.Name}");

                return selected;
            }

            Console.WriteLine("Неверный выбор!");

            return null;
        }
        #endregion
    }
}
