import request from '../utils/request';
import qs from 'qs';


export function CheckPey(params) {
  return request(`/api/Login/CheckPey?${qs.stringify(params)}`,{
      request:"get"
  });
}
export function CheckToken(params) {
  return request(`/api/InvoiceAssistant/Get`,{
    method: "post",
    mode: 'cors',
    traditional: true,
    body:JSON.stringify(params),
  });
}
