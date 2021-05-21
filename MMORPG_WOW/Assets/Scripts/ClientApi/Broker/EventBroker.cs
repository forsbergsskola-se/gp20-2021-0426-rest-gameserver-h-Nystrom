using System;
using System.Collections.Generic;

namespace ClientApi.Broker {
    public class EventBroker : IEventBroker {
        private static EventBroker thisInstance;

        public static EventBroker Instance() {
            return thisInstance ??= new EventBroker();
        }

        readonly Dictionary<Type, object> subscribers = new Dictionary<Type, object>();

        public void SubscribeMessage<TMessage>(Action<TMessage> callback) {
            if (subscribers.TryGetValue(typeof(TMessage), out var oldSubscribers)) {
                callback = (oldSubscribers as Action<TMessage>) + callback;
            }

            subscribers[typeof(TMessage)] = callback;
        }

        public void UnsubscribeMessage<TMessage>(Action<TMessage> callback) {
            if (subscribers.TryGetValue(typeof(TMessage), out var oldSubscribers)) {
                callback = (oldSubscribers as Action<TMessage>) - callback;

                if (callback != null)
                    subscribers[typeof(TMessage)] = callback;
                else
                    subscribers.Remove(typeof(TMessage));
            }
        }

        public void SendMessage<TMessage>(TMessage message) {
            if (subscribers.TryGetValue(typeof(TMessage), out var currentSubscribers)) {
                (currentSubscribers as Action<TMessage>)?.Invoke(message);
            }
        }
    }
}