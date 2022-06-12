export function getStorgeItem(key) {
  return JSON.parse(sessionStorage.getItem(key));
}

export function setStorgeItem(key, value) {
  sessionStorage.setItem(key, JSON.stringify(value));
  return JSON.parse(sessionStorage.getItem(key))
}

export function deleteStorgeItem(key) {
  sessionStorage.removeItem(key);
  return true;
}

export function clearStorge() {
  sessionStorage.clear();
  return true;
}