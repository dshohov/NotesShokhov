using Microsoft.AspNetCore.SignalR;

namespace NotesShokhov.Helpers
{
    public class NoteHubSignalR : Hub
    {
        public async Task SendNewNote(string message)
        {
            // Отправляем уведомление всем подключенным клиентам
            await Clients.All.SendAsync("ReceiveNewNote", message);
        }
        public async Task SendUpdateMessage(string message)
        {
            // Отправляем уведомление всем подключенным клиентам
            await Clients.All.SendAsync("ReceiveUpdateNoteMessage", message);
        }
    }
}
