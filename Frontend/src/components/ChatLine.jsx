function ChatLine(props) {
  const time = props.time
  const message = props.message
  const sender = props.sender
  const receiver = props.receiver

  if (props.right === 1) {
    return (
      <div className="direct-chat-msg right">
        <div className="direct-chat-infos clearfix">
          <span className="direct-chat-name float-right">{sender}</span>
          <span className="direct-chat-timestamp float-left">{time}</span>
        </div>
        <div className="direct-chat-text">
          {message}
        </div>
      </div>
    );
  } else {
    return (
      <div className="direct-chat-msg">
        <div className="direct-chat-infos clearfix">
          <span className="direct-chat-name float-left">{receiver}</span>
          <span className="direct-chat-timestamp float-right">{time}</span>
        </div>
        <div className="direct-chat-text">
          {message}
        </div>
      </div>
    );
  }
}

export default ChatLine;