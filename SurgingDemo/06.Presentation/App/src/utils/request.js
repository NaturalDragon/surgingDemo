import fetch from 'dva/fetch';
import config from './config'
import com from './commom'
import { message } from 'antd'
function toCamel(o) {
  if (typeof (o) == 'string') {
    o = JSON.parse(o)
  }
  var newO, origKey, newKey, value
  if (o instanceof Array) {
    newO = []
    for (origKey in o) {
      value = o[origKey]
      if (typeof value === 'object') {
        value = toCamel(value)
      }
      newO.push(value)
    }
  } else {
    newO = {}
    for (origKey in o) {
      if (o.hasOwnProperty(origKey)) {
        newKey = (origKey.charAt(0).toLowerCase() + origKey.slice(1) || origKey).toString()
        value = o[origKey]
        if (value !== null && (value.constructor === Object || value instanceof Array)) {
          value = toCamel(value)
        }
        newO[newKey] = value
      }
    }
  }
  return newO
}
function parseText(response) {
  return response.text();
}
function parseJSON(text) {
  debugger
  try {
    var json = JSON.parse(text);
    message.destroy()
  
    if (json.hasOwnProperty('entity')) {

      if (json.entity instanceof Object) {
        json.entity = toCamel(json.entity);
        if (json.isValid===false) {
          message.warn(json.errorMessages || json.errorMessages, 2, () => { message.destroy() })
        }
        return json.entity;
      }
      if (json.entity === 'null') {
        return null;
      }
    }

    return json;
  }
  catch (e) {
    return text;
  }
}

function checkStatus(response) {
  if (response.status >= 200 && response.status < 300) {
    return response;
  }

  const error = new Error(response.statusText);
  error.response = response;
  throw error;
}

/**
 * Requests a URL, returning a promise.
 *
 * @param  {string} url       The URL we want to request
 * @param  {object} [options] The options we want to pass to "fetch"
 * @return {object}           An object containing either "data" or "err"
 */
export default function request(url, options) {
  var token=com.GetPkey();
  if (options.headers) {
    options.headers['Accept'] = 'application/json';
    options.headers['Authorization'] = "Bearer "+token
    //options.headers['Content-Type'] = 'application/json;charset=UTF-8';
  } else {
    options.headers = {
      'Content-Type': 'application/json',//'application/json',
      'Authorization': "Bearer "+token
    }
  }


  var httpUrl;

  if(url.indexOf('http')>-1||url.indexOf('http')>-1){
    httpUrl=url;
  }else{
    httpUrl=config.ip+url;
  }
  return fetch(`${httpUrl}`, options)
    .then(checkStatus)
    .then(parseText)
    .then(parseJSON)
    .then(data => ({ data }))
    .catch(err => ({ err }));
}
