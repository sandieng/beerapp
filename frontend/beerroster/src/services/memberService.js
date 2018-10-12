import axios from 'axios';


const memberService = {
  save(memberDetails) {
    let url = 'http://localhost:60908/api/member/signup';

    let payloadWithToken = this.attachToken(memberDetails);

    return new Promise((resolve, reject) => {
      axios.post(url, payloadWithToken)
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

    let payloadWithToken = this.attachToken(updatedMember);

    return new Promise((resolve, reject) => {
      axios.post(url, payloadWithToken)
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