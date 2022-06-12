import { Link } from "react-router-dom";

function ForgetPassword() {
  return (
    <div className="login-page">
      <div className="login-box">
        <div className="card card-outline card-primary">
          <div className="card-body">
            <p className="login-box-msg">You forgot your password? Here you can easily retrieve a new password.</p>
            <form action="recover-password.html" method="post">
              <div className="input-group mb-3">
                <input type="email" className="form-control" placeholder="Email" />
                <div className="input-group-append">
                  <div className="input-group-text">
                    <span className="fas fa-envelope"></span>
                  </div>
                </div>
              </div>
              <div className="row">
                <div className="col-12">
                  <button type="submit" className="btn btn-primary btn-block">Request new password</button>
                </div>
              </div>
            </form>
            <p className="mt-3 mb-1">
              <Link to="/login">Login</Link>
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ForgetPassword;