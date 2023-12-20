﻿using AutoMapper;
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
using BusinessObjects.DTO.AuthDTO;

namespace Services
{
    public class AccountService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IPermitRepository _permitRepository;
        private readonly PermissionService _permissionService;

        public AccountService(ILogger<AuthService> logger, IMapper mapper, JwtServices jwtService, IAccountRepository accountRepository, IPermitRepository permitRepository, PermissionService permissionService)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._accountRepository = accountRepository;
            this._permitRepository = permitRepository;
            this._permissionService = permissionService;
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

        public async Task<List<AccountSuccinctDto>> getListStaffAccount (string search)
        {
            try
            {
                List<AccountSuccinctDto> accounts = await this._accountRepository.findAllStaffAccount(search);
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
                // get user Permissions
                List<PermissonDto> permissionIds = await _permissionService.GetPermissionsByUserId(Guid.Parse(accountId));
                accountResult.Permissions = permissionIds;
                return accountResult;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> getAccountToUpdate(string accountId)
        {
            try
            {
                Account? accountResult = await this._accountRepository.findAccountToUpdateById(accountId);
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


        public async Task UpdateAccount(string accountId, UpdateAccountDto updateAccountDTO)
        {
            try
            {
                Account existAccount = await this.getAccountToUpdate(accountId);
                if (existAccount == null)
                {
                    throw new BadRequestException("Account does not exist!");
                }
                    existAccount.Avatar = updateAccountDTO?.Avatar ?? existAccount.Avatar;
                    existAccount.PhoneNumber = updateAccountDTO?.PhoneNumber ?? existAccount.PhoneNumber;
                    existAccount.Name = updateAccountDTO?.Name ?? existAccount.Name;
                    existAccount.Gender = updateAccountDTO?.Gender ?? existAccount.Gender;
                    existAccount.DateOfBirth = updateAccountDTO?.DateOfBirth ?? existAccount.DateOfBirth;
                    existAccount.IsBlock = updateAccountDTO?.IsBlock ?? existAccount.IsBlock;
                    existAccount.UpdatedAt = DateTime.Now;

                if(existAccount != null)
                    await _accountRepository.UpdateAccount(existAccount);
            }
            catch (BadRequestException ex)
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
