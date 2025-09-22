using Microsoft.AspNetCore.Http;
using n_Tier_Architecture.DAL.DTO.Requests;
using n_Tier_Architecture.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.BLL.Services.Interfaces
{
    public interface ICheckOutService
    {
        Task<CheckOutResponse> ProceedPaymentAsync(CheckOutRequest request, string UserId, HttpRequest httpRequest);
        Task<bool> SuccessPaymentAsync(int orderId);

    }
}
