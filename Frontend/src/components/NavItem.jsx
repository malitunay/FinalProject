import { Link } from "react-router-dom";

function NavItem(props) {
  const toArray = props.to.split("/")

  if (props.subMenu.length === 0) {
    return (
      <li className={`nav-item`}>
        <Link to={props.to} className={`nav-link ${(props.url[1] === toArray[1] && props.url[2] === toArray[2]) ? 'active' : ''}`}>
          <i className={`nav-icon ${props.icon}`}></i>
          <p>
            {props.title}
          </p>
        </Link>
      </li>
    );
  } else {
    const subItems = props.subMenu.map((item) =>
      <NavItem url={props.url} key={item.title} to={item.to} subMenu={item.subMenu} icon={item.icon} title={item.title} />
    )
    return (
      <>
        <li className={`nav-item ${(props.url[1] === toArray[1]) ? 'menu-open' : ''}`}>
          <a href="#" className={`nav-link ${(props.url[1] === toArray[1]) ? 'active' : ''}`}>
            <i className={`nav-icon ${props.icon}`}></i>
            <p>
              {props.title}
              <i className="right fas fa-angle-left"></i>
            </p>
          </a>
          <ul className="nav nav-treeview">
            {subItems}
          </ul>
        </li>
      </>
    );
  }
}
export default NavItem;