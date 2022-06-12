import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Script } from "../../components/Script";
import Table from "../../components/Table";
import { deleteApartment, getAllApartments } from "../../services/endpointsloc";


function Apartments() {
  const { toastr } = Script('/plugins/toastr/toastr.min.js', 'toastr')

  const [data, setData] = useState([])
  const keys = [
    'apartmentBlockNo',
    'apartmentType',
    'apartmentFloor',
    'apartmentNo',
  ];
  const titles = {
    apartmentBlockNo: 'Block No',
    apartmentType: 'Type',
    apartmentFloor: 'Floor',
    apartmentNo: "No",
  };

  const handleDelete = async (id) => {
    let response = await deleteApartment(id)
    if (response.data.statusCode === 200) {
      toastr.success(response.data.message)
      fetchData()
    } else {
      toastr.error(response.data.message)
    }
  }

  async function fetchData() {
    let response = await getAllApartments()
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
              <Link to={'/apartments/new'} className="btn btn-success btn-sm float-right"><i className="fas fa-plus"></i> Add Apartment</Link>
            </div>
            <div className="card-body">
              <Table keys={keys} data={data} titles={titles} url="apartments" handleDelete={handleDelete} />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Apartments;