<script setup>
  const props = defineProps({
    status:String
  })
</script>

<template>
  <h5 class="fs-6 text-white p-2 border border-1 rounded m-sm-0" :class="{ 
    'bg-success': status === 'Completada',
    'bg-secondary': status === 'Pendiente'
    }">{{ status }}</h5>
</template>