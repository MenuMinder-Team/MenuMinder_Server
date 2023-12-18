using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.AccountDTO;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Exceptions;

namespace Services
{
    public class AccountService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IPermitRepository _permitRepository;

        public AccountService(ILogger<AuthService> logger, IMapper mapper, JwtServices jwtService, IAccountRepository accountRepository, IPermitRepository permitRepository)
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
                if (isExistAcccount != null)
                {
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

        public async Task<List<AccountSuccinctDto>> getListStaffAccount ()
        {
            try
            {
                List<AccountSuccinctDto> accounts = await this._accountRepository.findAllStaffAccount();
                return accounts;
            }catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResultAccountDTO> getDetailAccount(string accountId)
        {
            try
            {
                ResultAccountDTO? accountResult = await this._accountRepository.findAccountById(accountId);
                List<int> permissionIds = await this._permitRepository.FindPermissions(accountId);
                accountResult.PermissionIds = permissionIds.ToArray();
                return accountResult;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        //public async Task<List<Permit>> PermissionsToPermits(List<int> permissionIds , Account account)
        //{
        //    await _permitRepository.DeletePermitByUserId(account.AccountId);

        //    List<Permit> result = new List<Permit>();

        //    foreach (int permissionId in permissionIds)
        //    {
        //        Permit permit = new Permit();
        //        permit.PermissionId = permissionId;
        //        permit.AccountId = account.AccountId;

        //        await _permitRepository.SavePermit(permit);

        //        result.Add(permit);
        //    }

        //    return result;
        //}


        //public async Task UpdateAccount(string email, CreateAccountDTO updateAccountDTO)
        //{
        //    Account updateAccount = await _accountRepository.getAccountEntityByEmail(email);
        //    if (updateAccount == null)
        //    {
        //        throw new Exception("Account does not exist!");
        //    }

        //    updateAccount.Avatar = updateAccountDTO.Avatar;
        //    updateAccount.PhoneNumber = updateAccountDTO.PhoneNumber;
        //    updateAccount.Name = updateAccountDTO.Name;
        //    updateAccount.DateOfBirth = DateOnly.FromDateTime(updateAccountDTO.DateOfBirth);
        //    updateAccount.UpdatedAt = DateTime.Now;
        //    updateAccount.Password = updateAccountDTO.Password;
        //    updateAccount.Role = updateAccountDTO.Role;

        //    await _accountRepository.SaveAccount(updateAccount);
        //    List<Permit> permits = await PermissionsToPermits(updateAccountDTO.PermissionsIds, updateAccount);
        //    updateAccount.Permits = permits;

        //    await _accountRepository.UpdateAccount(updateAccount);
        //}
    }
}
