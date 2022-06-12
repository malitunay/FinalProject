import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getCreditCardInfo, payInvoice } from "../../services/endpointsloc";
import { getInvoice } from "../../services/endpointsloc";
import { Script } from "../../components/Script";
import { getStorgeItem } from "../../services/storage";

export default function Payment() {
  const auth = getStorgeItem('auth');
  let params = useParams()
  const { toastr } = Script('/plugins/toastr/toastr.min.js', 'toastr')
  const [cardNo, setCardNo] = useState('')
  const [expireYear, setYear] = useState('')
  const [expireMonth, setMount] = useState('')
  const [ccv, setCcv] = useState('')
  const [invoice, setInvoice] = useState('')
  const [cardInfo, setCardInfo] = useState('')

  const handleSubmit = async () => {
    let response = await payInvoice(params.id, cardNo, expireMonth, expireYear, ccv)
    if (response.data.statusCode === 200) {
      toastr.success(response.data.message)
    } else {
      toastr.error(response.data.message)
    }
  };

  useEffect(() => {
    async function fetchData() {
      let response = await getInvoice(params.id)
      let invoice = response.data.data
      setInvoice(invoice)
    }
    async function fetchCard() {
      let response = await getCreditCardInfo(auth.dtoLoginUser.id)
      let cardInfo = response.data
      setCardInfo(cardInfo)
    }

    fetchCard()
    fetchData()
  }, [params])

  return (
    <div className="container">
      <div className="row">
        <div className="col-6">
          <div className="card">
            <div className="card-body">
              <form>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label" >Card No</p>
                    <div className="col-sm-10">
                      <input type="number" className="form-control" id="cardNo" onChange={(e) => { setCardNo(e.target.value) }} value={cardNo} />
                    </div>
                  </label>
                </div>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label" >Expire Year</p>
                    <div className="col-sm-10">
                      <input type="number" className="form-control" id="expireYear" onChange={(e) => { setYear(e.target.value) }} value={expireYear} />
                    </div>
                  </label>
                </div>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label" >Expire Month</p>
                    <div className="col-sm-10">
                      <input type="number" className="form-control" id="expireMonth" onChange={(e) => { setMount(e.target.value) }} value={expireMonth} />
                    </div>
                  </label>
                </div>
                <div className="form-group">
                  <label className="row">
                    <p className="col-sm-2 col-form-label" >CCV</p>
                    <div className="col-sm-10">
                      <input type="number" className="form-control" id="ccv" onChange={(e) => { setCcv(e.target.value) }} value={ccv} />
                    </div>
                  </label>
                </div>
              </form >
            </div >
          </div >
        </div >
        <div className="col-6">
          <div>
            <label htmlFor="">Invoice Period: </label> {invoice.invoicePeriod}
          </div>
          <div>
            <label htmlFor="">Invoice Amount: </label> {invoice.invoiceAmount}
          </div>
          <hr />
          <div>
            <label htmlFor="">Credit Card No: </label> {cardInfo.creditCardNo}
          </div>
          <div>
            <label htmlFor="">Credit Expire Month: </label> {cardInfo.creditCardExpireMonth}
          </div >
          <div>
            <label htmlFor="">Credit Card Expire Year: </label> {cardInfo.creditCardExpireYear}
          </div >
          <div>
            <label htmlFor="">Credit Card CCV: </label> {cardInfo.creditCardCCV}
          </div >
          <div>
            <label htmlFor="">Credit Card Budget: </label> {cardInfo.creditCardBudget}
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
