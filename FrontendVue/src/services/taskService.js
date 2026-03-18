import axios from 'axios';

const API_URL = 'https://localhost:7103/api/tasks';

const api = axios.create({
  baseURL: API_URL
})

export const getTasks = (params = {}) => {
  return api.get('/', {
    params
  })
}

export const getTask = (id) => {
  return api.get(`/${id}`)
}

export const createTask = (task) => {
  return api.post('/',task)
}

export const updateTask = (id,task) => {
  return api.put(`/${id}`,task)
}

export const deleteTask = (id) => {
  return api.delete(`/${id}`)
}

export const completeTask = (id) => {
  return api.patch(`/${id}/complete`)
}