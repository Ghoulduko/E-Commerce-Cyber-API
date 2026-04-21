using AutoMapper;
using Cyber.Application.Dtos.Shipping;
using Cyber.Core.Entities;
using Cyber.Core.Interfaces;

namespace Cyber.Application.Services;

public class ShippingHistoryService
{
    private readonly IGenericService<ShippingHistory> _historyService;
    private readonly IMapper _mapper;

    public ShippingHistoryService(IGenericService<ShippingHistory> historyService,IMapper mapper)
    {
        _historyService = historyService;
        _mapper = mapper;
    }

    public async Task AddHistory(ShippingHistoryDto req)
    {
        var existingHistory = await _historyService.CheckExistence(s => s.Id == req.Id || s.ShippingId == req.ShippingId);
        if (existingHistory != false) 
            throw new InvalidOperationException("The shipping history already exists.");
        
        var historyToAdd = _mapper.Map<ShippingHistory>(req);
        
        await _historyService.Add(historyToAdd);
    }

    public async Task RemoveHistory(ShippingHistoryDto req)
    {
        var history = await _historyService.GetById(req.Id);
        if (history == null)
            throw new InvalidOperationException("The shipping history does not exist.");
        
        await _historyService.Delete(req.Id);
    }
}