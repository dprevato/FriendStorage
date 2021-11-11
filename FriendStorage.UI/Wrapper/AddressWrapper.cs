using FriendStorage.Model;

namespace FriendStorage.UI.Wrapper
{
    public class AddressWrapper : ModelWrapper<Address>
    {
        public AddressWrapper(Address model) : base(model) { }

        #region property Id
        public int Id { get => GetProperty<int>(); set => SetProperty(value); }
        public int IdOriginalValue => GetOriginalValue<int>();
        public bool IdIsChanged => GetIsChanged();
        #endregion property Id

        #region property City
        public string City { get => GetProperty<string>(); set => SetProperty(value); }
        public string CityOriginalValue => GetOriginalValue<string>();
        public bool CityIsChanged => GetIsChanged();
        #endregion property City

        #region property Street
        public string Street { get => GetProperty<string>(); set => SetProperty(value); }
        public string StreetOriginalValue => GetOriginalValue<string>();
        public bool StreetIsChanged => GetIsChanged();
        #endregion property Street

        #region property StreetNumber
        public string StreetNumber { get => GetProperty<string>(); set => SetProperty(value); }
        public string StreetNumberOriginalValue => GetOriginalValue<string>();
        public bool StreetNumberIsChanged => GetIsChanged();
        #endregion property StreetNumber




    }
}
