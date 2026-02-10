using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnClickInvest.Api.Modules.Subscriptions.DTOs;
using OnClickInvest.Api.Modules.Subscriptions.Models;
using OnClickInvest.Api.Modules.Subscriptions.Repositories;

namespace OnClickInvest.Api.Modules.Subscriptions.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _repository;

        public SubscriptionService(ISubscriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SubscriptionDto>> GetAllAsync()
        {
            var subscriptions = await _repository.GetAllAsync();
            return subscriptions.Select(MapToDto).ToList();
        }

        public async Task<SubscriptionDto?> GetByTenantIdAsync(Guid tenantId)
        {
            var subscription = await _repository.GetActiveByTenantIdAsync(tenantId);
            return subscription == null ? null : MapToDto(subscription);
        }

        public async Task<SubscriptionDto> CreateAsync(SubscriptionDto dto)
        {
            var existing = await _repository.GetActiveByTenantIdAsync(dto.TenantId);
            if (existing != null)
                throw new Exception("Tenant já possui uma assinatura ativa.");

            var subscription = new Subscription(
                dto.TenantId,
                dto.PlanId,
                dto.StartAt == default ? DateTime.UtcNow : dto.StartAt
            );

            await _repository.AddAsync(subscription);
            await _repository.SaveChangesAsync();

            return MapToDto(subscription);
        }

        public async Task CancelAsync(Guid subscriptionId)
        {
            var subscription = await _repository.GetByIdAsync(subscriptionId)
                ?? throw new Exception("Assinatura não encontrada.");

            subscription.Cancel();
            await _repository.SaveChangesAsync();
        }

        private static SubscriptionDto MapToDto(Subscription subscription)
        {
            return new SubscriptionDto
            {
                Id = subscription.Id,
                TenantId = subscription.TenantId,
                PlanId = subscription.PlanId,
                StartAt = subscription.StartAt,
                EndAt = subscription.EndAt,
                IsActive = subscription.IsActive
            };
        }
    }
}
