import { h } from "vue";
import { toastMessage } from "./toastProvider";

export default function handleError(error: any) {

    console.error(error);
    let errorMessage = 'An unexpected error occurred';
    // Check if error is an instance of Error
    if (error instanceof Error) {
        errorMessage = error.message;
    }
    // Further type check if error has a 'response' property
    const axiosError = error as { response?: { status: number; data: any } };

    if (axiosError.response) {
        const { status, data } = axiosError.response;
        errorMessage = `${status}: ${data}`;
    }
    // Use toastMessage with the determined errorMessage
    toastMessage(
        "An error occurred",
        h('div', { class: 'text-wrap' }, errorMessage),
        6000,
        "destructive"
    );

    /* toastMessage('An error occurred', error.response.status + ": " + error.response.data, 5000, "destructive"); */
    /*  toast({
         title: "An error occured",
         description: h('div', { class: ' text-wrap' }, error.response.status + ": " + error.response.data),
         duration: 6000,
         variant: "destructive"
     }) */

}
