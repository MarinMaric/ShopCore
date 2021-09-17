function SetItem(key, value) {
    sessionStorage.setItem(key, value);
}

function GetItem(key) {
    return sessionStorage.getItem(key);
}

function CheckItem(key) {
    var item = GetItem(key);
    if(item=== undefined || item===null)
        return false;
    else return true;
}

function ClearSession() {
    sessionStorage.clear();
}