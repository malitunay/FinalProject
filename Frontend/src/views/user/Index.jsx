import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Script } from "../../components/Script";
import Table from "../../components/Table";
import { deleteUser, getAllActiveUsersList } from "../../services/endpointsloc";

function Users() {
  const { toastr } = Script('/plugins/toastr/toastr.min.js', 'toastr')

  const [data, setData] = useState([])
  const keys = [
    'name',
    'surname',
    'email',
    'phoneNumber'
  ];
  const titles = {
    name: 'Name',
    surname: 'Surname',
    email: 'Email',
    phoneNumber: 'Phone Number'
  };

  const handleDelete = async (id) => {
    let response = await deleteUser(id)
    if (response.data.statusCode === 200) {
      toastr.success(response.data.message)
      fetchData()
    } else {
      toastr.error(response.data.message)
    }
  }

  async function fetchData() {
    let response = await getAllActiveUsersList()
    setData(response.data.data)
  }

  useEffect(() => {
    fetchData()
  }, [])

  return (
    <div className="container-fluid">
      <div className="row">
        <div className="col">
          <div className="card">
            <div className="card-header">
              <Link to={'/users/new'} className="btn btn-success btn-sm float-right"><i className="fas fa-plus"></i> Add User</Link>
            </div>
            <div className="card-body">
              <Table keys={keys} data={data} titles={titles} url="users" handleDelete={handleDelete} />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Users;