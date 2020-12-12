using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using System;

namespace DeskBooker.Core.Processer
{
    public class DeskBookingRequestProcesser
    {
        private IDeskBookingRepository _deskBookingRepository;

        public DeskBookingRequestProcesser()
        {
        }

        public DeskBookingRequestProcesser(IDeskBookingRepository deskBookingRepository)
        {
            this._deskBookingRepository = deskBookingRepository;
        }

        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            _deskBookingRepository.Save(new DeskBooking
            {
                FirstName = request.FirstName
            });
            return new DeskBookingResult
            {
                FirstName = request.FirstName
            };
        }
    }
}