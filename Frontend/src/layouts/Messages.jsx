import { useEffect, useState } from "react";
import Chat from "../components/Chat";
import MessagesLines from "../components/MessageLines";
import { GetMessagesAndUsers } from "../services/endpoints";
import { getStorgeItem } from "../services/storage";

function Header() {
  const auth = getStorgeItem('auth');
  const [id, setId] = useState(0)
  const [name, setName] = useState('')
  const [show, setShow] = useState(false)
  const [data, setData] = useState([])

  const getFetch = async () => {
    let response
    if (auth.dtoLoginUser.rolId === 1) {
      response = await GetMessagesAndUsers()
      setData(response.data.data)
    } else {
      setShow(true)
    }
  }

  useEffect(() => {
    getFetch()
  }, [])

  if (auth.dtoLoginUser.rolId === 1) {
    return (
      <>
        <aside className="control-sidebar control-sidebar-dark" style={{overflow: "auto"}}>
          <MessagesLines data={data} setId={setId} setName={setName} setShow={setShow}/>
        </aside>
        <Chat id={id} name={name} show={show} setShow={setShow} getFetch={getFetch}/>
      </>
    );
  } else {
    return (
      <Chat id={id} name={name} show={show} setShow={setShow} getFetch={getFetch}/>
    );
  }
}

export default Header;