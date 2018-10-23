import axios from 'axios'

// axios.defaults.baseURL = 'http://localhost:60908/api/roster';
//
axios.interceptors.request.use(function(config) {
  if (typeof window === 'undefined') return config;
  
    const token = window.localStorage.getItem('jwtToken');
    if (token) {
     // config.headers.Authorization = token;
     config.headers.Authorization = 'Bearer ' + token;
    }
  
    return config;
  })

const rosterService = {
  create(cycle) {
    let uri = 'http://localhost:60908/api/roster/create';
  
    return new Promise((resolve) => {
      axios.post(uri, { cycle: cycle })
        .then((response) => {
          resolve(response);
          })
        .catch((error) => {
          return error;
        });
      })
  },

  createForNewMembers() {
    let uri = 'http://localhost:60908/api/roster/create/newmembers';
  
    return new Promise((resolve) => {
      axios.post(uri)
        .then((response) => {
          resolve(response);
          })
        .catch((error) => {
          return error;
        });
      })
  },

  list() {
    let uri = 'http://localhost:60908/api/roster';

    return new Promise((resolve) => {
      axios.get(uri)
        .then((response) => {
          resolve(response);
          })
        .catch((error) => {
          return error;
        });
      })
  },

  delete(rosterId) {
    let uri = `http://localhost:60908/api/roster/${rosterId}`;
    
    return new Promise((resolve) => {
      axios.delete(uri)
        .then((response) => {
          resolve(response);
          })
        .catch((error) => {
          return error;
        });
      })
  },

  update(roster) {
    let uri = `http://localhost:60908/api/roster/${roster.memberID}`;

    return new Promise((resolve) => {
      axios.put(uri, roster)
        .then((response) => {
          resolve(response);
          })
        .catch((error) => {
          return error;
        });
      })
  }
}

export default rosterService
