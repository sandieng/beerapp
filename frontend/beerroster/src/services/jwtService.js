import VueJwtDecode from 'vue-jwt-decode';
import moment from 'moment';

const jwtService = {
    getJwtToken() {
        let token =  window.localStorage.getItem('jwtToken');
        return token;
    },

    saveJwtToken(token) {
        window.localStorage.setItem('jwtToken', token);
    },

    removeJwtToken() {
        window.localStorage.removeItem('jwtToken');
    },

    tokenHasExpired(token) {
        let jwtToken = VueJwtDecode.decode(token);
        let unixTimestamp = moment().unix();
        let expiredTime = moment.utc(jwtToken.exp).valueOf();

        if (expiredTime < unixTimestamp)
            return true;
        else
            return false;
    }
}

export default jwtService;