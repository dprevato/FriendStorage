using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FriendStorage.UITests.Wrapper
{
    [TestClass]
    public class ChangeTrackingComplexProperty
    {
        private Friend _friend;

        [TestInitialize]
        public void Initialize()
        {
            _friend = new Model.Friend
            {
                FirstName = "Thomas",
                Address = new Address { City = "Müllheim" },
                Emails = new List<FriendEmail>()
            };
        }

        [TestMethod]
        public void ShouldSetIsChangedOfFriendWrapper()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.Address.City = "Salt Lake City";
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.Address.City = "Müllheim";
            Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsChangedPropertyofFriendWrapper()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.IsChanged))
                {
                    fired = true;
                }
            };
            wrapper.Address.City = "Salt Lake City";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldAcceptChanges()
        {
            var wrapper = new FriendWrapper(_friend); // Alla creazione della variabile  _friend.Address.City = Müllheim, _friend.Address.CityOriginalValue = Müllheim
            wrapper.Address.City = "Salt Lake City";  // Adesso City = "Salt Lake City", CityOriginalValue = Müllheim
            Assert.AreEqual("Müllheim", wrapper.Address.CityOriginalValue);

            wrapper.AcceptChanges(); // questo dovrebbe portare a City = "Salt Lake City" e CityOriginalValue = "Salt Lake City".

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("Salt Lake City", wrapper.Address.City);
            Assert.AreEqual("Salt Lake City", wrapper.Address.CityOriginalValue);
        }

        [TestMethod]
        public void ShouldRejectChanges()
        {
            var wrapper = new FriendWrapper(_friend); // Alla creazione della variabile  _friend.Address.City = Müllheim, _friend.Address.CityOriginalValue = Müllheim
            wrapper.Address.City = "Salt Lake City";  // Adesso City = "Salt Lake City", CityOriginalValue = Müllheim
            Assert.AreEqual("Müllheim", wrapper.Address.CityOriginalValue);

            wrapper.RejectChanges();                  // questo dovrebbe riportare a City = Müllheim e CityOriginalValue = Müllheim.

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("Müllheim", wrapper.Address.City);
            Assert.AreEqual("Müllheim", wrapper.Address.CityOriginalValue);
        }

    }
}
