import axios from 'axios'

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