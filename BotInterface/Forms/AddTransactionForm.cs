using TelegramBotBase.Enums;
using TelegramBotBase.Form;

namespace BotInterface.Forms
{
    internal class AddTransactionForm: AutoCleanForm
    {
        public AddTransactionForm()
        {
            DeleteMode = EDeleteMode.OnLeavingForm;
        }
    }
}
