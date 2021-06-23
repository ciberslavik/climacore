using System.Net.Sockets;

namespace Clima.TcpServer.CoreServer
{
    public class Server
    {
        public Server()
        {
        }

        /// <summary>
        /// Открытие сессии
        /// </summary>
        /// <param name="session">Открываемая сессия</param>
        protected virtual void OnConnecting(Session session) {}
        /// <summary>
        /// Сессия открыта
        /// </summary>
        /// <param name="session">Открытая сессия</param>
        protected virtual void OnConnected(Session session) {}
        /// <summary>
        /// Закрытие сессии
        /// </summary>
        /// <param name="session">Закрываемая сессия</param>
        protected virtual void OnDisconnecting(Session session) {}
        /// <summary>
        /// Сессия закрыта
        /// </summary>
        /// <param name="session">Закрытая сессия</param>
        protected virtual void OnDisconnected(Session session) {}

        /// <summary>
        /// Обработчик сообщений об ошибках
        /// </summary>
        /// <param name="error">Socket error code</param>
        protected virtual void OnError(SocketError error) {}

        internal void OnConnectingInternal(Session session) { OnConnecting(session); }
        internal void OnConnectedInternal(Session session) { OnConnected(session); }
        internal void OnDisconnectingInternal(Session session) { OnDisconnecting(session); }
        internal void OnDisconnectedInternal(Session session) { OnDisconnected(session); }
    }
}