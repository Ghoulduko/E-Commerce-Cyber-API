using System.Linq.Expressions;
using Cyber.Application.Dtos.Shipping;
using Cyber.Core.Entities;

namespace Cyber.Application.Interfaces;

public interface IShippingHistoryService
{
    Task AddHistory(ShippingHistoryDto req);

    Task<ShippingHistory> GetFirst(Expression<Func<ShippingHistory, bool>> predicate);

    Task RemoveHistory(ShippingHistoryDto req);
}