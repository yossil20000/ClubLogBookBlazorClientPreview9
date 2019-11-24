using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Application.Specifications;

namespace ClubLogBook.Application.Services
{
	public class ReservationService : IReservationService
	{
		IReservationRepository _reservationRepository;
		public ReservationService(IReservationRepository reservationRepository)
		{
			_reservationRepository = reservationRepository;
		}

		public async Task<AircraftReservation> AddReservation(AircraftReservation aircraftReservation)
		{
			var reservations = await _reservationRepository.ListAllAsync();
			var exist = reservations.Where(f => f.Intersect(aircraftReservation)).FirstOrDefault();
			if (exist != null)
				return null;
			await _reservationRepository.AddAsync(aircraftReservation);
			return aircraftReservation;
		}

		public async Task DeleteReservation(int id)
		{
			AircraftReservation aircraftReservation;
			aircraftReservation = await _reservationRepository.GetByIdAsync(id);
			await _reservationRepository.DeleteAsync(aircraftReservation);
		}
		public async Task<AircraftReservation> GetReservation(int id)
		{
			AircraftReservation aircraftReservation;
			aircraftReservation = await _reservationRepository.GetByIdAsync(id);
			
			return aircraftReservation;
		}
		public async Task<bool> IsValid(AircraftReservation aircraftReservation)
		{
			var reservations = await _reservationRepository.ListAllAsync();
			var exist = reservations.Where(f => f.Intersect(aircraftReservation)).FirstOrDefault();
			if (exist != null && exist.Id != aircraftReservation.Id)
				return false;
			return true;
		}
		public async Task EditReservation(AircraftReservation aircraftReservation)
		{

			await _reservationRepository.UpdateReservation(aircraftReservation);
			//await _reservationRepository.DeleteAsync(aircraftReservation);
			//await _reservationRepository.UpdateAsync(aircraftReservation);
		}

		public async Task<IEnumerable<AircraftReservation>> GetAircraftReservation(string tailNumber)
		{
			ReservationSpecification reservationSpecification = new ReservationSpecification(0, 10, tailNumber, "");
			return await _reservationRepository.ListAsync(reservationSpecification);
		}

		public async Task<IEnumerable<AircraftReservation>> GetPilotReservation(string idNumber)
		{
			ReservationSpecification reservationSpecification = new ReservationSpecification(0, 10, "", idNumber);
			return await _reservationRepository.ListAsync(reservationSpecification);
		}

		public async Task<IEnumerable<AircraftReservation>> GetReservation()
		{
			return await _reservationRepository.ListAllAsync();
		}
	}
}
