import axios from 'axios';
axios.interceptors.request.use(function(config) {
  if (typeof window === 'undefined') return config;
  
    const token = window.localStorage.getItem('jwtToken');
    if (token) {
      config.headers.Authorization = token;
    }
  
    return config;
  })

const memberService = {
  save(memberDetails) {
    let url = 'http://localhost:60908/api/member/signup';

    //let payloadWithToken = this.attachToken(memberDetails);

    return new Promise((resolve, reject) => {
      axios.post(url, memberDetails)
      .then((response) => {
        resolve(response);
        })
        .catch((error) => {
          reject(error);
        });
    })
  },

  update(updatedMember) {
    let url = `http://localhost:60908/api/member/${updatedMember.id}`;        

    //let payloadWithToken = this.attachToken(updatedMember);

    return new Promise((resolve, reject) => {
      axios.post(url, updatedMember)
      .then((response) => {
        resolve(response);
        })
        .catch((error) => {
          reject(error);
        });
    })
  },

  list() {
    let url = 'http://localhost:60908/api/member';

    return new Promise((resolve, reject) => {
      axios.get(url)
      .then((response) => {
        resolve(response);
        })
        .catch((error) => {
          reject(error);
        });
    })
  },

  attachToken(model) {
    let token =  { token : window.localStorage.getItem('jwtToken') };
    let updatedModel = Object.assign({}, token, model);
    return updatedModel;
  }
}

export default memberService