using FriendStorage.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FriendStorage.UI.Wrapper.Tests
{
    [TestClass]
    public class BasicTests
    {
        private Friend _friend;

        [TestInitialize]
        public void Initialize()
        {
            _friend = new Friend
            {
                FirstName = "Thomas",
                Address = new Address(),
                Emails = new List<FriendEmail>()
            };
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionIfModelIsNull()
        {
            try
            {
                var wrapper = new FriendWrapper(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("model", e.ParamName);
                throw;
            }
        }

        [TestMethod]
        public void ShouldContainModelInModelProperty()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.AreEqual(_friend, wrapper.Model);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionIfAddressIsNull()
        {
            try
            {
                _friend.Address = null;
                var wrapper = new FriendWrapper(_friend);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Address cannot be null", e.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionIfEmailCollectionIsNull()
        {
            try
            {
                _friend.Emails = null;
                var wrapper = new FriendWrapper(_friend);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Emails cannot be null", e.Message);
                throw;
            }
        }

        [TestMethod]
        public void ShouldGetValueOfUnderlyingModelProperty()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.AreEqual(_friend.FirstName, wrapper.FirstName);
        }

        [TestMethod]
        public void ShouldSetValueOfUnderlyingModelProperty()
        {
            var wrapper = new FriendWrapper(_friend)
            {
                FirstName = "Julia"
            };

            Assert.AreEqual("Julia", _friend.FirstName);
        }

    }
}