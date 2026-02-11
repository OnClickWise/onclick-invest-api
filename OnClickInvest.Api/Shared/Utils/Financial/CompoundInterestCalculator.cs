using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;

namespace OnClickInvest.Api.Shared.Utils.Financial
{
    public static class CompoundInterestCalculator
    {
        public static decimal Calculate(decimal principal, decimal monthlyRate, int months)
        {
            return principal * (decimal)Math.Pow((double)(1 + monthlyRate), months);
        }
    }
}
