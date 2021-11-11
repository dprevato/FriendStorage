using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendStorage.UITests.Wrapper
{
    [TestClass]
    public class ChangeTrackingSimpleProperties
    {
        private Friend _friend;

        [TestInitialize]
        public void Initialize() {
            _friend = new Model.Friend {
                FirstName = "Thomas",
                Address = new Address(),
                Emails = new List<FriendEmail>()
            };
        }

        [TestMethod]
        public void ShouldStoreOriginalValue() {
            var wrapper = new FriendWrapper(_friend);
            Assert.AreEqual("Thomas", wrapper.FirstNameOriginal);

            wrapper.FirstName = "Julia";
            Assert.AreEqual("Thomas", wrapper.FirstNameOriginal);
        }

        [TestMethod]
        public void ShouldSetChanged() {
            var wrapper = new FriendWrapper(_friend);
            Assert.IsFalse(wrapper.FirstNameChanged);
            Assert.IsFalse(wrapper.IsChanged);            // Aggiunto, se cambia la property cambia anche la classe

            wrapper.FirstName = "Julia";
            Assert.IsTrue(wrapper.FirstNameChanged);
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.FirstName = "Thomas";               // FirstName ritorna al valore originale...
            Assert.IsFalse(wrapper.FirstNameChanged);   // ... mi aspetto che xxxChanged torni ad essere false
            Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForFirstNameChanged() {
            var fired = false;

            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) => {
                if (e.PropertyName == nameof(wrapper.FirstNameChanged)) {
                    fired = true;
                }
            };
            wrapper.FirstName = "Julia";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsChanged() {
            var fired = false;

            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) => {
                if (e.PropertyName == nameof(wrapper.IsChanged)) {
                    fired = true;
                }
            };
            wrapper.FirstName = "Julia";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldAcceptChanges() {
            var wrapper = new FriendWrapper(_friend);
            wrapper.FirstName = "Julia";
            Assert.AreEqual("Julia", wrapper.FirstName);
            Assert.AreEqual("Thomas", wrapper.FirstNameOriginal);
            Assert.IsTrue(wrapper.FirstNameChanged);
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.AcceptChanges();

            Assert.AreEqual("Julia", wrapper.FirstName);
            Assert.AreEqual("Julia", wrapper.FirstNameOriginal);
            Assert.IsFalse(wrapper.FirstNameChanged);
            Assert.IsFalse(wrapper.IsChanged);

        }

        [TestMethod]
        public void ShouldRejectChanges() {
            var wrapper = new FriendWrapper(_friend);
            wrapper.FirstName = "Julia";
            Assert.AreEqual("Julia", wrapper.FirstName);
            Assert.AreEqual("Thomas", wrapper.FirstNameOriginal);
            Assert.IsTrue(wrapper.FirstNameChanged);
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.RejectChanges();

            Assert.AreEqual("Thomas", wrapper.FirstName);
            Assert.AreEqual("Thomas", wrapper.FirstNameOriginal);
            Assert.IsFalse(wrapper.FirstNameChanged);
            Assert.IsFalse(wrapper.IsChanged);

        }
    }
}
