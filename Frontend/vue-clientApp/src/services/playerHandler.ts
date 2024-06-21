import axios from 'axios';

export async function fetchPlayerById(username: string, accessToken: any) {
    return axios.get(`https://localhost:7299/api/players/${username}`, {
        headers: { 'Authorization': `Bearer ${accessToken}` },
    });
}