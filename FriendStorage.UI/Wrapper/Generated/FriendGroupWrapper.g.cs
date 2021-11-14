 /* Class FriendGroupWrapper
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
	public partial class FriendGroupWrapper : ModelWrapper<FriendGroup> 
	{
		public FriendGroupWrapper(FriendGroup model) : base(model) { }


	#region property Id
	public System.Int32 Id
	{
		get => GetProperty<System.Int32>();
		set => SetProperty(value);
	}

	public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
	public bool IdIsChanged => GetIsChanged(nameof(Id));
	#endregion property Id

	#region property Name
	public System.String Name
	{
		get => GetProperty<System.String>();
		set => SetProperty(value);
	}

	public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
	public bool NameIsChanged => GetIsChanged(nameof(Name));
	#endregion property Name
	}
}
