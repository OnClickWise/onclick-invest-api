using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;

namespace OnClickInvest.Api.Shared.Utils.Financial
{
    public static class RecurringContributionCalculator
    {
        public static decimal Calculate(decimal contribution, decimal monthlyRate, int months)
        {
            if (monthlyRate == 0)
                return contribution * months;

            return contribution *
                   (((decimal)Math.Pow((double)(1 + monthlyRate), months) - 1) / monthlyRate);
        }
    }
}
