using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnClickInvest.Api.Modules.Reports.Models
{
    public class ProjectionSnapshot
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Relacionamento com Scenario
        public Guid ScenarioId { get; set; }
        public ProjectionScenario Scenario { get; set; } = null!;

        // Data de referência do snapshot
        public DateTime Date { get; set; }

        // Valores financeiros acumulados
        public decimal TotalInvested { get; set; }
        public decimal TotalAmount { get; set; }

        // ✅ Propriedade calculada (não vai para o banco)
        [NotMapped]
        public decimal Profit => TotalAmount - TotalInvested;
    }
}
