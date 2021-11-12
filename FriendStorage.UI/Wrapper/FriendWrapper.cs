using FriendStorage.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;


namespace FriendStorage.UI.Wrapper
{
    public class FriendWrapper : ModelWrapper<Friend>
    {
        public FriendWrapper(Friend model) : base(model)
        {
            InitializeComplexProperties(model);
            InitializeCollectionProperties(model);
        }

        private void InitializeComplexProperties(Friend model)
        {
            if (model.Address is null)
            {
                throw new ArgumentException("Address cannot be null");
            }

            Address = new AddressWrapper(model.Address);
        }

        private void InitializeCollectionProperties(Friend model)
        {
            if (model.Emails is null)
            {
                throw new ArgumentException("Emails cannot be null");
            }

            Emails = new ObservableCollection<FriendEmailWrapper>(model.Emails.Select(e => new FriendEmailWrapper(e)));
            RegisterCollection(Emails, model.Emails);
        }


        #region property Id

        public int Id
        {
            get => GetProperty<int>();
            set => SetProperty(value);
        }

        public int IdOriginalValue => GetOriginalValue<int>();
        public bool IdIsChanged => GetIsChanged();

        #endregion property Id

        #region property FriendGroupId

        public int FriendGroupId
        {
            get => GetProperty<int>();
            set => SetProperty(value);
        }

        public int FriendGroupIdOriginalValue => GetOriginalValue<int>();
        public bool FriendGroupIdIsChanged => GetIsChanged();

        #endregion property FriendGroupId

        #region property FirstName

        public string FirstName
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public string FirstNameOriginalValue => GetOriginalValue<string>();
        public bool FirstNameIsChanged => GetIsChanged();

        #endregion property FirstName

        #region property LastName

        public string LastName
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public string LastNameOriginalValue => GetOriginalValue<string>();
        public bool LastNameIsChanged => GetIsChanged();

        #endregion property LastName

        #region property Birthday

        public DateTime? Birthday
        {
            get => GetProperty<DateTime?>();
            set => SetProperty(value);
        }

        public DateTime? BirthdayOriginalValue => GetOriginalValue<DateTime?>();
        public bool BirthdayIsChanged => GetIsChanged();

        #endregion property Birthday

        #region property IsDeveloper

        public bool IsDeveloper
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        public bool IsDeveloperOriginalValue => GetOriginalValue<bool>();
        public bool IsDeveloperIsChanged => GetIsChanged();

        #endregion property IsDeveloper

        public AddressWrapper Address { get; private set; }

        public ObservableCollection<FriendEmailWrapper> Emails { get; private set; }
    }
}