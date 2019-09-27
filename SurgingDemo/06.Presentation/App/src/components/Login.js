import React, { PropTypes } from 'react'
import {
  Icon,
  message,
  Button,
  Row,
  Col,
  Form,
  Input,
  Select
} from 'antd'
import config from '../utils/config';
import styles from './Login.less'

const FormItem = Form.Item

function hasError(fieldsError) {
  return Object.keys(fieldsError).some(field => fieldsError[field]);
}
//生成从minNum到maxNum的随机数
function randomNum(minNum,maxNum){ 
  switch(arguments.length){ 
      case 1: 
          return parseInt(Math.random()*minNum+1,10); 
      break; 
      case 2: 
          return parseInt(Math.random()*(maxNum-minNum+1)+minNum,10); 
      break; 
          default: 
              return 0; 
          break; 
  } 
}

class Login extends React.Component {
  componentDidMount() {
    // To disabled submit button at the beginning.

  //  this.props.form.validateFields();
  }
  constructor(props) {

    super(props);
    
  }
  handleOk = () => {
debugger
    this.props.form.validateFieldsAndScroll((errors, values) => {
      if (errors) {
        return
      }
      var random= randomNum(1,1000000000)
    !this.props.isRegister?  this.props.onOk(values):

    //this.props.Regisger(values)
    this.props.Regisger({Name:`lizn${random}`,Password:`password${random}`})

    })
    
  }
  render() {
    const {
    getFieldDecorator,
      validateFieldsAndScroll,
      getFieldsError,
      isFieldTouched,
      getFieldError
  } = this.props.form;
  const {isRegister}=this.props;
    const userNameError = isFieldTouched('username') && getFieldError('userName');
    const passwordError = isFieldTouched('password') && getFieldError('password');
    return (
      <div className={styles.form}>
        <div className={styles.logo}>
          <span>surging demo {!isRegister?"登录":"注册"}</span>
        </div>
        {
          !isRegister?
          <form>
          <FormItem validateStatus={userNameError ? 'error' : ''}
            help={userNameError || ''} hasFeedback>
            {getFieldDecorator('Name', {
              rules: [
                {
                  required: true,
                  message: '请填写用户名'
                }
              ]
            })(<Input prefix={<Icon type="user"></Icon>} size="large" placeholder="用户名" />)}
          </FormItem>
          <FormItem validateStatus={passwordError ? 'error' : ''}
            help={passwordError || ''} hasFeedback>
            {getFieldDecorator('Password', {
              rules: [
                {
                  required: true,
                  message: '请填写密码'
                }
              ]
            })(<Input size="large" prefix={<Icon type="lock" />} type="password" placeholder="密码" />)}
          </FormItem>
          <Row>
            <Button type="primary" size="large" disabled={hasError(getFieldsError())} onClick={this.handleOk} loading={this.props.loginButtonLoading}>
              登录
          </Button>
          <span onClick={this.props.RegisterToggle} className={styles.register}>注册</span>
          </Row>
        </form>:
        <form>
        <FormItem validateStatus={userNameError ? 'error' : ''}
          help={userNameError || ''} hasFeedback>
          {getFieldDecorator('Name', {
            rules: [
              {
                required: true,
                message: '请填写用户名'
              }
            ]
          })(<Input prefix={<Icon type="user"></Icon>} size="large" placeholder="用户名" />)}
        </FormItem>
        <FormItem validateStatus={passwordError ? 'error' : ''}
          help={passwordError || ''} hasFeedback>
          {getFieldDecorator('Password', {
            rules: [
              {
                required: true,
                message: '请填写密码'
              }
            ]
          })(<Input size="large" prefix={<Icon type="lock" />} type="password" placeholder="密码" />)}
        </FormItem>
        <Row>
          <Button type="primary" size="large" disabled={hasError(getFieldsError())} 
          onClick={this.handleOk} loading={this.props.loginButtonLoading}>
            注册
        </Button>
        <span onClick={this.props.RegisterToggle}  className={styles.register}>登录</span>
        </Row>
      </form>
        }
      
      </div>
    )
  }
}
const Login1 = ({
  loginButtonLoading,
  onOk,
  form: {
    getFieldDecorator,
    validateFieldsAndScroll,
    getFieldsError
  }
}) => {

}

// Login.propTypes = {
//   form: PropTypes.object,
//   loginButtonLoading:PropTypes.bool,
//   onOk: PropTypes.func
// }

export default Form.create()(Login)
