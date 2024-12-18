import Axios from 'axios';
import { showNotification } from './notifications';

export const api = Axios.create({
  baseURL: 'http://localhost:8080',
  withCredentials: true,
});

api.interceptors.response.use(
  (response) => {
    return response.data;
  },
  (error) => {
    const statusCode = error.response?.status;

    if (statusCode !== 401) {
      const errorMessage = error.response?.data?.errorMessage || 'Something went wrong...';

      showNotification({
        type: 'failure',
        title: 'An error occurred',
        message: errorMessage,
      });
    }

    return Promise.reject(error);
  },
);
