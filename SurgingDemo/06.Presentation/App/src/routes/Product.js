import React, { PropTypes } from 'react';
import { connect } from 'dva';
import List from '../components/Product';
import {routerRedux,Router} from 'dva/router';
import {pageSize} from '../utils/config'
import { Guid } from '../utils/commom';
const ApplicationRoute = ({ location, dispatch, application }) => {
  const {
    appList,appPageInfo,loading
    } = application;
    const ApplicationProps={
    appList,
    loading,
    appPageInfo,
    ApplicationsGetListPaged(page){
      debugger
      dispatch({
        type:"application/ApplicationsGetListPaged",
        payload:{
          pageIndex:page,pageSize:pageSize
        }
      })
    },
    AddToggle(){
      // dispatch({
      //   type:'appMain/ChangePanesActiveKey',
      //   payload:
      //   {
      //    activeKey:`/main/application/create`,
      //    title:"新增应用"
      //  }
      // })

      dispatch(routerRedux.push({
        pathname:`/main/application/create`,
       // search:`type=Modify&tabId=1`
      }))
     
    },
    GoDetail(item){
      dispatch(routerRedux.push({
        pathname:`/app/detail/${item.id}`,
       // search:`type=Modify&tabId=${item.id}`
      }))
    },
    AppCateGoryGetAll(){
      dispatch({
        type:"application/AppCateGoryGetAll"
      })
    },
    Copy(Item){
      dispatch({
        type: 'application/Copy',
        payload: {
          pageIndex:appPageInfo.pageIndex,
          data: {
            id: Guid(),
            name: `${Item.name}_副本`,
            icon: Item.icon,
            desc: Item.desc,
            type: 0,
            publishStatus: 0,
            applicationTemplateActionRequests:[]
          }

        }
      })
    },
    Remove(id){
      dispatch({
        type:"application/Remove",
        payload:{
          item:{
            entityIdList:[id]
          },pageIndex:appPageInfo.pageIndex
        }
      })
    },
    ItemOver(item){
      dispatch({
        type:"application/ItemOver",
        payload:{item}
      })
    },
    ItemOut(item){
      dispatch({
        type:"application/ItemOut",
        payload:{item}
      })
    },
    ItemToolShow(item){
      dispatch({
        type:"application/ItemToolShow",
        payload:{item}
      })
    },
    

    }
    return (
      <List {...ApplicationProps}/>
    )
  
}

//监听属性，建立组件和数据的映射关系
function mapStateToProps( {application} ) {
  return { application };
}
//关联model
export default connect(mapStateToProps)(ApplicationRoute);