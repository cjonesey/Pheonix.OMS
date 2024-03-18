function saveAsFile(fileName, content) {
    var link = document.createElement('a');
    link.download = fileName;
    link.href = 'data:application/octet-stream,' + encodeURIComponent(content);
    document.body.appendChild(link);
    link.click();
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