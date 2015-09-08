/// <reference name="MicrosoftAjax.js"/>
function getPopupObject(myId) {
    if (document.getElementById(myId)) {
        return document.getElementById(myId);
    }
    else {
        return window.document[myId];
    }
}