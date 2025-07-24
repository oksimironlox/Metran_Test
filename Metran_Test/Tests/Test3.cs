
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Metran_Test.Tests
{
    public class Test3 : AbsTests
    {
        private string _idProduct;
        private int _numError;
        private bool _success;
        private string _resultTest1;
        private string _resultTest2;
        public Test3(string idProduct, string resultTest1, string resultTest2)
        {
            _idProduct = idProduct;
            _resultTest1 = resultTest1;
            _resultTest2 = resultTest2;
        }

        public override string GetResult()
        {
            SetSuccess();
            SetNumError();

            Result = $"ID: {_idProduct},\nСтатус: {_success}";

            if (!_success && _numError != 0)
            {
                Result += $",\nНомер ошибки: {_numError}";
            }

            return Result;
        }

        public string ConvertToJSON()
        {
            var testData = new
            {
                ProductID = _idProduct,
                Success = _success,
                ErrorCode = !_success && _numError != 0 ? _numError : (int?)null
            };

            return JsonSerializer.Serialize(testData, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }

        private void SetNumError()
        {
            if (!_success)
            {
                foreach (var pair in _errorMessages)
                {
                    if (_resultTest2.Contains(pair.Value))
                    {
                        _numError = pair.Key;
                        break;
                    }
                }
            }
        }

        private void SetSuccess()
        {
            _success = string.Compare(_resultTest1.ToLower(), "успешно") == 0 ? true : false;
        }
    }
}
