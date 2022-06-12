import axios from "axios";
import { getStorgeItem } from "./storage";

const APP_URL = 'http://localhost:40183'

export const postLogin = async (email, password) => {
  return axios.post(APP_URL + "/api/Account/Login", {
    email: email,
    password: password
  });
}

export const changePassword = async (email, oldPassword, newPassword) => {
  const auth = getStorgeItem('auth');

  return axios.post(APP_URL + "/api/User/ChangePassword", {
    email: email,
    oldPassword: oldPassword,
    newPassword: newPassword
  }, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};

export const getAllActiveUsersList = async () => {
  const auth = getStorgeItem('auth');

  return axios.get(APP_URL + '/api/User/GetAllActiveUsers', {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  });
}


export const addUser = async (userName, name, surname, userTypeId, password, tcnumber, email, phoneNumber, plate, rolId) => {
  const auth = getStorgeItem('auth');

  return axios.post(APP_URL + "/api/User/AddUser", {
    userName: userName,
    name: name,
    surname: surname,
    userTypeId: userTypeId,
    password: password,
    tcnumber: tcnumber,
    email: email,
    phoneNumber: phoneNumber,
    plate: plate,
    rolId: rolId,
    isActive: true
  }, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};

export const updateUser = async (userName, name, surname, userTypeId, password, tcnumber, email, phoneNumber, plate, rolId) => {
  const auth = getStorgeItem('auth');

  return axios.post(APP_URL + "/api/User/UpdateUser", {
    userName: userName,
    name: name,
    surname: surname,
    userTypeId: userTypeId,
    tcnumber: tcnumber,
    password: password,
    email: email,
    phoneNumber: phoneNumber,
    plate: plate,
    rolId: rolId,
    isActive: true
  }, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};

export const deleteUser = async (userId) => {
  const auth = getStorgeItem('auth');

  return axios.put(APP_URL + "/api/User/DeleteUser?userId=" + userId, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};

export const findUser = async (userId) => {
  const auth = getStorgeItem('auth');

  return axios.get(APP_URL + "/api/User/Find?id=" + userId, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};


export const addApartment = async (apartmentBlockNo, apartmentType, apartmentFloor, apartmentNo) => {
  const auth = getStorgeItem('auth');

  return axios.post(APP_URL + "/api/Apartment/AddApartment", {
    apartmentBlockNo: apartmentBlockNo,
    apartmentType: apartmentType,
    apartmentFloor: apartmentFloor,
    apartmentNo: apartmentNo
  }, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};


export const updateApartment = async (apartmentBlockNo, apartmentType, apartmentFloor, apartmentNo) => {
  const auth = getStorgeItem('auth');

  return axios.post(APP_URL + "/api/Apartment/UpdateApartment", {
    apartmentBlockNo: apartmentBlockNo,
    apartmentType: apartmentType,
    apartmentFloor: apartmentFloor,
    apartmentNo: apartmentNo
  }, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};


export const deleteApartment = async (id) => {
  const auth = getStorgeItem('auth');

  return axios.delete(APP_URL + '/api/Apartment/DeleteApartment?id=' + id, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};

export const getAllApartments = async () => {
  const auth = getStorgeItem('auth');

  return axios.get(APP_URL + "/api/Apartment/GetAll", {}, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
}

export const findApartment = async (id) => {
  const auth = getStorgeItem('auth');

  return axios.get(APP_URL + "/api/Apartment/Find?id=" + id, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
}

export const assignApartmentToUser = async (Id, UserId) => {
  const auth = getStorgeItem('auth');

  return axios.post(APP_URL + "/api/Apartment/AssignApartmentToUser", {
    Id,
    UserId
  }, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};


export const addInvoiceToApartment = async (invoicePeriod, invoiceTypeId, invoiceAmount, apartmentId, paymentDateTime) => {
  const auth = getStorgeItem('auth');

  return axios.post(APP_URL + "/api/Invoice/AddInvoiceToApartment", {
    invoicePeriod: invoicePeriod,
    invoiceTypeId: invoiceTypeId,
    invoiceAmount: invoiceAmount,
    apartmentId: apartmentId,
    paymentDateTime: paymentDateTime
  }, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};



export const addInvoiceToAllApartments = async (invoicePeriod, invoiceTypeId, invoiceAmount, paymentDateTime) => {
  const auth = getStorgeItem('auth');

  return axios.post(APP_URL + "/api/Invoice/AddInvoiceToAllApartments", {
    invoicePeriod: invoicePeriod,
    invoiceTypeId: invoiceTypeId,
    invoiceAmount: invoiceAmount,
    paymentDateTime: paymentDateTime
  }, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};



export const getInvoicesByStatusId = async (invoiceStatusId) => {
  const auth = getStorgeItem('auth');

  return axios.get(APP_URL + '/api/Invoice/GetInvoicesByStatusId?invoiceStatusId=' + invoiceStatusId, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};

export const getAllInvoices = async () => {
  const auth = getStorgeItem('auth');

  return axios.get(APP_URL + '/api/Invoice/GetAll', {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};

export const getInvoice = async (id) => {
  const auth = getStorgeItem('auth');

  return axios.get(APP_URL + '/api/Invoice/Find?id=' + id, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};


export const getInvoicesBySignedUserIdAndInvoiceStatusId = async (invoiceStatusId) => {
  const auth = getStorgeItem('auth');
  return axios.get(APP_URL + '/api/Invoice/GetInvoicesBySignedUserIdAndInvoiceStatusId?invoiceStatusId=' + invoiceStatusId, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};


export const sendMessageToAdmin = async (messageText) => {
  const auth = getStorgeItem('auth');

  return axios.post(APP_URL + "/api/Message/SendMessageToAdmin", {
    messageText: messageText
  }, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};

export const GetAllUsersMessages = async (messageText) => {
  const auth = getStorgeItem('auth');

  return axios.get(APP_URL + "/api/Message/GetAllUsersMessages", {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};


export const sendMessageToUser = async (receiverId, messageText) => {
  const auth = getStorgeItem('auth');

  return axios.post(APP_URL + "/api/Message/SendMessageToUser", {
    receiverId: receiverId,
    messageText: messageText
  }, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};

export const getMessagesByUserId = async (userId) => {
  const auth = getStorgeItem('auth');
  return axios.get(APP_URL + '/api/Message/GetMessagesByUserId?userId=' + userId, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};


export const GetMessagesBySigninUserId = async () => {
  const auth = getStorgeItem('auth');
  return axios.get(APP_URL + '/api/Message/GetMessagesBySigninUserId', {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};

export const GetAllMessages = async () => {
  const auth = getStorgeItem('auth');
  return axios.get(APP_URL + '/api/Message/GetAll', {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};

export const payInvoice = async (id, creditCardNo, creditCardExpireMonth, creditCardExpireYear, creditCardCCV) => {
  const auth = getStorgeItem('auth');

  return axios.post(APP_URL + "/api/Invoice/PayInvoice?invoiceId=" + id, {
    creditCardNo: creditCardNo,
    creditCardExpireMonth: creditCardExpireMonth,
    creditCardExpireYear: creditCardExpireYear,
    creditCardCCV: creditCardCCV
  }, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};

export const getCreditCardInfo = async (userId) => {
  const auth = getStorgeItem('auth');
  return axios.get(APP_URL + '/api/Payment/GetByUserId?userId=' + userId, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
}

export const MarkRead = (relatedUserId) => {
  const auth = getStorgeItem('auth');
  return axios.post(APP_URL + '/api/MarkRead?relatedUserId=' + relatedUserId, {}, {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
}


export const GetMessagesAndUsers = async () => {
  const auth = getStorgeItem('auth');

  return axios.get(APP_URL + "/api/Message/GetMessagesAndUsers", {
    headers: {
      'Authorization': `Bearer ${auth.accessToken}`
    }
  })
};
