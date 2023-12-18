﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.DTO.AccountDTO;

namespace Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public Task<Account> getAccountByEmail(string email);
        public Task<Account> SaveAccount(Account accountData);
        public Task<List<AccountSuccinctDto>> findAllStaffAccount();
        public Task<ResultAccountDTO> findAccountById(string accountId);
    }
}
