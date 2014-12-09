using GalaSoft.MvvmLight.Messaging;

namespace OrdiniCatCe.Gui.Messages
{
    public class ErrorMessageMessage : MessageBase
    {
        public string Message { get; private set; }

        public ErrorMessageMessage(string message)
        {
            Message = message;
        }
    }
}
