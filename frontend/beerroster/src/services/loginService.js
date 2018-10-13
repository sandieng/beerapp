import axios from 'axios'

axios.interceptors.request.use(function(config) {
  if (typeof window === 'undefined') return config;
  
    const token = window.localStorage.getItem('jwtToken');
    if (token) {
      config.headers.Authorization = token;
    }
  
    return config;
  })

const loginService = {
  login(credentials) {
    let url = 'http://localhost:60908/api/member/login';

    return new Promise((resolve, reject) => {
      axios.post(url, credentials)
      .then((response) => {
        resolve(response);
        })
        .catch((error) => {
          reject(error);
        });
    })
  },
}

export default loginService