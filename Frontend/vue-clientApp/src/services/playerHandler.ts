import axios from 'axios';

export const fetchPlayerById = (username: string, accessToken: any) => {
    return axios.get(`players/${username}`, {
        headers: { 'Authorization': `Bearer ${accessToken}` },
    });
}