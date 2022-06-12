import { useState } from "react";
import { Script } from "../../components/Script";
import { changePassword } from "../../services/endpointsloc";
import { getStorgeItem } from "../../services/storage";

export default function ChangePassword() {
  const auth = getStorgeItem('auth');
  const { toastr } = Script('/plugins/toastr/toastr.min.js', 'toastr')
  const [password, setPassword] = useState('')
  const [rpassword, setRpassword] = useState('')
  const [opassword, setOpassword] = useState('')

  const handleSubmit = async () => {
    if (password !== rpassword) {
      toastr.error('Passwords do not match!')
      return
    }
    if (password === opassword) {
      toastr.error('New password must be different from old password')
      return
    }
    let response = await changePassword(auth.dtoLoginUser.email, opassword, password)
    if (response.data.statusCode === 200) {
      toastr.success(response.data.message)
    } else {
      toastr.error(response.data.message)
    }
  };

  return (
    <div className="container">
      <div className="row">
        <div className="col">
          <div className="card">
            <div className="card-body">
              <form>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label" >Old Password</p>
                    <div className="col-sm-10">
                      <input type="text" className="form-control" id="opassword" onChange={(e) => { setOpassword(e.target.value) }} value={opassword} />
                    </div>
                  </label>
                </div>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label" >New Password</p>
                    <div className="col-sm-10">
                      <input type="text" className="form-control" id="password" onChange={(e) => { setPassword(e.target.value) }} value={password} />
                    </div>
                  </label>
                </div>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label" >Password Repeat</p>
                    <div className="col-sm-10">
                      <input type="text" className="form-control" id="rpassword" onChange={(e) => { setRpassword(e.target.value) }} value={rpassword} />
                    </div>
                  </label>
                </div>
              </form >
            </div >
          </div >
        </div >
      </div >
      <div className="card" id="save-card">
        <div className="card-body">
          <button className="btn btn-success btn-sm float-right" onClick={handleSubmit}>Kaydet</button>
        </div>
      </div>
    </div >
  );
}