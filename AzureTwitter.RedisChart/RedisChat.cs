using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace RedisChat
{
    class ChatUser
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public ChatUser(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public ChatUser()
        {
            
        }
    }

    [Serializable]
    class ChatMessage
    {
        public ChatUser User { get; set; }
        public string Text { get; set; }

        public ChatMessage(ChatUser user, string text)
        {
            User = user;
            Text = text;
        }
    }

    class RedisChat
    {
        private readonly string _address;
        private readonly string _chatName;
        private readonly ChatUser _user;
        private readonly Action<bool, ChatMessage> _onMessageAction;
        private readonly ISubscriber _subscriber;


        private RedisChat(string address, string userName, Action<bool, ChatMessage> onMessageAction, string chatName = "chatqueue")
        {
            _chatName = chatName;
            _user = new ChatUser(Guid.NewGuid(), userName);
            _onMessageAction = onMessageAction;
            _address = address;
            _subscriber = InitSubscriber();

            Subscribe(_subscriber);
        }

        private ISubscriber InitSubscriber()
        {
            var connection = ConnectionMultiplexer.Connect(_address);
            var subscriber = connection.GetSubscriber();
            return subscriber;
        }

        private void Subscribe(ISubscriber subscriber)
        {
            subscriber.SubscribeAsync(_chatName, (channel, value) =>
            {
                var message = JsonConvert.DeserializeObject<ChatMessage>(value);

                _onMessageAction(message.User.Id == _user.Id, message);
            });
        }
             
        public static RedisChat Create(string address, string userName, Action<bool, ChatMessage> onMessageAction)
        {
            return new RedisChat(address, userName, onMessageAction);
        }

        public void Send(string text)
        {
            var message = new ChatMessage(_user, text);
            _subscriber.Publish(_chatName, JsonConvert.SerializeObject(message));
        }
    }
}
