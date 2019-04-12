using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Chatman
{
    class MessageDataTemplateSelector : DataTemplateSelector
    {
        private Database db = Database.DbInstance;
        public DataTemplate SentMsgDataTemplate { get; set; }
        public DataTemplate ReceivedMsgDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item,
                   DependencyObject container)
        {
            Message msg = item as Message;
            if (msg.Sender == db.getCurrentUser().Id)
            {
                return SentMsgDataTemplate;
            }
            else
            {
                return ReceivedMsgDataTemplate;
            }
        }
    }
}
