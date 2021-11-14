 /* Class FriendEmailWrapper
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
  public partial class FriendEmailWrapper : ModelWrapper<FriendEmail>
  {
    public FriendEmailWrapper(FriendEmail model) : base(model)
    {
    }

    #region property Id
    public System.Int32 Id 	{ get => GetProperty<System.Int32>(); set => SetProperty(value); }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));
    #endregion property Id


    #region property Email
    public System.String Email 	{ get => GetProperty<System.String>(); set => SetProperty(value); }
    public System.String EmailOriginalValue => GetOriginalValue<System.String>(nameof(Email));
    public bool EmailIsChanged => GetIsChanged(nameof(Email));
    #endregion property Email


    #region property Comment
    public System.String Comment 	{ get => GetProperty<System.String>(); set => SetProperty(value); }
    public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
    public bool CommentIsChanged => GetIsChanged(nameof(Comment));
    #endregion property Comment

  }
}
