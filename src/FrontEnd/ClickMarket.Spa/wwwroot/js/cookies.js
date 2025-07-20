// cookies.js
function adicionarCookie(chave, valor, expiraEmDias) {
    var expires = "";
    if (expiraEmDias) {
        var date = new Date();
        date.setTime(date.getTime() + (expiraEmDias * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = chave + "=" + (valor || "") + expires + "; path=/";
}

function removerCookie(chave) {
    document.cookie = chave + '=; Max-Age=-99999999;';
}

function obterCookie(chave) {
    var chaveEQ = chave + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(chaveEQ) == 0) return c.substring(chaveEQ.length, c.length);
    }
    return null;
}
