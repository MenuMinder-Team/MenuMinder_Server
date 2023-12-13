using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.AccountDTO;
using BusinessObjects.DTO.PermitDTO;
using Microsoft.Extensions.Logging;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Exceptions;

namespace Services
{
    public class AccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IPermitRepository _permitRepository;  

        public AccountService(ILogger<AccountService> logger, IMapper mapper, IAccountRepository accountRepository, IPermitRepository permitRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._accountRepository = accountRepository;
            this._permitRepository = permitRepository;
        }

        public async Task<ResultAccountDTO> createAccount(CreateAccountDto dataInvo)
        {
            try
            {
                // account create data
                Account accountCreate = new Account();
                accountCreate.Email = dataInvo.Email;
                accountCreate.Password = dataInvo.Password;
                accountCreate.Gender = dataInvo.Gender;
                accountCreate.Name = dataInvo.Name;
                accountCreate.PhoneNumber = dataInvo.PhoneNumber;

                // check Account already exists
                Account? isExistAcccount = await this._accountRepository.getAccountByEmail(accountCreate.Email);
                if (isExistAcccount != null) {
                    throw new BadRequestException("Email already exists.");
                }

                // create account
                Account accountQuery = await this._accountRepository.SaveAccount(accountCreate);
                ResultAccountDTO accountResult = this._mapper.Map<Account, ResultAccountDTO>(accountQuery);
                // list permission id
                if (dataInvo?.PermissionIds != null && dataInvo.PermissionIds.Length > 0)
                {
                    // create account permission
                    Permit[] permitCreates = dataInvo.PermissionIds.Select(id => new Permit { PermissionId = id, AccountId = accountQuery.AccountId }).ToArray();
                    await this._permitRepository.createBulkPermits(permitCreates);
                    accountResult.PermissionIds = dataInvo.PermissionIds;
                };
                
                return accountResult;
            }
            catch (BadRequestException ex)
            {
                throw ex;
            }
            catch (UnauthorizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
