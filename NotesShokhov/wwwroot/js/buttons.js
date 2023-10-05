const buttons = document.querySelectorAll('.toggleButton');

buttons.forEach(function (button) {
    button.addEventListener('click', function () {
        const id = this.getAttribute('data-id');
        const type = this.getAttribute('data-type');

        toggleNoteVisibility(id, type);
    });
});

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
            formNote.style.display = 'none'; // Скрываем форму редактирования
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
            // Обработчик изменения поля textarea
            editTextNote.addEventListener('input', function () {
                confirmButton.style.display = (editTextNote.value !== baseTextTextarea.value) ? 'block' : 'none';
            });

            // Обработчик изменения поля title
            titleNote.addEventListener('input', function () {
                confirmButton.style.display = (titleNote.value !== baseTitleInput.value) ? 'block' : 'none';
            });
        }
    }
}