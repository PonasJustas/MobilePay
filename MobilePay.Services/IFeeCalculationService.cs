﻿using MobilePay.Contracts;
using System.Collections.Generic;

namespace MobilePay.Services
{
    public interface IFeeCalculationService
    {
        List<TransactionFee> CalculateFees(List<Transaction> transactions);
        TransactionFee CalculateFee(Transaction transaction);
    }
}