using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;

namespace OnClickInvest.Api.Modules.Portfolios.Models
{
    public class Investment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid PortfolioId { get; set; }
        public Portfolio? Portfolio { get; set; }

        public string AssetName { get; set; } = null!;
        public string AssetType { get; set; } = null!;

        public decimal Quantity { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal TotalInvested { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
