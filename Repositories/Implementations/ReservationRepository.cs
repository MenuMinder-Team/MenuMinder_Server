﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class ReservationRepository : IReservationRepository
    {

        private readonly Menu_minder_dbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ReservationRepository> _logger;

        public ReservationRepository(Menu_minder_dbContext context, IMapper mapper, ILogger<ReservationRepository> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task InsertReservation(Reservation reservation)
        {
            try
            {
                this._context.Reservations.Add(reservation);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Reservation>> FindAllReservation()
        {
            try
            {
                List<Reservation> data = await this._context.Reservations.OrderByDescending(r => r.ReservationTime).ToListAsync();
                return data;
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex.Message);  
                throw new Exception(ex.Message);
            }
        }
    }
}
