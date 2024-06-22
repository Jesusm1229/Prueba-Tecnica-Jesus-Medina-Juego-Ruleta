import axios from "axios";

//spinwheel to get the winning number and color
export const spinWheel = async () => {
    const response = await axios.get("https://localhost:7299/api/wheels");
    return response.data;
}