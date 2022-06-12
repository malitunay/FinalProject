import React from 'react'
import { useState } from 'react'
import { Link, useNavigate } from "react-router-dom";
import { postLogin } from "../../services/endpoints";
import { setStorgeItem } from '../../services/storage';

function Login() {
  let navigate = useNavigate();

  const [email, setEmail] = useState('malitunay1@gmail.com')
  const [password, setPassword] = useState('123')

  const handleLogin = () => {
    postLogin(email, password).then((response) => {
      console.log(response);
      setStorgeItem('auth', response.data.data);
      if (response.data.data.dtoLoginUser.roleName === 'Admin' || response.data.data.dtoLoginUser.roleName === 'Yönetici') {
        window.location.href = window.location.origin;
        //navigate('/');
      }
    })
  }

  return (
    <div className="login-page">
      <div className="login-box">
        <div className="card card-outline card-primary">
          <div className="card-body">
            <p className="login-box-msg">Sign in to start your session</p>

            <div className="input-group mb-3">
              <input type="email" className="form-control" placeholder="Email" onChange={(e) => { setEmail(e.target.value) }} value={email} />
              <div className="input-group-append">
                <div className="input-group-text">
                  <span className="fas fa-envelope"></span>
                </div>
              </div>
            </div>
            <div className="input-group mb-3">
              <input type="password" className="form-control" placeholder="Password" onChange={(e) => { setPassword(e.target.value) }} value={password} />
              <div className="input-group-append">
                <div className="input-group-text">
                  <span className="fas fa-lock"></span>
                </div>
              </div>
            </div>
            <div className="row">
              <div className="col-8">
                <div className="icheck-primary">
                  <label>
                    <input type="checkbox" id="remember" className='mr-2' />
                    Remember Me
                  </label>
                </div>
              </div>
              <div className="col-4">
                <button type="submit" className="btn btn-primary btn-block" onClick={() => handleLogin()}>Sign In</button>
              </div>
            </div>

            <p className="mb-1">
              <Link to="/forget-password">I forgot my password</Link>
            </p>
          </div>
        </div>
      </div>
    </div >
  );
}

export default Login;