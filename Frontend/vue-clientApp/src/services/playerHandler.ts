import axios from 'axios';

export const fetchPlayerById = (username: string, accessToken: any) => {
    return axios.get(`https://localhost:7299/api/players/${username}`, {
        headers: { 'Authorization': `Bearer ${accessToken}` },
    });
}