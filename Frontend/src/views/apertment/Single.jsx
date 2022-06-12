import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Script } from "../../components/Script";
import { addApartment, findApartment, getAllActiveUsersList, updateApartment } from "../../services/endpointsloc";


function SingleApartment() {
  let params = useParams();
  const { toastr } = Script('/plugins/toastr/toastr.min.js', 'toastr')
  const [block, setBlock] = useState('')
  const [type, setType] = useState('')
  const [floor, setFloor] = useState('')
  const [no, setNo] = useState('')
  const [userid, setuserid] = useState(1)
  const [users, setUsers] = useState([])

  const handleSubmit = async () => {
    let response
    if (params.id !== 'new') {
      response = await updateApartment(params.id, block, type, floor, no, userid)
    } else {
      response = await addApartment(block, type, floor, no)
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
        let response = await findApartment(params.id)
        setBlock(response.data.data.apartmentBlockNo)
        setType(response.data.data.apartmentType)
        setFloor(response.data.data.apartmentFloor)
        setNo(response.data.data.apartmentNo)
        setuserid(response.data.data.userId)
      }

      fetchData()
    }
    async function fetchUser() {
      let response = await getAllActiveUsersList()
      setUsers(response.data.data)
    }
    fetchUser()
  }, [params])

  return (
    <div className="container">
      <div className="row">
        <div className="col">
          <div className="card">
            <div className="card-body">
              <form>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label" >Block No</p>
                    <div className="col-sm-10">
                      <input type="text" className="form-control" id="block" onChange={(e) => { setBlock(e.target.value) }} value={block} />
                    </div>
                  </label>
                </div>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label" >Type</p>
                    <div className="col-sm-10">
                      <input type="text" className="form-control" id="type" onChange={(e) => { setType(e.target.value) }} value={type} />
                    </div>
                  </label>
                </div>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label" >Floor</p>
                    <div className="col-sm-10">
                      <input type="email" className="form-control" id="floor" onChange={(e) => { setFloor(e.target.value) }} value={floor} />
                    </div>
                  </label>
                </div>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label" >No</p>
                    <div className="col-sm-10">
                      <input type="text" className="form-control" id="no" onChange={(e) => { setNo(e.target.value) }} value={no} />
                    </div>
                  </label>
                </div>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label">User</p>
                    <div className="col-sm-10">
                      <select className="form-control" id="userid" onChange={(e) => { setuserid(e.target.value) }} value={userid}>
                        {users.map((item) =>
                          <option value={item.id} key={item.id} > {item.name + ' ' + item.surname}</option >
                        )}
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
  );
}

export default SingleApartment;