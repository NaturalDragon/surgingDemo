
import { parse } from 'qs';
import { Authentication,Regisger } from '../services/Login'
import {message} from 'antd';
import com from '../utils/commom'
export default {
  namespace: 'login',
  state: {
    test:{abc:1232},
    isRegister:false
  },
  subscriptions: {
    sb: function () {
    }
  },
  effects: {
    *Regisger({ payload }, { call, put }){
      
      const { data } = yield call(Regisger, parse({input:payload}))
      if(data&&data.IsSucceed&&data.StatusCode===200){
        
        message.success('注册成功!')
        // yield put({
        //   type:'RegisterToggle'
        // })
      }
      
    },
    *LoginOn({ payload }, { call, put }) {  // eslint-disable-line
     // com.SetCookie('token', payload.username)
     debugger
      const { data } = yield call(Authentication, parse({input:payload}))
      debugger

        if(data&&data.isSucceed){
          com.SetPkey(data.accessToken)
          yield put({
            type:'authorize/Power'
          })
        }else{
          message.error(data.message)
        }
    },
  },
  reducers: {
    save(state, action) {
      return { ...state, ...action.payload };
    },
    RegisterToggle(state, action){
      return { ...state, isRegister:!state.isRegister };
    }
  }
};
