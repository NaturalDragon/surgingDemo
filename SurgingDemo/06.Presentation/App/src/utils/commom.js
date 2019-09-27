import config from './config'
function SetCookie(name, value) {
    document.cookie = name + "=" + escape(value) + ";path=/";
}
function GetCookie(objName) {
    var arrStr = document.cookie.split("; ");
    for (var i = 0; i < arrStr.length; i++) {
        var temp = arrStr[i].split("=");
        if (temp[0] == objName) return unescape(temp[1]);
    }
    return "";
}
function Guid() {
    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}
var GetQueryString = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var param = window.location.href.split('?')[1];
    if(!param)return null
    var r =param.match(reg);
    if (r != null) return r[2]; return null;
}
var GetQueryUrlString = function (url,name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var param = url.split('?')[1];
    if(!param)return null
    var r =param.match(reg);
    if (r != null) return r[2]; return null;
}
function SetPkey(data){
    
    SetCookie(config.pkey,data);
}

function GetPkey(){
 return GetCookie(config.pkey)
}


export default {
    GetQueryString: GetQueryString,
    GetQueryUrlString:GetQueryUrlString,
    SetPkey:SetPkey,
    GetPkey:GetPkey,
    SetCookie:SetCookie,
    GetCookie:GetCookie
}