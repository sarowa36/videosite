using EntityLayer.Models.Contents;

namespace EntityLayer.ViewModels.MessageHub
{
    public class MessageViewModel
    {
        public MessageViewModel(Message? msg = null)
        {
            if (msg != null)
            {
                this.Id= msg.Id;
                this.ToId = msg.ToId;
                this.FromId = msg.FromId;
                this.Text = msg.Text;
                this.Date= msg.Date;
            }
        }
        public int Id { get; set; }
        public string ToId { get; set; }
        public string FromId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Message AsMessage()
        {
            return new Message { ToId = ToId, FromId = FromId, Text = Text };
        }
    }
}
