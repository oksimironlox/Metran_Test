using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metran_Test.Tests
{
    public abstract class AbsTests
    {
        public string Result { get; set; }
        public bool TimeoutFlag = false;
        public readonly Dictionary<int, string> _errorMessages = new Dictionary<int, string>
        {
            { 1, "Ошибка соединения" },
            { 2, "Превышено время ожидания" },
            { 3, "Неверные параметры" },
            { 4, "Ошибка инициализации" },
            { 5, "Недостаточно ресурсов" },
            { 6, "Критическая ошибка системы" }
        };
        public async Task<bool> Timeout()
        {
            Random random = new Random();
            int timeoutSeconds = random.Next(10, 31);

            try
            {
                await Task.Delay(timeoutSeconds * 1000);
                TimeoutFlag = true;
                return TimeoutFlag;
            }
            catch
            {
                return false;
            }
        }
        public abstract string GetResult();
    }
}
