import { defineStore } from 'pinia';
import { ref } from 'vue';
import { getTasks,createTask,updateTask,deleteTask,completeTask } from '../services/taskService'

export const useTaskStore = defineStore('taskStore', () => {
  // const tasks = ref([]);
  // const filter = ref('');

  // const fetchTasks = async () =>{
  //   try {

  //     let params = {}

  //     if(filter.value === 'pending') {
  //       params.status = 'pending'
  //     }     

  //     if(filter.value === 'completed') {
  //       params.status = 'completed'
  //     }     

  //     const response = await getTasks(params);
  //     tasks.value = response.data.reverse();
  //   } catch (error) {
  //     console.log(error)
  //   }
  // }

  // const setFilter = async (newFilter) => {
  //   filter.value = newFilter
  //   await fetchTasks()
  // }

  const addTask = async (task) => {
    try {
      await createTask(task);
    } catch (error) {
      console.log(error)
    }
  }

  const editTask = async (id,task) => {
    try {
      await updateTask(id,task);
    } catch (error) {
      console.log(error)
    }
  }

  const removeTask = async (id) => {
    try {
      await deleteTask(id);
    } catch (error) {
      console.log(error)
    }
  }

  const complete = async (id) => {
    try {
      await completeTask(id);

      const task = tasks.value.find(x => x.id == id);
      task.status = 2; // cambia el status a completado
      
      await fetchTasks();
    } catch (error) {
      console.log(error)
    }
  }

  return {
    tasks,
    fetchTasks,
    addTask,
    editTask,
    removeTask,
    complete,
    setFilter
  }

})