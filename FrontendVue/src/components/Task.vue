<script setup>
import { ref } from 'vue';
import TaskStatus from './TaskStatus.vue';

const props = defineProps({
  id:Number,
  title:String,
  description:String,
  createdAt:String,
  dateLimit:String,
  status:Number
})

const emit = defineEmits(['check-task','delete-task','update-task'])

const toggleCheck = () => {
  emit('check-task');
}

const deleteTask = () => {
  emit('delete-task');
}

const updateTask = () => {
  emit('update-task');
}

</script>

<template>
  <article>
    <div class="card my-3" :class="{completed: status === 2}">
      <div class="card-body d-flex align-items-center gap-3">
        <!-- checkbox -->
        <input type="checkbox" class="form-check-input custom-checkbox" @click="toggleCheck()" :checked="status === 2" :disabled="status === 2">

        <div class="w-100">
          <div class="d-flex justify-content-between align-items-center">
            
            <h4 class="card-title fs-5 ">{{ title }}</h4>
            <div class="d-flex flex-column align-items-center gap-2">
              <TaskStatus :status="status === 1 ? 'Pendiente' : 'Completada'" />
              <div class="d-flex gap-3">
                <i v-if="status !== 2" class="bi bi-pencil-square fs-4 text-primary pointer" @click="updateTask"></i> <!--update icon-->
                <i class="bi bi-trash3 fs-4 text-danger pointer" @click="deleteTask"></i> <!--Delete icon-->
              </div>
            </div>
            
          </div>
          <p v-if="!description" class="card-text fs-6 text-secondary">Sin descripcion</p>
          <p v-else class="card-text fs-6 text-secondary">{{ description }}</p>

          <!-- Footer -->
          <div class="d-sm-flex justify-content-between">
            <p class="card-text fs-6 text-secondary m-0"><i class="bi bi-calendar"></i> Creacion: {{ createdAt }}</p>
            <p class="card-text fs-6 text-secondary m-0"><i class="bi bi-calendar"></i> Vencimiento: {{ dateLimit }}</p>
          </div>
          <!-- Footer -->
        </div>
      </div>
    </div>
  </article>
</template>

<style scoped>
.card:hover {
  background-color: #f8f9fa;
}
.custom-checkbox {
  cursor: pointer;
  transform: scale(1.4);
  border-radius: 50%;
}
.custom-checkbox:hover {
  border-color: #0d6efd;
}
.pointer {
  cursor: pointer;
}
.completed {
  opacity: 0.6;
}
.card.completed:hover {
  background-color: transparent;
}
.completed .card-title {
  text-decoration: line-through;
}
</style>