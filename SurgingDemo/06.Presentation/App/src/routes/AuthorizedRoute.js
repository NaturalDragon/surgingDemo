import React from 'react'
import { Route, Redirect } from 'react-router-dom'
import { connect } from 'react-redux'
import Login from './Login'
import config from '../utils/config'
import com from '../utils/commom'
class AuthorizedRoute extends React.Component {

  componentWillMount() {
    console.log('authore')
  }

  render() {
    const { component: Component, ...rest } = this.props
    debugger
    const { pending, logged,autoLogin}=this.props.authorize
    let token = com.GetQueryUrlString(this.props.location.search,'token')
    let financeSharewxappToken=com.GetCookie('financeSharewxappToken');
    if(token&&financeSharewxappToken!==token){
      com.SetCookie(config.pkey,'')
    }
    var key= com.GetPkey();
    console.log(token)
    if(!key){

      return (
        <Route {...rest} render={props => {
          return  <Login {...props} autoLogin={autoLogin}/>
        }} />
      )
    }else{

      // this.props.dispatch({
      //   type:"authorize/CheckToken",
      //   payload:{userCode:token, }
      // })
      return (
        <Route {...rest} render={props => {
          if (pending) return <div>Loading...</div>
          return logged
            ? <Component {...props} />
            : <Login {...props} autoLogin={autoLogin}/>
        }} />
      )
    }


  }
}

// const stateToProps = ({ authorize }) => ({
//   pending: authorize.pending,
//   logged: authorize.logged
// })
function mapStateToProp({ authorize }) {
	return { authorize };
}
export default connect(mapStateToProp)(AuthorizedRoute)
