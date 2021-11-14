
 /* Class AddressWrapper
	* 
	* ATTENZIONE:
	* Questo file è stato generato automaticamente. Qualsiasi modifica apportata ad esso verrebbe riscritta alla successiva 
	* rigenerazione del codice.
	* Daniele Prevato, © 2022
	*/


	using System;
	using System.Linq;
	using FriendStorage.Model;

namespace FriendStorage.UI.Wrapper
{
	public partial class AddressWrapper : ModelWrapper<Address> 
	{
		public AddressWrapper(Address model) : base(model) { }


	#region property Id
	public System.Int32 Id
	{
		get => GetProperty<System.Int32>();
		set => SetProperty(value);
	}

	public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
	public bool IdIsChanged => GetIsChanged(nameof(Id));
	#endregion property Id

	#region property City
	public System.String City
	{
		get => GetProperty<System.String>();
		set => SetProperty(value);
	}

	public System.String CityOriginalValue => GetOriginalValue<System.String>(nameof(City));
	public bool CityIsChanged => GetIsChanged(nameof(City));
	#endregion property City

	#region property Street
	public System.String Street
	{
		get => GetProperty<System.String>();
		set => SetProperty(value);
	}

	public System.String StreetOriginalValue => GetOriginalValue<System.String>(nameof(Street));
	public bool StreetIsChanged => GetIsChanged(nameof(Street));
	#endregion property Street

	#region property StreetNumber
	public System.String StreetNumber
	{
		get => GetProperty<System.String>();
		set => SetProperty(value);
	}

	public System.String StreetNumberOriginalValue => GetOriginalValue<System.String>(nameof(StreetNumber));
	public bool StreetNumberIsChanged => GetIsChanged(nameof(StreetNumber));
	#endregion property StreetNumber
	}
}
