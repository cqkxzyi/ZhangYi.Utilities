var storage = window.localStorage;


function localStorageSet(name, val) {
    if ((typeof val) === "object") {
        var jsonStr = JSON.stringify(val);
        storage.setItem(name, jsonStr);
    }
    else {
        storage.setItem(name, val);
    }
}

//获取字符串
function localStorageGet(name) {
    return storage.getItem(name);
    //var jsonObj = JSON.parse(json);
    //console.log(typeof jsonObj);
}

//获取json数据
function localStorageGetJson(name) {
    var str = storage.getItem(name);
    if (name) {
        return JSON.parse(str);
    } else {
        return null;
    }
}

function localStorageRemoveItem(name) {
    storage.removeItem(name);
}

function localStorageClearAll() {
    storage.clear();
}


