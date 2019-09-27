import request from '../utils/request';
import qs from 'qs';


export async function ApplicationsGetListPaged(params) {
	return request(`/api/Goods/GetPageList`, {
		method: "post",	
		body: JSON.stringify(params)	
	})
}


export async function GetForModify(params) {
	return request(`/api/Goods/GetForModify`, {
		method: "post",	
		body: JSON.stringify(params)	
	})
}

export async function CreateOrder(params){
	return request(`/api/OrderInfo/Create`, {
		method: "post",	
		body: JSON.stringify(params)	
	})
}


