 /* Class FriendWrapper
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
  public partial class FriendWrapper : ModelWrapper<Friend>
  {
    public FriendWrapper(Friend model) : base(model)
    {
    }

    #region property Id
    public System.Int32 Id 	{ get => GetProperty<System.Int32>(); set => SetProperty(value); }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));
    #endregion property Id


    #region property FriendGroupId
    public System.Int32 FriendGroupId 	{ get => GetProperty<System.Int32>(); set => SetProperty(value); }
    public System.Int32 FriendGroupIdOriginalValue => GetOriginalValue<System.Int32>(nameof(FriendGroupId));
    public bool FriendGroupIdIsChanged => GetIsChanged(nameof(FriendGroupId));
    #endregion property FriendGroupId


    #region property FirstName
    public System.String FirstName 	{ get => GetProperty<System.String>(); set => SetProperty(value); }
    public System.String FirstNameOriginalValue => GetOriginalValue<System.String>(nameof(FirstName));
    public bool FirstNameIsChanged => GetIsChanged(nameof(FirstName));
    #endregion property FirstName


    #region property LastName
    public System.String LastName 	{ get => GetProperty<System.String>(); set => SetProperty(value); }
    public System.String LastNameOriginalValue => GetOriginalValue<System.String>(nameof(LastName));
    public bool LastNameIsChanged => GetIsChanged(nameof(LastName));
    #endregion property LastName


    #region property Birthday
    public System.Nullable<System.DateTime> Birthday 	{ get => GetProperty<System.Nullable<System.DateTime>>(); set => SetProperty(value); }
    public System.Nullable<System.DateTime> BirthdayOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(Birthday));
    public bool BirthdayIsChanged => GetIsChanged(nameof(Birthday));
    #endregion property Birthday


    #region property IsDeveloper
    public System.Boolean IsDeveloper 	{ get => GetProperty<System.Boolean>(); set => SetProperty(value); }
    public System.Boolean IsDeveloperOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsDeveloper));
    public bool IsDeveloperIsChanged => GetIsChanged(nameof(IsDeveloper));
    #endregion property IsDeveloper

    public AddressWrapper Address { get; private set; }

    public ChangeTrackingCollection<FriendEmailWrapper> Emails { get; private set; }

    
    protected override void InitializeComplexProperties(Friend model)
    {
      if (model.Address == null)
      {
        throw new ArgumentException("Address cannot be null");
      }
      Address = new AddressWrapper(model.Address);
      RegisterComplex(Address);
    }

    protected override void InitializeCollectionProperties(Friend model)
    {
      if (model.Emails == null)
      {
        throw new ArgumentException("Emails cannot be null");
      }
 
      Emails = new ChangeTrackingCollection<FriendEmailWrapper>(
        model.Emails.Select(e => new FriendEmailWrapper(e)));
      RegisterCollection(Emails, model.Emails);
    }
  }
}
