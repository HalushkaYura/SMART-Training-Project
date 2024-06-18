// wwwroot/js/site.js
window.copyTextToClipboard = function (text) {
    navigator.clipboard.writeText(text).then(function () {
        console.log('Text copied to clipboard');
    }).catch(function (err) {
        console.error('Error copying text: ', err);
    });
}
