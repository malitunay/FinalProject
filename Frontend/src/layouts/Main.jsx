import { useEffect } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import { getStorgeItem } from "../services/storage";
import Messages from "./Messages";
import Footer from "./Footer";
import Header from "./Header";
import Nav from "./Nav";

function Main() {
  let navigate = useNavigate()
  let auth = getStorgeItem('auth')

  useEffect(() => {
    if (auth === null) {
      navigate('/login')
    } else {

    }
  })

  if (auth != null) {
    return (
      <>
        <Header />
        <Nav auth={auth.dtoLoginUser} />
        <div className="content-wrapper pt-3">
          <div className="content">
            <Outlet />
          </div>
        </div>
        <Messages />
        <Footer />
      </>
    );
  }
}

export default Main;