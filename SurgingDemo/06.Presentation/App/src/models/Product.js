import { parse } from 'qs';
import { ApplicationsGetListPaged ,New,Remove} from '../services/Product'
import {pageSize} from '../utils/config'
import { message } from 'antd';
message.config({
  top: 10,
  duration: 3,
  maxCount: 1,
});
export default {
  namespace: 'application',
  state: {
    appList: [],
    appPageInfo: {},
    loading: false,
    


  },
  subscriptions: {

  },
  effects: {
    *Copy({ payload }, { call, put }) {
      message.loading();
      const { data } = yield call(New, parse(payload.data))
      if (data.isValid) {
        message.success("保存成功!")
        yield put({
          type: 'ApplicationsGetListPaged',
          payload: {
            pageIndex:payload.pageIndex,pageSize:pageSize
          }
        })
       
      }
    },
    *Remove({ payload }, { call, put }){
      message.loading('处理中...')
      const { data } = yield call(Remove, payload.item);
      if (data) {
        message.success('处理成功!')
        yield put({
          type: 'ApplicationsGetListPaged',
          payload: {
            pageIndex:payload.pageIndex,pageSize:pageSize
          }
        })
      
      }
    },
    *ApplicationsGetListPaged({ payload }, { call, put }) {
      yield put({
        type: 'Loading'
      })
      message.loading("加载中...")
      const { data } = yield call(ApplicationsGetListPaged, parse({input:payload}));
      if (data) {
debugger
        message.destroy()
        
        yield put({
          type: 'AppGetSuccess',
          payload: {
            pagination: data,
            list: data.data
          }
        })
        yield put({
          type: 'Loading'
        })
      }
    },
  },
  reducers: {
    Loading(state, action) {

      return { ...state, loading: !state.loading }
    },
    ItemOver(state, action) {
      const { item } = action.payload;
      const appList = state.appList;
      appList.forEach(element => {
        if (item.id === element.id) {
          element.hover = true;
        } else {
          element.hover = false;
        }
      });
      return { ...state,appList }
    },
    ItemOut(state, action) {
      const { item } = action.payload;
      const appList = state.appList;
      appList.forEach(element => {
        if (item.id === element.id) {
          element.hover = false;
          element.show = false;
        }
      });
      return { ...state,appList }
    },
    ItemToolShow(state, action){
      const { item } = action.payload;
      const appList = state.appList;
      appList.forEach(element => {
        if (item.id === element.id) {
          element.show = true;
        }
      });
      return { ...state,appList }
    },
    AppGetSuccess(state, action) {
      return { ...state, appList: action.payload.list, appPageInfo: action.payload.pagination }
    },
    querySuccess(state, action) {
      return { ...state, cateGoryList: action.payload.data }
    },
    AddToggle(state, action) {
      return { ...state, addShow: !state.addShow }
    },
  }
}