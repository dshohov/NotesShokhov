const buttonNewNote = document.getElementById('newNoteButton');
const blockNewNote = document.getElementById('blockNewNote');

buttonNewNote.addEventListener('click', function () {
    blockNewNote.style.display = (blockNewNote.style.display === 'none') ? 'block' : 'none';
});
