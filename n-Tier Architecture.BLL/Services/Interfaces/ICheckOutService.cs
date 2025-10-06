using Microsoft.AspNetCore.Http;
using NTierArchitecture.DAL.DTO.Requests;
using NTierArchitecture.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.BLL.Services.Interfaces
{
    public interface ICheckOutService
    {
        Task<CheckOutResponse> ProceedPaymentAsync(CheckOutRequest request, string UserId, HttpRequest httpRequest);
        Task<bool> SuccessPaymentAsync(int orderId);

    }
}
