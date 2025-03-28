﻿using Banking_system.DAL.Model;

//using System.Transactions;

namespace Banking_system.DAL.Repositories.IRepositories
{
    public interface ITransactionRepo : IGenericRepo<Transaction>
    {
        Task<List<Transaction>> GetAllTrxByAccId(int AccId);

        Task<List<Transaction>> GetAllTrxByFromAccId(int AccId);

        Task<List<Transaction>> GetAllTrxByToAccId(int AccId);



    }
}
