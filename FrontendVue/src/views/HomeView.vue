<script setup>

</script>

<template>

  <main>
    <section class="min-vh-100 d-flex flex-column flex-sm-row align-items-center">
      <div class="col-md-6 ps-3 order-2 order-sm-1 mt-4 mt-sm-0">
        <h1 class="fs-1 fw-bold font-modern">Planifica tus tareas, 
          <span class="text-primary d-block">sé dueño de tu tiempo</span>
        </h1>
        <p class="fs-6 text-secondary">Organiza tus actividades de forma sencilla y mantén el control de lo que realmente importa. Crea, prioriza y completa tus tareas sin complicaciones, para aprovechar mejor tu tiempo cada día</p>

        <RouterLink to="/tasks" class="btn btn-primary p-2 font-buttons px-3">
          Empezar gratuitamente <i class="bi bi-arrow-right" />
        </RouterLink>
      </div>

      <div class="col-md-6 order-1 order-sm-2 mt-4 mt-sm-0">
        <img src="../assets/mockup-app.png" alt="mockup-home" class="pe-lg-3">
      </div>
    </section>
  </main>

</template>

<style scoped>
img {
  width: 100%;
}
</style>