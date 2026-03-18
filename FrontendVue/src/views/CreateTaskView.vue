<script setup>
import { ref } from 'vue';
import { createTask } from '../services/taskService';
import { useRouter } from 'vue-router';
import { useToast } from 'vue-toastification';

const toast = useToast();
const router = useRouter();

const task = ref({
  title: '',
  description: '',
  dateLimit: ''
})

const handleCreate = async () => {
  try {
    await createTask(task.value)
    toast.success('Tarea creada correctamente');

    goToTasksView();
  } catch (error) {
    console.log(error)
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

</script>


<template>
  <div class="container">
    <main class="d-flex flex-column align-items-center text-start">
      <div class="col-12 col-lg-7">
        <div class="mt-3">
          <h2 class="fs-2 fw-bold m-0">Crear Nueva Tarea</h2>
          <p class="fs-6 text-secondary">Presiona Crear para guardar cambios.</p>
        </div>
        <div class="p-4 bg-white rounded-4">
          <form @submit.prevent="handleCreate" id="updateTaskForm">
            <div class="form-floating mb-4">
              <input id="title" type="text" class="form-control" placeholder="titulo" v-model="task.title">
              <label for="title">Titulo</label>
            </div>
            <div class="form-floating mb-3">
              <textarea id="description" class="form-control text-area" placeholder="descripcion" v-model="task.description"></textarea>
              <label for="description">Descripcion</label>
            </div>
            <div class="mb-2">
              <label for="dateLimit" class="col-form-label">Fecha limite</label>
              <input id="dateLimit" type="date" class="form-control" v-model="task.dateLimit">
            </div>
            <div class="d-flex gap-3 mt-4">
              <button class="btn btn-primary w-75 font-buttons">Crear</button>
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