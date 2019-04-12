using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Windows;
using Flurl.Http;
using Newtonsoft.Json;

namespace Chatman
{
    public class MessagesViewModel: INotifyPropertyChanged
    {
        private Database db = Database.DbInstance;

        private ObservableCollection<User> users = new ObservableCollection<User>();
        private ObservableCollection<Message> messages = new ObservableCollection<Message>();

        private User currentUser;
        private User selectedUser;

        public RelayCommandParameter CloseWindowCommand { get; set; }
        public RelayCommandParameter OpenRegisterCommand { get; set; }
        public RelayCommandParameter AjouterUserCommand { get; set; }
        public RelayCommandParameter RemoveUserCommand { get; set; }
        public RelayCommandParameter LoginCommand { get; set; }
        public RelayCommandParameter RegisterCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }


        public MessagesViewModel()
        {
            CloseWindowCommand = new RelayCommandParameter(CloseWindow, true);
            OpenRegisterCommand = new RelayCommandParameter(OpenRegister, true);
            AjouterUserCommand = new RelayCommandParameter(AddNewUser, true);
            RemoveUserCommand = new RelayCommandParameter(RemoveUserAtIndex, true);
            LoginCommand = new RelayCommandParameter(LoginAsync, true);
            RegisterCommand = new RelayCommandParameter(RegisterAsync, true);
            SendMessageCommand = new RelayCommand(SendMessageAsync);
        }

        public ObservableCollection<User> Users
        {
            get
            {
                if (users == null)
                    users = new ObservableCollection<User>();
                return users;
            }
            set
            {
                users = value;
            }
        }

        public ObservableCollection<Message> Messages
        {
            get
            {
                if (messages == null)
                    messages = new ObservableCollection<Message>();
                return messages;
            }
            set
            {
                messages = value;
            }
        }

        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                GetMessagesForCurrentUsers();
            }
        }

        public User CurrentUser { get; set; }
        
        public string LoginUsername { get; set; }
        public string LoginPassword { get; set; }

        public string RegisterUsername { get; set; }
        public string RegisterPassword { get; set; }
        public string RegisterConfirmPassword { get; set; }
        
        public string NewUserNickname { get; set; }
        public string NewUserMessage { get; set; }
        public string NewMessageBody { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private async void LoginAsync(Object loginWindow)
        {
            if (LoginUsername != null && LoginPassword != null)
            {
                try
                {
                    db.clearDB();

                    var responseString = await "http://baobab.tokidev.fr/api/login"
                    .PostJsonAsync(new { username = LoginUsername, password = LoginPassword })
                    .ReceiveString();

                    dynamic response = JsonConvert.DeserializeObject(responseString);
                    db.createNewUser(Convert.ToString(response.username), Convert.ToString(response.access_token), true);
                    currentUser = db.getCurrentUser();

                    FetchMessages();

                    MainWindow mainWindow = new MainWindow(this);
                    mainWindow.Show();

                    ((Window)loginWindow).Close();

                }
                catch (FlurlHttpException ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Identifiants incorrects");
                }
            } else
            {
                MessageBox.Show("Identifiants incorrects");
            }
        }

        private async void RegisterAsync(Object registerWindow)
        {
            if (RegisterUsername != null && RegisterPassword != null && RegisterPassword == RegisterConfirmPassword)
            {
                try
                {
                    var responseString = await "http://baobab.tokidev.fr/api/createUser"
                    .PostJsonAsync(new { username = RegisterUsername, password = RegisterPassword })
                    .ReceiveString();

                    ((Window)registerWindow).Close();
                }
                catch (FlurlHttpException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } else
            {
                MessageBox.Show("Identifiants incorrects");
            }
        }

        private async void FetchMessages()
        {
                try
                {
                var responseString = await "http://baobab.tokidev.fr/api/fetchMessages"
                    .WithOAuthBearerToken(currentUser.Token)
                    .GetJsonListAsync();

                User sender;

                foreach (dynamic element in responseString)
                {
                    if (!db.checkUserExists(element.author))
                    {
                        db.createNewUser(element.author, null, false);
                    }

                    sender = db.getUserByNickname(element.author);
                    db.createNewMessage(element.msg, sender.Id, currentUser.Id, element.dateCreated);
                }

                foreach (User user in db.getAllContacts())
                {
                    Users.Add(user);
                }
                }
                catch (FlurlHttpException ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }

        private async void SendMessageAsync()
        {
            if (NewMessageBody != null && NewMessageBody != "" && SelectedUser != null)
            {
                try
                {
                    var sendResponseString = await "http://baobab.tokidev.fr/api/sendMsg"
                        .WithOAuthBearerToken(currentUser.Token)
                        .PostJsonAsync(new { message = NewMessageBody, receiver = SelectedUser.Nickname })
                        .ReceiveString();

                    db.createNewMessage(NewMessageBody, currentUser.Id, SelectedUser.Id, DateTime.Now);
                    GetMessagesForCurrentUsers();

                    Console.WriteLine("--------");
                    Console.WriteLine(sendResponseString);
                    Console.WriteLine("--------");
                }
                catch (FlurlHttpException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void GetMessagesForCurrentUsers()
        {
            List<Message> newMessages = new List<Message>();
            if (SelectedUser != null)
            {
                newMessages = new List<Message>(from msgs in db.getAllMessages()
                              where (msgs.Sender == currentUser.Id && msgs.Receiver == SelectedUser.Id)
                              || (msgs.Sender == SelectedUser.Id && msgs.Receiver == currentUser.Id)
                              orderby msgs.Date ascending
                              select msgs);
            }

            Messages.Clear();
            foreach (Message msg in newMessages)
            {
                Messages.Add(msg);
            }
        }

        private void CloseWindow(Object obj)
        {
            ((Window)obj).Close();
        }

        private void OpenRegister(Object obj)
        {
            Register register = new Register(this);
            register.Show();
        }

        private async void AddNewUser(Object obj)
        {
            if (NewUserNickname != null && NewUserMessage != null)
            {
                try
                {
                    var sendResponseString = await "http://baobab.tokidev.fr/api/sendMsg"
                        .WithOAuthBearerToken(currentUser.Token)
                        .PostJsonAsync(new { message = NewUserMessage, receiver = NewUserNickname })
                        .ReceiveString();

                    Console.WriteLine("--------");
                    Console.WriteLine(sendResponseString);
                    Console.WriteLine("--------");

                    User receiver;

                    if (!db.checkUserExists(NewUserNickname))
                    {
                        db.createNewUser(NewUserNickname, null, false);
                        receiver = db.getUserByNickname(NewUserNickname);
                        Users.Add(receiver);
                    } else
                    {
                        receiver = db.getUserByNickname(NewUserNickname);
                    }

                    db.createNewMessage(NewUserMessage, currentUser.Id, receiver.Id, DateTime.Now);
                }
                catch (FlurlHttpException ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Cet utilisateur n'existe pas");
                }
                CloseWindow(obj);
            }
        }

        public void RemoveUserAtIndex(Object index)
        {
            if ((int)index != -1)
            {
                users.RemoveAt((int)index);
            }
        }
    }
}
