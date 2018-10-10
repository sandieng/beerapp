import axios from 'axios'

const memberService = {
  save(memberDetails) {
    let url = 'http://localhost:60908/api/member/signup';

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
  }
}

export default memberService