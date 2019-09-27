import request from '../utils/request';

export async function query() {
  return request('/api/users');
}
export async function Authentication(params) {
	return request(`/api/users/authentication`, {
		method: 'post',
		mode: 'cors',
    traditional: true,
		body: JSON.stringify(params),
	})
}
export async function Regisger(params) {
	return request(`/api/Users/Create`, {
		method: 'post',
		mode: 'cors',
    traditional: true,
		body: JSON.stringify(params),
	})
}