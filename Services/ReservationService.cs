using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.ReservationDTO;
using Microsoft.Extensions.Logging;
using Repositories.Implementations;
using Repositories.Interfaces;

namespace Services
{
    public class ReservationService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ReservationService> _logger;
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IMapper mapper, ILogger<ReservationService> logger, IReservationRepository reservationRepository)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._reservationRepository = reservationRepository;
        }

        public async Task createReservation(CreateReservationDto dataInvo)
        {
            try
            {
                Reservation reservationCreate = new Reservation
                {
                    CustomerName = dataInvo.CustomerName,
                    CustomerPhone = dataInvo.CustomerPhone,
                    Note = dataInvo.Note,
                    ReservationTime = DateTime.Now,
                    NumberOfCustomer = dataInvo.NumberOfCustomer ?? 1,
                    CreatedBy = dataInvo.CreatedBy,
                    Status = dataInvo.Status
                };
                // insert Reservations
                await this._reservationRepository.InsertReservation(reservationCreate);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Reservation>> getAllReservation()
        {
            try
            {
                List<Reservation> reservations = await this._reservationRepository.FindAllReservation();
                return reservations;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<Reservation> getAnReservation(int reservationId)
        {
            try
            {
                Reservation reservationResult = await this._reservationRepository.FindReservationById(reservationId);
                return reservationResult;
            }
            catch(Exception ex) {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
