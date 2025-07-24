using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metran_Test.Tests
{
    public class Test1 : AbsTests
    {
        public Test1()
        {

        }

        public override string GetResult()
        {
            Random random = new Random();
            int result = random.Next(0, 2);
            return Result = result == 0 ? "Успешно" : "Ошибка";
        }
    }
}
