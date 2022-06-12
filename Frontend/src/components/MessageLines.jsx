
export default function MessagesLines(props) {

  const handleMessage = (id, name) => {
    props.setId(id)
    props.setName(name)
    props.setShow(true)
  }

  const lines = props.data.map((item) =>
    <a key={item.relatedUserId} href="#" onClick={() => { handleMessage(item.relatedUserId, item.name + ' ' +item.surname) }}>
      <div className="position-relative border-bottom p-1">
        <h5>{item.name}</h5>
        {item.messageCount === 0 ? '' : <span className="badge badge-info navbar-badge">{item.messageCount}</span>}
      </div>
    </a>
  )

  return (

    <div className="p-3">
      {lines}
    </div>
  );
}