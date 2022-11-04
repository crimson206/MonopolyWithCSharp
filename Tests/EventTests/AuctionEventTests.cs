using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests
{
    [TestClass]
    public class AuctionEventTests
    {

        public Mock<IDataForAuctionEvent> GetIDataForAuctionEventMock1()
        {
            var dataForEventMock = new Mock<IDataForAuctionEvent>();
            dataForEventMock.Setup(data => data.Balances).Returns(new List<int> {100, 200, -20, 50});
            dataForEventMock.Setup(data => data.AreInGame).Returns(new List<bool> {true, true, false, true});
            dataForEventMock.Setup(data => data.CurrentPlayerNumber).Returns(3);

            var propertyDataMock = new Mock<IPropertyData>();
            propertyDataMock.Setup(data => data.Price).Returns(70);
            dataForEventMock.Setup(data => data.CurrentPropertyData).Returns(propertyDataMock.Object);

            return dataForEventMock;
        }

        [TestMethod]
        public void GiveMeAName()
        {
            var IDataForEventMock = this.GetIDataForAuctionEventMock1();
            AuctionEvent auctionEvent = new AuctionEvent(IDataForEventMock.Object, new Delegator());
        
            auctionEvent.StartAuction();

        }
    }
}