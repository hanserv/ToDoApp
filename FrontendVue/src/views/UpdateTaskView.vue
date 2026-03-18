<script setup>
import { useToast } from 'vue-toastification';
import { getTask,updateTask } from '../services/taskService'
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';
import { useRouter } from 'vue-router';

const router = useRouter();
const route = useRoute();
const taskId = route.params.id;
const toast = useToast();

const task = ref({
  title: '',
  description: '',
  dateLimit: '',
  status:Number
})

const loadTaskData = async () => {
  try {
    const response = await getTask(taskId);
    task.value.title = response.data.title;
    task.value.description = response.data.description;
    task.value.dateLimit = response.data.dateLimit;
    task.value.status = response.data.status;
  } catch (error) {
    if(error.response.status === 404) {
      toast.error('La tarea no existe');
    }
    else {
      toast.error(error.response.data);
    }
    goToTasksView();
  }
}

const handleUpdate = async () => {
  try {
    await updateTask(taskId,task.value);
    toast.success('La tarea se actualizo correctamente');

    goToTasksView();
  } catch (error) {
    const errors = [];
    const dataErrors = error.response?.data;

    if(dataErrors?.errors) {
      for (const error in dataErrors.errors) {
        errors.push(...dataErrors.errors[error]);
      }
    }
    else {
      toast.error(dataErrors);
    }

    errors.forEach(error => {
      toast.error(error);
    });
  }
}

const goToTasksView = async () => {
  await router.push({name: 'tasks'});
}

onMounted(() => {
  loadTaskData();
})
</script>

<template>
  <div class="container">
    <main class="d-flex flex-column align-items-center text-start">
      <div class="col-12 col-lg-7">
        <div class="mt-3">
          <h2 class="fs-2 fw-bold m-0">Actualizar Tarea</h2>
          <p class="fs-6 text-secondary">Presiona Actualizar para guardar cambios.</p>
        </div>
        <div class="p-4 bg-white rounded-4">
          <form @submit.prevent="handleUpdate" id="updateTaskForm">
            <div class="form-floating mb-4">
              <input id="title" type="text" class="form-control" placeholder="titulo" v-model="task.title" :disabled="task.status === 2">
              <label for="title">Titulo</label>
            </div>
            <div class="form-floating mb-3">
              <textarea id="description" class="form-control text-area" placeholder="descripcion" v-model="task.description" :disabled="task.status === 2"></textarea>
              <label for="description">Descripcion</label>
            </div>
            <div class="mb-2">
              <label for="dateLimit" class="col-form-label">Fecha limite</label>
              <input id="dateLimit" type="date" class="form-control" v-model="task.dateLimit" :disabled="task.status === 2">
            </div>
            <div class="d-flex gap-3 mt-4">
              <button class="btn btn-primary w-75 font-buttons" :disabled="task.status === 2">Actualizar</button>
              <button class="btn btn-secondary w-25 font-buttons" @click="goToTasksView">Cancelar</button>
            </div>
          </form>
        </div>
      </div>
    </main>
  </div>
</template>

<style scoped>
.text-area {
  height: 140px;
}
</style>