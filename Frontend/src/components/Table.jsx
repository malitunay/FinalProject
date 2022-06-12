import { Link } from "react-router-dom"
import { getStorgeItem } from "../services/storage";

function Table(props) {

  const auth = getStorgeItem('auth');
  const titles = props.titles
  const keys = props.keys
  const data = props.data
  const url = props.url

  let renderedKeys = keys.map((key) =>
    <th key={titles[key]}>{titles[key]}</th>
  )
  
  if (url === 'invoices' && auth.dtoLoginUser.rolId === 1) {
      
  } else {
    renderedKeys.push(<th key="Actions">Actions</th>)
  }
  

  const renderRow = (item) => {
    let row = keys.map((key) =>
      <td key={key}>{item[key]}</td>
    )
    if (url === 'invoices' && auth.dtoLoginUser.rolId === 1) {
      
    } else if (url === 'payment') {
      row.push(<td key="id"><Link to={"/" + url + "/" + item['id']}><i className="fas fa-credit-card"></i></Link></td>)
    } else if (url === 'invoices' && auth.dtoLoginUser.rolId !== 1) {
      row.push(<td key="id"><Link to={"/" + url + "/" + item['id']}><i className="fas fa-pencil-alt"></i></Link></td>)
    } else {
      row.push(<td key="id"><Link to={"/" + url + "/" + item['id']}><i className="fas fa-pencil-alt"></i></Link><a className="text-danger ml-3" href="#" onClick={() => { props.handleDelete(item['id']) }}><i className="fas fa-times"></i></a></td>)
    }
    
    return row
  }

  const renderedRows = data.map((item) =>
    <tr key={item['id']}>
      {renderRow(item)}
    </tr>
  )

  return (
    <table id="table" className="table table-bordered table-striped">
      <thead>
        <tr>{renderedKeys}</tr>
      </thead>
      <tbody>
        {renderedRows}
      </tbody>
      <tfoot>
        <tr>{renderedKeys}</tr>
      </tfoot>
    </table>);
}

export default Table;