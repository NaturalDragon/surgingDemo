import React, { PropTypes } from 'react';
import { connect } from 'dva';
import ProductDetail from '../components/ProductDetail';

function ProductDetailRoute({ match, dispatch, productDetail,history }) {
   const {info}=productDetail;
    const ProductDetailProps = {
        info,
        GetForModify() {
            debugger
            dispatch({
                type: 'productDetail/GetForModify',
                payload: {id:match.params.id},
              
            })
         
        },
        CreateOrder(item){
            var goodsList=[];
            goodsList.push(item)
            dispatch({
                type: 'productDetail/CreateOrder',
                payload: {goodsRequestDtos:goodsList}
              
            })
        }
    }
    return (
        <ProductDetail {...ProductDetailProps} />
    )
}
//监听属性，建立组件和数据的映射关系
function mapStateToProps({ productDetail }) {
    return { productDetail };
}
//关联model
export default connect(mapStateToProps)(ProductDetailRoute);