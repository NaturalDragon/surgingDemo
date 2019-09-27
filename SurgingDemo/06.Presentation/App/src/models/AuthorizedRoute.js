import com from '../utils/commom'
import { CheckPey, CheckToken } from '../services/AuthorizedRoute';
import { message } from 'antd'
export default {
  namespace: 'authorize',
  state: {
    pending: false,
    autoLogin:false,
    logged: com.GetPkey() ? true : false
  },

  subscriptions: {
    setup({ dispatch, history }) {  // eslint-disable-line

    },
  },

  effects: {
    *CheckToken({ payload }, { call, put }) {
      message.loading('身份认证中...');
     yield  put({
       type:'AutoLoginFn'
     })
      const { data,err } = yield call(CheckToken, { userCode: payload.userCode,
         Platform: 'FinanceShare' });

      if (data) {
                debugger
        if (data.success) {
          com.SetPkey(data.data)
          com.SetCookie('financeSharewxappToken',payload.token)
          yield put({
            type: 'Power'
          })
         
        } else {
          message.fail('登录失败!')
        }

        yield  put({
          type:'AutoLoginFn'
        })
      }

      if(err){
        yield  put({
          type:'AutoLoginFn'
        })
        message.fail('错误')
      }
    },
    *Author({ payload }, { call, put }) {

      yield put({
        type: 'Power'
      })
    },
    *CheckPey({ payload }, { call, put }) {

      const { data } = yield call(CheckPey, { pKey: com.GetPkey() });
      if (!data) {
        com.SetPkey("")
        window.location.href = "/"
      }
    }
  },

  reducers: {
    AutoLoginFn(state, action){
      return { ...state, autoLogin: !state.autoLogin };
    },
    Power(state, action) {

      return { ...state, logged: true };
    }
  },

};
