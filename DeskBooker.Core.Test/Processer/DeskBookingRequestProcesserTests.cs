using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using DeskBooker.Core.Processer;
using Moq;
using System;
using Xunit;

namespace DeskBooker.Core
{
    public class DeskBookingRequestProcesserTests
    {
        private readonly DeskBookingRequest _request;
        private readonly Mock<IDeskBookingRepository> _deskBookingRepositoryMock;
        private readonly DeskBookingRequestProcesser _processer;

        public DeskBookingRequestProcesserTests()
        {
            _request = new DeskBookingRequest
            {
                FirstName = "Atmar",
                LastName = "Kohistany",
                Email = "a@b.com",
                Date = new DateTime(2020, 1, 28)
            };
            //this _processer should take irepository so let create a moke object below
            _deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();
            //_processer = new DeskBookingRequestProcesser();
            _processer = new DeskBookingRequestProcesser(
                // now lets  insert 
                _deskBookingRepositoryMock.Object);
        }
        [Fact]
        public void ShouldReturnDeskBookingResultWiththeRequestValues()
        {          
            //refator
            //var request = new DeskBookingRequest
            //{
            //    FirstName = "Atmar",
            //    LastName = "Kohistany",
            //    Email = "a@b.com",
            //    Date = new DateTime(2020, 1, 28)
            //};

            //refactoring
            /////var processer = new DeskBookingRequestProcesser();
            // on the process we need a method called BookDesk
            // and in the parameter we need to pass an object 
            // lets create request object type DeskBookingRequest
            //Act
            DeskBookingResult result= _processer.BookDesk(_request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_request.FirstName, result.FirstName);
        }
        [Fact]
        public void ShouldThrowExpectionIfRequestIsNull()
        {
            // first we need a processer 
            ////var processer = new DeskBookingRequestProcesser(); refactoring 
            var exception=  Assert.Throws<ArgumentNullException>(() => _processer.BookDesk(null));
            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveDeskBooking()
        {
            DeskBooking savedDeskBooking = null;
            _deskBookingRepositoryMock.Setup(x => x.Save(It.IsAny<DeskBooking>()))
                .Callback<DeskBooking>(
                deskbooking =>
                {
                    savedDeskBooking = deskbooking;
                }
                );
            _processer.BookDesk(_request);
            // now we need to verify to save in the database
            _deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Once);
            Assert.NotNull(savedDeskBooking);
            Assert.Equal(_request.FirstName, savedDeskBooking.FirstName);
        }

    }
}
