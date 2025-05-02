using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Domain.ValueObjects
{
    public class Description
    {
        public string Value { get; }

        public Description(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("البيان مطلوب.");
            }

            Value = input;
        }
    }

}
