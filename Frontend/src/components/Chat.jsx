import { useEffect, useState } from "react";
import { getMessagesByUserId, sendMessageToAdmin, sendMessageToUser } from "../services/endpoints";
import { GetMessagesBySigninUserId } from "../services/endpointsloc";
import { getStorgeItem } from "../services/storage";
import ChatBody from "./ChatBody";
import { MarkRead } from "../services/endpointsloc"
import { Script } from "./Script"


function Chat(props) {
  const { toastr } = Script('/plugins/toastr/toastr.min.js', 'toastr')
  const auth = getStorgeItem('auth');

  const [data, setData] = useState([])
  const [text, setText] = useState('')

  let id = auth.dtoLoginUser.rolId === 1 ? props.id : auth.dtoLoginUser.id
  let show = props.show ? 'block' : 'none'

  if (props.show) {
    const fetchMark = async () => {
      let response = await MarkRead(id)
      if (response.data.statusCode !== 200) {
        toastr.error(response.data.message)
      } else {
        //props.getFetch()
      }
    }
  
    fetchMark()
  }

  const sendMessage = () => {
    async function getFetch() {
      console.log(text);
      let response
      if (auth.dtoLoginUser.rolId === 1) {
        response = await sendMessageToUser(id, text)
      } else {
        response = await sendMessageToAdmin(text)
      }
      setText('')
      getData()
    }
    getFetch()
  }

  const getData = async () => {
    let response
    if (auth.dtoLoginUser.rolId === 1) {
      response = await getMessagesByUserId(id)
    } else {
      response = await GetMessagesBySigninUserId()
    }
    setData(response.data.data)
  }

  useEffect(() => {
    getData()
  }, [props, id]);

  return (
    <div className="position-fixed" style={{ right: auth.dtoLoginUser.rolId === 1 ? "250px" : "0px", bottom: auth.dtoLoginUser.rolId === 1 ? "0px" : "50px" }}>
      <div className="card card-info card-outline direct-chat direct-chat-info" style={{ display: show }}>
        <div className="card-header">
          <h3 className="card-title">Direct Chat</h3>

          <div className="card-tools">
            <button type="button" className="btn btn-tool" data-card-widget="collapse">
              <i className="fas fa-minus"></i>
            </button>
            <button type="button" className="btn btn-tool" onClick={() => { props.setShow(false) }}>
              <i className="fas fa-times"></i>
            </button>
          </div>
        </div>
        <div className="card-body" style={{ display: "block" }}>
          <ChatBody data={data} auth={auth} name={props.name} />
        </div>

        <div className="card-footer" style={{ display: "block" }}>
          <form action="#" method="post">
            <div className="input-group">
              <input type="text" name="message" placeholder="Type Message ..." className="form-control" onChange={(e) => { setText(e.target.value) }} value={text}/>
              <span className="input-group-append">
                <button type="button" className="btn btn-info" onClick={sendMessage}>Send</button>
              </span>
            </div>
          </form>
        </div>
      </div>
    </div >
  );
}

export default Chat;