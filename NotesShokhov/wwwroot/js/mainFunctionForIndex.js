/*Connection with SignalR*/
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connection.start()
    .then(() => {
        console.log("SignalR connected");
    })
    .catch(error => {
        console.error("SignalR connection failed: " + error);
    });
const notesContainer = document.getElementById("notes");

/*Connection with my method in SignalR, which adds new notes without refreshing the page*/
connection.on("ReceiveNewNote", note => {
    // Create li
    const listItem = document.createElement("li");
    listItem.style.display = "flex";
    listItem.style.backgroundColor = "#C4C4C4";
    listItem.classList.add("note", "mt-3", "w-100", "p-2", "text-bg-dark", "flex-wrap", "align-items-center", "justify-content-between", "justify-content-lg-between");

    //Add html code with note in our list
    listItem.innerHTML = `
            <div class="p-2 w-100 d-flex flex-wrap align-items-center justify-content-between justify-content-lg-between">
                <p class="myNoteTitle my-0 align-self-center" style="width:100px; margin-left: 5%;">${note.title}</p>
                <p class="my-0 align-self-center">
                    Created just now
                </p>
                <div>
                    <button data-id="${note.id}" class="toggleButton btn btn-sm btn-light" data-type="view" style="width:75px; font-size:12px; margin-right:10px">
                        <span>View note</span>
                    </button>
                        <button class="toggleButton btn btn-sm btn-light" style="visibility:hidden; width:75px; font-size:12px;" data-type="edit"><span>Edit note</span></button>

                </div>
            </div>
            <div id="textNote/${note.id}" class="w-100 m-2" style="background-color:#fff;height: 200px; text-align:left; display:none; height:200px; overflow: auto;">
                <p class="myNoteText">${note.text}</p>
            </div>
        `;

    //Get first element in list
    const firstItem = notesContainer.firstElementChild;

    //Insert the last element at the very top
    notesContainer.insertBefore(listItem, firstItem);

    
    const buttons = listItem.querySelectorAll('.toggleButton');

    buttons.forEach(function (button) {
        button.addEventListener('click', function () {
            const id = this.getAttribute('data-id');
            const type = this.getAttribute('data-type');

            toggleNoteVisibility(id, type);
        });
    });
});
/*Connection with my method in SignalR, which send message when one note updating*/
connection.on("ReceiveUpdateNoteMessage", message => {
    const notificationElement = document.getElementById("updateMessage");
    notificationElement.innerHTML = `<h5>${message}</h5>`;
})

/*Function for work "Edit button" and "View button" and "Confirm button"*/
function toggleNoteVisibility(id, type) {
    const titleNote = document.getElementById('titleNote/' + id);
    const confirmButton = document.getElementById('confirmButton/' + id);
    const editTextNote = document.getElementById('editTextNote/' + id);
    const editButton = document.querySelector(`button[data-id="${id}"][data-type="edit"]`);
    const viewButton = document.querySelector(`button[data-id="${id}"][data-type="view"]`);
    const textNote = document.getElementById('textNote/' + id);
    const baseTextTextarea = document.getElementById('baseText/' + id);
    const baseTitleInput = document.getElementById('baseTitle/' + id);
    const formNote = document.getElementById('formNote/' + id);

    if (type == 'view') {
        if (textNote.style.display === 'block') {
            textNote.style.display = 'none';
            viewButton.textContent = 'View note';
        } else {
            textNote.style.display = 'block';
            viewButton.textContent = 'Hide note';
            titleNote.style.display = 'none';
            editTextNote.style.display = 'none';
            formNote.style.display = 'none';
            editButton.textContent = 'Edit note';
        }
    }
    if (type == 'edit') {
        if (formNote.style.display === 'block') {
            formNote.style.display = 'none';
            titleNote.style.display = 'none';
            editTextNote.style.display = 'none';
            confirmButton.style.display = 'none';
            editButton.textContent = 'Edit note';
        } else {
            formNote.style.display = 'block';
            titleNote.style.display = 'block';
            editTextNote.style.display = 'block';
            textNote.style.display = 'none';
            editButton.textContent = 'Cancel';
            viewButton.textContent = 'View note';


            //Functions that show the Confirm button only
            //if there are changes are designed so as not to update records in the database
            //if there are no changes.
            editTextNote.addEventListener('input', function () {
                confirmButton.style.display = (editTextNote.value !== baseTextTextarea.value) ? 'block' : 'none';
            });

            titleNote.addEventListener('input', function () {
                confirmButton.style.display = (titleNote.value !== baseTitleInput.value) ? 'block' : 'none';
            });
        }
    }
}