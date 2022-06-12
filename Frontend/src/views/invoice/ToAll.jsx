import { useState } from "react";
import { Script } from "../../components/Script";
import { addInvoiceToAllApartments, getInvoice } from "../../services/endpointsloc";
import { getStorgeItem } from "../../services/storage";

function ToAll() {
  const auth = getStorgeItem('auth');
  const { toastr } = Script('/plugins/toastr/toastr.min.js', 'toastr')
  const [period, setPeriod] = useState('')
  const [type, setType] = useState(1)
  const [amount, setAmount] = useState('')
  const [paymentDate, setPaymentDate] = useState('')

  const handleSubmit = async () => {
    let response
    response = await addInvoiceToAllApartments(period, type, amount, paymentDate)
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
                    <p className="col-sm-2 col-form-label" >Period</p>
                    <div className="col-sm-10">
                      <input type="text" className="form-control" id="period" onChange={(e) => { setPeriod(e.target.value) }} value={period} disabled={auth.dtoLoginUser.rolId !== 1 ? 'disabled' : ''}/>
                    </div>
                  </label>
                </div>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label" >Amount</p>
                    <div className="col-sm-10">
                      <input type="text" className="form-control" id="amount" onChange={(e) => { setAmount(e.target.value) }} value={amount} disabled={auth.dtoLoginUser.rolId !== 1 ? 'disabled' : ''}/>
                    </div>
                  </label>
                </div>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label" >Payment Date</p>
                    <div className="col-sm-10">
                      <input type="date" className="form-control" id="paymentDate" onChange={(e) => { setPaymentDate(e.target.value) }} value={paymentDate} disabled={auth.dtoLoginUser.rolId !== 1 ? 'disabled' : ''}/>
                    </div>
                  </label>
                </div>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label">Type</p>
                    <div className="col-sm-10">
                      <select className="form-control" id="type" onChange={(e) => { setType(e.target.value) }} value={type} disabled={auth.dtoLoginUser.rolId !== 1 ? 'disabled' : ''}>
                        <option value={1}>Natural Gas</option>
                        <option value={2}>Water</option>
                        <option value={3}>Electricity</option>
                        <option value={4}>Dues</option>
                        <option value={5}>Other</option>
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

export default ToAll;