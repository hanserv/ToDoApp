<script setup>
import { ref,defineEmits } from 'vue';

const emit = defineEmits(['change-filter'])

const activeButton = ref('all')

const allFilter = 'all';
const pendingFilter = 'pending';
const completedFilter = 'completed';

const changeFilter = () => {
  emit('change-filter',activeButton.value)
}
</script>

<template>
  <div class="btn-group d-flex d-sm-block" role="group">
    <button class="btn btn-outline-dark px-sm-5" :class="{ active: activeButton === 'all' }" @click="activeButton = 'all', changeFilter()">Todas</button>
    <button class="btn btn-outline-dark px-sm-4" :class="{ active: activeButton === 'pending' }" @click="activeButton = 'pending', changeFilter()">Pendientes</button>
    <button class="btn btn-outline-dark px-sm-4" :class="{ active: activeButton === 'completed' }" @click="activeButton = 'completed', changeFilter()">Completadas</button>
  </div>
</template>