var clipboard = new ClipboardJS('.copy');

clipboard.on('success', function (e) {
    alertify.notify('copied', 'success', 5, function () { });
});

clipboard.on('error', function (e) {
    alertify.notify('error', 'error', 5, function () { });
});