import { useEffect, useState } from "react"
import { Script } from "../../components/Script"
import { useParams } from "react-router-dom";
import { addUser, findUser, updateUser } from "../../services/endpointsloc";


function SingleUser() {
  let params = useParams();

  const { toastr } = Script('/plugins/toastr/toastr.min.js', 'toastr')
  const [email, setEmail] = useState('')
  const [userName, setUserName] = useState('')
  const [name, setName] = useState('')
  const [surname, setSurname] = useState('')
  const [tc, setTc] = useState('')
  const [phoneNumber, setPhoneNumber] = useState('')
  const [plate, setPlate] = useState('')
  const [rolId, setRoleId] = useState(2)
  const [userTypeId, setUserTypeId] = useState(0)

  const handleSubmit = async () => {
    if (email === '') {
      toastr.error('Email must be fill')
      return
    }
    if (name === '') {
      toastr.error('First name must be fill')
      return
    }
    let response
    if (params.id !== 'new') {
      response = await updateUser(params.id, userName, name, surname, userTypeId, tc, email, phoneNumber, plate, rolId)
    } else {
      response = await addUser(userName, name, surname, userTypeId, tc, email, phoneNumber, plate, rolId)
    }
    if (response.data.statusCode === 200) {
      toastr.success(response.data.message)
    } else {
      toastr.error(response.data.message)
    }
  };

  useEffect(() => {
    if (params.id !== 'new') {
      async function fetchData() {
        let response = await findUser(params.id)
        setEmail(response.data.data.email)
        setName(response.data.data.name)
        setSurname(response.data.data.surname)
        setUserName(response.data.data.userName)
        setTc(response.data.data.tcnumber)
        setUserTypeId(response.data.data.userTypeId)
        setPhoneNumber(response.data.data.phoneNumber)
        setPlate(response.data.data.plate)
        setRoleId(response.data.data.rolId)
      }

      fetchData()
    }
  }, [params])

  return (
    <>
      <div className="container">
        <div className="row">
          <div className="col">
            <div className="card">
              <div className="card-body">
                <form>
                  <div className="form-group">
                    <label className="row">
                      <p className="col-sm-2 col-form-label" >First Name</p>
                      <div className="col-sm-10">
                        <input type="text" className="form-control" id="name" onChange={(e) => { setName(e.target.value) }} value={name} />
                      </div>
                    </label>
                  </div>
                  <div className="form-group">
                    <label className="row">
                      <p className="col-sm-2 col-form-label" >Last Name</p>
                      <div className="col-sm-10">
                        <input type="text" className="form-control" id="surname" onChange={(e) => { setSurname(e.target.value) }} value={surname} />
                      </div>
                    </label>
                  </div>
                  <div className="form-group">
                    <label className="row">
                      <p className="col-sm-2 col-form-label" >Email</p>
                      <div className="col-sm-10">
                        <input type="email" className="form-control" id="email" onChange={(e) => { setEmail(e.target.value) }} value={email} />
                      </div>
                    </label>
                  </div>
                  <div className="form-group">
                    <label className="row">
                      <p className="col-sm-2 col-form-label" >User Name</p>
                      <div className="col-sm-10">
                        <input type="text" className="form-control" id="name" onChange={(e) => { setUserName(e.target.value) }} value={userName} />
                      </div>
                    </label>
                  </div>
                  <div className="form-group">
                    <label className="row">
                      <p className="col-sm-2 col-form-label" >Tc No</p>
                      <div className="col-sm-10">
                        <input type="number" className="form-control" id="tc" onChange={(e) => { setTc(e.target.value) }} value={tc} />
                      </div>
                    </label>
                  </div >
                  <div className="form-group">
                    <label className="row">
                      <p className="col-sm-2 col-form-label" >Plate</p>
                      <div className="col-sm-10">
                        <input type="text" className="form-control" id="plate" onChange={(e) => { setPlate(e.target.value) }} value={plate} />
                      </div>
                    </label>
                  </div >
                  <div className="form-group">
                    <label className="row">
                      <p className="col-sm-2 col-form-label" >Phone Number</p>
                      <div className="col-sm-10">
                        <input type="text" className="form-control" id="number" onChange={(e) => { setPhoneNumber(e.target.value) }} value={phoneNumber} />
                      </div>
                    </label>
                  </div >
                  <div className="form-group">
                    <label className="row">
                      <p className="col-sm-2 col-form-label">Kullanıcı Tipi</p>
                      <div className="col-sm-10">
                        <select className="form-control" id="userType" onChange={(e) => { setUserTypeId(e.target.value) }} value={userTypeId}>
                          <option value={1}>Kiracı</option>
                          <option value={2}>Ev Sahibi</option>
                          <option value={2}>Diğer</option>
                        </select>
                      </div>
                    </label>
                  </div>
                  <div className="form-group">
                    <label className="row">
                      <p className="col-sm-2 col-form-label">Role</p>
                      <div className="col-sm-10">
                        <select className="form-control" id="role" onChange={(e) => { setRoleId(e.target.value) }} value={rolId}>
                          <option value={1}>Admin</option>
                          <option value={2}>User</option>
                        </select>
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
    </>
  );
}

export default SingleUser;