function saveAsFile(filename, bytesBase64) {
    debugger;
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}

function makeListCheckBoxVisible() {
    let displayVal = 'block';
    var checkBoxes = document.getElementsByClassName('listPage_checkBox');
    if (checkBoxes.length > 0) {
        if (checkBoxes[0].style.display == 'block') {
            displayVal = 'none';
        }
    }
    for (var i = 0; i < checkBoxes.length; i++) {
        if (displayVal == 'none') {
            checkBoxes[i].checked = false;
        }
        checkBoxes[i].style.display = displayVal;
    }
}

window.showDialogFunction = function () {
    document.getElementById('oms-dialog').showModal;
}

