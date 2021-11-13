using FriendStorage.Model;
using System.ComponentModel.DataAnnotations;

namespace FriendStorage.UI.Wrapper
{
    public class FriendEmailWrapper : ModelWrapper<FriendEmail>
    {
        public FriendEmailWrapper(FriendEmail model) : base(model) { }

        #region property Id
        public int Id { get => GetProperty<int>(); set => SetProperty(value); }
        public int IdOriginalValue => GetOriginalValue<int>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion property Id

        #region property Email
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not a valid email address")]
        public string Email { get => GetProperty<string>(); set => SetProperty(value); }
        public string EmailOriginalValue => GetOriginalValue<string>(nameof(Email));
        public bool EmailIsChanged => GetIsChanged(nameof(Email));
        #endregion property Email

        #region property Comment
        public string Comment { get => GetProperty<string>(); set => SetProperty(value); }
        public string CommentOriginalValue => GetOriginalValue<string>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        #endregion property Comment


    }
}
