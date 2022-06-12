import ChatLine from "./ChatLine";

function ChatBody(props) {
  let data = props.data ?? []
  let name = props.name
  let auth = props.auth

  const messages = data.map((item) =>
    <ChatLine time={item.time} key={item.time} message={item.messageText} right={auth.dtoLoginUser.id === item.senderId ? 1 : 0} sender={auth.dtoLoginUser.name} receiver={name} />
  )

  return (
    <div className="direct-chat-messages">
      {messages}
    </div>);
}

export default ChatBody;