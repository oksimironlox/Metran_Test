using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metran_Test.Tests
{
    public class Test2 : AbsTests
    {
        private string _resultTest1 = string.Empty;
        
        public Test2(string ResultTest1)
        {
            _resultTest1 = ResultTest1;
        }

        public override string GetResult()
        {
            if (string.IsNullOrEmpty(_resultTest1))
            {
                return Result = string.Compare(_resultTest1.ToLower(), "успешно") == 0 ? "Не было выявлено ошибки" : GetRandomErrorMessage();
            }
            else
            {
                return Result = "Нет результатов первого теста";
            }
        }
        private string GetRandomErrorMessage()
        {
            Random random = new Random();
            var randomKey = _errorMessages.Keys.ElementAt(random.Next(_errorMessages.Count));
            return $"{_errorMessages[randomKey]}";
        }
    }
}
