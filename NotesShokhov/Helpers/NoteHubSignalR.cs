using Microsoft.AspNetCore.SignalR;

namespace NotesShokhov.Helpers
{

    //Class witch help in work with SignalR 
    public class NoteHubSignalR : Hub
    {
        //Send new note other users when one user add new note 
        public async Task SendNewNote(string message)
        {
            await Clients.All.SendAsync("ReceiveNewNote", message);
        }
        //Send message other users when one user change note
        public async Task SendUpdateMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveUpdateNoteMessage", message);
        }
    }
}
