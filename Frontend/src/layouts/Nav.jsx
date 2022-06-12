import { Link } from "react-router-dom";
import { deleteStorgeItem } from "../services/storage";
import NavItem from "../components/NavItem";

function Nav(props) {
  const menuAdmin = [
    {
      title: "Dashboard",
      to: "/",
      icon: "fas fa-tachometer-alt",
      permission: "",
      subMenu: [],
    },
    {
      title: "Users",
      to: "/users",
      icon: "fas fa-users",
      permission: "",
      subMenu: [
        {
          title: "All",
          to: "/users",
          icon: "far fa-circle",
          permission: "",
          subMenu: [],
        },
        {
          title: "New User",
          to: "/users/new",
          icon: "far fa-circle",
          permission: "",
          subMenu: [],
        }
      ],
    },
    {
      title: "Apartments",
      to: "/apartments",
      icon: "fas fa-building",
      permission: "",
      subMenu: [
        {
          title: "All",
          to: "/apartments",
          icon: "far fa-circle",
          permission: "",
          subMenu: [],
        },
        {
          title: "New Apartment",
          to: "/apartments/new",
          icon: "far fa-circle",
          permission: "",
          subMenu: [],
        }
      ],
    },
    {
      title: "Invoices",
      to: "/invoices",
      icon: "fas fa-file-invoice",
      permission: "",
      subMenu: [
        {
          title: "All",
          to: "/invoices",
          icon: "far fa-circle",
          permission: "",
          subMenu: [],
        },
        {
          title: "New Invoice",
          to: "/invoices/new",
          icon: "far fa-circle",
          permission: "",
          subMenu: [],
        },
        {
          title: "Add Invpice To All Apartments",
          to: "/invoices-to-all",
          icon: "far fa-circle",
          permission: "",
          subMenu: [],
        }
      ],
    }
  ];

  const menuUser = [
    {
      title: "Dashboard",
      to: "/",
      icon: "fas fa-tachometer-alt",
      permission: "",
      subMenu: [],
    },
    {
      title: "Change Password",
      to: "/change-password",
      icon: "fas fa-lock",
      permission: "",
      subMenu: [],
    },
    {
      title: "Invoices",
      to: "/invoices",
      icon: "fas fa-file-invoice",
      permission: "",
      subMenu: [],
    }
  ];

  let url = window.location.pathname.split("/")

  let renderedMenu;
  if (props.auth.roleName === 'Admin') {
    renderedMenu = menuAdmin.map((item) =>
      <NavItem url={url} key={item.title} to={item.to} subMenu={item.subMenu} icon={item.icon} title={item.title} />
    )
  } else {
    renderedMenu = menuUser.map((item) =>
      <NavItem url={url} key={item.title} to={item.to} subMenu={item.subMenu} icon={item.icon} title={item.title} />
    )
  }

  const handleLogout = () => {
    deleteStorgeItem('auth');
  }

  return (
    <aside className="main-sidebar sidebar-dark-primary elevation-4">
      <Link to="/" className="brand-link">
        <img src="/dist/img/AdminLTELogo.png" alt="AdminLTE Logo" className="brand-image img-circle elevation-3" style={{ opacity: .8 }} />
        <span className="brand-text font-weight-light">Site Manager</span>
      </Link>

      <div className="sidebar">
        <nav className="mt-3">
          <ul className="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
            {renderedMenu}
            <li className="nav-item">
              <a href="/#" onClick={() => handleLogout()} className="nav-link">
                <i className="nav-icon fas fa-power-off text-danger" />
                <p className="text">Logout</p>
              </a>
            </li>
          </ul>
        </nav>
      </div>
    </aside>
  );
}
export default Nav;