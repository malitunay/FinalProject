import './App.css';
import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";
import Dashboard from './views/Dashboard';
import Login from './views/login/Index';
import ForgetPassword from './views/login/ForgetPassword';
import Users from './views/user/Index';
import SingleUser from './views/user/Single';
import Invoices from './views/invoice/Index';
import SingleInvoices from './views/invoice/Single';
import Apartments from './views/apertment/Index';
import SingleApartment from './views/apertment/Single';
import Payment from './views/payment/Index';
import Main from './layouts/Main';
import ChangePassword from './views/user/ChangePassword';
import ToAll from './views/invoice/ToAll';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />}></Route>
        <Route path="/forget-password" element={<ForgetPassword />}></Route>
        <Route path="/" element={<Main />}>
          <Route path="/" element={<Dashboard />}></Route>
          <Route path="users" element={<Users />}></Route>
          <Route path="users/:id" element={<SingleUser />}></Route>
          <Route path="change-password" element={<ChangePassword />}></Route>
          <Route path="invoices" element={<Invoices />}></Route>
          <Route path="invoices/:id" element={<SingleInvoices />}></Route>
          <Route path="invoices-to-all" element={<ToAll />}></Route>
          <Route path="apartments" element={<Apartments />}></Route>
          <Route path="apartments/:id" element={<SingleApartment />}></Route>
          <Route path="payment/:id" element={<Payment />}></Route>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
