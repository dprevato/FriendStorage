using FriendStorage.Model;
using System;
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
            RegisterComplex(Address);
        }

        private void InitializeCollectionProperties(Friend model)
        {
            if (model.Emails is null)
            {
                throw new ArgumentException("Emails cannot be null");
            }

            Emails = new ChangeTrackingCollection<FriendEmailWrapper>(model.Emails.Select(e => new FriendEmailWrapper(e)));
            RegisterCollection(Emails, model.Emails);
        }


        #region property Id

        public int Id
        {
            get => GetProperty<int>();
            set => SetProperty(value);
        }

        public int IdOriginalValue => GetOriginalValue<int>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion property Id

        #region property FriendGroupId

        public int FriendGroupId
        {
            get => GetProperty<int>();
            set => SetProperty(value);
        }

        public int FriendGroupIdOriginalValue => GetOriginalValue<int>(nameof(FriendGroupId));
        public bool FriendGroupIdIsChanged => GetIsChanged(nameof(FriendGroupId));

        #endregion property FriendGroupId

        #region property FirstName

        public string FirstName
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public string FirstNameOriginalValue => GetOriginalValue<string>(nameof(FirstName));
        public bool FirstNameIsChanged => GetIsChanged(nameof(FirstName));

        #endregion property FirstName

        #region property LastName

        public string LastName
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public string LastNameOriginalValue => GetOriginalValue<string>(nameof(LastName));
        public bool LastNameIsChanged => GetIsChanged(nameof(LastName));

        #endregion property LastName

        #region property Birthday

        public DateTime? Birthday
        {
            get => GetProperty<DateTime?>();
            set => SetProperty(value);
        }

        public DateTime? BirthdayOriginalValue => GetOriginalValue<DateTime?>(nameof(Birthday));
        public bool BirthdayIsChanged => GetIsChanged(nameof(Birthday));

        #endregion property Birthday

        #region property IsDeveloper

        public bool IsDeveloper
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        public bool IsDeveloperOriginalValue => GetOriginalValue<bool>(nameof(IsDeveloper));
        public bool IsDeveloperIsChanged => GetIsChanged(nameof(IsDeveloper));

        #endregion property IsDeveloper

        public AddressWrapper Address { get; private set; }

        public ChangeTrackingCollection<FriendEmailWrapper> Emails { get; private set; }
    }
}