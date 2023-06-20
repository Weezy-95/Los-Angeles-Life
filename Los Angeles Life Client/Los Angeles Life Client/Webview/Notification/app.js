function ShowNotify() {
    if ('alt' in window) {
        alt.emit('ShowNotify');
    }
}