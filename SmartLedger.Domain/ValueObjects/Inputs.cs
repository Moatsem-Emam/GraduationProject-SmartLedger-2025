using SmartLedger.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Domain.ValueObjects
{
    public class Inputs<T>
    {
        public T Value { get; }

        public Inputs(T? input, LedgerInputs ledgerInputs = LedgerInputs.All)
        {
            switch (ledgerInputs)
            {
                case LedgerInputs.Name:
                    if (string.IsNullOrWhiteSpace(input as string))
                        throw new ArgumentException("اليومية مطلوبة.");
                    break;
                case LedgerInputs.Description:
                    if (string.IsNullOrWhiteSpace(input as string))
                        throw new ArgumentException("البيان مطلوب.");
                    break;
                case LedgerInputs.Account:
                    if (input is null)
                        throw new ArgumentException("الحساب الأقتصادي مطلوب.");
                    break;
                case LedgerInputs.Category:
                    if (input is null)
                        throw new ArgumentException("الفئة مطلوبة.");
                    break;
                default:
                    break;
            }


            Value = input!;
        }
    }
}
