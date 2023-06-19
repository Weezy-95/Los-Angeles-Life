const listItems = document.querySelectorAll('.list-group-item');
let selectedItem = null;

function handleItemHover(event) {
    if (selectedItem !== event.currentTarget) {
        event.currentTarget.classList.add('selected');
    }
}

function handleItemLeave(event) {
    if (selectedItem !== event.currentTarget) {
        event.currentTarget.classList.remove('selected');
    }
}

function handleItemClick(event) {
    if (selectedItem) {
        selectedItem.classList.remove('selected');
    }

    selectedItem = event.currentTarget;
    selectedItem.classList.add('selected');

    const selectedItemId = selectedItem.id;
}

listItems.forEach((item) => {
    item.addEventListener('mouseover', handleItemHover);
    item.addEventListener('mouseleave', handleItemLeave);
    item.addEventListener('click', handleItemClick);
});

function ShowGarage() {
    if ('alt' in window) {
        alt.emit('ShowGarage');
    }
}

const closeButton = document.getElementById('garage-close');

closeButton.addEventListener('click', () => {
    if ('alt' in window) {
        alt.emit('CloseGarageWebView');
    }
});

function ParkIntoGarage() {
    if ('alt' in window) {
        alt.emit('ParkIntoGarage');
    }
}