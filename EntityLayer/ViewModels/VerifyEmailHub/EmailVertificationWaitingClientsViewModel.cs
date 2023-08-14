namespace EntityLayer.ViewModels.VerifyEmailHub
{
    public class EmailVertificationWaitingClientsViewModel
    {
        public EmailVertificationWaitingClientsViewModel()
        {
            this.Guid = System.Guid.NewGuid().ToString();
        }
        public string ClientId { get; set; }
        public string Email { get; set; }
        public string Guid { get; set; }
    }
}
