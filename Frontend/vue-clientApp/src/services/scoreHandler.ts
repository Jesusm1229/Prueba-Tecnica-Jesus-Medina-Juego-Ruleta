import type { Player } from "@/lib/types";
import axios from "axios";

export async function fetchScoresById(idUsername: string, accessToken: string) {
    return axios.get(`https://localhost:7299/api/players/${idUsername}/scores`, {
        headers: { 'Authorization': `Bearer ${accessToken}` },
    });
}


export async function updatePlayerScore(username: string | null | undefined, scoreId: string | null | undefined, score: number | null | undefined, accessToken: string | null | undefined) {


    const response = await axios.put(`https://localhost:7299/api/players/${username}/scores/${scoreId}`,
        { points: score },
        { headers: { 'Authorization': `Bearer ${accessToken}` } }
    );
    return response.data;

}