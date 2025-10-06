namespace InputHelperLibrary
{
    /// <summary>
    /// Помощник ввода.
    /// </summary>
    public class InputHelper
    {
        /// <summary>
        /// Читает целое число с проверкой корректности ввода.
        /// </summary>
        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) && result >= 0)
                {
                    return result;
                }
                Console.WriteLine("Ошибка: введите корректное число");
            }
        }

        /// <summary>
        /// Читает строку с проверкой на пустоту.
        /// </summary>
        public static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }
                Console.WriteLine("Ошибка: ввод не может быть пустым");
            }
        }
    }
}
