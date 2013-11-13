function skm_LockScreen(str) {
    scroll(0, 0);
    var back = document.getElementById('skm_LockBackground');
    var pane = document.getElementById('skm_LockPane');
    var text = document.getElementById('skm_LockPaneText');

    if (back)
        back.className = 'LockBackground';
    if (pane)
        pane.className = 'LockPane';
    if (text)
        text.innerHTML = str;
}