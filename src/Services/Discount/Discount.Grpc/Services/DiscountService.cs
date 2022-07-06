using Grpc.Core;
using Discount.Grpc;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly ILogger<DiscountService> _logger;
    public DiscountService(ILogger<DiscountService> logger)
    {
        _logger = logger;
    }
    
}

