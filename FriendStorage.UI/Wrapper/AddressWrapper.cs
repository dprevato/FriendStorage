using FriendStorage.Model;
using System.ComponentModel.DataAnnotations;

namespace FriendStorage.UI.Wrapper
{
    public class AddressWrapper : ModelWrapper<Address>
    {
        public AddressWrapper(Address model) : base(model) { }

        #region property Id
        public int Id { get => GetProperty<int>(); set => SetProperty(value); }
        public int IdOriginalValue => GetOriginalValue<int>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion property Id

        #region property City
        [Required(ErrorMessage = "City is required")]
        public string City { get => GetProperty<string>(); set => SetProperty(value); }
        public string CityOriginalValue => GetOriginalValue<string>(nameof(City));
        public bool CityIsChanged => GetIsChanged(nameof(City));
        #endregion property City

        #region property Street
        public string Street { get => GetProperty<string>(); set => SetProperty(value); }
        public string StreetOriginalValue => GetOriginalValue<string>(nameof(Street));
        public bool StreetIsChanged => GetIsChanged(nameof(Street));
        #endregion property Street

        #region property StreetNumber
        public string StreetNumber { get => GetProperty<string>(); set => SetProperty(value); }
        public string StreetNumberOriginalValue => GetOriginalValue<string>(nameof(StreetNumber));
        public bool StreetNumberIsChanged => GetIsChanged(nameof(StreetNumber));
        #endregion property StreetNumber




    }
}
