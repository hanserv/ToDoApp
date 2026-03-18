<script setup>
import { ref, onMounted } from 'vue';
import TasksFilter from '../components/TasksFilter.vue';
import Task from '../components/Task.vue';
import { useToast } from 'vue-toastification';
import { getTasks,deleteTask,completeTask } from '../services/taskService'

import { useRouter } from 'vue-router';

const toast = useToast();

const tasks = ref([]);
const filter = ref('');

const createTaskModalRef = ref(null);

const router = useRouter();


// Filter switcher
const changeFilter = async (filterValue) => {
  filter.value = filterValue
  await fetchTasks()
}

// Get-all
const fetchTasks = async () =>{
  try {

    let params = {}

    if(filter.value === 'pending') {
      params.status = 'pending'
    }     

    if(filter.value === 'completed') {
      params.status = 'completed'
    }     

    const response = await getTasks(params);
    tasks.value = response.data.reverse();
  } catch (error) {
    toast.error(error.response.data);
  }
}

// Mark as completed
const toggleCheck = async (id) => {
  try {
    await completeTask(id);
    
    const task = tasks.value.find(x => x.id == id);
    task.status = 2; // cambia el status a completado
    
    await fetchTasks();
    toast.success('Se completo la tarea correctamente');
  } catch (error) {
    toast.error(error.response.data);
  }
}


// DELETE
const removeTask = async (id) => {
  try {
    await deleteTask(id);
    toast.success('Se elimino la tarea correctamente');
    await fetchTasks();
  } catch (error) {
    toast.error(error.response.data)
  }
}

const handleCreate = async () => {
  await router.push({ name: 'taskCreate' });
}

const handleUpdate = async (id) => {
  await router.push({ name: 'taskUpdate', params: { id }});
}

onMounted(() => {
  fetchTasks();
})
</script>

<template>

  <main>
    <div class=""><!--bg-->
      <div class="container">
        <!-- end create component -->
        <div class="d-flex flex-column flex-sm-row justify-content-between mt-4">
          <div class="align-content-center">
            <h1 class="fs-2 text-primary m-0 font-secundary">Mis Tareas</h1>
            <p class="fs-6 m-0">Actualmente tienes {{ tasks.length }} tareas registradas.</p>
          </div>
          <div class="align-content-center mt-3 mt-sm-0 col-12 col-sm-3">
            <button class="btn btn-primary fw-semibold col-12 font-buttons"
              @click="handleCreate" 
            >
          <i class="bi bi-plus-circle"/> Nueva Tarea
            </button>
          </div>
        </div>
        
        <!-- Componente filtro tareas -->
        <TasksFilter @change-filter="changeFilter" class="mt-4" />

        <!-- Lista de tareas -->
        <template v-if="tasks.length === 0">
          <h2 class="fs-2 fw-semibold text-center mt-5">No hay tareas para mostrar</h2>
        </template>
        <template v-else>
          <Task v-for="task in tasks" 
          @check-task="toggleCheck(task.id)"
          @delete-task="removeTask(task.id)"
          @update-task="handleUpdate(task.id)"
          :key="task.id" 
          :title="task.title" 
          :description="task.description" 
          :created-at="task.createdAt" 
          :date-limit="task.dateLimit" 
          :status="task.status" 
          />
        </template>
      </div>
    </div>
  </main>

</template>