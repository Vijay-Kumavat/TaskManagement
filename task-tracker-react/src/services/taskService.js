import axios from "axios";

const BASE_URL = "https://localhost:7165/tasks"; // Update the URL as per your API endpoint.

const taskService = {
    getTasks: () => axios.get(BASE_URL),
    addTask: (task) => axios.post(BASE_URL, task),
    updateTask: (taskId, task) => axios.put(`${BASE_URL}/${taskId}`, task),
    deleteTask: (taskId) => axios.delete(`${BASE_URL}/${taskId}`),
};

export default taskService;
