import axios from 'axios'

axios.interceptors.request.use(function(config) {
  if (typeof window === 'undefined') return config;
  
    const token = window.localStorage.getItem('jwtToken');
    if (token) {
    //  config.headers.Authorization = token;
    config.headers.Authorization = 'Bearer ' + token;
    }
  
    return config;
  })

const emailService = {
  send(email) {
    let url = 'http://localhost:60908/api/email/send'

    return new Promise((resolve, reject) => {
      axios.post(url, email)
      .then((response) => {
        resolve(response);
        })
        .catch((error) => {
          reject(error);
        });
    })
  },

  search(email) {
    let uri = `http://localhost:60908/api/member/${email}`;
    return new Promise((resolve, reject) => {
      axios.get(uri)
      .then((response) => {
        resolve(response);
      })
      .catch((error) => {
        reject(error);
      })
    })
  }
}

export default emailService
