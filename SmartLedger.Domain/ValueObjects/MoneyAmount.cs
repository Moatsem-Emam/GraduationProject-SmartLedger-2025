using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Domain.ValueObjects
{
    public class MoneyAmount
    {
        public long Value { get; }
        
        public MoneyAmount(string? input)
        {
            
            if (!string.IsNullOrWhiteSpace(input) & !long.TryParse(input, out long value))
            {
                throw new ArgumentException("المبلغ غير صالح، يجب أن يكون رقمًا.");
            }

            Value = value;
        }

        public bool IsZero => Value == 0;
    }

}
