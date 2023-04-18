//export function get() {
//    return document.cookie;
//}

export function get(cname) {
    //let cname = "UserSession";
    let name = cname + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}


export function set(key, value) {
    document.cookie = `${key}=${value}`;
}