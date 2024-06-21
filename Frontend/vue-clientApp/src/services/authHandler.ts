import { toast } from "@/components/ui/toast";
import axios from "axios";
import { h } from "vue";
import { resetUser } from "./localStorageHandler";
import { isTokenExpired, RefreshToken } from "./tokenHandler";
import { toastMessage } from "@/provider/toastProvider";


export async function registerUser(values: { username: string, score: { points: number } }) {
    const response = await axios.post('https://localhost:7299/api/authentication', values);
    return response.data;
}

export async function loginUser(values: { username: string }) {
    const response = await axios.post('https://localhost:7299/api/authentication/login', values);
    return response.data;
}

export async function logOutUser() {
    const userObj = getUserObjectFromLocalStorage();
    if (!userObj) return;

    const token = {
        accessToken: userObj.accessToken,
        refreshToken: userObj.refreshToken,
    };

    if (typeof token.accessToken === 'string' && isTokenExpired(token.accessToken)) {
        resetUser();
    }

    try {
        await axios.post('https://localhost:7299/api/authentication/logout', token);
        resetUser();
        toastMessage(
            'Logout successful',
            h('div', { class: 'text-wrap' }, 'You have been logged out'),
            5000,
            'success',
        );
    } catch (error) {
        toastMessage(
            'Logout failed',
            h('div', { class: 'text-wrap' }, 'An error occurred while logging out'),
            5000,
            'destructive',
        );
    }
}

function getUserObjectFromLocalStorage() {
    const storedUserObj = localStorage.getItem('UserObject');
    if (!storedUserObj) return null;

    try {
        return JSON.parse(storedUserObj);
    } catch (error) {
        console.error('Error parsing user object from local storage:', error);
        return null;
    }
}