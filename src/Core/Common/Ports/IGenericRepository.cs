﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Ports;
public interface IGenericRepository<TResponse, TFilterRequest, TCreateRequest>
{
    Task<IEnumerable<TResponse>> List(
    TFilterRequest dto = default,
    CancellationToken cancellationToken = default
);

    Task<TResponse> View(
        TFilterRequest dto = default,
        CancellationToken cancellationToken = default
    );

    Task<TResponse> Create(
        TCreateRequest dto = default,
        CancellationToken cancellationToken = default
    );
}