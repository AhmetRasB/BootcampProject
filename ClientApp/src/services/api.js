import axios from 'axios';

const API_BASE_URL = 'http://localhost:5078/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Bootcamps
export const getBootcamps = () => api.get('/bootcamp');
export const getBootcamp = (id) => api.get(`/bootcamp/${id}`);
export const createBootcamp = (data) => api.post('/bootcamp', data);
export const updateBootcamp = (id, data) => api.put(`/bootcamp/${id}`, data);
export const deleteBootcamp = (id) => api.delete(`/bootcamp/${id}`);

// Applications
export const getApplications = () => api.get('/application');
export const getApplication = (id) => api.get(`/application/${id}`);
export const createApplication = (data) => api.post('/application', data);
export const updateApplicationState = (id, state) => api.put(`/application/${id}/state`, state);
export const deleteApplication = (id) => api.delete(`/application/${id}`);

// Applicants
export const getApplicants = () => api.get('/applicant');
export const getApplicant = (id) => api.get(`/applicant/${id}`);
export const createApplicant = (data) => api.post('/applicant', data);
export const updateApplicant = (id, data) => api.put(`/applicant/${id}`, data);
export const deleteApplicant = (id) => api.delete(`/applicant/${id}`);

// Instructors
export const getInstructors = () => api.get('/instructor');
export const getInstructor = (id) => api.get(`/instructor/${id}`);
export const createInstructor = (data) => api.post('/instructor', data);
export const updateInstructor = (id, data) => api.put(`/instructor/${id}`, data);
export const deleteInstructor = (id) => api.delete(`/instructor/${id}`);

// Auth
export const login = (data) => api.post('/auth/login', data);
export const register = (data) => api.post('/auth/register', data);

// Seed Data
export const seedData = () => api.post('/seed');

export default api; 