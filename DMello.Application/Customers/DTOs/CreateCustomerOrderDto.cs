using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Application.Customers.DTOs
{
    public record CreateCustomerOrderDto
    (
        string OrderNo,
    string MainSku,
    string? SubSku,
    string? Size,
    string Customer,
    string? Description
    );
}
