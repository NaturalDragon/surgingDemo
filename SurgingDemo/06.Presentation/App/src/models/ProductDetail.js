
import { parse } from 'qs';
import { GetForModify,CreateOrder } from '../services/Product'
import com from '../utils/commom'
import {message} from 'antd';
export default {
  namespace: 'productDetail',
  state: {
    info:{

    }
  },
  subscriptions: {
    sb: function () {
    }
  },
  effects: {
    *GetForModify({ payload }, { call, put }) {  // eslint-disable-line
      const { data } = yield call(GetForModify, parse({input:payload}))
      debugger
        if(data){
          yield put({
            type:'GetForModifySuc',
            payload:{
                     data:data
            }
          })
        }
    },
    *CreateOrder({ payload }, { call, put }){
      message.loading('处理中...')
      const { data } = yield call(CreateOrder, parse({input:payload}))

      if(data){
          if(data&&data.isValid){
            message.success("购买成功!")
          }
      }
    }
  },
  reducers: {
    GetForModifySuc(state, action) {
      return { ...state, info:action.payload.data };
    }
  }
};
