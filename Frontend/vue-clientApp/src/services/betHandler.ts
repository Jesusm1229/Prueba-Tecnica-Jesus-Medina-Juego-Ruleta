/* eslint-disable no-useless-catch */
import type { Bet } from '@/lib/types';
import axios from 'axios';

export const postBet = async (values: Bet) => {
    try {
        const response = await axios.post('bets', values)

        return response.data;

    } catch (error) {
        throw error;
    }
}