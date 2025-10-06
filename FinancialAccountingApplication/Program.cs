using TransactionProcessorLibrary;
using WalletLibrary;
using WalletOperationLibrary;
using InputHelperLibrary;
using System.Transactions;
using TransactionLibrary;

namespace FinancialAccountingApplication
{
    /// <summary>
    /// Главный файл программы.
    /// </summary>
    class Program
    {
        #region Меню приложения.
        /// <summary>
        /// Меню приложения.
        /// </summary>
        private static class MenuNavigator
        {
            /// <summary>
            /// Показывает меню приложения.
            /// </summary>
            public static void ShowMenu()
            {
                while (true)
                {
                    Console.WriteLine("\n=== Финансовое приложение ===");
                    Console.WriteLine("1. Создать кошелёк");
                    Console.WriteLine("2. Выбрать кошелёк");
                    Console.WriteLine("3. Создать транзакцию");
                    Console.WriteLine("4. Отчеты");
                    Console.WriteLine("0. Выход");

                    var choice = InputHelper.ReadInt("");

                    switch (choice)
                    {
                        case 1:
                            var wallet = WalletOperation.GenerateWallet();
                            wallets.Add(wallet);
                            currentWallet = wallet;
                            Console.WriteLine($"Кошелёк '{wallet.Name}' создан и выбран!");
                            break;

                        case 2:
                            currentWallet = WalletOperation.SelectWallet(wallets);
                            break;

                        case 3:
                            if (currentWallet == null)
                            {
                                Console.WriteLine("Сначала выберите или создайте кошелёк!");
                                break;
                            }
                            TransactionProcessor.TryProcessTransaction(currentWallet);
                            break;

                        case 4:
                            if (currentWallet == null)
                            {
                                Console.WriteLine("Сначала выберите или создайте кошелёк!");
                                break;
                            }
                            ShowReports(); // Теперь можно вызвать только отсюда
                            break;

                        case 0:
                            return;
                    }
                }
            }

            /// <summary>
            /// Показывает отчеты по конкретному кошельку.
            /// </summary>
            private static void ShowReports()
            {
                Console.WriteLine("\n=== Отчеты ===");
                Console.WriteLine("1. Доходы/расходы за месяц");
                Console.WriteLine("2. Группировка транзакций");
                Console.WriteLine("3. Топ-3 трат по кошелькам");

                var reportChoice = InputHelper.ReadInt("");

                switch (reportChoice)
                {
                    case 1:
                        ShowIncomeExpence(currentWallet);
                        break;
                    case 2:
                        ShowGroupAndSortTransactions(currentWallet);
                        break;
                    case 3:
                        ShowTopThreeExpencesPerWallet(wallets);
                        break;
                    default:
                        Console.WriteLine("Ошибка. Неверный выбор отчёта");
                        break;
                }
            }
        }
        #endregion

        #region Приватные переменные.
        /// <summary>
        /// Список кошельков пользователя.
        /// </summary>
        private static List<Wallet> wallets = new List<Wallet>();

        /// <summary>
        /// Текущий кошелёк пользователя.
        /// </summary>
        private static Wallet currentWallet = null;
        #endregion

        /// <summary>
        /// Запускает программу.
        /// </summary>
        static void Main()
        {
            MenuNavigator.ShowMenu();
        }

        #region Вспомогательные методы.
        /// <summary>
        /// Выводит в консоль 3 самые большие траты за указанный месяц для каждого кошелька.
        /// </summary>
        /// <param name="myWalletCollection">Кошельки пользователя.</param>
        private static void ShowTopThreeExpencesPerWallet(List<Wallet> myWalletCollection)
        {
            var transactionYear = InputHelper.ReadInt("Введите год транзакций: ");
            var transactionMonth = InputHelper.ReadInt("Введите месяц транзакций: ");

            var resultTopThree = WalletOperation.GetTopThreeExpensesPerWallet(myWalletCollection, transactionYear, transactionMonth);
            foreach (KeyValuePair<string, List<TransactionLibrary.Transaction>> walletName in resultTopThree)
            {
                Console.WriteLine($"Кошелёк {walletName.Key} имеет следующие транзакции: ");
                foreach (TransactionLibrary.Transaction transaction in walletName.Value)
                {
                    Console.WriteLine($"{transaction}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Показывает отсортированные и сгруппированные транзакции за конкретный месяц.
        /// </summary>
        /// <param name="myWallet">Кошелёк пользователя.</param>
        private static void ShowGroupAndSortTransactions(Wallet myWallet)
        {
            var transactionYear = InputHelper.ReadInt("Введите год транзакций: ");
            var transactionMonth = InputHelper.ReadInt("Введите месяц транзакций: ");

            var resultSort = WalletOperation.GroupAndSortTransactions(myWallet.Transactions, transactionYear, transactionMonth);

            foreach (KeyValuePair<TransactionType, List<TransactionLibrary.Transaction>> transactionType in resultSort)
            {
                foreach (TransactionLibrary.Transaction transaction in transactionType.Value)
                {
                    Console.WriteLine(transaction);
                }
            }
            Console.WriteLine("----------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// Показывает доход и расход за определённый месяц.
        /// </summary>
        /// <param name="myWallet">Кошелёк пользователя.</param>
        private static void ShowIncomeExpence(Wallet myWallet)
        {
            var transactionYear = InputHelper.ReadInt("Введите год транзакций: ");
            var transactionMonth = InputHelper.ReadInt("Введите месяц транзакций: ");

            var result = WalletOperation.CalculateMonthlyTransactions(myWallet.Transactions, transactionYear, transactionMonth);

            Console.WriteLine($"\nДоход за {transactionMonth} месяц {transactionYear} года: {result.Income} {currentWallet.CurrencyType}");
            Console.WriteLine($"Расход за {transactionMonth} месяц {transactionYear} года: {result.Expense} {currentWallet.CurrencyType}");
            Console.WriteLine("----------------------------------------------------------------------------------------");
        }
        #endregion
    }
}