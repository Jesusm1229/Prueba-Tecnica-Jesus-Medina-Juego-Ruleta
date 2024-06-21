/* eslint-disable no-useless-catch */
import axios from 'axios';

export const postBet = async (values: { category: string; betAmount: number; color: "Red" | "Black"; number?: string | undefined; score?: number | undefined }) => {
    try {
        const response = await axios.post('https://localhost:7299/api/bets', values)

        return response.data;

    } catch (error) {
        throw error;
    }
}