using FriendStorage.Model;

namespace FriendStorage.UI.Wrapper
{
    public class FriendEmailWrapper : ModelWrapper<FriendEmail>
    {
        public FriendEmailWrapper(FriendEmail model) : base(model) { }

        #region property Id
        public int Id { get => GetProperty<int>(); set => SetProperty(value); }
        public int IdOriginalValue => GetOriginalValue<int>();
        public bool IdIsChanged => GetIsChanged();
        #endregion property Id

        #region property Email
        public string Email { get => GetProperty<string>(); set => SetProperty(value); }
        public string EmailOriginalValue => GetOriginalValue<string>();
        public bool EmailIsChanged => GetIsChanged();
        #endregion property Email

        #region property Comment
        public string Comment { get => GetProperty<string>(); set => SetProperty(value); }
        public string CommentOriginalValue => GetOriginalValue<string>();
        public bool CommentIsChanged => GetIsChanged();
        #endregion property Comment


    }
}
