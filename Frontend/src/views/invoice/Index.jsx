import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Script } from "../../components/Script";
import Table from "../../components/Table";
import { getAllInvoices, getInvoicesBySignedUserIdAndInvoiceStatusId, getInvoicesByStatusId } from "../../services/endpointsloc";
import { getStorgeItem } from "../../services/storage";

function Invoices() {
  const auth = getStorgeItem('auth');
  const { toastr } = Script('/plugins/toastr/toastr.min.js', 'toastr')
  const [paid, setPaid] = useState([])
  const [unPaid, setUnpaid] = useState([])
  const keys = [
    'invoicePeriod',
    'invoiceTypeId',
    'invoiceStatusId',
    'invoiceAmount',
    'apartmentId',
    'paymentDateTime'
  ];
  const titles = {
    invoicePeriod: 'Period',
    invoiceTypeId: 'Type',
    invoiceStatusId: 'Status',
    invoiceAmount: 'Amount',
    apartmentId: 'Apartment Id',
    paymentDateTime: 'Payment Date'
  };

  const handleDelete = async (id) => {
    toastr.error("Invoices can not deleted")
  }

  async function fetchData() {
    let response;
    if (auth.dtoLoginUser.rolId === 1) {
      response = await getInvoicesByStatusId(2)
      setPaid(response.data.data)
      response = await getInvoicesByStatusId(1)
      setUnpaid(response.data.data)
    } else {
      response = await getInvoicesBySignedUserIdAndInvoiceStatusId(2)
      setPaid(response.data.data)
      response = await getInvoicesBySignedUserIdAndInvoiceStatusId(1)
      setUnpaid(response.data.data)
    }
  }

  useEffect(() => {
    fetchData()
  }, [])

  if (auth.dtoLoginUser.rolId === 1) {
    return (
      <div className="container-fluid">
        <div className="row">
          <div className="col">
            <div className="card">
              <div className="card-header">
                Paid
              </div>
              <div className="card-body">
                <Table keys={keys} data={paid} titles={titles} url="invoices" handleDelete={handleDelete} />
              </div>
            </div>
          </div>
          <div className="col">
            <div className="card">
              <div className="card-header">
                Unpaid
              </div>
              <div className="card-body">
                <Table keys={keys} data={unPaid} titles={titles} url="invoices" handleDelete={handleDelete} />
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  } else {
    return (
      <div className="container-fluid">
        <div className="row">
          <div className="col">
            <div className="card">
              <div className="card-header">Paid</div>
              <div className="card-body">
                <Table keys={keys} data={paid} titles={titles} url="invoices" handleDelete={handleDelete} />
              </div>
            </div>
          </div>
          <div className="col">
            <div className="card">
              <div className="card-header">Unpaid</div>
              <div className="card-body">
                <Table keys={keys} data={unPaid} titles={titles} url="payment" handleDelete={handleDelete} />
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default Invoices;