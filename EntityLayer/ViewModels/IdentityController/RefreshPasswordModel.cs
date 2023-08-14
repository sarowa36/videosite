namespace EntityLayer.ViewModels.IdentityController
{
    public class RefreshPasswordModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Token { get; set; }
    }
}
