const searchInput = document.getElementById('searchInput');
const listItems = document.querySelectorAll('.note');
searchInput.addEventListener('input', function () {
    const searchText = searchInput.value.trim().toLowerCase();

    listItems.forEach(function (item) {
        const titleElement = item.querySelector('.myNoteTitle');
        const textElement = item.querySelector('.myNoteText');
        // Checking for elements
        if (titleElement && textElement) {
            const title = titleElement.textContent.toLowerCase();
            const text = textElement.textContent.toLowerCase();
            const isVisible = title.includes(searchText) || text.includes(searchText);
            item.style.display = isVisible ? 'flex' : 'none';
        } else {
            // If no elements are found, hide the element
            item.style.display = 'none';
        }
    });

});